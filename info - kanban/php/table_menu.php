<div class="dialog-menu" title="Table Menu" id="Table_Menu">
</div>

<div class="dialog-menu-mini" title="Create Table" id="table_create">
  <form class="content">
    <label>
      Table's Name:<br />
      <input type="text" placeholder="Table's name..." name="table_name"/>
    </label><br />
    <label>
      Table's Content:<br />
      <input type="number" min="1" step="1" placeholder="1" name="table_content">
    </label>
    <fieldset class="bar">
      <input type="button" value="Create Table"  onclick="createTable(table_name.value, (parseInt(table_content) || 1));" class="item" />
    </fieldset>
  </form>
</div>
