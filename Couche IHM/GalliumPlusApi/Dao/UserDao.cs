using Couche_Data.Interfaces;
using GalliumPlusApi.Dto;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class UserDao : IUserDAO
    {
        private UserSummary.UserMapper usMapper = new();
        private RoleDetails.Mapper rdMapper = new();

        public void CreateCompte(User compte)
        {
            throw new NotImplementedException();
        }

        public List<User> GetComptes()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var roles = client.Get<List<RoleDetails>>("v1/roles");
                int adherentRoleId = roles.Find(role => role.Name == "Adhérent")?.Id ?? -1;

                var users = client.Get<List<UserSummary>>("v1/users")
                                  .Where(user => user.Role != adherentRoleId);

                return usMapper.ToModel(users).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des utilisateurs : {ex}");
                return new List<User>();
            }
        }

        public List<Role> GetRoles()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var roles = client.Get<List<RoleDetails>>("v1/roles");

                return rdMapper.ToModel(roles).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des rôles : {ex}");
                return new List<Role>();
            }
        }

        public void RemoveCompte(User compte)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompte(User compte)
        {
            throw new NotImplementedException();
        }
    }
}
