using GalliumPlusApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GalliumPlusApi
{
    internal class GalliumPlusHttpClient : HttpClient
    {
        private JsonSerializerOptions jsonOptions;

        public JsonSerializerOptions JsonOptions => jsonOptions;

        public GalliumPlusHttpClient()
        {
            BaseAddress = new Uri("https://" + ApiConfig.Current.Host);
            DefaultRequestHeaders.Add("X-Api-Key", ApiConfig.Current.ApiKey);
            jsonOptions = new JsonSerializerOptions();
        }

        private static T WaitForTask<T>(Task<T> task)
        {
            task.Wait();

            if (task.IsFaulted)
            {
                throw task.Exception ?? new Exception("Unknown error in task");
            }

            return task.Result;
        }

        public T Post<T>(string resource, object data)
        {
            var response = WaitForTask(this.PostAsJsonAsync(resource, data, jsonOptions));

            ExceptionFactory.ThrowForStatus(response);

            return WaitForTask(response.Content.ReadFromJsonAsync<T>(jsonOptions))
                ?? throw new NullReferenceException("Json deserialisation return null");
        }

        // public T Get(string url)

    }
}
