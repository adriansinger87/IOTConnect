﻿$(function () {

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
                getDevices: getDevices,
                trimmed: function (s) { return s.trimCss(); }
            },
            updated: function () {

            }
        });
    }


    function getDevices() {
        $.ajax({
            url: PATH + "api/devices",
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

    //function getSenor0815() {
    //    $.ajax({
    //        url: "/api/devices/data?id=M40/Sensor0815",
    //        type: 'GET',
    //        contentType: "application/json; charset=utf-8",
    //        datatype: 'json',
    //        async: true,
    //        error: function (result) {
    //            console.error(result);
    //        },
    //        success: function (result) {
    //            console.info(result);
    //            appVue2.jsonData = result;
    //        }
    //    });
    //}


});