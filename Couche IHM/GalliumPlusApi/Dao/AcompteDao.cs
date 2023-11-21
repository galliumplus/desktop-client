using Couche_Data.Interfaces;
using GalliumPlusApi.CompatibilityHelpers;
using GalliumPlusApi.Dto;
using GalliumPlusApi.Exceptions;
using GalliumPlusApi.ModelDecorators;
using Modeles;
using System.Diagnostics;
using static GalliumPlusApi.Dto.AccountSummary;
using System.Numerics;

namespace GalliumPlusApi.Dao
{
    public class AccountDao : IAccountDao
    {
        private AccountSummary.AccountMapper mapper = new();
        private RoleDetails.Mapper roleMapper = new();

        public void CreateAdhérent(Account adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var existingUser = client.Get<AccountDetails>($"v1/users/{adhérent.Identifiant}");
                client.Put($"v1/users/{adhérent.Identifiant}", mapper.PatchWithModel(existingUser, adhérent));
            }
            catch (ItemNotFoundException)
            {
                client.Post("v1/users", mapper.FromModel(adhérent));
            }
        }




        public List<Role> GetRoles()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var roles = client.Get<List<RoleDetails>>("v1/roles");

                return roleMapper.ToModel(roles).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des rôles : {ex}");
                return new List<Role>();
            }
        }
        public List<Account> GetAdhérents()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var users = client.Get<List<AccountSummary>>("v1/users")
                                  .Where(user => user.Deposit != null);

                return mapper.ToModel(users).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des acomptes : {ex}");
                return new List<Account>();
            }
        }

        public void RemoveAdhérent(Account adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            var existingUser = client.Get<AccountDetails>($"v1/users/{adhérent.Identifiant}");
            existingUser.Deposit = null;

            client.Put($"v1/users/{adhérent.Identifiant}", existingUser.AsUserSummary);
        }

        public void UpdateAdhérent(Account adhérent)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            if (adhérent is DecoratedAccount deco)
            {
                client.Put($"v1/users/{adhérent.Identifiant}", mapper.FromModel(deco));
            }
            else
            {
                var original = client.Get<AccountDetails>($"v1/users/{adhérent.Identifiant}");
                client.Put($"v1/products/{adhérent.Identifiant}", mapper.PatchWithModel(original, adhérent));
            }
        }
    }
}
