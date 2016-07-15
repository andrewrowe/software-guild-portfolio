$(document)
    .ready(function () {

        $('#createPostForm')
            .validate({
                rules: {
                    postTitleEntry: {
                        required: true
                    },
                    publishDateEntry: {
                        required: true
                    },
                    categoryList: {
                        required: true
                    }
                },
                messages: {
                    postTitleEntry: {
                        required: "Give your post a title!"
                    },
                    publishDateEntry: {
                        required: "Please select a publish date."
                    },
                    categoryList: {
                        required: "Please select at least one category."
                    }
                }
            });

    });
