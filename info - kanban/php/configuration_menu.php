<div class="dialog-menu" title="Configuration Menu" id="Configuration_Menu">
  <div class="ui-tabs" id="Configuration_Tabs">
    <ul>
      <li><a href="#Configuration_T_1">Users</a></li>
      <li><a href="#Configuration_T_2">Configuration</a></li>
      <li><a href="#Configuration_T_3">Log</a></li>
    </ul>
    <div id="Configuration_T_1">
      <div class="users">
        <?php
          // Initialize session if not already
          if(!isset($_SESSION)) {
            session_start();
          }

          include_once "./php/DB.php";

          $sql = "SELECT * FROM `users`";

          $result = $GLOBALS["conn"]->query($sql);

          if ($result->num_rows > 0) {
            while($row = $result->fetch_assoc()) {
              $icon = "";
              if ($row["id"] == $_SESSION["user_data"]["id"]) {
                $icon = "&#9733; ";
              }
              echo '<div class="user" onclick="current_user = $(this).attr(\'data-id\');" data-id="' . $row["id"] . '"><span class="header">' . $icon . '<span class="name">' . $row["name"] . '</span></span> <input type="button" onclick="$(\'#user_rename\').dialog(\'open\');" value="Rename" class="option" /> <input type="button" onclick="$(\'#user_repassword\').dialog(\'open\'); " value="Change Password" class="option" /> <input type="button" onclick="$(\'#user_reaccess\').dialog(\'open\');" value="Change Access Level" class="option" /> <input type="button" onclick="$(\'#user_remove\').dialog(\'open\'); " value="Remove" class="option" /> </div>';
            }
          }
        ?>
      </div>
      <fieldset class="bar">
        <label class="item">
          Search For User:
          <input type="text" oninput="seachForUser(this.value);" />
        </label>
        <input type="button" onclick="$('#user_create').dialog('open');" value="New User" class="item" /> |
        <input type="button" onclick="logout();" value="Sign-out" class="item" />
      </fieldset>
    </div>
    <div id="Configuration_T_2">
    </div>
    <div id="Configuration_T_3">
      <div class="log">
        <?php
          $result = $GLOBALS["conn"]->query("SELECT logs.id, logs.date, users.name, logs.msg FROM logs INNER JOIN users ON logs.user = users.id ORDER BY `date` DESC");

          if ($result->num_rows > 0) {
            while($log = $result->fetch_assoc()) {
              echo "<span class='msg' data-log-msg-id='" . $log["id"] . "'>(" . $log["date"] . ") : " . $log["name"] . " : " . $log["msg"] . "<br /></span>";
            }
          }
        ?>
      </div>
      <fieldset class="bar">
        <label class="item">
          Search For Msg:
          <input type="text" oninput="searchInLog(this.value);" />
        </label> |
        <input type="button" onclick="$('#log_clear').dialog('open');" value="Clear Log" class="item" />
      </fieldset>
    </div>
  </div>
</div>

<div class="dialog-menu-mini" title="Clear Log" id="log_clear">
  <form class="content">
    Clear log?
    <fieldset class="bar">
      <input type="button" value="Clear Log"  onclick="clearLog();" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Create User" id="user_create">
  <form class="content">
    <label>
      Username:<br />
      <input type="text" placeholder="Username..." name="user_name" />
    </label><br />
    <label>
      Password:<br />
      <input type="password" placeholder="Password..." name="user_password"/>
    </label><br />
    <label>
      Access Level:
      (Viewer : 0,
      Worker : 1,
      Admin  : 2)
      <br />
      <input type="number" min="0" max="2" step="1" placeholder="0" name="user_access" />
    </label>
    <fieldset class="bar">
      <input type="button" value="Create User"  onclick="createUser(user_name.value, user_password.value, (parseInt(user_access.value) || 0));" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Rename User" id="user_rename">
  <form class="content">
    <label>
      New User Name:<br />
      <input type="text" placeholder="Username..." name="new_name"/>
    </label>
    <fieldset class="bar">
      <input type="button" value="Rename User"  onclick="modifyUser(current_user, {'name' : new_name.value}); $('.user[data-id=\'' + current_user + '\'] > .header > .name').html(new_name.value);" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Change User Password" id="user_repassword">
  <form class="content">
    <label>
      New User Password:<br />
      <input type="password" placeholder="Password..." name="new_password" />
    </label>
    <fieldset class="bar">
      <input type="button" value="Change User Password"  onclick="modifyUser(current_user, {'password' : new_password.value});" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Change User Access Level" id="user_reaccess">
  <form class="content">
    <label>
      New Access Level:
      (Viewer : 0,
      Worker : 1,
      Admin  : 2)
      <br />
      <input type="number" min="0" max="2" step="1" placeholder="0" name="new_access" />
    </label>
    <fieldset class="bar">
      <input type="button" value="Change User Access Level"  onclick="modifyUser(current_user, {'access' : (new_access.value || 0)});" class="item" />
    </fieldset>
  </form>
</div>

<div class="dialog-menu-mini" title="Remove User" id="user_remove">
  <form class="content">
    Remove User?
    <fieldset class="bar">
      <input type="button" value="Remove User"  onclick="removeUser(current_user);" class="item" />
    </fieldset>
  </form>
</div>
