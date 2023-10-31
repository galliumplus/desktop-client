using Couche_Data.Interfaces;
using GalliumPlusApi.CompatibilityHelpers;
using GalliumPlusApi.Dto;
using GalliumPlusApi.ModelDecorators;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class UserDao : IUserDAO
    {
        private UserSummary.UserMapper userMapper = new();
        private RoleDetails.Mapper roleMapper = new();

        public void CreateCompte(User compte)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            compte.ID = UserIdRepository.Current.GetIdFor(RandomHelper.RandomUsername());

            client.Post("v1/users", userMapper.FromModel(compte));
        }

        public List<User> GetComptes()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var roles = client.Get<List<RoleDetails>>("v1/roles");
                int adherentRoleId = roles.Find(role => role.Name == "Adhérent")?.Id ?? -1;

                SessionStorage.Current.Put("adherentRoleId", adherentRoleId);

                var users = client.Get<List<UserSummary>>("v1/users")
                                  .Where(user => user.Role != adherentRoleId);

                return userMapper.ToModel(users).ToList();
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

                return roleMapper.ToModel(roles).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des rôles : {ex}");
                return new List<Role>();
            }
        }

        public void RemoveCompte(User compte)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            string username = UserIdRepository.Current.FindUsernameOf(compte.ID);

            client.Delete($"v1/users/{username}");
        }

        public void UpdateCompte(User compte)
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            string username = UserIdRepository.Current.FindUsernameOf(compte.ID);

            if (compte is DecoratedUser deco)
            {
                client.Put($"v1/users/{username}", userMapper.FromModel(deco));
            }
            else
            {
                var original = client.Get<UserDetails>($"v1/users/{username}");
                client.Put($"v1/products/{username}", userMapper.PatchWithModel(original, compte));
            }
        }
    }
}
