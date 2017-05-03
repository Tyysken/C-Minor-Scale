$(document).ready(function () {
    $("#signin").click(function (event) {
        event.preventDefault();
        var email = $("#inputEmail").val();
        var pass = $("#inputPassword").val();
        $.ajax({
            url: "https://stage-core.intelligentdesk.com/v3/user/" + email,
            type: "GET",
            headers: {
                'idesk-auth-method': 'up',
                'idesk-auth-username': email,
                'idesk-auth-password': SHA256(email + pass),
                'Accept': 'application/vnd.idesk-v5+json'
            },
            contentType: 'application/vnd.idesk-v5+json',
            success: function (data) {
                alert('Success!');
                localStorage.setItem('user', data.Username)
                localStorage.setItem('password', SHA256(email + pass))
                window.location.href = '/Home/Index/'
            },

            error: function (data) {
                alert(JSON.parse(data.responseText).Message);
            }
        });
    })
})