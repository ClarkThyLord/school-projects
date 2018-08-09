var PATH = '../dashboard/';

window.onload = function() {
  $.getJSON('./assets/forms/candidates.json').then(function(data) {
    if (typeof data !== 'object') {
      return alert('formato de formulario es invalido!');
    }

    var steps = form_to_html(data);

    steps.push($('<br /> <input type="button" value="Someter" onclick="form_submit($(this).parent().first()[0]);" style="margin: 5px;" class="btn btn-primary" />'));

    $('#curriculum').html(steps);
  }).catch(function(reason) {
    alert('Formato de formulario no encontrado!');
  });
};


/**
 * Submite a form.
 * @param {Object} form Form DOM to be submited.
 * @return {undefined} Returns nothing.
 */
async function form_submit(form) {
  var data = await html_to_data(form);

  if (data) {
    $.post({
      url: PATH + '/server/api.php/candidates/add?debug=' + DEBUGGING.server,
      data: {
        data: {
          name: $(':input[data-backup=\'Nombre Completo\']').val() || 'Nuevo Candidato',
          data: data || {}
        }
      },
      success: function(response) {
        response = JSON.parse(response);
        if (response.status === 'success') {
          alert('enviado con éxito!');
        }

        if (response.status === 'failure') {
          alert(response.reason);
        }
      }
    });
  }
}