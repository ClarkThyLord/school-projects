var html_pdf, drag_and_drop, prevent_popups = true;

$(function() {
  html_pdf = new jsPDF({
    orientation: 'landscape'
  });

  // Setup dialog menus(popups)
  $(".dialog-menu").each(function() {
    $(this).dialog({
      autoOpen: false,
      width: window.innerWidth / 2,
      height: window.innerHeight / 1.5,
      modal: true,
    });
  });
  $(".ui-tabs").tabs({
    active: 0
  });

  $(".dialog-menu-mini").each(function() {
    $(this).dialog({
      autoOpen: false,
      width: window.innerWidth / 2.7,
      height: window.innerHeight / 2.2,
      modal: true,
    });
  });

  $("#Configuration_Menu").on("dialogopen", function(event, ui) {
    $.get({
      url: "./php/API.php/log/construct",
      success: function(response) {
        response = JSON.parse(response);
        if (response.status === "success") {
          $(".log").html(response.data.html);

          if (prevent_popups == false) {
            alert(response.reason);
          }
        } else {
          if (prevent_popups == false) {
            alert(response.reason);
          }
        }
      }
    });
  });

  $("#print_table").on("dialogopen", function(event, ui) {
    $.get({
      url: "./php/API.php/task/construct",
      success: function(response) {
        response = JSON.parse(response);
        if (response.status === "success") {
          $(".print-content").html(response.data.html);

          if (prevent_popups == false) {
            alert(response.reason);
          }
        } else {
          if (prevent_popups == false) {
            alert(response.reason);
          }
        }
      }
    });
  });

  $("#task_file_dropzone").filedrop({
    fallback_id: "new_files",
    fallback_dropzoneClick: true,
    withCredentials: true,
    data: {
      "task_id": function() {
        return current_task;
      }
    },
    url: "./php/API.php/file/create",
    error: function(err, file) {
      console.log(err);
      switch (err) {
        case 'BrowserNotSupported':
          alert('browser does not support HTML5 drag and drop');
          break;
        case 'TooManyFiles':
          break;
        case 'FileTooLarge':
          break;
        case 'FileTypeNotAllowed':
          break;
        case 'FileExtensionNotAllowed':
          break;
        default:
          break;
      }
    },
    uploadStarted: function(i, file, len) {
      // a file began uploading
      // i = index => 0, 1, 2, 3, 4 etc
      // file is the actual file of the index
      // len = total files user dropped
      // console.log("upload started");
      // console.log(i);
      // console.log(file);
      // console.log(len);
    },
    uploadFinished: function(i, file, response, time) {
      // response is the data you got back from server in JSON format.
      // console.log("upload finished");
      // console.log(i);
      // console.log(file);
      // console.log(response);
      // console.log(time);

      if (response.status === "success") {
        for (var file of Object.keys(response.data.files)) {
          file = response.data.files[file];
          $("#task_file_preview").append('<div onclick="current_file = $(this).attr(\'data-file-id\');" class="file" data-table-id="' + current_table + '" data-task-id="' + current_task + '" data-file-id="' + file.id + '" data-file-url="' + file.url + '"> <span class="name selectable" onclick="var win = window.open($(this).parent().attr(\'data-file-url\'), \'_blank\'); win.focus();"> ' + file.name + ' </span> <input type="button" value="&#10006;" onclick="removeFile(' + file.id + ');" /> </div>');
        }

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    },
    progressUpdated: function(i, file, progress) {
      // this function is used for large files and updates intermittently
      // progress is the integer value of file being uploaded percentage to completion
      // console.log("progress updated");
      // console.log(i);
      // console.log(file);
      // console.log(progress);
    },
    globalProgressUpdated: function(progress) {
      // progress for all the files uploaded on the current instance (percentage)
      // ex: $('#progress div').width(progress+"%");
      // console.log("global progress updated");
      // console.log(progress);
    },
    speedUpdated: function(i, file, speed) {
      // speed in kb/s
      // console.log("speed updated");
      // console.log(i);
      // console.log(file);
      // console.log(speed);
    },
    rename: function(name) {
      // name in string format
      // must return alternate name as string
      // console.log("rename");
      // console.log(name);
    },
    beforeEach: function(file) {
      // file is a file object
      // return false to cancel upload
      // console.log("before each");
      // console.log(file);
    },
    beforeSend: function(file, i, done) {
      // file is a file object
      // i is the file index
      // call done() to start the upload
      // console.log("before send");
      // console.log(file);
      // console.log(i);
      // console.log(done);

      done();
    },
    afterAll: function() {
      // runs after all files have been uploaded or otherwise dealt with
      // console.log("after all");
    }
  });
});

// Setup dragula(drag and drop)
// drag_and_drop = dragula($(".kanban > .table > .container").toArray());
var drag_and_drop = dragula({
  isContainer: function(el) {
    return el.classList.contains("dragula-container");
  }
});

drag_and_drop.on("drop", function(el, target, source, sibling) {
  $(el).attr("data-table-id", $(target).attr("data-table-id"));

  modifyTask($(el).attr("data-task-id"), {
    "table_id": $(target).attr("data-table-id")
  });
});

var current_user, current_table, current_task, current_file;

// COMMON FUNCTIONS
// *****************************************************************************


/**
 * Move Kanban view to the left.
 * @return {undefined} Returns nothing.
 */
function kanbanViewLeft() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position - 150
  }, 100);
}


