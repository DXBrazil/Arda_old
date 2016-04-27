var _activeTaskList;
var _doneTaskList;

var KANBAN_ENDPOINT = 'http://localhost:5000/api/tasks/';

function httpGet(cmd, data, callback) {
    $.get(KANBAN_ENDPOINT + cmd, data, callback);
}

function create(name) {
    httpGet('create', { name: name }, function (data) {
        var task = JSON.parse(data);

        _activeTaskList.push(task);
        render();

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

    return _tasks;
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

