
using System;
using System.IO;

namespace Couche_IHM.ImagesProduit
{
    public class ImageManager
    {

        /// <summary>
        /// Permet de vérifier que les dossier existe bien
        /// </summary>
        public static void VerifyFiles()
        {
            // Création des répertoires
            if (!Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit"))
            {
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit");
            }
            

            //Création de l'image de base
            if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\unknownProduct.png"))
            {
                byte[] imageInconnu = ConvertImageToBlob("../../../Images/unknownProduct.png");
                CreateImageFromBlob("unknownProduct", imageInconnu);
            }
        }

        /// <summary>
        /// Permet de convertir une image en blob
        /// </summary>
        /// <param name="path"></param>
        public static byte[] ConvertImageToBlob(string path)
        {
            if (path.Contains("file:///"))
            {
                
                path = path.Substring(8, path.Length - 8);
            }
            byte[] blob;
            using (FileStream imgStream = File.OpenRead(path))
            {
                blob = new byte[imgStream.Length];
                imgStream.Read(blob, 0, (int)imgStream.Length);
            }

            return blob;
        }

        /// <summary>
        /// Permet d'obtenir l'image d'un produit ou une image par défaut si elle n'existe pas
        /// </summary>
        public static string GetImageFromProduct(string productName)
        {
            string pathImage = "";
            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\{productName}.jpg"))
            {
                pathImage = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\{productName}.jpg";
            }
            else if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\{productName}.png"))
            {
                pathImage = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\{productName}.png";
            }
            else
            {
                pathImage = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\unknownProduct.png";
            }


            return pathImage;

        }

        /// <summary>
        /// Permet de créer une image d'après un blob
        /// </summary>
        public static void CreateImageFromBlob(string fileName, byte[] blob)
        {
            if (fileName != "")
            {

                string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Gallium", "ImagesProduit", $"{fileName}.png"
                );
                using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    fileStream.Write(blob, 0, blob.Length);
                }

            }
        }
    }
}
