using Couche_Data.Interfaces;
using GalliumPlusApi.Dto;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class AcompteDao : IAcompteDao
    {
        private UserSummary.AcompteMapper mapper = new();
        public void CreateAdhérent(Acompte adhérent)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateAdhérent(Acompte adhérent)
        {
            throw new NotImplementedException();
        }
    }
}
