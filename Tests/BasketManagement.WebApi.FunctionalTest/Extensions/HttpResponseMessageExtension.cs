using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BasketManagement.WebApi.FunctionalTest.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ReadContentAsAsync<T>(this HttpResponseMessage responseMessage)
            where T : class
        {
            string? responseContent = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Include,
                StringEscapeHandling = StringEscapeHandling.Default,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TypeNameHandling = TypeNameHandling.Auto,
            });
            if (result == null)
            {
                throw new ApplicationException($"{typeof(T).Name} could not built with this content => {responseContent}");
            }

            return result;
        }

        public static async Task<string> ReadContentAsStringAsync(this HttpResponseMessage responseMessage)
        {
            string? responseContent = await responseMessage.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}