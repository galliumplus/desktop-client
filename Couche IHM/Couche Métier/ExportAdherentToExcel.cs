using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ClosedXML.Excel;

namespace Couche_Métier
{
    /// <summary>
    /// Export adhérent sous format excel
    /// </summary>
    public class ExportAdherentToExcel : IExportableAdherent
    {
        public void Export(List<Adhérent> adhérents)
        {
            // Chemin du fichier vers le bureau
            string pathDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gallium";
            string pathFile = $"{pathDocuments}\\ListeAdherents{DateTime.Now.Year}-{DateTime.Now.Year + 1}.xlsx";

            // Création du répertoire s'il n'existe pas
            if (!Directory.Exists(pathDocuments)) 
            {
                Directory.CreateDirectory(pathDocuments);

            }


            // Création du excel
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.AddWorksheet("Adhérents");
            CreationTableauxExcel(adhérents, worksheet);
            AffichageAdherents(adhérents, worksheet);
            workbook.SaveAs(pathFile);


            
            // Lancement d'excel
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(pathFile)
            {
                UseShellExecute = true
            };
            p.Start();
            

        }

        /// <summary>
        /// Permet d'afficher les adhérents sur excel
        /// </summary>
        /// <param name="adhérents">liste des adhérents</param>
        /// <param name="worksheet">feuille excel</param>
        private void AffichageAdherents(List<Adhérent> adhérents,IXLWorksheet worksheet)
        {

            // Remplissage des adhérents
            int compteur = 0;
            for (int i = 0; i < adhérents.Count-1; i++)
            {
                // Si le compte est toujours adhérent
                if (adhérents[i].StillAdherent)
                {
                    worksheet.Cell($"A{compteur + 2}").Value = adhérents[i].Nom;
                    worksheet.Cell($"B{compteur + 2}").Value = adhérents[i].Prenom;
                    worksheet.Cell($"C{compteur + 2}").Value = adhérents[i].Formation;
                    compteur++;
                }   
            }

            // Création style de la table des adhérents
            IXLRange range = worksheet.Range(1, 1, compteur + 1, 3);
            IXLTable table = range.CreateTable();
            table.Name = "Liste d'adhérents";
            table.Column(1).WorksheetColumn().Width = 15;
            table.Column(2).WorksheetColumn().Width = 15;
            table.Column(3).WorksheetColumn().Width = 15;
            worksheet.Cell($"A1").Value = "Nom";
            worksheet.Cell($"B1").Value = "Prénom";
            worksheet.Cell($"C1").Value = "Formation";
        }


        /// <summary>
        /// Permet de créer la feuille d'excel
        /// </summary>
        /// <param name="adhérents">liste des adhérents</param>
        /// <param name="worksheet">feuille excel</param>
        private void CreationTableauxExcel(List<Adhérent> adhérents, IXLWorksheet worksheet)
        { 

            // Deuxième table ( comptage )
            IXLRange range2 = worksheet.Range(1, 5, 6, 6);
            IXLTable table2 = range2.CreateTable();

            // Valeurs
            table2.Column(1).WorksheetColumn().Width = 11;
            table2.ShowAutoFilter = false;
            worksheet.Cell($"E1").Value = "Formation";
            worksheet.Cell($"E2").Value = "Total";
            worksheet.Cell($"E6").Value = "Autres";
            worksheet.Cell($"E5").Value = "1A";
            worksheet.Cell($"E4").Value = "2A";
            worksheet.Cell($"E3").Value = "3A";
            worksheet.Cell($"F1").Value = "NB";

            // Formules
            worksheet.Cell($"F2").FormulaA1 = "=COUNTA(B:B)-1";
            worksheet.Cell($"F3").FormulaA1 = @"=COUNTIF(C:C,""3A"")";
            worksheet.Cell($"F4").FormulaA1 = "=COUNTIF(C:C,\"2A\")";
            worksheet.Cell($"F5").FormulaA1 = "=COUNTIF(C:C,\"1A\")";
            worksheet.Cell($"F6").FormulaA1 = "=F2-F3-F4-F5";
            

        }


    }
}
