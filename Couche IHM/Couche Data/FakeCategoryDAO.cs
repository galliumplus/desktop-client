

using Modeles;

namespace Couche_Data
{
    public class FakeCategoryDAO : ICategoryDao
    {
        private List<Category> categories = new List<Category>()
        {
            new Category(1,"BOISSON",true),
            new Category(2,"SNACKS",true),
            new Category(3,"HIDDEN",true),
            new Category(4,"PABLO",true),
            new Category(5,"test",true)
        };

        public void CreateCategory(Category cat)
        {
            categories.Add(cat);
        }

        public void DeleteCategory(Category cat)
        {
            categories.Remove(cat);
        }

        public Category GetCategory(Category category)
        {
            return categories.Find(x => x.IdCat == category.IdCat);
        }

        public List<Category> ListALlCategory()
        {
            return categories;
        }

        public void UpdateCategory(Category cat)
        {
            Category category = categories.Find(x => x.IdCat == cat.IdCat);
            category.NomCategory = cat.NomCategory;

        }
    }
}
