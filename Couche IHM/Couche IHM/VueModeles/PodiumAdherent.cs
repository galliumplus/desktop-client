using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class PodiumAdherent
    {
        private AdherentViewModel adherentViewModel;
        private int podium;
        private int purchaseCount;

        public PodiumAdherent(AdherentViewModel adherentViewModel, int podium)
        {
            this.adherentViewModel = adherentViewModel;
            this.podium = podium;
            this.purchaseCount = adherentViewModel.PurchaseCount;
        }

        public AdherentViewModel AdherentViewModel { get => adherentViewModel; set => adherentViewModel = value; }
        public int Podium { get => podium; set => podium = value; }
        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
    }
}
