<!-- SECTIONS HTML -->
<!-- SECTION Add -->
<div class="modal fade" id="section_add" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Agregar Sección</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
        <form>
					<label class="col-form-label">Nombre de la Sección:</label>
					<input type="text" class="form-control" name="title" />
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#section_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
        <button type="button" class="btn btn-primary">Crear</button>
      </div>
    </div>
  </div>
</div>


<!-- SECTION Modify -->
<div class="modal fade" id="section_modify" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Modificar Sección</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
        <form>
					<label class="col-form-label">Nombre de la Sección:</label>
					<input type="text" class="form-control" name="title" />
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#section_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
				<button type="button" onclick="$('#section_remove').model('show');" class="btn btn-danger">Eliminar</button>
        <button type="button" class="btn btn-primary">Crear</button>
      </div>
    </div>
  </div>
</div>


<!-- SECTION Remove -->
<div class="modal fade" id="section_remove" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Eliminar Sección</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
				<p class="font-italic">
					¿Seguro que quieres eliminar <b>Sección</b>?<br />
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
