using DotNetCore.CAP.Filter;
using DotNetCore.CAP.Messages;
using HKSH.BaseService.CustomException;
using HKSH.Common.Caching.Redis;
using HKSH.Common.Constants;
using HKSH.Common.Extensions;
using HKSH.Utils.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HKSH.Common.Filters
{
    /// <summary>
    /// CapActionFilter
    /// </summary>
    /// <seealso cref="ISubscribeFilter" />
    public class CapActionFilter : ISubscribeFilter
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IRedisRepository _redisRepository;

        private readonly ILogger<CapActionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CapActionFilter" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="redisRepository">The redis repository.</param>
        public CapActionFilter(IServiceProvider serviceProvider,
            IRedisRepository redisRepository,
            ILogger<CapActionFilter> logger)
        {
            _serviceProvider = serviceProvider;
            _redisRepository = redisRepository;
            _logger = logger;
        }

        /// <summary>
        /// Called before the subscriber executes.
        /// </summary>
        /// <param name="context">The <see cref="T:DotNetCore.CAP.Filter.ExecutingContext" />.</param>
        public void OnSubscribeExecuting(ExecutingContext context)
        {
            // lockKey
            string lockToken = GetLockToken(context.DeliverMessage.Headers);
            string lockKey = $"{RedisLockKeys.LockCapAction}:{lockToken}";

            if (_redisRepository.LockTakeAsync(lockKey, lockToken).Result)
            {
                try
                {
                    //Init httpcontexts
                    if (context.DeliverMessage.Headers != null && context.DeliverMessage.Headers.Count > 0)
                    {
                        //todo need auth server
                        //IHttpContextAccessor httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
                        //httpContextAccessor?.InitHttpContext(context.DeliverMessage.Headers);

                        //if (!context.DeliverMessage.Headers.ContainsKey(ClaimDefined.CapCustomNoNeedAuthHeader) && context.DeliverMessage.Headers.ContainsKey(ClaimDefined.UAccount) && !string.IsNullOrEmpty(context.DeliverMessage.Headers[ClaimDefined.UAccount]))
                        //{
                        //    IServiceInvoker serviceInvoker = _serviceProvider.GetService<IServiceInvoker>();

                        //    // Get login user
                        //    string account = context.DeliverMessage.Headers[ClaimDefined.UAccount];
                        //    IBasicRepository<User> repositoryUser = _serviceProvider.GetService<IBasicRepository<User>>();
                        //    User user = repositoryUser.Entities.FirstOrDefault(a => a.UserName == account);

                        //    // get token
                        //    IInvokeIdentityServer invokeId4 = serviceInvoker.CreateInvoker<IInvokeIdentityServer>();
                        //    MessageResult<Models.LoginResponseModel> loginReponse = invokeId4.BackendAuth(new Models.UserLoginModel { Account = user?.UserName, Password = user?.Password, IsSso = true }).GetAwaiter().GetResult();
                        //    if (loginReponse.Success)
                        //    {
                        //        httpContextAccessor?.HttpContext.Request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"{JwtBearerDefaults.AuthenticationScheme} {loginReponse.Data?.Token}");
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogExc(ex);
                }
            }
            else
            {
                throw new SubscribeActionRepeateException(lockToken);
            }
        }

        /// <summary>
        /// Called after the subscriber executes.
        /// </summary>
        /// <param name="context">The <see cref="T:DotNetCore.CAP.Filter.ExecutedContext" />.</param>
        public void OnSubscribeExecuted(ExecutedContext context)
        {
            context.DeliverMessage.Headers.TryGetValue(Headers.MessageName, out string messageName);
            ILogger<CapActionFilter> logger = _serviceProvider.GetService<ILogger<CapActionFilter>>();
            if (!string.IsNullOrEmpty(messageName))
            {
                logger.LogInformation(messageName);
            }

            string lockToken = GetLockToken(context.DeliverMessage.Headers);
            string lockKey = $"{RedisLockKeys.LockCapAction}:{lockToken}";
            _ = _redisRepository.LockReleaseAsync(lockKey, lockToken).Result;
        }

        /// <summary>
        /// Called after the subscriber has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:DotNetCore.CAP.Filter.ExceptionContext" />.</param>
        public void OnSubscribeException(ExceptionContext context)
        {
            if (context.Exception is SubscribeActionRepeateException)
            {
                _logger.LogExc(context.Exception);
            }
            else
            {
                context.DeliverMessage.Headers.TryGetValue(Headers.MessageName, out string messageName);
                string lockToken = GetLockToken(context.DeliverMessage.Headers);
                string lockKey = $"{RedisLockKeys.LockCapAction}:{lockToken}";
                _ = _redisRepository.LockReleaseAsync(lockKey, lockToken).Result;

                _logger.LogExc(context.Exception);
            }
        }

        /// <summary>
        /// Gets the lock token.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        private static string GetLockToken(IDictionary<string, string?> headers)
        {
            string lockToken = string.Empty;
            headers?.TryGetValue(GlobalConstant.CAP_SUBSCRIBE_REQUEST_ID, out lockToken);
            if (string.IsNullOrEmpty(lockToken))
            {
                headers?.TryGetValue(Headers.MessageId, out lockToken);
            }
            if (string.IsNullOrEmpty(lockToken))
            {
                lockToken = "globle";
            }
            return lockToken;
        }
    }
}