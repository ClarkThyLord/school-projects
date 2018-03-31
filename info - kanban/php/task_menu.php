<div class="dialog-menu" title="Modify Task" id="task_modify">
  <form class="content">
    <label>
    </label>
    <fieldset class="bar">
      <input type="button" value="Submit Changes" onclick="" class="item" />
      <input type="button" value="Rename Task"  onclick="$('#task_rename').dialog('open');" class="item" /> |
      <input type="button" value="Remove Task"  onclick="$('#task_remove').dialog('open');" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Create Task" id="task_create">
  <form class="content">
    <label>
      Task's Name:<br />
      <input type="text" placeholder="Task's name..." name="task_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Create Task"  onclick="createTask(current_table, task_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Rename Task" id="task_rename">
  <form class="content">
    <label>
      Task's New Name:<br />
      <input type="text" placeholder="Task's new name..." name="task_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Task"  onclick='modifyTask(current_task, {name: task_name.value}); $(".task[data-task-id=\"" + current_task + "\"]").html(task_name.value);' class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove Task" id="task_remove">
  <form class="content">
    Remove task?
    <fieldset class="bar">
      <input type="button" value="Remove Task"  onclick="removeTask(current_table, current_task);" class="item" />
    </fieldset>
  </form>
</div>
