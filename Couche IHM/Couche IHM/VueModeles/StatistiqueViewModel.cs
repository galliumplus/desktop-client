using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class StatistiqueViewModel : INotifyPropertyChanged
    {
        #region attributes
        private StatProduitManager statProduitManager;
        private List<ProductViewModel> products;
        private List<PodiumProduit> statsProduit = new List<PodiumProduit>();
        #endregion

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region properties

        /// <summary>
        /// Podium des trois meilleurs produits
        /// </summary>
        public List<PodiumProduit> PodiumProduits
        {
            get
            {
                return statsProduit.OrderByDescending(x => x.PurchaseCount).Take(3).ToList();
            }
        }

        #endregion

        public StatistiqueViewModel(List<ProductViewModel> products)
        {
            // Initialisation des objets métiers
            this.statProduitManager = new StatProduitManager();

            // Initialisation des datas
            this.products = products;
            InitStatsProduit();
        }

        #region
        /// <summary>
        /// Permet de mettre à jour les stats de la journée
        /// </summary>
        /// <param name="stat"></param>
        public void AddStatProduit(StatProduit stat)
        {
            this.statsProduit.Find(x => x.ProductViewModel.Id == stat.Product_id).PurchaseCount += stat.Number_sales;
            NotifyPropertyChanged(nameof(this.PodiumProduits));
        }

        /// <summary>
        /// Permet de récupérer la liste des catégories
        /// </summary>
        public void InitStatsProduit()
        {
            this.statsProduit.Clear();
            List<StatProduit> statProduit = this.statProduitManager.GetStats();
            int classement = 0;
            foreach (StatProduit stat in statProduit)
            {
                classement++;
                this.statsProduit.Add(new PodiumProduit(stat,products.Find(x => x.Id == stat.Product_id), classement));
            }
        }
        #endregion
    }
}
