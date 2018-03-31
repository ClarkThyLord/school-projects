var drag_and_drop;

$(function() {
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

  // Setup dragula(drag and drop)
  // drag_and_drop = dragula($(".kanban > .table > .container").toArray());
  var drag_and_drop = dragula({
    isContainer: function(el) {
      return el.classList.contains("dragula-container");
    }
  });
});

var current_user, current_table, current_task;

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
      }

      alert(response.reason);
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
      }

      alert(response.reason);
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

      alert(response.reason);

      if (response.status === "success") {
        $(".user[data-id='" + response.data.user_id + "']").remove();
        $(".dialog-menu-mini").dialog("close");

        return true;
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
        var doms = '<div class="table" onclick="current_table = $(this).attr(`data-id`);" data-id="' + response.data.table.id + '"> <div class="header"> <span class="name selectable" onclick="$(`#table_rename`).dialog(`open`)">' + response.data.table.name + '</span> <hr /> <label> Search: <br /> <input type="text" placeholder="Search term..." data-table="' + response.data.table.id + '" oninput="seachForTaskInTable(current_table, this.value);" /> </label> <br /><span class="item selectable" onclick="$(`#table_remove`).dialog(`open`);"> &#128465; </span> <span class="item selectable" onclick="$(`#task_create`).dialog(`open`);"> Add + </span> </div> <form class="dragula-container" data-table="' + response.data.table.id + '"></form> </div>';

        $(".kanban").append(doms);
        $("#table_create").dialog("close");
      }

      alert(response.reason);
    }
  });
}


/**
 * Remove a table.
 * @param {integer} table_id Table's ID.
 * @return {undefined} Returns nothing.
 */
function removeTable(table_id) {
  $.post({
    url: "./php/API.php/table/remove",
    data: {
      "table_id": table_id
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === "success") {
        $(".kanban > .table[data-id='" + response.data.table_id + "']").remove();
        $("#table_remove").dialog("close");
      }

      alert(response.reason);
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

      alert(response.reason);

      if (response.status === "success") {
        $(".dialog-menu-mini").dialog("close");

        return true;
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
    $(".kanban > .table[data-id='" + table_id + "']").each(function() {
      $(this).find(".dragula-container .item").each(function() {
        $(this).show();
      });
    });
  } else {
    $(".kanban > .table[data-id='" + table_id + "']").each(function() {
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
 * Create a task.
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
        var doms = '<div class="item selectable" onclick="current_task = $(this).attr(`name`); $(`#task_modify`).dialog(`open`);" data-table="' + response.data.task.table_id + '" name="' + response.data.task.id + '">' + response.data.task.name + '</div>';

        $(".kanban > .table[data-id='" + response.data.task.table_id + "'] > .dragula-container").append(doms);
        $("#task_create").dialog("close");
      }

      alert(response.reason);
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
        $(".kanban > .table[data-id='" + response.data.table_id + "'] > .dragula-container > .item[name='" + response.data.task_id + "']").remove();
        $("#task_remove").dialog("close");
      }

      alert(response.reason);
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

      alert(response.reason);

      if (response.status === "success") {
        $(".dialog-menu-mini").dialog("close");

        return true;
      }
    }
  });
}