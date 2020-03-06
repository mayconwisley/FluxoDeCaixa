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

namespace FluxoDeCaixa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        wpfCategoria cadCategoria;
        WpfMovimentacao cadMovimentacao;

        private void MenuItemCadastraCategoria_Click(object sender, RoutedEventArgs e)
        {
            cadCategoria = new wpfCategoria();
            cadCategoria.ShowDialog();
        }

        private void MenuItemCadastraMovimento_Click(object sender, RoutedEventArgs e)
        {
            cadMovimentacao = new WpfMovimentacao();
            cadMovimentacao.ShowDialog();
        }
    }
}
