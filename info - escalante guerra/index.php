<?php
	// Initialize session if not already
	if (!isset($_SESSION)) {
		session_start();
	}

	// Check if client is in a session
	if (isset($_SESSION['user'])) {
		header('Location: dashboard.php');
	}
?>
<!DOCTYPE html>
<html lang="en">

<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

	<title>EG - Ingresar</title>
	<link rel="icon" href="./assets/escalante_guerra_logo.jpg">

	<!-- CSS Libraries -->
	<link href="./css/libs/jquery-waitMe.min.css" rel="stylesheet">
	<link href="./css/libs/bootstrap.min.css" rel="stylesheet">

	<!-- Custom CSS -->
	<link href="./css/index.css" rel="stylesheet">
</head>

<body class="text-center">
	<form onsubmit="login(username_input.value, password_input.value); return false;" class="form-signin">
		<img class="mb-4" src="./assets/escalante_guerra_logo.jpg" alt="" width="125" height="125" />
		<h1 class="h3 mb-3 font-weight-normal">Por Favor, Ingresar</h1>

		<input type="text" onchange="try { setCustomValidity(''); } catch (e) {}" oninvalid="this.setCustomValidity('Por favor, inserte su nombre de usuario');" placeholder="Nombre de usuario..." required autofocus  class="form-control" id="username_input" />
		<input type="password" id="password_input" class="form-control" placeholder="Contraseña..." />

		<button class="btn btn-lg btn-primary btn-block" type="submit">Ingresar</button>
	</form>

	<!-- JS Libraries -->
	<script src="./js/libs/jquery-3.3.1.min.js"></script>
  <script src="./js/libs/jquery-waitMe.min.js"></script>

	<!-- Custom JS -->
	<script src="./js/common.js"></script>
</body>

</html>
