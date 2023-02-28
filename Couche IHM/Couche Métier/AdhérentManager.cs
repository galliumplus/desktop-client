using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    public class AdhérentManager
    {
        // Représente le dao des adhérents
        private IAdhérentDao adhérentDao;

        // Représente les adhérents
        private Dictionary<string,Adhérent> adhérents = new Dictionary<string, Adhérent>();


        /// <summary>
        /// Constructeur de la classe adhérentManager
        /// </summary>
        /// <param name="adhérentDao">Dao des adhérents</param>
        public AdhérentManager(IAdhérentDao adhérentDao)
        {
            this.adhérentDao = adhérentDao;

            // Récupération des adhérents
            adhérents = new Dictionary<string, Adhérent>(adhérentDao.GetAdhérents());

        }

        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à créer</param>
        public void CreateAdhérent(Adhérent adhérent)
        {
            adhérentDao.CreateAdhérent(adhérent);
            adhérents.Add(adhérent.Identifiant, adhérent);
        }

        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à supprimer</param>
        public void RemoveAdhérent(Adhérent adhérent)
        {
            adhérentDao.RemoveAdhérent(adhérent);
            adhérents.Remove(adhérent.Identifiant);
        }


        /// <summary>
        /// Permet de mettre à jour les adhérents
        /// </summary>
        /// <param name="adhérent">adhérent à modifier</param>
        public void UpdateAdhérent(Adhérent adhérent)
        {
            adhérentDao.UpdateAdhérent(adhérent);
            adhérents[adhérent.Identifiant] = adhérent;
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
            if (this.adhérents.Keys.Contains(infoAdherent))
            {
                a = this.adhérents[infoAdherent];
            }
            else
            {
                foreach (Adhérent adhérent in this.adhérents.Values)
                {
                    if (adhérent.Prenom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.NomCompletIHM.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Nom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Identifiant.ToUpper().Contains(infoAdherent.ToUpper()))
                    {
                        a = adhérent;
                        break;
                    }
                }
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
            List<Adhérent> a = new List<Adhérent>();
            foreach (Adhérent adhérent in this.adhérents.Values)
            {
                if (adhérent.Prenom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.NomCompletIHM.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Nom.ToUpper().Contains(infoAdherent.ToUpper()) || adhérent.Identifiant.ToUpper().Contains(infoAdherent.ToUpper()))
                {
                    a.Add(adhérent);
                }
            }
            
            return a;
        }



    }
}
