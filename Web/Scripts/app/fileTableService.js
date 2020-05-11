
define(["require"], function (require) {

    var baseUrl = window.location.origin;

    function deleteFile(id) {
        $.ajax({
            url: baseUrl + '/api/filemetadata/' + id,
            type: 'DELETE',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable(); // circular dependency -  rethink!!!!!
                }
                else {
                    // TO DO
                    // toastr.error("Error deleting event!");
                }
            }
        });
    }

    function refrheshExpiryDate(id) {
        $.ajax({
            url: baseUrl + '/api/filemetadata/' + id,
            type: 'PUT',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable();  // circular dependency -  rethink!!!!!
                }
                else {
                    // TO DO
                }
            }
        });
    }

    function renameFile(data) {
        $.ajax({
            url: baseUrl + '/api/filemetadata',
            data: data,
            type: 'PUT',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable();  // circular dependency -  rethink!!!!!
                }
                else {
                    // TO DO
                }
            }
        });
    }

    return {
        deleteFile: deleteFile,
        refrheshExpiryDate: refrheshExpiryDate,
        renameFile: renameFile
    }
});