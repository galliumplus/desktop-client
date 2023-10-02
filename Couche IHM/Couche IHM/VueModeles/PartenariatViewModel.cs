using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Couche_IHM.VueModeles
{
    public class PartenariatViewModel : INotifyPropertyChanged
    {
        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private List<BitmapImage> imagesDispo = new List<BitmapImage>();
        private List<BitmapImage> imagesPartenariats = new List<BitmapImage>();

        public PartenariatViewModel()
        {
            String[] filenames = System.IO.Directory.GetFiles($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesPartenariat\\");
            foreach (string filename in filenames)
            {
                imagesDispo.Add(new BitmapImage(new Uri(filename, UriKind.Absolute)));
            }
            if (imagesDispo.Count > 4)
            {
                Thread t = new Thread(() =>
                {
                    while (true)
                    {
                        InitializePartenariats();
                        Thread.Sleep(10000);
                    }
                });
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                foreach (BitmapImage image in imagesDispo)
                {
                    imagesPartenariats.Add(image);
                    NotifyPropertyChanged(nameof(ImagesPartenariats));
                }
            }
            
        }
        #endregion

        #region properties
        /// <summary>
        /// Retourne les images des partenariats
        /// </summary>
        public List<BitmapImage> ImagesPartenariats
        {
            get { return imagesPartenariats; }
        }
        #endregion

        #region methods
        private void InitializePartenariats()
        {
            int index = 0;
            if (ImagesPartenariats.Count > 0)
            {
                index = imagesDispo.IndexOf(imagesPartenariats[0]);
                imagesPartenariats.Clear();
            }
            
            imagesPartenariats.Add(imagesDispo[(index + 1)% imagesDispo.Count]);
            imagesPartenariats.Add(imagesDispo[(index + 2) % imagesDispo.Count]);
            imagesPartenariats.Add(imagesDispo[(index + 3) % imagesDispo.Count]);
            imagesPartenariats.Add(imagesDispo[(index + 4) % imagesDispo.Count]);
            NotifyPropertyChanged(nameof(ImagesPartenariats));  
        }
        #endregion

    }
}
