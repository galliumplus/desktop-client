
using Couche_Data;

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
        private List<string> categories;

        public List<string> Categories { get => categories; set => categories = value; }


        /// <summary>
        /// Constructeur du manager des catégories
        /// </summary>
        /// <param name="category">dao des catégories</param>
        public CategoryManager(ICategoryDao category) 
        {
            this.iCategory = category;
            categories = this.iCategory.ListALlCategory();
        }
      
       /// <summary>
       /// Permet de créer une catégorie
       /// </summary>
       /// <param name="ca">catégorie à créer</param>
        public void CreateCategory(string ca)
        {
            this.iCategory.CreateCategory(ca);
            this.categories.Add(ca);
        }

        /// <summary>
        /// Permet de supprimer une catégorie
        /// </summary>
        /// <param name="ca">catégorie à supprimer</param>
        public void DeleteCategory(string ca)
        {
            this.iCategory.DeleteCategory(ca);
            this.categories.Remove(ca);
        }

        /// <summary>
        /// Permet de mettre à jour une catégorie
        /// </summary>
        /// <param name="bases"></param>
        /// <param name="news"></param>
        public void UpdateCategory(string bases, string news)
        {
            this.iCategory.UpdateCategory(bases, news);
            this.categories.Remove(bases);
            this.categories.Add(news);
        }

        /// <summary>
        /// Liste les catégories
        /// </summary>
        public List<string> ListAllCategory()
        {
            return categories;
        }

        /// <summary>
        /// Cherche une catégorie
        /// </summary>
        public string GetCategory(string category)
        {
            string cat = null;
            if (categories.Contains(category))
            {
                cat = categories[categories.IndexOf(category)];
            }
            else
            {
                cat = this.iCategory.GetCategory(category);
            }
            return cat;
        }
    }
}
