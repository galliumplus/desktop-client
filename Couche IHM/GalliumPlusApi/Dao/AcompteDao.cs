using Couche_Data.Interfaces;
using GalliumPlusApi.CompatibilityHelpers;
using GalliumPlusApi.Dto;
using GalliumPlusApi.Exceptions;
using GalliumPlusApi.ModelDecorators;
using Modeles;
using System.Diagnostics;
using static GalliumPlusApi.Dto.UserSummary;
using System.Numerics;

namespace GalliumPlusApi.Dao
{
    public class AcompteDao : IAcompteDao
    {
        private UserSummary.AcompteMapper mapper = new();

        public void CreateAdhérent(Acompte adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var existingUser = client.Get<UserDetails>($"v1/users/{adhérent.Identifiant}");
                client.Put($"v1/users/{adhérent.Identifiant}", mapper.PatchWithModel(existingUser, adhérent));
            }
            catch (ItemNotFoundException)
            {
                client.Post("v1/users", mapper.FromModel(adhérent));
            }
        }

        public List<Acompte> GetAdhérents()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var users = client.Get<List<UserSummary>>("v1/users")
                                  .Where(user => user.Deposit != null);

                return mapper.ToModel(users).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des acomptes : {ex}");
                return new List<Acompte>();
            }
        }

        public void RemoveAdhérent(Acompte adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            var existingUser = client.Get<UserDetails>($"v1/users/{adhérent.Identifiant}");
            existingUser.Deposit = null;

            client.Put($"v1/users/{adhérent.Identifiant}", existingUser.AsUserSummary);
        }

        public void UpdateAdhérent(Acompte adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            if (adhérent is DecoratedAcompte deco)
            {
                client.Put($"v1/users/{adhérent.Identifiant}", mapper.FromModel(deco));
            }
            else
            {
                var original = client.Get<UserDetails>($"v1/users/{adhérent.Identifiant}");
                client.Put($"v1/products/{adhérent.Identifiant}", mapper.PatchWithModel(original, adhérent));
            }
        }
    }
}
