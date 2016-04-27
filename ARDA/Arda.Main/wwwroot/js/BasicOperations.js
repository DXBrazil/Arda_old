//Functions with automatic initialization
$(function ($) {

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
    var ModalBody = "<p style='margin-bottom:20px;' class='p-modal-body'>Please, fill all requested informations at form below.</p>";
    
    //Injecting contents
    $("#GenericModal .modal-title").html("<strong>" + ModalTitle + "</strong>");
    $("#GenericModal .modal-body").html(ModalBody);
    $("#GenericModal .modal-footer").html("<button type='button' class='btn btn-success' data-dismiss='modal'><span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Save</button>");
}
