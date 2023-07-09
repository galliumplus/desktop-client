

using Couche_Data;
using Modeles;

namespace Couche_Métier
{
    public class AdhérentManager
    {
        // Représente le dao des adhérents
        private IAdhérentDao adhérentDao;

        // Représente les adhérents
        private Dictionary<int,Adhérent> adhérents = new Dictionary<int, Adhérent>();


        /// <summary>
        /// Constructeur de la classe adhérentManager
        /// </summary>
        /// <param name="adhérentDao">Dao des adhérents</param>
        public AdhérentManager()
        {
            this.adhérentDao = new FakeAdherentDao();

            // Récupération des adhérents
            adhérents = new Dictionary<int, Adhérent>(adhérentDao.GetAdhérents());

        }

        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à créer</param>
        public void CreateAdhérent(Adhérent adhérent)
        {
            adhérent.Id = this.adhérents.Count + 1;
            adhérentDao.CreateAdhérent(adhérent);
            adhérents.Add(adhérent.Id, adhérent);
        }

        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à supprimer</param>
        public void RemoveAdhérent(Adhérent adhérent)
        {
            adhérentDao.RemoveAdhérent(adhérent);
            adhérents.Remove(adhérent.Id);
        }


        /// <summary>
        /// Permet de mettre à jour les adhérents
        /// </summary>
        /// <param name="adhérent">adhérent à modifier</param>
        public void UpdateAdhérent(Adhérent adhérent)
        {
            adhérentDao.UpdateAdhérent(adhérent);
            adhérents[adhérent.Id] = adhérent;
        }


       /// <summary>
       /// Permet de récupérer tous les adhérents
       /// </summary>
       /// <returns>tous les adhérents</returns>
        public List<Adhérent> GetAdhérents()
        {
            return this.adhérents.Values.ToList();
        }

        /// <summary>
        /// Permet de récupérer un adhérent
        /// </summary>
        /// <param name="id">info de l'adhérent</param>
        /// <returns>un adhérent</returns>
        public Adhérent GetAdhérent(string infoAdherent)
        {
            Adhérent a = null;
            foreach (Adhérent adhérent in this.adhérents.Values)
            {
                /*if (adhérent.Prenom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.NomCompletIHM.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Nom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Identifiant.ToUpper().Contains(infoAdherent.ToUpper()))
                {
                    a = adhérent;
                    break;
                }*/
            }
            return a;
        }


        /// <summary>
        /// Permet de récupérer une liste d'adhérent selo ndes infos
        /// </summary>
        /// <param name="id">infos des adhérents</param>
        /// <returns>des adhérent</returns>
        public List<Adhérent> GetAdhérents(string infoAdherent)
        {
            List<Adhérent> a = this.adhérents.Values.ToList().FindAll(adhérent => 
                adhérent.Prenom.ToUpper().Contains(infoAdherent.ToUpper()) ||  
                adhérent.Nom.ToUpper().Contains(infoAdherent.ToUpper()) || 
                adhérent.Identifiant.ToUpper().Contains(infoAdherent.ToUpper()));

            
            return a;
        }



    }
}
