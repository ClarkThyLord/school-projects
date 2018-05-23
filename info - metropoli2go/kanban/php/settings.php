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
					<div class="list-group" role="tablist col-3">
					  <a class="list-group-item list-group-item-action active" data-toggle="list" href="#settings-options" role="tab">Opciones</a>
					  <a class="list-group-item list-group-item-action" data-toggle="list" href="#settings-users" role="tab">Usuarios</a>
					  <a class="list-group-item list-group-item-action" data-toggle="list" href="#settings-logs" role="tab">Registros</a>
					</div>

					<div class="tab-content col-9">
						<!-- OPTIONS -->
					  <div class="tab-pane active" id="settings-options" role="tabpanel">
							<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center border-bottom">
				        <h2>Opciones</h2>
							</div>

							<form action="#" onsubmit="return false;">
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
				            <button onclick="" class="form-control btn btn-sm btn-outline-secondary">↻ Refrescar</button>
				          </div>
				        </div>
							</div>

			        <div class="table-responsive" id="users">
								<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
			        </div>
						</div>

						<!-- LOGS -->
					  <div class="tab-pane" id="settings-logs" role="tabpanel">
							<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center border-bottom">
				        <h2>Registros</h2>
								<div class="btn-toolbar">
				          <div class="btn-group">
										<input type="text" oninput="VUE_ELEMENTS.logs.search_term = this.value;" placeholder="🔍 Buscar..." class="form-control btn-sm" />
				            <button onclick="" class="form-control btn btn-sm btn-outline-secondary">↻ Refrescar</button>
				          </div>
				        </div>
							</div>

			        <div class="table-responsive" id="logs">
								<table-component :asset="asset" :more="more" :modifiable="modifiable" :removable="removable" :sort_key="sort_key" :search_term="search_term" :columns="columns" :data="data"></table-component>
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
