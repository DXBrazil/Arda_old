// Functions with automatic initialization
$(function ($) {

    // Send the new account request to specific controller/action in Arda.Main.
    $("#loginform").submit(function (e) {
        e.preventDefault();

        $("#email").attr("disabled", "disabled");
        $("#password").attr("disabled", "disabled");
        $("#signin").attr("disabled", "disabled");

        $("#signin").text("Validating user data...");

        var email = $("#email").val();
        var password = $("#password").val();

        $.ajax({
            url: "http://localhost:2787/api/authentication/userauthentication",
            type: "POST",
            data: { Email: email, Password: password },
            dataType: "json"
        }).done(function (data) {
            if (data.Status == "Ok") {
                ClearModalForm();
                RedirectTo("http://localhost:2168/Dashboard/Index");
            }
            else if (data.Status == "Inactive")
            {
                $("#MessagePanelLogin").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Error!</strong> The requested user is here but is inactive. Please, consult the system admin.</div>");
                $("#signin").html("Sign in");
                ClearModalForm();
                $("#email").removeAttr("disabled");
                $("#password").removeAttr("disabled");
                $("#signin").removeAttr("disabled");
                
            }
            else
            {
                $("#MessagePanelLogin").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Ops!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
                $("#signin").html("Sign in");
                ClearModalForm();
                $("#email").removeAttr("disabled");
                $("#password").removeAttr("disabled");
                $("#signin").removeAttr("disabled");
            }
        }).fail(function (e, f) {
            $("#MessagePanelLogin").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Ops!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
            $("#signin").html("Sign in");
            ClearModalForm();
            $("#email").removeAttr("disabled");
            $("#password").removeAttr("disabled");
            $("#signin").removeAttr("disabled");
        });

    });

    // Send the new account request to specific controller/action in Arda.Main.
    $("#NewAccountRequest").submit(function (e) {
        e.preventDefault();

        $("#RequestAccountButton").attr("disabled", "disabled");
        $("#RequestAccountButton").text("Requesting...");

        var pName = $("#Name").val();
        var pEmail = $("#Email").val();
        var pPhone = $("#Phone").val();
        var pJustification = $("#Justification").val();

        $.ajax({
            url: "http://localhost:2787/api/accountoperations/requestnewaccount",
            type: "POST",
            data: { Name: pName, Email: pEmail, Phone: pPhone, Justification: pJustification },
            dataType: "json"
        }).done(function (data) {
            if (data.Status == "Ok") {
                $("#MessagePanel").html("<div class='alert alert-success'><strong>Success!</strong> Your request was sent. Thank you.</div>");
                $("#RequestAccountButton").removeAttr("disabled");
                $("#RequestAccountButton").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Request account");
                ClearModalForm();
            }
        }).fail(function (e, f) {
            $("#MessagePanel").html("<div class='alert alert-danger'><strong>Error!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
            $("#RequestAccountButton").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Request account");
        });
    
    });

    // Send the help request to specific controller/action in Arda.Main.
    $("#RadioGroup").submit(function (e) {
        e.preventDefault();

        $("#SendHelpRequest").attr("disabled", "disabled");
        $("#SendHelpRequest").text("Requesting...");

        var Value;

        if ($("input[name='radiooption']:checked").val() == "1") {
            Value = $("#YourCompleteName").val();
        }
        else if($("input[name='radiooption']:checked").val() == "2") {
            Value = $("#YourEmail").val();
        }
        else {
            Value = $("#YourDescription").val();
        }

        // Sending the help request
        $.ajax({
            url: "http://localhost:2787/api/accountoperations/requesthelp",
            type: "POST",
            data: { RequestType: Value },
            dataType: "json"
        }).done(function (data) {
            if (data.Status == "Ok") {
                $("#MessagePanel").html("<div class='alert alert-success'><strong>Success!</strong> Your help request was sent. Thank you.</div>");
                $("#SendHelpRequest").removeAttr("disabled");
                $("#SendHelpRequest").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Send help request");
                ClearModalForm();
            }
            else {
                $("#MessagePanel").html("<div class='alert alert-danger'><strong>Error!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
                $("#SendHelpRequest").removeAttr("disabled");
                $("#SendHelpRequest").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Send help request");
            }
        }).fail(function () {
            $("#MessagePanel").html("<div class='alert alert-danger'><strong>Error!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
            $("#RequestAccountButton").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Send help request");
        });
    });

    // Set background blue on the menu clicked item.
    //class="nav-item active"
});

// General functions

// Modal Login

function MountNeedHelpModal() {
    //Defining values
    var ModalTitle = "How can we help?";
    var ModalBody = "<p style='margin-bottom:20px;' class='p-modal-body'>What's happening? Please, select the best option below.</p>";
    ModalBody += "<p class='p-modal-body'>";
    ModalBody += "<div>";
    ModalBody += "<div class='radio'>";
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioEmail' value='1' onchange='CheckRadio();'>I'm having problems with my email (manual process)</label>";
    ModalBody += "</div>";
    ModalBody += "<div id='YourCompleteNameField'></div>"
    ModalBody += "<div class='radio'>";
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioPassword' value='2' onchange='CheckRadio();'>I'm having problems with my password (automatic process)</label>";
    ModalBody += "</div>";
    ModalBody += "<div id='YourEmailField'></div>"
    ModalBody += "<div class='radio'>";
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioAnother' value='3' onchange='CheckRadio();'>I'm having another kind of problem (manual process)</label>";
    ModalBody += "</div>";
    ModalBody += "<div id='DescriptionField'></div>"
    ModalBody += "</p>";
    ModalBody += "</div>";
    ModalBody += "<div id='MessagePanel'></div>";
    
    //Injecting contents
    $("#GenericModal2 .modal-title").html("<strong>" + ModalTitle + "</strong>");
    $("#GenericModal2 .modal-body").html("<strong>" + ModalBody + "</strong>");
    $("#GenericModal2 .modal-footer").html("<button type='submit' class='btn btn-success' id='SendHelpRequest' disabled='disabled'><span class='glyphicon glyphicon-ok'></span>&nbsp;Send help request</button>");
}

function MountRequestNewAccountModal() {
    var ModalTitle = "Request a new account";
    var ModalBody = "<p style='margin-bottom:20px;' class='p-modal-body'>In order to get a new system account please, fill all requested informations at form below and click in 'Request account'.</p>";
    
    ModalBody += "<p>";
            ModalBody += "<fieldset class='form-group'>";
            ModalBody += "<label for='Name'>Name</label>";
            ModalBody += "<input type='text' class='form-control' id='Name' placeholder='Your complete name' required>";
            ModalBody += "</fieldset>";
            ModalBody += "<fieldset class='form-group'>";
            ModalBody += "<label for='Email'>Email</label>";
            ModalBody += "<input type='email' class='form-control' id='Email' placeholder='alias@yourdomain.com' required>";
            ModalBody += "</fieldset>";
            ModalBody += "<fieldset class='form-group'>";
            ModalBody += "<label for='Phone'>Phone</label>";
            ModalBody += "<input type='tel' class='form-control' id='Phone' placeholder='(11) 01234-5678'>";
            ModalBody += "</fieldset>";
            ModalBody += "<fieldset class='form-group'>";
            ModalBody += "<label for='Justification'>Justification</label>";
            ModalBody += "<textarea class='form-control' id='Justification' placeholder='Tell us: why you need this account?' required></textarea>";
            ModalBody += "</fieldset>";
     ModalBody += "</p>";
     ModalBody += "<div id='MessagePanel'></div>";
    
    //Injecting contents
    $("#GenericModal .modal-title").html("<strong>" + ModalTitle + "</strong>");
    $("#GenericModal .modal-body").html(ModalBody);
    $("#GenericModal .modal-footer").html("<button type='submit' class='btn btn-success' id='RequestAccountButton'><span class='glyphicon glyphicon-ok'></span>&nbsp;Request account</button>");
}

// Another functions

function ClearModalForm() {
    $("form").trigger("reset");
}

function CheckRadio() {
    $("#SendHelpRequest").removeAttr("disabled");

    if ($("input[name='radiooption']:checked").val() == "1") {
        $("#YourCompleteNameField").html("<fieldset class='form-group'><input type='text' class='form-control' id='YourCompleteName' placeholder='Your complete name here' required></fieldset>");
        $("#YourEmailField").html("");
        $("#DescriptionField").html("");
    }
    else if ($("input[name='radiooption']:checked").val() == "2") {
        $("#YourEmailField").html("<fieldset class='form-group'><input type='text' class='form-control' id='YourEmail' placeholder='Your email here' required></fieldset>");
        $("#YourCompleteNameField").html("");
        $("#DescriptionField").html("");
    }
    else {
        $("#DescriptionField").html("<fieldset class='form-group'><textarea class='form-control' id='YourDescription' placeholder='Describes your problem here' required></textarea></fieldset>");
        $("#YourCompleteNameField").html("");
        $("#YourEmailField").html("");
    }
}

function RedirectTo(url)
{
    window.location = url;
}
