function dragstart(ev) {
    ev.dataTransfer.setData('text', ev.target.id);
}

function dragover(ev) {
    ev.preventDefault();
}

function drop(ev) {
    var target = this;
    ev.preventDefault();
    var data = ev.dataTransfer.getData('text');
    var elem = document.getElementById(data);
    target.appendChild(elem);

    var state = (target.dataset['state']);
    var numstate = state | 0;
    var task = { Id: elem.id, State: numstate };

    update(task);
}

//var tasks = $('.task');
//tasks.map(function (i, task) {
//    task.draggable = true;
//    task.addEventListener('dragstart', dragstart);
//});

var folders = $('.folder');
folders.map(function (i, folder) {
    folder.addEventListener('dragover', dragover);
    folder.addEventListener('drop', drop.bind(folder));
});

//var btnAdd = $('#btnAdd');
//var txtAdd = $('#txtAdd');

//btnAdd.click(function () {
//    var taskName = txtAdd.val();

//    if (taskName != null && taskName != '') {
//        create(taskName, function (id, name, state) {
//            createTask(id, name, state);
//        });
//    }
//});

gettasklist(function (tasklist) {
    tasklist.map(function (task) {
        createTask(task.data, task.value, task.status);
    });
});

function createTask(id, name, state) {
    var task_state = '.state' + state;
    createTaskInFolder(id, name, task_state);
}

function createTaskInFolder(taskId, taskName, folderSelector) {
    var content = document.querySelector('#templateTask').content;
    var clone = document.importNode(content, true);
    var folder = document.querySelector(folderSelector);

    clone.querySelector('.task').id = taskId;
    clone.querySelector('.task .templateText').textContent = taskName;

    clone.querySelector('.task').addEventListener('dragstart', dragstart);

    folder.appendChild(clone, true);
}

function httpCall(action, url, data, callback, error) {

    $.ajax({
        type: action, // GET POST PUT
        url: url,
        data: JSON.stringify(data),
        cache: false,
        contentType: 'application/json',
        dataType: 'json',
        success: callback,
        error: error,
        processData: false
    });

}

function gettasklist(callback) {
    httpCall('GET', '/Workload/ListWorkloadsByUser', null, callback);
}

//function create(taskname, callback) {
//    alert(2);
//    var task = { Id: null, Name: taskname, State: 0 };

//    httpCall('POST', 'api/tasks', task, function (data) {
//        data && callback(data.Id, data.Name, data.State);
//    })
//}

function update(task) {
    httpCall('PUT', '/Workload/UpdateStatus?id=' + task.Id + '&status=' + task.State, task, function (data) {
        // done
    })

}

$(function () {
    //Initialize:
    Initialize();

    //Load values:
    //Get All Activities:
    $.getJSON('/activity/GetActivities', null, callbackGetActivities);
    //Get User Technologies:
    $.getJSON('/technology/GetTechnologies', null, callbackGetTechnologies);
    //Get User Metrics:
    $.getJSON('/metric/GetMetrics', null, callbackGetMetrics);
    //Get User Users:
    $.getJSON('/users/GetUsers', null, callbackGetUsers);

    //TODO: Remove this test line:
    //$('#_WBID').val('fd6984f2-6483-4788-87c3-dbeef10a7d4e');
});

function Initialize() {
    //Click events:

    //New Workload:
    $('#btnNew').click(newWorkloadState);
    //Workload Details:
    $('#btnDetails').click(detailsWorkloadState);
    //Editing Workload:
    $('#btnWorkloadEdit').click(editWorkloadState);
    //Reset Button:
    $('#btnWorkloadReset').click(resetWorkloadForm);
    //Delete Button:
    $('#btnWorkloadDelete').click(deleteWorkload);


    //Other events:
    $('#WBComplexity').on('change', changeComplexity);

    //Components:
    $("#WBIsWorkload").bootstrapSwitch();

    $('#WBStartDate').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    });

    $('#WBEndDate').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    });

    $("#WBComplexity").ionRangeSlider({
        min: 1,
        max: 5,
        hide_min_max: true,
        hide_from_to: true,
        grid: false,
        keyboard: true
    });
}

