$(document).ready(function () {
    $("#addBookBtn").click(
        function () {
            sendAjaxForm('addForm', 'api/Book/', addBookSuccess);
        }
    );

    $("#resetBookBtn").click(
        function () {
            $("#addForm")[0].reset();
        }
    );

    $("#cancelBookBtn").click(
        function () {
            $("#addForm")[0].reset();
            $("#addBtn").click();
        }
    );

    $("#createLibBtn").click(
        function () {
            sendAjaxForm('createForm', 'api/Library/', createLibrarySuccess);
        }
    );

    $("#cancelLibBtn").click(
        function () {
            $("#createForm")[0].reset();
            $("#createBtn").click();
        }
    );
});

function sendAjaxForm(ajax_form, url, success) {
    var data = $("#" + ajax_form).serialize();
    $.ajax({
        url: url,
        method: "POST",
        dataType: "text",
        data: data,
        success: success,
        error: function (response) {
            alert('error');
        }
    });
}

function addBookSuccess() {
    $("#cancelBookBtn").click();
}

function createLibrarySuccess() {
    $("#cancelLibBtn").click();
}

function DeleteBook(id) {
    $.ajax({
        url: 'api/Book/',
        type: "DELETE",
        dataType: "text",
        data: { bookId: id },
        success: function () { alert('ok'); },
        error: function (response) {
            alert('error');
        }
    });
}

function UpdateLibrary() {
    $.ajax({
        url: url,
        method: "GET",
        dataType: "json",
        success: function(json) { UpdateTables(json); },
        error: function (response) {
            alert('error');
        }
    });
}

function UpdateTables(json) {
    var data = JSON.parse(json);


}

function SetAdmin(bool) {
    if (bool == 'true') {
        window.isAdmin = true;
    } else {
        window.isAdmin = false;
    }
}