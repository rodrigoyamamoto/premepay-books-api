# premepay-books-api
Projeto de avaliação desenvolvido para a oportunidade de Desenvolvedor Backend Junior para a empresa [Preme Pay](https://premepay.com/)

## Objetivo
Mostrar as habilidades de codificação e práticas utilizadas.

## Teste de codificação:
<br>•	Criar endpoints para CRUD de entidade book (id:number, title:String, Autor:String e Price:decimal); 
<br>•	Criar endpoint para compra de um ou mais livros, considerando possibilidade de conversão do valor para USD, EUR ou GBP;
<br>•	Criar endpoint para listagem dos pedidos realizados (exibir valor em BRL e moeda estrangeira);
 
## Desejamos ver no teste:
<br>•	Disponibilizar o código fonte em repositório GIT;
<br>•	Solução escrita em .Net Core Web Api;
<br>•	Testes unitários automatizados;
<br>•	Código legível;
<br>•	Readme sobre como rodar e utilizar o projeto;
<br>•	Armazenar os dados em banco de dados InMemory;
<br>•	Para obtenção das cotações, sugerimos utilizar APIs públicas como https://exchangeratesapi.io
 
## Não obrigatório porém desejável:
<br>•	User interface em React.js;
<br>•	Disponibilizar API na AWS;

## Executando o Projeto 
Baixar o código fonte e abrir a o arquivo PremePayBooks.API.sln no Visual Studio. Após o projeto ser carregado, clicar em Run para executar.

Com o projeto em execução, utilizar uma ferramenta como [Postman](https://www.postman.com/) para efetuar as requisições REST. 
<br/>
## CRUD de Livros
Todos os requests devem possuir as informações no formato Json

#### Incluindo um novo livro
• Apontar para o endereço https://localhost:5001/api/book/ e utilizar o tipo Post para o Request.
<br>• Para incluir apenas um livro, utilize
```
{
    "Title": "Baldwin's Kitchen",
    "Author": "Emily Na",
    "Price": "10.99"
}
```
• Para incluir mais de um livro no mesmo Request, utilize
```
[
	{
	    "Title": "Baptism by History",
	    "Author": "Miller Wilbourn",
	    "Price": "12.99"
	},
	{
	    "Title": "Birthing a New World",
	    "Author": "Marquita R. Smith",
	    "Price": "13.99"
	 },
]
```

## Atualizando as informações de um Livro
• Apontar para o endereço https://localhost:5001/api/book/{id} (onde "id" é um valor do tipo Guid e possui o formato gerado automaticamente por seu proprio algoritmo. 
O endereço deve se parecer como o seguinte: https://localhost:5001/api/book/feb34aa0-fbc0-44a0-a219-f8ef51fbdb3e
<br>• Utilizar o tipo Put para o Request.

## Localizando um livro pelo Id
<br>• Apontar para o endereço https://localhost:5001/api/book/{id} 
<br>O endereço deve se parecer como o seguinte: https://localhost:5001/api/book/feb34aa0-fbc0-44a0-a219-f8ef51fbdb3e
<br>• Utilizar o tipo Get para o Request
```
[
	{
	    "Title": "Baptism by History - Alterado",
	    "Author": "Miller Wilbourn",
	    "Price": "12.99"
	},
	{
	    "Title": "Birthing a New World",
	    "Author": "Marquita R. Smith",
	    "Price": "13.99"
	 },
]
```

## Listando todos os Livros
• Apontar para o endereço https://localhost:5001/api/book/
<br>• Utilizar o tipo Get para o Request.

## Removendo um livro pelo Id
• Apontar para o endereço https://localhost:5001/api/book/{id} 
<br>O endereço deve se parecer como o seguinte: https://localhost:5001/api/book/feb34aa0-fbc0-44a0-a219-f8ef51fbdb3e.
<br>• Utilizar o tipo Delete para o Request.


# Order (Pedidos)
É possível efetuar o pedido de um ou mais livros.
Para definir qual será a moeda utilizada para definir os valores/conversão, deve ser utilizado o nome do simbolo na variavel "c" na Url. Os tipos considerados são: USD, GBP, EUR e BRL. Qualquer tipo de moeda diferente dos indicados não serão suportados. Os ids para cada simbolo segue a ordem USD = 1, EUR = 2,  GBP = 3,   BRL = 4 e poderão ser encontrados nos elementos **currencyTypeBase** e **currencyTypeConverted**.
O elemento **Total**  refere-se ao valor total dos pedidos (somando o preço de cada livro) e o elemento **convertedTotal**  refere-se ao valor total já convertido pela moeda indicada na Url. Mais detalhes podem ser encontrados na seção **Localizando um pedido pelo seu Id**.

## Incluindo um novo pedido
• Apontar para o endereço https://localhost:5001/api/order?c=EUR e utilizar o tipo Post para o Request
<br>• O seguinte Json inclui um pedido com apenas um livro e considerada o valor como **Euro**
```
{
    "customerName": "John Doe",
    "books": {
        "title": "Baldwin's Transatlantic Reverberations",
        "author": "Jovita dos Santos Pinto, Noemi Michel",
        "price": 11.99
    }
}
```

• Para incluir um pedido com mais de um livro, utilize
```
{
    "customerName": "Jane Doe",
    "books": [{
            "title": "Baldwin's Transatlantic Reverberations",
            "author": "Jovita dos Santos Pinto, Noemi Michel",
            "price": 11.99
        },
        {
            "title": "Baldwin's Kitchen",
            "author": "Emily Na",
            "price": 10.99
        }
    ]
}
```

## Localizando um pedido pelo Id
<br>• Apontar para o endereço https://localhost:5001/api/order/{id} ?c=BRL
<br>O endereço deve se parecer como o seguinte: https://localhost:5001/api/order/feb34aa0-fbc0-44a0-a219-f8ef51fbdb3e?c=BRL
<br>• Utilizar o tipo Get para o Request.
• Em cenários de listagem de Pedidos, o valor base (utilizado no registro do Pedido) é convertido para o definido na variavel "c", retornando um Json parecido com o abaixo:
```
  {
    "customerName": "John Doe",
    "currencyTypeBase": 1,
    "currencyTypeConverted": 4,
    "total": 57.97,
    "convertedTotal": 325.33,
    "books": [{
            "title": "Baldwin's Transatlantic Reverberations",
            "author": "Jovita dos Santos Pinto, Noemi Michel",
            "price": 67.29,
            "orderId": "f5f54a91-006d-4a7b-9eba-4d145eeeca21",
            "id": "adf26df9-35c3-41c5-8dfc-31b9d0963d30"
        },
        {
            "title": "Unwords",
            "author": "Shane Weller",
            "price": 145.86,
            "orderId": "f5f54a91-006d-4a7b-9eba-4d145eeeca21",
            "id": "edb197c8-0493-4549-af6f-21a2a5f95350"
        },
        {
            "title": "Trends in Baldwin Criticism",
            "author": "Joseph Vogel",
            "price": 112.19,
            "orderId": "f5f54a91-006d-4a7b-9eba-4d145eeeca21",
            "id": "a719f337-d75f-4906-9fc1-1059f802a4fd"
        }
    ],
    "id": "f5f54a91-006d-4a7b-9eba-4d145eeeca21"
}
```

#### Listando todos os Pedidos
• Apontar para o endereço https://localhost:5001/api/order?c=?USD
<br>• Utilizar o tipo Get para o Request.
