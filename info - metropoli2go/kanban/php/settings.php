<!-- SETTINGS HTML -->
<div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="settings" aria-hidden="true" id="settings">
  <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
		<!-- HEADER -->
		<div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Ajustes</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
				<div class="row">
					<!-- TABS -->
					<div class="list-group" role="tablist col-3">
					  <a class="list-group-item list-group-item-action active" data-toggle="list" href="#settings-options" role="tab">Opciones</a>
					  <a onclick="refresh('users');" class="list-group-item list-group-item-action" data-toggle="list" href="#settings-users" role="tab">Usuarios</a>
					  <a onclick="refresh('logs');" class="list-group-item list-group-item-action" data-toggle="list" href="#settings-logs" role="tab">Registros</a>
					</div>

					<div class="tab-content col-9">
						<!-- OPTIONS -->
						<div class="tab-pane active" id="settings-options" role="tabpanel">
							<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center border-bottom">
						    <h2>Opciones</h2>
							</div>

							<form action="#" onsubmit="return false;" action="#" onsubmit="return false;">
						    <div class="form-group">
									<label class="col-form-label">Autorefrescar:</label>
									<div class="custom-switch custom-switch-label-onoff">
									  <input type="checkbox" checked="true" class="custom-switch-input" name="autorefresh" id="autorefresh">
									  <label class="custom-switch-btn" for="autorefresh"></label>
									</div>
								</div>
							</form>
						</div>

						<!-- USERS -->
						<div class="tab-pane" id="settings-users" role="tabpanel">
							<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center border-bottom">
						    <h2>Usuarios</h2>
								<div class="btn-toolbar">
						      <div class="btn-group">
										<input type="text" oninput="VUE_ELEMENTS.users.search_term = this.value;" placeholder="🔍 Buscar..." class="form-control btn-sm" />
						        <button onclick="$('#user_add').modal('show');" class="form-control btn btn-sm btn-outline-secondary">+ Agregar Usuario</button>
						        <button onclick="refresh('users');" class="form-control btn btn-sm btn-outline-secondary">↻ Refrescar</button>
						      </div>
						    </div>
							</div>

						  <div class="table-responsive" id="users">
								<table-component :asset="asset" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
						  </div>
						</div>

						<!-- LOGS -->
						<div class="tab-pane" id="settings-logs" role="tabpanel">
							<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center border-bottom">
							  <h2>Registros</h2>
								<div class="btn-toolbar">
							    <div class="btn-group">
										<input type="text" oninput="VUE_ELEMENTS.logs.search_term = this.value;" placeholder="🔍 Buscar..." class="form-control btn-sm" />
							      <button onclick="refresh('logs');" class="form-control btn btn-sm btn-outline-secondary">↻ Refrescar</button>
										<button onclick="$('#log_clear').modal('show');" class="form-control btn btn-sm btn-outline-secondary">Borrar Registros</button>
							    </div>
							  </div>
							</div>

						  <div class="table-responsive" id="logs">
								<table-component :asset="asset" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
						  </div>
						</div>
					</div>
				</div>
      </div>

			<!-- FOOTER -->
			<div class="modal-footer">
      </div>
    </div>
  </div>
</div>


<!-- USERS HTML -->
<!-- USER Add -->
<div class="modal fade" id="user_add" role="dialog" aria-labelledby="user_add" aria-hidden="true">
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
				<form action="#" onsubmit="return false;" action="#">
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
				<button type="button" onclick="var data = {}; $('#user_add form :input').each(function() { data[this.name] = $(this).val(); }); users_add(data); $('#user_add').modal('hide').find('form').trigger('reset');" class="btn btn-primary">Agregar</button>
			</div>
		</div>
	</div>
</div>

<!-- USER Modify -->
<div class="modal fade" id="user_modify" role="dialog" aria-labelledby="user_modify" aria-hidden="true">
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
					<form action="#" onsubmit="return false;" action="#">
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
					<button onclick="var data = {}; $('#user_modify form :input').each(function() { data[this.name] = $(this).val(); }); users_modify(GLOBALS.asset.id, data); $('#user_modify').modal('hide').find('form').trigger('reset');" type="button" class="btn btn-primary">Someter</button>
				</div>
			</div>
		</div>
	</div>

<!-- USER Remove -->
<div class="modal fade" id="user_remove" role="dialog" aria-labelledby="user_remove" aria-hidden="true">
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
					¿Seguro que quieres eliminar <b>usuario</b>?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
			</div>

			<!-- FOOTER -->
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
				<button onclick="users_remove(GLOBALS.asset.id); $('#user_remove').modal('hide');" type="button" class="btn btn-primary">Eliminar</button>
			</div>
		</div>
	</div>
</div>


<!-- LOGS HTML -->
<!-- LOG Clear -->
<div class="modal fade" id="log_clear" role="dialog" aria-labelledby="log_clear" aria-hidden="true">
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
					¿Seguro que quieres borrar todos los <b>registros</b>?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
			</div>

			<!-- FOOTER -->
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
				<button onclick="logs_clear(); $('#log_clear').modal('hide');" type="button" class="btn btn-primary">Borrar</button>
			</div>
		</div>
	</div>
</div>
