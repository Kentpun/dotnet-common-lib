using DotNetCore.CAP;

namespace HKSH.Common.Cap
{
    /// <summary>
    /// IContextCapPublisher
    /// </summary>
    /// <seealso cref="DotNetCore.CAP.ICapPublisher" />
    public interface IContextCapPublisher : ICapPublisher
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        new IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        new AsyncLocal<ICapTransaction> Transaction { get; }

        /// <summary>
        /// Publishes the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="headers">The headers.</param>
        new void Publish<T>(string name, T value, IDictionary<string, string?> headers);

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        new Task PublishAsync<T>(string name, T value, IDictionary<string, string?> headers, CancellationToken cancellationToken = default);
    }
}