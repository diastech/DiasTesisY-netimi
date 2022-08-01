var Homepage = (function () {

    var lstDuyurular = null;
    var lstMesajlar = null;
    var lstIsListesi = null;

    function init() {

    }

    function homepageLoad () {
        lstDuyurular = $("#lstDuyurular").dxList("instance");
        //vBase.vGet(vSessionStorage.WebApiUrl + "/api/base/DuyuruGetir", "MesajDuyurularOzet", "MesajDuyurularOzetLoad", {
        //    treekey: vSessionStorage.KullaniciKendiKurumList[0].TreeKey,
        //    pkKullanici: vSessionStorage.kullanici.PkKullanici
        //}).done(function (duyurular) {
        //    lstDuyurular.option("dataSource", duyurular.VeriTablosu);
        //});
        lstMesajlar = $("#lstMesajlar").dxList("instance");
        //vBase.vGet(vSessionStorage.WebApiUrl + "/api/base/MesajGetir", "MesajDuyurularOzet", "MesajDuyurularOzetLoad", {
        //    treekey: vSessionStorage.KullaniciKendiKurumList[0].TreeKey,
        //    pkKullanici: vSessionStorage.kullanici.PkKullanici
        //}).done(function (mesajlar) {
        //    lstMesajlar.option("dataSource", mesajlar.VeriTablosu);
        //});
        lstIsListesi = $("#lstIsListesi").dxList("instance");
        //vBase.vGet(vSessionStorage.WebApiUrl + "/api/base/GetJobList", "MesajDuyurularOzet", "MesajDuyurularOzetLoad", {
        //    id: -1,
        //    fk_Ref: -1,
        //    screenCode: "",
        //    dtBas: new Date(2011, 1, 1, 0, 0, 0).toJSON(),
        //    dtBit: new Date().toJSON(),
        //    ilgili_kisi: vSessionStorage.kullanici.PkKullanici
        //}).done(function (mesajlar) {
        //    lstIsListesi.option("dataSource", mesajlar.VeriTablosu);
        //});
    };

    function duyuruItemTemplate (itemData, itemIndex, element) {
        var dv = document.createElement("div");
        dv.className = "list-group";
        dv.id = "duyuruTemplate" + itemIndex;
        if (!itemData.pk_mesajokuma)
            dv.className += " font-weight-bold";
        var dv1 = document.createElement("div");
        dv1.className = "d-flex w-100 justify-content-between";
        var divKonu = document.createElement("div");
        divKonu.innerText = itemData.konu;
        divKonu.style.whiteSpace = "pre-wrap";
        divKonu.style.fontSize = "17px";
        dv1.append(divKonu);
        if (itemData.onem_derecesi != 147) {
            var span = document.createElement("span");
            span.className = "fas fa-exclamation";
            span.style.color = "red";
            span.style.float = "right";
            span.style.fontSize = "20px";
            dv1.append(span);
        }
        dv.append(dv1);
        var p = document.createElement("p");
        p.innerText = itemData.birim_adi;
        p.className = "mb-1 text-truncate";
        dv.append(p);
        var small2 = document.createElement("small");
        var duyuruTarih = new Date(itemData.gonderimtarihi);
        small2.innerText = vBase.vGetLocalizerBaseClientText("duyuruTarih") + ": " + duyuruTarih.toLocaleString();
        ;
        dv.append(small2);
        element.append(dv);
    };
    function duyuruSelectionChanged (e) {
        var arr = e.addedItems;
        if (arr.length == 0)
            return;
        $("#duyuruKonu").text(arr[0].konu);
        $("#duyuruAciklama").text(arr[0].aciklama);
        var btnDuyuruDownload = $("#btnDuyuruDownload").dxButton("instance");
        if (arr[0].belge_adi) {
            btnDuyuruDownload.option("text", arr[0].belge_adi);
            btnDuyuruDownload.option("visible", true);
        }
        else
            btnDuyuruDownload.option("visible", false);
        if (!arr[0].pk_mesajokuma) {
            var id = "duyuruTemplate" + e.component.option("selectedIndex");
            $("#" + id).removeClass("font-weight-bold");
            var mesajOkuma = {};
            mesajOkuma.fk_mesaj = arr[0].pk_mesaj;
            vBase.vPost(vSessionStorage.WebApiUrl + "/api/base/SetGnlMesajokuma", "MesajDuyurularOzet", "duyuruSelectionChanged", mesajOkuma)
                .done(function (data) {
                    arr[0].pk_mesajokuma = data.pk_mesajokuma;
                });
        }
    };

    function OnDuyuruDownload (e) {
        var opt = lstDuyurular.option();
        var item = opt.selectedItem;
        var element = document.createElement('a');
        element.setAttribute('href', "/Base/MesajDuyurularOzet?handler=DuyuruEkDownload&pk_mesaj=" + item.pk_mesaj);
        element.setAttribute('download', item.belge_adi);
        element.click();
    };

    function mesajItemTemplate (itemData, itemIndex, element) {
        var dv = document.createElement("div");
        dv.className = "list-group";
        dv.id = "mesajTemplate" + itemIndex;
        if (itemData.okundu != "E")
            dv.className += " font-weight-bold";
        var dv1 = document.createElement("div");
        dv1.className = "d-flex w-100 justify-content-between";
        var divKonu = document.createElement("div");
        divKonu.innerText = itemData.konu;
        divKonu.style.whiteSpace = "pre-wrap";
        divKonu.style.fontSize = "17px";
        dv1.append(divKonu);
        if (itemData.onem_derecesi != 147) {
            var span = document.createElement("span");
            span.className = "fas fa-exclamation";
            span.style.color = "red";
            span.style.float = "right";
            span.style.fontSize = "20px";
            dv1.append(span);
        }
        dv.append(dv1);
        var p = document.createElement("p");
        p.innerText = itemData.birim_adi;
        p.className = "mb-1 text-truncate";
        dv.append(p);
        var small2 = document.createElement("small");
        var mesajTarih = new Date(itemData.gonderimtarihi);
        small2.innerText = vBase.vGetLocalizerBaseClientText("mesajTarih") + ": " + mesajTarih.toLocaleString();
        ;
        dv.append(small2);
        element.append(dv);
    };
    function mesajSelectionChanged (e) {
        var arr = e.addedItems;
        if (arr.length == 0)
            return;
        $("#mesajKonu").text(arr[0].konu);
        $("#mesajAciklama").text(arr[0].aciklama);
        var btnMesajDownload = $("#btnMesajDownload").dxButton("instance");
        if (arr[0].belge_adi) {
            btnMesajDownload.option("text", arr[0].belge_adi);
            btnMesajDownload.option("visible", true);
        }
        else
            btnMesajDownload.option("visible", false);
        if (!arr[0].okundu) {
            var id = "mesajTemplate" + e.component.option("selectedIndex");
            $("#" + id).removeClass("font-weight-bold");
            var mesajOkuma = {};
            mesajOkuma.pk_mesaj = arr[0].pk_mesaj;
            mesajOkuma.okuyanperid = vSessionStorage.kullanici.PkKullanici;
            vBase.vGet(vSessionStorage.WebApiUrl + "/api/base/MesajOkundu", "MesajDuyurularOzet", "duyuruSelectionChanged", mesajOkuma)
                .done(function (data) {
                    arr[0].okundu = "E";
                });
        }
    };
    function OnMesajDownload (e) {
        var opt = lstMesajlar.option();
        var item = opt.selectedItem;
        var element = document.createElement('a');
        element.setAttribute('href', "/Base/MesajDuyurularOzet?handler=DuyuruEkDownload&pk_mesaj=" + item.pk_mesaj);
        element.setAttribute('download', item.belge_adi);
        element.click();
    };

    function isListesiItemTemplate (itemData, itemIndex, element) {
        var dv = document.createElement("div");
        dv.className = "list-group";
        dv.id = "isListesiTemplate" + itemIndex;
        var dv1 = document.createElement("div");
        dv1.className = "d-flex w-100 justify-content-between";
        var divKonu = document.createElement("div");
        divKonu.innerText = itemData.ekran_aciklama;
        divKonu.style.whiteSpace = "pre-wrap";
        divKonu.style.fontSize = "17px";
        dv1.append(divKonu);
        dv.append(dv1);
        var p = document.createElement("p");
        p.innerText = itemData.ekleyenKullanici_AdSoyad;
        p.className = "mb-1 text-truncate";
        dv.append(p);
        var small2 = document.createElement("small");
        var mesajTarih = new Date(itemData.eklemeTarihi);
        small2.innerText = vBase.vGetLocalizerBaseClientText("mesajTarih") + ": " + mesajTarih.toLocaleString();
        ;
        dv.append(small2);
        element.append(dv);
    };

    function isListesiSelectionChanged (e) {
        var arr = e.addedItems;
        if (arr.length == 0)
            return;
        $("#isListesiEkran").text(arr[0].ekran_aciklama);
        $("#isListesiAciklama").text(arr[0].aciklama);
    };
    return {
        init: init,
        homepageLoad: homepageLoad,
        duyuruItemTemplate: duyuruItemTemplate,
        duyuruSelectionChanged: duyuruSelectionChanged,
        OnDuyuruDownload: OnDuyuruDownload,
        mesajItemTemplate: mesajItemTemplate,
        mesajSelectionChanged: mesajSelectionChanged,
        OnMesajDownload: OnMesajDownload,
        isListesiItemTemplate: isListesiItemTemplate
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Homepage.init();
});
