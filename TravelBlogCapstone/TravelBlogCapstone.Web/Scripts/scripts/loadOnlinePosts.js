//var uri = '/api/post/getonlineposts';

//$(document)
//    .ready(function() {
//        loadPosts();
//        $("table.sieve").sieve();
//        $("#allOnlinePosts")
//            .on("click",
//                ".selectButton",
//                function(event) {
//                    var id = ($(this).attr("data-value"));
//                    var uri = 'api/post/getonlinepost/' + id;

//                    $.getJSON(uri);
//                    event.preventDefault();
//                });

//        function loadPosts() {
//            $.getJSON(uri)
//                .done(function(data) {
//                    $.each(data,
//                        function(index, post) {
//                            $(addPost(post)).appendTo($('#allOnlinePosts'));
//                        });
//                });
//        };

//        function addPost(post) {
//            var blurb = post.PostContent.substring(0, 750);

//            return ('<tr><td><b><div class="row"><div class="col-xs-6">' +
//                post.Title +
//                '</b></div><div class="col-xs-6" align="right">' +
//                moment(post.PublishedDate).format('MM/DD/YY') +
//                '</div></div><br/>' +
//                blurb +
//                '...' +
//                '<br/><button type="button" ' +
//                'class="btn btn-primary btn-block selectButton" data-value="' +
//                post.ID +
//                '">View Post</button>' +
//                '</td></tr>');
//        };
//    });