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
<!--   <link rel="stylesheet" href="./css/common/gui.css"> -->
  <link rel="stylesheet" href="./css/login.css">

</head>

<body>
	<div class="container">
		<div class="card card-container">
<!-- 			<center><p class="brand">Bienvenidos</p></center> -->
			<img id="profile-img" class="profile-img-card" src="./m2go.png"/>

			<form class="form-signin">
				<input type="text" id="inputEmail" class="form-control" placeholder=" Usuario" name="username" />
				<input type="password" id="inputPassword" class="form-control" placeholder=" Contraseña" name="password" />
				<div id="error_msg">

				</div>
				<br />
				<input type="button" class="btn btn-lg btn-primary btn-block btn-signin" onclick="login(username.value, password.value);" value="Entrar"/>
				<input type="reset" class="btn btn-lg btn-primary btn-block btn-signin" value="Resetiar" />
			</form>
		</div>
	</div>

  <!-- Javascript -->
  <script src="./js/lib/jquery-3.3.1.min.js"></script>
  <script src="./js/common/gui.js"></script>
  <script src="./js/common/user.js"></script>

</body>

</html>
