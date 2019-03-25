$(function () {

    var appVue = getAppVue();
    initActions();

    console.info("_home.js done");

    // -- functions

    function initActions() {
        $(document).ready(function () {
            appVue.getDevices();
        });
    }

    function getAppVue() {
        return new Vue({
            el: '#app-vue',
            data: {
                devices: null
            },
            methods: {
                getDevices: getDevices
            },
            updated: function () {

            }
        });
    }

    function getDevices() {
        $.ajax({
            url: "/api/devices",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
                appVue.devices = result;
            }
        });
    }
});