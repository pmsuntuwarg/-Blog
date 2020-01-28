$(function () {
    $('#reset-form').validate({
        errorClass: "help-block",
        rules: {
            Password: {
                required: true,
                password: true
            },
            ConfirmPassword: {
                required: true,
                password: true
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