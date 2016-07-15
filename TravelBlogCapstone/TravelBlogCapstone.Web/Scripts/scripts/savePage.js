$(document)
    .ready(function () {
        var uri = '/api/admin/SavePage';

        $('#submitPage')
            .click(function (e) {
                if ($("#createPageForm").valid()) {
                    e.preventDefault();

                    var newPage = {}; 

                    newPage.Title = $('#pageTitle').val();
                    newPage.PageContent = tinymce.activeEditor.getContent();

                    $.post(uri, newPage)
                        .done(function () {
                            alert('Page Added.');
                            window.location.href = '/Admin';
                        })
                        .fail(function (jqXhr, status, err) {
                            alert(status + ' - ' + err);
                        });
                }
            });
    });