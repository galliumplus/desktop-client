using GalliumPlusApi.Dao;
using Couche_Data.Interfaces;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatProduitManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats produits
        /// </summary>
        private IStatProduitDAO dao;

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

            try
            {
                dao = new Couche_Data.Dao.StatProduitDAO();
            }
            catch (Exception error)
            {
                Console.WriteLine("Impossible de se connecter à la base de donnée GV2 : " + error.Message);
                dao = new GalliumPlusApi.Dao.StatProduitDAO();
            }
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
