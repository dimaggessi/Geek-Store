# 🛒 [GeekStore](https://dimaggessi.info/) App 

O GeekStore é um projeto de portfólio desenvolvido para aplicar, na prática, diversos conceitos e tecnologias que estudei sobre Desenvolvimento de Software. Utiliza Angular para o Frontend e .NET C# para o Backend, criando um ambiente completo de e-commerce fictício.
</br></br>
Observação:
</br>
**` Os produtos que estão nesse site são ficcionais, não existem, e servem para ilustrar um ambiente de ecommerce `**

</br>
💳 Cartão de Crédito para Testes (Stripe)</br>
Para simular compras, utilize os seguintes dados de cartão de crédito fornecido pela API de pagamento do Stripe:
</br></br>

Número: 4242 4242 4242 4242

Validade: 12/30

CVC: 123
</br></br>
Link do projeto em: https://dimaggessi.info/
</br></br>

## 📃 Tópicos Abordados 
</br>

`Frontend`
<ul>
  <li>NgRx: Utilizado para gerenciamento de estado na autenticação (features/auth) e na listagem de produtos (shared/products).</li>
  <li>RxJS: Biblioteca essencial para programação reativa, aplicada em todo o projeto.</li>
  <li>Bootstrap 5: Framework utilizado para responsividade e estilização com SCSS.</li>
  <li>Angular Signals: Implementado para reatividade em componentes específicos.</li>
  <li>Angular v18.2.11: Versão utilizada para o desenvolvimento do frontend.</li>
</ul>

</br>

`Backend`
<ul>
  <li>CQRS Pattern com MediatR: Separação clara entre comandos (escrita) e consultas (leitura) na camada de aplicação.</li>
  <li>Generic Repository e Specification Pattern: Padrões aplicados para encapsular o acesso ao banco de dados e melhorar a manutenção do código.</li>
  <li>Result Pattern e ValidationResult: Utilizados para encapsular resultados de requisições, facilitando o tratamento de erros e sucessos.</li>
  <li>Resource Error Messages .resx: Centralização de mensagens de erro em um único local para facilitar a internacionalização.</li>
  <li>Entity Framwork Core: ORM utilizado para mapeamento e acesso ao banco de dados.</li>
  <li>Culture Middleware: Middleware para retornar mensagens em inglês (EN) ou português (PT-BR) de acordo com o idioma do navegador.</li>
  <li>Integração com APIs externas:</li>
  Stripe: Para processamento de pagamentos.</br>
  Melhor Envio: Para cálculo de fretes.
  </ul>
</ul>
</br>

## 📷 Imagens do projeto 
Página Inicial (index.html)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/index.jpg"/>
Página da Loja (/loja)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/loja.jpg"/>
Carrinho de Compras (/carrinho)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/carrinho.jpg"/>
Finalização de Pedido (/pedido)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/pedido.jpg"/>
Área Administrativa (/admin)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/admin.jpg"/>
</br></br>
Backend com Swagger (Modo Desenvolvimento)
<img style="padding-top:10px;" src="https://github.com/dimaggessi/Geek-Store/blob/main/screenshots/backend.jpg"/>

