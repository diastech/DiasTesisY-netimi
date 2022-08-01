var FastTicketsRedirect = (function () {

    let webApiVUrl, webApiUrl;
    let userId;
    let gridFastTicket;
    let formDataAttachment;
    let formDataFiles;
    let currentFastTicket;
    let selectedAttachments;

    function init() {
        webApiVUrl = sessionStorage.getItem("webApiVUrl");
        webApiUrl = sessionStorage.getItem("webApiUrl");
        userId = sessionStorage.getItem("userId");

        gridFastTicketRedirect = $("#gridFastTicketRedirect").dxDataGrid("instance");
        //grd_FastTicketRedirectDataSource();
    }

    function load() {
    }

    function grd_FastTicketRedirectDataSource() {
    }

    function grd_selectionChanged(data) {
        let rowData = data.selectedRowsData[0];
        if (!rowData)
            return;
        currentFastTicket = rowData;
    }

    function accordionSelectionChanged(e) {
        if (e.component.option("selectedIndex") == 0)
            return;
    }

    function grd_btnFastTicketRedirectClick(e) {
        gridFastTicketRedirect.selectRowsByIndexes([e.row.rowIndex]);
        if (!currentFastTicket) return;
        let accordion = $("#accordion-container").dxAccordion("instance");
        accordion.collapseItem(1);
        accordion.expandItem(0);

        $("#sbTicketUser").dxSelectBox("instance").option("disabled", true);
        $("#sbTicketUser").dxSelectBox("instance").option("value", currentFastTicket.insertUserId);
        $("#txtCellNumber").dxTextBox("instance").option("value", currentFastTicket.cellNumber);
        $("#txtDescription").dxTextArea("instance").option("value", currentFastTicket.description);
        resetTicketForm();

        let fastTicketAttachments = $("#listAttachments").dxList("instance");
        Base.get(webApiVUrl + "Attachments/GetAllTicketAttachmentsByBasicTicketId?id=" + currentFastTicket.id, "FastTicketsRedirect", "grd_btnFastTicketRedirectClick", {})
            .done(function (result) {
                try {
                    if (result.Data.length > 0) {
                        fastTicketAttachments.option("dataSource", result.Data);
                    }
                    else {
                        fastTicketAttachments.option("dataSource", []);
                    }
                }
                catch (err) {
                    Base.showToast("Veri alma sırasında hata oluştu.!", "error");
                }
            });
        gridTicketDataSource();
    }

    function saveTicket() {
        // if (!currentFastTicket) return;
        let form = $("#ticketForm").dxForm("instance");
        let formValidationResult = form.validate();
        if (!formValidationResult.isValid) {
            Base.showToast("Lütfen tüm alanların dolu olduğundan emin olunuz.", "error");
            return;
        }

        let formData = {};
        formData.reasonId = parseInt($("#dropDownReasonSingle").dxDropDownBox("instance").option("value"));
        formData.locationId = null;
        let locations = $("#dropDownLocation").dxDropDownBox("instance").option("value");
        formData.priority = $("#sbTicketPriority").dxSelectBox("instance").option("value");
        //formData.ticketOwner = $("#sbTicketOwner").dxSelectBox("instance").option("value");
        formData.responsibleAssignmentGroupId = $("#sbAsgGroup").dxSelectBox("instance").option("value");
        formData.responsibleUserId = parseInt($("#sbAsgGroupEmp").dxSelectBox("instance").option("value") ? $("#sbAsgGroupEmp").dxSelectBox("instance").option("value") : null);
        formData.ticketOpenedTime = moment($("#dateTicket").dxDateBox("instance").option("value")).format();
        formData.ticketDescription = $("#textAreaDescription").dxTextArea("instance").option("value");

        //Create
        formData.ticketStatus = 1;
        formData.ticketInsertUser = userId;
        if (!currentFastTicket) {
            let result = Base.showMessage("Herhangi bir hızlı iş emrine bağlı olmadan iş emri kaydetmek üzeresiniz. Devam etmek istediğinize emin misiniz ?", messageType.Confirm, "Uyarı.!");
            result.done(function (dialogResult) {
                if (dialogResult) {
                    selectedAttachments = null;
                    formData.basicTicketsId = null;
                }
                else
                    return;
            });
        }
        else
            formData.basicTicketsId = currentFastTicket.id;
        Base.post(webApiVUrl + "Ticket/AddTicket", "FastTicketsRedirect", "saveTicket", { formData })
            .done(function (result) {
                try {
                    ticketLocationAdd(result.Data.id, locations);
                    updateTicketAttachments(result.Data.id);
                    gridTicketDataSource();
                    resetTicketForm();
                    $("#listAttachments").dxList("instance").unselectAll();
                    Base.showToast("Kayıt başarı ile eklendi.!", "success");
                }
                catch (err) {
                    Base.showToast("Kayıt sırasında hata oluştu.!", "error");
                }
            });
        grd_FastTicketRedirectDataSource();
    }

    function ticketLocationAdd(ticketId, locations) {
        let formData = {};
        formData.ticketId = ticketId;
        formData.ticketRelatedLocations = locations;
        Base.post(webApiVUrl + "TicketRelatedLocation/AddTicketRelatedLocations", "FastTicketsRedirect", "ticketLocationAdd", { formData })
            .done(function (result) {
                try {
                }
                catch (err) {
                    Base.showToast("Kayıt sırasında hata oluştu.!", "error");
                }
            });
    }

    function updateTicketAttachments(ticketId) {
        if (!selectedAttachments) return;
        let attachments = selectedAttachments.split(";");
        //$("#listAttachments").dxList("instance").getDataSource().filter(["id", "!=", attachments[0]]);
        //var a  = $("#listAttachments").dxList("instance").getDataSource().items();
        $.each(attachments, function (index, value) {
            let formData = { ticketId: ticketId, id: value };
            Base.post(webApiVUrl + "Attachments/UpdateAttachments", "FastTicketsRedirect", "updateTicketAttachments", { formData })
                .done(function (result) {
                    try {
                        //$("#listAttachments").dxList("instance").getDataSource().items().filter(item => item.id != value);
                        $("#listAttachments").dxList("instance").option("dataSource", $("#listAttachments").dxList("instance").getDataSource().items().filter(item => item.id != value));
                    }
                    catch (err) {
                        Base.showToast("Kayıt sırasında hata oluştu.!", "error");
                    }
                });
        });
    }

    function resetTicketForm() {
        let form = $("#ticketForm").dxForm("instance");
        form.resetValues();
        $("#dateTicket").dxDateBox("instance").option("value", moment().format());
    }

    function gridTicketDataSource() {
        if (!currentFastTicket) return;
        let gridTicket = $("#gridTicket").dxDataGrid("instance");
        Base.get(webApiVUrl + "Ticket/GetAllTicketsVwByBasicTicketId?id=" + currentFastTicket.id, "FastTicketsRedirect", "gridTicketDataSource", {})
            .done(function (result) {
                try {
                    if (result.Data.length > 0) {
                        gridTicket.repaint();
                        gridTicket.option("dataSource", result.Data);
                    }
                    else {
                        gridTicket.repaint();
                        gridTicket.option("dataSource", []);
                    }
                }
                catch (err) {
                    Base.showToast("Veri alma sırasında hata oluştu.!", "error");
                }
            });

    }

    function fastTicketAttachmentsTemplate(data, index) {
        if (!data.folder)
            return;
        var result = $("<div>");
        $('<a/>').addClass('dx-link')
            .text(data.folder)
            .on('dxclick', function () {
                $.ajax({
                    type: 'GET',
                    url: `${webApiUrl}TicketFile/GetFile?ticketId=0&fileName=${data.folder}`,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', sessionStorage.getItem("token"));
                    },
                    success: function (data) {
                        window.location = `${webApiUrl}TicketFile/GetFile?ticketId=0&fileName=${data.folder}`;
                    }
                });
            })
            .appendTo(result);
        return result;
    }

    function listOnSelectionChanged(e) {
        selectedAttachments = e.component.option("selectedItemKeys").join(";");
    }

    function cellTemplate_CurrentStatus(container, options) {
        if (options.data.ticketStatus == 1)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/01_acik.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 2)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/02_atandi.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 3)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/03_calisiliyor.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 4)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/04_beklet.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 5)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/05_reddet.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 6)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/06_cozumlendi.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 7)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/07_kapali.png" }))
                .appendTo(container);
        else if (options.data.ticketStatus == 8)
            $("<div>")
                .append($("<img>", { "src": "../../images/general/08_yenidenacildi.png" }))
                .appendTo(container);
        else
            $("<div>")
                .append($("<img>", {}))
                .appendTo(container);
    }

    return {
        //Init, Load
        load: load,
        init: init,

        grd_selectionChanged: grd_selectionChanged,
        listOnSelectionChanged: listOnSelectionChanged,
        accordionSelectionChanged: accordionSelectionChanged,
        grd_btnFastTicketRedirectClick: grd_btnFastTicketRedirectClick,
        fastTicketAttachmentsTemplate: fastTicketAttachmentsTemplate,
        cellTemplate_CurrentStatus: cellTemplate_CurrentStatus,
        saveTicket: saveTicket
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    FastTicketsRedirect.init();
});
