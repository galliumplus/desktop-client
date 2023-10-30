using Couche_Data.Interfaces;
using GalliumPlusApi.Dto;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class ProductDao : IProductDAO
    {
        private ProductSummary.Mapper summaryMapper = new();

        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var products = client.Get<List<ProductSummary>>("v1/products");
                
                return summaryMapper.ToModel(products).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des produits : {ex}");
                return new List<Product>();
            }
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
