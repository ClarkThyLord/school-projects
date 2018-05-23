<?php
	// Initialize session if not already
	if (!isset($_SESSION)) {
		session_start();
	}

	// Check if client is in a session
	if (!isset($_SESSION['user'])) {
		header('Location: index.php');
	}
?>
<!DOCTYPE html>
<html lang="es">

<head>
  <link rel="icon" href="./assets/images/m2go.png">
  <title>Metropoli2Go - Kanban</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- CSS -->
	<link rel="stylesheet" href="./css/libs/jquery-WaitMe.min.css">
	<link rel="stylesheet" href="./css/libs/bootstrap.min.css">
	<link rel="stylesheet" href="./css/libs/bootstrap-Switch.min.css">
  <link rel="stylesheet" href="./css/libs/dragula.min.css">

  <link rel="stylesheet" href="./css/kanban.css">
</head>

<body>

  <?php include "./php/settings.php" ?>
	<?php include "./php/sections.php" ?>
	<?php include "./php/landmarks.php" ?>

	<nav class="navbar navbar-expand-lg navbar-light bg-light">
		<a href="#" class="navbar-brand"><img src="./assets/images/m2go_full.png" style="max-width: 135px;"></a>
	  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbar" aria-controls="navbar" aria-expanded="false">
	    <span class="navbar-toggler-icon"></span>
	  </button>

	  <div class="collapse navbar-collapse" id="navbar">
	    <ul class="navbar-nav mr-auto">
				<!-- SECTIONS -->
	      <li class="nav-item">
	        <a class="nav-link" href="#">+ Agregar Sección</a>
	      </li>
	    </ul>

	    <div class="form-inline my-2 my-lg-0">
	      <input class="form-control mr-sm-2" type="text" placeholder="🔍 Buscar..." aria-label="🔍 Buscar...">
				<div class="btn-group mr-sm-2" role="group">
					<button onclick="" class="form-control btn btn-outline-secondary my-2 my-sm-0">↻ Refrescar</button>
					<button onclick="" class="form-control btn btn-outline-secondary my-2 my-sm-0">⇱ Exportar</button>
				</div>
				<div class="btn-group mr-sm-2" role="group">
		      <button onclick="$('#settings').modal('show');" class="form-control btn btn-outline-success my-2 my-sm-0" type="submit">Ajustes</button>
		      <button onclick="logout();" class="form-control btn btn-outline-success my-2 my-sm-0" type="submit">Cerrar Sesión</button>
				</div>
	    </div>
	  </div>
	</nav>

  <!-- Javascript -->
  <script src="./js/libs/jquery-3.3.1.min.js"></script>
  <script src="./js/libs/jquery-FileDrop.js"></script>
  <script src="./js/libs/jquery-WaitMe.min.js"></script>
  <script src="./js/libs/bootstrap.min.js"></script>
  <script src="./js/libs/vue.min.js"></script>
  <script src="./js/libs/dragula.min.js"></script>
  <script src="./js/libs/html2pdf.bundle.min.js"></script>

  <script src="./js/common.js"></script>
  <script src="./js/kanban.js"></script>

	<!-- VUE Templates -->
	<script type="text/x-template" id="table-component">
	  <table class="table table-striped table-hover table-sm">
	    <thead>
		    <tr>
		      <th v-for="(val, key) in columns" @click="val.order = (val.order === 'des') ? 'asc' : 'des'; sort_key = key;" style="cursor: pointer;" class="unselectable" :class="{ active: sort_key == key }">
						{{ key | capitalize }}
						<span v-if="key === sort_key && val.order === 'des'">↑</span>
						<span v-if="key === sort_key && val.order === 'asc'">↓</span>
		      </th>
		      <th v-if="more" class="unselectable">
						Información
		      </th>
		      <th v-if="modifiable || removable" class="unselectable">
						Acciónes
		      </th>
		    </tr>
	    </thead>
	    <tbody>
	      <tr v-for="entry in filtered_data" v-on:click="select($event, entry);">
	        <td v-for="(val, key) in columns">
	          <span v-if="key === 'Activo' && entry[val.referencing] === '0'">🔴</span>
	          <span v-else-if="key === 'Activo' && entry[val.referencing] === '1'">🔵</span>
						<span v-else>{{ entry[val.referencing] }}</span>
	        </td>
		      <th v-if="more" class="unselectable">
						<a href="#" v-on:click="information($event, entry);">Ver Más</a>
		      </th>
					<td v-if="modifiable || removable">
						<span v-if="modifiable" v-on:click="edit($event);" style="cursor: pointer;">✏️</span>
						<span v-if="removable" v-on:click="remove($event);" style="cursor: pointer;">🗑️</span>
					</td>
	      </tr>
  		</tbody>
		</table>
	</script>

</body>

</html>
