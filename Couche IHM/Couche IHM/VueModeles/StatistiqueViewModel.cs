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
        private AdhérentManager acompteManager;
        private List<PodiumProduit> statsProduit = new List<PodiumProduit>();

        private StatAcompteManager statAcompteManager;
        private ProductManager productManager;
        private List<PodiumAdherent> statsAcompte = new List<PodiumAdherent>();
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

        /// <summary>
        /// Podium des trois meilleurs acomptes
        /// </summary>
        public List<PodiumAdherent> PodiumAcompte
        {
            get
            {
                return statsAcompte.OrderByDescending(x => x.Argent).Take(3).ToList();
            }
        }

        #endregion

        public StatistiqueViewModel(ProductManager produtManager,AdhérentManager acompteManager)
        {
            // Initialisation des objets métiers
            this.statProduitManager = new StatProduitManager();
            this.statAcompteManager = new StatAcompteManager(); 
            this.productManager = produtManager;
            this.acompteManager = acompteManager;

            // Initialisation des datas
            InitStatsProduit();
            InitStatsAcompte();
        }

        #region
        /// <summary>
        /// Permet de mettre à jour les stats de la journée
        /// </summary>
        /// <param name="stat"></param>
        public void AddStatProduit(StatProduit stat)
        {
            PodiumProduit? produitStat = this.statsProduit.Find(x => x.ProductViewModel.Id == stat.Product_id);
            if( produitStat != null)
            {
                produitStat.PurchaseCount += stat.Number_sales;
            }
            else
            {
                this.statsProduit.Add(new PodiumProduit(stat, new ProductViewModel(productManager.GetProducts().Find(x => x.ID == stat.Product_id),null,null,null)));
            }
            NotifyPropertyChanged(nameof(this.PodiumProduits));
        }

        /// <summary>
        /// Permet de mettre à jour les stats de la journée
        /// </summary>
        /// <param name="stat"></param>
        public void AddStatAcompte(StatAcompte stat)
        {
            PodiumAdherent? acompteStat = this.statsAcompte.Find(x => x.AdherentViewModel.Id == stat.Aompte_Id);
            if (acompteStat != null)
            {
                acompteStat.Argent += stat.Amount_money;
            }
            else
            {
                this.statsAcompte.Add(new PodiumAdherent(stat, new AdherentViewModel(acompteManager.GetAdhérents().Find(x => x.Id == stat.Aompte_Id),null)));
            }
            NotifyPropertyChanged(nameof(this.PodiumAcompte));
        }

        /// <summary>
        /// Permet d'inistaliser les stats des produits
        /// </summary>
        public void InitStatsProduit()
        {
            this.statsProduit.Clear();
            List<StatProduit> statProduit = this.statProduitManager.GetStats();
            foreach (StatProduit stat in statProduit)
            {
                this.statsProduit.Add(new PodiumProduit(stat, new ProductViewModel(productManager.GetProducts().Find(x => x.ID == stat.Product_id), null, null, null)));
            }
        }


        /// <summary>
        /// Permet d'initialiser les stats des acomptes
        /// </summary>
        public void InitStatsAcompte()
        {
            this.statsAcompte.Clear();
            List<StatAcompte> statAcompte = this.statAcompteManager.GetStats();
            foreach (StatAcompte stat in statAcompte)
            {
                this.statsAcompte.Add(new PodiumAdherent(stat, new AdherentViewModel(acompteManager.GetAdhérents().Find(x => x.Id == stat.Aompte_Id), null)));
            }
        }
        #endregion
    }
}
