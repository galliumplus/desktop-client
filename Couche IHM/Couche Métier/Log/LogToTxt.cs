using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Log
{
    /// <summary>
    /// Enregistre des logs dans un fichier dont l'extension est .txt
    /// </summary>
    public class LogToTXT : ILog
    {
        public string Path => @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Gallium\\Log";
        private string name => "\\GalliumLog.txt";

        public void registerLog(CategorieLog categorieLog, string message, User author)
        {
            // Gestion fichiers
            if (!Directory.Exists(Path)) // Créer le dossier s'il n'existe pas
            {
                Directory.CreateDirectory(Path);
                if(!File.Exists(Path+name)) // Créer le fichier si n'existe pas
                {
                    File.Create(Path+name);
                }
            }
            
            // Sauvegarde le log
            message = string.Format("{0,-25} | {1,15} | {2,90} | {3,60}", DateTime.Now, categorieLog, message, author);
            using (StreamWriter file = new(Path+name, append: true))
            {
                file.WriteLine(message);
            }
        }

        public List<string> loadLog()
        {
            return null;
        }

    }
}
