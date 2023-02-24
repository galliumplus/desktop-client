using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Couche_Métier
{
    public class CategoryManager
    {
        private ICategory iCategory;

        public CategoryManager(ICategory category) 
        {
            this.iCategory = category;
        }
      
        /// <summary>
        /// Créer une catéogorie
        /// </summary>
        public void CreateCategory(string ca)
        {
            this.iCategory.CreateCategory(ca);
        }

        /// <summary>
        /// Suprimme une catégorie
        /// </summary>
        public void DeleteCategory(string ca)
        {
            this.iCategory.DeleteCategory(ca);
        }

        /// <summary>
        /// Met à jour une catégorie
        /// </summary>
        public void UpdateCategory(string bases, string news)
        {
            this.iCategory.UpdateCategory(bases, news);
        }

        /// <summary>
        /// Liste les catégories
        /// </summary>
        public List<string> ListAllCategory()
        {
            return this.iCategory.ListALlCategory();
        }

        /// <summary>
        /// Cherche une catégorie
        /// </summary>
        public string GetCategory(string category)
        {
            return this.iCategory.GetCategory(category);
        }
    }
}
