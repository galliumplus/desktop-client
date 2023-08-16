
using Modeles;

namespace Couche_Data
{
    /// <summary>
    /// I Category
    /// </summary>
    public interface ICategoryDao
    {
        /// <summary>
        /// Créer une catégorie
        /// </summary>
        public void CreateCategory(Category cat);
        
        /// <summary>
        /// Modifie une catéogorie
        /// </summary>
        /// <param name="baseCategory"> nom de base de la catégorie</param>
        /// <param name=""> Nouveau nom</param>
        public void UpdateCategory(Category category);
        /// <summary>
        /// Suprimme une catégorie
        /// </summary>
        public void DeleteCategory(Category cat);
        /// <summary>
        /// Liste toutes les catégories
        /// </summary>
        public List<Category> ListALlCategory();

        /// <summary>
        /// Récupère une catégorie
        /// </summary>
        public Category GetCategory(Category category);
    }
}
