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
	<link href="./css/libs/bootstrap-switch.min.css" rel="stylesheet">

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
    <input onchange="$('.nav-item[data-location=\'search\']').show(); $('#search').data('search-term', this.value); content_change('search');" class="form-control form-control-dark w-100" type="text" placeholder="Buscar... (e.j. puestos, cotizaciónes, requisiciones y candidatos)" aria-label="Buscar">
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
            <li data-location="quotations" onclick="content_change('quotations');" class="nav-item">
              <a href="#" class="nav-link">
                Cotizaciónes
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
			<!-- DESK -->
      <main role="main" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="desk">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Escritorio</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('desk');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
            </div>
          </div>
        </div>

        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Puestos Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('jobs');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="recent_jobs_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Cotizaciónes Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('quotations');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="recent_quotations_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Requisiciones Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('requisitions');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="recent_requisitions_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Candidatos Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('candidates');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="recent_candidates_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Registros Recientes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
	            <button onclick="content_change('logs');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="recent_logs_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>

			<!-- SEARCH -->
      <main data-search_term="" role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="search">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Búsqueda</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_change('search');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
            </div>
          </div>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Puestos Relevantes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
							<button onclick="html_export($('#search_jobs_table').first()[0]);" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <button onclick="content_change('jobs');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="search_jobs_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Cotizaciónes Relevantes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
							<button onclick="html_export($('#search_quotations_table').first()[0]);" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <button onclick="content_change('quotations');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="search_quotations_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Requisiciones Relevantes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
							<button onclick="html_export($('#search_requisitions_table').first()[0]);" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <button onclick="content_change('requisitions');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="search_requisitions_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>

				<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center">
	        <h2>Candidatos Relevantes</h2>
					<div class="btn-toolbar">
	          <div class="btn-group">
							<button onclick="html_export($('#search_candidates_table').first()[0]);" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <button onclick="content_change('candidates');" class="btn btn-sm btn-outline-secondary">Ver Todo</button>
	          </div>
	        </div>
				</div>

        <div class="table-responsive" id="search_candidates_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
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
	            <input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.all_jobs.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive" id="all_jobs_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>

			<!-- QUOTATIONS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="quotations">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Cotizaciónes</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('quotations');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="$('#quotations_add').modal('show');" class="btn btn-sm btn-outline-secondary">+ Agregar Cotización</button>
              <button onclick="content_export('quotations');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.all_quotations.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive" id="all_quotations_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>

			<!-- REQUISITIONS -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="requisitions">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Requisiciones</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('requisitions');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="$('#requisitions_add').modal('show');" class="btn btn-sm btn-outline-secondary">+ Agregar Requisicion</button>
              <button onclick="content_export('requisitions');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive" id="all_requisitions_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>

			<!-- CANDIDATES -->
      <main role="main" style="display: none;" class="page-content col-md-9 ml-sm-auto col-lg-10 px-4" id="candidates">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
          <h1 class="h2">Candidatos</h1>

					<div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group mr-2">
              <button onclick="content_refresh('candidates');" class="btn btn-sm btn-outline-secondary">&#8635; Refrescar</button>
              <button onclick="$('#candidates_add').modal('show');" class="btn btn-sm btn-outline-secondary">+ Agregar Candidato</button>
              <button onclick="content_export('candidates');" class="btn btn-sm btn-outline-secondary">&#8689; Exportar</button>
	            <input type="text" placeholder="Buscar..." style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
            </div>
          </div>
        </div>
        <div class="table-responsive" id="all_candidates_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
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
				      <input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.all_users.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
				    </div>
				  </div>
				</div>
				<div class="table-responsive" id="all_users_table">
			  <table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
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
							<input type="text" placeholder="Buscar..." oninput="VUE_ELEMENTS.all_logs.search_term = this.value;" style="text-align: left;" class="btn btn-sm btn-outline-secondary" />
						</div>
          </div>
        </div>
        <div class="table-responsive" id="all_logs_table">
					<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
        </div>
      </main>
    </div>
  </div>


	<!-- DIALOGS -->
	<!-- FORM DIALOGS -->
	<!-- FORM VIEW -->
	<div class="modal fade" id="forms_view" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Visualización de Datos</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="forms_view_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="$('#forms_view').modal('hide'); $('#forms_view_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>


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
	        <form action="#" id="jobs_add_info">
	          <div class="form-group">
	            <label class="col-form-label">Título:</label>
	            <input type="text" class="form-control" name="title">
	            <label class="col-form-label">Descripción del Puesto:</label>
							<textarea class="form-control" name="description"></textarea>
	          </div>
	        </form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#jobs_add_info :input').each(function() { data[this.name] = $(this).val(); }); jobs_add(data); $('#jobs_add').modal('hide'); $('#jobs_add_info').trigger('reset');" class="btn btn-primary">Agregar</button>
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
		        <form action="#" id="jobs_modify_info">
		          <div class="form-group">
								<label class="col-form-label">Activo:</label>
								<div class="custom-switch custom-switch-label-onoff">
								  <input type="checkbox" class="custom-switch-input" name="active" id="jobs_active_switch">
								  <label class="custom-switch-btn" for="jobs_active_switch"></label>
								</div>
		            <label class="col-form-label">Título:</label>
		            <input type="text" class="form-control" name="title">
		            <label class="col-form-label">Descripción del Puesto:</label>
								<textarea class="form-control" name="description"></textarea>
		          </div>
		        </form>
		      </div>

					<!-- FOOTER -->
		      <div class="modal-footer">
		        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
		        <button onclick="var data = {}; $('#jobs_modify_info :input').each(function() { if ($(this).attr('type') === 'checkbox') { data[this.name] = $(this).prop('checked') ? 1 : 0; } else { data[this.name] = $(this).val(); } }); jobs_modify(GLOBALS.asset.id, data); $('#jobs_modify').modal('hide');" type="button" class="btn btn-primary">Someter</button>
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


	<!-- QUOTATIONS DIALOGS -->
	<!-- QUOTATIONS ADD -->
	<div class="modal fade" id="quotations_add" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Agregar Cotización</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="quotations_add_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#quotations_add_info').first()[0]); if (!data) { return; } $('#quotations_add').modal('hide'); quotations_add({'company name': $('#quotations_add_info :input[data-backup=\'Nombre de la Empresa\']').val(), job: $('#quotations_add_info :input[data-backup=\'Nombre del Puesto\']').val(), data: data}); $('#quotations_add_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- QUOTATIONS MODIFY -->
	<div class="modal fade" id="quotations_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Cotización</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" id="quotations_modify_info">
						<label class="col-form-label">Activo:</label>
						<div class="custom-switch custom-switch-label-onoff">
							<input type="checkbox" class="custom-switch-input" name="active" id="quotations_active_switch">
							<label class="custom-switch-btn" for="quotations_active_switch"></label>
						</div>
						<label class="col-form-label">Información:</label>
						<input type="button" value="✏️ Modificar" onclick="setup_form('quotations', (GLOBALS.asset ? GLOBALS.asset.data : {}));" class="form-control" />
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#quotations_modify_info :input').each(function() { if ($(this).attr('type') === 'checkbox') { data[this.name] = $(this).prop('checked') ? 1 : 0; } else if (this.type !== 'button') { data[this.name] = $(this).val(); } }); quotations_modify(GLOBALS.asset.id, data); $('#quotations_modify').modal('hide');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- QUOTATIONS DATA MODIFY -->
	<div class="modal fade" id="quotations_data_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Cotización</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="quotations_data_modify_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#quotations_data_modify_info').first()[0]); if (!data) { return; } $('#quotations_data_modify').modal('hide'); quotations_modify(GLOBALS.asset.id || alert('¡Algo salió mal!'), {'company name': $('#quotations_data_modify_info :input[data-backup=\'Nombre de la Empresa\']').val(), job: $('#quotations_data_modify_info :input[data-backup=\'Nombre del Puesto\']').val(), data: data}); $('#quotations_data_modify_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- QUOTATIONS REMOVE -->
	<div class="modal fade" id="quotations_remove" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered" role="document">
		<div class="modal-content">
			<!-- HEADER -->
			<div class="modal-header">
				<h5 class="modal-title">Eliminar Cotización</h5>

				<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					<span aria-hidden="true">&times;</span>
				</button>
			</div>

			<!-- BODY -->
			<div class="modal-body">
				<p class="font-italic">
					¿Seguro que quieres eliminar cotización?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
			</div>

			<!-- FOOTER -->
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
				<button onclick="quotations_remove(GLOBALS.asset.id); $('#quotations_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
			</div>
		</div>
	</div>
	</div>


	<!-- REQUISITIONS DIALOGS -->
	<!-- REQUISITIONS ADD -->
	<div class="modal fade" id="requisitions_add" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Agregar Requisición</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="requisitions_add_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#requisitions_add_info').first()[0]); if (!data) { return; } $('#requisitions_add').modal('hide'); requisitions_add({'company name': $('#requisitions_add_info :input[data-backup=\'Nombre de la Empresa\']').val(), data: data}); $('#requisitions_add_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- REQUISITIONS MODIFY -->
	<div class="modal fade" id="requisitions_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Requisición</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" id="requisitions_modify_info">
						<label class="col-form-label">Activo:</label>
						<div class="custom-switch custom-switch-label-onoff">
							<input type="checkbox" class="custom-switch-input" name="active" id="requisitions_active_switch">
							<label class="custom-switch-btn" for="requisitions_active_switch"></label>
						</div>
						<label class="col-form-label">Información:</label>
						<input type="button" value="✏️ Modificar" onclick="setup_form('requisitions', (GLOBALS.asset ? GLOBALS.asset.data : {}));" class="form-control" />
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#requisitions_modify_info :input').each(function() { if ($(this).attr('type') === 'checkbox') { data[this.name] = $(this).prop('checked') ? 1 : 0; } else if (this.type !== 'button') { data[this.name] = $(this).val(); } }); requisitions_modify(GLOBALS.asset.id, data); $('#requisitions_modify').modal('hide');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- REQUISITIONS DATA MODIFY -->
	<div class="modal fade" id="requisitions_data_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Requisición</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="requisitions_data_modify_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#requisitions_data_modify_info').first()[0]); if (!data) { return; } $('#requisitions_data_modify').modal('hide'); requisitions_modify(GLOBALS.asset.id || alert('¡Algo salió mal!'), {'company name': $('#requisitions_data_modify_info :input[data-backup=\'Nombre de la Empresa\']').val(), job: $('#requisitions_data_modify_info :input[data-backup=\'Nombre del Puesto\']').val(), data: data}); $('#requisitions_data_modify_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- REQUISITIONS REMOVE -->
	<div class="modal fade" id="requisitions_remove" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered" role="document">
		<div class="modal-content">
			<!-- HEADER -->
			<div class="modal-header">
				<h5 class="modal-title">Eliminar Requisición</h5>

				<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					<span aria-hidden="true">&times;</span>
				</button>
			</div>

			<!-- BODY -->
			<div class="modal-body">
				<p class="font-italic">
					¿Seguro que quieres eliminar Requisición?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
			</div>

			<!-- FOOTER -->
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
				<button onclick="requisitions_remove(GLOBALS.asset.id); $('#requisitions_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
			</div>
		</div>
	</div>
	</div>


	<!-- CANDIDATES DIALOGS -->
	<!-- CANDIDATES ADD -->
	<div class="modal fade" id="candidates_add" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Agregar Candidato</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="candidates_add_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#candidates_add_info').first()[0]); if (!data) { return; } $('#candidates_add').modal('hide'); candidates_add({'name': $('#candidates_add_info :input[data-backup=\'Nombre Completo\']').val(), data: data}); $('#candidates_add_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- CANDIDATES MODIFY -->
	<div class="modal fade" id="candidates_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Candidato</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" id="candidates_modify_info">
						<label class="col-form-label">Activo:</label>
						<div class="custom-switch custom-switch-label-onoff">
							<input type="checkbox" class="custom-switch-input" name="active" id="candidates_active_switch">
							<label class="custom-switch-btn" for="candidates_active_switch"></label>
						</div>
						<label class="col-form-label">Información:</label>
						<input type="button" value="✏️ Modificar" onclick="setup_form('candidates', (GLOBALS.asset ? GLOBALS.asset.data : {}));" class="form-control" />
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#candidates_modify_info :input').each(function() { if ($(this).attr('type') === 'checkbox') { data[this.name] = $(this).prop('checked') ? 1 : 0; } else if (this.type !== 'button') { data[this.name] = $(this).val(); } }); candidates_modify(GLOBALS.asset.id, data); $('#candidates_modify').modal('hide');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- CANDIDATES DATA MODIFY -->
	<div class="modal fade" id="candidates_data_modify" role="dialog" aria-labelledby="jobs_edit" aria-hidden="true">
	  <div class="modal-dialog modal-dialog-centered" role="document">
	    <div class="modal-content">
				<!-- HEADER -->
	      <div class="modal-header">
	        <h5 class="modal-title">Modifica Candidato</h5>

					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
	          <span aria-hidden="true">&times;</span>
	        </button>
	      </div>

				<!-- BODY -->
	      <div class="modal-body">
	        <form action="#" data-current-step="0" id="candidates_data_modify_info">
					</form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = html_to_data($('#candidates_data_modify_info').first()[0]); if (!data) { return; } $('#candidates_data_modify').modal('hide'); candidates_modify(GLOBALS.asset.id || alert('¡Algo salió mal!'), {'name': $('#candidates_data_modify_info :input[data-backup=\'Nombre Completo\']').val(), data: data}); $('#candidates_data_modify_info').trigger('reset');" class="btn btn-primary">Someter</button>
	      </div>
	    </div>
	  </div>
	</div>

	<!-- CANDIDATES REMOVE -->
	<div class="modal fade" id="candidates_remove" role="dialog" aria-labelledby="users_edit" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered" role="document">
		<div class="modal-content">
			<!-- HEADER -->
			<div class="modal-header">
				<h5 class="modal-title">Eliminar Candidato</h5>

				<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					<span aria-hidden="true">&times;</span>
				</button>
			</div>

			<!-- BODY -->
			<div class="modal-body">
				<p class="font-italic">
					¿Seguro que quieres eliminar candidato?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
			</div>

			<!-- FOOTER -->
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
				<button onclick="candidates_remove(GLOBALS.asset.id); $('#candidates_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
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
	        <form action="#" id="users_add_info">
	          <div class="form-group">
	            <label class="col-form-label">Nombre de Usuario:</label>
	            <input type="text" class="form-control" name="username">
	            <label class="col-form-label">Contraseña:</label>
	            <input type="password" class="form-control" name="password">
	            <label class="col-form-label">Nivel de Acceso:</label>
	            <input type="number" step="1" min="0" max="2" class="form-control" name="access">
	          </div>
	        </form>
	      </div>

				<!-- FOOTER -->
	      <div class="modal-footer">
	        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
	        <button type="button" onclick="var data = {}; $('#users_add_info :input').each(function() { data[this.name] = $(this).val(); }); users_add(data); $('#users_add').modal('hide'); $('#users_add_info').trigger('reset');" class="btn btn-primary">Agregar</button>
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
		        <form action="#" id="users_modify_info">
		          <div class="form-group">
		            <label class="col-form-label">Nombre de Usuario:</label>
		            <input type="text" class="form-control" name="username">
		            <label class="col-form-label">Contraseña:</label>
		            <input type="password" placeholder="Nueva contraseña..." class="form-control" name="password">
		            <label class="col-form-label">Nivel de Acceso:</label>
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

  <!-- Custom JS -->
  <script src="./js/common.js"></script>
  <script src="./js/dashboard.js"></script>
</body>

</html>
