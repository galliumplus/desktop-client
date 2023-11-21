using Couche_Data.Dao;
using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatAccountManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats d'acompte
        /// </summary>
        private IStatAccountDAO dao;

        /// <summary>
        /// Liste des stats d'acompte
        /// </summary>
        private List<StatAccount> statAccountList;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du statProduit Manager
        /// </summary>
        public StatAccountManager()
        {
            dao = new Couche_Data.Dao.StatAccountDAO();
            this.statAccountList = new List<StatAccount>();
            

        }
        #endregion

        #region methods
        public void CreateStat(StatAccount stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatAccount> GetStats()
        {
            this.statAccountList = dao.GetStat();
            return this.statAccountList;
        }
        #endregion
    }
}
