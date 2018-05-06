<?php
	// Initialize session if not already
	if (!isset($_SESSION)) {
		session_start();
	}

	// Check if client isn't in a session
	if (!isset($_SESSION['user'])) {
		header('Location: index.php');
	}
?>
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

  <title>EG - Tablero</title>
  <link rel="icon" href="./assets/escalante_guerra_logo.jpg">

	<!-- CSS Libraries -->
	<link href="./css/libs/bootstrap.min.css" rel="stylesheet">

	<!-- Custom CSS -->
	<link href="./css/dashboard.css" rel="stylesheet">
</head>

<body>
	<!-- MENU BAR -->
  <nav class="navbar navbar-dark fixed-top bg-dark flex-md-nowrap p-0 shadow">
    <a onclick="content_change('desk');" class="navbar-brand col-sm-3 col-md-2 mr-0" href="#">Escalante Guerra</a>
    <input class="form-control form-control-dark w-100" type="text" placeholder="Buscar... (e.j. puestos, candidatos, requisiciones y registros)" aria-label="Buscar">
    <ul class="navbar-nav px-3">
      <li onclick="logout();" class="nav-item text-nowrap">
        <a class="nav-link" href="#">Cerrar Sesión</a>
      </li>
    </ul>
  </nav>

  <div class="container-fluid">
    <div class="row">
			<!-- MENU -->
      <nav class="col-md-2 d-none d-md-block bg-light sidebar">
        <div class="sidebar-sticky">
          <ul class="nav flex-column">
            <li data-location="desk" onclick="content_change('desk');" class="nav-item border-bottom">
              <a href="#" class="nav-link active">
                Escritorio
              </a>
            </li>
            <li data-location="desk" onclick="content_change('search');" style="display: none;" class="nav-item border-bottom">
              <a href="#" class="nav-link active">
                Búsqueda
              </a>
            </li>
            <li data-location="jobs" onclick="content_change('jobs');" class="nav-item">
              <a href="#" class="nav-link">
                Puestos
              </a>
            </li>
            <li data-location="requisitions" onclick="content_change('requisitions');" class="nav-item">
              <a href="#" class="nav-link">
                Requisiciones
              </a>
            </li>
            <li data-location="candidates" onclick="content_change('candidates');" class="nav-item">
              <a href="#" class="nav-link">
                Candidatos
              </a>
            </li>
            <li data-location="logs" onclick="content_change('logs');" class="nav-item border-bottom">
              <a href="#" class="nav-link">
                Registros
              </a>
            </li>
            <li data-location="users" onclick="content_change('users');" class="nav-item">
              <a href="#" class="nav-link">
                Usuarios
              </a>
            </li>
          </ul>
        </div>
      </nav>

			<!-- CONTENT -->
			<!-- RECENT -->
      <main role="main" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="desk">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Escritorio</h1>
        </div>

        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Puestos Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('jobs');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Publicado</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Requisiciones Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('requisitions');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Candidato</th>
                <th>Correo</th>
                <th>Puesto</th>
                <th>Archivo de CV</th>
								<th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Candidatos Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('candidates');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Empresa</th>
                <th>Teléfono</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Registros Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('logs');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>Fecha y Hora</th>
                <th>Responsable</th>
                <th>Movimiento</th>
                <th>Identificador</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- SEARCH -->
      <main data-search_term="" role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="search">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Búsqueda</h1>
        </div>

        <h2>Puestos Coincidentes</h2>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Publicado</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

        <h2>Requisiciones Coincidentes</h2>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Candidato</th>
                <th>Correo</th>
                <th>Puesto</th>
                <th>Archivo de CV</th>
								<th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

        <h2>Candidatos Coincidentes</h2>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Empresa</th>
                <th>Teléfono</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>

        <h2>Registros Coincidentes</h2>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>Fecha y Hora</th>
                <th>Responsable</th>
                <th>Movimiento</th>
                <th>Identificador</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- JOBS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="jobs">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Puestos</h1>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Publicado</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- REQUISITIONS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="requisitions">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Requisiciones</h1>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Candidato</th>
                <th>Correo</th>
                <th>Puesto</th>
                <th>Archivo de CV</th>
								<th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- CANDIDATES -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="candidates">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Candidatos</h1>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>ID.</th>
                <th>Creado</th>
                <th>Empresa</th>
                <th>Teléfono</th>
                <th>Puesto</th>
                <th>Activo</th>
                <th>Acciónes</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- LOGS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="logs">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Registros</h1>
					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button class="btn btn-sm btn-outline-secondary">Borrar Registros</button>
            </div>
          </div>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-sm">
            <thead>
              <tr>
                <th>Fecha y Hora</th>
                <th>Responsable</th>
                <th>Movimiento</th>
                <th>Identificador</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div>
      </main>

			<!-- USERS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="users">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Usuarios</h1>
        </div>
        <div class="table-responsive" id="all_users_table">
				  <table-component :search_term="search_term" :visual_columns="visual_columns" :real_columns="real_columns" :data="data"></table-component>
        </div>
      </main>
    </div>
  </div>

  <!-- JS Libraries -->
  <script src="./js/libs/jquery-3.3.1.min.js"></script>
  <script src="./js/libs/bootstrap.min.js"></script>
  <script src="./js/libs/vue.min.js"></script>

	<!-- VUE Templates -->
	<script type="text/x-template" id="table-component">
	  <table class="table table-striped table-sm">
	    <thead>
		    <tr>
		      <th v-for="key in visual_columns" @click="sortBy(key)" :class="{ active: sortKey == key }">
						{{ key | capitalize }}
		        <span class="arrow" :class="sortOrders[key] > 0 ? 'asc' : 'dsc'"></span>
		      </th>
		    </tr>
	    </thead>
	    <tbody>
	      <tr v-for="entry in filteredData">
	        <td v-for="key in real_columns">
	          {{ entry[key] }}
	        </td>
	      </tr>
  		</tbody>
		</table>
	</script>

  <!-- Custom JS -->
  <script src="./js/common.js"></script>
  <script src="./js/dashboard.js"></script>
</body>

</html>
