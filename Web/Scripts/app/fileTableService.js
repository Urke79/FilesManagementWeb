
define(["require", "constants"], function (require, constants) {

    function deleteFile(id) {
        $.ajax({
            url: constants.baseUrl + '/api/filemetadata/' + id,
            type: 'DELETE',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable();
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
            url: constants.baseUrl + '/api/filemetadata/' + id,
            type: 'PUT',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable();
                }
                else {
                    // TO DO
                }
            }
        });
    }

    function renameFile(data) {
        $.ajax({
            url: constants.baseUrl + '/api/filemetadata',
            data: data,
            type: 'PUT',
            success: function (result) {
                if (result === true) {
                    require("dataTableCustom").reloadTable();
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