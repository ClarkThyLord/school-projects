// GLOBAL Variables
var GLOBALS = {
  user: undefined, // Client's current user
  asset: undefined, // Current asset being interacted with
  eng_to_spa: {
    jobs: 'puestos',
    quotations: 'cotizaciónes',
    requisitions: 'requisiciones',
    candidates: 'candidatos',
    users: 'usuarios',
    logs: 'registros'
  }
};

window.onload = function() {
  content_change('desk');

  // Setup forms
  (async function() {
    var forms = [
      'quotations',
      'requisitions',
      'candidates'
    ];

    for (var form in forms) {
      $('#' + forms[form] + '_add_info').html(form_to_html(await forms_format_get(forms[form])));
      $('#' + forms[form] + '_data_modify_info').html(form_to_html(await forms_format_get(forms[form])));
    }
  })();
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
    filename: (GLOBALS.eng_to_spa[content] || 'custom') + '.pdf',
    html2canvas: {},
    jsPDF: {
      orientation: 'landscape'
    }
  });
}


/**
 * Export html to a PDF.
 * @param {string} html HTML to export to a PDF.
 * @return {undefined} Returns nothing.
 */
function html_export(html) {
  html2pdf(html, {
    margin: 10,
    filename: (GLOBALS.eng_to_spa[content] || 'custom') + '.pdf',
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

      VUE_ELEMENTS.recent_quotations.data = JSON.parse(await quotations_get({}, {
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
  } else if (content === 'search') {
    (async function() {
      await search_setup($('#search').data('search-term'));

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

// SEARCH Functions
// *****************************************************************************

/**
 * Search and setup matching data from database.
 * @param {String} term Term to search databse with.
 * @return {undefined} Returns nothing.
 */
async function search_setup(term) {
  await (async function(term) {
    search_get(term).then(function(data) {
      data = JSON.parse(data);

      VUE_ELEMENTS.search_jobs.data = data.data.jobs;

      VUE_ELEMENTS.search_quotations.data = data.data.quotations;

      VUE_ELEMENTS.search_requisitions.data = data.data.requisitions;

      VUE_ELEMENTS.search_candidates.data = data.data.candidates;
    });
  })(term || '');
}


/**
 * Search databse for matching data.
 * @param {String} term Term to search databse with.
 * @return {undefined} Returns nothing.
 */
async function search_get(term) {
  return await $.get({
    url: './server/api.php/search?debug=' + DEBUGGING.server + '&term=' + term || '',
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// FORMS Functions
// *****************************************************************************

/**
 * Retrieve a form's format.
 * @param {String} identifier Form's identifier.
 * @return {undefined} Returns nothing.
 */
async function forms_format_get(identifier) {
  return await $.getJSON('./assets/forms/' + identifier + '.json');
}


/**
 * Setup a form in form's view.
 * @param {String} identifier Form's ID to be setup.
 * @param {Object} data Data to setup form with.
 * @return {undefined} Returns nothing.
 */
function setup_form(identifier, data) {
  if (typeof data === 'string') {
    data = JSON.parse(data);
  }

  $('#' + identifier + '_data_modify_info :input').each(function(num) {
    if ($(this).attr('type') === 'checkbox') {
      $(this).prop('checked', !!(data['id_' + num] * 1));
    } else if ($(this).attr('type') === 'file') {
      $(this).after('<a href="">Descargar Archivo</a>');
    } else {
      $(this).val(data['id_' + num]);
    }
  });

  $('#' + identifier + '_data_modify').modal('show');
}


/**
 * Convert a object form to a html.
 * @param {Object} dom Object form.
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

        for (var option in value.extra.options) {
          $(dom).children('select').append('<option value="' + value.extra.options[option] + '">' + value.extra.options[option] + '</option>');
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
function html_to_data(dom) {
  var required = false;
  var required_fields = [];
  var data = {};
  $(dom).find(':input').each(function(num) {
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
        for (var file in Object.keys(files)) {
          file = files[file];

          var reader = new FileReader();
          reader.addEventListener("load", function() {
            data['id_' + num][file.name] = reader.result;
          }, false);

          if (file) {
            reader.readAsDataURL(file);
          }
        }
      }
    } else {
      data['id_' + num] = $(this).val();
    }
  });

  if (required) {
    alert('Debe completar lo siguiente:\n' + required_fields.join('\n'));
  } else {
    return data;
  }
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

// QUOTATIONS Functions
// *****************************************************************************

$('#quotations_modify').on('shown.bs.modal', function(e) {
  $('#quotations_modify_info :input').each(function() {
    if ($(this).attr('type') === 'checkbox') {
      $(this).prop('checked', !!(GLOBALS.asset[this.name] * 1));
    } else if (GLOBALS.asset[this.name]) {
      $(this).val(GLOBALS.asset[this.name]);
    }
  });
});


/**
 * Retrieve a quotation from quotations.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
async function quotations_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/quotations/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
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
 * Add a quotation.
 * @param {object} data Data to create quotation with.
 * @return {undefined} Returns nothing.
 */
function quotations_add(data) {
  $('#quotations').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/quotations/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        'company name': data['company name'] || 'Nuevo Empresa',
        job: data.job || 'Posición Vacante',
        data: data.data || {}
      }
    },
    success: function(response) {
      $('#quotations').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.all_quotations.data = VUE_ELEMENTS.recent_quotations.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a quotation's data.
 * @param {integer} id quotation's ID.
 * @param {object} data Data to modify quotation with.
 * @return {undefined} Returns nothing.
 */
function quotations_modify(id, data) {
  $('#quotations').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'company name',
    'job',
    'data',
    'active'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/quotations/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('#quotations').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.all_quotations.data = VUE_ELEMENTS.recent_quotations.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a quotation.
 * @param {integer} id quotation's ID.
 * @return {undefined} Returns nothing.
 */
function quotations_remove(id) {
  $('#quotations').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/quotations/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('#quotations').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.all_quotations.data = VUE_ELEMENTS.recent_quotations.data = response.data.dump || [];
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
    } else if (GLOBALS.asset[this.name]) {
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
        'company name': data['company name'] || 'Nueva Empresa',
        data: data.data
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
    } else if (GLOBALS.asset[this.name]) {
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
        name: data.name || 'Nuevo Candidato',
        data: data.data || {}
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
    information: function(event, asset) {
      setup_form(this.asset, asset.data || {});
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

VUE_ELEMENTS.search_jobs = new Vue({
  el: '#search_jobs_table',
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

VUE_ELEMENTS.all_quotations = new Vue({
  el: '#all_quotations_table',
  data: {
    asset: 'quotations',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
      },
      'Puesto': {
        order: '',
        referencing: 'job'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.search_quotations = new Vue({
  el: '#search_quotations_table',
  data: {
    asset: 'quotations',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
      },
      'Puesto': {
        order: '',
        referencing: 'job'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.recent_quotations = new Vue({
  el: '#recent_quotations_table',
  data: {
    asset: 'quotations',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
      },
      'Puesto': {
        order: '',
        referencing: 'job'
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
    asset: 'requisitions',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
      },
      'Activo': {
        order: '',
        referencing: 'active'
      }
    },
    data: []
  }
});

VUE_ELEMENTS.search_requisitions = new Vue({
  el: '#search_requisitions_table',
  data: {
    asset: 'requisitions',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
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
    asset: 'requisitions',
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
      'Nombre de Empresa': {
        order: '',
        referencing: 'company name'
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

VUE_ELEMENTS.search_candidates = new Vue({
  el: '#search_candidates_table',
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