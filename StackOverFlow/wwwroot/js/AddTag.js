let count = 1;
$(() => {
    $("#add-tag").on('click', function () {
        $("#tag-div").append(`<br/><input name=tags[${count}]/>`);
        count++;
    })
})