<!DOCTYPE html>
<html>

<head>

  <!-- Setup the icon for the page -->
  <link rel="icon" href="./assets/icon.png">

  <title>Metropoli2Go - Login</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- CSS -->
  <link rel="stylesheet" href="./css/common/gui.css">
  <link rel="stylesheet" href="./css/login.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui.min.css">
  <link rel="stylesheet" href="./css/lib/jquery-ui-theme.css">
  <link rel="stylesheet" href="./css/lib/dragula.min.css">

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/lib/jquery-ui.min.js"></script>
  <script src="./js/lib/jquery-ui-touch-punch.min.js"></script>
  <script src="./js/lib/dragula.min.js"></script>
  <script src="./js/common/gui.js"></script>

</head>

<body>

  <!-- TODO redirect to index if already logedin -->
  <?php



  ?>
  <div class="title">
    Metropoli2Go
  </div>
  <form>
    <fieldset>
      <label>
        Username:
        <input type="text" name="username" placeholder="Username..." />
      </label>
      <br />
      <label>
        Password:
        <input type="password" name="password" placeholder="Password..." />
      </label>
    </fieldset>
    <input type="submit" value="Log-in" />
    <input type="reset" value="Reset" />
  </form>

</body>

</html>
