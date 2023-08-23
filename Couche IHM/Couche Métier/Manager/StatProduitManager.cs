using Couche_Data;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Manager
{
    public class StatProduitManager
    {

        private StatProduitDAO dao;
        public StatProduitManager()
        {
            dao = new StatProduitDAO();
        }

        public void CreateStat(StatProduit stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatProduit> GetStats()
        {
            return this.dao.GetStat();
        }
    }
}
