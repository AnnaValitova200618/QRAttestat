using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using QRAttestat.Tools;
using Spire.Doc;

namespace QRAttestat
{
    public class MainVM : BaseVM
    {
        private Model selectModel;

        public ObservableCollection<Model> Models { get; set; } = new();
        public Model SelectModel 
        {
            get => selectModel;
            set
            {
                selectModel = value;
                Signal();
            }
        }
        public DateTime SelectDate { get; set; } = DateTime.Now;
        public CustomCommand SetDate {  get; set; }
        public CustomCommand Create {  get; set; }
        internal void AddUser(string[] cols)
        {
            Models.Add(new Model { Number = cols[0], X = cols[1], Y = cols[2], Z = cols[3]});

            
        }
        public MainVM(System.Windows.Controls.DataGrid grid)
        {
            SetDate = new CustomCommand(() =>
            {
                foreach (var model in Models)
                {
                    model.Date = SelectDate;
                }
                Signal(nameof(Models));
            });
            Create = new CustomCommand(() =>
            {
                if (grid.SelectedItems == null)
                {
                    MessageBox.Show("Необходимо выбрать запись");
                    return;
                }
                Document document = new Document();
                document.LoadFromFile("титул2.docx");
               

                Document resultDocument = new Document();
                foreach ( Model model in grid.SelectedItems)
                {
                    var cloneAttack = document.Clone();
                    DirectionCreator.GetDirection(model, resultDocument, cloneAttack);
                }

                try
                {
                    string file = $"Аттестаты.docx";
                    resultDocument.SaveToFile(file, FileFormat.Docx);
                    ProcessStartInfo process = new ProcessStartInfo();
                    process.FileName = "explorer.exe";
                    process.Arguments = file;
                    process.UseShellExecute = true;
                    System.Diagnostics.Process.Start(process);
                }
                catch
                {
                    MessageBox.Show("Закройте предыдущий файл", "Несерьёзная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });
        }
    }
}
