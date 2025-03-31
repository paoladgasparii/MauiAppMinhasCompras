using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        // Cria uma conexão assíncrona com o banco de dados SQLite
        readonly SQLiteAsyncConnection _conn;

        // Construtor da classe, inicializa a conexão com o banco de dados e cria a tabela 'Produto'
        public SQLiteDatabaseHelper(string path)
        {
            // Inicializa a conexão com o banco de dados SQLite usando o caminho fornecido
            _conn = new SQLiteAsyncConnection(path);

            // Cria a tabela 'Produto' caso ainda não exista
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // Método para inserir um novo produto no banco de dados
        public Task<int> Insert(Produto p)
        {
            // Insere o produto 'p' no banco de dados
            return _conn.InsertAsync(p);
        }

        // Método para atualizar um produto existente no banco de dados
        public Task<List<Produto>> Update(Produto p)
        {
            // Comando SQL para atualizar o produto com base no ID
            string sql = "UPDATE Produto SET Descricao=?, " +
                "Quantidade=?, Preco=? WHERE Id=?";

            // Executa o comando SQL com os parâmetros fornecidos
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // Método para deletar um produto com base no ID
        public Task<int> Delete(int id)
        {
            // Deleta o produto com o ID especificado
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Método para obter todos os produtos do banco de dados
        public Task<List<Produto>> GetAll()
        {
            // Retorna todos os produtos da tabela 'Produto'
            return _conn.Table<Produto>().ToListAsync();
        }

        // Método para buscar produtos no banco de dados pelo nome ou descrição
        public Task<List<Produto>> Search(string q)
        {
            // Comando SQL para realizar a busca de produtos com a descrição que contenha o texto 'q'
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";

            // Executa a consulta SQL e retorna os resultados
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
