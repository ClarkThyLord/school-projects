<!DOCTYPE html>
<html>

<head>

  <!-- Setup the icon for the page -->
  <link rel="icon" href="./assets/icon.png">

  <title>Metropoli2Go</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- CSS -->
  <link rel="stylesheet" href="./css/common/gui.css">
  <link rel="stylesheet" href="./css/index.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui.min.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui-theme.css">
  <link rel="stylesheet" href="./css/lib/dragula.min.css">

</head>

<body>

  <?php

    // Initialize session if not already
    if(!isset($_SESSION)) {
      session_start();
    }

    if(!array_key_exists("user_data", $_SESSION) || empty($_SESSION["user_data"])) {
      header("Location: login");
    }

  ?>
  <?php include "./php/configuration_menu.php" ?>
  <?php include "./php/category_menu.php" ?>
  <?php include "./php/task_menu.php" ?>

  <!-- Navigation Bar -->
  <div class="nav">
    <!-- Menu -->
    <div class="menu">
      <span class="title">
        Metropoli2Go
      </span>
      <span class="user selectable" onclick="$('#Configuration_Menu').dialog('open');" id="user">
        <?php

          // Initialize session if not already
          if(!isset($_SESSION)) {
            session_start();
          }

          echo $_SESSION["user_data"]["name"];

        ?>
      </span>
    </div>
    <!-- Toolbar -->
    <div class="toolbar">
      <label>
        Search:
        <input type="text" class="tool" placeholder="Search term..." />
      </label>
      <span class="tool selectable" onclick="$('#Task_Menu').dialog('open');">
        &#9881;
      </span>
      <span class="tool selectable">
        Add +
      </span>
    </div>
  </div>

  <!-- Workspace -->
  <div class="workspace">
    <!-- prev and next controls -->
    <a class="prev selectable">
      &#10094;
    </a>
    <a class="next selectable">
      &#10095;
    </a>
    <!-- Kanban -->
    <div class="kanban">
      <?php
        include_once "./php/DB.php";

        $msg = "";
        $tables = $GLOBALS["conn"]->query("SELECT * FROM `tables`");
        if ($tables->num_rows > 0) {
          while($table = $tables->fetch_assoc()) {
            $msg .= '<div class="table" data-id="' . $table["id"] . '"> <div class="header"> <span class="name">' . $table["name"] . '</span> <hr /> <label> Search: <br /> <input type="text" placeholder="Search term..." data-table="' . $table["id"] . '" onchange="" /> </label> </div> <form class="container" data-table="' . $table["id"] . '">';

            $tasks =  $GLOBALS["conn"]->query("SELECT * FROM `tasks` WHERE `table_id` = " . $table["id"]);
            if ($tasks->num_rows > 0) {
              while($task = $tasks->fetch_assoc()) {
                $msg .= '<div class="item selectable" onclick="" data-table="' . $table["id"] . '" name="' . $task["id"] . '">' . $task["name"] . '</div>';
              }
            }
            $msg .= "</form> </div>";
          }
        }

        echo $msg;

        $GLOBALS["conn"]->close();
      ?>
    </div>
  </div>

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/lib/jquery-ui.min.js"></script>
  <script src="./js/lib/jquery-ui-touch-punch.min.js"></script>
  <script src="./js/lib/jquery-ExtendedDialogs.js"></script>
  <script src="./js/lib/dragula.min.js"></script>
  <script src="./js/common/gui.js"></script>
  <script src="./js/common/user.js"></script>
  <script src="./js/index.js"></script>

</body>

</html>