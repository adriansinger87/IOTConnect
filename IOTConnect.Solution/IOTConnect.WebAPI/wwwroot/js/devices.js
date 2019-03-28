$(function () {

    var itemsArray = [];
    var chart = initChart();

    initActions();
    console.info("devices.js done");

    // -- functions

    function initActions() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl(PATH + "devicesHub")
            .configureLogging(signalR.LogLevel.Warning)
            .build();

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
                type: "area",
                x: 'x',
                xFormat: '%Y-%m-%dT%H:%M:%S.%LZ', // 'xFormat' can be used as custom format of 'x'
                columns: []
            },
            axis: {
                x: {
                    type: 'timeseries',
                    tick: {
                        format: '%Y-%m-%d %H:%M:%S'
                    }
                }
            }
        });
        return chart;
    }

    function onMqttReceived(topic, message) {

        var selected = $("#cb_" + topic.trimCss())[0].checked;

        if (selected) {
            if (itemsArray[topic] === undefined || 
                itemsArray[topic].length === 0) {

                itemsArray[topic] = [];
                itemsArray[topic].push(message);
                updateChart(chart, topic, itemsArray);

            } else if (itemsArray[topic].last().timestamp !== message.timestamp) {

                var lastTime = moment().add(-1, 'm');  // older than now minus 1 minute
                var ts = itemsArray[topic][0].timestamp;
                if (moment(ts).isBefore(lastTime)) {
                    itemsArray[topic].shift();  // removes the first item ant pushes the new one
                }

                itemsArray[topic].push(message);
                updateChart(chart, topic, itemsArray);
            }
        }
    }
    
    function getSelected(elementId) {
        var elt = document.getElementById(elementId);

        if (elt.selectedIndex === -1)
            return null;

        return elt.options[elt.selectedIndex].text;
    }

    function updateChart(chartObject, name, json) {

        var list = json[name];
        var times = [];
        var values = [];

        times = list.map(element => element.timestamp);
        values = list.map(element => element.value);

        times.unshift('x');
        values.unshift(name);

        //TODO @ AS in devices.js removing the old device data from the chart via unload in the update function isn't working
        chartObject.load({
            columns: [times, values]
        });

    }
});