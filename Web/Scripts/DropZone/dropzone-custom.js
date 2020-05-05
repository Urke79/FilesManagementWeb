Dropzone.options.myDropzone = {
    init: function () {
        this.on("complete", function (file) {
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                reloadTable();
            }
        });
    },
    dictDefaultMessage: "Drag and Drop Files Here"
}