$("#myDVDs")
    .on("click",
        ".removeButton",
        function(dvd) {
            $.ajax({
                url: '/api/',
                type: 'DELETE',
                success: function(data, status, xhr) {
                    loadDVDs();
                },
                error: function(xhr, status, err) {
                    alert('error:' + err);
                }
            });
        });
