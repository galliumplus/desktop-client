using Couche_Data;
using Couche_Data.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatProduitManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats produits
        /// </summary>
        private StatProduitDAO dao;

        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du statProduit manager
        /// </summary>
        public StatProduitManager()
        {
            dao = new StatProduitDAO();
        }
        #endregion

        #region methods
        public void CreateStat(StatProduit stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatProduit> GetStats()
        {
            return this.dao.GetStat();
        }
        #endregion
    }
}
