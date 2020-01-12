$('#Admine').click(function () {
    $('.bg-modal').css('display', 'flex');
    $('body').addClass('dontMoveY-axis');

});

$(document).on("click", ".logIn-popup-close", function () {
    $('.bg-modal').css('display', 'none');
    $('body').removeClass('dontMoveY-axis');
})

$('#logInForm').submit(function (e) {
    ajaxCall("GET", "../api/admin", "", logInSuccessCB, logInErrorCB);
    e.preventDefault();

})
function logInSuccessCB(adminList) {
    let userName = $('#userName').val();
    let userPassword = $('#userPassword').val();
    console.log(userName)
    console.log(userPassword)

    for (let i in adminList) {
        if (adminList[i].UserName == userName && adminList[i].UserPassword == userPassword) {
            location.href = 'admin.html'
            return;
        }
    }
    logInErrorCB();
}
function logInErrorCB() {
    alert("User name or password not correct try agine")
}
