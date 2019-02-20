/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="AjaxRequest.ts" />

interface Window {
    userName: any;
    isAdmin: any;
    takeBookId: any;
}

$(document).ready(function () {

    GetUserInfo(getCookie('user'));

    GetLibraries(1);

    if ($('#booksBlock').length > 0) {
        GetListOfUsersBook(getCookie('user'));
    }

    $("#addBookBtn").click(
        function () {
            sendAjaxForm('addForm', 'api/Book/', addBookSuccess);
        }
    );

    $("#resetBookBtn").click(
        function () {
            //$("#addForm")[0].reset();
        }
    );

    $("#cancelBookBtn").click(
        function () {
            //$("#addForm")[0].reset();
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
            //$("#createForm")[0].reset();
            $("#createBtn").click();
        }
    );
});

function createLibrarySuccess() {
    $("#cancelLibBtn").click();

    var alert = '<div class="alert alert-success" role="alert">Library successfully created!</div >';

    GetLibraries(1);
    $("#alert").html(alert);

    UpdateListOfLibraries();

    setTimeout(function () {
        $("#alert").empty();
    }, 2000);
}

function InsertHtml(html, id) {
    $("#" + id).html(html);
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function GetUserInfoSuccess(data) {
    window.userName = data.Name;
    window.isAdmin = data.isAdmin;
}

function SetTakeBook(id) {
    window.takeBookId = id;
}

function DateTimeChanged() {
    $('#modalTakeBtn').prop('disabled', false);
}