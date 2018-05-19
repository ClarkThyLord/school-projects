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

window.onload = function() {
  content_change('desk');

  // (async function() {
  //   $('#forms_view_info').html(form_to_html(await forms_format_get('candidates')));
  //   $('#forms_view').modal('show');
  // })();
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
  $('#' + content).waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  if (content === 'desk') {
    (async function() {
      VUE_ELEMENTS.recent_jobs.data = JSON.parse(await jobs_get({}, {
        limit: 5
      })).data.dump || [];

      VUE_ELEMENTS.recent_requisitions.data = JSON.parse(await requisitions_get({}, {
        limit: 5
      })).data.dump || [];

      VUE_ELEMENTS.recent_candidates.data = JSON.parse(await candidates_get({}, {
        limit: 5
      })).data.dump || [];

      VUE_ELEMENTS.recent_logs.data = JSON.parse(await logs_get({}, {
        limit: 5
      })).data.dump || [];

      $('#' + content).waitMe('hide');
    })();
  } else {
    $.get({
      url: './server/api.php/' + content + '/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify({}) + '&options=' + JSON.stringify({}),
      success: function(response) {
        $('#' + content).waitMe('hide');

        response = JSON.parse(response);
        if (response.status === 'success') {
          VUE_ELEMENTS['all_' + content].data = response.data.dump || [];
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
 * Convert a object form to a html.
 * @param {object} dom Object form.
 * @return {undefined} Returns html.
 */
function form_to_html(data) {
  var num = 0;
  var current_step = 0;
  var steps = [$('<div class="form-group" data-step="0"></div>')];
  for (var key in data) {
    value = data[key];

    num++;

    var dom = $('<div></div>');

    // Setup label
    if (value.label) $(dom).append('<label class="col-form-label">' + value.label + ':' + (value.extra && value.extra.comment ? (' <span style="font-size: 10px; opacity: 0.7;">' + value.extra.comment + '</span>') : '') + '</label>');

    switch (value.type) {
      case 'break':
        current_step++;
        steps.push($('<div data-step="' + current_step + '" style="display: none;" class="form-group">' + (value.extra && value.extra.title ? '<h1 class="h2">' + value.extra.title + '</h1>' : '') + '</div>'));
        break;
      case 'text':
        $(dom).append('<input type="text" placeholder="Inserte aquí..." class="form-control" />').prop("required", value.required);
        break;
      case 'textarea':
        $(dom).append('<textarea placeholder="Inserte aquí..." class="form-control"></textarea>').prop("required", value.required);
        break;
      case 'date':
        $(dom).append('<input type="date" class="form-control" />').prop("required", value.required);
        break;
      case 'email':
        $(dom).append('<input type="email" placeholder="ejemplo@gmail.com" class="form-control" />').prop("required", value.required);
        break;
      case 'phonenumber':
        $(dom).append('<input type="tel" placeholder="(###) ### - ####" class="form-control" />').prop("required", value.required);
        break;
      case 'dropdown':
        $(dom).append('<select class="form-control"></select>').prop("required", value.required);

        for (var option in value.extra.options) {
          $(dom).children('select').append('<option value="' + value.extra.options[option] + '">' + value.extra.options[option] + '</option>');
        }
        break;
      case 'file':
        $(dom).append('<input type="file" class="form-control" />').prop("required", value.required);
        break;
      case 'files':
        $(dom).append('<input type="file" multiple class="form-control" />').prop("required", value.required);
        break;
      default:
        console.log(value.type);
        continue;
    }

    // Give proper DOM attributes
    $(dom).attr('name', value.label || ('new_value_' + num));

    steps[current_step].append($(dom).children());
  }

  if (steps.length > 1) {
    steps.push($('<button type="button" onclick="var form = $(this).parent(); if (parseInt(form.data(\'current-step\')) > 0) { form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').hide(); form.data(\'current-step\', \'\' + (parseInt(form.data(\'current-step\')) - 1)); form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').show(); }" class="btn btn-primary">Anterior</button> <button type="button" onclick="var form = $(this).parent(); if ((parseInt(form.data(\'current-step\')) + 1) < form.children(\'.form-group\').length) { form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').hide(); form.data(\'current-step\', \'\' + (parseInt(form.data(\'current-step\')) + 1)); form.children(\'.form-group[data-step=\' + form.data(\'current-step\') + \']\').show(); }" class="btn btn-primary">Siguiente</button>'))
  }

  return steps;
}


/**
 * Convert a html form to a object containing form's data.
 * @param {object} dom DOM form.
 * @return {undefined} Returns data.
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
async function jobs_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/jobs/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

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
        VUE_ELEMENTS.all_jobs.data = VUE_ELEMENTS.recent_jobs.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_jobs.data = VUE_ELEMENTS.recent_jobs.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_jobs.data = VUE_ELEMENTS.recent_jobs.data = response.data.dump || [];
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
async function requisitions_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/requisitions/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

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
        VUE_ELEMENTS.all_requisitions.data = VUE_ELEMENTS.recent_requisitions.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_requisitions.data = VUE_ELEMENTS.recent_requisitions.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_requisitions.data = VUE_ELEMENTS.recent_requisitions.data = response.data.dump || [];
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
async function candidates_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/candidates/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

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
        VUE_ELEMENTS.all_candidates.data = VUE_ELEMENTS.recent_candidates.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_candidates.data = VUE_ELEMENTS.recent_candidates.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_candidates.data = VUE_ELEMENTS.recent_candidates.data = response.data.dump || [];
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
async function users_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/users/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

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
        VUE_ELEMENTS.all_users.data = VUE_ELEMENTS.recent_users.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_users.data = VUE_ELEMENTS.recent_users.data = response.data.dump || [];
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
        VUE_ELEMENTS.all_users.data = VUE_ELEMENTS.recent_users.data = response.data.dump || [];
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
async function logs_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/logs/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


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
        VUE_ELEMENTS.all_logs.data = VUE_ELEMENTS.recent_logs.data = response.data.dump || [];

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
VUE_ELEMENTS.all_jobs = new Vue({
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

VUE_ELEMENTS.recent_jobs = new Vue({
  el: '#recent_jobs_table',
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

VUE_ELEMENTS.all_requisitions = new Vue({
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

VUE_ELEMENTS.recent_requisitions = new Vue({
  el: '#recent_requisitions_table',
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

VUE_ELEMENTS.all_candidates = new Vue({
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

VUE_ELEMENTS.recent_candidates = new Vue({
  el: '#recent_candidates_table',
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

VUE_ELEMENTS.all_users = new Vue({
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

VUE_ELEMENTS.all_logs = new Vue({
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

VUE_ELEMENTS.recent_logs = new Vue({
  el: '#recent_logs_table',
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