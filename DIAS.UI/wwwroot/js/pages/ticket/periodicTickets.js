var PeriodicTickets = (function () {

    var webApiVUrl, webApiUrl;
    var userId;
    function init() {
        webApiVUrl = sessionStorage.getItem("webApiVUrl");
        webApiUrl = sessionStorage.getItem("webApiUrl");
        userId = sessionStorage.getItem("userId");

       }

    function load() {
    }

    function rbTicketTypeOnValueChanged(e) {
        if (e.value == 1) {
            $("#daily").show(0);
            $("#weekly").hide(0);
            $("#monthly").hide(0);
            $("#yearly").hide(0);
        }
        if (e.value == 2) {
            $("#daily").hide(0);
            $("#weekly").show(0);
            $("#monthly").hide(0);
            $("#yearly").hide(0);
        }
        if (e.value == 3) {
            $("#daily").hide(0);
            $("#weekly").hide(0);
            $("#monthly").show(0);
            $("#yearly").hide(0);
        }
        if (e.value == 4) {
            $("#daily").hide(0);
            $("#weekly").hide(0);
            $("#monthly").hide(0);
            $("#yearly").show(0);
        }
    }

    function rbDailyOption1(itemData, itemIndex, itemElement) {
        itemElement.append($("<div />").attr("style", "float:left").text("Her "));
        itemElement.append($("<div id='dailyOption1-repeat-count'/>").attr("style", "float:left"));
        itemElement.append($("<div />").text(" günde bir").attr("style", "float:left"));
        $("#dailyOption1-repeat-count").dxNumberBox({
            value: 1,
            min: 1,
            max: 31,
            showClearButton: false,
            showSpinButtons: true,
            width: 70
        })
    }

    function rbDailyOption2(itemData, itemIndex, itemElement) {
        itemElement.append($("<div />").attr("style", "float:left").text("Haftaiçi hergün "));
    }
    return {
        //Init, Load
        load: load,
        init: init,

        rbTicketTypeOnValueChanged: rbTicketTypeOnValueChanged,

        rbDailyOption1: rbDailyOption1,
        rbDailyOption2: rbDailyOption2
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    PeriodicTickets.init();
});
