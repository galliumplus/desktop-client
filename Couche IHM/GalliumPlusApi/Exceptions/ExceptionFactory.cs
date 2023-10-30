using System.Net;

namespace GalliumPlusApi.Exceptions
{
    internal static class ExceptionFactory
    {
        public static void ThrowForStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => new UnauthenticatedException(),
                    _ => new GalliumPlusHttpException($"Erreur HTTP non gérée : {response.StatusCode} {response.ReasonPhrase}"),
                };
            }
        }
    }
}
