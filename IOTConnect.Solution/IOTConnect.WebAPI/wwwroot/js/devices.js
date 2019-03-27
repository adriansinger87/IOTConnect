$(function () {

    var itemsArray = [];

    var chart = initChart();

    initActions();

    console.info("devices.js done");


    // -- functions

    function initActions() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/devicesHub")
            .configureLogging(signalR.LogLevel.Warning)
            .build();

         /*TODO @ AS still on browser reload or by having more then on browser 
         * opend data from signalr is written multiple times in the array for the chart*/

        connection.on("MqttReceived", onMqttReceived);

        connection
            .start()
            .then(function () {
                console.info("signalR sarted");
            })
            .catch(function (err) {
                return console.error(err.toString());
        });
    }
    
    function initChart() {
        var chart = c3.generate({
            bindto: '#chart',
            data: {
                type: "area-spline",
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

    function onMqttReceived(topic, message) {

        var text = getSelectedText('selection');

        if (topic == text) {
            var data = JSON.parse(message);
            if (itemsArray[topic] == undefined || 
                itemsArray[topic].length == 0) {

                itemsArray[topic] = [];
                itemsArray[topic].push(data);
                updateChart(chart, topic, itemsArray)

            } else if (itemsArray[topic].last().timestamp != data.timestamp) {

                itemsArray[topic].push(data);
                updateChart(chart, topic, itemsArray)
            }
        }
    }
    
    function getSelectedText(elementId) {
        var elt = document.getElementById(elementId);

        if (elt.selectedIndex == -1)
            return null;

        return elt.options[elt.selectedIndex].text;
    }

    function updateChart(chartObject, name, json) {

        var list = json[name];
        var times = [];
        var values = [];

        times = list.map(element => element.timestamp)
        values = list.map(element => element.value)

        times.unshift('x')
        values.unshift(name)

        //TODO @ AS in devices.js removing the old device data from the chart via unload in the update function isn't working
        chartObject.load({
            columns: [times, values]
        });

    }
});