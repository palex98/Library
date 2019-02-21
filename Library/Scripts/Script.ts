/// <reference path="AjaxRequest.ts" />

interface Window {
    userName: any;
    isAdmin: any;
    takeBookId: any;
}

window.onload =  function () {

    GetUserInfo(getCookie('user'));

    GetLibraries(1);

    if (document.getElementById("booksBlock") != null) {
        GetListOfUsersBook(getCookie('user'));
    }

    var addBookBtn = document.getElementById("addBookBtn");

    addBookBtn.onclick = function () {
        sendAjaxForm('addForm', 'api/Book/', addBookSuccess);
    };

    document.getElementById("cancelBookBtn").onclick = function () {
        document.getElementById("resetBookBtn").click();
        document.getElementById("addBtn").click();
    };

    document.getElementById("createLibBtn").onclick = function () {
        sendAjaxForm('createForm', 'api/Library/', createLibrarySuccess);
    };

    document.getElementById("cancelLibBtn").onclick = function () {
        document.getElementById("createBtn").click();
    };
};

function createLibrarySuccess() {
    document.getElementById("cancelLibBtn").click();

    var alert = '<div class="alert alert-success" role="alert">Library successfully created!</div >';

    GetLibraries(1);
    document.getElementById("alert").innerHTML = alert;

    UpdateListOfLibraries();

    setTimeout(function () {
        document.getElementById("alert").innerHTML = "";
    }, 2000);
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