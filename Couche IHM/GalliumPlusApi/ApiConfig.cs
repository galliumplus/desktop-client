namespace GalliumPlusApi
{
    internal class ApiConfig
    {
        private static ApiConfig? current;

        public static ApiConfig Current
        {
            get
            {
                if (current == null)
                {
                    current = new();
                }
                return current;
            }
        }

        private string apiKey;
        private string host;

        public string ApiKey => apiKey;

        public string Host => host;

        private ApiConfig()
        {
            apiKey = "test-api-key";
            host = "api.gallium.etiq-dijon.fr";
        }
    }
}
