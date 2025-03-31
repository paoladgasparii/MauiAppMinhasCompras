using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a p�gina de lista de produtos
    public partial class ListaProduto : ContentPage
    {
        // ObservableCollection para armazenar os produtos e atualizar automaticamente a UI
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        // Construtor da p�gina, inicializa os componentes e associa a lista ao ItemsSource da ListView
        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista; // Associa a lista de produtos � ListView
        }

        // M�todo que � chamado quando a p�gina aparece
        protected async override void OnAppearing()
        {
            try
            {
                // Obt�m todos os produtos do banco de dados e os adiciona � lista
                List<Produto> tmp = await App.Db.GetAll();

                lista.Clear();
                tmp.ForEach(i => lista.Add(i)); // Atualiza a lista de produtos
            }
            catch (Exception ex)
            {
                // Exibe um alerta caso ocorra um erro
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        // M�todo chamado quando a ListView � atualizada (pull-to-refresh)
        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {
                lista.Clear(); // Limpa a lista antes de recarregar

                // Obt�m novamente os produtos e os adiciona � lista
                List<Produto> tmp = await App.Db.GetAll();
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                // Exibe um alerta em caso de erro
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                // Desativa o indicador de refresh ap�s a atualiza��o
                lst_produtos.IsRefreshing = false;
            }
        }

        // M�todo chamado quando um item da ListView � selecionado
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto p = e.SelectedItem as Produto; // Obt�m o produto selecionado
                // Navega para a p�gina de edi��o do produto, passando o produto selecionado como contexto
                Navigation.PushAsync(new Views.EditarProduto
                {
                    BindingContext = p,
                });
            }
            catch (Exception ex)
            {
                // Exibe um alerta em caso de erro
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        // M�todo chamado quando o item "Adicionar" da barra de ferramentas � clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Navega para a p�gina de adi��o de novo produto
                Navigation.PushAsync(new Views.NovoProduto());
            }
            catch (Exception ex)
            {
                // Exibe um alerta em caso de erro
                DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                lst_produtos.IsRefreshing = false; // Desativa o indicador de refresh
            }
        }

        // M�todo chamado quando o texto da barra de pesquisa muda
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue; // Obt�m o novo valor da pesquisa

            lista.Clear(); // Limpa a lista de produtos

            // Realiza a busca no banco de dados com o termo pesquisado
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i)); // Atualiza a lista com os produtos encontrados
        }

        // M�todo chamado quando o item "Somar" da barra de ferramentas � clicado
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            // Calcula o total somando o valor de 'Total' de cada produto
            double soma = lista.Sum(i => i.Total);

            // Exibe o total calculado em um alerta
            string msg = $"O total � {soma:C}";
            DisplayAlert("Total dos Produtos", msg, "Ok");
        }

        // M�todo chamado quando o item "Remover" de um produto � clicado no menu de contexto
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem; // Obt�m o item do menu
                Produto p = selecionado.BindingContext as Produto; // Obt�m o produto associado ao menu

                // Exibe uma confirma��o antes de remover o produto
                bool confirm = await DisplayAlert(
                    "Tem certeza?", $"Remover {p.Descricao}?", "Sim", "N�o");

                if (confirm)
                {
                    // Remove o produto do banco de dados e da lista
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
            catch (Exception ex)
            {
                // Exibe um alerta caso ocorra um erro
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
    }
}
