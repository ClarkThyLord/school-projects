<div class="dialog-menu" title="Modify Task" id="task_modify">
  <form class="content">
    <div name="task_name">

    </div>
    <label>
      Classification: <br />
      <select class="task_input selectable" name="classification">
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
        <input type="number" placeholder="0.0" step="0.001" class="task_input" name="latitude" />
      </label>
      <label style="flex: 0.4;">
        Longitude: <br />
        <input type="number" placeholder="0.0" step="0.001" class="task_input" name="longitude" />
      </label> <br />
    </div>
    <div style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
      <label style="flex: 0.4;">
        Summary: <br />
        <textarea placeholder="A text summary goes here...." style="width: 100%;" class="task_input selectable" name="summary"></textarea>
      </label>
      <label style="flex: 0.4;">
        URLs: <br />
        <textarea placeholder="Seperate URLs via ','...." style="width: 100%;" class="task_input selectable" name="urls"></textarea>
      </label> <br />
    </div>
    <label>
      Files:
      <input type="file" style="display: none;" class="task_input" name="new_files" />
      <div class="dropzone selectable" id="task_file_dropzone">
        Drop Files Here or Click To Upload
      </div>
    </label>
    <div id="task_file_preview"></div>
    <fieldset class="bar">
      <input type="button" value="Submit Changes" onclick="modifyTask(current_task, {classification : classification.value, latitude: latitude.value, longitude: longitude.value, summary: summary.value, urls: urls.value})" class="item" />
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

<div class="dialog-menu-mini" title="Remove File" id="file_remove">
  <form class="content">
    Remove file?
    <fieldset class="bar">
      <input type="button" value="Remove Task"  onclick="removeFile(current_file);" class="item" />
    </fieldset>
  </form>
</div>
