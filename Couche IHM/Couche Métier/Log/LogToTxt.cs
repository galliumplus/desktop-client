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
        public string Path => @Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Gallium\\Log\\GalliumLog.txt";

        public void registerLog(CategorieLog categorieLog, string message)
        {
            // Créer le fichier s'il n'existe pas
            if (!File.Exists(Path))
            {
                Directory.CreateDirectory(Path.Split('\\')[Path.Split('\\').Length - 1]);
                File.Create(Path);
            }

            // Sauvegarde le log
            message = $"{DateTime.Now} | {categorieLog.ToString()} | {message}";
            using (StreamWriter file = new(Path, append: true))
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
