<?php

	// Initialize session if not already
	if(!isset($_SESSION)) {
		session_start();
	}

  if(array_key_exists("user_data", $_SESSION) && !empty($_SESSION["user_data"])) {
    header("Location: index.php");
  }

?>

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

</head>

<body>

  <div class="title">
    Metropoli2Go
  </div>
  <form>
    <fieldset>
      <label>
        Username:
        <input type="text" placeholder="Username..." name="username" />
      </label>
      <br />
      <label>
        Password:
        <input type="password" placeholder="Password..." name="password" />
      </label>
      <div id="error_msg">

      </div>
    </fieldset>
    <input type="button" onclick="login(username.value, password.value);" value="Log-in" />
    <input type="reset" value="Reset" />
  </form>

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/common/gui.js"></script>
  <script src="./js/common/user.js"></script>

</body>

</html>