function loadWorkload(workloadID) {
    $("#buttonsPanel").hide();
    //DisableWorkloadFields();

    $.ajax({
        url: '/Workload/GetWorkload?=' + workloadID,
        type: 'GET',
        processData: false,
        contentType: false,
        cache: false,
        success: function (data) {
            $('#WBID').val(data.WBID);
            //Dates
            formatDate(data.WBStartDate, function (str) {
                $('#WBStartDate').val(str);
            });
            formatDate(data.WBEndDate, function (str) {
                $('#WBEndDate').val(str);
            });

            var isWorkload = $('#WBIsWorkload')
            isWorkload.bootstrapSwitch('toggleDisabled', true, true);
            isWorkload.bootstrapSwitch('state', data.WBIsWorkload);
            isWorkload.bootstrapSwitch('toggleDisabled', true, true);

            $('#ModalTitle').append('Workload: ' + data.WBTitle); // setting modal title
            $('#WBTitle').val(data.WBTitle);
            $('#WBDescription').val(data.WBDescription);
            $('#WBExpertise').val(data.WBExpertise);
            $('#WBActivity').val(data.WBActivity);

            //Complexity
            var slider = $("#WBComplexity").data("ionRangeSlider");
            slider.update({
                from: data.WBComplexity,
                disable: true
            });
            var txt = '';
            switch (data.WBComplexity) {
                case 1:
                    txt = 'Very Low';
                    break;
                case 2:
                    txt = 'Low';
                    break;
                case 3:
                    txt = 'Medium';
                    break;
                case 4:
                    txt = 'High';
                    break;
                case 5:
                    txt = 'Very High';
                    break;
            }
            $('#ComplexityLevel').text(txt);

            //Multi-Select:
            $('#WBTechnologies').multiselect('select', data.WBTechnologies);
            $('#WBMetrics').multiselect('select', data.WBMetrics);
            $('#WBUsers').multiselect('select', data.WBUsers);

            //Files:
            var list = $('#filesList').html("");
            $(data.WBFilesList).each(function () {

                var div = $('<div id="' + this.Item1 + '">');
                var a = $('<a class="filePrev" FileID="' + this.Item1 + '" href=' + this.Item2 + '>').text(this.Item3);
                var remove = $('<a class="fileDel hidden" style="padding-left: 5px;"/>').text('(remove)');

                remove.click(function () {
                    $(this).parent().remove();
                });

                div.append(a);
                div.append(remove);
                list.append(div);
            });
        }
    });

}

function newWorkloadState() {
    //Clean values:
    resetWorkloadForm();
    //Set submit event:
    $('#form-workload').unbind();
    $('#form-workload').submit(addWorkload);

    //Modal Title:
    $('#ModalTitle').text('New Workload:');
    //Get GUID:
    getGUID(function (data) {
        $('#WBID').attr('value', data);
    });
    //Fields:
    $('#WBStartDate').removeAttr("disabled");
    $('#WBEndDate').removeAttr("disabled");
    if ($('#WBIsWorkload').bootstrapSwitch('disabled')) {
        $('#WBIsWorkload').bootstrapSwitch('toggleDisabled', true, true);
    }
    $('#WBTitle').removeAttr("disabled");
    $('#WBDescription').removeAttr("disabled");
    $('#WBExpertise').removeAttr("disabled");
    $('#WBActivity').removeAttr("disabled");
    var slider = $("#WBComplexity").data("ionRangeSlider");
    slider.update({
        from: 1,
        disable: false
    });
    $('.multiselect-container.dropdown-menu li a label input').removeAttr("disabled");
    $('.fileinput').removeClass('hidden');
    //Buttons:
    $('#btnWorkloadReset').removeClass('hidden');
    $('#btnWorkloadSend').removeClass('hidden');
    $('#btnWorkloadSend').text('Add');
    $('#btnWorkloadEdit').addClass('hidden');
    $('#btnWorkloadDelete').addClass('hidden');
}

