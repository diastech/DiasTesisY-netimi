var Reason = (function () {

    function init() {
    }

    function syncTreeViewSelection(treeView, value) {
        if (!value) {
            treeView.unselectAll();
        } else {
            treeView.selectItem(value);
        }
    }

    function treeBoxMulti_valueChanged(e) {
        if (e.component.content() == null)
            return;
        var $treeView = e.component.content().find(".dx-treeview");
        if ($treeView.length) {
            syncTreeViewSelection($treeView.dxTreeView("instance"), e.value);
        }
    }

    function treeBox_valueChanged(e) {
        if (e.component.content() == null)
            return;
        var $treeView = e.component.content().find(".dx-treeview");
        if ($treeView.length) {
            syncTreeViewSelection($treeView.dxTreeView("instance"), e.value);
        }
    }

    return {
        init: init,
        syncTreeViewSelection: syncTreeViewSelection,
        treeBox_valueChanged: treeBox_valueChanged,
        treeBoxMulti_valueChanged: treeBoxMulti_valueChanged,
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Reason.init();
});
