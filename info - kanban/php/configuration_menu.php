<div class="dialog-menu" title="Configuration Menu" id="Configuration_Menu">
  <div id="Configuration_Tabs">
    <ul>
      <li><a href="#Configuration_T_1">Users</a></li>
      <li><a href="#Configuration_T_2">Configuration</a></li>
      <li><a href="#Configuration_T_3">Log</a></li>
    </ul>
    <div id="Configuration_T_1">
      <div>
        <?php
          include_once "./php/DB.php";

          $sql = "SELECT * FROM `users`";

          $result = $GLOBALS["conn"]->query($sql);

          if ($result->num_rows > 0) {
            while($row = $result->fetch_assoc()) {
              echo '<div class="user" data-id="' . $row["id"] . '"> <span class="name">' . $row["name"] . '</span> <input type="button" onclick="$(`#user_rename`).dialog(`open`);" value="Rename" class="option" /> <input type="button" onclick="$(`#user_repassword`).dialog(`open`);" value="Change Password" class="option" /> <input type="button" onclick="$(`#user_reaccess`).dialog(`open`);" value="Change Access Level" class="option" /> <input type="button" onclick="$(`#user_remove`).dialog(`open`);" value="Remove" class="option" /> </div>';
            }
          }
        ?>
      </div>
      <fieldset class="bar">
        <input type="button" onclick="$(`#user_create`).dialog(`open`);" value="New User" class="item" /> |
        <input type="button" onclick="logout();" value="Sign-out" class="item" />
      </fieldset>
    </div>
    <div id="Configuration_T_2">
    </div>
    <div id="Configuration_T_3">
      <div id="system_log">
        <?php
          $sql = "SELECT * FROM `log` ORDER BY `date` DESC";

          $result = $GLOBALS["conn"]->query($sql);

          if ($result->num_rows > 0) {
            while($row = $result->fetch_assoc()) {
              echo "(" . $row["date"] . ") : " . $row["text"] . "<br />";
            }
          }

          $GLOBALS["conn"]->close();
        ?>
      </div>
      <fieldset class="bar">
        <input type="button" onclick="" value="Clear Log" class="item" />
      </fieldset>
    </div>
  </div>
</div>

<div class="dialog-menu" title="Create User" id="user_create">
  <form style="text-align: center;">
    <label>
      Username:<br />
      <input type="text" placeholder="Username..." />
    </label><br />
    <label>
      Password:<br />
      <input type="password" placeholder="Password..." />
    </label><br />
    <label>
      Access Level:
      (Viewer : 0,
      Worker : 1,
      Admin  : 2)
      <br />
      <input type="number" min="0" max="2" step="1" placeholder="1" />
    </label>
  </form>
  <fieldset class="bar">
    <input type="button" value="Create User"  onclick="" class="item" />
  </fieldset>
</div>

<div class="dialog-menu" title="Rename User" id="user_rename">
  <form style="text-align: center;">
    <label>
      New User Name:<br />
      <input type="text" placeholder="Username..." />
    </label>
  </form>
  <fieldset class="bar">
    <input type="button" value="Rename User"  onclick="" class="item" />
  </fieldset>
</div>

<div class="dialog-menu" title="Change User Password" id="user_repassword">
  <form style="text-align: center;">
    <label>
      New User Password:<br />
      <input type="password" placeholder="Password..." />
    </label>
  </form>
  <fieldset class="bar">
    <input type="button" value="Change User Password"  onclick="" class="item" />
  </fieldset>
</div>

<div class="dialog-menu" title="Change User Access Level" id="user_reaccess">
  <form style="text-align: center;">
    <label>
      New Access Level:
      (Viewer : 0,
      Worker : 1,
      Admin  : 2)
      <br />
      <input type="number" min="0" max="2" step="1" placeholder="1" />
    </label>
  </form>
  <fieldset class="bar">
    <input type="button" value="Change User Access Level"  onclick="" class="item" />
  </fieldset>
</div>

<div class="dialog-menu" title="Remove User" id="user_remove">
  <form style="text-align: center;">
    Remove User?
  </form>
  <fieldset class="bar">
    <input type="button" value="Delete User"  onclick="" class="item" />
  </fieldset>
</div>
