

using Couche_Data;
using Modeles;

namespace Couche_Métier
{
    public class AdhérentManager
    {
        // Représente le dao des adhérents
        private IAdhérentDao adhérentDao;

        // Représente les adhérents
        private List<Adhérent> adhérents = new List<Adhérent>();


        /// <summary>
        /// Constructeur de la classe adhérentManager
        /// </summary>
        /// <param name="adhérentDao">Dao des adhérents</param>
        public AdhérentManager()
        {
            this.adhérentDao = new AdhérentDao();

            // Récupération des adhérents
            adhérents = adhérentDao.GetAdhérents();

        }

        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à créer</param>
        public void CreateAdhérent(Adhérent adhérent)
        {
            adhérentDao.CreateAdhérent(adhérent);
            adhérents.Add(adhérent);
        }

        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à supprimer</param>
        public void RemoveAdhérent(Adhérent adhérent)
        {
            adhérentDao.RemoveAdhérent(adhérent);
            adhérents.Remove(adhérent);
        }


        /// <summary>
        /// Permet de mettre à jour les adhérents
        /// </summary>
        /// <param name="adhérent">adhérent à modifier</param>
        public void UpdateAdhérent(Adhérent adhérent)
        {
            adhérentDao.UpdateAdhérent(adhérent);
            Adhérent adhér = adhérents.Find(adh => adh.Id == adhérent.Id);
            adhér.Nom = adhérent.Nom;
            adhér.Prenom = adhérent.Prenom;
            adhér.Argent = adhérent.Argent;
            adhér.StillAdherent = adhérent.StillAdherent;
            adhér.CanPass = adhérent.CanPass;
            adhér.Formation = adhérent.Formation;
        }


       /// <summary>
       /// Permet de récupérer tous les adhérents
       /// </summary>
       /// <returns>tous les adhérents</returns>
        public List<Adhérent> GetAdhérents()
        {
            return this.adhérents;
        }

        /// <summary>
        /// Permet de récupérer une liste d'adhérent selo ndes infos
        /// </summary>
        /// <param name="id">infos des adhérents</param>
        /// <returns>des adhérent</returns>
        public List<Adhérent> GetAdhérents(string infoAdherent)
        {
            List<Adhérent> a = this.adhérents.FindAll(adhérent => 
                adhérent.Prenom.ToUpper().Contains(infoAdherent.ToUpper()) ||  
                adhérent.Nom.ToUpper().Contains(infoAdherent.ToUpper()) || 
                adhérent.Identifiant.ToUpper().Contains(infoAdherent.ToUpper()));

            
            return a;
        }



    }
}