function detailsWorkloadState() {
    resetWorkloadForm();
    //Modal Title:
    $('#ModalTitle').text('Workload Details:');
    //Set GUID:
    var guid = $('#_WBID').val();
    $('#WBID').attr('value', guid);
    //Load Workload:
    loadWorkload(guid);

    //Fields:
    $('#WBStartDate').attr("disabled", "disabled");
    $('#WBEndDate').attr("disabled", "disabled");
    if (!($('#WBIsWorkload').bootstrapSwitch('disabled'))) {
        $('#WBIsWorkload').bootstrapSwitch('toggleDisabled', true, true);
    }
    $('#WBTitle').attr("disabled", "disabled");
    $('#WBDescription').attr("disabled", "disabled");
    $('#WBExpertise').attr("disabled", "disabled");
    $('#WBActivity').attr("disabled", "disabled");
    var slider = $("#WBComplexity").data("ionRangeSlider");
    slider.update({
        from: 1,
        disable: true
    });
    $('.multiselect-container.dropdown-menu li a label input').attr("disabled", "disabled");
    $('.fileinput').addClass('hidden');
    //Buttons:
    $('#btnWorkloadReset').addClass('hidden');
    $('#btnWorkloadSend').addClass('hidden');
    $('#btnWorkloadEdit').removeClass('hidden');
    $('#btnWorkloadDelete').addClass('hidden');
}

function editWorkloadState() {
    //Set submit event:
    $('#form-workload').unbind();
    $('#form-workload').submit(updateWorkload);
    //Modal Title:
    $('#ModalTitle').text('Editing Workload:');
    //Fields:
    $('#WBStartDate').removeAttr("disabled");
    $('#WBEndDate').removeAttr("disabled");
    if ($('#WBIsWorkload').bootstrapSwitch('disabled')) {
        $('#WBIsWorkload').bootstrapSwitch('toggleDisabled', true, true);
    }
    $('#WBTitle').removeAttr("disabled");
    $('#WBDescription').removeAttr("disabled");
    $('#WBExpertise').removeAttr("disabled");
    $('#WBActivity').removeAttr("disabled");
    var slider = $("#WBComplexity").data("ionRangeSlider");
    slider.update({
        disable: false
    });
    $('.multiselect-container.dropdown-menu li a label input').removeAttr("disabled");
    $('.fileinput').removeClass('hidden');
    //Events:
    $('.fileDel').removeClass('hidden');
    //Buttons:
    $('#btnWorkloadReset').addClass('hidden');
    $('#btnWorkloadSend').text('Update');
    $('#btnWorkloadSend').removeClass('hidden');
    $('#btnWorkloadDelete').removeClass('hidden');
    $('#btnWorkloadEdit').addClass('hidden');
}

function resetWorkloadForm() {
    $('#msg').text('');
    $('#WBStartDate').val('');
    $('#WBEndDate').val('');
    if ($('#WBIsWorkload').bootstrapSwitch('disabled')) {
        $('#WBIsWorkload').bootstrapSwitch('toggleDisabled', true, true);
        $('#WBIsWorkload').bootstrapSwitch('state', true);
        $('#WBIsWorkload').bootstrapSwitch('toggleDisabled', true, true);
    } else {
        $('#WBIsWorkload').bootstrapSwitch('state', true);
    }

    $('#WBTitle').val('');
    $('#WBDescription').val('');
    $('#WBExpertise').val('-1');
    $('#WBActivity').val('-1');
    //Slider:
    var slider = $("#WBComplexity").data("ionRangeSlider");
    slider.update({
        from: 1
    });
    //Technologies Multiselect:
    var tech = [];
    $("#WBTechnologies option").each(function () {
        tech.push($(this).val());
    });
    $('#WBTechnologies').multiselect('deselect', tech);
    //Metrics Multiselect:
    var met = [];
    $("#WBMetrics option").each(function () {
        met.push($(this).val());
    });
    $('#WBMetrics').multiselect('deselect', met);
    //Users Multiselect:
    var users = [];
    $("#WBUsers option").each(function () {
        users.push($(this).val());
    });
    $('#WBUsers').multiselect('deselect', users);
    //Files:
    $('.fileinput').fileinput('clear');
    $('#filesList').html('');
}


function changeComplexity(e) {
    var value = $(this).val();
    var txt = '';
    switch (value) {
        case '1':
            txt = 'Very Low';
            break;
        case '2':
            txt = 'Low';
            break;
        case '3':
            txt = 'Medium';
            break;
        case '4':
            txt = 'High';
            break;
        case '5':
            txt = 'Very High';
            break;
    }
    $('#ComplexityLevel').text(txt);
}

