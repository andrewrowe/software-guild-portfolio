var uri = '/api/employee/getapprovedposts';

$(document)
    .ready(function () {
        loadPosts();
    });

function loadPosts() {
    $.getJSON(uri)
        .done(function (data) {
            $('#approvedPosts tbody tr').remove();
            $.each(data,
                function (index, post) {
                    $(createRow(post)).appendTo($('#approvedPosts tbody'));
                });

            if (data.length > 5) {
                $('#approvedPosts tbody tr:gt(4)').hide();
                $("#showAllButton").show();
            }

        });

    $("#approvedPosts").on("click", ".postLink", function (event) {
        var id = ($(this).attr("data-value"));
        var getPostUri = '/api/post/GetOnlinePost/';

        $.getJSON(getPostUri + id)
        .done(function (data) {
            console.log(data);

            $('#viewPostBody').empty();
            $('#ViewPostHeading').empty();

            $('#viewPostBody').append((data.PostContent));
            $('#ViewPostHeading').append((data.Title));

            $('#viewPostModal').modal('show');


        });



    });
};

function createRow(post) {

    var status;
    if (new Date(post.PublishedDate) <= Date.now()) {
        status = "<span class='glyphicon glyphicon-ok green'></span>";
    } else {
        status = moment(post.PublishedDate).format("MM/DD/YYYY");
    }


    return '<tr><td style="padding-left: 6%">' +
        status +
        '</td><td>' +
        '<a href="#' + post.Id + '" data-value="' + post.Id + '" class="postLink">' + post.Title + '</a>' +
        '</td><td>' +
       '<a class="editButton" href="/employee/updatepost/' + post.Id + '" data-value="' + post.Id + '" ><i class="glyphicon glyphicon-pencil"></i></a></td></tr>';
};