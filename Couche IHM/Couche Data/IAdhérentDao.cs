
using Modeles;

namespace Couche_Data
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
        public List<Adhérent> GetAdhérents();


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
