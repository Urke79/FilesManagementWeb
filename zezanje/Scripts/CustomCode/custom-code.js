function reloadTable() {
    $('#filesTable').DataTable().ajax.reload()
}

var baseUrl = window.location.origin;

// SignalR related
// Reference the auto-generated proxy for the hub.  
var hubProxy = $.connection.scheduledDeletionFilesHub;

hubProxy.client.refreshTable = function () {
    reloadTable();
}

// Start the connection.
$.connection.hub.start().done(function () {
    console.log('signalR connection active');
});

// CRUD
function deleteFile(id) {
    $.ajax({
        url: baseUrl + '/api/filemetadata/' + id,
        type: 'DELETE',
        success: function (result) {
            if (result === true) {
                reloadTable();
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
                reloadTable();
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
                reloadTable();
            }
            else {
                // TO DO
            }
        }
    });
}

// Copy to clipboard functionality
function fallbackCopyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Fallback: Copying text command was ' + msg);
    } catch (err) {
        console.error('Fallback: Oops, unable to copy', err);
    }

    document.body.removeChild(textArea);
}

function copyTextToClipboard(text) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text);
        return;
    }
    navigator.clipboard.writeText(text).then(function () {
        console.log('Async: Copying to clipboard was successful!');
    }, function (err) {
        console.error('Async: Could not copy text: ', err);
    });
}

$(document).ready(function () {

    var table = $('#filesTable').DataTable({
        "processing": true,
        "ajax": {
            "url": baseUrl + "/api/filemetadata/showfiles",
            "type": "POST",
            "dataSrc": ""
        },
        "language": {
            "search": "Search by Name:"
        },
        "columns": [
           { "data": "FileName" },
           { "data": "Size", "searchable": false },
           { "data": "DateUploaded", "searchable": false },
           { "data": "Expires", "searchable": false },
           { "data": null, "searchable": false }
        ],
        "columnDefs": [{
            "targets": 4, "data": "null", "render": function (data, type, full, meta) {
                return '<button title="copies the file link to the clipboard" class="btn js-clipboard"><i class="glyphicon glyphicon-upload"></i></button>' +
                  ' <button title="renews expiration for 90 days from today’s date" class="btn js-refreshExpiryDate"><i class="glyphicon glyphicon-repeat"></i></button>' +
                  ' <button title="deletes file from the system and this list" class="btn js-delete"><i class="glyphicon glyphicon-trash"></i></button>'
            }
        }]

    });

    $('#filesTable').on('click', 'tbody td', function () {

        // Check if it's a first column cell
        var isFirstColumnCell = $(this).parent().children().first().is($(this));

        if (isFirstColumnCell) {
            // enable editing of the cell
            this.setAttribute('contenteditable', true);
            // remove all events for the cell
            $(this).off();
            // add focus & blur events to the cell
            $(this).on("focus", function (e) {
                original = e.target.textContent;
            });

            $(this).on("blur", function (e) {
                var newName = e.target.textContent;

                if (original !== newName) {

                    // TO DO check if file extension is removed prior to submitting

                    // submit change to server

                    var id = $(this).closest('tr')[0].id;

                    var data = {
                        "id": id,
                        "newName": newName
                    }
                    renameFile(data);

                }
            });
        }
    });

    $('#filesTable').on('click', 'button', function () {
        var id = $(this).closest('tr')[0].id;

        switch (true) {
            case $(this).hasClass('js-clipboard'):
                var text = baseUrl + '/api/FileMetadata/DownloadFile/' + id;
                copyTextToClipboard(text);
                alert("File link was added to the clipboard. File Link:" + "\n" + text);
                break;
            case $(this).hasClass('js-refreshExpiryDate'):
                refrheshExpiryDate(id);
                break;
            case $(this).hasClass('js-delete'):
                deleteFile(id);
                break;
        }
    });
});