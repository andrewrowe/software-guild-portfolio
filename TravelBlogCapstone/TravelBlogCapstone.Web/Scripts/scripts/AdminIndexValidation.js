$(document)
    .ready(function () {
        $('#remarkForm')
            .validate({
                rules: {
                    remark: {
                        required: true
                    }
                },
                messages: {
                    remark: {
                        required: "Please enter a remark"
                    }
                }
            });

        $('#AddCategoryForm')
           .validate({
               rules: {
                   category: {
                       required: true,
                       remote: "api/post/CheckCategory"
                   }
               },
               messages: {
                   category: {
                       required: "Please enter a category",
                       remote: "Category Exists"
                   }
               }
           });
    });