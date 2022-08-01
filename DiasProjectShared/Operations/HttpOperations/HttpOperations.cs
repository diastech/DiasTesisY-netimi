using DiasShared.Operations.JsonOperation;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiasShared.Operations.HttpOperation
{
    public static class HttpOperations
    {
        /// <summary>
        /// Convert Http content from predefined Json
        /// </summary>
        /// <param name="content">predefined Json</param>
        /// <returns></returns>
        public static HttpContent ProduceHttpContentFromJson(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var memoryStream = new MemoryStream();
                JsonOperations.SerializeJsonIntoStream(content, memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(memoryStream);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> envelope class for return string</typeparam>
        /// <param name="tokenUrl"></param>
        /// <param name="parameters"><request params/param>
        /// <param name="header">request content-type</param>
        /// <returns>Access Token in string</returns>
        public async static Task<string> GetRefreshedAccessToken<T>(string tokenUrl, Dictionary<string, string> parameters, string header)
        {
            if ((!string.IsNullOrEmpty(tokenUrl)) && (parameters.Count == 7))
            {

                RestClient client = new RestClient(tokenUrl);
                RestRequest request = new RestRequest();

                request.AddParameter("grant_type", parameters["grant_type"]);
                request.AddParameter("username", parameters["username"]);
                request.AddParameter("password", parameters["password"]);
                request.AddParameter("client_id", parameters["client_id"]);
                request.AddParameter("audience", parameters["audience"]);
                request.AddParameter("realm", parameters["realm"]);
                request.AddParameter("scope", parameters["scope"]);

                request.AddHeader("content-type", header);

                RestResponse response = await client.PostAsync(request);
                string content = response.Content; // Raw content as string

                T accessTokenModel = JsonConvert.DeserializeObject<T>(content);
                string accessTokenStr = null;

                // this check is probably not needed, but safety first
                if (typeof(T).GetProperties().Any(p => p.Name == "AccessToken"))
                {
                    accessTokenStr  = typeof(T).GetProperty("AccessToken").GetValue(accessTokenModel, null).ToString();
                }

                return accessTokenStr;
            }
            else
            {
                return null;
            }
        }
    }
}
