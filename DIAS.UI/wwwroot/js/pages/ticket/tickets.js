var Tickets = (function () {
    let countDownGridRefreshDate;

    function load() {}

    function init() {
        refreshTimer(0);
    }

    //!!!IMPORTANT Grid üzerinde seçim değiştiğinde global olarak kullanılan currentTicket nesnesini tekrar set eder
    function grd_selectionChanged(data) {
        let rowData = data.selectedRowsData[0];
        if (!rowData)
            return;
        currentTicket = rowData; //Seçili satırdaki datayı al
    }

    //Sayfada bulunan refresh butonu her 5 dkda bir mevcut seçilmiş filtreye göre sayfayı yeniler
    function refreshTimer(count) {
        countDownGridRefreshDate = moment().add(5, 'm').toDate();
        let countDown = setInterval(function () {
            let now = new Date().getTime();
            let distance = countDownGridRefreshDate - now;
            if (distance < 0) {
                document.getElementById("timer").innerHTML = `Yenileniyor..`;
                /*btn_ticketFindClick();*/
                refreshTimer(0);
            }
            let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            let seconds = Math.floor((distance % (1000 * 60)) / 1000);
            let zero = "";
            if (seconds < 10)
                zero = "0";
            document.getElementById("timer").innerHTML = `Yenile: (0${minutes}:${zero}${seconds})`;
        }, 1000);
    }

    //Refresh button click
    function btn_refreshGridClick() {
        btn_ticketFindClick();
    }

    function grd_onRowPrepared(e) {        
        if (e.rowType !== "data")
            return;
        if (e.data.TicketPriority == 1) {
            e.rowElement.find("td").css('background', 'red'); //Satırı boya
            //e.rowElement.css("color", "red");    //Yazıyı boya
            e.rowElement.css("color", "black");
            e.rowElement.css("font-weight", "bold");
        }
        if (e.data.TicketPriority == 2) {
            e.rowElement.find("td").css('background', 'yellow');
            //e.rowElement.css("color", "yellow");
            e.rowElement.css("color", "black");
            e.rowElement.css("font-weight", "bold");
        }
    }

    function cellTemplate_TimeRemaining(container, options) {
        let countDownDate = new Date(moment(options.data.expectedResponseTime)).getTime();
        let timeRemaining = setInterval(function () {
            if (options.data.ticketStatus == 6 || options.data.ticketStatus == 7) {
                clearInterval(timeRemaining);
                if (document.getElementById("clock" + options.data.id)) {
                    document.getElementById("clock" + options.data.id).innerHTML = moment().format('DD/MM/YYYY HH:mm');
                    return;
                }
                else
                    clearInterval(timeRemaining);
            }
            let now = new Date().getTime();
            if (options.data.ticketOpenedTime > moment().format()) {
                if (document.getElementById("clock" + options.data.id)) {
                    document.getElementById("clock" + options.data.id).innerHTML = `Planlı<br>(${moment(options.data.ticketOpenedTime).format('DD/MM/YYYY HH:mm')})`;
                    return;
                }
                else
                    clearInterval(timeRemaining);
            }
            let distance = countDownDate - now;
            if (distance <= 0) {
                clearInterval(timeRemaining);
                if (document.getElementById("clock" + options.data.id))
                    document.getElementById("clock" + options.data.id).innerHTML = `Tamamlandı.`;
                return;
            }
            let days = Math.floor(distance / (1000 * 60 * 60 * 24));
            let d = "g";
            let hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            let h = "sa";
            let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            let m = "dk";
            let seconds = Math.floor((distance % (1000 * 60)) / 1000);
            let s = "sn";
            let zero = "";
            if (days == 0) {
                days = "";
                d = "";
            }
            if (hours == 0) {
                hours = "";
                h = "";
            }
            if (minutes == 0) {
                minutes = "";
                m = "";
            }
            if (seconds < 10) {
                zero = "0";
            }
            if (document.getElementById("clock" + options.data.id))
                document.getElementById("clock" + options.data.id).innerHTML = `${days}${d} ${hours}${h} ${minutes}${m} ${zero}${seconds}${s}`;
            else
                clearInterval(timeRemaining);
        }, 1000);
        $("<div id= 'clock" + options.data.id + "''>")
            .append()
            .appendTo(container);
    }

    function cellTemplate_CurrentStatus(container, options) {




        if (options.data.TicketStatusId == 1)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/01_acik.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 2)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/02_atandi.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 3)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/03_calisiliyor.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 4)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/04_beklet.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 5)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/05_reddet.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 6)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/06_cozumlendi.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 7)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/07_kapali.svg" }))
                .appendTo(container);
        else if (options.data.TicketStatusId == 8)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/01_acik.png" }))
                .appendTo(container);
        else
            $("<div>")
                .append($("<img>", {}))
                .appendTo(container);
    }


    function btnStateTemplate(itemData)
    {
        //TODO Get from db later
        let sourceImg;
        if (itemData.Id == 1)
            sourceImg = `../../images/general/01_acik.svg`;
        if (itemData.Id == 2)
            sourceImg = `../../images/general/02_atandi.svg`;
        if (itemData.Id == 3)
            sourceImg = `../../images/general/03_calisiliyor.svg`;
        if (itemData.Id == 4)
            sourceImg = `../../images/general/04_beklet.svg`;
        if (itemData.Id == 5)
            sourceImg = `../../images/general/05_reddet.svg`;
        if (itemData.Id == 6)
            sourceImg = `../../images/general/06_cozumlendi.svg`;
        if (itemData.Id == 7)
            sourceImg = `../../images/general/07_kapali.svg`;
        if (itemData.Id == 8)
            sourceImg = `../../images/general/01_acik.svg`;

        return `<div> <img style='float:left' src='${sourceImg}' /> <div style='float:left'>${itemData.StateDescription}</div> </div>`;
    }


    //Grid Ticket filter
    async function btn_ticketFindClick(e) {

    }

    return {
        //Init, Load
        load: load,
        init: init,

        //Buttons Func.
        btn_ticketFindClick: btn_ticketFindClick,
        btn_refreshGridClick: btn_refreshGridClick,

        //Grid Func.
        grd_selectionChanged: grd_selectionChanged,
        grd_onRowPrepared: grd_onRowPrepared,
        cellTemplate_TimeRemaining: cellTemplate_TimeRemaining,
        cellTemplate_CurrentStatus: cellTemplate_CurrentStatus,
        btnStateTemplate: btnStateTemplate,
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Tickets.init();
});