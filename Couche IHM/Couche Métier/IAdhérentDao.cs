using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// IAdhérent
    /// </summary>
    public interface IAdhérentDao
    {

        /// <summary>
        /// Permet de récupérer tous les adhérents
        /// </summary>
        /// <returns>tous les adhérents</returns>
        public Dictionary<string, Adhérent> GetAdhérents();

        /// <summary>
        /// Permet de récupérer un adhérent
        /// </summary>
        /// <param name="id">id de l'adhérent</param>
        /// <returns>un adhérent</returns>
        public Adhérent GetAdhérent(int id);


        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à supprimer</param>
        public void RemoveAdhérent(Adhérent adhérent);


        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à créer</param>
        public void CreateAdhérent(Adhérent adhérent);

        /// <summary>
        /// Permet de modifier un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à modifier</param>
        public void UpdateAdhérent(Adhérent adhérent);
    }
}
