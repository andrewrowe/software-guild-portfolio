$(document)
    .ready(function() {
        $('#categorySelect')
            .on('change',
                function () {
                    $(".blogPostSelect").hide();
                    $(".blogPostSelect")
                        .each(function() {
                            var self = this;
                            var arr = $(this).data('id').split('_');
                            var category = parseInt($('#categorySelect').val());
                            $.each(arr,
                                function(index, item) {
                                    if (item.trim() == category) {
                                        $(self).show();
                                    } 
                                });
                        });
                });
    });
