function buscarCEP() {
    var cep = document.querySelector("#cep").value;
    var buscar = document.querySelector("#btnBuscar");
    if (cep.length == 9) {
        var inputCep = document.querySelector("#cepForm");
        var inputUrl = document.querySelector("#urlForm");
        var url = window.location.href
        inputCep.value = cep.replace(/-/g, "")
        inputUrl.value = url;
        buscar.click();
    }
}