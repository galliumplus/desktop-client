using Modeles;
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
    public class LogToTxt : ILog
    {
        public string Path => @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gallium\\Log";
        private string name = "\\GalliumLog.txt";

        public void registerLog(CategorieLog categorieLog, string message, User author)
        {
            // Vérification du fichier et récupération de la modification
            VerifyFiles();

            // Formattage du log
            string log = $"{DateTime.Now}|{categorieLog}|{message}|{author.NomComplet}|{categorieLog}";

            // Ecriture du log
            using (StreamWriter file = new(Path+name, append: true))
            {
                file.WriteLine(log);
            }
        }

        /// <summary>
        /// Récupère les logs
        /// </summary>
        /// <returns></returns>
        public List<Modeles.Log> loadLog()
        {
            VerifyFiles();
            List<Modeles.Log> logs = new List<Modeles.Log>();
            foreach (string line in System.IO.File.ReadLines(Path + name))
            {
                // Récupération des informations
                string[] SplitedLogLine = line.Split('|');
                string date = DateTime.Parse(SplitedLogLine[0]).ToString("g");
                string type = SplitedLogLine[1];
                string message = SplitedLogLine[2];
                string auteur = SplitedLogLine[3];
                string operation = SplitedLogLine[4];

                Modeles.Log log = new Modeles.Log(date, type, message, auteur, operation);
                logs.Add(log);
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
