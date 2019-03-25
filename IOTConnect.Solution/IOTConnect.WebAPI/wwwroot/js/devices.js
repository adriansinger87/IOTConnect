$(function () {

    initActions();

    console.info("devices.js done");

    // -- functions

    function initActions() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/devicesHub")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        connection.on("MqttReceived", function (topic, message) {
            console.log(topic, message);
        });

        connection.start();
        //    .start()
        //    .catch(function (err) {
        //    return console.error(err.toString());
        //});
    }
});