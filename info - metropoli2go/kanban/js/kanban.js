var GLOBALS = {
  dad: undefined, // Dragula instance
  asset: undefined, // Asset being handled
  section: undefined, // Section being handled
  landmark: undefined // Landmark being handled
};

window.onload = function() {

  // Setup Dragula.js
  var dad = dragula({
    isContainer: function(el) {
      return el.classList.contains("landmarks");
    }
  });

  dad.on("drop", function(el, target, source, sibling) {
    $(el).attr("data-table-id", $(target).attr("data-table-id"));

    modifyTask($(el).attr("data-task-id"), {
      "table_id": $(target).attr("data-table-id")
    });
  });

  // // Setup FileDrop.js
  // $("#task_file_dropzone").filedrop({
  //   fallback_id: "new_files",
  //   fallback_dropzoneClick: true,
  //   withCredentials: true,
  //   data: {
  //     "task_id": function() {
  //       return current_task;
  //     }
  //   },
  //   url: "./php/API.php/files/create",
  //   error: function(err, file) {
  //     console.log(err);
  //     switch (err) {
  //       case 'BrowserNotSupported':
  //         alert('browser does not support HTML5 drag and drop');
  //         break;
  //       case 'TooManyFiles':
  //         break;
  //       case 'FileTooLarge':
  //         break;
  //       case 'FileTypeNotAllowed':
  //         break;
  //       case 'FileExtensionNotAllowed':
  //         break;
  //       default:
  //         break;
  //     }
  //   },
  //   uploadStarted: function(i, file, len) {},
  //   uploadFinished: function(i, file, response, time) {
  //     if (response.status === "success") {
  //       for (var file in Object.keys(response.data.files)) {
  //         file = response.data.files[file];
  //         $("#task_file_preview").append('<div onclick="current_file = $(this).attr(\'data-file-id\');" class="file" data-table-id="' + current_table + '" data-task-id="' + current_task + '" data-file-id="' + file.id + '" data-file-url="' + file.url + '"> <span class="name selectable" onclick="$(\'.file-preview\').attr(\'src\',\'' + file.url + '\'); $(\'#file_preview\').dialog(\'open\');"> ' + file.name + ' </span> <input type="button" value="&#10006;" onclick="removeFile(' + file.id + ');" /> </div>');
  //       }
  //
  //       if (prevent_popups == false) {
  //         alert(response.reason);
  //       }
  //     } else {
  //       alert(response.reason);
  //     }
  //   },
  //   progressUpdated: function(i, file, progress) {},
  //   globalProgressUpdated: function(progress) {},
  //   speedUpdated: function(i, file, speed) {},
  //   rename: function(name) {},
  //   beforeEach: function(file) {},
  //   beforeSend: function(file, i, done) {
  //     done();
  //   },
  //   afterAll: function() {}
  // });
};

// CONTENT Functions
// *****************************************************************************

/**
 * Scroll Kanban content to the left.
 * @return {undefined} Returns nothing.
 */
function scroll_left() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position - 150
  }, 100);
}


/**
 * Scroll Kanban content to the right.
 * @return {undefined} Returns nothing.
 */
function scroll_right() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position + 150
  }, 100);
}


/**
 * Refresh content with given identifier.
 * @param {String} identifier Identifier of content to be refreshed.
 * @return {undefined} Returns nothing.
 */
function refresh(identifier) {
  switch (identifier) {
    case 'kanban':
      (async function() {
        VUE_ELEMENTS.kanban.data = JSON.parse(await kanban_get()).data.dump || [];
      })();

      break;
    case 'users':
      (async function() {
        VUE_ELEMENTS.users.data = JSON.parse(await users_get()).data.dump || [];
      })();

      break;
    case 'logs':
      (async function() {
        VUE_ELEMENTS.logs.data = JSON.parse(await logs_get()).data.dump || [];
      })();

      break;
    default:
      return;
  }
}

// KANBAN Functions
// *****************************************************************************

/**
 * Retrieve kanban.
 * @return {undefined} Returns nothing.
 */
