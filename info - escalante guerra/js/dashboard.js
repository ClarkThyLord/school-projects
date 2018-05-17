// GLOBAL Variables
var GLOBALS = {
  user: undefined, // Client's current user
  asset: undefined, // Current asset being interacted with
  eng_to_spa: {
    'jobs': 'puestos',
    'requisitions': 'requisiciones',
    'candidates': 'candidatos',
    'users': 'usuarios',
    'logs': 'registros'
  }
};

// CONTENT Functions
// *****************************************************************************

/**
 * Change to content via their tag.
 * @param {string} content Content's identifier.
 * @return {undefined} Returns nothing.
 */
function content_change(content) {
  // Close other content and open specified content
  $('.page-content').hide();
  $('#' + content).show();

  content_refresh(content);

  // Update selected in MENU
  $('.nav-item > .active').removeClass('active');
  $('.nav-item[data-location="' + content + '"] > a').addClass('active');
}


/**
 * Export content's table.
 * @param {string} content Content's identifier.
 * @return {undefined} Returns nothing.
 */
function content_export(content) {
  html2pdf($('#' + content + ' table').first()[0], {
    margin: 10,
    filename: GLOBALS.eng_to_spa[content] + '.pdf',
    html2canvas: {},
    jsPDF: {
      orientation: 'landscape'
    }
  });
}

/**
 * Portrait given user and users in the page.
 * @param {String} content Content's identifier.
 * @return {undefined} Returns nothing.
 */
function content_refresh(content) {
  if (content === 'jobs' || content === 'users' || content === 'logs') {
    $('#' + content).waitMe({
      waitTime: -1,
      effect: 'stretch',
      text: 'Cargando...',
      bg: 'rgba(255, 255, 255, 0.7)',
      color: 'rgba(0, 0, 0)',
    });

    $.get({
      url: './server/api.php/' + content + '/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify({}) + '&options=' + JSON.stringify({}),
      success: function(response) {
        $('#' + content).waitMe('hide');

        response = JSON.parse(response);
        if (response.status === 'success') {
          VUE_ELEMENTS[content].data = response.data.dump || [];
        }

        if (response.status === 'failure' || DEBUGGING.popups) {
          alert(response.reason);
        }
      }
    });
  }
}

// FORMS Functions
// *****************************************************************************

/**
 * Retrieve a form's format.
 * @param {string} identifier Form's identifier.
 * @return {undefined} Returns nothing.
 */
async function forms_format_get(identifier) {
  return await $.getJSON('./assets/forms/' + identifier + '.json');
}


/**
 * Retrive data from forms.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function forms_get(filter, options) {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/forms/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump || [];
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
        VUE_ELEMENTS.forms.data = response.data.dump || [];
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
        VUE_ELEMENTS.forms.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a form from forms.
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
        VUE_ELEMENTS.forms.data = response.data.dump || [];
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

// JOBS Functions
// *****************************************************************************

$('#jobs_modify').on('shown.bs.modal', function(e) {
  $('#jobs_modify_info :input').each(function() {
    if ($(this).attr('type') === 'checkbox') {
      $(this).prop('checked', !!(GLOBALS.asset[this.name] * 1));
    } else {
      $(this).val(GLOBALS.asset[this.name]);
    }
  });
});


/**
 * Retrieve a job from jobs.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function jobs_get(filter, options) {
  $('#jobs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/jobs/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#jobs').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.jobs.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a job.
 * @param {object} data Data to create job with.
 * @return {undefined} Returns nothing.
 */
