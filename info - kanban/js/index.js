$(function() {
  // Setup dialog menus(popups)
  $("#Configuration_Menu").dialog({
    autoOpen: false,
    width: window.innerWidth / 1.3,
    height: window.innerHeight / 1.3,
    modal: true,
  });
  $("#Configuration_Tabs").tabs();

  $("#user_create").dialog({
    autoOpen: false,
    width: window.innerWidth / 2,
    height: window.innerHeight / 2,
    modal: true,
  });

  $("#user_rename").dialog({
    autoOpen: false,
    width: window.innerWidth / 2,
    height: window.innerHeight / 2,
    modal: true,
  });

  $("#user_repassword").dialog({
    autoOpen: false,
    width: window.innerWidth / 2,
    height: window.innerHeight / 2,
    modal: true,
  });

  $("#user_reaccess").dialog({
    autoOpen: false,
    width: window.innerWidth / 2,
    height: window.innerHeight / 2,
    modal: true,
  });

  $("#user_remove").dialog({
    autoOpen: false,
    width: window.innerWidth / 2,
    height: window.innerHeight / 2,
    modal: true,
  });

  $("#Login_Menu").dialog({
    autoOpen: false,
    width: window.innerWidth / 1.3,
    height: window.innerHeight / 1.3,
    modal: true
  });


  $("#Category_Menu").dialog({
    autoOpen: false,
    width: window.innerWidth / 1.3,
    height: window.innerHeight / 1.3,
    modal: true
  });


  $("#Task_Menu").dialog({
    autoOpen: false,
    width: window.innerWidth / 1.3,
    height: window.innerHeight / 1.3,
    modal: true
  });
});

var current_user, current_table, current_task;

// USER FUNCTIONS
// *****************************************************************************


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
        var doms = '<div class="user" data-id="' + response.data.user.id + '"> <span class="name">' + response.data.user.name + '</span> <input type="button" onclick="$(`#user_rename`).dialog(`open`);" value="Rename" class="option" /> <input type="button" onclick="$(`#user_repassword`).dialog(`open`);" value="Change Password" class="option" /> <input type="button" onclick="$(`#user_reaccess`).dialog(`open`);" value="Change Access Level" class="option" /> <input type="button" onclick="$(`#user_remove`).dialog(`open`);" value="Remove" class="option" /> </div>';
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
        $("#user_rename").dialog("close");
        $("#user_repassword").dialog("close");
        $("#user_reaccess").dialog("close");

        return true;
      }
    }
  });
}


// TABLE FUNCTIONS
// *****************************************************************************


/**
 * Creat and setup a Kanban table; server and client side.
 * @return {object} Returns object that's a DOM.
 */
function createTable() {
  // Setup created table
  var dom = createTableDOM(table_id);

  return dom;
}


/**
 * Delete a Kanban table; server and client side.
 * @param {string} table_id ID belonging to table.
 * @return {boolean} Returns true if sucesfully deleted; false, if unsucesfully deleted.
 */
function deleteTable(table_id) {

}


/**
 * Modify a Kanban table; server and client side.
 * @param {string} table_id ID belonging to task's table.
 * @param {object} data Data being modified in table.
 * @return {boolean} Returns true if sucesfully modified; false, if unsucesfully modified.
 */
function modifyTable(table_id, data) {

}


/**
 * Creat and setup a Kanban table, DOM; client side.
 * @return {object} Returns object that's a DOM.
 */
function createTableDOM() {
  var dom;

  // Setup created table
  setupTableDOM(dom);

  return dom;
}


/**
 * Setup a Kanban table, DOM; client side.
 * @param {object} dom Object that's a DOM.
 * @return {boolean} Returns true if sucesfully setup; false, if unsucesfully setup.
 */
function setupTableDOM(dom) {

}


// TASK FUNCTIONS
// *** --- ***


/**
 * Creat and setup a Kanban task for a given table; server and client side.
 * @param {string} table_id ID of table in which to create task.
 * @return {object} Returns object that's a DOM.
 */
function createTask() {
  // Setup created table
  var dom = createTaskDOM(task_id);

  return dom;
}


/**
 * Delete a Kanban task; server and client side.
 * @param {string} table_id ID belonging to task's table.
 * @param {string} task_id ID belonging to task.
 * @return {boolean} Returns true if sucesfully deleted; false, if unsucesfully deleted.
 */
function deleteTask(table_id, task_id) {

}


/**
 * Modify a Kanban task; server and client side.
 * @param {string} table_id ID belonging to task's table.
 * @param {string} task_id ID belonging to task.
 * @param {object} data Data being modified in task.
 * @return {boolean} Returns true if sucesfully modified; false, if unsucesfully modified.
 */
function modifyTask(table_id, task_id, data) {

}


/**
 * Creat and setup a Kanban task for a given table, DOM; client side.
 * @param {string} table_id ID of table in which to create task.
 * @return {object} Returns object that's a DOM.
 */
function createTaskDOM(table_id) {
  var dom;

  // Setup created table
  setupTaskDOM(dom);

  return dom;
}


/**
 * Setup a Kanban task, DOM; client side.
 * @param {object} dom Object that's a DOM.
 * @return {boolean} Returns true if sucesfully setup; false, if unsucesfully setup.
 */
function setupTaskDOM(dom) {

}