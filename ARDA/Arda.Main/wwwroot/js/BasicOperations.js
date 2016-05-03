// Functions with automatic initialization
$(function ($) {

    // Send the new account request to specific controller/action in Arda.Main.
    $("form").submit(function (e) {
        e.preventDefault();

        $("#RequestAccountButton").attr("disabled", "disabled");
        $("#RequestAccountButton").text("Requesting...");

        var pName = $("#Name").val();
        var pEmail = $("#Email").val();
        var pPhone = $("#Phone").val();
        var pJustification = $("#Justification").val();
        
        $.ajax({
            url: "/ClientAuthentication/RequestNewAccount",
            type: "POST",
            data: { Name: pName, Email: pEmail, Phone: pPhone, Justification: pJustification },
            success: function (data) {
                if (data.Status == "Ok") {
                    $("#MessagePanel").html("<div class='alert alert-success'><strong>Success!</strong> Your request was sent. Thank you.</div>");
                    $("#RequestAccountButton").removeAttr("disabled");
                    $("#RequestAccountButton").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Request account");
                    ClearModalForm();
                }
            },
            error: function () {
                $("#MessagePanel").html("<div class='alert alert-danger'><strong>Error!</strong> Something wrong happened with your request. Try again in few minutes.</div>");
                $("#RequestAccountButton").html("<span class='glyphicon glyphicon-ok'></span>&nbsp;Request account");
            }
        });
    
    });


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
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioEmail'>I'm having problems with my email</label>";
    ModalBody += "</div>";
    ModalBody += "<div class='radio'>";
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioPassword'>I'm having problems with my password</label>";
    ModalBody += "</div>";
    ModalBody += "<div class='radio'>";
    ModalBody += "<label><input type='radio' name='radiooption' id='RadioAnother'>I'm having another kind of problem</label>";
    ModalBody += "</div>";
    ModalBody += "</p>";
    ModalBody += "</div>";
    
    //Injecting contents
    $("#GenericModal .modal-title").html("<strong>" + ModalTitle + "</strong>");
    $("#GenericModal .modal-body").html("<strong>" + ModalBody + "</strong>");
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