function jobs_add(data) {
  $('#jobs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/jobs/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        title: data.title || 'Nuevo Puesto',
        description: data.description || 'Nueva posición abierta!'
      }
    },
    success: function(response) {
      $('#jobs').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.jobs.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a job's data.
 * @param {integer} id Job's ID.
 * @param {object} data Data to modify job with.
 * @return {undefined} Returns nothing.
 */
function jobs_modify(id, data) {
  $('#jobs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'title',
    'description'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/jobs/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#jobs').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.jobs.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a job.
 * @param {integer} id Job's ID.
 * @return {undefined} Returns nothing.
 */
function jobs_remove(id) {
  $('#jobs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/jobs/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#jobs').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.jobs.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// REQUISITIONS Functions
// *****************************************************************************

$('#requisitions_modify').on('shown.bs.modal', function(e) {
  $('#requisitions_modify_info :input').each(function() {
    if ($(this).attr('type') === 'checkbox') {
      $(this).prop('checked', !!(GLOBALS.asset[this.name] * 1));
    } else {
      $(this).val(GLOBALS.asset[this.name]);
    }
  });
});


/**
 * Retrieve a requisition from requisitions.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function requisitions_get(filter, options) {
  $('#requisitions').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/requisitions/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#requisitions').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.requisitions.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a requisition.
 * @param {object} data Data to create requisition with.
 * @return {undefined} Returns nothing.
 */
function requisitions_add(data) {
  $('#requisitions').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/requisitions/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        title: data.title || 'Nuevo Puesto',
        description: data.description || 'Nueva posición abierta!'
      }
    },
    success: function(response) {
      $('#requisitions').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.requisitions.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a requisition's data.
 * @param {integer} id requisition's ID.
 * @param {object} data Data to modify requisition with.
 * @return {undefined} Returns nothing.
 */
function requisitions_modify(id, data) {
  $('#requisitions').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'title',
    'description'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/requisitions/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#requisitions').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.requisitions.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a requisition.
 * @param {integer} id requisition's ID.
 * @return {undefined} Returns nothing.
 */
function requisitions_remove(id) {
  $('#requisitions').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/requisitions/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#requisitions').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.requisitions.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// CANDIDATES Functions
// *****************************************************************************

$('#candidates_modify').on('shown.bs.modal', function(e) {
  $('#candidates_modify_info :input').each(function() {
    if ($(this).attr('type') === 'checkbox') {
      $(this).prop('checked', !!(GLOBALS.asset[this.name] * 1));
    } else {
      $(this).val(GLOBALS.asset[this.name]);
    }
  });
});


/**
 * Retrieve a candidate from candidates.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function candidates_get(filter, options) {
  $('#candidates').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/candidates/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#candidates').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.candidates.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a candidate.
 * @param {object} data Data to create candidate with.
 * @return {undefined} Returns nothing.
 */
function candidates_add(data) {
  $('#candidates').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/candidates/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        title: data.title || 'Nuevo Puesto',
        description: data.description || 'Nueva posición abierta!'
      }
    },
    success: function(response) {
      $('#candidates').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.candidates.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a candidate's data.
 * @param {integer} id candidate's ID.
 * @param {object} data Data to modify candidate with.
 * @return {undefined} Returns nothing.
 */
function candidates_modify(id, data) {
  $('#candidates').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'title',
    'description'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/candidates/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#candidates').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.candidates.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a candidate.
 * @param {integer} id candidate's ID.
 * @return {undefined} Returns nothing.
 */
function candidates_remove(id) {
  $('#candidates').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/candidates/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#candidates').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.candidates.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// USERS Functions
// *****************************************************************************

$('#users_modify').on('shown.bs.modal', function(e) {
  $('#users_modify_info :input').each(function() {
    $(this).val(GLOBALS.asset[this.name]);
  });
});


/**
 * Retrieve a user from users.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function users_get(filter, options) {
  $('#logs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/logs/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#users').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.logs.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a user.
 * @param {object} data Data to create user with; options: username, password and access.
 * @return {undefined} Returns nothing.
 */
function users_add(data) {
  $('#users').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/users/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        username: data.username || 'Nuevo Usuario',
        password: data.password || '',
        access: data.access || 0
      }
    },
    success: function(response) {
      $('#users').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.users.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a user's data.
 * @param {integer} id User's ID.
 * @param {object} data Data to modify user with.
 * @return {undefined} Returns nothing.
 */
function users_modify(id, data) {
  $('#users').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'username',
    'password',
    'access'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/users/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#users').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.users.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a user from users.
 * @param {integer} id User's ID.
 * @return {undefined} Returns nothing.
 */
function users_remove(id) {
  $('#users').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/users/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#users').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.users.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// LOGS Functions
// *****************************************************************************

/**
 * Retrieve a log from logs.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
function logs_get(filter, options) {
  $('#forms_view').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? filter : {};

  $.post({
    url: './server/api.php/forms/clear?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      $('#forms_view').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.forms.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Add a log to logs.
 * @return {undefined} Returns nothing.
 */
function logs_add() {}


/**
 * Clear the all the logs.
 * @return {undefined} Returns nothing.
 */
function logs_clear() {
  $('#logs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/logs/clear?debug=' + DEBUGGING.server,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.logs.data = response.data.dump || [];

        $('#logs').waitMe('hide');
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// VUE.js Code
// *****************************************************************************
var VUE_ELEMENTS = {};

// Register Table component
Vue.component('table-component', {
  template: '#table-component',
  props: {
    asset: String,
    more: Boolean,
    modifiable: Boolean,
    removable: Boolean,
    sort_key: String,
    search_term: String,
    columns: Object,
    data: Array
  },
  data: function() {},
  computed: {
    filtered_data: function() {
      if ((this.sort_key || this.search_term) && this.data.length > 0) {
        var data = this.data;

        // Search data
        if (this.search_term) {
          var search_term = this.search_term;
          data = data.filter(function(row) {
            return Object.keys(row).some(function(key) {
              return String(row[key]).toLowerCase().indexOf(search_term) > -1;
            });
          });
        }

        // Sort data
        if (this.sort_key) {
          var sort_key = this.columns[this.sort_key].referencing;
          var sort_type = this.columns[this.sort_key].order === 'des' ? -1 : 1;
          data = data.slice().sort(function(a, b) {
            if (typeof a[sort_key] === 'string' && isNaN(a[sort_key])) {
              a = a[sort_key].toLowerCase();
              b = b[sort_key].toLowerCase();

              if (a < b) {
                return sort_type * (-1);
              } else if (a > b) {
                return sort_type * (1);
              } else {
                return 0;
              }
            } else {
              return sort_type * ((a[sort_key] * 1) - b[sort_key]);
            }
          });
        }

        return data;
      } else {
        return this.data;
      }
    }
  },
  filters: {
    capitalize: function(str) {
      return str.charAt(0).toUpperCase() + str.slice(1);
    }
  },
  methods: {
    sort_by: function(key) {
      this.search_term = key;
    },
    select: function(event, asset) {
      GLOBALS.asset = asset;
    },
    edit: function(event) {
      $('#' + this.asset + '_modify').modal('show');
    },
    remove: function(event) {
      $('#' + this.asset + '_remove').modal('show');
    }
  }
});

// Register VUE Elements
VUE_ELEMENTS.jobs = new Vue({
  el: '#all_jobs_table',
  data: {
    asset: 'jobs',
    more: false,
    modifiable: true,
    removable: true,
    sort_key: 'Creado',
    search_term: '',
    columns: {
      'ID.': {
        order: '',
        referencing: 'id'
      },
      'Creado': {
        order: 'des',
        referencing: 'created'
      },
      'Puesto': {
        order: '',
        referencing: 'title'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.requisitions = new Vue({
  el: '#all_requisitions_table',
  data: {
    asset: 'candidates',
    more: true,
    modifiable: true,
    removable: true,
    sort_key: 'Creado',
    search_term: '',
    columns: {
      'ID.': {
        order: '',
        referencing: 'id'
      },
      'Creado': {
        order: 'des',
        referencing: 'created'
      },
      'Puesto': {
        order: '',
        referencing: 'job'
      },
      'Candidato': {
        order: '',
        referencing: 'candidate'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.candidates = new Vue({
  el: '#all_candidates_table',
  data: {
    asset: 'candidates',
    more: true,
    modifiable: true,
    removable: true,
    sort_key: 'Creado',
    search_term: '',
    columns: {
      'ID.': {
        order: '',
        referencing: 'id'
      },
      'Creado': {
        order: 'des',
        referencing: 'created'
      },
      'Nombre': {
        order: '',
        referencing: 'name'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.users = new Vue({
  el: '#all_users_table',
  data: {
    asset: 'users',
    more: false,
    modifiable: true,
    removable: true,
    sort_key: 'ID.',
    search_term: '',
    columns: {
      'ID.': {
        order: 'asc',
        referencing: 'id'
      },
      'Creado': {
        order: '',
        referencing: 'created'
      },
      'Nombre': {
        order: '',
        referencing: 'username'
      },
      'Acceso': {
        order: '',
        referencing: 'access'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.logs = new Vue({
  el: '#all_logs_table',
  data: {
    asset: 'logs',
    more: false,
    modifiable: false,
    removable: false,
    sort_key: 'Fecha y Hora',
    search_term: '',
    columns: {
      'Fecha y Hora': {
        order: 'des',
        referencing: 'created'
      },
      'Responsable': {
        order: '',
        referencing: 'responsible'
      },
      'Movimiento': {
        order: '',
        referencing: 'action'
      },
      'Identificador': {
        order: '',
        referencing: 'asset_id'
      }
    },
    data: []
  }
});