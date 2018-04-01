<div class="dialog-menu" title="Modify Task" id="task_modify">
  <form class="content">
    <div name="task_name">

    </div>
    <label>
      Classification: <br />
      <select class="selectable" name="classification">
        <option value="geo/historia">Geo/Historia</option>
        <option value="actual">Actual</option>
        <option value="riesgo">Riesgo</option>
        <option value="hechos graciosos">Hechos Graciosos</option>
        <option value="hard facts">Hard Facts</option>
        <option value="m2go todo">M2GO ToDo</option>
        <option value="otro tema">Otro Tema.</option>
      <select>
    </label> <br />
    <div style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
      <label style="flex: 0.4;">
        Latitude: <br />
        <input type="number" placeholder="0.0" step="0.001"/>
      </label>
      <label style="flex: 0.4;">
        Longitude: <br />
        <input type="number" placeholder="0.0" step="0.001"/>
      </label> <br />
    </div>
    <div style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
      <label style="flex: 0.4;">
        Summary: <br />
        <textarea placeholder="A text summary goes here...." style="width: 100%;" class="selectable" name="summary"></textarea>
      </label>
      <label style="flex: 0.4;">
        URLs: <br />
        <textarea placeholder="Seperate URLs via ','...." style="width: 100%;" class="selectable" name="urls"></textarea>
      </label> <br />
    </div>
    <div style="border-radius: 25px; border: dashed 5px var(--secondary);" class="selectable">
      Files: <br />
      Drag & Drop or <input type="file" style="display: none;" name="new_files" />
      <input type="button" onclick="new_files.click();" value="Browse files..." />
      <div name="set_files">

      </div>
    </div> <br />
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
