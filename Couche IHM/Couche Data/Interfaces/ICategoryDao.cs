
using Modeles;

namespace Couche_Data.Interfaces
{

    public interface ICategoryDao
    {
        /// <summary>
        /// Créer une catégorie
        /// </summary>
        public void CreateCategory(Category cat);
        
        /// <summary>
        /// Modifie une catéogorie
        /// </summary>
        public void UpdateCategory(Category category);

        /// <summary>
        /// Suprimme une catégorie
        /// </summary>
        public void DeleteCategory(Category cat);

        /// <summary>
        /// Liste toutes les catégories
        /// </summary>
        public List<Category> ListALlCategory();


    }
}
