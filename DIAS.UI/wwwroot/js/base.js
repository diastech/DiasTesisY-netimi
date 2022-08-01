var Base = (function () {
    var userToken, webApiUrl, webApiVUrl;
    var startDate, endDate;
    var requestJSON, responseJSON;

    function init() {
        userToken = sessionStorage.getItem("token");
        webApiUrl = sessionStorage.getItem("webApiUrl");
        webApiVUrl = sessionStorage.getItem("webApiVUrl");
    }

    function log(screenCode, url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, user, method, status) {
        var a = screenCode + url + javaScriptName;
    }

    function getCookie(cname) {
        let name = cname + "=";
        let ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    function mainWindow(_window) {
        if (_window.location.pathname == "/")
            return _window;
        if (_window.location.pathname == _window.parent.location.pathname)
            return _window;
        else
            return mainWindow(_window.parent);
    };

    function showMessage (text, type, title) {
        if (window.location.pathname != window.parent.location.pathname) {
            return mainWindow(window)["Base"].showMessage(text, type, title);
        }
        var html = "";
        if (text.indexOf("<!DOCTYPE html>") > -1) {
            html += "<div style='overflow: auto; max-height: 400px; width:auto; min-width:300px; max-width:500px;' > ";
            var he = document.createElement("html");
            he.innerHTML = text;
            $(he).find("script").each(function (indx, element) {
                $(element).remove();
            });
            html += he.outerHTML;
            html += "</div>";
        }
        else {
            html += "<div class='input-group-prepend'><span class='input-group-prepend'><i class='dx-icon-";
            if (type == messageType.Error)
                html += "clear";
            else if (type == messageType.Success)
                html += "check";
            else if (type == messageType.Info)
                html += "info";
            else if (type == messageType.Confirm)
                html += "help";
            else
                html += "warning";
            html += "' style = 'font-size:300%; color: ";
            if (type == messageType.Error)
                html += "red";
            else if (type == messageType.Success)
                html += "springgreen";
            else if (type == messageType.Info)
                html += "blue";
            else if (type == messageType.Confirm)
                html += "black";
            else
                html += "orange";
            html += ";'></i></span><div style='overflow: auto; max-height: 400px; width:auto; min-width:300px; max-width:500px; padding-left:10px; white-space:pre-wrap;'>";
            html += text;
            html += "</div></div>";
        }
        var resultValue = $.Deferred();
        if (type == messageType.Confirm) {
            DevExpress.ui.dialog.confirm(html, title)
                .done(function (dialogResult) {
                    resultValue.resolve(dialogResult);
                });
            return resultValue.promise();
        }
        else {
            DevExpress.ui.dialog.alert(html, title)
                .done(function (dialogResult) {
                    resultValue.resolve(dialogResult);
                });
            return resultValue.promise();
        }
    }

    function showToast(messageContent, messageType) {
        var top = 0;
        var lastOffset = null;
        var allObjects = $(".dx-toast-content");
        if (allObjects.length > 0) {
            lastOffset = allObjects.last().offset();
        }
        if (lastOffset !== null) {
            top = lastOffset.top;
        }
        if (top <= 0) {
            top = 30;
        }
        else {
            top = window.innerHeight - top;
            top += 30;
        }
        window.DevExpress.ui.notify({
            message: messageContent,
            type: messageType,
            displayTime: 3000,
            width: "300",
            closeOnClick: true,
            position: {
                my: "right",
                at: "bottom right",
                of: null,
                offset: "-1 -" + top
            },
            animation: {
                show: {
                    type: "slide",
                    to: {
                        opacity: 1
                    },
                    duration: 0
                }
                ,
                hide: {
                    type: "slide",
                    to: {
                        opacity: 0,
                        left: -1000
                    },
                    duration: 1000
                }
            }
        });
            //DevExpress.ui.notify({
            //    message: "The task was not saved. Please check if all fields are valid.",
            //    type: "error",
            //    displayTime: 3000,
            //    closeOnClick: true,
            //    //position: { my: 'right', at: 'right', offset: '0 0' },
            //    position: {
            //        my: "right",
            //        at: "top right",
            //        of: window,
            //        offset: "-10 100"
            //    },
            //    width: 300,
            //    animation: {
            //        show: {
            //            type: "slide",
            //            from: {
            //                left: $(window).width()
            //            },
            //            to: {
            //                opacity: 1
            //            },
            //            duration: 1000
            //        }
            //        ,
            //        hide: {
            //            type: "slide",
            //            to: {
            //                opacity: 0,
            //                left: -1000
            //            },
            //            duration: 1000
            //        }
            //    }
            //});
    }

    function messageShowResponseHeader (responseHeader) {
        var title = "";
        if (responseHeader.status >= 500)
            title = responseHeader.status + "-" + responseHeader.statusText;
        else
            title = responseHeader.status + "-" + responseHeader.statusText;
        if (!responseHeader.responseText)
            responseHeader.responseText = responseHeader.statusText;
        if (responseHeader.responseJSON) {
            responseHeader.responseText = "";
            for (var i in responseHeader.responseJSON) {
                responseHeader.responseText += i + "= " + responseHeader.responseJSON[i] + "\n";
            }
        }
        if (responseHeader.status >= 100 && responseHeader.status < 200)
            showMessage(responseHeader.responseText, messageType.Info, title);
        else if (responseHeader.status >= 200 && responseHeader.status < 300)
            showMessage(responseHeader.responseText, messageType.Success, title);
        else
            showMessage(responseHeader.responseText, messageType.Error, title);
    }

    function get(url, javaScriptName, functionName, data, async) {
        try {
            startDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
            if (async === void 0) { async = true; }
            var ajaxHeader = {};
            ajaxHeader["Content-Type"] = "application/json";
            if (url.indexOf(webApiUrl) > -1)
                ajaxHeader["Authorization"] = userToken;
            ajaxHeader["FormName"] = javaScriptName;
            ajaxHeader["ControlName"] = functionName;
            if (data) {
                requestJSON = JSON.stringify(data);
            }
            return jQuery.ajax({
                async: async,
                headers: ajaxHeader,
                type: 'GET',
                url: url,
                // data: data ? JSON.stringify(data) : null,
                data: data ? data : null,
                beforeSend: function () {
                },
                error: function (err) {
                    messageShowResponseHeader(err);
                    endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
                    log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Get", "Error");
                },
                success: function (data) {
                    responseJSON = JSON.stringify(data);
                    endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
                    log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Get", "Success");
                }
            });
        }
        catch (err) {
            Base.showMessage(err, messageType.Error, "Base.get");
            endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
            log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Get", "Error");
        }
        finally { }
    }

    function post(url, javaScriptName, functionName, data, async) {
        try {
            startDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
            if (async === void 0) { async = true; }
            var ajaxHeader = {};
            ajaxHeader["Content-Type"] = "application/json";
            if (url.indexOf(webApiUrl) > -1)
                ajaxHeader["Authorization"] = userToken;
            ajaxHeader["FormName"] = javaScriptName;
            ajaxHeader["ControlName"] = functionName;
            requestJSON = JSON.stringify(data.formData);
            return jQuery.ajax({
                async: async,
                headers: ajaxHeader,
                type: 'POST',
                url: url,
                data: data.formData ? JSON.stringify(data.formData) : null,
                beforeSend: function () {
                },
                error: function (err) {
                    messageShowResponseHeader(err);
                    endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
                    log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Post", "Error");
                },
                success: function (data) {
                    responseJSON = JSON.stringify(data);
                    endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
                    log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Post", "Success");
                }
            });
        }
        catch (err) {
            Base.showMessage(err, messageType.Error, "Base.post");
            endDate = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
            log("1001", url, javaScriptName, functionName, requestJSON, responseJSON, startDate, endDate, 1, "Post", "Success");
        }
        finally { }
    }

    function formatPhoneNumber (data) {
        if (typeof (data) === "object") {
            data = data.value;
        }
        //return data.replace(/(\d{3})(\d{3})(\d{4})/, "+90($1)$2-$3");
        return data.replace(/(\d{3})(\d{3})(\d{4})/, "($1)$2-$3");
    };

    return {
        init: init,
        log: log,
        showToast: showToast,
        showMessage: showMessage,
        get: get,
        post: post,
        getCookie: getCookie,
        formatPhoneNumber: formatPhoneNumber
    };
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Base.init();
});

var messageType;
(function (messageType) {
    messageType["Alert"] = "alert";
    messageType["Error"] = "error";
    messageType["Info"] = "info";
    messageType["Success"] = "success";
    messageType["Confirm"] = "confirm";
})(messageType || (messageType = {}));


//"use strict";
//window.Base = window.Base || {};

//Base.exportToExcel = function () {
//    ($("#grid").length
//        ? $("#grid").dxDataGrid("instance")
//        : $("#revenue-analysis").dxPivotGrid("instance")
//    ).exportToExcel();
//};

//Base.formatShortDate = function (date) {
//    return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear().toString().substring(2);
//};

//Base.screenByWidth = function () {
//    return "lg";
//};

//Base.showDBNotice = function () {
//    var popup = $("#disable-db-notice").dxPopup("instance");

//    popup.option("visible", !popup.option("visible"));
//};

//Base.isFormValid = function (selector) {
//    var form = $(selector).dxForm("instance");
//    return form.validate().isValid;
//};