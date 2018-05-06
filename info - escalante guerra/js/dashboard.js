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
      url: './server/api.php/' + content + '/get?debug=' + DEBUGGING + '&filter=' + JSON.stringify({}) + '&options=' + JSON.stringify({}),
      success: function(response) {
        response = JSON.parse(response);
        if (response.status === 'success') {
          VUE_ELEMENTS[content].data = response.data.dump;

          $('#' + content).waitMe('hide');
        }

        if (response.status === 'failure' || DEBUGGING) {
          alert(response.reason);
        }
      }
    });
  }
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
    url: './server/api.php/logs/clear?debug=' + DEBUGGING,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {
        VUE_ELEMENTS.logs.data = response.data.dump;

        $('#logs').waitMe('hide');
      }

      if (response.status === 'failure' || DEBUGGING) {
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
    content: String,
    modifiable: Boolean,
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
    edit: function(event) {
      $('#' + this.content + '_edit').modal('show');
    }
  }
});

// Register VUE Elements
VUE_ELEMENTS.users = new Vue({
  el: '#all_users_table',
  data: {
    content: 'users',
    modifiable: true,
    search_term: '',
    visual_columns: ['ID.', 'Creado', 'Nombre', 'Acceso', 'Acciónes'],
    real_columns: ['id', 'created', 'username', 'access'],
    data: []
  }
});

VUE_ELEMENTS.logs = new Vue({
  el: '#all_logs_table',
  data: {
    content: 'logs',
    modifiable: false,
    search_term: '',
    visual_columns: ['Fecha y Hora.', 'Responsable', 'Movimiento', 'Identificador'],
    real_columns: ['created', 'responsible', 'action', 'asset_id'],
    data: []
  }
});