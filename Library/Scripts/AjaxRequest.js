/// <reference path="Script.ts" />
function addBookSuccess() {
    var alert = '<div class="alert alert-success" role="alert">Book successfully added!</div >';
    document.getElementById("cancelBookBtn").click();
    GetLibraries(1);
    document.getElementById("alert").innerHTML = alert;
    setTimeout(function () {
        document.getElementById("alert").innerHTML = "";
    }, 2000);
}
function DeleteBook(id) {
    $.ajax({
        url: 'api/Book/',
        type: "DELETE",
        dataType: "text",
        data: { bookId: id },
        success: function () { GetLibraries(1); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function TakeBook(id) {
    $.ajax({
        url: 'api/UsersBook/',
        type: "PUT",
        dataType: "text",
        data: { userId: getCookie('user'), bookId: id, date: document.getElementById("datePicker").value },
        success: function () { GetListOfUsersBook(getCookie('user')); GetLibraries(1); document.getElementById("modalCancel").click(); },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function GetLibraries(sort) {
    var loader = '<div style="text-align: center; margin-top: 100px;"><div class="loader" style="display: inline-block;"></div></div>';
    document.getElementById("libContainer").innerHTML = loader;
    $.ajax({
        url: 'api/Library',
        method: "GET",
        dataType: "json",
        data: { sort: sort },
        success: function (data) { RenderTables(data); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function sendAjaxForm(ajax_form, url, success) {
    var data = $("#" + ajax_form).serialize();
    $.ajax({
        url: url,
        method: "POST",
        dataType: "text",
        data: data,
        success: function () {
            success();
        },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function GetUserInfo(userId) {
    $.ajax({
        url: 'api/User/',
        method: "GET",
        dataType: "json",
        data: { id: userId },
        success: function (data) { GetUserInfoSuccess(data); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function GetListOfUsersBook(userId) {
    $.ajax({
        url: 'api/Book/',
        method: "GET",
        dataType: "json",
        data: { id: userId },
        success: function (data) { GetListOfUsersBookSuccess(data); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function ReturnBook(bookId) {
    $.ajax({
        url: 'api/UsersBook/',
        method: "DELETE",
        dataType: "json",
        data: { bookId: bookId, userId: getCookie('user') },
        success: function () { GetListOfUsersBook(getCookie('user')); GetLibraries(1); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function ChangeCounter(value, bookId) {
    $.ajax({
        url: 'api/Counter/',
        method: "PUT",
        data: { bookId: bookId, value: value },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function DeleteLibrary(title) {
    $.ajax({
        url: 'api/Library/',
        method: "DELETE",
        data: { title: title },
        success: function () { GetLibraries(1); UpdateListOfLibraries(); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function UpdateListOfLibraries() {
    $.ajax({
        url: 'api/Counter/',
        method: "GET",
        success: function (data) { RenderLibrariesList(data); },
        error: function () {
            alert('Something went wrong:( Try again!');
        }
    });
}
function GetUserInfoSuccess(data) {
    window.userName = data.Name;
    window.isAdmin = data.isAdmin;
}
function SetTakeBook(id) {
    window.takeBookId = id;
}
function DateTimeChanged() {
    document.getElementById("modalTakeBtn").removeAttribute('disabled');
}
//# sourceMappingURL=AjaxRequest.js.map