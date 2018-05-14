// FORMS Functions
// *****************************************************************************

/**
 * Clear the all the logs.
 * @param {string} identifier Form's identifier.
 * @return {undefined} Returns nothing.
 */
async function forms_format_get(identifier) {
  return await $.getJSON('./assets/forms/' + identifier + '.json');
}


/**
 * Clear the all the logs.
 * @return {undefined} Returns nothing.
 */
function forms_get() {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/forms/clear?debug=' + DEBUGGING.server,
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a form.
 * @param {object} data Data to create form with.
 * @return {undefined} Returns nothing.
 */
function forms_add(data) {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/forms/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        type: data.type || 'desconocido',
        data: JSON.stringify(data.data || {})
      }
    },
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a form's data.
 * @param {integer} id Form's ID.
 * @param {object} data Data to modify form with.
 * @return {undefined} Returns nothing.
 */
function forms_modify(id, data) {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'type',
    'data'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/forms/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Clear the all the logs.
 * @param {integer} id Form's ID.
 * @return {undefined} Returns nothing.
 */
function forms_remove(id) {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/forms/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Convert a object form to a html form.
 * @param {object} dom Object form.
 * @return {undefined} Returns nothing.
 */
function form_to_html(data) {
  var num = 0;
  var form = $('<form></form>');
  for (var key in data) {
    value = data[key];

    num++;

    var dom;
    switch (value.type) {
      case 'break':
        dom = $('<hr />');
        break;
      case 'text':
        dom = $('<input type="text" placeholder="Inserte aquí..." />');
        break;
      case 'textarea':
        dom = $('<textarea placeholder="Inserte aquí..."></textarea>');
        break;
      case 'email':
        dom = $('<input type="email" placeholder="ejemplo@gmail.com" />');
        break;
      case 'phonenumber':
        dom = $('<input type="tel" placeholder="(###) ### - ####" />');
        break;
      case 'dropdown':
        dom = $('<select></select>');

        for (var option in value.extra.options) {
          $(dom).append('<option value="' + value.extra.options[option] + '">' + value.extra.options[option] + '</option>');
        }
        break;
      case 'file':
        dom = $('<input type="file" />');
        break;
      case 'files':
        dom = $('<input type="file" multiple />');
        break;
      default:
        continue;
    }

    $(dom).attr('name', value.label || ('new_value_' + num));

    if (value.label) dom = $(dom).wrap('<label>' + value.label + ': ' + (value.extra && value.extra.comment ? ('<span style="font-size: 10px; opacity: 0.7;">' + value.extra.comment + '</span>') : '') + '</label>').parent()[0];

    if (value.required) $(dom).prop("required", true);

    form.append(dom);
  }

  return form;
}


/**
 * Convert a html form to a object containing form's data.
 * @param {object} dom DOM form.
 * @return {undefined} Returns nothing.
 */
function html_to_data(dom) {
  var data = {};
  $(dom).find(':input').each(function() {
    if ($(this).attr('type') === 'checkbox') {
      data[$(this).attr('name')] = $(this).prop('checked');
    } else if ($(this).attr('type') === 'file') {
      data[$(this).attr('name')] = this.files;
    } else {
      data[$(this).attr('name')] = $(this).val(GLOBALS.asset[this.name]);
    }
  });

  return data;
}