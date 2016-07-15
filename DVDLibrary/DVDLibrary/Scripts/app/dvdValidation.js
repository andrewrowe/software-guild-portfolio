$(document)
    .ready(function() {
        $('#dvdForm')
            .validate({
                rules: {
                    "DVD.Title": {
                        required: true,
                        maxlength: 50
                    },
                    "DVD.ReleaseDate": {
                        required: true,
                        date: true
                    },
                    "DVD.Studio": {
                        required: true,
                        maxlength: 30
                    },
                    "DVD.MPAARating": {
                        required: true,
                        range: [1, 5]
                    },
                    "Personnel[0].Name": {
                        required: true,
                        maxlength: 30
                    },
                    "Personnel[0].Role": {
                        required: true,
                        maxlength: 50
                    },
                    "DVD.UserRating": {
                        required: true
                    },
                    "DVD.URL": {
                        url: true
                    }
                },
                messages: {
                    "DVD.Title": "Enter a title",
                    "DVD.ReleaseDate": {
                        required: "Enter a release date",
                        date: "That's not a valid date format"
                    },
                    "DVD.Studio": {
                        required: "Enter a studio",
                        maxlength: "Cannot be more than 30 characters"
                    },
                    "DVD.MPAARating": "Enter the MPAA Rating",
                    "Personnel[0].Name" : {
                        required: "Enter a cast member's name",
                        maxlength: "Cannot be more than 30 characters"
                    },
                    "DVD.UserRating": "How do you rate this movie?",
                    "Personnel[0].Role": {
                        required: "Enter the cast member's role",
                        maxlength: "Cannot be more than 50 characters"
                    },
                    "DVD.URL": "Please provide a valid URL"
                }
            });
    }); 