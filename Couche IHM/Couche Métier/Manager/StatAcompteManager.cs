using Couche_Data;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Manager
{
    public class StatAcompteManager
    {

        private StatAcompteDAO dao;
        public StatAcompteManager()
        {
            dao = new StatAcompteDAO();
        }

        public void CreateStat(StatAcompte stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatAcompte> GetStats()
        {
            return this.dao.GetStat();
        }
    }
}
