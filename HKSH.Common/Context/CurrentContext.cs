using HKSH.Common.Caching.Redis;
using HKSH.Common.Constants;
using HKSH.Common.Exceptions;
using HKSH.Common.Extensions;
using HKSH.Common.Repository.Database;
using HKSH.Common.Repository.Database.Privileges;
using HKSH.Common.ShareModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using HKSH.Common.Helper;

namespace HKSH.Common.Context
{
    /// <summary>
    /// CurrentContext
    /// </summary>
    /// <seealso cref="ICurrentContext" />
    /// <seealso cref="IRepositoryCurrentContext" />
    public class CurrentContext : ICurrentContext, IRepositoryCurrentContext
    {
        /// <summary>
        /// http context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// redis repo
        /// </summary>
        private readonly IRedisRepository _redisRepository;

        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<CurrentContext> _logger;

        /// <summary>
        /// The current user
        /// </summary>
        private UserInfoResponse? _currentUser;

        /// <summary>
        /// current user id
        /// </summary>
        private string? _currentUserId;

        /// <summary>
        /// user permissions
        /// </summary>
        private List<UserPrivilegeModule> _userPermissions = new();

        /// <summary>
        /// user current location
        /// </summary>
        private string? _currentLocation;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="redisRepository"></param>
        /// <param name="logger"></param>
        public CurrentContext(IHttpContextAccessor httpContextAccessor, IRedisRepository redisRepository,
            ILogger<CurrentContext> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _redisRepository = redisRepository;
            _logger = logger;
        }

        /// <summary>
        /// current user id
        /// </summary>
        public string CurrentUserId
        {
            get
            {
                if (_currentUserId != null)
                {
                    return _currentUserId;
                }

                var userId = "";

                var tokens = _httpContextAccessor?.HttpContext?.Request.Headers[GlobalConstant.AUTH_HEADER];
                if (tokens.HasValue)
                {
                    var token = _httpContextAccessor?.HttpContext?.Request.Headers[GlobalConstant.AUTH_HEADER][0] ?? "";
                    if (!string.IsNullOrEmpty(token))
                    {
                        userId = JwtHelper.GetUserId(token);
                    }
                }

                if (string.IsNullOrEmpty(userId))
                {
                    var containsUserId =
                        _httpContextAccessor?.HttpContext?.Request.Headers.ContainsKey(GlobalConstant.CURRENT_USER_CODE) ??
                        false;
                    if (containsUserId)
                    {
                        userId = _httpContextAccessor?.HttpContext?.Request.Headers[GlobalConstant.CURRENT_USER_CODE];
                    }
                }

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogExc(
                        $"request headers {JsonSerializer.Serialize(_httpContextAccessor?.HttpContext?.Request.Headers)}");

                    throw new UnAuthorizedException("Error access token");
                }

                _currentUserId = userId;

                return _currentUserId;
            }
        }

        /// <summary>
        /// current user
        /// </summary>
        public UserInfoResponse CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                string userId = CurrentUserId;

                try
                {
                    var claimCurrentUser = _redisRepository.HashGet<UserInfoResponse>(ContextConst.KEY_PATTERN + userId,
                        ContextConst.USER_INFO);
                    if (claimCurrentUser == null || claimCurrentUser.UserId != userId)
                    {
                        throw new UnAuthorizedException("You are not logged in yet");
                    }

                    _currentUser = claimCurrentUser;
                    _currentUser.Permissions = UserPermissions;

                    return _currentUser;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user info from cache err ");
                    throw;
                }
            }
        }

        /// <summary>
        /// current location
        /// </summary>
        public string CurrentLocation
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLocation))
                {
                    return _currentLocation;
                }

                string userId = CurrentUserId;

                try
                {
                    _currentLocation = _redisRepository.HashGet<string>(ContextConst.KEY_PATTERN + userId,
                        ContextConst.LOCATION_INFO);
                    return _currentLocation;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user location info from cache err ");
                    throw;
                }
            }
        }

        /// <summary>
        /// get user permissions
        /// </summary>
        public List<UserPrivilegeModule> UserPermissions
        {
            get
            {
                if (_userPermissions.Count > 0)
                {
                    return _userPermissions;
                }

                string userId = CurrentUserId;

                try
                {
                    _userPermissions =
                        _redisRepository.HashGet<List<UserPrivilegeModule>>(ContextConst.KEY_PATTERN + userId,
                            ContextConst.PRIVILEGE_INFO);
                    return _userPermissions;
                }
                catch (Exception e)
                {
                    _logger.LogExc(e, "Get user privilege info from cache err ");
                    throw;
                }
            }
        }
    }
}