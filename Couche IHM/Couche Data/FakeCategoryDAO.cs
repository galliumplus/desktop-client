

namespace Couche_Data
{
    public class FakeCategoryDAO : ICategoryDao
    {
        private List<string> _categories = new List<string>()
        {
            "BOISSON",
            "SNACKS",
            "HIDDEN",
            "PABLO",
            "test"
        };

        public void CreateCategory(string ca)
        {
            _categories.Add(ca);
        }

        public void DeleteCategory(string ca)
        {
            _categories.Remove(ca);
        }

        public string GetCategory(string category)
        {
            return _categories.Find(x => x == category);
        }

        public List<string> ListALlCategory()
        {
            return _categories;
        }

        public void UpdateCategory(string baseCategory, string category)
        {
            _categories[_categories.IndexOf(baseCategory)] = category;
        }
    }
}
