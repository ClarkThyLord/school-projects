<div class="dialog-menu-mini" title="Create Table" id="table_create">
  <form class="content">
    <label>
      Table's Name:<br />
      <input type="text" placeholder="Table's name..." name="table_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Create Table"  onclick="createTable(table_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Rename Table" id="table_rename">
  <form class="content">
    <label>
      Table's New Name:<br />
      <input type="text" placeholder="Table's new name..." name="table_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename Table"  onclick="modifyTable(parseInt(current_table), {name: table_name.value}); $('.table[data-table-id=\'' + current_table + '\'] > .header > .name').html(table_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove Table" id="table_remove">
  <form class="content">
    Remove Table?
    <fieldset class="bar">
      <input type="button" value="Remove Table"  onclick="removeTable(current_table);" class="item" />
    </fieldset>
  </form>
</div>
