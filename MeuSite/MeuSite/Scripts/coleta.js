//Acressentando uma linha nova na tabela
//Alterando as cores da tabela
function tabela(a) {
    var body = document.getElementsByTagName("body")[0];
    var div = document.getElementById("colunaPesquisa");
    var varlor = document.getElementById("ValorPesquisa").value;

    var tbl = document.getElementById("table");
    var tblBody = document.getElementById("tbody");
    var row = document.createElement("tr");
    var cell = document.createElement("th");
    var cellText = document.createTextNode(a);
    cell.appendChild(cellText);
    row.appendChild(cell);
    tblBody.appendChild(row);
    tbl.appendChild(tblBody);
    div.appendChild(tbl);
    tbl.setAttribute("border", "1");
    tbl.setAttribute("style", "background-color: green");
    div.setAttribute("style", "background-color: black");

}
//Alterando o valor de um elemento na tabela
function elitabela(a) {
    document.getElementById("conteudo").innerHTML = a;
}
//Deletando itens da tabela
function apagartabela(a) {
    var tabela = document.getElementById("table");
    tabela.deleteRow(0);


}
