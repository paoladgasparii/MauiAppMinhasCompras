using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Classe que representa a página de edição de um produto
    public partial class EditarProduto : ContentPage
    {
        // Construtor da página, que inicializa os componentes da interface
        public EditarProduto()
        {
            InitializeComponent();
        }

        // Método assíncrono chamado quando o item da barra de ferramentas é clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtém o produto anexado ao contexto de dados da página (BindingContext)
                Produto produto_anexado = BindingContext as Produto;

                // Cria um novo objeto Produto com os dados modificados
                Produto p = new Produto
                {
                    Id = produto_anexado.Id, // Mantém o mesmo ID do produto original
                    Descricao = txt_descricao.Text, // Atualiza a descrição com o texto do campo de entrada
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Atualiza a quantidade convertendo o texto para double
                    Preco = Convert.ToDouble(txt_preco.Text) // Atualiza o preço convertendo o texto para double
                };

                // Atualiza o produto no banco de dados
                await App.Db.Update(p);

                // Exibe uma mensagem de sucesso
                await DisplayAlert("Sucesso!", "Registro atualizado", "OK");

                // Retorna à página anterior na navegação
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
