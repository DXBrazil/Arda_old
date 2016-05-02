//Functions with automatic initialization
$(function ($) {

//Send the new account request to specific service in Arda.Authentication
$("#RequestAccountButton").click(function () {
        var Name = $("#Name").val();
        var Email = $("#Email").val();
        var Phone = $("#Phone").val();
        var Justification = $("#Justification").val();
        
        $.ajax({
            url: "http://localhost:49493/api/Values",
            type: "Post",
            data: JSON.stringify([name, address, dob]), //{ Name: name, 
                                              // Address: address, DOB: dob },
            contentType: 'application/json; charset=utf-8',
            success: function (data) { },
            error: function () { alert('error'); }
        });
    });

});

//General functions

//Modal Login

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
    
    //Injecting contents
    $("#GenericModal .modal-title").html("<strong>" + ModalTitle + "</strong>");
    $("#GenericModal .modal-body").html(ModalBody);
    $("#GenericModal .modal-footer").html("<button type='submit' class='btn btn-success' id='RequestAccountButton'><span class='glyphicon glyphicon-ok'></span>&nbsp;Request account</button>");
}