async function kanban_get() {
  return await $.get({
    url: './server/api.php/kanban/get?debug=' + DEBUGGING.server,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {}

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// SECTION Functions
// *****************************************************************************
$('#section_modify').on('shown.bs.modal', function(e) {
  $(this).find('form :input').each(function() {
    $(this).val(GLOBALS.section[this.name]);
  });
});

/**
 * Retrieve a section from sections.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
async function sections_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/sections/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
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
 * Add a section.
 * @param {object} data Data to create section with.
 * @return {undefined} Returns nothing.
 */
function sections_add(data) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/sections/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        name: data.name || 'Nueva Sección'
      }
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a section's data.
 * @param {integer} id section's ID.
 * @param {object} data Data to modify section with.
 * @return {undefined} Returns nothing.
 */
function sections_modify(id, data) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'name'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/sections/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a section from sections.
 * @param {integer} id section's ID.
 * @return {undefined} Returns nothing.
 */
function sections_remove(id) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/sections/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// LANDMARK Functions
// *****************************************************************************
$('#landmark_modify').on('shown.bs.modal', function(e) {
  $(this).find('form :input').each(function() {
    $(this).val(GLOBALS.landmark[this.name]);
  });
});

/**
 * Retrieve a landmark from landmarks.
 * @param {Object} filter Filter used to retrieve with.
 * @param {Object} options Options used to retrieve with.
 * @return {undefined} Returns nothing.
 */
async function landmarks_get(filter, options) {
  filter = typeof filter === 'object' ? filter : {};
  options = typeof options === 'object' ? options : {};

  return await $.get({
    url: './server/api.php/landmarks/get?debug=' + DEBUGGING.server + '&filter=' + JSON.stringify(filter) + '&options=' + JSON.stringify(options),
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
 * Add a landmark.
 * @param {object} data Data to create landmark with.
 * @return {undefined} Returns nothing.
 */
function landmarks_add(data) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/landmarks/add?debug=' + DEBUGGING.server,
    data: {
      data: {
        section: data.section || 0,
        name: data.name || 'Nuevo Landmark'
      }
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a landmark's data.
 * @param {integer} id landmark's ID.
 * @param {object} data Data to modify landmark with.
 * @return {undefined} Returns nothing.
 */
function landmarks_modify(id, data) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  var valid = [
    'name',
    'classification',
    'latitude',
    'longitude',
    'summary',
    'urls'
  ];
  var valid_data = {};
  $.each(data, function(key, value) {
    if (value && valid.indexOf(key) !== -1) {
      valid_data[key] = value;
    }
  });

  $.post({
    url: './server/api.php/landmarks/modify?debug=' + DEBUGGING.server,
    data: {
      id: id,
      data: data
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a landmark from landmarks.
 * @param {integer} id landmark's ID.
 * @return {undefined} Returns nothing.
 */
function landmarks_remove(id) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/landmarks/remove?debug=' + DEBUGGING.server,
    data: {
      id: id
    },
    success: function(response) {
      $('body').waitMe('hide');

      response = JSON.parse(response);

      if (response.status === 'success') {
        VUE_ELEMENTS.kanban.data = response.data.dump || [];
      }

      if (response.status === 'failure' || DEBUGGING.popups) {
        alert(response.reason);
      }
    }
  });
}

// USER Functions
// *****************************************************************************
$('#user_modify').on('shown.bs.modal', function(e) {
  $(this).find('form :input').each(function() {
    $(this).val(GLOBALS.asset[this.name]);
  });
});

/**
 * Retrieve user(s) from users.
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
 * @param {object} data Data to create user with.
 * @return {undefined} Returns nothing.
 */
function users_add(data) {
  $('#settings-users').waitMe({
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
      $('#settings-users').waitMe('hide');

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
  $('#settings-users').waitMe({
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
      $('#settings-users').waitMe('hide');

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
  $('#settings-users').waitMe({
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
      $('#settings-users').waitMe('hide');

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

// LOG Functions
// *****************************************************************************

/**
 * Retrieve log(s) from logs.
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
 * Clear the log of all logs.
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
      $('#logs').waitMe('hide');

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

// VUE.js Code
// *****************************************************************************
var VUE_ELEMENTS = {};

window.onload = function() {
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

  // Register Table component
  Vue.component('kanban-component', {
    template: '#kanban-component',
    props: {
      search_term: String,
      data: Array
    },
    data: function() {},
    computed: {
      filtered_data: function() {
        var data = this.data;

        // if (this.search_term && this.data.length > 0) {
        //   // Search for valid data
        //   if (this.search_term) {
        //     var search_term = this.search_term;
        //     data = data.filter(function(entry) {
        //       return Object.keys(row).some(function(key) {
        //         return String(row[key]).toLowerCase().indexOf(search_term) > -1;
        //       });
        //     });
        //   }
        // }

        return data;
      }
    },
    filters: {
      capitalize: function(str) {
        return str.charAt(0).toUpperCase() + str.slice(1);
      }
    },
    methods: {
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
  VUE_ELEMENTS.kanban = new Vue({
    el: '#kanban',
    data: {
      search_term: '',
      data: []
    }
  });

  VUE_ELEMENTS.users = new Vue({
    el: '#users',
    data: {
      asset: 'user',
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
    el: '#logs',
    data: {
      asset: 'log',
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
          referencing: 'asset'
        }
      },
      data: []
    }
  });

  refresh('kanban');
};