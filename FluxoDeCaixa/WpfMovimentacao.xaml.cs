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
using System.Data;
using Modelo;
using Controle;
using System.Globalization;
using System.Windows.Markup;
using System.Threading;

namespace FluxoDeCaixa
{
    /// <summary>
    /// Interaction logic for WpfMovimentacao.xaml
    /// </summary>
    public partial class WpfMovimentacao : Window
    {
        MovimentacaoControle movimentacaoControle;
        Movimentacao movimentacao;
        CategoriaControle categoriaControle;
        Categoria categoria;
        ValidarNumero validarNumero;

        int idCategoria = 0, idMovimentacao = 0;
        string tipo1, tipo2;

        public WpfMovimentacao()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
        }

        private void Limpar()
        {
            TxtDescricao.Clear();
            TxtValor.Text = "0,00";
            BtnExcluir.IsEnabled = false;
            BtnAlterar.IsEnabled = false;
            BtnGravar.IsEnabled = true;
        }

        private void Manutencao(TipoManutencao tipoManutencao)
        {
            movimentacao = new Movimentacao();
            movimentacaoControle = new MovimentacaoControle();

            try
            {
                movimentacao.Id = idMovimentacao;
                movimentacao.Descricao = TxtDescricao.Text.Trim();
                movimentacao.Valor = decimal.Parse(TxtValor.Text);
                movimentacao.Data_Movimentacao = DateTime.Parse(DpDataMovimentacao.Text);
                movimentacao.Categoria = new Categoria();
                movimentacao.Categoria.Id = idCategoria;

                if (tipoManutencao == TipoManutencao.Gravar)
                {
                    movimentacaoControle.Gravar(movimentacao);
                }
                else if (tipoManutencao == TipoManutencao.Alterar)
                {
                    movimentacaoControle.Alterar(movimentacao);
                }
                else if (tipoManutencao == TipoManutencao.Excluir)
                {
                    movimentacaoControle.Excluir(movimentacao);
                }
                ListaMovimentacao();
                Limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListaMovimentacao()
        {
            movimentacaoControle = new MovimentacaoControle();
            decimal valorEntrada, valorSaida, saldo;
            try
            {
                DgMovimentacao.ItemsSource = movimentacaoControle.Consulta().DefaultView;
                valorEntrada = movimentacaoControle.TotalEntrada();
                valorSaida = movimentacaoControle.TotalSaida();
                saldo = valorEntrada - valorSaida;

                LblInformacao1.Content = "Totais: Entrada: " + valorEntrada.ToString("#,##0.00") +
                                         " - Saida: " + valorSaida.ToString("#,##0.00") +
                                         " - Saldo: " + saldo.ToString("#,##0.00");
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
                CbxCategoria.ItemsSource = null;
                CbxCategoria.ItemsSource = categoriaControle.Consulta().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InfoCategoria(Categoria categoria)
        {
            categoriaControle = new CategoriaControle();
            try
            {
                tipo1 = categoriaControle.Tipo1(categoria);
                tipo2 = categoriaControle.Tipo2(categoria);
                idCategoria = categoriaControle.Id(categoria);

                LblInformacao.Content = "Tipo 1: " + tipo1 + " - Tipo 2: " + tipo2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnGravar_Click(object sender, RoutedEventArgs e)
        {
            if (TxtDescricao.Text != "")
            {
                Manutencao(TipoManutencao.Gravar);
            }
        }

        private void BtnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (TxtDescricao.Text != "")
            {
                Manutencao(TipoManutencao.Alterar);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Manutencao(TipoManutencao.Excluir);
        }

        private void DgMovimentacao_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row = DgMovimentacao.SelectedItems[0] as DataRowView;
                idMovimentacao = int.Parse(row["Id"].ToString());
                TxtDescricao.Text = row["Movimento"].ToString();
                TxtValor.Text = row["Valor"].ToString();
                DpDataMovimentacao.Text = row["Data_Movimentacao"].ToString();
                CbxCategoria.SelectedValue = row["Categoria"].ToString();
                BtnAlterar.IsEnabled = true;
                BtnExcluir.IsEnabled = true;
                BtnGravar.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TxtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtValor.Text == "0,00")
            {
                TxtValor.Text = "";
            }
        }

        private void TxtValor_LostFocus(object sender, RoutedEventArgs e)
        {
            validarNumero = new ValidarNumero();
            TxtValor.Text = validarNumero.Zero(TxtValor.Text);
            TxtValor.Text = validarNumero.Formatar(TxtValor.Text);
        }

        private void TxtValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            validarNumero = new ValidarNumero();
            TxtValor.Text = validarNumero.Validar(TxtValor.Text);
            TxtValor.Select(TxtValor.Text.Length, 0);
        }

        private void WpfMovimentacao1_Loaded(object sender, RoutedEventArgs e)
        {
            ListaCategoria();
            ListaMovimentacao();
            DpDataMovimentacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void CbxCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoria = new Categoria();
            categoria.Descricao = CbxCategoria.SelectedValue.ToString();
            InfoCategoria(categoria);
        }
    }
}
