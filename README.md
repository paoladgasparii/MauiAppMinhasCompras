# AppMinhasCompras

Este é um aplicativo desenvolvido com .NET MAUI para gerenciar uma lista de compras. Ele utiliza SQLite para armazenar dados localmente e oferece funcionalidades para adicionar, editar, visualizar e excluir produtos.

## Estrutura do Repositório

### **SQLiteDatabaseHelper.cs:**
A classe `SQLiteDatabaseHelper` é responsável pela interação com o banco de dados SQLite. Ela contém métodos para inserir, atualizar, excluir e consultar produtos na base de dados local.

- **Métodos:**
  - `Insert(Produto p)`: Insere um novo produto na tabela de produtos.
  - `Update(Produto p)`: Atualiza as informações de um produto existente.
  - `Delete(int id)`: Exclui um produto baseado no ID.
  - `GetAll()`: Retorna todos os produtos armazenados no banco.
  - `Search(string q)`: Realiza uma busca de produtos com base em um termo de pesquisa.

### **Produto.cs:**
A classe `Produto` representa um produto na aplicação. Cada produto possui uma descrição, quantidade, preço e total.

- **Propriedades:**
  - `Id`: Identificador único do produto (chave primária).
  - `Descricao`: Descrição do produto.
  - `Quantidade`: Quantidade do produto.
  - `Preco`: Preço unitário do produto.
  - `Total`: Calcula o total baseado na quantidade e no preço.

### **EditarProduto.xaml:**
A interface de usuário para editar os detalhes de um produto já existente. Contém campos para editar a descrição, quantidade e preço do produto.

- **Função:** Exibe um formulário de edição onde o usuário pode alterar as informações de um produto. Após salvar, as mudanças são persistidas no banco de dados.

### **EditarProduto.xaml.cs:**
A lógica por trás da interface de edição de produto.

- **Função:** 
  - Captura os dados modificados pelo usuário no formulário.
  - Atualiza o produto no banco de dados SQLite.
  - Exibe uma mensagem de sucesso ou erro dependendo do resultado da operação.

### **ListaProduto.xaml:**
A interface de usuário para exibir a lista de produtos cadastrados.

- **Função:** Exibe uma lista de produtos com informações como ID, descrição, preço, quantidade e total. Permite ao usuário buscar produtos, editar ou excluir.

### **ListaProduto.xaml.cs:**
A lógica por trás da tela de listagem de produtos.

- **Função:**
  - Carrega todos os produtos do banco de dados e os exibe na interface.
  - Permite a pesquisa de produtos.
  - Lida com ações de seleção, que levam à tela de edição de produtos.
  - Oferece a opção de adicionar um novo produto.
  - Permite a exclusão de produtos diretamente da lista.

### **NovoProduto.xaml:**
A interface de usuário para adicionar um novo produto.

- **Função:** Exibe um formulário para o usuário preencher os dados de um novo produto, como descrição, quantidade e preço.

### **NovoProduto.xaml.cs:**
A lógica por trás da interface de criação de um novo produto.

- **Função:** 
  - Captura os dados inseridos pelo usuário no formulário.
  - Insere o novo produto no banco de dados SQLite.
  - Exibe uma mensagem de sucesso ou erro dependendo do resultado da operação.

### **App.xaml.cs:**
Configura o banco de dados SQLite e a navegação do aplicativo. Define a página inicial como a lista de produtos.

### **MainPage.xaml:**
A interface de usuário para a página principal do aplicativo. Inicialmente apresenta um contador de cliques.

### **MainPage.xaml.cs:**
Controla a lógica por trás da página principal, especificamente o contador de cliques no botão.

### **MauiProgram.cs:**
Configura o aplicativo MAUI, definindo as fontes usadas e configurando o logging no ambiente de desenvolvimento.