/**
 * Move Kanban view to the right.
 * @return {undefined} Returns nothing.
 */
function kanbanViewRight() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position + 150
  }, 100);
}


// USER FUNCTIONS
// *****************************************************************************


/**
 * Search for a msg in log by a term.
 * @param {string} search_term Term used to search for user.
 * @return {undefined} Returns nothing.
 */
function searchInLog(search_term) {
  if (search_term === "") {
    $(".log > .msg").each(function() {
      $(this).show();
    });
  } else {
    $(".log > .msg").each(function() {
      if ($(this).html().toLowerCase().indexOf(search_term.toLowerCase()) === -1) {
        $(this).hide();
      } else {
        $(this).show();
      }
    });
  }
}


/**
 * Clear log.
 * @return {undefined} Returns nothing.
 */
function clearLog() {
  $.post({
    url: "./php/API.php/log/clear",
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".log > .msg").each(function() {
          $(this).remove();
        });

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


// USER FUNCTIONS
// *****************************************************************************


/**
 * Search for a user by it's name.
 * @param {string} search_term Term used to search for user.
 * @return {undefined} Returns nothing.
 */
function seachForUser(search_term) {
  if (search_term === "") {
    $(".users > .user").each(function() {
      $(this).show();
    });
  } else {
    $(".users > .user").each(function() {
      if ($(this).find(".header > .name").first().html().toLowerCase().indexOf(search_term.toLowerCase()) === -1) {
        $(this).hide();
      } else {
        $(this).show();
      }
    });
  }
}

/**
 * Create a user.
 * @param {string} name New user's name.
 * @param {string} password New user's password.
 * @param {integer} access_level New user's access level.
 * @return {undefined} Returns nothing.
 */
function createUser(name, password, access_level) {
  $.post({
    url: "./php/API.php/user/create",
    data: {
      "name": name,
      "password": password,
      "access": access_level
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        var doms = '<div class="user" onclick="current_user = $(this).attr(`data-id`);" data-id="' + response.data.user.id + '"><span class="header"><span class="name">' + response.data.user.name + '</span></span> <input type="button" onclick="$(`#user_rename`).dialog(`open`);" value="Rename" class="option" /> <input type="button" onclick="$(`#user_repassword`).dialog(`open`); " value="Change Password" class="option" /> <input type="button" onclick="$(`#user_reaccess`).dialog(`open`);" value="Change Access Level" class="option" /> <input type="button" onclick="$(`#user_remove`).dialog(`open`); " value="Remove" class="option" /> </div>';
        $(".users").append(doms);
        $("#user_create").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a user.
 * @param {integer} user_id User's ID.
 * @return {undefined} Returns nothing.
 */
function removeUser(user_id) {
  $.post({
    url: "./php/API.php/user/remove",
    data: {
      "user_id": user_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".user[data-id='" + response.data.user_id + "']").remove();
        $("#user_remove").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a user.
 * @param {integer} user_id User's ID.
 * @param {object} modifications Things going to be modified.
 * @return {undefined} Returns nothing.
 */
function modifyUser(user_id, modifications) {
  var data = Object.assign({
    "user_id": user_id
  }, modifications);
  $.post({
    url: "./php/API.php/user/modify",
    data: data,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".user[data-id='" + response.data.user_id + "']").remove();
        $(".dialog-menu-mini").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


// TABLE FUNCTIONS
// *****************************************************************************


/**
 * Search for a table by it's name.
 * @param {string} search_term Term used to search for user.
 * @return {undefined} Returns nothing.
 */
function seachForTable(search_term) {
  if (search_term === "") {
    $(".kanban > .table").each(function() {
      $(this).show();
    });
  } else {
    $(".kanban > .table").each(function() {
      if ($(this).find(".header > .name").first().html().toLowerCase().indexOf(search_term.toLowerCase()) === -1) {
        $(this).hide();
      } else {
        $(this).show();
      }
    });
  }
}

/**
 * Create a table.
 * @param {string} name New table's name.
 * @return {undefined} Returns nothing.
 */
function createTable(name) {
  $.post({
    url: "./php/API.php/table/create",
    data: {
      "name": name
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        var doms = '<div class="table" onclick="current_table = $(this).attr(\'data-table-id\');" data-table-id="' + response.data.table.id + '"> <div class="header"> <span class="name selectable" onclick="$(\'#table_rename\').dialog(\'open\')">' + response.data.table.name + '</span> <hr /> <label> Search: <br /> <input type="text" placeholder="Search term..." data-table-id="' + response.data.table.id + '" oninput="seachForTaskInTable(current_table, this.value);" /> </label> <br /><span class="item selectable" onclick="$(\'#table_remove\').dialog(\'open\');"> &#128465; </span> <span class="item selectable" onclick="$(\'#task_create\').dialog(\'open\');"> Add + </span> </div> <div class="dragula-container" data-table-id="' + response.data.table.id + '"></div> </div>';

        $(".kanban").append(doms);
        $("#table_create").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a table.
 * @param {integer} table_id Table's ID.
 * @return {undefined} Returns nothing.
 */
function removeTable(table_id) {
  $(".kanban .table[data-table-id=\"" + table_id + "\"] > .dragula-container > .task").each(function() {
    removeTask(table_id, $(this).attr("data-task-id"));
  });

  $.post({
    url: "./php/API.php/table/remove",
    data: {
      "table_id": table_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".table[data-table-id='" + response.data.table_id + "']").remove();
        $("#table_remove").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a table.
 * @param {integer} table_id Table's ID.
 * @param {object} modifications Things going to be modified.
 * @return {undefined} Returns nothing.
 */
function modifyTable(table_id, modifications) {
  var data = Object.assign({
    "table_id": table_id
  }, modifications);
  $.post({
    url: "./php/API.php/table/modify",
    data: data,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".dialog-menu-mini").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


// TASK FUNCTIONS
// *****************************************************************************


/**
 * Search for a task, in a table, by it's name.
 * @param {string} search_term Term used to search for task.
 * @return {undefined} Returns nothing.
 */
function seachForTask(search_term) {
  if (search_term === "") {
    $(".kanban > .table").each(function() {
      $(this).find(".container .item").each(function() {
        $(this).show();
      });
    });
  } else {
    $(".kanban > .table").each(function() {
      $(this).find(".dragula-container .item").each(function() {
        if ($(this).html().toLowerCase().indexOf(search_term.toLowerCase()) === -1) {
          $(this).hide();
        } else {
          $(this).show();
        }
      });
    });
  }
}


/**
 * Search for a task, in a table, by it's name.
 * @param {integer} table_id Table's ID in which to search for task.
 * @param {string} search_term Term used to search for task.
 * @return {undefined} Returns nothing.
 */
function seachForTaskInTable(table_id, search_term) {
  if (search_term === "") {
    $(".kanban > .table[data-table-id='" + table_id + "']").each(function() {
      $(this).find(".dragula-container .item").each(function() {
        $(this).show();
      });
    });
  } else {
    $(".kanban > .table[data-table-id='" + table_id + "']").each(function() {
      $(this).find(".dragula-container .item").each(function() {
        if ($(this).html().toLowerCase().indexOf(search_term.toLowerCase()) === -1) {
          $(this).hide();
        } else {
          $(this).show();
        }
      });
    });
  }
}

/**
 * Get and set a task.
 * @param {string} task_id Task's id.
 * @return {undefined} Returns nothing.
 */
function getAndSetTask(task_id) {
  $.get({
    url: "./php/API.php/task",
    data: {
      "id": task_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".task_input[name='latitude']").val(response.data.tasks[current_task].latitude);
        $(".task_input[name='longitude']").val(response.data.tasks[current_task].longitude);
        $(".task_input[name='summary']").val(response.data.tasks[current_task].summary);
        $(".task_input[name='urls']").val(response.data.tasks[current_task].urls);

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });

  getAndSetTaskFiles(task_id);
}

/**
 * Create a task.
 * @param {integer} table_id New task's table's id.
 * @param {string} name New task's name.
 * @return {undefined} Returns nothing.
 */
function createTask(table_id, name) {
  $.post({
    url: "./php/API.php/task/create",
    data: {
      "table_id": table_id,
      "name": name
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        var doms = '<div class="item task selectable" onclick="current_task = $(this).attr(\'data-task-id\'); getAndSetTask(current_task); $(\'#task_modify\').dialog(\'open\');" data-table-id="' + response.data.task.table_id + '" data-task-id="' + response.data.task.id + '">' + response.data.task.name + '</div>';

        $(".kanban > .table[data-table-id='" + response.data.task.table_id + "'] > .dragula-container").append(doms);
        $("#task_create").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Remove a task.
 * @param {integer} table_id Task's Table ID.
 * @param {integer} task_id Task's ID.
 * @return {undefined} Returns nothing.
 */
function removeTask(table_id, task_id) {
  $.post({
    url: "./php/API.php/task/remove",
    data: {
      "table_id": table_id,
      "task_id": task_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".kanban > .task[data-task-id='" + response.data.task_id + "']").remove();
        $("#task_remove").dialog("close");
        $("#task_modify").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


/**
 * Modify a task.
 * @param {integer} task_id Task's ID.
 * @param {object} modifications Things going to be modified.
 * @return {undefined} Returns nothing.
 */
function modifyTask(task_id, modifications) {
  var data = Object.assign({
    "task_id": task_id
  }, modifications);
  $.post({
    url: "./php/API.php/task/modify",
    data: data,
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".dialog-menu-mini").dialog("close");

        if (prevent_popups === false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}


//  FILE FUNCTIONS
// *****************************************************************************


/**
 * Get and set a task's files.
 * @param {string} task_id Task's id.
 * @return {undefined} Returns nothing.
 */
function getAndSetTaskFiles(task_id) {
  $("#task_file_preview").html("");

  $.get({
    url: "./php/API.php/file",
    data: {
      "task_id": task_id
    },
    success: function(response) {
      response = JSON.parse(response);

      if (response.status === "success") {
        for (var file of Object.keys(response.data.files)) {
          file = response.data.files[file];
          $("#task_file_preview").append('<div onclick="current_file = $(this).attr(\'data-file-id\');" class="file" data-table-id="' + current_table + '" data-task-id="' + current_task + '" data-file-id="' + file.id + '" > <span class="name selectable" onclick="var win = window.open(' + file.url + ', \'_blank\'); win.focus();"> ' + file.name + ' </span> <input type="button" value="&#10006;" onclick="removeFile(' + file.id + ');" /> </div>');
        }

        if (prevent_popups == false) {
          alert(response.reason);
        }
      }
    }
  });
}



/**
 * Remove a file.
 * @param {integer} file_id File ID.
 * @return {undefined} Returns nothing.
 */
function removeFile(file_id) {
  $.post({
    url: "./php/API.php/file/remove",
    data: {
      "file_id": file_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".file[data-file-id='" + response.data.file_id + "']").remove();
        $("#file_remove").dialog("close");

        if (prevent_popups == false) {
          alert(response.reason);
        }
      } else {
        alert(response.reason);
      }
    }
  });
}