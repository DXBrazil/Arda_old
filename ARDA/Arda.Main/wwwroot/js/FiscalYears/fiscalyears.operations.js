(function ($) {

    $("#form-edit-fiscal-year").submit(function (e) {
        e.preventDefault();

        DisableFiscalYearFields();

        $("#btnUpdate").text("Updating fiscal year data...");

        var form = S("#form-edit-fiscal-year");

        $.ajax({
            url: "/FiscalYear/EditFiscalYear",
            type: "POST",
            data: form.serialize()
        }).done(function (data) {
            if (data.Status == "Ok") {
                ClearModalForm();
                $("#message").html("<div class='alert alert-success'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Success!</strong>The fiscal year was updated succefully.</div>");
                $("#btnUpdate").html("<i class='fa fa-floppy-o' aria-hidden='true'></i> Save");
                EnableFiscalYearFields();
                RedirectIn(5000, "/FiscalYear/Index");
            }
            else {
                $("#message").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Ops!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
                $("#btnUpdate").html("<i class='fa fa-floppy-o' aria-hidden='true'></i> Save");
                EnableFiscalYearFields();
            }
        }).fail(function (e, f) {
            $("#message").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Ops!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
            $("#btnUpdate").html("<i class='fa fa-floppy-o' aria-hidden='true'></i> Save");
            EnableFiscalYearFields();
        });

    });
});

function DisableFiscalYearFields()
{
    $("#FiscalYearID").attr("disabled", "disabled");
    $("#TextualFiscalYearMain").attr("disabled", "disabled");
    $("#FullNumericFiscalYearMain").attr("disabled", "disabled");
    $("#btnUpdate").attr("disabled", "disabled");
}

function EnableFiscalYearFields()
{
    $("#FiscalYearID").removeAttr("disabled");
    $("#TextualFiscalYearMain").removeAttr("disabled");
    $("#FullNumericFiscalYearMain").removeAttr("disabled");
    $("#btnUpdate").removeAttr("disabled", "disabled");
}

function RedirectIn(delay, url)
{
    setTimeout(function () {
        window.location = url;
    }, delay);
}