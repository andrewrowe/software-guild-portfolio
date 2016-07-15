var uri = '/api/employee/getuserpostsindex';

$(document)
    .ready(function() {
        loadPosts();

        $("#pendingPosts").on("click", ".deleteButton", function (event) {
            if (confirm("Are you sure to delete the post?")) {
                $.ajax({
                    url: '/api/Employee/RemovePost/' + $(this).attr("data-value"),
                    type: 'PUT',
                    data: $(this).attr("data-value"),
                    success: function(data, status, xhr) {
                        $("div.alert-success").html("<strong>Success:</strong> Post was deleted succesfully.");
                        $("div.alert-success").fadeIn(300).delay(2500).fadeOut(500);
                        loadPosts();
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

function loadPosts() {
    $.getJSON(uri)
        .done(function (data) {
            $('#pendingPosts tbody tr').remove();
            $.each(data,
                function (index, post) {
                    $(createRow(post)).appendTo($('#pendingPosts tbody'));
                });

            if (data.length > 5) {
                $('#pendingPosts tbody tr:gt(4)').hide();
                $("#showAllButton").show();
            }

        });
};

function createRow(post) {

    var status;
    if (post.StatusString == "PendingUpdate") {
        status = "Pending - Updated Post";
    } else if (post.StatusString == "PendingNew") {
        status = "Pending - New Post";
    } else {
        status = post.StatusString;
    }


    return '<tr><td>' +
        status +
        '</td><td>' +
        '<a href="/employee/updatepost/' + post.Id + '" data-value="' + post.Id + '" class="postLink">' + post.Title + '</a>' +
        '</td><td style="padding-left: 2%">' +
       '<a class="deleteButton" href="#" data-value="' + post.Id + '" ><i class="glyphicon glyphicon-remove red"></i></a></td></tr>';
};