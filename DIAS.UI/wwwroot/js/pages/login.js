window.Login = {

    init: function () {
        let username = Base.getCookie("username");
        let password = Base.getCookie("password");
        let rememberMe = Base.getCookie("rememberMe");

        if (rememberMe) {
            $("#txtUsername").dxTextBox("instance").option("value", username);
            $("#txtPassword").dxTextBox("instance").option("value", password);
            $("#chkRememberMe").dxCheckBox("instance").option("value", rememberMe);
        }
    },

    changePasswordMode: function (e) {
        try {
            let tb = $("#txtPassword").dxTextBox("instance");
            let mode = tb.option("mode");
            if (mode == "password")
                tb.option("mode", "text");
            else
                tb.option("mode", "password");
        } catch (err) {
            Base.showMessage("changePasswordMode : " + err, messageType.Error, "Login.changePasswordMode");
        }
    },

    onChkRememberMeValueChanged: function (e) {
    },

    btnLoginClick: function (e) {
        try {
            let result = e.validationGroup.validate();
            if (result.isValid) {
                Base.showToast("Kullanıcı girişi başarılı", "success");
                var chkRememberMe = $("#chkRememberMe").dxCheckBox("instance");
                if (chkRememberMe.option("value")) {
                    var password = $("#txtPassword").dxTextBox("instance");
                    var username = $("#txtUsername").dxTextBox("instance");
                    document.cookie = "rememberMe=true; expires=Thu, 01 Jan 2030 00:00:00 UTC;";
                    document.cookie = "username=" + username.option("value") + ";expires=Thu, 01 Jan 2030 00:00:00 UTC;";
                    document.cookie = "password=" + password.option("value") + ";expires=Thu, 01 Jan 2030 00:00:00 UTC;";
                }
                else {
                    document.cookie = "rememberMe=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
                    document.cookie = "username=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
                    document.cookie = "password=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
                }
            }
            else {
                Base.showToast("Lütfen tüm alanların dolu olduğundan emin olunuz.!", "error");
            }
        } catch (err) {
            Base.showMessage("btnLoginClick : " + err, messageType.Error, "Login.btnLoginClick");
        }
        // Check browser support
        //if (typeof (Storage) !== "undefined") {
        //    sessionStorage.setItem("lastname", "Smith");
        //    console.log(sessionStorage.getItem("lastname"));
        //}
        //else {
        //    console.log("Sorry, your browser does not support Web Storage...");
        //}

        //var form = $("#frmLogin").dxForm("instance");
        //var formData = form.option("formData");
        //$.ajax({
        //    url: 'https://localhost:44327/api/auth/login',
        //    type: 'post',
        //    dataType: 'json',
        //    contentType: 'application/json',
        //    data: JSON.stringify(formData),
        //    //data: '{"username": "user1", "password" : "A.123456d*"}',
        //    success: function (data, textStatus, xhr) {
        //        console.log(data.token);
        //        console.log("Token claim successfull.!")
        //        sessionStorage.setItem("token", "Bearer " + data.token);

        //        var type = "success",
        //            text = "Login successfull";
        //        DevExpress.ui.notify(text, type, 2000);
        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        console.log("Error in Operation.!")
        //        var type = "error",
        //            text = "Error.!";
        //        DevExpress.ui.notify(text, type, 2000);

        //        //DevExpress.ui.notify({
        //        //    message: "Error.!",
        //        //    type: "error",
        //        //    displayTime: 3000,
        //        //    closeOnClick: true,
        //        //    position: {
        //        //        my: 'bottom center', at: 'bottom right' }
        //        //});
        //    }
        //});
    },
};
