function adicionar() {

    //var nome = document.getElementById("item").value;
    //console.log("item: " + nome)

    var itens = document.getElementsByClassName("itens");
    var lista = document.getElementById("lista");

    for (var cont in itens.length) {
        if (cont.valueOf() == true) {
            console.log(cont.value);
            //lista.innerHTML += "<li>" + cont.value + "</li>"
        }
        console.log(cont.value)
    }

}