using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        public string Path => @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gallium\\Log";
        private string name = "\\GalliumLog.txt";

        public void registerLog(CategorieLog categorieLog, string message, User author)
        {
            VerifyFiles();
            // Sauvegarde le log
            
            message = $"{DateTime.Now}|{categorieLog}|{message}|{author.NomComplet}";
            using (StreamWriter file = new(Path+name, append: true))
            {
                file.WriteLine(message);
            }
        }

        /// <summary>
        /// Récupère les logs
        /// </summary>
        /// <returns></returns>
        public List<string> loadLog()
        {
            VerifyFiles();
            List<string> logs = new List<string>();
            foreach (string line in System.IO.File.ReadLines(Path+name))
            {
                logs.Add(line);
            }
            return logs;
        }

        /// <summary>
        /// Vérifie la bonne existences des fichiers
        /// </summary>
        private void VerifyFiles()
        {
            // Gestion Dossier
            if (!Directory.Exists(Path)) // Créer le dossier s'il n'existe pas
            {
                Directory.CreateDirectory(Path);
            }

            // Création fichier
            if (!File.Exists(Path + name)) // Créer le fichier si n'existe pas
            {
                FileStream stream = File.Create(Path + name);
                stream.Close();
            }
        }
    }
}
