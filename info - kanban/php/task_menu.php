<div class="dialog-menu" title="Task Menu" id="Task_Menu">
</div>

<div class="dialog-menu-mini" title="Create Task" id="task_create">
  <form class="content">
    <label>
      Task's Name:<br />
      <input type="text" placeholder="Task's name..." name="task_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Create Task"  onclick="createTask(table_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu" title="Modify Task" id="task_modify">
  <form class="content">
    <label>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Task"  onclick="$('#task_rename').dialog('open');" class="item" /> |
      <input type="button" value="Modify Task"  onclick="modifyTask(current_task);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Create Task" id="task_rename">
  <form class="content">
    <label>
      Task's New Name:<br />
      <input type="text" placeholder="Task's new name..." name="task_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Task"  onclick="modifyTask(current_task, {name: task_name.value});" class="item" />
    </fieldset>
  </form>
</div>
