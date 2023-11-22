using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Couche_Data.Dao
{
    public class StatProduitDAO : IStatProduitDAO
    {

        public List<StatProduit> GetStat()
        {
            return new List<StatProduit>();
        }

        public void CreateStat(StatProduit stat)
        {
        }
    }
}
