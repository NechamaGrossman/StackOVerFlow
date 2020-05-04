var count = 1;
$(() => {
    $("#add-tag").on('click', function () {
        console.log('Here in the add tag')
        $("#tag-div").append(`<br/><input name=qvm.Tags[${count}]/>`);
        count++;
    })
})