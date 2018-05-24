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
        <form action="#" onsubmit="return false;">
					<label class="col-form-label">Nombre del Landmark:</label>
					<input type="text" class="form-control" name="name" />
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#landmark_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
        <button type="button" onclick="var data = {section: GLOBALS.section.id}; $('#landmark_add form :input').each(function() { data[this.name] = $(this).val(); }); landmarks_add(data); $('#landmark_add').modal('hide').find('form').trigger('reset');" class="btn btn-primary">Crear</button>
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
        <form action="#" onsubmit="return false;">
					<div class="form-group form-row">
						<label class="col-sm-5 col-form-label">ID del Landmark:</label>
						<input type="text" disabled class="col-sm-7 form-control" name="id" />

						<label class="col-form-label">Nombre del Landmark:</label>
						<input type="text" class="form-control" name="name" />
					</div>
					<div class="form-group">
						<label class="col-form-label">Clasificación:</label> <br />
				    <select class="form-control" name="classification">
			        <option value="Geo/Historia">Geo/Historia</option>
			        <option value="Actual">Actual</option>
			        <option value="Riesgo">Riesgo</option>
			        <option value="Hechos Graciosos">Hechos Graciosos</option>
			        <option value="Hard Facts">Hard Facts</option>
			        <option value="M2GO ToDo">M2GO ToDo</option>
			        <option value="Otro Tema.">Otro Tema.</option>
		        </select>
						<div class="form-row">
					    <div class="form-group col-md-6">
					      <label>Latitud:</label>
					      <input type="number" placeholder="0.0" step="0.001" class="form-control" name="latitude">
					    </div>
					    <div class="form-group col-md-6">
					      <label>Longitud:</label>
					      <input type="number" placeholder="0.0" step="0.001" class="form-control" name="longitude">
					    </div>
					  </div>
						<label class="col-form-label">Descripción:</label>
						<textarea placeholder="Descripción..." class="form-control" name="summary"></textarea>
						<label class="col-form-label">URLs:</label>
						<textarea placeholder="URLs... (separar con ,)" class="form-control" name="summary"></textarea>
						<label class="col-form-label">Archivos:</label>
				    <input type="file" style="display: none;" class="task_input" id="landmark_files" />
				    <div class="btn btn-info btn-block" id="landmark_files_dropzone">
				        Dar Click o Arrastrar Archivo
				    </div>
						<div class="" id="landmark_files_preview">
							<files-component :files="files"></files-component>
						</div>
					</div>
				</form>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" onclick="$('#landmark_add').find('form').first().trigger('reset');" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
				<button type="button" onclick="$('#landmark_remove').modal('show');" class="btn btn-danger">Eliminar</button>
        <button type="button" onclick="var data = {}; $('#landmark_modify form :input').each(function() { if (this.name) { data[this.name] = $(this).val(); } }); landmarks_modify(GLOBALS.landmark.id, data); $('#landmark_modify').modal('hide').find('form').trigger('reset');" class="btn btn-primary">Modificar</button>
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
				<button type="button" onclick="landmarks_remove(GLOBALS.landmark.id); $('#landmark_remove').modal('hide'); $('#landmark_modify').modal('hide').trigger('reset');" class="btn btn-danger">Confirmar</button>
      </div>
    </div>
  </div>
</div>
