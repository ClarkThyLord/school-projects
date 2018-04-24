<div class="dialog-menu" title="Modify Task" id="task_modify">
  <form class="content">
    <div name="task_id">

    </div>
    <div name="task_name">

    </div>
    <label>
      Clasificación: <br />
      <select class="task_input selectable" name="classification">
        <option value="Geo/Historia">Geo/Historia</option>
        <option value="Actual">Actual</option>
        <option value="Riesgo">Riesgo</option>
        <option value="Hechos Graciosos">Hechos Graciosos</option>
        <option value="Hard Facts">Hard Facts</option>
        <option value="M2GO ToDo">M2GO ToDo</option>
        <option value="Otro Tema.">Otro Tema.</option>
      <select>
    </label> <br />
    <div style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
      <label style="flex: 0.4;">
        Latitud: <br />
        <input type="number" placeholder="0.0" step="0.001" class="task_input" name="latitude" />
      </label>
      <label style="flex: 0.4;">
        Longitud: <br />
        <input type="number" placeholder="0.0" step="0.001" class="task_input" name="longitude" />
      </label> <br />
    </div>
    <div style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
      <label style="flex: 0.4;">
        Descripción: <br />
        <textarea placeholder="Descripcion...." style="width: 100%;" class="task_input selectable" name="summary"></textarea>
      </label>
      <label style="flex: 0.4;">
        URLs: <br />
        <textarea placeholder="Separar por comas'...." style="width: 100%;" class="task_input selectable" name="urls"></textarea>
      </label> <br />
    </div>
    <label>
      Archivos:
      <input type="file" style="display: none;" class="task_input" id="new_files" />
      <div class="dropzone selectable" id="task_file_dropzone">
        Dar click o arrastrar archivo..
      </div>
    </label>
    <div id="task_file_preview"></div>
    <fieldset class="bar">
      <input type="button" value="Submit Changes" onclick="let modified = getTaskData(); if (Object.keys(modified).length === 0) { return; } modifyTask(current_task, modified);" class="item" />
      <input type="button" value="Rename Task"  onclick="$('#task_rename').dialog('open');" class="item" /> |
      <input type="button" value="Remove Task"  onclick="$('#task_remove').dialog('open');" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Create Task" id="task_create">
  <form class="content">
    <label>
      Landmark:<br />
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
      Nuevo nombre de landmark:<br />
      <input type="text" placeholder="Task's new name..." name="task_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Task"  onclick='modifyTask(current_task, {name: task_name.value}); $(".task[data-task-id=\"" + current_task + "\"]").html(task_name.value);' class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove Task" id="task_remove">
  <form class="content">
    Eliminar landmark?
    <fieldset class="bar">
      <input type="button" value="Remove Task"  onclick="removeTask(current_table, current_task);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove File" id="file_remove">
  <form class="content">
    Eliminar archivo?
    <fieldset class="bar">
      <input type="button" value="Remove Task"  onclick="removeFile(current_file);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu" title="Preview File" id="file_preview">
  <img class="content file-preview" src=""> </img>
</div>
