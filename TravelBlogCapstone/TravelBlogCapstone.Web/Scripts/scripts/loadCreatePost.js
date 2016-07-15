var uri = '/api/employee/GetCategories';

$(document)
    .ready(function () {
        loadCategories();
    });

function loadCategories() {
    $.getJSON(uri)
        .done(function (data) {
            $.each(data,
                function (index, category) {
                    $(addCategory(category)).appendTo($('#categories'));
                });
        });
};

function addCategory(category) {
    return '<option value=\"' + category.Id + '\">' + category.CategoryName + '</option>';
}