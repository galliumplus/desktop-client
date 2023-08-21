using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Resources;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace Couche_IHM.ImagesProduit
{
    public class ImageManager
    {
        /// <summary>
        /// Permet de convertir une image en blob
        /// </summary>
        /// <param name="path"></param>
        public byte[] ConvertImageToBlob(string path)
        {
            if (path.Contains("file:///"))
            {
                
                path = path.Substring(8, path.Length - 8);
            }
            FileStream imgStream = File.OpenRead(path);

            byte[] blob = new byte[imgStream.Length];
            imgStream.Read(blob, 0, (int)imgStream.Length);

            imgStream.Dispose();

            return blob;
        }

        /// <summary>
        /// Permet d'obtenir l'image d'un produit ou une image par défaut si elle n'existe pas
        /// </summary>
        public string GetImageFromProduct(string productName)
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
            else if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\unknownProduct.png"))
            {
                pathImage = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\unknownProduct.png";
            }
            else
            {
                // On créer l'image dans les documents TEMPORAIREMENT
                byte[] imageInconnu = ConvertImageToBlob("../../../Images/unknownProduct.png");
                CreateImageFromBlob("unknownProduct",imageInconnu);

                pathImage = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\unknownProduct.png";
            }

            return pathImage;

        }

        /// <summary>
        /// Permet de créer une image d'après un blob
        /// </summary>
        /// <param name="path"></param>
        public void CreateImageFromBlob(string fileName, byte[] blob)
        {
            File.WriteAllBytes($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Gallium\\ImagesProduit\\{fileName}.png", blob);
        }
    }
}
