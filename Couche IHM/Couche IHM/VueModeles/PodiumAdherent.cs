using Couche_Métier.Utilitaire;
using Modeles;
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
        private float argent;

        public PodiumAdherent(StatAcompte stat,AdherentViewModel adherentViewModel)
        {
            this.adherentViewModel = adherentViewModel;
            this.argent = stat.Money;
        }

        public AdherentViewModel AdherentViewModel { get => adherentViewModel; set => adherentViewModel = value; }
        public string FormattedArgent
        {
            get
            {
                return ConverterFormatArgent.ConvertToString(argent);
            }
            
        }
        public float Argent { get => argent; set => argent = value; }
    }
}
