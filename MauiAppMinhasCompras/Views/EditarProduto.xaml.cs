using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a p�gina de edi��o de um produto
    public partial class EditarProduto : ContentPage
    {
        // Construtor da p�gina, que inicializa os componentes da interface
        public EditarProduto()
        {
            InitializeComponent();
        }

        // M�todo ass�ncrono chamado quando o item da barra de ferramentas � clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obt�m o produto anexado ao contexto de dados da p�gina (BindingContext)
                Produto produto_anexado = BindingContext as Produto;

                // Cria um novo objeto Produto com os dados modificados
                Produto p = new Produto
                {
                    Id = produto_anexado.Id, // Mant�m o mesmo ID do produto original
                    Descricao = txt_descricao.Text, // Atualiza a descri��o com o texto do campo de entrada
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Atualiza a quantidade convertendo o texto para double
                    Preco = Convert.ToDouble(txt_preco.Text) // Atualiza o pre�o convertendo o texto para double
                };

                // Atualiza o produto no banco de dados
                await App.Db.Update(p);

                // Exibe uma mensagem de sucesso
                await DisplayAlert("Sucesso!", "Registro atualizado", "OK");

                // Retorna � p�gina anterior na navega��o
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe uma mensagem com o erro
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
