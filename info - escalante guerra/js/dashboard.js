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
 * @param {string} content Content's identifier.
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
          VUE_ELEMENTS[content].data = response.data.dump;
        }

        if (response.status === 'failure' || DEBUGGING.popups) {
          alert(response.reason);
        }
      }
    });
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
 * Clear the all the logs.
 * @return {undefined} Returns nothing.
 */
function jobs_get() {
  $('#jobs').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/jobs/clear?debug=' + DEBUGGING.server,
    success: function(response) {
      $('#jobs').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.jobs.data = response.data.dump;
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
        VUE_ELEMENTS.jobs.data = response.data.dump;
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
        VUE_ELEMENTS.jobs.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Clear the all the logs.
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
        VUE_ELEMENTS.jobs.data = response.data.dump;
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
 * Clear the all the logs.
 * @return {undefined} Returns nothing.
 */
function users_get() {
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
      $('#users').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.logs.data = response.data.dump;
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
        VUE_ELEMENTS.users.data = response.data.dump;
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
        VUE_ELEMENTS.users.data = response.data.dump;
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Clear the all the logs.
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
        VUE_ELEMENTS.users.data = response.data.dump;
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
 * Clear the all the logs.
 * @return {undefined} Returns nothing.
 */
function logs_get() {}


/**
 * Clear the all the logs.
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
        VUE_ELEMENTS.logs.data = response.data.dump;

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
    selected: function(event, asset) {
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
    modifiable: true,
    removable: true,
    sort_key: 'Publicado',
    search_term: '',
    columns: {
      'ID.': {
        order: '',
        referencing: 'id'
      },
      'Publicado': {
        order: 'asc',
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

// Register VUE Elements
VUE_ELEMENTS.users = new Vue({
  el: '#all_users_table',
  data: {
    asset: 'users',
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