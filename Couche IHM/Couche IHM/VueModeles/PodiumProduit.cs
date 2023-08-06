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

        public PodiumProduit(ProductViewModel productViewModel, int podium)
        {
            this.productViewModel = productViewModel;
            this.podium = podium;
            this.purchaseCount = productViewModel.PurchaseCount;
        }

        public ProductViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        public int Podium { get => podium; set => podium = value; }
        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
    }
}
