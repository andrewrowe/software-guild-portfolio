var uri = '/api/role/getroles';

$(document)
    .ready(function () {
       
        loadRoles();
        var test = $(".UserRole").attr('value');
        console.log(test);
       
        $("#tableRoles").on("click", ".upgradeUserToEmployee", function (event) {
            
            var id = ($(this).attr("data-value"));
            $.ajax({
                url: '/api/role/upgradeUserToEmployee/' + id,
                type: 'PUT',
                dataType: 'json',
                data: id,
                success: function (data, status, xhr) {
                    $("div.alert-success").html("<strong>Success:</strong> User was upgraded to Employee");
                    $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                    loadRoles();
                },
                error: function (xhr, status, err) {
                    $("div.alert-danger").html("<strong>Failure:</strong> There was an issue with the server: " + err);
                    $("div.alert-danger").fadeIn(300);
                }
            });
            event.preventDefault();
        });

        $("#tableRoles").on("click", ".upgradeEmployeeToAdmin", function (event) {

            var id = ($(this).attr("data-value"));
            $.ajax({
                url: '/api/role/upgradeEmployeeToAdmin/' + id,
                type: 'PUT',
                dataType: 'json',
                data: id,
                success: function (data, status, xhr) {
                    $("div.alert-success").html("<strong>Success:</strong> Employee was upgraded to Admin");
                    $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                    loadRoles();
                },
                error: function (xhr, status, err) {
                    $("div.alert-danger").html("<strong>Failure:</strong> There was an issue with the server: " + err);
                    $("div.alert-danger").fadeIn(300);
                }
            });
            event.preventDefault();
        });

        $("#tableRoles").on("click", ".downgradeEmployeeToUser", function (event) {

            var id = ($(this).attr("data-value"));
            $.ajax({
                url: '/api/role/downgradeEmployeeToUser/' + id,
                type: 'PUT',
                dataType: 'json',
                data: id,
                success: function (data, status, xhr) {
                    $("div.alert-success").html("<strong>Success:</strong> Employee was downgraded to User");
                    $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                    loadRoles();
                },
                error: function (xhr, status, err) {
                    $("div.alert-danger").html("<strong>Failure:</strong> There was an issue with the server: " + err);
                    $("div.alert-danger").fadeIn(300);
                }
            });
            event.preventDefault();
        });

        $("#tableRoles").on("click", ".downgradeAdminToEmployee", function (event) {

            var id = ($(this).attr("data-value"));
            $.ajax({
                url: '/api/role/downgradeAdminToEmployee/' + id,
                type: 'PUT',
                dataType: 'json',
                data: id,
                success: function (data, status, xhr) {
                    $("div.alert-success").html("<strong>Success:</strong> Admin was downgraded to Employee");
                    $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                    loadRoles();
                },
                error: function (xhr, status, err) {
                    $("div.alert-danger").html("<strong>Failure:</strong> There was an issue with the server: " + err);
                    $("div.alert-danger").fadeIn(300);
                }
            });
            event.preventDefault();
        });

        $("#tableRoles").on("click", ".deleteUser", function (event) {
            if (confirm("Are you sure to delete the user?")) {
                var id = ($(this).attr("data-value"));
                $.ajax({
                    url: '/api/role/deleteUser/' + id,
                    type: 'PUT',
                    dataType: 'json',
                    data: id,
                    success: function(data, status, xhr) {
                        $("div.alert-success").html("<strong>Success:</strong> User was deleted");
                        $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                        loadRoles();
                    },
                    error: function(xhr, status, err) {
                        $("div.alert-danger")
                            .html("<strong>Failure:</strong> There was an issue with the server: " + err);
                        $("div.alert-danger").fadeIn(300);
                    }
                });
                event.preventDefault();
            }
        });
});

function loadRoles() {
    $.getJSON(uri)
        .done(function (data) {
            $('#tableRoles tbody tr').remove();
            $.each(data,
                function (index, role) {
                    $(createRow(role)).appendTo($('#tableRoles tbody'));
                });
        });
};

function createRow(role) {

    var link = '';
    var currentUserId = $(".UserRole").attr('value');
    if (role.UserId != currentUserId) {
        if (role.RoleName == "Admin") {
            link = '<a class="downgradeAdminToEmployee" href="#" data-value="' + role.UserId + '"><i class="glyphicon glyphicon-arrow-down red"></i></a>';
        } else if (role.RoleName == "Employee") {
            link = '<a class="upgradeEmployeeToAdmin" href="#" data-value="' + role.UserId + '"><i class="glyphicon glyphicon-asterisk"></i></a>' +
               '<a class="downgradeEmployeeToUser" href="#" data-value="' + role.UserId + '" ><i class="glyphicon glyphicon-chevron-down red"></i></a> ';
        } else {
            link = '<a class="upgradeUserToEmployee" href="#" data-value="' + role.UserId + '" ><i class="glyphicon glyphicon-chevron-up"></i></a> ' +
                '<a class="deleteUser" href="#" data-value="' + role.UserId + '"><i class="glyphicon glyphicon-trash" style="color:grey"></i></a>';
        }
    }


    return '<tr><td>' +
        role.RoleName +
        '</td><td>' +
        role.FirstName +
        '</td><td>' +
        role.LastName +
        '</td><td>' +
        link + 
        '</td></tr>';
};