function formatDate(dateStr, callback) {
    var date = new Date(dateStr);
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var str = month + '/' + day + '/' + year;
    callback(str)
}

function getGUID(callback) {
    $.ajax({
        url: 'Workload/GetGuid',
        type: 'GET',
        processData: false,
        contentType: false,
        cache: false,
        success: function (data) {
            callback(data);
        }
    });
}


function callbackGetActivities(data) {
    var options = [];
    options.push('<option selected disabled value="-1">Select the activity</option>');
    for (var i = 0; i < data.length; i++) {
        var text = data[i].ActivityName;
        var key = data[i].ActivityID;
        options.push('<option value="' + key + '">' + text + '</option>');
    }
    $('#WBActivity').html(options.join(''));
}

function callbackGetMetrics(data) {
    var options = [];
    var select = $('#WBMetrics');
    for (var i = 0; i < data.length; i++) {
        var text = '[' + data[i].MetricCategory + '] ' + data[i].MetricName;
        var key = data[i].MetricID;
        options.push('<option value="' + key + '">' + text + '</option>');
    }
    select.html(options.join(''));

    select.multiselect({
        buttonWidth: '100%',
        numberDisplayed: 1,
        nonSelectedText: 'Click to select the metrics'
    });
}

function callbackGetTechnologies(data) {
    var options = [];
    var select = $('#WBTechnologies');
    for (var i = 0; i < data.length; i++) {
        var text = data[i].TechnologyName;
        var key = data[i].TechnologyID;
        options.push('<option value="' + key + '">' + text + '</option>');
    }
    select.html(options.join(''));

    select.multiselect({
        buttonWidth: '100%',
        numberDisplayed: 2,
        nonSelectedText: 'Click to select the technologies'
    });
}

function callbackGetUsers(data) {
    var options = [];
    var select = $('#WBUsers');
    for (var i = 0; i < data.length; i++) {
        var text = data[i].Name;
        var key = data[i].UniqueName;
        options.push('<option value="' + key + '">' + text + '</option>');
    }
    select.html(options.join(''));

    select.multiselect({
        buttonWidth: '100%',
        numberDisplayed: 2,
        nonSelectedText: 'Click to select the users'
    });
}


function addWorkload(e) {
    //Gets bootstrap-switch component value:
    var value = $('#WBIsWorkload').bootstrapSwitch('state');
    //Serializes form and append bootstrap-switch value:
    var data = new FormData(this);
    data.append('WBIsWorkload', value)

    var workload = { id: this.WBID.value, name: this.WBTitle.value, state: 0 };

    DisableWorkloadFields();
    $('#msg').text('Wait...');
    $.ajax({
        url: 'Workload/Add',
        type: 'POST',
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {

            if (response.IsSuccessStatusCode) {
                $('#msg').text('Success!');

                // add a task (workload.js)
                createTask(workload.id, workload.name, workload.state);
            } else {
                $('#msg').text('Error!');
            }
        }
    });
    e.preventDefault();
}

function updateWorkload(e) {
    //Gets bootstrap-switch component value:
    var value = $('#WBIsWorkload').bootstrapSwitch('state');
    //Serializes form and append bootstrap-switch value:
    var data = new FormData(this);
    data.append('WBIsWorkload', value)
    //Append previous files:
    var files = $('#filesList div a.filePrev');
    for (var i = 0; i < files.length; i++) {

        data.append('oldFiles', files[i].attributes[1].value + '&' + files[i].href + '&' + files[i].text);
    }

    DisableWorkloadFields();
    $('#msg').text('Wait...');
    $.ajax({
        url: 'Workload/Update',
        type: 'PUT',
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.IsSuccessStatusCode) {
                $('#msg').text('Success!');
            } else {
                $('#msg').text('Error!');
            }
        }
    });
    e.preventDefault();
}

function deleteWorkload() {
    var workloadID = $('#WBID').val();
    $('#msg').text('Wait...');
    $.ajax({
        url: 'Workload/Delete?=' + workloadID,
        type: 'DELETE',
        success: function (response) {
            if (response.IsSuccessStatusCode) {
                $('#msg').text('Success!');
            } else {
                $('#msg').text('Error!');
            }
        }
    });
}