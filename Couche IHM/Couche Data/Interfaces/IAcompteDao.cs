
using Modeles;

namespace Couche_Data.Interfaces
{

    public interface IAcompteDao
    {

        /// <summary>
        /// Permet de récupérer tous les adhérents
        /// </summary>
        public List<Acompte> GetAdhérents();


        /// <summary>
        /// Permet de supprimer un adhérent
        /// </summary>
        public void RemoveAdhérent(Acompte adhérent);


        /// <summary>
        /// Permet de créer un nouvel adhérent
        /// </summary>
        public void CreateAdhérent(Acompte adhérent);

        /// <summary>
        /// Permet de modifier un adhérent
        /// </summary>
        public void UpdateAdhérent(Acompte adhérent);
    }
}
