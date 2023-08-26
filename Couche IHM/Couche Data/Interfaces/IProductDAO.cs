
using Modeles;

namespace Couche_Data.Interfaces
{

    public interface IProductDAO
    {
        /// <summary>
        /// Récupère tous les produits
        /// </summary>
        public List<Product> GetProducts();

        /// <summary>
        /// Supprime un produit
        /// </summary>
        public void RemoveProduct(Product product);

        /// <summary>
        /// Créer un produit 
        /// </summary>
        public void CreateProduct(Product product);

        /// <summary>
        /// Modifie un produit
        /// </summary>
        public void UpdateProduct(Product product);

 
    }
}
