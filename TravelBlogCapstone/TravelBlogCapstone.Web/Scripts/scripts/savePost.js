$(document)
    .ready(function () {
        var uri = '/api/employee';

        $('#submitPost')
            .click(function (e) {
                if ($("#createPostForm").valid()) {
                    e.preventDefault();

                    if ($("#expireDate").val() != "" && $("#expireDate").val() < $("#publishDate").val()) {
                        alert("Expire date must be after the publish date.");
                        return;
                    };
                    var d = new Date();
                    if (d >= $("#publishDate").val()) {
                        alert("Publish date must be in the future.");
                        return;
                    };

                    var newPost = {};

                    //get values from inputs
                    newPost.Title = $('#postTitle').val();
                    if ($('#submissionOptions').val() == 2) {
                        newPost.StatusId = 2;//pending new
                    }
                    else if ($('#submissionOptions').val() == 4) {
                        newPost.StatusId = 4;//draft
                    }
                    newPost.PostContent = tinymce.activeEditor.getContent();
                    newPost.PublishedDate = $('#publishDate').val();
                    newPost.ExpiredDate = $('#expireDate').val();
                    newPost.CategoriesId = $('#categories').val();
                    newPost.UserId = $('#UserId').val();
                    
                    var array = $("input[name=tagsArray]").val().split(',');

                    newPost.TagsName = array;



                    //post it to the WebAPI, passing the javascript object
                    $.post(uri, newPost)
                        .done(function () {
                            alert('Submitted succesfully.');
                            window.location.href = '/Employee/Index/';
                        })
                        .fail(function (jqXhr, status, err) {
                            alert(status + ' - ' + err);
                        });
                }
            });
    });
