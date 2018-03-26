<!DOCTYPE html>
<html>

<head>

  <!-- Setup the icon for the page -->
  <link rel="icon" href="./assets/icon.png">

  <title>Potential - Home</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- CSS -->
  <link rel="stylesheet" href="./css/gui.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui.min.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui-theme.css">

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/lib/jquery-ui.min.js"></script>
  <script src="./js/lib/jquery-ui-touch-punch.min.js"></script>
  <script src="./js/gui.js"></script>

</head>

<body>

  <?php include "./php/login_menu.php" ?>
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
      <span class="user selectable" id="user">
        ClarkThyLord
      </span>
    </div>
    <!-- Categories -->
    <div class="categories">
      <a class="prev selectable">
        &#10094;
      </a>
      <a class="next selectable">
        &#10095;
      </a>
      <div class="category selectable choosen">
        A
      </div>
      <div class="category selectable">
        B
      </div>
      <div class="category selectable">
        C
      </div>
      <div class="category selectable">
        ...
      </div>
      <div class="category selectable">
        &#9881;
      </div>
      <div class="category selectable">
        Add +
      </div>
    </div>
  </div>

  <!-- Workspace -->
  <div class="workspace">
    <!-- Toolbar -->
    <div class="toolbar">
      <label>
        Search:
        <input type="text" placeholder="Search term..." />
      </label>
      <label>
        Filter:
        <input type="text" placeholder="Filter by..." />
      </label>
      <span class="tool selectable">
        &#9881;
      </span>
      <span class="tool selectable">
        Add +
      </span>
    </div>

    <!-- Kanban -->
    <div class="kanban">

    </div>
  </div>

</body>

</html>
