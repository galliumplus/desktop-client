using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Couche_Data.Dao
{
    public class StatAccountDAO : IStatAccountDAO
    {

        public List<StatAccount> GetStat()
        {
          return new List<StatAccount>();
        }

        public void CreateStat(StatAccount stat)
        {
           
        }
    }
}
