using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QRAttestat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM(grid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainVM)DataContext;
            var t = Clipboard.GetText();
            var rows = t.Split("\r\n");
            foreach (var row in rows)
            {
                var cols = row.Split("\t");
                if (cols.Length  == 4)
                    vm.AddUser(cols);
            }
        }
    }
}
