using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a página de adição de um novo produto
    public partial class NovoProduto : ContentPage
    {
        // Construtor da página, inicializa os componentes
        public NovoProduto()
        {
            InitializeComponent();
        }

        // Método que é chamado quando o item "Salvar" da barra de ferramentas é clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Criação de um novo objeto Produto a partir dos dados inseridos pelo usuário
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,  // Atribui a descrição do produto
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte e atribui a quantidade
                    Preco = Convert.ToDouble(txt_preco.Text) // Converte e atribui o preço
                };

                // Insere o novo produto no banco de dados
                await App.Db.Insert(p);
                // Exibe uma mensagem de sucesso
                await DisplayAlert("Sucesso!", "Registro Inserido", "Ok");
            }
            catch (Exception ex)
            {
                // Exibe um alerta com a mensagem de erro, caso algo dê errado
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
