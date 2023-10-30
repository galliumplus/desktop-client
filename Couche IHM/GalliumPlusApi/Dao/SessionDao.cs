using GalliumPlusApi.Dto;
using GalliumPlusApi.Exceptions;
using Modeles;
using System.Text.Json;

namespace GalliumPlusApi.Dao
{
    public class SessionDao
    {
        private UserDetails.Mapper detailsMapper = new();

        public User? LogIn(string username, string password)
        {
            using var client = new GalliumPlusHttpClient();
            client.JsonOptions.PropertyNamingPolicy = null; // pas de casse chameau

            try
            {
                var loggedIn = client.Post<LoggedIn>("v1/login", new { Username = username, Password = password });

                if (loggedIn.User == null) return null;

                return this.detailsMapper.ToModel(loggedIn.User);
            }
            catch (UnauthenticatedException)
            {
                return null;
            }
        }
    }
}
