// GLOBAL Variables
var GLOBALS = {
  user: undefined, // Client's current user
  asset: undefined // Current asset being interacted with
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
 * Portrait given user and users in the page.
 * @param {string} content Content's identifier.
 * @return {undefined} Returns nothing.
 */
function content_refresh(content) {
  if (content === 'users' || content === 'logs') {
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
    search_term: String,
    visual_columns: Array,
    real_columns: Array,
    data: Array
  },
  data: function() {
    var sortOrders = {};
    this.real_columns.forEach(function(key) {
      sortOrders[key] = 1;
    });
    return {
      sortKey: '',
      sortOrders: sortOrders
    };
  },
  computed: {
    filteredData: function() {
      var sortKey = this.sortKey;
      var filterKey = this.search_term && this.search_term.toLowerCase();
      var order = this.sortOrders[sortKey] || 1;
      var data = this.data;
      if (filterKey) {
        data = data.filter(function(row) {
          return Object.keys(row).some(function(key) {
            return String(row[key]).toLowerCase().indexOf(filterKey) > -1;
          });
        });
      }
      if (sortKey) {
        data = data.slice().sort(function(a, b) {
          a = a[sortKey];
          b = b[sortKey];
          return (a === b ? 0 : a > b ? 1 : -1) * order;
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
    sortBy: function(key) {
      this.search_term = key;
      this.sortOrders[key] = this.sortOrders[key] * -1;
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
VUE_ELEMENTS.users = new Vue({
  el: '#all_users_table',
  data: {
    asset: 'users',
    modifiable: true,
    removable: true,
    search_term: '',
    visual_columns: ['ID.', 'Creado', 'Nombre', 'Acceso', 'Acciónes'],
    real_columns: ['id', 'created', 'username', 'access'],
    data: []
  }
});

VUE_ELEMENTS.logs = new Vue({
  el: '#all_logs_table',
  data: {
    asset: 'logs',
    modifiable: false,
    removable: false,
    search_term: '',
    visual_columns: ['Fecha y Hora.', 'Responsable', 'Movimiento', 'Identificador'],
    real_columns: ['created', 'responsible', 'action', 'asset_id'],
    data: []
  }
});