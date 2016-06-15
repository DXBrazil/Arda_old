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

var tasks = $('.task');
tasks.map(function (i, task) {
    task.draggable = true;
    task.addEventListener('dragstart', dragstart);
});

var folders = $('.folder');
folders.map(function (i, folder) {
    folder.addEventListener('dragover', dragover);
    folder.addEventListener('drop', drop.bind(folder));
});

var btnAdd = $('#btnAdd');
var txtAdd = $('#txtAdd');

btnAdd.click(function () {
    var taskName = txtAdd.val();

    if (taskName != null && taskName != '') {
        create(taskName, function (id, name, state) {
            createTask(id, name, state);
        });
    }
});

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

function create(taskname, callback) {
    alert(2);
    var task = { Id: null, Name: taskname, State: 0 };

    httpCall('POST', 'api/tasks', task, function (data) {
        data && callback(data.Id, data.Name, data.State);
    })


}

function update(task) {
    alert(3);
    httpCall('PUT', 'api/tasks', task, function (data) {
        alert(1)
    })

}