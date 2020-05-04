$(() => {
    setInterval(() => {
        var Id = $("#QuestionId").val();
        $.get(`/Home/GetLikes?Id=${Id}`, function (likes) {
            console.log(likes)
            $("#likes").text(likes.number);
        })
    }, 1000)

    $("#button").on('click', function () {
        var Id = $("#QuestionId").val();
        $.post(`/Home/AddLikes?QuestionId=${Id}`, function (p) {
            $(".button").prop("disabled", true);
        })
    })
})