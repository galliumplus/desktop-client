using Couche_Métier;
using Couche_Métier.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class CategoryViewModel : INotifyPropertyChanged
    {

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private CategoryManager categoryManager;
        private string currentNameCategory;
        private string nameCategory;
        #endregion

        #region properties
        public string NameCategory 
        { 
            get => nameCategory;
            set 
            { 
                nameCategory = value;
                NotifyPropertyChanged(nameof(NameCategory));
            }
        }

        public string CurrentNameCategory 
        { 
            get => currentNameCategory;
            set 
            { 
                currentNameCategory = value; 
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region events
        public RelayCommand UpdateCat { get; set; }
        public RelayCommand DeleteCat { get; set; }

        #endregion
        public CategoryViewModel(CategoryManager categoryManager,string category)
        {
            this.categoryManager = categoryManager;
            this.CurrentNameCategory = category;
            this.nameCategory = category;
            this.UpdateCat = new RelayCommand(x => this.UpdateCategory());
            this.DeleteCat = new RelayCommand(x => this.DeleteCategory());

        }

        #region methods

        /// <summary>
        /// Permet de mettre à jour une category
        /// </summary>
        public void UpdateCategory()
        {   
            // Mise à jour data
            this.categoryManager.UpdateCategory(CurrentNameCategory,nameCategory);
            this.CurrentNameCategory=nameCategory;


            // Log l'action
            //this.log.registerLog(CategorieLog.UPDATE, this.currentNameCategory, MainWindowViewModel.Instance.CompteConnected);

            MainWindowViewModel.Instance.ProductViewModel.ShowCategories = false;
        }



        /// <summary>
        /// Permet de reset les propriétés de la catégorie
        /// </summary>
        public void ResetCategory()
        {
            // Mise à jour data
            this.NameCategory = this.currentNameCategory;
            

            // Log l'action
            //this.log.registerLog(CategorieLog.UPDATE, this.currentNameCategory, MainWindowViewModel.Instance.CompteConnected);

            MainWindowViewModel.Instance.ProductViewModel.ShowCategories = false;
        }

        /// <summary>
        /// Permet de supprimer une catégorie
        /// </summary>
        public void DeleteCategory()
        {
            // Mise à jour data
            this.categoryManager.DeleteCategory(CurrentNameCategory);
            foreach (ProductViewModel prod in MainWindowViewModel.Instance.ProductViewModel.Products.FindAll(x => x.CategoryIHM == this))
            {
                prod.CategoryIHM = null;
            }
            
            // Notifier la vue
            MainWindowViewModel.Instance.ProductViewModel.Categories.Remove(this);


            MainWindowViewModel.Instance.ProductViewModel.ShowCategories = false;
        }

        public override bool Equals(object? obj)
        {
            return obj is CategoryViewModel model &&
                   currentNameCategory == model.currentNameCategory;
        }
        #endregion


    }
}
