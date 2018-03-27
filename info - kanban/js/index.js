$(function() {});


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