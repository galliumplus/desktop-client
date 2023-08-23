using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class PodiumProduit
    {
        private ProductViewModel productViewModel;
        private int podium;
        private int purchaseCount;

        public PodiumProduit(StatProduit stat,ProductViewModel product,int podium)
        {
            this.purchaseCount = stat.Number_sales;
            this.productViewModel = product;
            this.podium = podium;
        }

        public ProductViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        public int Podium { get => podium; set => podium = value; }
        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
    }
}
