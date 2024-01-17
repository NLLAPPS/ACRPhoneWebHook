$(function () {
    var username = "";
    var password = "";

    function initialize() {
        $(".container").removeClass("hidden");

        var table = $("#recordings-table").DataTable({
            "ajax": {
                "url": "api/recording/all",
                "type": "POST",
                "data": {
                    "username": username,
                    "password": password
                },
                "dataSrc": ""
            },
            "columnDefs": [
                {
                    //1 is the second row which we put fiel name in the html table  https://datatables.net/reference/option/columnDefs.targets
                    targets: 1,
                    render: function (data, type, row, meta) {
                        if (type === 'display') {
                            return $('<a>')
                                .attr('href', '/uploads/'+ data)
                                .text(data)
                                .wrap('<div></div>')
                                .parent()
                                .html();

                        } else {
                            return data;
                        }
                    }
                }
            ],
            "columns": [
                { "data": "id" },
                { "data": "fileName", },
                { "data": "note" },
                { "data": "date" },
                { "data": "fileSize" },
                { "data": "duration" }
            ],
            "order": [[0, "desc"]]
        });
    }

    $("#login-form").submit(function (e) {
        e.preventDefault();

        var usernameInput = $("#username").val();
        var passwordInput = $("#password").val();
        var $loginMessage = $("#login-message");

        $.post("/api/login", { username: usernameInput, password: passwordInput },
            function (data) {
                username = usernameInput;
                password = passwordInput;

                $loginMessage.html("");
                $loginMessage.addClass("hidden");
                $("#login-modal").modal("hide");

                initialize();
            }
        ).fail(function () {
            $loginMessage.html("Wrong username or password");
            $loginMessage.removeClass("hidden");
        });
    });

    $("#login-modal").modal({
        backdrop: "static",
        keyboard: false,
        show: true
    });
});