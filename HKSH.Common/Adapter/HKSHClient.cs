using HKSH.Common.Extensions;
using HKSH.Common.ShareModel;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Text;

namespace HKSH.Common.Adapter
{
    /// <summary>
    /// HkshClient
    /// </summary>
    /// <seealso cref="IHkshClient" />
    public class HkshClient : IHkshClient
    {
        /// <summary>
        /// The rest client
        /// </summary>
        private readonly RestClient _restClient;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<IHkshClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HKSHClient"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="logger">The logger.</param>
        public HkshClient(RestClient restClient, ILogger<IHkshClient> logger)
        {
            _restClient = restClient;
            _restClient.UseNewtonsoftJson();
            _logger = logger;
        }

        /// <summary>
        /// 不需要返回值的Get请求.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public async Task<MessageResult> Get(string url, IDictionary<string, string> parameters, IDictionary<string, string> headers)
        {
            if (headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    _restClient.AddDefaultHeader(header.Key, header.Value);
                }
            }
            RestRequest webRequest = new RestRequest(new Uri(url), Method.Get);
            if (parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> para in parameters)
                {
                    webRequest.AddQueryParameter(para.Key, para.Value);
                }
            }
            try
            {
                RestResponse<MessageResult> response = await _restClient.ExecuteAsync<MessageResult>(webRequest);
                return response.Data!;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex);
                return MessageResult.FailureResult();
            }
        }

        /// <summary>
        /// 有返回值的Get请求.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public async Task<TResult> Get<TResult>(string url, IDictionary<string, string> parameters, IDictionary<string, string> headers)
        {
            if (headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    _restClient.AddDefaultHeader(header.Key, header.Value);
                }
            }
            RestRequest webRequest = new RestRequest(new Uri(url), Method.Get)
            {
                Timeout = 300 * 1000//默认设置超时5分钟
            };
            if (parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> para in parameters)
                {
                    webRequest.AddQueryParameter(para.Key, para.Value);
                }
            }
            try
            {
                RestResponse<TResult> response = await _restClient.ExecuteAsync<TResult>(webRequest);
                return response.Data!;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex);
                return default!;
            }
        }

        /// <summary>
        /// Form表单Post请求.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public async Task<TResult> Post<TResult>(string url, IDictionary<string, string> parameters, Dictionary<string, string>? headers = null)
        {
            AddHeaders(headers);

            RestRequest webRequest = new RestRequest(new Uri(url), Method.Post);

            if (parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> para in parameters)
                {
                    webRequest.AddQueryParameter(para.Key, para.Value);
                }
            }
            try
            {
                RestResponse<TResult> response = await _restClient.ExecuteAsync<TResult>(webRequest);
                return response.Data!;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex);
                return default!;
            }
        }

        /// <summary>
        /// 不需要返回值的Post请求.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public async Task<MessageResult> Post<T>(string url, T parameters, Dictionary<string, string>? headers = null) where T : class
        {
            AddHeaders(headers);

            RestRequest webRequest = new RestRequest(new Uri(url), Method.Post);
            webRequest.AddJsonBody<T>(parameters);
            try
            {
                RestResponse<MessageResult> response = await _restClient.ExecuteAsync<MessageResult>(webRequest);
                return response.Data!;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex);
                return MessageResult.FailureResult();
            }
        }

        /// <summary>
        /// 有返回值的Post请求.
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public async Task<TResult> Post<TParam, TResult>(string url, TParam parameters, Dictionary<string, string>? headers = null, Encoding? encoding = null) where TParam : class
        {
            AddHeaders(headers);

            RestRequest webRequest = new RestRequest(new Uri(url), Method.Post);

            webRequest.AddJsonBody(parameters);
            try
            {
                RestResponse<TResult> response = await _restClient.ExecuteAsync<TResult>(webRequest);
                return (response == null || response.Data == null) ? default! : response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogExc(ex);
                return default!;
            }
        }

        /// <summary>
        /// 添加默认的请求头.
        /// </summary>
        /// <param name="headers">The headers.</param>
        private void AddHeaders(Dictionary<string, string>? headers)
        {
            //Default Header
            Dictionary<string, string> defaultHeader = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "charset", "utf-8" }
            };

            //Add Header and Default Header
            headers ??= new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in defaultHeader)
            {
                if (!headers.Keys.Any(x => x.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                {
                    headers.Add(item.Key, item.Value);
                }
            }

            foreach (KeyValuePair<string, string> item in headers)
            {
                if (_restClient.DefaultParameters.Any(x => !string.IsNullOrEmpty(x.Name) && x.Name.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                {
                    _restClient.DefaultParameters.RemoveParameter(item.Key);
                }
                _restClient.AddDefaultHeader(item.Key, item.Value);
            }
        }
    }
}