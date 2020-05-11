
define(["dataTableCustom", "signalR", "signalRHubs"], function (table) {

    // SignalR related
    // Reference the auto-generated proxy for the hub.  
    var hubProxy = $.connection.scheduledDeletionFilesHub;

    hubProxy.client.refreshTable = function () {
        table.reloadTable();
    }

    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log('signalR connection active');
    });

});
