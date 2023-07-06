
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
        public void CreateCategory(string ca);
        
        /// <summary>
        /// Modifie une catéogorie
        /// </summary>
        /// <param name="baseCategory"> nom de base de la catégorie</param>
        /// <param name=""> Nouveau nom</param>
        public void UpdateCategory(string baseCategory, string category);
        /// <summary>
        /// Suprimme une catégorie
        /// </summary>
        public void DeleteCategory(string ca);
        /// <summary>
        /// Liste toutes les catégories
        /// </summary>
        public List<string> ListALlCategory();

        /// <summary>
        /// Récupère une catégorie
        /// </summary>
        public string GetCategory(string category);
    }
}
