$(function () {
    $('#forget-form').validate({
        errorClass: "help-block",
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error");
        }
    });
});