var templateUrl = '/feedback/template.html?user=fab&redirect=/ok.html';

function openForm() {
    feedbackForm.src = templateUrl;
    feedbackForm.style.display = 'block'
}
function closeForm() {
    feedbackForm.src = '';
    feedbackForm.style.display = 'none';
}
function checkForm(url) {
    var isTemplate = url.indexOf(templateUrl) >= 0;
    var isValidPage = url.startsWith('http');

    alert(url)

    if (isValidPage && (!isTemplate)) {
        closeForm();
    }
}

