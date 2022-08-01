var FastTickets = (function () {

    let webApiVUrl, webApiUrl;
    let userId;
    let gridFastTicket;
    let formDataAttachment;
    let formDataFiles;

    function init() {
        webApiVUrl = sessionStorage.getItem("webApiVUrl");
        webApiUrl = sessionStorage.getItem("webApiUrl");
        userId = sessionStorage.getItem("userId");

        gridFastTicket = $("#gridFastTicket").dxDataGrid("instance");
        //grd_FastTicketDataSource();
    }

    function load() {
    }

    function grd_FastTicketDataSource() {
        Base.get(sessionStorage.getItem("webApiVUrl") + "BasicTicket/GetAllBasicTicketsVwByUserId?id=" + userId, "FastTickets", "grd_FastTicketDataSource", {})
            .done(function (result) {
                try {
                    if (result.Data.length > 0) {
                        gridFastTicket.repaint();
                        gridFastTicket.option("dataSource", result.Data);
                    }
                    else {
                        gridFastTicket.repaint();
                        gridFastTicket.option("dataSource", []);
                    }
                }
                catch (err) {
                    Base.showToast("Veri alma sırasında hata oluştu.!", "error");
                }
            });
    }

    function saveFastTicket() {
        console.log("sadik");
        let accordion = $("#accordion-container").dxAccordion("instance");
        let form = $("#frmFastTicket").dxForm("instance");
        let formValidationResult = form.validate();
        if (!formValidationResult.isValid) {
            Base.showToast("Lütfen tüm alanların dolu olduğundan emin olunuz.", "error");
            return;
        }

        let formData = form.option("formData");
        formData.insertUser = parseInt($("#sbTicketUser").dxSelectBox("instance") ? $("#sbTicketUser").dxSelectBox("instance").option("value") : userId),
        Base.post(webApiVUrl + "BasicTicket/AddBasicTicket", "FastTickets", "saveFastTicket", { formData })
            .done(function (result) {
                try {
                    formDataFiles.append("basicTicketId", result.Data.id);
                    saveBasicTicketAttachments(formDataFiles);
                    $("#file-uploader").dxFileUploader("instance").reset();
                    form.resetValues();
                    Base.showToast("Kayıt başarı ile eklendi.!", "success");
                    accordion.collapseItem(0);
                    accordion.expandItem(1);
                    grd_FastTicketDataSource();
                }
                catch (err) {
                    Base.showToast("Kayıt sırasında hata oluştu.!", "error");
                }
            });
    }

    function fileUploader_valueChanged(e) {
        let files = e.value;
        formDataFiles = new FormData();
        for (let i = 0; i != files.length; i++) {
            formDataFiles.append("myFile", files[i]);
        }
    }

    function saveBasicTicketAttachments(formData) {

        $.ajax({
            url: webApiUrl + "TicketFile/AddBasicTicketAttachment",
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', sessionStorage.getItem("token"));
            },
            success: function (data, textStatus, xhr) {
                //Dont do anything
            },
            error: function (xhr, textStatus, errorThrown) {
                Base.showToast("Kayıt sırasında hata oluştu.!", "error");
            }
        });
    }

    function fastTicketGridCellTemplate(container, options) {
        if (!options.data.ticketAttachments)
            return;
        let myArr = options.data.ticketAttachments.split(";");
        myArr.forEach((file, index, array) => {
            $('<a/>').addClass('dx-link')
                .text(file)
                .on('dxclick', function () {
                    $.ajax({
                        type: 'GET',
                        url: `${webApiUrl}TicketFile/GetFile?ticketId=0&fileName=${file}`,
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Authorization', sessionStorage.getItem("token"));
                        },
                        success: function (data) {
                            window.location = `${webApiUrl}TicketFile/GetFile?ticketId=0&fileName=${file}`;
                        }
                    });
                })
                .append("<br />")
                .appendTo(container)
        });
    }

    function accordionSelectionChanged(e) {
        //TODO Check phonenumber aswell
        if (e.component.option("selectedIndex") == 0)
            if ($("#sbTicketUser").dxSelectBox("instance"))
                if (!$("#sbTicketUser").dxSelectBox("instance").option("value"))
                    $("#sbTicketUser").dxSelectBox("instance").option("value", userId)
    }

    return {
        //Init, Load
        load: load,
        init: init,

        saveFastTicket: saveFastTicket,
        fileUploader_valueChanged: fileUploader_valueChanged,
        fastTicketGridCellTemplate: fastTicketGridCellTemplate,
        accordionSelectionChanged: accordionSelectionChanged
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    FastTickets.init();
});
