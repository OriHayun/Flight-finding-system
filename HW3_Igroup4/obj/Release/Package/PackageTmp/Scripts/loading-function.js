function loading() {

    let timerInterval
    Swal.fire({
        title: 'Thanks for waiting, we bring you the information',
        html: 'I will close in <b></b> milliseconds.',
        timer: 3000,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                Swal.getContent().querySelector('b')
                  .textContent = Swal.getTimerLeft()
            }, 100)
        },
        onClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        if (
          result.dismiss === Swal.DismissReason.timer
        ) { }
    })
}