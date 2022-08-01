var Tickets = (function () {    

    function load() {}

    function init() {        
    }
    

    return {
        //Init, Load
        load: load,
        init: init,
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Tickets.init();
});