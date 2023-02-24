using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class FakeProduitsDAO : IProductDAO
    {
        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product("Coca cola", 20, 0.80f, getRandomCategorie()));
            products.Add(new Product("Fanta", 20, 0.80f, getRandomCategorie()));
            products.Add(new Product("Monster", 20, 1.20f, getRandomCategorie()));
            products.Add(new Product("SUPER MONSTER", 1, 2.20f, getRandomCategorie()));
            products.Add(new Product("Pablo", 1, 500f, getRandomCategorie())); 
            return products;
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        private string getRandomCategorie()
        {
            CategoryManager c = new CategoryManager(new FakeCategoryDAO());
            List<string> categories = c.ListAllCategory();
            return categories[new Random().Next(0, categories.Count)];
        }
    }
}
