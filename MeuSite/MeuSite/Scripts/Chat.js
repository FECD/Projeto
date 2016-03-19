function start(k, tamanho) {
    var body = document.getElementsByTagName("body")[0];
    var div = document.getElementById("chat");
        
    var tbl = document.createElement("table");
    var tblBody = document.createElement("tbody");
    var Nsala;
    var Nusuario;
    var Num;
    var MinhaMensagem;
    var por = '';
    var cont = 0;
    for (var item = 0; item < tamanho ; item++) {
        if (k[item] != '|'){
            por += k[item];
        }
        else {
            if (cont == 0) {
                Nsala = por;
                cont += 1;
                por = ''
            }
            else {
                if (cont == 1) {
                    Nusuario = por;
                    cont += 1;
                    por = ''
                }
                else{
                    if (cont == 2) {
                        Num = por;
                        cont += 1;
                        por = ''
                    }
                    else{
                        if (cont == 3) {
                            MinhaMensagem = por;
                            cont = 0;
                            por = ''
                        }
                    }
                }
            }
        }
    }
        
    for (var j = 0; j < 1; j++) {

        var row = document.createElement("tr");
        for (var i = 0; i < 2; i++) {

            if (i == 0) {
                var cell = document.createElement("th");
                var cellText = document.createTextNode(Nusuario);
            }
            else {
                var cell = document.createElement("td");
                var cellText = document.createTextNode(MinhaMensagem);
            }
            cell.appendChild(cellText);
            row.appendChild(cell);
        }
        tblBody.appendChild(row);
    }
    tbl.appendChild(tblBody);
    div.appendChild(tbl);
    tbl.setAttribute("border", "1");
}
function startIN(k, tamanho, comprimento) {
    var body = document.getElementsByTagName("body")[0];
    var div = document.getElementById("chat");

    var tbl = document.createElement("table");
    var tblBody = document.createElement("tbody");
    var Nsala;
    var Nusuario;
    var Num;
    var MinhaMensagem;
    var por = '';
    var cont = 0;
    for (var item = 0; item < tamanho ; item++) {
        if (k[item] != '|') {
            por += k[item];
        }
        else {
            if (cont == 0) {
                Nsala = por;
                cont += 1;
                por = ''
            }
            else {
                if (cont == 1) {
                    Nusuario = por;
                    cont += 1;
                    por = ''
                }
                else {
                    if (cont == 2) {
                        Num = por;
                        cont += 1;
                        por = ''
                    }
                    else {
                        if (cont == 3) {
                            MinhaMensagem = por;
                            cont = 0;
                            por = ''
                        }
                    }
                }
            }
        }
    }

    for (var j = 0; j < comprimento; j++) {
        list += 1;
        listStr = list.toString()
        if (Num == listStr) {
            var row = document.createElement("tr");
            for (var i = 0; i < 2; i++) {

                if (i == 0) {
                    var cell = document.createElement("th");
                    var cellText = document.createTextNode(Nusuario);
                }
                else {
                    var cell = document.createElement("td");
                    var cellText = document.createTextNode(MinhaMensagem);
                }
                cell.appendChild(cellText);
                row.appendChild(cell);
            }
            tblBody.appendChild(row);
        }
        tbl.appendChild(tblBody);
        div.appendChild(tbl);
        tbl.setAttribute("border", "1");
    }
}
function sair(a){
    @{
        ConexaoController conexao = new ConexaoController();
    }
    if (a == false){
        @{
            conexao.Sair();
        }
    }
}
function atualizarcon(){
    @{
        edbancoEntities banco = new edbancoEntities();
        foreach (var item in banco.usuario.ToList())
        {
            if (item.idusuario == ViewBag.usu){
                if (item.conexao == false)
                {
                    conexao.Sair();
                }
            }
        }
    }
}
function addmensagem(k, tamanho, valor) {
    var body = document.getElementsByTagName("body")[0];
    var div = document.getElementById("chat");

    var tbl = document.createElement("table");
    var tblBody = document.createElement("tbody");
    var Nsala;
    var Nusuario;
    var Num;
    var MinhaMensagem = valor;
    var por = '';
    var cont = 0;
    for (var item = 0; item < tamanho ; item++) {
        if (k[item] != '|'){
            por += k[item];
        }
        else {
            if (cont == 0) {
                Nsala = por;
                cont += 1;
                por = '';
            }
            else {
                if (cont == 1) {
                    Nusuario = por;
                    cont += 1;
                    por = '';
                }
                else{
                            
                    Num = por;
                    cont += 1;
                    por = '';
                }
            }
        }
    }

    for (var j = 0; j < 1; j++) {

        var row = document.createElement("tr");
        for (var i = 0; i < 2; i++) {

            if (i == 0) {
                var cell = document.createElement("th");
                var cellText = document.createTextNode(Nusuario);
            }
            else {
                var cell = document.createElement("td");
                var cellText = document.createTextNode(MinhaMensagem);
            }
            cell.appendChild(cellText);
            row.appendChild(cell);
        }
        tblBody.appendChild(row);
    }
    tbl.appendChild(tblBody);
    div.appendChild(tbl);
    tbl.setAttribute("border", "1");
}