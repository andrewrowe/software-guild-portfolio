var uri = '/api/DVD/';

$(document)
    .ready(function() {
        loadDVDs();
    });

function loadDVDs() {
    $.getJSON(uri)
        .done(function(data) {
            $('#myDVDs tbody tr').remove();

            $.each(data,
                function(index, dvd) {
                    switch(dvd.MPAARating) {
                        case 1:
                            dvd.MPAARating = "G";
                            break;
                        case 2:
                            dvd.MPAARating = "PG";
                            break;
                        case 3:
                            dvd.MPAARating = "PG13";
                            break;
                        case 4:
                            dvd.MPAARating = "R";
                            break;
                        case 5:
                            dvd.MPAARating = "NC17";
                            break;
                    };

                    $(createRow(dvd)).appendTo($('#myDVDs tbody'));
                });
        });
};

function createRow(dvd) {
    return '<tr><td style="text-align: center"><img src=' +
        dvd.URL +
        'width="18" height="28"/></td>' +
        '<td style="text-align: center">' +
        dvd.Title +
        '</td><td style="text-align: center">' +
        dvd.ReleaseDate +
        '</td><td style="text-align: center">' +
        dvd.MPAARating +
        '</td><td style="text-align: center">' +
        dvd.Studio +
        '</td> <td style="text-align: center" id="rating">' +
        $.each(new Array(dvd.UserRating + 1),
            function () {
                //var parent = document.getElementById('rating');
                //img = new Image();
                //img.src = 'img src="~/Content/Images/star.png';
                //img.attributes = 'width="20" height="20"';
                //parent.appendChild(img);
            }) +
        '</td><td style="text-align: center"><button type="button" class="btn btn-info infoButton" id="btnView' +
        dvd.title +  '"value=' + dvd.title + '>View</button>' +
        '<button type="button" class="btn btn-info removeButton" id="btnRemove' +
        dvd.title + '">Remove</button>';
};

$("#myDVDs")
    .on("click",
        ".infoButton",
        dvd,
        function (dvd) {
            $.ajax({
                type: 'GET',
                url: "http://localhost/home/DVDInfo/" + dvd.title,
            });
        });