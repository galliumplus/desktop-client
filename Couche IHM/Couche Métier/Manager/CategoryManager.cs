
using Couche_Data;
using Modeles;

namespace Couche_Métier
{
    /// <summary>
    /// Manager des catégories 
    /// </summary>
    public class CategoryManager
    {
        // Attribut représentant le DAO gérant les catégories
        private ICategoryDao iCategory;

        // Attribut représentant les catégories en cache
        private List<Category> categories;

        public List<Category> Categories { get => categories; set => categories = value; }


        /// <summary>
        /// Constructeur du manager des catégories
        /// </summary>
        /// <param name="category">dao des catégories</param>
        public CategoryManager() 
        {
            this.iCategory = new CategoryDAO();
            categories = this.iCategory.ListALlCategory();
        }
      
       /// <summary>
       /// Permet de créer une catégorie
       /// </summary>
       /// <param name="ca">catégorie à créer</param>
        public void CreateCategory(Category cat)
        {
            this.iCategory.CreateCategory(cat);
            this.categories.Add(cat);
        }

        /// <summary>
        /// Permet de supprimer une catégorie
        /// </summary>
        /// <param name="ca">catégorie à supprimer</param>
        public void DeleteCategory(Category cat)
        {
            this.iCategory.DeleteCategory(cat);
            this.categories.Remove(cat);
        }

        /// <summary>
        /// Permet de mettre à jour une catégorie
        /// </summary>
        /// <param name="bases"></param>
        /// <param name="news"></param>
        public void UpdateCategory(Category cat)
        {
            this.iCategory.UpdateCategory(cat);
            Category category = this.categories.Find(x => x.IdCat == cat.IdCat);
            category.NomCategory = cat.NomCategory;
        }

        /// <summary>
        /// Liste les catégories
        /// </summary>
        public List<Category> ListAllCategory()
        {
            return categories;
        }

    }
}
