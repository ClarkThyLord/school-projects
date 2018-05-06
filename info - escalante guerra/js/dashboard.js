/**
 * Change to content via their tag.
 * @param {string} content Content's tag.
 * @return {undefined} Returns nothing.
 */
function content_change(content) {
  // Close other content and open specified content
  $('.page-content').hide();
  $('#' + content).show();

  if (content === 'users') {
    $.get({
      url: './server/api.php/users/get?debug=' + DEBUGGING + '&filter=' + JSON.stringify({}) + '&options=' + JSON.stringify({}),
      success: function(response) {
        response = JSON.parse(response);

        all_users_table.data = response.data.users;
      }
    });
  }

  // Change current in MENU
  $('.nav-item > .active').removeClass('active');
  $('.nav-item[data-location="' + content + '"] > a').addClass('active');
}

/**
 * Portrait given user and users in the page.
 * @param {object} user User belonging to client.
 * @param {object} users Users to portrait.
 * @return {undefined} Returns nothing.
 */
function user_table(user, users) {}

// VUE.js Code
// *****************************************************************************

// Register Table component
Vue.component('table-component', {
  template: '#table-component',
  props: {
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
    }
  }
});

var all_users_table = new Vue({
  el: '#all_users_table',
  data: {
    search_term: '',
    visual_columns: ['ID.', 'Creado', 'Nombre', 'Acceso', 'Acciónes'],
    real_columns: ['id', 'created', 'username', 'access'],
    data: []
  }
});