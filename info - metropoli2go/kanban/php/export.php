<!-- EXPORT HTML -->
<!-- EXPORT View -->
<div class="modal fade" id="export_view" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
    <div class="modal-content">
			<!-- HEAD -->
			<div class="modal-header">
        <h5 class="modal-title">Exportar</h5>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

			<!-- BODY -->
      <div class="modal-body">
				<div id="export">
					<export-component :columns="columns" :data="data"></export-component>
				</div>
      </div>

			<!-- FOOTER -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
				<button type="button" onclick="pdf_export();" class="btn btn-succes">Descargar</button>
      </div>
    </div>
  </div>
</div>
