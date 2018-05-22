var PATH = '../dashboard/';

window.onload = function() {
  $.getJSON('./assets/forms/quotations.json').then(function(data) {
    if (typeof data !== 'object') {
      return alert('formato de formulario es invalido!');
    }

    var current_step = 0;
    var steps = [$('<div class="form-group" data-step="0"></div>')];
    for (var num in data) {
      var value = data[num];

      var dom = $('<div></div>');

      // Setup label
      if (value.label) $(dom).append('<label class="col-form-label">' + value.label + ':' + (value.extra && value.extra.comment ? (' <span style="font-size: 10px; opacity: 0.7;">' + value.extra.comment + '</span>') : '') + '</label>');

      switch (value.type) {
        case 'break':
          current_step++;
          steps.push($('<div data-step="' + current_step + '" style="display: none;" class="form-group">' + (value.extra && value.extra.title ? '<h1 class="h2">' + value.extra.title + '</h1>' : '') + '</div>'));
          break;
        case 'text':
          $(dom).append('<input type="text" placeholder="Inserte aquí..."' + (value.required ? ' required=""' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        case 'textarea':
          $(dom).append('<textarea placeholder="Inserte aquí..."' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '"></textarea>');
          break;
        case 'date':
          $(dom).append('<input type="date"' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        case 'email':
          $(dom).append('<input type="email" placeholder="ejemplo@gmail.com"' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        case 'phonenumber':
          $(dom).append('<input type="tel" placeholder="(###) ### - ####"' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        case 'dropdown':
          $(dom).append('<select' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '"></select>');

          var option_val = 0;
          for (var option in value.extra.options) {
            $(dom).children('select').append('<option value="' + option_val + '">' + value.extra.options[option] + '</option>');
            option_val += 1;
          }
          break;
        case 'file':
          $(dom).append('<input type="file"' + (value.required ? ' required' : '') + ' class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        case 'files':
          $(dom).append('<input type="file"' + (value.required ? ' required' : '') + ' multiple class="form-control" data-backup="' + (value.label || '') + '" name="' + ('id_' + num) + '" />');
          break;
        default:
          console.log('NON REGISTRED INPUT TYPE:\n' + value.type + '\n---');
          continue;
      }

      steps[current_step].append($(dom).children());
    }

    if (steps.length > 1) {
      steps.push($('<button type="button" onclick="var form = $(this).parent(); if (parseInt(form.data(\'current-step\')) > 0) { form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').hide(); form.data(\'current-step\', \'\' + (parseInt(form.data(\'current-step\')) - 1)); form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').show(); }" class="btn btn-primary">Anterior</button> <button type="button" onclick="var form = $(this).parent(); if ((parseInt(form.data(\'current-step\')) + 1) < form.children(\'.form-group\').length) { form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').hide(); form.data(\'current-step\', \'\' + (parseInt(form.data(\'current-step\')) + 1)); form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').show(); }" class="btn btn-primary">Siguiente</button>'));
    }

    steps.push($('<br /> <input type="button" value="Someter" onclick="form_submit($(this).parent().first()[0]);" style="margin: 5px;" class="btn btn-primary" />'));

    $('#quotation').html(steps);
  }).catch(function(reason) {
    alert('Formato de formulario no encontrado!');
  });
};


/**
 * Submite a form.
 * @param {Object} form Form DOM to be submited.
 * @return {undefined} Returns nothing.
 */
function form_submit(form) {
  var required = false;
  var required_fields = [];
  var data = {};
  $(form).find(':input').each(function(num) {
    if (!$(this).attr('name')) {
      return;
    }

    if ($(this).prop('required') && !($(this).val() || (this.files && this.files.length < 0))) {
      return required = true && required_fields.push($(this).data('backup')) && $(this).val(undefined);
    } else if ($(this).attr('type') === 'checkbox') {
      data['id_' + num] = $(this).prop('checked');
    } else if ($(this).attr('type') === 'file' || $(this).attr('type') === 'files') {
      files = this.files;
      if (files.length > 0) {
        data['id_' + num] = {};
        for (var file in files) {
          file = files[file];

          var reader = new FileReader();
          reader.addEventListener("load", function() {

            data['id_' + num][file.name] = reader.result;
          }, false);

          if (file) {
            reader.readAsDataURL(file);
          }
        }
      } else {
        data['id_' + num] = $(this).val();
      }
    } else {
      data['id_' + num] = $(this).val();
    }
  });

  if (required) {
    alert('Debe completar lo siguiente:\n' + required_fields.join('\n'));
  } else {
    $.post({
      url: PATH + '/server/api.php/quotations/add?debug=' + DEBUGGING.server,
      data: {
        data: {
          'company name': $(':input[data-backup=\'Nombre de la Empresa\']').val() || 'Nueva Empresa',
          job: $(':input[data-backup=\'Nombre del Puesto\']').val() || 'Nuevas Posición',
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