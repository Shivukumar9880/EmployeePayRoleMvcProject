$(document).ready(function () {
    ShowEmployeeData();

});

function ShowEmployeeData() {debugger

    $.ajax(
        {
            url: '/Ajax/AjaxController',
            type: 'Get',
            dataType: 'json',
            contentType: 'applications/json;charset=utf-8;',
            success: function (result, statu, xhr) {

                var object = '';
                $.each(result, function (index, item) {

                    object += '<tr>';
                    object += '<td>' + item.employeeId + '</td>';
                    object += '<td>' + item.fullName + '</td>';
                    object += '<td>' + item.gender + '</td>';
                    object += '<td>' + item.department + '</td>';
                    object += '<td>' + item.imagePath + '</td>';
                    object += '<td>' + item.startDate + '</td>';
                    object += '<td>' + item.salary + '</td>';
                    object += '<td>' + item.notes + '</td>';
                    object += '<td><a class="btn btn-primary" href="EmployeeControllercs/Edit/' + item.employeeId + '">Edit</a> || <a class="btn btn-danger" href="#">Delete</a></td>';
                    object += '</tr>';
                });
                $('#table_data').html(object);
            },
            error: function () {
                alert("Data can not get");
            }

        }
    );
}