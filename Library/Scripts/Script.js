/// <reference path="AjaxRequest.ts" />
window.onload = function () {
    GetUserInfo(getCookie('user'));
    GetLibraries(1);
    if (document.getElementById("booksBlock") != null) {
        GetListOfUsersBook(getCookie('user'));
    }
    var addBookBtn = document.getElementById("addBookBtn");
    if (addBookBtn != null) {
        addBookBtn.onclick = function () {
            sendAjaxForm('addForm', 'api/Book/', addBookSuccess);
        };
    }
    var cancelBookBtn = document.getElementById("cancelBookBtn");
    if (cancelBookBtn != null) {
        cancelBookBtn.onclick = function () {
            document.getElementById("resetBookBtn").click();
            document.getElementById("addBtn").click();
        };
    }
    var createLibBtn = document.getElementById("createLibBtn");
    if (createLibBtn != null) {
        createLibBtn.onclick = function () {
            sendAjaxForm('createForm', 'api/Library/', createLibrarySuccess);
        };
    }
    var cancelLibBtn = document.getElementById("cancelLibBtn");
    if (cancelLibBtn != null) {
        cancelLibBtn.onclick = function () {
            document.getElementById("createBtn").click();
        };
    }
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
//# sourceMappingURL=Script.js.map