using Couche_Data.Interfaces;
using GalliumPlusApi.Dto;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class CategoryDao : ICategoryDao
    {
        private CategoryDetails.Mapper summaryMapper = new();

        public void CreateCategory(Category cat)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category cat)
        {
            throw new NotImplementedException();
        }

        public List<Category> ListALlCategory()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var categories = client.Get<List<CategoryDetails>>("v1/categories");

                return summaryMapper.ToModel(categories).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des catégories : {ex}");
                return new List<Category>();
            }
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
