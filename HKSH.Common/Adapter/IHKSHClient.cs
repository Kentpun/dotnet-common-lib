using HKSH.Common.ShareModel;
using System.Text;

namespace HKSH.Common.Adapter
{
    /// <summary>
    /// IHkshClient
    /// </summary>
    public interface IHkshClient
    {
        /// <summary>
        /// 沒有返回值的Get請求.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        Task<MessageResult> Get(string url, IDictionary<string, string> parameters, IDictionary<string, string> headers);

        /// <summary>
        /// 有返回值的Get請求.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        Task<TResult> Get<TResult>(string url, IDictionary<string, string> parameters, IDictionary<string, string> headers);

        /// <summary>
        /// Form表單Post請求.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        Task<TResult> Post<TResult>(string url, IDictionary<string, string> parameters, Dictionary<string, string>? headers = null);

        /// <summary>
        /// 不需要返回值的Post請求.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        Task<MessageResult> Post<T>(string url, T parameters, Dictionary<string, string>? headers = null) where T : class;

        /// <summary>
        /// 有返回值的Post請求.
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        Task<TResult> Post<TParam, TResult>(string url, TParam parameters, Dictionary<string, string>? headers = null, Encoding? encoding = null) where TParam : class;
    }
}