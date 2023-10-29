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

        /// <summary>
        /// Liste des stats des produits
        /// </summary>
        private List<StatProduit> statProduitList;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du statProduit manager
        /// </summary>
        public StatProduitManager()
        {
            dao = new StatProduitDAO();
            statProduitList = new();
            Task.Run(()=> statProduitList = dao.GetStat());
        }
        #endregion

        #region methods
        public void CreateStat(StatProduit stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatProduit> GetStats()
        {
            return this.statProduitList;
        }
        #endregion
    }
}
