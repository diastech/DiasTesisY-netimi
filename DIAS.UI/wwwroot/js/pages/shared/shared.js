var Shared = (function () {

    function init() {
    }

    //duracak
    function sbTicketUserDiplayExpr(item) {
        if (!item)
            return "";
        return item.FirstName + " " + item.LastName;
    }

    //duracak
    function sbAsgGroupSelectionChanged(data) {
        //TODO Önce sorumlu kişi seçmişse seçtiklerini temizle sonra filtrele
        //Kontroller
        //$("#sbAsgGroupEmp").dxSelectBox("instance").option("value", null);
        if (data.selectedItem) {
            //$("#sbAsgGroupEmp").dxSelectBox("instance").getDataSource().filter(["asgGroupId", "=", data.selectedItem.id]);
            //$("#sbAsgGroupEmp").dxSelectBox("instance").getDataSource().reload();
            ticketAsgGroupEmpViewDataSource(data.selectedItem.id);
        }
        else {
            //$("#sbAsgGroupEmp").dxSelectBox("instance").getDataSource().filter(null);
            //$("#sbAsgGroupEmp").dxSelectBox("instance").getDataSource().reload();
            ticketAsgGroupEmpDataSource();
        }
    }

    function sbPriorityItemTemplate(item) {
        if (!item)
            return "";
        if (item.ID == 1)
            return "<div style='background-color:red; font-weight: bold;' >" + item.Text + "</div>";
        if (item.ID == 2)
            return "<div style='background-color:yellow; font-weight: bold;' >" + item.Text + "</div>";
        return "<div  >" + item.Text + "</div>";
    }

    return {
        init: init,
        sbTicketUserDiplayExpr: sbTicketUserDiplayExpr,
        sbAsgGroupSelectionChanged: sbAsgGroupSelectionChanged,
        sbPriorityItemTemplate: sbPriorityItemTemplate
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Shared.init();
});
