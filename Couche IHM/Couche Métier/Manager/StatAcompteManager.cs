using Couche_Data;
using Couche_Data.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatAcompteManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats d'acompte
        /// </summary>
        private StatAcompteDAO dao;

        /// <summary>
        /// Liste des stats d'acompte
        /// </summary>
        private List<StatAcompte> statAcompteList;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du statProduit Manager
        /// </summary>
        public StatAcompteManager()
        {
            dao = new StatAcompteDAO();
            Task.Run(() => this.statAcompteList = dao.GetStat());

        }
        #endregion

        #region methods
        public void CreateStat(StatAcompte stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatAcompte> GetStats()
        {
            return this.statAcompteList;
        }
        #endregion
    }
}
