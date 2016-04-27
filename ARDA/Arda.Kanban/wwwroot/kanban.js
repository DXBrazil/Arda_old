var _activeTaskList;
var _doneTaskList;

var KANBAN_ENDPOINT = 'http://localhost:5000/api/tasks/';

function httpGet(cmd, data, callback) {
    $.get(KANBAN_ENDPOINT + cmd, data, callback);
}

function httpPost(cmd, data, callback) {
    $.ajax({
        url: KANBAN_ENDPOINT + cmd,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (data) {
            callback(data);
        },
        error: function (result) {
            alert('Error');
        }
    });
}

function create(name) {
    
    httpPost('', { Name: name, Description: 'blabla', State: 0 }, function (data) {
        //var task = JSON.parse(data);
        var task = data;

        _activeTaskList.push(task);

        // render();
        var taskId = task.Id;
        var taskName = task.Name;
        createTaskInFolder(taskId, taskName, '.folder-active');

    });
}

function update(task, props) {
    //alert('API: update');
    props.id = task.id;

    // assume the update always works
    httpGet('update', props, function () {

        (props.name != null) && (task.name = props.name);

        if (props.status != null) {
            var srcList = (task.status == 0) ? _activeTaskList : _doneTaskList;
            var dstList = (props.status == 0) ? _activeTaskList : _doneTaskList;

            for (var deletePosition = 0; deletePosition < srcList.length; deletePosition++) {
                if (srcList[deletePosition].id == task.id)
                    break;
            }

            if (deletePosition < srcList.length) {
                srcList.splice(deletePosition, 1);
                dstList.push(task);
            }

            task.status = props.status;
        }

        // update React
        render();
    });

}

function tasklist(callback) {

    httpGet('', null, function (data) {
        callback(data);
    });

}

var tasks;

tasklist((dataReceived) => {
    tasks = dataReceived;

    _activeTaskList = tasks.filter((t) => { return t.State == 0 });
    _doneTaskList = tasks.filter((t) => { return t.State == 1 });
    
    // Render
    _activeTaskList.map((t) => {
        var taskId = t.Id;
        var taskName = t.Name;
        createTaskInFolder(taskId, taskName, '.folder-active')
    });

    _doneTaskList.map((t) => {
        var taskId = t.Id;
        var taskName = t.Name;
        createTaskInFolder(taskId, taskName, '.folder-done')
    });

});

