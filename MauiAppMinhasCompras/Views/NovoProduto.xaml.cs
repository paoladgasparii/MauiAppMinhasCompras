using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a p�gina de adi��o de um novo produto
    public partial class NovoProduto : ContentPage
    {
        // Construtor da p�gina, inicializa os componentes
        public NovoProduto()
        {
            InitializeComponent();
        }

        // M�todo que � chamado quando o item "Salvar" da barra de ferramentas � clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Cria��o de um novo objeto Produto a partir dos dados inseridos pelo usu�rio
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,  // Atribui a descri��o do produto
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte e atribui a quantidade
                    Preco = Convert.ToDouble(txt_preco.Text) // Converte e atribui o pre�o
                };

                // Insere o novo produto no banco de dados
                await App.Db.Insert(p);
                // Exibe uma mensagem de sucesso
                await DisplayAlert("Sucesso!", "Registro Inserido", "Ok");
            }
            catch (Exception ex)
            {
                // Exibe um alerta com a mensagem de erro, caso algo d� errado
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
