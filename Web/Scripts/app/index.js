
define(["dropZone", "dataTableCustom"], function (dropzone, table) {

    // initialize drop zone 
    window.Dropzone = dropzone;
    window.Dropzone.autoDiscover = false;

    $('.dropzone').dropzone({
        init: function () {
            this.on("complete", function (file) {
                if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                    table.reloadTable();
                }
            });
        },
        dictDefaultMessage: "Drag and Drop Files Here"
    });
});