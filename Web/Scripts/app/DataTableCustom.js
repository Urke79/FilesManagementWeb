define(["copyToClipboard", "fileTableService", "datatablesBootstrap"],
    function (clipboard, fileTableService) {

        var table;
        var baseUrl = window.location.origin;

        var _init = function () {

            table = $('#filesTable').DataTable({
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

            table.on('click', 'tbody td', function () {

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
                            fileTableService.renameFile(data);

                        }
                    });
                }
            });

            table.on('click', 'button', function () {
                var id = $(this).closest('tr')[0].id;

                switch (true) {
                    case $(this).hasClass('js-clipboard'):
                        var text = baseUrl + '/api/FileMetadata/DownloadFile/' + id;
                        clipboard.copyTextToClipboard(text);
                        alert("File link was added to the clipboard. File Link:" + "\n" + text);
                        break;
                    case $(this).hasClass('js-refreshExpiryDate'):
                        fileTableService.refrheshExpiryDate(id);
                        break;
                    case $(this).hasClass('js-delete'):
                        fileTableService.deleteFile(id);
                        break;
                }
            });
        }

        function reloadTable() {
            table.ajax.reload()
        }

        _init();

        return {
            reloadTable: reloadTable
        };
    });