<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:  | Access:
	// -------------------------------------------
	// database   | setup     | POST      | 2^
	// users      | get    	  | GET       | none
	// users      | login     | POST      | none
	// users      | logout    | POST      | none
	// users      | add    	  | POST      | 2^
	// users      | modify    | POST      | self, 2^
	// users      | remove    | POST      | 2^
	// logs       | get       | GET       | none
	// logs       | add       | POST      | none
	// logs       | clear     | POST      | 2^
	// *******************************************

	// Initialize session if not already
	if (!isset($_SESSION)) {
		session_start();
	}

	// Setup Response that will be sent back
	$response = array('status' => 'success', 'reason' => 'initial response', 'data' => array(), 'user' => (isset($_SESSION['user']) ? $_SESSION['user'] : null));

	// Stripping the base URL and getting all the 'routes'
	// ***************************************************************************

	// Get the base URL and remove it from the request URL
	$url = substr($_SERVER['REQUEST_URI'], strlen(implode('/', array_slice(explode('/', 'server.php'), 0, -1)) . '/'));
	// Remove query elements and excess /
	if (strstr($url, '?')) $url = substr($url, 0, strpos($url, '?'));
	$url = '/' . trim($url, '/');
	// Make an array of it
	$url = explode('/', $url);

	// Create a routes array
	$routes = array();
	// If head, this file, has been reached
	$head = false;
	// Remove bad routes from the URL array and add them to valid routes
	foreach ($url as $route){
		 // If that it's not a empty string and it's not this file
    if (trim($route) != '') {
			// Check if route is this file; meaning head
	    if ($route === basename(__FILE__)) {
				$head = true;
			}
			// If true then route is valid
			else if ($head === true) {
				array_push($routes, $route);
			}
    }
	}

	// FOR DEBUGGING
	if (is_debugging()) {
		// Setup debug spot in response
		$GLOBALS['response']['debug'] = Array();

		$GLOBALS['response']['debug']['session'] = $_SESSION;
		$GLOBALS['response']['debug']['method'] = $_SERVER['REQUEST_METHOD'];
		$GLOBALS['response']['debug']['routes'] = $routes;
	}

	// Include Extensions
	// ***************************************************************************
	include_once './db/db.php';

	// Common Endpoints
	// ***************************************************************************
	include_once './endpoints/logs.php';

	// Common Functions
	// ***************************************************************************

	/**
	* Check whether currently debugging.
	* @return {boolean} Returns true, if debugging; false, if not debugging.
	*/
	function is_debugging() {
		return (isset($_GET['debug']) && $_GET['debug'] == true);
	}


	/**
	* Checks whether client has required level of access; deny request if insufficient access level.
	* @param {integer} required_level Level of access required.
	* @return {boolean} Returns nothing.
	*/
	function access_check($required_level=0) {
		// Check if client isn't in a session
		if (!isset($_SESSION['user']) || $_SESSION["user"]["access"] < $required_level) {
			response_send(false, "acceso no concedido; solo los usuarios con {$required_level}^ acceso tienen permiso");
		}
	}


	/**
	* Checks whether client has required level of access.
	* @param {integer} required_level Level of access required.
	* @return {boolean} Returns True, if client has required access level; False, if client doesn't have required access level.
	*/
	function access_level_check($required_level=0) {
		// Check if client isn't in a session
		if (!isset($_SESSION['user'])) {
			return false;
		} else if ($_SESSION["user"]["access"] < $required_level) {
			return false;
		} else {
			return true;
		}
	}


	/**
	* Modify response's status and reason.
	* @param status [boolean] True, response is sucesfull; False, response is unsucesfull.
	* @param reason [string] Reason for given status.
	* @return {undefined} Returns nothing.
	*/
	function response_status($status=true, $reason='unknown') {
		if ($status === true) { $status = 'success'; }
		else { $status = 'failure'; }

		$GLOBALS['response']['status'] = $status;
		$GLOBALS['response']['reason'] = $reason;
	}


	/**
	* Echo current resopnse end exit.
	* @param status [boolean] True, response is sucesfull; False, response is unsucesfull.
	* @param reason [string] Reason for given status.
	* @return {undefined} Returns nothing.
	*/
	function response_send($status=null, $reason=null) {
		if ($status !== null) { response_status($status, $reason); }

		// Close connection to SQL database if any
		conn_close();

		// Echo response
		echo json_encode($GLOBALS['response']);

		// Kill PHP
		die();
	}

	// Call on endpoint according to client's request
	// ***************************************************************************

	if (count($routes) <= 0) {
		response_status(false, 'no valid endpoint given');
	} else if ($routes[0] === 'jobs') {
		include_once './endpoints/jobs.php';

		if (count($routes) === 2){
			if ($_SERVER['REQUEST_METHOD'] === 'GET') {
				switch ($routes[1]) {
					case 'get':
						job_get(json_decode($_GET['filter'], true), $_GET['options']);
						break;
				}
			} else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
				switch ($routes[1]) {
					case 'add':
						job_add($_POST['data']);
						break;
					case 'modify':
						job_modify($_POST['id'], $_POST['data']);
						break;
					case 'remove':
						job_remove($_POST['id']);
						break;
				}
			}
		}
	} else if ($routes[0] === 'users') {
		include_once './endpoints/users.php';

		if (count($routes) === 2){
			if ($_SERVER['REQUEST_METHOD'] === 'GET') {
				switch ($routes[1]) {
					case 'get':
						user_get(json_decode($_GET['filter'], true), $_GET['options']);
						break;
				}
			} else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
				switch ($routes[1]) {
					case 'login':
						user_login($_POST['username'], $_POST['password']);
						break;
					case 'logout':
						user_logout();
						break;
					case 'add':
						user_add($_POST['data']);
						break;
					case 'modify':
						user_modify($_POST['id'], $_POST['data']);
						break;
					case 'remove':
						user_remove($_POST['id']);
						break;
				}
			}
		}
	} else if ($routes[0] === 'database') {
		include_once './endpoints/database.php';

		if (count($routes) === 2 && $_SERVER['REQUEST_METHOD'] === 'POST') {
			switch ($routes[1]) {
				case 'setup':
					database_setup();
					break;
			}
		}
	} else if ($routes[0] === 'logs') {
		if (count($routes) === 2){
			if ($_SERVER['REQUEST_METHOD'] === 'GET') {
				switch ($routes[1]) {
					case 'get':
						log_get(json_decode($_GET['filter'], true), $_GET['options']);
						break;
				}
			} else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
				switch ($routes[1]) {
					case 'add':
						log_add($_POST['data']);
						break;
					case 'clear':
						log_clear();
						break;
				}
			}
		}
	} else {
		response_status(false, "`{$routes[0]}` endpoint not found");
	}

	response_send();

?>
