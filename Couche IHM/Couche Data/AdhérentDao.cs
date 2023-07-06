
using Modeles;


namespace Couche_Data
{
    public class AdhérentDao : IAdhérentDao
    {
        public void CreateAdhérent(Adhérent adhérent)
        {
            dbsDAO.Instance.Execute("");
        }

        public Adhérent GetAdhérent(int id)
        {
            dbsDAO.Instance.Fetch("");
            return null;
        }

        public Dictionary<int, Adhérent> GetAdhérents()
        {
            dbsDAO.Instance.Fetch("");
            return null;
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            dbsDAO.Instance.Execute("");
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {
            dbsDAO.Instance.Execute("");
        }
    }
}
