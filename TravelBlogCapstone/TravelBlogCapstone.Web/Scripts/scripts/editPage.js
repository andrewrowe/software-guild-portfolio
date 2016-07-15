$(document)
    .ready(function () {
        $('#submitPage')
            .click(function (e) {
                if ($("#createPageForm").valid()) {
                    e.preventDefault();

                    var newPage = {};

                    newPage.ID = $('#ID').val();
                    newPage.Title = $('#pageTitle').val();
                    newPage.PageContent = tinymce.activeEditor.getContent();

                    $.ajax({
                        url: '/api/admin/EditPage',
                        type: 'PUT',
                        dataType: 'json',
                        data: newPage,
                        success: function(data, status, xhr) {
                            window.location.href = '/admin/index';
                        },
                        error:function(xhr, status, err) {
                            alert('Error:' + err);
                        }
                });
                }
            });
    });