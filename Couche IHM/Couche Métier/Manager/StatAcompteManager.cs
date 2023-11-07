using Couche_Data.Dao;
using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatAcompteManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats d'acompte
        /// </summary>
        private IStatAcompteDAO dao;

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
            dao = new Couche_Data.Dao.StatAcompteDAO();
            this.statAcompteList = new List<StatAcompte>();
            

        }
        #endregion

        #region methods
        public void CreateStat(StatAcompte stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatAcompte> GetStats()
        {
            this.statAcompteList = dao.GetStat();
            return this.statAcompteList;
        }
        #endregion
    }
}
