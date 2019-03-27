$(function () {

    getAppVue2();
    var chart = getAppChart();
    chart.columns
    initActions();
    console.info("devices.js done");

    // field
    //TODO @ AS all data is still writen in one array should be one for each data source
    var itemsArray = [];

    // -- functions

    function initActions() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/devicesHub")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

         /*TODO @ AS still on browser reload or by having more then on browser 
         * opend data from signalr is written multiple times in the array for the chart */

        connection.on("MqttReceived", function (topic, message) {
            //console.log(topic, message);
            var text = getSelectedText('selection');
            if (topic == text) {
                itemsArray.push(JSON.parse(message))
          //TODO @ AS when the removeDups function for removing duplicate values is asigned to overwrite itemsArray a failure in signalR occures
               // itemsArray = removeDups(itemsArray);
                updateChart(chart, topic, itemsArray)
            }

        });

        connection.start();
        //    .start()
        //    .catch(function (err) {
        //    return console.error(err.toString());
        //});
    }

    function getAppVue2() {
        return new Vue({
            el: '#app-vue2',
            data: {
                jsonData: null
            }
        });
    }



    function getSelectedText(elementId) {
        var elt = document.getElementById(elementId);

        if (elt.selectedIndex == -1)
            return null;

        return elt.options[elt.selectedIndex].text;
    }

    function updateChart(chartObject, name, jsonData) {

        var timeArray = [];
        var valuesArray = [];


        timeArray = jsonData.map(element => element.timestamp)
        valuesArray = jsonData.map(element => element.value)

        timeArray.unshift('x')
        valuesArray.unshift(name)

        //TODO @ AS in devices.js removing the old device data from the chart via unload in the update function isn't working
        chartObject.load({
            columns: [timeArray, valuesArray]//,unload: ['device']
        });

    }
    function getAppChart() {

        var timeArray = [];
        var valuesArray = [];

        var chart = c3.generate({
            bindto: '#chart',
            data: {
                type: "area",
                x: 'x',
                xFormat: '%Y-%m-%dT%H:%M:%S.%LZ', // 'xFormat' can be used as custom format of 'x'
                columns: []
            },
            axis: {
                x: {
                    type: 'timeseries',
                    tick: {
                        format: '%Y-%m-%d %H:%M:%S.%L'
                    }
                }
            }
        });
        return chart;
    }

    function removeDups(items) {
        let unique = {};
        items.forEach(function (i) {
            if (!unique[i]) {
                unique[i] = true;
            }
        });
        return Object.keys(unique);
    }
});