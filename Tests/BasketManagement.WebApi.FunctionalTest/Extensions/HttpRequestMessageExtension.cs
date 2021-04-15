using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace BasketManagement.WebApi.FunctionalTest.Extensions
{
    public static class HttpRequestMessageExtension
    {
        public static HttpRequestMessage WithJsonBody(this HttpRequestMessage requestMessage, object requestBody)
        {
            string stringContent = JsonConvert.SerializeObject(requestBody);
            var requestContent = new StringContent(stringContent, Encoding.Default, MediaTypeNames.Application.Json);

            requestMessage.Content = requestContent;
            return requestMessage;
        }
    }
}