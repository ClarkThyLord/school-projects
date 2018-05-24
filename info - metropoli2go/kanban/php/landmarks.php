<!-- LANDMARKS HTML -->
<!-- LANDMARK Add -->
<div class="modal fade" id="landmark_add" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Agregar Landmark</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
        <form>
					<label class="col-form-label">Nombre del Landmark:</label>
					<input type="text" class="form-control" name="title" />
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#landmark_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
        <button type="button" class="btn btn-primary">Crear</button>
      </div>
    </div>
  </div>
</div>


<!-- LANDMARK Modify -->
<div class="modal fade" id="landmark_modify" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Modificar Landmark</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
        <form>
					<label class="col-form-label">Nombre del Landmark:</label>
					<input type="text" class="form-control" name="title" />
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#landmark_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
				<button type="button" onclick="$('#landmark_remove').model('show');" class="btn btn-danger">Eliminar</button>
        <button type="button" class="btn btn-primary">Crear</button>
      </div>
    </div>
  </div>
</div>


<!-- LANDMARK Remove -->
<div class="modal fade" id="landmark_remove" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Eliminar Landmark</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
				<p class="font-italic">
					¿Seguro que quieres eliminar <b>Landmark</b>?<br />
					<span class="text-danger font-weight-bold">¡No es reversible!</span>
				</p>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
				<button type="button" class="btn btn-danger">Confirmar</button>
      </div>
    </div>
  </div>
</div>
