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
            try
            {
                dao = new Couche_Data.Dao.StatAcompteDAO();
            }
            catch (Exception error)
            {
                Console.WriteLine("Impossible de se connecter à la base de donnée GV2 : " + error.Message);
                dao = new GalliumPlusApi.Dao.StatAcompteDAO();
            }
            this.statAcompteList = new List<StatAcompte>();
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
