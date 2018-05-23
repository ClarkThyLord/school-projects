var GLOBALS = {
  dad: undefined
};

window.onload = function() {
  // // Setup Dragula.js
  // var dad = dragula({
  //   isContainer: function(el) {
  //     return el.classList.contains("dragula-container");
  //   }
  // });
  //
  // dad.on("drop", function(el, target, source, sibling) {
  //   $(el).attr("data-table-id", $(target).attr("data-table-id"));
  //
  //   modifyTask($(el).attr("data-task-id"), {
  //     "table_id": $(target).attr("data-table-id")
  //   });
  // });
  //
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

// VUE.js Code
// *****************************************************************************
var VUE_ELEMENTS = {};

window.onload = function() {
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

        if (this.search_term && this.data.length > 0) {
          // Search for valid data
          if (this.search_term) {
            var search_term = this.search_term;
            data = data.filter(function(entry) {
              return Object.keys(row).some(function(key) {
                return String(row[key]).toLowerCase().indexOf(search_term) > -1;
              });
            });
          }
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
    el: '#logs',
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
};