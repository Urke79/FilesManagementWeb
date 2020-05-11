var require = {
    baseUrl: "/Scripts",  
    paths: {
        jquery: "lib/jquery-1.10.2.min",
        datatables: "lib/DataTables-1.10.20/js/jquery.dataTables",
        datatablesBootstrap: "lib/DataTables-1.10.20/js/dataTables.bootstrap4",
        'datatables.net': "lib/DataTables-1.10.20/js/jquery.dataTables.min",
        dataTableCustom: "app/dataTableCustom",
        index: "app/index",
        dropZone: "lib/DropZone/dropzone-amd-module",
        signalR: "lib/jquery.signalR-2.4.1.min",
        signalRHubs: "../signalr/hubs?noext",
        copyToClipboard: "app/clipboard",
        fileTableService: "app/fileTableService"

    },

    shim: {
        datatablesBootstrap: {
          deps: ["datatables"]
        },
        signalR: {
          deps: ["jquery"]
        },
        signalRHubs: {
          deps: ["signalR"]
        }
    }
};