$(document).ready(function () {
    $("#addBookBtn").click(
        function () {
            sendAjaxForm('addForm', 'api/Book/PostBook/', addBookSuccess);
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
    alert("success");
}
