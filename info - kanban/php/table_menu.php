<div class="dialog-menu-mini" title="Create Table" id="table_create">
  <form class="content">
    <label>
      Nombre de la tabla:<br />
      <input type="text" placeholder="Escibir nombre..." name="table_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Crear Tabla"  onclick="createTable(table_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Renombrar Tabla" id="table_rename">
  <form class="content">
    <label>
      Nuevo nombre de tabla:<br />
      <input type="text" placeholder="Escribir nuevo nombre..." name="table_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Table"  onclick="modifyTable(parseInt(current_table), {name: table_name.value}); $('.table[data-table-id=\'' + current_table + '\'] > .header > .name').html(table_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove Table" id="table_remove">
  <form class="content">
    Eliminar Tabla?
    <fieldset class="bar">
      <input type="button" value="Eliminar Table"  onclick="removeTable(current_table);" class="item" />
    </fieldset>
  </form>
</div>
