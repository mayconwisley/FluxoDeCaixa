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
using System.Windows.Shapes;
using Modelo;
using Controle;
using System.Data;

namespace FluxoDeCaixa
{
    /// <summary>
    /// Interaction logic for wpfCategoria.xaml
    /// </summary>
    public partial class wpfCategoria : Window
    {
        CategoriaControle categoriaControle;
        Categoria categoria;
        int idCategoria = 0;

        public wpfCategoria()
        {
            InitializeComponent();
        }

        private void Limpar()
        {
            txtDescricao.Clear();
            BtnAlterar.IsEnabled = false;
            BtnExcluir.IsEnabled = false;
            BtnGravar.IsEnabled = true;
        }

        private void Manutencao(TipoManutencao tipoManutencao)
        {
            categoria = new Categoria();
            categoriaControle = new CategoriaControle();

            try
            {
                categoria.Id = idCategoria;
                categoria.Descricao = txtDescricao.Text;

                if (RbEntrada.IsChecked == true)
                {
                    categoria.Tipo1 = "Entrada";
                }
                else
                {
                    categoria.Tipo1 = "Saida";
                }

                if (RbFixo.IsChecked == true)
                {
                    categoria.Tipo2 = "Fixo";
                }
                else
                {
                    categoria.Tipo2 = "Variavel";
                }

                if (tipoManutencao == TipoManutencao.Gravar)
                {
                    categoriaControle.Gravar(categoria);
                }
                else if (tipoManutencao == TipoManutencao.Alterar)
                {
                    categoriaControle.Alterar(categoria);
                }
                else if (tipoManutencao == TipoManutencao.Excluir)
                {
                    categoriaControle.Excluir(categoria);
                }
                Limpar();
                ListaCategoria();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListaCategoria()
        {
            categoriaControle = new CategoriaControle();
            try
            {
                DgCategoria.ItemsSource = categoriaControle.Consulta().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void wpfCategoria1_Loaded(object sender, RoutedEventArgs e)
        {
            ListaCategoria();
        }

        private void BtnGravar_Click(object sender, RoutedEventArgs e)
        {
            Manutencao(TipoManutencao.Gravar);
        }

        private void BtnAlterar_Click(object sender, RoutedEventArgs e)
        {
            Manutencao(TipoManutencao.Alterar);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Manutencao(TipoManutencao.Excluir);
        }

        private void DgCategoria_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row = DgCategoria.SelectedItems[0] as DataRowView;
                idCategoria = int.Parse(row["Id"].ToString());
                txtDescricao.Text = row["Descricao"].ToString();
                BtnAlterar.IsEnabled = true;
                BtnExcluir.IsEnabled = true;
                BtnGravar.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
