<?php

	// Initialize session if not already
	if(!isset($_SESSION)) {
		session_start();
	}

  if(!array_key_exists("user_data", $_SESSION) || empty($_SESSION["user_data"])) {
    header("Location: login.php");
  }

?>

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

  <?php include "./php/configuration_menu.php" ?>
  <?php include "./php/print_table.php" ?>
  <?php include "./php/table_menu.php" ?>
  <?php include "./php/task_menu.php" ?>

	<div title="Vista Previa Del Mapa" id="map_preview">
	  <form class="content">
			<img src="./assets/map.jpg" />
	  </form>
	</div>

  <!-- Navigation Bar -->
  <div class="nav">
    <!-- Menu -->
    <div class="menu">
      <span class="title">
       <a href="index.php" ><img src="m2go_full.png" width="230px" height="37"></a>
      </span>
      <span class="item">
				<input type="button" onclick="$('#Configuration_Menu').dialog('open');" value="<?php echo $_SESSION["user_data"]["name"]; ?>" />
        <input type="button" onclick="logout();" value="Sign-out" />
      </span>
    </div>
    <!-- Toolbar -->
    <div class="toolbar">
      <label>
        Buscar Tabla:
        <input type="text" class="tool" placeholder="buscar..." oninput="seachForTable(this.value);" />
      </label>
      <label>
        Buscar landmark:
        <input type="text" class="tool" placeholder="buscar..." oninput="seachForTask(this.value);" />
      </label>
      <span class="tool selectable" onclick="$('#table_create').dialog('open');">
        Agregar nuevo +
      </span>
			<label class="tool selectable" onclick="$('#map_preview').dialog('open');">
				Mostrar Mapa
			</label>
			<label class="tool selectable" onclick="$('#print_table').dialog('open');">
				Imprimir Tabla
			</label>
    </div>
  </div>

  <!-- Workspace -->
  <div class="workspace">
    <!-- prev and next controls -->
    <a class="prev selectable" onmousedown="kanbanViewLeft();">
      &#10094;
    </a>
    <a class="next selectable" onmousedown="kanbanViewRight();">
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
            $msg .= '<div class="table" onclick="current_table = $(this).attr(\'data-table-id\');" data-table-id="' . $table["id"] . '"> <div class="header"> <span class="name selectable" onclick="$(\'#table_rename\').dialog(\'open\')">' . $table["name"] . '</span> <hr /> <span class="item selectable" onclick="$(\'#table_remove\').dialog(\'open\');"> &#128465; </span> <span class="item selectable" onclick="$(\'#task_create\').dialog(\'open\');"> Add + </span> </div> <div class="dragula-container" data-table-id="' . $table["id"] . '">';

            $tasks =  $GLOBALS["conn"]->query("SELECT * FROM `tasks` WHERE `table_id` = " . $table["id"]);
            if ($tasks->num_rows > 0) {
              while($task = $tasks->fetch_assoc()) {
                $msg .= '<div class="item task selectable" onclick="current_task = $(this).attr(\'data-task-id\'); getAndSetTask(current_task); $(\'#task_modify\').dialog(\'open\');" data-table-id="' . $table["id"] . '" data-task-id="' . $task["id"] . '">' . $task["name"] . '</div>';
              }
            }
            $msg .= "</div> </div>";
          }
        }

        echo $msg;

        $GLOBALS["conn"]->close();
      ?>
    </div>
  </div>

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/lib/jquery-FileDrop.js"></script>
  <script src="./js/lib/jquery-ui.min.js"></script>
  <script src="./js/lib/jquery-ui-touch-punch.min.js"></script>
  <script src="./js/lib/jquery-ExtendedDialogs.js"></script>
  <script src="./js/lib/dragula.min.js"></script>
  <script src="./js/lib/html2canvas.js"></script>
  <script src="./js/lib/jspdf.debug.js"></script>
  <script src="./js/common/gui.js"></script>
  <script src="./js/common/user.js"></script>
  <script src="./js/index.js"></script>

</body>

</html>
