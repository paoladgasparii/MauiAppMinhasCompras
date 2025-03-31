using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // Classe que representa o modelo 'Produto' no banco de dados
    public class Produto
    {
        // Campo privado para armazenar a descrição do produto
        string _descricao;

        // Propriedade Id, chave primária do produto, com auto incremento
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Propriedade Descricao, que valida se o valor não é nulo antes de ser atribuído
        public string Descricao
        {
            get => _descricao;
            set
            {
                // Lança uma exceção caso a descrição seja nula
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha a descrição");
                }

                // Atribui o valor à variável privada _descricao
                _descricao = value;
            }
        }

        // Propriedade Quantidade do produto
        public double Quantidade { get; set; }

        // Propriedade Preco do produto
        public double Preco { get; set; }

        // Propriedade Total que calcula o valor total (Quantidade * Preço)
        public double Total { get => Quantidade * Preco; }

    } // Fecha a classe Produto
} // Fecha o namespace MauiAppMinhasCompras.Models

