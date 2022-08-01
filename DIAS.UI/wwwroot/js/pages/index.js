var Index = (function () {

    window.setInterval(function () {
        $('#clock').html(moment().format('dddd, Do MMM YYYY, HH:mm:ss'))
    }, 1000);

    function init() {
    }

    function IndexLoad () {
    }

    function onTreeViewItem_click(e) {
        try {
            $(".dx-treeview-item").removeClass("currently-active-item");
            $(e.itemElement).addClass("currently-active-item");
            var node = e.node;
            //if (e.node.key == 5) {
            //    break;
            //}
            var component = e.component;
            var nodes = component.getNodes();
            for (var n in nodes) {
                var _node = nodes[n];
                if (_node.key != node.key && _node.expanded && node.children.length > 0)
                    component.collapseItem(_node.key);
            }

            if (node.children.length > 0 && !node.expanded) {
                component.expandItem(node.key);
            }
           
            if (node.itemData.url) {
                var tabMain = $("#tabMain").dxTabPanel("instance");
                var tabItems = tabMain.option("items");
                var kayitvar = false;
                for (var i in tabItems) {
                    if (e.node.text == " İş Emri") {
                        tabMain.option("selectedItem", tabItems[0]);
                        kayitvar = true;
                        break;
                    }                  

                    var tabItem = tabItems[i];
                    if (tabItem["key"] == node.key || node.key==2) {
                        tabMain.option("selectedItem", tabItem);
                        kayitvar = true;
                        break;
                    }
                }
                if (!kayitvar) {
                    var item = {};
                    item.title = node.text;
                    item["key"] = node.key;
                    item.template = "<text><iframe style='width: 100%; height:100%; border:none; background-color: white; ' src='" + node.itemData.url + "' id='frame" + node.key + "'></iframe></text>";
                    tabItems.push(item);
                    tabMain.option("items", tabItems);
                    tabMain.option("selectedItem", item);
                }
            }
        } catch (err) {
            Base.showMessage("onTreeViewItem_click : " + err, messageType.Error, "Index.onTreeViewItem_click");
        }
    }

    function titleTemplate (itemData, itemIndex, itemElement) {
        itemElement.append(
            $("<b>")
                .text(` ${itemData.title}  `)
        );
        if (itemData.key == "-1")
            return;
        itemElement.append(
            $("<i style='color:red; padding-left:10px; padding-right:0px;'>")
                .addClass("dx-icon")
                .addClass("dx-icon-close")
                .click(function () { closeButton_click(itemData); })
        );
    }

    //function clockTemplate(itemData, itemIndex, itemElement) {
    //    itemElement.append(
    //        $("<div id='clock'></div>")
    //    );
    //}

    function closeButton_click(itemData) {
        try {
            if (!itemData)
                return;
            var tabMain = $("#tabMain").dxTabPanel("instance");
            var tabPanelItems = tabMain.option("items").slice();
            var index = tabPanelItems.indexOf(itemData);
            tabPanelItems.splice(index, 1);
            if (index >= tabPanelItems.length && index > 0)
                tabMain.option("selectedIndex", index - 1);
            tabMain.option("items", tabPanelItems);
        } catch (err) {
            Base.showMessage("closeButtonHandler : " + err, messageType.Error, "Index.closeButtonHandler");
        }

    }

    function drawerMenu_click(e) {
        try {
            var drawer = $("#drawer").dxDrawer("instance");
            drawer.toggle();
        } catch (err) {
            Base.showMessage("drawerMenu_click : " + err, messageType.Error, "Index.drawerMenu_click");
        }

    }

    function btnLogOut_click(e) {
        try {
            var result = Base.showMessage("Sistemden çıkmak istediğinize emin misiniz ?", messageType.Confirm, "Çıkış yapmak üzeresiniz.!");
            result.done(function (dialogResult) {
                var ajaxHeader = {};
                ajaxHeader["Content-Type"] = "application/json";
                $.ajax({
                    url: "Account/Login?handler=Logout",                    
                    contentType: false,
                    processData: false,
                    headers: ajaxHeader,
                    type: 'post',
                    success: function (response) {
                        if (dialogResult) {
                            Base.showToast("Çıkış başarılı.!", "success");
                            window.location.href = "/Account/Login/?culture=tr";
                        }
                    }
                });

                
            });
        } catch (err) {
            Base.showMessage("btnLogOut_click : " + err, messageType.Error, "Index.btnLogOut_click");
        }
    }

    function btnRefresh_click(e) {
        try {
            window.location.reload();
        } catch (err) {
            Base.showMessage("profileItem_click : " + err, messageType.Error, "Index.profileItem_click");
        }
    }

    function btnChangeFacility_click(e) {
        try {
            Base.showToast("btnChangeFacility_click", "success");
        } catch (err) {
            Base.showMessage("btnChangeFacility_click : " + err, messageType.Error, "Index.btnChangeFacility_click");
        }
    }

    function btnHelpDesk_click(e) {
        try {
            Base.showToast("btnHelpDesk_click", "success");
        } catch (err) {
            Base.showMessage("btnHelpDesk_click : " + err, messageType.Error, "Index.btnHelpDesk_click");
        }
    }

    function btnChangePassword_click(e) {
        try {
            Base.showToast("btnChangePassword_click", "success");
        } catch (err) {
            Base.showMessage("btnChangePassword_click : " + err, messageType.Error, "Index.btnChangePassword_click");
        }
    }

    function btnAboutPage_click(e) {
        try {
            Base.showToast("btnAboutPage_click", "success");
        } catch (err) {
            Base.showMessage("btnAboutPage_click : " + err, messageType.Error, "Index.btnAboutPage_click");
        }
    }

    function profileButton_click(e) {
        try {
            Base.showToast("Go to " + e.component.option("text") + "'s profile", "success");
        } catch (err) {
            Base.showMessage("profileButton_click : " + err, messageType.Error, "Index.profileButton_click");
        }
    }

    function profileItem_click(e) {
        try {
            if (e.itemData.Id == 1 || e.itemData.Id == 2 || e.itemData.Id == 3 || e.itemData.Id == 4) {
                Base.showToast(e.itemData.Text + " clicked", "success");
            }
            if (e.itemData.Id == 5) {
                Base.showToast("Messages", "success");
            }
            if (e.itemData.Id == 6) {
                Base.showToast("Contacts", "success");
            }
            if (e.itemData.Id == 7) {
                logoutDeneme(e);
            }
        } catch (e) {
            Base.showMessage("profileItem_click : " + err, messageType.Error, "Index.profileItem_click");
        }
    }

    function onLanguageSelectBoxFieldTemplate(data, container) {
        var result = $("<div><img id='customImg' src='" +
            (data ? "/icons/" + data.value + ".svg" : '') +
            "' /><div id='divCustomSelect'></div></div>");
        result
            .find("#divCustomSelect")
            .dxTextBox({
                value: data && data.value,
                readOnly: true
            });
        container.append(result);
    };

    function onLanguageSelectBoxItemTemplate(data) {
        return "<div><img id='customImg' src='" +
            "/icons/" + data.value + ".svg" + "' /><div id='divCustomSelect'> " +
            data.key + "</div></div>";
    };

    function onLanguageSelectValueChanged(e) {
        var culture = e.value;
        if (culture == undefined || culture == null)
            return;
        try {
            setCulture(culture, false);
        }
        catch (error) {
            return;
        }
    };

    var setCulture = function (culture, ready) {
        const d = new Date();
        d.setTime(d.getTime() + (1 * 24 * 60 * 60 * 1000)); //1 Day
        let expires = "expires=" + d.toUTCString();
        document.cookie = "userLang=" + culture + ";" + expires;
        window.location.href = "/?culture="+ culture;
    };

    //function onTreeMenuValueChanged(data) {
    //    if (data.value) {
    //        let element = document.getElementById("treeMenu");
    //        let instance = DevExpress.ui.dxTreeView.getInstance(element);

    //        $("#treeMenu").dxTreeView("instance").getDataSource().filter(["Text", "contains", data.value]);
    //        $("#treeMenu").dxTreeView("instance").getDataSource().reload();
    //    }
    //    else
    //        $("#treeMenu").dxTreeView("instance").getDataSource().clearFilter();
    //}

    return {
        init: init,
        IndexLoad: IndexLoad,
        drawerMenu_click: drawerMenu_click,
        onTreeViewItem_click: onTreeViewItem_click,
        titleTemplate: titleTemplate,
        //clockTemplate: clockTemplate,
        btnLogOut_click: btnLogOut_click,
        btnRefresh_click: btnRefresh_click,
        btnChangeFacility_click: btnChangeFacility_click,
        btnHelpDesk_click: btnHelpDesk_click,
        btnChangePassword_click: btnChangePassword_click,
        btnAboutPage_click: btnAboutPage_click,
        profileButton_click: profileButton_click,
        profileItem_click: profileItem_click,
        onLanguageSelectBoxFieldTemplate: onLanguageSelectBoxFieldTemplate,
        onLanguageSelectBoxItemTemplate: onLanguageSelectBoxItemTemplate,
        onLanguageSelectValueChanged: onLanguageSelectValueChanged
    };
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Index.init();
});
