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
    public class LogVenteToTxt : ILog
    {
        public string Path => @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gallium\\Log";
        private string name = "\\GalliumLog.txt";

        public void registerLog(CategorieLog categorieLog, object itemModif, User author)
        {
            // Vérification du fichier et récupération de la modification
            VerifyFiles();
            Adhérent u = (Adhérent)itemModif;

            // Modification du message selon l'opération
            string message = "";
            switch (categorieLog)
            {
                case CategorieLog.VENTE:
                    //message = $"{u.NomCompletIHM.ToUpper()} a acheté des produits ";
                    break;

            }      
            string log = $"{DateTime.Now}|VENTE|{message}|{author.NomComplet}|{categorieLog}";

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
