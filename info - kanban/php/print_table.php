<div class="dialog-menu" title="Print Table" id="print_table">
	<div class="content">

		<div class="print-content">
			<table>
			  <tr>
			    <th>Ubicación</th>
			    <th>Tema A Difundir</th>
			    <th>Geo/Historia</th>
			    <th>Actual</th>
			    <th>Riesgo</th>
			    <th>Hechos Graciosos</th>
			    <th>Hard Facts</th>
			    <th>M2GO ToDo</th>
			    <th>Otro Tema.</th>
			  </tr>
			  <tr>
			    <td></td>
			    <td></td>
			    <td></td>
			  </tr>
			</table>
		</div>

    <fieldset class="bar">
      <input type="button" value="Download PDF"  onclick="html_pdf.addHTML($('.print-content').get(0), function () { html_pdf.save('M2GO.pdf'); });" class="item" />
    </fieldset>
	</div>
</div>