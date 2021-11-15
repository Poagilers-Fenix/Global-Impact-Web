window.onload = function () {

    var porta = document.querySelector('#porta')

    porta.addEventListener('mouseenter', () => {
        porta.src = '../img/portaAberta.png'
    })

    porta.addEventListener('mouseout', () => {
        porta.src = '../img/portaFechada.png'
    })

}

function goBack() {
    window.history.back();
}
