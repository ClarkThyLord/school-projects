<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

	<title>EG - Ingresar</title>
	<link rel="icon" href="./assets/escalante_guerra_logo.jpg">

	<!-- CSS Libraries -->
	<link href="./css/libs/bootstrap.min.css" rel="stylesheet">

	<!-- Custom CSS -->
	<link href="./css/index.css" rel="stylesheet">
</head>

<body class="text-center">
	<form onsubmit="login(inputEmail.value, inputPassword.value); return false;" class="form-signin">
		<img class="mb-4" src="./assets/escalante_guerra_logo.jpg" alt="" width="125" height="125">
		<h1 class="h3 mb-3 font-weight-normal">Por Favor, Ingresar</h1>

		<label for="inputEmail" class="sr-only">Insertar un correo electrónico válido</label>
		<input type="email" id="inputEmail" class="form-control" placeholder="Correo electrónico..." required autofocus>

		<label for="inputPassword" class="sr-only">Insertar contraseña válida</label>
		<input type="password" id="inputPassword" class="form-control" placeholder="Contraseña..." required>

		<button class="btn btn-lg btn-primary btn-block" type="submit">Ingresar</button>
	</form>

	<!-- JS Libraries -->
	<script src="./js/libs/jquery-3.3.1.min.js"></script>

	<!-- Custom JS -->
	<script src="./js/common.js"></script>
</body>

</html>
