using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Manager de produit qui gère la DATA et le METIER
    /// </summary>
    public class ProductManager
    {
        private List<Product> products;
        /// <summary>
        /// Liste des produits
        /// </summary>
        public List<Product> Products
        {
            get => products;
            set => products = value;
        }

        private List<string> categoryProduct;
        /// <summary>
        /// Catégorie de produits
        /// </summary>
        public List<string> CategoryProduct
        {
            get => categoryProduct;
            set => categoryProduct = value;
        }

        private IProductDAO productDAO;

        public ProductManager(IProductDAO productDAO)
        {
            // Initialisation
            this.products = new List<Product>();
            this.categoryProduct = new List<string>();
            this.productDAO = productDAO;
            products = this.productDAO.GetProducts();
            InitialiseCategory();
        }

        /// <summary>
        /// Ajoute un produit
        /// </summary>
        public void CreateProduct(Product p)
        {
            products.Add(p);
            productDAO.CreateProduct(p);
        }

        /// <summary>
        /// Retire un produit
        /// </summary>
        public void RemoveProduct(Product p)
        {
            products.Remove(p); 
            productDAO.RemoveProduct(p);
        }

        /// <summary>
        /// Update un produit
        /// </summary>
        public void UpdateProduct(Product p)
        {
            Product actalProduit = this.GetProduct(p.NomProduit);
            actalProduit = p;

            productDAO.UpdateProduct(p);

        }

        /// <summary>
        /// Cherche un produit
        /// </summary>
        /// <param name="productName"> nom du produit </param>
        /// <returns> produit </returns>
        public Product GetProduct(string productName)
        {
            Product produit = null;
            foreach(Product product in products)
            {
                if(product.NomProduit == productName)
                    produit = product;
            }
            return produit;
        }
        
        /// <summary>
        /// Retourne la liste des produits
        /// </summary>
        /// <returns> Liste de produit </returns>
        public List<Product> GetProducts()
        {
            return this.products;
        }

        /// <summary>
        /// Intialise les catégories 
        /// </summary>
        private void InitialiseCategory()
        {
            foreach(Product p in this.products)
            {
                if (!this.categoryProduct.Contains(p.Categorie))
                {
                    this.categoryProduct.Add(p.Categorie);
                }
            }
        }
    }
}
