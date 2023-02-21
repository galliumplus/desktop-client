using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ClosedXML.Excel;

namespace Couche_Métier
{
    public class ExportAdherentToExcel : IExportableAdherent
    {
        public void Export(List<Adhérent> adhérents)
        {
            // Chemin du fichier vers le bureau
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathFile = $"{pathDesktop}\\ListeAdherents{DateTime.Now.Year}-{DateTime.Now.Year + 1}.xlsx";

            // Si le fichier existe on le met à jour
            if (File.Exists(pathFile))
            {
                XLWorkbook workbook = new XLWorkbook(pathFile);
                IXLWorksheet worksheet = workbook.Worksheets.Worksheet("Adhérents");
                affichageAdhérents(adhérents,worksheet);
                workbook.Save();
            }
            // Sinon on le créer si on est en septembre
            else if (DateTime.Now.Month == 9)
            {
                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.AddWorksheet("Adhérents");
                creationFeuilleExcel(adhérents,worksheet);
                affichageAdhérents(adhérents, worksheet);
                workbook.SaveAs(pathFile);
            }
            // Temporaire ( si pas de fichier)
            else
            {
                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.AddWorksheet("Adhérents");
                creationFeuilleExcel(adhérents, worksheet);
                affichageAdhérents(adhérents, worksheet);
                workbook.SaveAs(pathFile);
            }


            
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
        private void affichageAdhérents(List<Adhérent> adhérents,IXLWorksheet worksheet)
        {
            for (int i = 0; i < adhérents.Count-1; i++)
            {
                // Si le compte est toujours adhérent
                if (adhérents[i].StillAdherent)
                {
                    worksheet.Cell($"A{i + 2}").Value = adhérents[i].Nom;
                    worksheet.Cell($"B{i + 2}").Value = adhérents[i].Prenom;
                    worksheet.Cell($"C{i + 2}").Value = adhérents[i].Formation;
                }   
            }
        }


        /// <summary>
        /// Permet de créer la feuille d'excel
        /// </summary>
        /// <param name="adhérents">liste des adhérents</param>
        /// <param name="worksheet">feuille excel</param>
        private void creationFeuilleExcel(List<Adhérent> adhérents, IXLWorksheet worksheet)
        {
            // Première table ( listage )
            IXLRange range = worksheet.Range(1, 1, adhérents.Count, 3);
            IXLTable table = range.CreateTable();

            // apply style
            table.Name = "Liste d'adhérents";
            table.Column(1).WorksheetColumn().Width = 15;
            table.Column(2).WorksheetColumn().Width = 15;
            table.Column(3).WorksheetColumn().Width = 15;
            worksheet.Cell($"A1").Value = "Nom";
            worksheet.Cell($"B1").Value = "Prénom";
            worksheet.Cell($"C1").Value = "Formation";


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
