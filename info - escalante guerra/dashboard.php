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
<html lang="en" style="width: 100%; height: 100%">

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

  <title>EG - Tablero</title>
  <link rel="icon" href="./assets/escalante_guerra_logo.jpg">

	<!-- CSS Libraries -->
	<link href="./css/libs/jquery-waitMe.min.css" rel="stylesheet">
	<link href="./css/libs/bootstrap.min.css" rel="stylesheet">

	<!-- Custom CSS -->
	<style>
		.page-content {
			height: 100%;
		}

		.unselectable {
			user-select: none;
      -moz-user-select: none;
      -khtml-user-select: none;
      -webkit-user-select: none;
      -o-user-select: none;
		}
	</style>
	<link href="./css/dashboard.css" rel="stylesheet">
</head>

<body style="width: 100%; height: 100%">
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

  <div style="height: 100%;" class="container-fluid">
    <div style="height: 100%;" class="row">
			<!-- MENU -->
      <nav class="col-md-2 d-none d-md-block bg-light sidebar">
        <div class="sidebar-sticky">
          <ul class="nav flex-column">
            <li data-location="desk" onclick="content_change('desk');" class="nav-item border-bottom">
              <a href="#" class="nav-link active">
                Escritorio
              </a>
            </li>
            <li data-location="search" onclick="content_change('search');" style="display: none;" class="nav-item border-bottom">
              <a href="#" class="nav-link">
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
            <li data-location="candidates" onclick="content_change('candidates');" class="nav-item border-bottom">
              <a href="#" class="nav-link">
                Candidatos
              </a>
            </li>
            <li data-location="users" onclick="content_change('users');" class="nav-item">
              <a href="#" class="nav-link">
                Usuarios
              </a>
            </li>
            <li data-location="logs" onclick="content_change('logs');" class="nav-item">
              <a href="#" class="nav-link">
                Registros
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
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('search');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>

        <h2>Puestos Coincidentes</h2>
        <div class="table-responsive">
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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
          <table class="table table-striped table-hover table-sm">
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

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('jobs');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="$('#jobs_add').modal('show');" class="btn btn-sm btn-outline-secondary">+ Agregar Puesto</button>
              <button onclick="content_export('jobs');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive" id="all_jobs_table">
					<table-component :asset="asset" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>

			<!-- REQUISITIONS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="requisitions">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Requisiciones</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('requisitions');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="" class="btn btn-sm btn-outline-secondary">+ Agregar Requisicion</button>
              <button class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-hover table-sm">
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

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('candidates');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="" class="btn btn-sm btn-outline-secondary">+ Agregar Candidato</button>
              <button class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive">
          <table class="table table-striped table-hover table-sm">
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

			<!-- USERS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="users">
				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
				  <h1 class="h2">Usuarios</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
				    <div class="btn-group mr-2">
				      <button onclick="content_refresh('users');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
				      <button onclick="$('#users_add').modal('show');" class="btn btn-sm btn-outline-secondary">+ Agregar Usuario</button>
				      <button onclick="content_export('users');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
				      <input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.users.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
				    </div>
				  </div>
				</div>
				<div class="table-responsive" id="all_users_table">
				  <table-component :asset="asset" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
				</div>
			</main>

			<!-- LOGS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="logs">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Registros</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('logs');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="$('#logs_clear').modal('show');" class="btn btn-sm btn-outline-secondary">Borrar Registros</button>
							<button onclick="content_export('logs');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
							<input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.logs.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
						</div>
          </div>
        </div>
        <div class="table-responsive" id="all_logs_table">
					<table-component :asset="asset" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>
    </div>
  </div>

	<!-- DIALOGS -->
	<!-- JOBS DIALOGS -->
	<!-- JOBS ADD -->
	<div class="modal fade" id="jobs_add" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Agregar Puesto</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form id="jobs_add_info">
	          <div class="form-group">
	            <label for="recipient-name" class="col-form-label">Título:</label>
	            <input type="text" class="form-control" name="title">
	            <label for="recipient-name" class="col-form-label">Descripción del Puesto:</label>
							<textarea placeholder="" class="form-control" name="description"></textarea>
	          </div>
	        </form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#jobs_add_info :input').each(function() { data[this.name] = $(this).val(); }); jobs_add(data); $('#jobs_add').modal('hide'); $('#jobs_add_info').trigger('reset')" class="btn btn-primary">Agregar</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- JOBS MODIFY -->
	<div class="modal fade" id="jobs_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
		  <div class="modal-dialog modal-dialog-centered" role="document">
		    <div class="modal-content">
					<!-- HEADER -->
		      <div class="modal-header">
		        <h5 class="modal-title">Editar Puesto</h5>

						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
		          <span aria-hidden="true">&times;</span>
		        </button>
		      </div>

					<!-- BODY -->
		      <div class="modal-body">
		        <form id="jobs_modify_info">
		          <div class="form-group">
		            <label for="recipient-name" class="col-form-label">Título:</label>
		            <input type="text" class="form-control" name="title">
		            <label for="recipient-name" class="col-form-label">Descripción del Puesto:</label>
								<textarea placeholder="" class="form-control" name="description"></textarea>
		          </div>
		        </form>
		      </div>

					<!-- FOOTER -->
		      <div class="modal-footer">
		        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
		        <button onclick="var data = {}; $('#jobs_modify_info :input').each(function() { data[this.name] = $(this).val(); }); jobs_modify(GLOBALS.asset.id, data); $('#jobs_modify').modal('hide');" type="button" class="btn btn-primary">Someter</button>
		      </div>
		    </div>
		  </div>
		</div>

	<!-- JOBS REMOVE -->
	<div class="modal fade" id="jobs_remove" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Eliminar Usuario</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <p class="font-italic">
						¿Seguro que quieres eliminar puesto?<br />
						<span class="text-danger font-weight-bold">¡No es reversible!</span>
					</p>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button onclick="jobs_remove(GLOBALS.asset.id); $('#jobs_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
	      </div>
	    </div>
	  </div>
	</div>


	<!-- USERS DIALOGS -->
	<!-- USERS ADD -->
	<div class="modal fade" id="users_add" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Agregar Usuario</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form id="users_add_info">
	          <div class="form-group">
	            <label for="recipient-name" class="col-form-label">Nombre de Usuario:</label>
	            <input type="text" class="form-control" name="username">
	            <label for="recipient-name" class="col-form-label">Contraseña:</label>
	            <input type="password" class="form-control" name="password">
	            <label for="recipient-name" class="col-form-label">Nivel de Acceso:</label>
	            <input type="number" step="1" min="0" max="2" class="form-control" name="access">
	          </div>
	        </form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#users_add_info :input').each(function() { data[this.name] = $(this).val(); }); users_add(data); $('#users_add').modal('hide'); $('#users_add_info').trigger('reset')" class="btn btn-primary">Agregar</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- USERS MODIFY -->
	<div class="modal fade" id="users_modify" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
		  <div class="modal-dialog modal-dialog-centered" role="document">
		    <div class="modal-content">
					<!-- HEADER -->
		      <div class="modal-header">
		        <h5 class="modal-title">Editar Usuario</h5>

						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
		          <span aria-hidden="true">&times;</span>
		        </button>
		      </div>

					<!-- BODY -->
		      <div class="modal-body">
		        <form id="users_modify_info">
		          <div class="form-group">
		            <label for="recipient-name" class="col-form-label">Nombre de Usuario:</label>
		            <input type="text" class="form-control" name="username">
		            <label for="recipient-name" class="col-form-label">Contraseña:</label>
		            <input type="password" placeholder="Nueva contraseña..." class="form-control" name="password">
		            <label for="recipient-name" class="col-form-label">Nivel de Acceso:</label>
		            <input type="number" step="1" min="0" max="2" class="form-control" name="access">
		          </div>
		        </form>
		      </div>

					<!-- FOOTER -->
		      <div class="modal-footer">
		        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
		        <button onclick="var data = {}; $('#users_modify_info :input').each(function() { data[this.name] = $(this).val(); }); users_modify(GLOBALS.asset.id, data); $('#users_modify').modal('hide');" type="button" class="btn btn-primary">Someter</button>
		      </div>
		    </div>
		  </div>
		</div>

	<!-- USERS REMOVE -->
	<div class="modal fade" id="users_remove" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Eliminar Usuario</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <p class="font-italic">
						¿Seguro que quieres eliminar usuario?<br />
						<span class="text-danger font-weight-bold">¡No es reversible!</span>
					</p>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button onclick="users_remove(GLOBALS.asset.id); $('#users_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
	      </div>
	    </div>
	  </div>
	</div>


	<!-- LOGS DIALOGS -->
	<!-- LOGS CLEAR -->
	<div class="modal fade" id="logs_clear" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Borrar Registros</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <p class="font-italic">
						¿Seguro que quieres borrar todos los registros?<br />
						<span class="text-danger font-weight-bold">¡No es reversible!</span>
					</p>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button onclick="logs_clear(); $('#logs_clear').modal('hide');" type="button" class="btn btn-primary">Borrar</button>
	      </div>
	    </div>
	  </div>
	</div>

  <!-- JS Libraries -->
  <script src="./js/libs/jquery-3.3.1.min.js"></script>
  <script src="./js/libs/jquery-waitMe.min.js"></script>
  <script src="./js/libs/bootstrap.min.js"></script>
  <script src="./js/libs/vue.min.js"></script>
  <script src="./js/libs/html2pdf.bundle.min.js"></script>

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
		      <th v-if="modifiable || removable" class="unselectable">
						Acciónes
		      </th>
		    </tr>
	    </thead>
	    <tbody>
	      <tr v-for="entry in filtered_data" v-on:click="selected($event, entry);">
	        <td v-for="(val, key) in columns">
	          {{ entry[val.referencing] }}
	        </td>
					<td v-if="modifiable || removable">
						<span v-if="modifiable" v-on:click="edit($event);" style="cursor: pointer;">✏️</span>
						<span v-if="removable" v-on:click="remove($event);" style="cursor: pointer;">🗑️</span>
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
