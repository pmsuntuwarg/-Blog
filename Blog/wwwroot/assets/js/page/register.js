﻿$(function () {
    $('#register-form').validate({
        errorClass: "help-block",
        rules: {
            UserName: {
                required: true
            },
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
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