<?php
	// Initialize session if not already
	if (!isset($_SESSION)) {
		session_start();
	}

	// Check if client is in a session
	if (isset($_SESSION['user'])) {
		header('Location: kanban.php');
	}
?>
<!DOCTYPE html>
<html lang="es">

<head>
  <link rel="icon" href="./assets/images/m2go.png">
  <title>Metropoli2Go - Iniciar Sesión</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- CSS -->
	<link rel="stylesheet" href="./css/libs/jquery-WaitMe.min.css">

  <link rel="stylesheet" href="./css/index.css">
</head>

<body>
	<div class="container">
		<div class="card card-container">
			<img id="profile-img" class="profile-img-card" src="./assets/images/m2go.png" />

			<form action="#" onsubmit="login(username.value, password.value); return false;" class="form-signin">
				<input type="text" id="inputEmail" class="form-control" placeholder="Usuario" name="username" />
				<input type="password" id="inputPassword" class="form-control" placeholder="Contraseña" name="password" />

				<br />

				<input type="button" class="btn btn-lg btn-primary btn-block btn-signin" onclick="login(username.value, password.value);" value="Entrar"/>
				<input type="reset" class="btn btn-lg btn-primary btn-block btn-signin" value="Resetiar" />
			</form>
		</div>
	</div>

  <!-- Javascript -->
  <script src="./js/libs/jquery-3.3.1.min.js"></script>
  <script src="./js/libs/jquery-WaitMe.min.js"></script>

  <script src="./js/common.js"></script>

</body>

</html>
