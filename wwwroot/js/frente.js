/* Declaração de variáveis */
var enderecoProduto = "https://localhost:5000/Produtos/Produto/";
var enderecoGerarVenda = "https://localhost:5000/Produtos/gerarvenda";
var produto;
var compra = [];
var __totalVenda__ = 0.0;

/* Início */

$("#posvenda").hide();

atualizarTotal();

/* Funções */

function atualizarTotal() {
    $("#totalVenda").html(__totalVenda__);
}

function preencherFomulario(dadosProduto) {
    $("#campoNome").val(dadosProduto.nome);
    $("#campoCategoria").val(dadosProduto.categoria.nome);
    $("#campoFornecedor").val(dadosProduto.fornecedor.nome);
    $("#campoPreco").val(dadosProduto.precoDeVenda);
}

function zerarFormulario() {
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
}

function adicionarNaTabela(p, q) {
    var produtoTemp = {};

    Object.assign(produtoTemp, produto);

    var venda = { produto: produtoTemp, quantidade: q, subtotal: produtoTemp.precoDeVenda * q };

    __totalVenda__ += venda.subtotal;
    atualizarTotal();

    compra.push(venda);
     
    $("#compras").append(`<tr>
        <td>${p.id}</td>
        <td>${p.nome}</td>
        <td>${q}</td>
        <td>${p.precoDeVenda} R$</td>
        <td>${p.medicao}</td>
        <td>${p.precoDeVenda * q} R$</td>
        <td> <button class="btn btn-danger"> Remover </button> </td>
     <tr/>`);
}

$("#produtoForm").on("submit", function (event) {
    event.preventDefault();
    var produtoParaTabela = produto;
    var qtd = $("#campoQuantidade").val();

    console.log(produtoParaTabela);
    console.log(qtd);
    adicionarNaTabela(produtoParaTabela, qtd);
    //var produto = undefined;
    zerarFormulario();
});

/* Ajax */
$("#pesquisar").click(function () {
    var codProduto = $("#codProduto").val();
    var enderecoTemporario = enderecoProduto + codProduto;
    $.post(enderecoTemporario, function (dados, status) {
        produto = dados;

        var med = "";

        switch (produto.medicao) {

            case 0:
                med = "L";
                break;

            case 1:
                med = "K";
                break;

            case 2:
                med = "U";
                break;

            default:
                med = "U";
                break;
        }

        produto.medicao = med;

        preencherFomulario(produto);
    }).fail(function () {
        alert("Produto inválido!");
    });
});

/* Finalização de venda */
$("#finalizarVendaBTN").click(function() {
    if (__totalVenda__ <= 0) {
        alert("Compra inválida. Nenhum produto adicionado!");
        return;
    }

    var _valorPago = $("#valorPago").val();

    if (!isNaN(_valorPago)) {

        _valorPago = parseFloat(_valorPago);

        if (_valorPago >= __totalVenda__) {

            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);

            var _troco = _valorPago - __totalVenda__;
            $("#troco").val(_troco);

            //Processar array de compra
            compra.forEach(elemento => {
                elemento.produto = elemento.produto.id;
            });

            //Preparar um novo objeto
            var _venda = { total: __totalVenda__, troco: _troco, produtos: compra };

            //Enviar pro back end
            $.ajax({
                type: "post",
                url: enderecoGerarVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(_venda),
                success: function (data) {
                    console.log("Dados enviados com sucesso!");
                    console.log(data);
                }
            });

        } else {

            alert("Valor pago não pode ser menor que o total!")

        }

    } else {

        alert("Valor pago inválido!");

    }
});

function restaurarModal() {
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
}

$("#fecharModal").click(function () {
    restaurarModal();
});
