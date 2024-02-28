using Spire.Barcode;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QRAttestat.Tools
{
    public class DirectionCreator
    {
        static BarcodeSettings settings = new BarcodeSettings();
        public static void GetDirection(Model model, Document resultDocument, Document cloneAttack)
        {
            settings.Type = BarCodeType.QRCode;
            settings.Data = $"{model.Number} {model.X} {model.Y} {model.Z}";
            settings.QRCodeDataMode = QRCodeDataMode.AlphaNumber;
            //settings.X = 1.0f;
            settings.QRCodeECL = QRCodeECL.H;
            settings.ShowText = false;
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            Image image = generator.GenerateImage();

            foreach (Section section in cloneAttack.Sections)
            {
                foreach (Paragraph p in section.Paragraphs)
                {
                    if (p.Text == "@pic")
                    {
                        DocPicture pic = p.AppendPicture(image);
                        p.Replace("@pic", "", false, true);
                        pic.Width = 50;
                        pic.Height = 50;
                        pic.VerticalPosition = 400;
                    }
                    p.Replace("@date", model.Date.ToString("dd MMMM yyyy"), false, true);
                    p.Replace("@year", model.Date.Year.ToString(), false, true);
                    p.Replace("@Lastname", model.X, false, true);
                    p.Replace("@Firstname", model.Y, false, true);
                    p.Replace("@Patronomic", model.Z, false, true);
                }
                resultDocument.Sections.Add(section.Clone()); 
            }

           

        }
    }
}
