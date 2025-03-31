using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a página de lista de produtos
    public partial class ListaProduto : ContentPage
    {
        // ObservableCollection para armazenar os produtos e atualizar automaticamente a UI
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        // Construtor da página, inicializa os componentes e associa a lista ao ItemsSource da ListView
        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista; // Associa a lista de produtos à ListView
        }

        // Método que é chamado quando a página aparece
        protected async override void OnAppearing()
        {
            try
            {
                // Obtém todos os produtos do banco de dados e os adiciona à lista
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

        // Método chamado quando a ListView é atualizada (pull-to-refresh)
        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {
                lista.Clear(); // Limpa a lista antes de recarregar

                // Obtém novamente os produtos e os adiciona à lista
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
                // Desativa o indicador de refresh após a atualização
                lst_produtos.IsRefreshing = false;
            }
        }

        // Método chamado quando um item da ListView é selecionado
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto p = e.SelectedItem as Produto; // Obtém o produto selecionado
                // Navega para a página de edição do produto, passando o produto selecionado como contexto
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

        // Método chamado quando o item "Adicionar" da barra de ferramentas é clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Navega para a página de adição de novo produto
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

        // Método chamado quando o texto da barra de pesquisa muda
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue; // Obtém o novo valor da pesquisa

            lista.Clear(); // Limpa a lista de produtos

            // Realiza a busca no banco de dados com o termo pesquisado
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i)); // Atualiza a lista com os produtos encontrados
        }

        // Método chamado quando o item "Somar" da barra de ferramentas é clicado
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            // Calcula o total somando o valor de 'Total' de cada produto
            double soma = lista.Sum(i => i.Total);

            // Exibe o total calculado em um alerta
            string msg = $"O total é {soma:C}";
            DisplayAlert("Total dos Produtos", msg, "Ok");
        }

        // Método chamado quando o item "Remover" de um produto é clicado no menu de contexto
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem; // Obtém o item do menu
                Produto p = selecionado.BindingContext as Produto; // Obtém o produto associado ao menu

                // Exibe uma confirmação antes de remover o produto
                bool confirm = await DisplayAlert(
                    "Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

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
