
using Modeles;

namespace Couche_Data
{
    /// <summary>
    /// Interfaction avec les produits de la base de donnée
    /// </summary>
    public class ProductDAO
    {
        public void CreateProduct(Product produit)
        {
            dbsDAO.Instance.Fetch("");
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
