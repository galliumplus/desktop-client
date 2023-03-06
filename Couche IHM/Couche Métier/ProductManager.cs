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
        private IProductDAO productDAO;

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

        /// <summary>
        /// Constructeur de Product Manager
        /// </summary>
        /// <param name="productDAO"> IProductDAO </param>
        public ProductManager(IProductDAO productDAO)
        {
            // Initialisation
           
            this.categoryProduct = new List<string>();
            this.productDAO = productDAO;
            this.products = new List<Product>(this.productDAO.GetProducts());
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
            Product actalProduit = this.GetProduct(p.ID);
            actalProduit.NomProduit = p.NomProduit;
            actalProduit.PrixNonAdherent = p.PrixNonAdherent;
            actalProduit.PrixAdherent = p.PrixAdherent;
            actalProduit.Quantite = p.Quantite;
            actalProduit.Categorie = p.Categorie;
            productDAO.UpdateProduct(p);

        }

        /// <summary>
        /// Cherche un produit
        /// </summary>
        /// <param name="productName"> nom du produit </param>
        /// <returns> produit </returns>
        public Product GetProduct(int idProduct)
        {
            Product produit = null;
            foreach(Product product in products)
            {
                if(product.ID == idProduct)
                    produit = product;
            }
            return produit;
        }

        /// <summary>
        /// Renvoie une liste de produits selon des critères
        /// </summary>
        /// <param name="infoProduct"> info sur le produit </param>
        /// <returns> liste de produits </returns>
        public List<Product> GetProducts(string infoProduct)
        {
            List<Product> p = new List<Product>();
            foreach (Product product in this.products)
            {
                if (product.NomProduit.ToUpper().Contains(infoProduct.ToUpper()) || product.Categorie.ToUpper().Contains(infoProduct.ToUpper()))
                {
                    p.Add(product);
                }
            }
            return p;
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
