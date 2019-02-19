function RenderTables(data) {

    var html = "";

    for (i = 0; i < data.length; i++) {
        html += '<h3>Library «' + data[i].Title + '»';
        if (window.isAdmin == true) {
            html += '<img src="/Content/trash.png" style="width: 26px;" alt="Delete library" title="Delete library" onclick="DeleteLibrary(\'' + data[i].Title + '\');">';
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