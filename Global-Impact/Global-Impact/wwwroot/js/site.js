window.onload = function () {

    var porta = document.querySelector('#porta')

    porta.addEventListener('mouseenter', () => {
        porta.src = 'http://localhost:5000/img/portaAberta.png'
    })

    porta.addEventListener('mouseout', () => {
        porta.src = 'http://localhost:5000/img/portaFechada.png'
    })

}