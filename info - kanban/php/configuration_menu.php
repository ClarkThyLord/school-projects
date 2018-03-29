<div class="dialog-menu" title="Configuration Menu" id="Configuration_Menu">
  <div id="Configuration_Tabs">
    <ul>
      <li><a href="#Configuration_T_1">Users</a></li>
      <li><a href="#Configuration_T_2">Configuration</a></li>
      <li><a href="#Configuration_T_3">Log</a></li>
    </ul>
    <div id="Configuration_T_1">
      <div>
          <!-- TODO Users control panel -->
          <?php



          ?>
          <div class="user">
            <span class="name">
              User Name
            </span>
            <input type="button" value="Rename" class="option" />
            <input type="button" value="Change Password" class="option" />
            <input type="button" value="Change Access Level" class="option" />
            <input type="button" value="Remove" class="option" />
          </div>
      </div>
      <fieldset class="bar">
        <input type="button" onclick="logout();" value="Sign-out" class="item" />
      </fieldset>
    </div>
    <div id="Configuration_T_2">
    </div>
    <div id="Configuration_T_3">
      <div style="max-height: 50%;  overflow-x: hidden; overflow-y: auto; word-wrap: break-word;" id="system_log">
        (date) : Example of things being logged here! <br />
        (date) : Hello world! <br />
        (date) : How are you? <br />
        (date) : Good?! <br />
        (date) : I'm trying to get this work done! =D <br />
      </div>
    </div>
  </div>
</div>
