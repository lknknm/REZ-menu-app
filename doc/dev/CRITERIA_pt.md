# REZ Restaurant App - CRITERIA (Portuguese)

O seguinte documento apresenta a primeira ideação e descrição de critérios de aceitação em cada página que será mostrada ao usuário final do aplicativo. 
O relatório tem como o objetivo antecipar o processo de prototipação do aplicativo e prever possíveis ajustes que serão necessários para melhor atender as necessidades tanto do usuário final quanto do restaurante.

## Menu Principal
#### Descrição: 
Como usuário, quero poder visualizar o cardápio do restaurante para conhecer as opções disponíveis. Desejo que os itens do cardápio estejam agrupados por categorias.

#### Critérios de Aceitação:

- Como usuário, quero acessar a página do cardápio que apresenta todos os itens disponíveis no restaurante.

- Os itens do cardápio devem ser organizados em categorias, como "Entradas", "Pratos Principais", "Sobremesas", etc.

- Deve ser possível selecionar uma categoria e visualizar apenas os itens pertencentes a essa categoria.

- Cada item do cardápio deve exibir informações como nome, descrição, preço e uma imagem ilustrativa.

- Ao selecionar um item específico, devo ser direcionado para uma página de detalhes exclusiva para esse item.

- Na página de detalhes do item, desejo visualizar informações mais detalhadas, como ingredientes, preço e uma imagem ilustrativa.  outras observações relevantes.

- Deve existir uma opção para retornar à página do cardápio após visualizar os detalhes do item.

- A busca por itens específicos no cardápio pode ser considerada como uma funcionalidade adicional, mas não é um requisito essencial para esta user story.

 
#### Título: Menu de categorias

Descrição: Como usuário, quero poder acessar um menu na parte superior do cardápio que contenha as categorias disponíveis (entradas, prato principal, bebidas, sobremesas). Ao clicar em uma das categorias, o cardápio deve ser atualizado automaticamente, mostrando apenas os itens referentes à categoria selecionada.

Critérios de Aceitação:

- O menu de categorias deve estar localizado na parte superior da página do cardápio.

- O menu de categorias deve ser exibido em forma de lista horizontal.
- O menu deve conter as seguintes categorias: "Entradas", "Prato Principal", "Bebidas" e "Sobremesas".

- Ao selecionar uma categoria no menu, a página deve rolar automaticamente até a seção correspondente do cardápio, tornando mais fácil para o usuário encontrar os itens desejados.

- O menu de categorias deve permanecer visível em todas as páginas relacionadas ao cardápio.


## ShoppingCart

#### Descrição: 
Como usuário, desejo poder adicionar itens do cardápio ao carrinho de pedidos, visualizar os itens adicionados, remover itens individuais, limpar o carrinho e efetuar o pedido. Além disso, quero que os itens adicionados ao carrinho sejam mantidos mesmo após sair e entrar novamente na aplicação.

#### Critérios de Aceitação:

- Deve existir uma opção claramente visível no cardápio principal e no detalhe do item que permita adicionar um item ao carrinho de pedidos.

- Ao selecionar essa opção, o item selecionado deve ser adicionado ao carrinho.

- Os itens adicionados ao carrinho devem ser armazenados em um banco de dados

- Ao sair e entrar novamente na aplicação, os itens previamente adicionados ao carrinho devem ser recuperados do banco de dados e exibidos na tela de visualização do carrinho.

- A tela de visualização do carrinho de pedidos deve apresentar uma lista dos itens adicionados, exibindo informações como nome, quantidade, preço unitário e preço total por item.

- Para cada item no carrinho, deve existir uma opção para removê-lo individualmente.

- Deve haver uma opção para limpar todo o carrinho, removendo todos os itens adicionados.

- A tela de visualização do carrinho deve exibir o valor total do pedido, somando o preço total de todos os itens adicionados.

- Após revisar os itens no carrinho e estiver satisfeito, o usuário deve ter a opção de efetuar o pedido.

- Ao efetuar o pedido, o sistema deve gerar automaticamente um número único para identificação do pedido.

- O número do pedido deve ser registrado no banco de dados junto com os itens adicionados no carrinho para referência futura.

- Após efetuar o pedido, o carrinho deve ser limpo automaticamente, removendo todos os itens adicionados.

- Deve ser possível vincular o pedido a um usuário, para que haja divisão na conta.

## Separar a conta ao fazer o pedido

#### Descrição: 

Como usuário, desejo que o sistema forneça uma funcionalidade para informar sobre a divisão da conta ao fazer o pedido. Isso permitirá que eu e meus acompanhantes dividamos os custos de forma adequada e transparente, separando os itens de acordo com as pessoas selecionadas.

#### Critérios de Aceitação:

- Durante o processo de efetuar o pedido, o sistema deve fornecer uma opção clara para informar sobre a divisão da conta.

- Caso a opção de divisão da conta seja selecionada, o sistema irá habilitar um campo para preenchimento do nome do usuário para qual o pedido será enviado. 

- Caso algum pedido já tenha sido efetuado com a opção de divisão habilitada, o sistema deve exibir o usuário utilizado no pedido anterior e a opção de incluir um novo


