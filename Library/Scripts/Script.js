$(document).ready(function () {

    GetUserInfo(getCookie('user'));

    GetLibraries();

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
        success: function (response) {
            success(response.responseText);
        },
        error: function (response) {
            alert('Something went wrong:( Try again!');
        }
    });
}

function addBookSuccess(message) {
    var alert = '<div class="alert alert-success" role="alert">Book successfully added!</div >';
    $("#cancelBookBtn").click();
    GetLibraries();
    $("#alert").html(alert);
    setTimeout(function () {
        $("#alert").empty();
    }, 2000);
}

function createLibrarySuccess() {
    $("#cancelLibBtn").click();

    var alert = '<div class="alert alert-success" role="alert">Library successfully created!</div >';

    GetLibraries();
    $("#alert").html(alert);

    UpdateListOfLibraries();

    setTimeout(function () {
        $("#alert").empty();
    }, 2000);
}

function DeleteBook(id) {
    $.ajax({
        url: 'api/Book/',
        type: "DELETE",
        dataType: "text",
        data: { bookId: id },
        success: function () { GetLibraries(); },
        error: function (response) {
            alert('error');
        }
    });
}

function TakeBook(id) {
    $.ajax({
        url: 'api/UsersBook/',
        type: "PUT",
        dataType: "text",
        data: { userId: getCookie('user'), bookId: id, date: $("#datePicker").val() },
        success: function () { GetListOfUsersBook(getCookie('user')); GetLibraries(); $("#modalCancel").click(); },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetLibraries() {

    var loader = '<div style="text-align: center; margin-top: 100px;"><div class="loader" style="display: inline-block;"></div></div>';

    $("#libContainer").html(loader);

    $.ajax({
        url: 'api/Library',
        method: "GET",
        dataType: "json",
        success: function (data) { RenderTables(data); },
        error: function (response) {
            alert('error');
        }
    });
}

function RenderTables(data) {

    var html = "";

    for (i = 0; i < data.length; i++) {
        html += '<h3>Library «' + data[i].Title + '»';
        if (window.isAdmin == true) {
            html += '<img src="/Content/trash.png" style="width: 26px;" onclick="DeleteLibrary(\'' + data[i].Title + '\');">';
        }
        html += '</h3>';
        html += '<table class="table table-striped">' +
            '<thead class="thead-inverse">' +
            '<tr><th>#</th><th>Title</th><th>Author</th>' +
            '<th>Genre</th> <th>Available</th> <th>Actions</th></tr ></thead > <tbody>';
        for (j = 0; j < data[i].ListOfBooks.length; j++) {
            html += '<tr><th>' + (j + 1) + '</th>';
            html += '<td>';
            html += data[i].ListOfBooks[j].Book.Title;
            html += '</td>';
            html += '<td>';
            html += data[i].ListOfBooks[j].Book.Author;
            html += '</td>';
            html += '<td>';
            html += data[i].ListOfBooks[j].Book.Genre;
            html += '</td>';
            if (window.isAdmin == true) {
                html += '<td><input class="form-control" onchange="ChangeCounter(this.value, ' + data[i].ListOfBooks[j].Book.Id + ')" type="number" value="' + data[i].ListOfBooks[j].Count + '" id="example-number-input" min="0"></td>';
                html += '<td><button type="button" class="btn btn-outline-danger" onclick="DeleteBook(\'' + data[i].ListOfBooks[j].Book.Id + '\');">Delete</button></td>';
            } else {
                html += '<td>' + data[i].ListOfBooks[j].Count + '</td>';
                html += '<td><button type="button" class="btn btn-outline-success" data-toggle="modal" data-target="#myModal" onclick="SetTakeBook(' + data[i].ListOfBooks[j].Book.Id + ');"';
                if (+data[i].ListOfBooks[j].Count < 1) {
                    html += ' disabled';
                }
                html += '>Take</button></td>';
            }
            html += '</tr>';
        }
        html += '</tbody></table>';
    }

    InsertHtml(html, 'libContainer');
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
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function GetUserInfo(userId) {
    $.ajax({
        url: 'api/User/',
        method: "GET",
        dataType: "json",
        data: { id: userId },
        success: function (data) { GetUserInfoSuccess(data); },
        error: function (response) {
            alert('error');
        }
    });
}

function GetUserInfoSuccess(data) {
    window.userName = data.Name;
    window.isAdmin = data.isAdmin;
}

function GetListOfUsersBook(userId) {
    $.ajax({
        url: 'api/Book/',
        method: "GET",
        dataType: "json",
        data: { id: userId },
        success: function (data) { GetListOfUsersBookSuccess(data); },
        error: function (response) {
            alert('error');
        }
    });
}

function GetListOfUsersBookSuccess(books) {
    var html = "";

    html += '<h3>My books</h3>';
    html += '<table class="table table-striped">';
    html += '<thead class="thead-inverse">';
    html += '<tr><th><div>#</th><th>Title</th><th>Author</th><th>End date</th><th>Action</th></tr>';
    html += '</thead>';
    html += '<tbody>';
    for (i = 0; i < books.length; i++) {
        html += '<tr>';
        html += '<th>' + (i + 1) + '</th>';
        html += '<td>';
        html += books[i].Book.Title;
        html += '</td>';
        html += '<td>';
        html += books[i].Book.Author;
        html += '</td>';
        html += '<td>' + books[i].EndDate + '</td>';
        html += '<td><button type="button" class="btn btn-outline-warning" onclick="ReturnBook(\'' + books[i].Book.Id + '\');">Return</button></td>';
        html += '</tr>';
    }
    html += '</tbody></table>';

    InsertHtml(html, 'booksBlock');
}

function ReturnBook(bookId) {
    $.ajax({
        url: 'api/UsersBook/',
        method: "DELETE",
        dataType: "json",
        data: { bookId: bookId, userId: getCookie('user') },
        success: function (data) { GetListOfUsersBook(getCookie('user')); GetLibraries(); },
        error: function (response) {
            alert('error');
        }
    });
}

function SetTakeBook(id) {
    window.takeBookId = id;
}

function DateTimeChanged() {
    $('#modalTakeBtn').prop('disabled', false);
}

function ChangeCounter(value, bookId) {
    $.ajax({
        url: 'api/Counter/',
        method: "PUT",
        data: { bookId: bookId, value: value },
        error: function (response) {
            alert('error');
        }
    });
}

function DeleteLibrary(title) {
    $.ajax({
        url: 'api/Library/',
        method: "DELETE",
        data: { title: title },
        success: function () { GetLibraries(); UpdateListOfLibraries(); },
        error: function (response) {
            alert('error');
        }
    });
}

function UpdateListOfLibraries() {
    $.ajax({
        url: 'api/Counter/',
        method: "GET",
        success: function (data) { RenderLibrariesList(data); },
        error: function (response) {
            alert('error');
        }
    });
}

function RenderLibrariesList(data) {
    var html = "";
    html += '<label for="exampleInputSelector">Add to library</label>';
    html += '<select name="LibraryTitle" class="form-control" id="exampleInputSelector">';
    html += '<option disabled selected value="0">Select library...</option>';
    for (i = 0; i < data.length; i++) {
        html += '<option value="' + data[i].Title + '">' + data[i].Title + '</option>';
    }
    html += '</select>';

    $("#listOfLibrary").html(html);
}
    