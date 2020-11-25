using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;

namespace SpotlightWallpaper
{
    public class Pexels
    {
        private static HttpClient client;

        private static string _token = "563492ad6f917000010000017cbff63c96c94902802bbe543c1d1688";
        private static string _baseAddress = BaseConstants.API_URL;
        private static string _apiVersion = BaseConstants.API_URL_VERSION;
        private static int _timeoutSecs = BaseConstants.REQUEST_TIMEOUT_SECS;

        public Pexels(string token)
        {
            _token = token;
            if (client == null)
            {
                this.CreateClient();
            }

            SetupClientAuthHeader(client);
        }

        private HttpClient CreateClient()
        {
            client = new HttpClient();
            SetupClientDefaults(client);
            return client;
        }

        protected virtual void SetupClientDefaults(HttpClient client)
        {
            client.Timeout = TimeSpan.FromSeconds(_timeoutSecs);
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual void SetupClientAuthHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", _token);
        }

        /// <summary>
        /// This endpoint enables you to search Pexels for any topic that you would like. For example your query could be something broad like 'Nature', 
        /// 'Tigers', 'People'. Or it could be something specific like 'Group of people working'.
        /// </summary>
        /// <param name="query">The search query. Ocean, Tigers, Pears, etc.</param>
        /// <param name="locale">The locale of the search you are performing. The current supported locales are: 'en-US' 'pt-BR' 'es-ES' 'ca-ES' 'de-DE' 'it-IT' 'fr-FR' 'sv-SE' 'id-ID' 'pl-PL' 'ja-JP' 'zh-TW' 'zh-CN' 'ko-KR' 'th-TH' 'nl-NL' 'hu-HU' 'vi-VN' 'cs-CZ' 'da-DK' 'fi-FI' 'uk-UA' 'el-GR' 'ro-RO' 'nb-NO' 'sk-SK' 'tr-TR' 'ru-RU'.</param>
        /// <param name="page">The number of the page you are requesting. Default: 1</param>
        /// <param name="pageSize">The number of results you are requesting per page. Default: 15 Max: 80</param>
        /// <returns></returns>
        public async Task<PhotoPage> SearchPhotosAsync(string query, string locale = "",string orientation = "landscape" , int page = 1,
            int pageSize = 15)
        {
            if (pageSize > 80) pageSize = 80;
            if (pageSize <= 0) pageSize = 1;
            if (page <= 0) page = 1;

            string _requestUrl =
                $"{_apiVersion}search?query={Uri.EscapeDataString(query)}&page={page}&per_page={pageSize}$orientation={orientation}";
            if (!string.IsNullOrEmpty(locale))
            {
                _requestUrl += $"&locale={locale}";
            }

            HttpResponseMessage response = await client.GetAsync(_requestUrl);

            var output = await ProcessResult<PhotoPage>(response);
            output.rateLimit = ProcessRateLimits(response);

            return output;
        }

        /// <summary>
        /// This endpoint enables you to receive real-time photos curated by the Pexels team.
        /// We add at least one new photo per hour to our curated list so that you always get a changing selection of trending photos.
        /// </summary>
        /// <param name="page">The number of the page you are requesting. Default: 1</param>
        /// <param name="pageSize">The number of results you are requesting per page. Default: 15 Max: 80</param>
        /// <returns></returns>
        public async Task<PhotoPage> CuratedPhotosAsync(int page = 1, int pageSize = 15)
        {
            if (pageSize > 80) pageSize = 80;
            if (pageSize <= 0) pageSize = 1;
            if (page <= 0) page = 1;

            string _requestUrl = $"{_apiVersion}curated?page={page}&per_page={pageSize}";

            HttpResponseMessage response = await client.GetAsync(_requestUrl);

            var output = await ProcessResult<PhotoPage>(response);
            output.rateLimit = ProcessRateLimits(response);

            return output;
        }
        

        private async Task<T> ProcessResult<T>(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<T>(responseBody);
            }

            throw new ErrorResponse(response.StatusCode, responseBody);
        }

        private RateLimit ProcessRateLimits(HttpResponseMessage response)
        {
            try
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var _resetValue = response.Headers.TryGetValues("X-Ratelimit-Reset", out var vals1)
                    ? vals1.FirstOrDefault()
                    : null;
                var _limitValue = response.Headers.TryGetValues("X-Ratelimit-Limit", out var vals2)
                    ? vals2.FirstOrDefault()
                    : "0";
                var _remainingValue = response.Headers.TryGetValues("X-Ratelimit-Remaining", out var vals3)
                    ? vals3.FirstOrDefault()
                    : "0";

                var output = new RateLimit()
                {
                    Limit = Convert.ToInt64(_limitValue),
                    Remaining = Convert.ToInt64(_remainingValue),
                    Reset = _resetValue == null
                        ? start.AddMilliseconds(Convert.ToInt64(_resetValue)).ToLocalTime()
                        : start.ToLocalTime()
                };

                return output;
            }
            catch (Exception e)
            {
            }

            return null;
        }
        
        public static class BaseConstants
        {
            public const string API_URL = "https://api.pexels.com/";
            public const string API_URL_VERSION = "v1/";
            public const int REQUEST_TIMEOUT_SECS = 30;
        }
    }
}