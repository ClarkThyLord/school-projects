var DEBUGGING = {
  console: false,
  popups: false,
  server: false
};

window.onload = function() {
  console.log('PAGE LOADED\n---');
};

/**
 * Convert a object form to a html.
 * @param {Object} data Object form.
 * @return {Array} Returns array with html.
 */
function form_to_html(data) {
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

  return steps;
}




/**
 * Convert a html form to a object containing form's data.
 * @param {object} dom DOM form.
 * @return {undefined} Returns data.
 */
async function html_to_data(dom) {
  var required = false;
  var required_fields = [];
  var data = {};

  var inputs = $(dom).find(':input');
  for (num = 0; num < inputs.length; num++) {
    var input = inputs[num];

    if (!$(input).attr('name')) {
      continue;
    }

    if ($(input).prop('required') && !($(input).val() || (input.files && input.files.length < 0))) {
      required = true;
      required_fields.push($(input).data('backup'));
      $(input).val(undefined)
      continue;
    } else if ($(input).attr('type') === 'checkbox') {
      data['id_' + num] = $(input).prop('checked');
    } else if ($(input).attr('type') === 'file' || $(input).attr('type') === 'files') {
      files = input.files;

      var getBase64 = function getBase64(file) {
        return new Promise((resolve, reject) => {
          const reader = new FileReader();
          reader.readAsDataURL(file);
          reader.onload = function() {
            resolve(reader.result);
          };
          reader.onerror = function(error) {
            reject(error);
            alert(error);
          };
        });
      }

      data['id_' + num] = {};
      if (files.length > 0) {
        data['id_' + num] = {};
        for (var file in Object.keys(files)) {
          file = files[file];

          var file_base64 = await getBase64(file);

          data['id_' + num][file.name] = file_base64;
        }
      }
    } else {
      data['id_' + num] = $(input).val();
    }
  }

  if (required) {
    alert('Debe completar lo siguiente:\n' + required_fields.join('\n'));
    return false;
  } else {
    return data;
  }
}