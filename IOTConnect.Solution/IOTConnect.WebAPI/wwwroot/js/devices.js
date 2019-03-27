$(function () {

    getAppVue2();
    var chart = getAppChart();
    chart.columns
    initActions();
    console.info("devices.js done");

    //field
    var itemsArray = [];
    // -- functions

    function initActions() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/devicesHub")
            .configureLogging(signalR.LogLevel.Trace)
            .build();


        connection.on("MqttReceived", function (topic, message) {
            //console.log(topic, message);
            var text = getSelectedText('selection');
            if (topic == text) {
                itemsArray.push(JSON.parse(message))
                removeDups(itemsArray);
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

        chartObject.load({
            columns: [timeArray, valuesArray]
        });
        //console.info("!!!valuesArray length!!!!!" + valuesArray.length);
        //var chart = c3.generate({
        //    bindto: '#chart',
        //    data: {
        //        type: "area",
        //        x: 'x',
        //        xFormat: '%Y-%m-%dT%H:%M:%S.%LZ', // 'xFormat' can be used as custom format of 'x'
        //        columns: [
        //            timeArray,
        //            valuesArray
        //        ]
        //    },
        //    axis: {
        //        x: {
        //            type: 'timeseries',
        //            tick: {
        //                format: '%Y-%m-%d %H:%M:%S.%L'
        //            }
        //        }
        //    }
        //});

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