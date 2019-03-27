$(function () {

    var appVue = getAppVue();
    var appVue2 = getAppVue2();

    initActions();
 
    console.info("_home.js done");

    // -- functions

    function initActions() {
        $(document).ready(function () {
            appVue.getDevices();
            appVue2.getSenor0815();
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

    function getAppVue2() {
        return new Vue({
            el: '#app-vue2',
            data: {
                jsonData: null
            },
            methods: {
                getSenor0815: getSenor0815
            },
            mounted() {
                getAppChart(this.jsonData);
            },
            updated: function () {
                getAppChart(this.jsonData);
            }
        });
    }

    function getAppChart(jsonData) {
        
        var values = [];
        var timeArray = [];
        var valuesArray = [];
        //TODO @ ap  connect to real data
        if (jsonData === null) {
            values = [
                {
                    "t": "2019-03-26T16:11:28.033+01:00",
                    "v": 60
                },
                {
                    "t": "2019-03-26T16:11:29.034+01:00",
                    "v": 4
                }]
        }
        else {
            values = jsonData;
        }
        timeArray = values.map(element => element.t)
        valuesArray = values.map(element => element.v)
        timeArray.unshift('x')
        valuesArray.unshift('Sensor0185')
        console.log("jsonData: " + jsonData)
        var chart = c3.generate({
            bindto: '#chart',
            data: {
                type: "area",
                x: 'x',
                xFormat: '%Y-%m-%dT%H:%M:%S.%L%Z', // 'xFormat' can be used as custom format of 'x'
                columns: [
                    timeArray,
                    valuesArray
                ]
            },
            axis: {
                x: {
                    type: 'timeseries',
                    tick: {
                        format: '%Y-%m-%d %H:%M:%S.%L%Z'
                    }
                }
            }
        });
        return chart;
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

    function getSenor0815() {
        $.ajax({
            url: "/api/devices/data?id=M40/Sensor0815",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            async: true,
            error: function (result) {
                console.error(result);
            },
            success: function (result) {
                console.info(result);
                appVue2.jsonData = result;
                //console.info("appVue2.jsonData: " + appVue2.jsonData);
            }
        });
    }
});