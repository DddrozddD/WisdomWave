function RegistrationUser() {
    var email = $("#EmailForReg").val();
    var password = $("#PasswordForReg").val();
    var name = $("#NameForReg").val();
    var surname = $("#SurnameForReg").val();
    var telephone = $("#TelephoneForReg").val();
    var confirmPass = $("#ConfirmPassForReg").val();

    $.ajax({
        async: true,
        url: "/api/ApiAuthorization/RegUser",
        method: "POST",
        data: JSON.stringify({
            "Email": email,
            "Password": password,
            "ConfirmPass": confirmPass,
            "Name": name,
            "Surname": surname,
            "Telephone": telephone
        }),
        headers: {
            "content-type": "application/json; odata=verbose"
        },
        success: function (data) {
            if (data == null) {
                alert("Account registered. Please verify your email to use the site.")
            }
            else {
                var errorStr = "Errors:"
                $.each(data, function (index, error) {
                    errorStr += error.code + ";"
                });
                alert(errorStr);
            }
            
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
   
}


function LoginUser() {


    var email = $("#EmailForLogIn").val();
    var password = $("#PasswordForLogIn").val();

    $.ajax({
        async: true,
        url: "/api/ApiAuthorization/LoginUser",
        method: "POST",
        data: JSON.stringify({
            "Email": email,
            "Password": password
        }),
        headers: {
            "content-type": "application/json; odata=verbose"
        },
        success: function (data) {
            if (data == null) {
                alert("Account login. Reset page.")
                closeLoginForm();
            }
            else {
                alert(data);
                
            }
           
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}

function LogoutUser() {

$.ajax({
        async: true,
        url: "/api/ApiAuthorization/LogoutUser",
        method: "GET",
        headers: {
            "content-type": "application/json; odata=verbose"
        },
        success: function (data) {
            alert("Account logout. Reset page.")
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}


function openRegForm() {
    document.getElementById("regForm").style.display = "block";
}

function closeRegForm() {
    document.getElementById("regForm").style.display = "none";
}

function openLoginForm() {
    document.getElementById("loginForm").style.display = "block";
}

function closeLoginForm() {
    document.getElementById("loginForm").style.display = "none";
}