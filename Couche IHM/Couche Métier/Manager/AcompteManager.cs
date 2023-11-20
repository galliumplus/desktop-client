using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class AcompteManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des acomptes
        /// </summary>
        private IAcompteDao adhérentDao;

        /// <summary>
        /// Liste des acomptes
        /// </summary>
        private List<Acompte> adhérents = new List<Acompte>();
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur de la classe adhérentManager
        /// </summary>
        public AcompteManager()
        {
            this.adhérentDao = new AcompteDao();

            // Récupération des adhérents
            adhérents = adhérentDao.GetAdhérents();
        }
        #endregion

        #region methods
        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à créer</param>
        public void CreateAdhérent(Acompte adhérent)
        {
            adhérentDao.CreateAdhérent(adhérent);
            adhérents.Add(adhérent);
        }

        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à supprimer</param>
        public void RemoveAdhérent(Acompte adhérent)
        {
            adhérentDao.RemoveAdhérent(adhérent);
            adhérents.Remove(adhérent);
        }


        /// <summary>
        /// Permet de mettre à jour les adhérents
        /// </summary>
        /// <param name="adhérent">adhérent à modifier</param>
        public void UpdateAdhérent(Acompte adhérent)
        {
            adhérentDao.UpdateAdhérent(adhérent);
            Acompte adhér = adhérents.Find(adh => adh.Id == adhérent.Id);
            adhér.Nom = adhérent.Nom;
            adhér.Prenom = adhérent.Prenom;
            adhér.Argent = adhérent.Argent;
            adhér.StillAdherent = adhérent.StillAdherent;
            adhér.Formation = adhérent.Formation;
        }


       /// <summary>
       /// Permet de récupérer tous les adhérents
       /// </summary>
       /// <returns>tous les adhérents</returns>
        public List<Acompte> GetAdhérents()
        {
            return this.adhérents;
        }

#endregion

    }
}
