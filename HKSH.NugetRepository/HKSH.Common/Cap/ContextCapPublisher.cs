using ABI.ArtWork.Common;
using DotNetCore.CAP;
using HKSH.Common.Constants;

namespace HKSH.Common.Cap
{
    /// <summary>
    /// ContextCapPublisher
    /// </summary>
    public class ContextCapPublisher : IContextCapPublisher
    {
        /// <summary>
        /// The cap publisher
        /// </summary>
        private readonly ICapPublisher _capPublisher;

        /// <summary>
        /// The current context
        /// </summary>
        private readonly ICurrentContext _currentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextCapPublisher"/> class.
        /// </summary>
        /// <param name="capPublisher">The cap publisher.</param>
        /// <param name="currentContext">The current context.</param>
        public ContextCapPublisher(ICapPublisher capPublisher, ICurrentContext currentContext)
        {
            _capPublisher = capPublisher;
            _currentContext = currentContext;
            ServiceProvider = capPublisher.ServiceProvider;
            Transaction = capPublisher.Transaction;
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        public AsyncLocal<ICapTransaction> Transaction { get; }

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task PublishAsync<T>(string name, T? value, IDictionary<string, string?> headers, CancellationToken cancellationToken = default)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string?>();
            }
            headers.TryAdd(GlobalConstant.CAP_SUBSCRIBE_REQUEST_ID, Guid.NewGuid().ToString());

            return _capPublisher.PublishAsync(name, value, headers, cancellationToken);
        }

        /// <summary>
        /// Asynchronous publish an object message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">the topic name or exchange router key.</param>
        /// <param name="contentObj">message body content, that will be serialized. (can be null)</param>
        /// <param name="callbackName">callback subscriber name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task PublishAsync<T>(string name, T? contentObj, string? callbackName = null, CancellationToken cancellationToken = default) => PublishAsync(name, contentObj, new Dictionary<string, string?>(), cancellationToken);

        /// <summary>
        /// Publishes the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="headers">The headers.</param>
        public void Publish<T>(string name, T? value, IDictionary<string, string?> headers)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string?>();
            }
            headers.TryAdd(GlobalConstant.CAP_SUBSCRIBE_REQUEST_ID, Guid.NewGuid().ToString());

            _capPublisher.Publish(name, value, headers);
        }

        /// <summary>
        /// Publishes the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="contentObj">The content object.</param>
        /// <param name="callbackName">Name of the callback.</param>
        /// <returns></returns>
        public void Publish<T>(string name, T? contentObj, string? callbackName = null) => Publish(name, contentObj, new Dictionary<string, string?>());
    }
}