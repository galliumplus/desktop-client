using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Dictionary<string, Adhérent> GetAdhérents()
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
