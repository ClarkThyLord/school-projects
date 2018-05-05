<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:  | Access:
	// -------------------------------------------
	// database   | setup     | POST      | 2
	// users      | login     | POST      | none
	// users      | logout    | POST      | none
	// *******************************************

	// Response object that will be sent back
	$response = array('status' => 'success', 'reason' => 'initial response', 'data' => array());

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

		$GLOBALS['response']['debug']['method'] = $_SERVER['REQUEST_METHOD'];
		$GLOBALS['response']['debug']['routes'] = $routes;
	}

	// Include database script
	// ***************************************************************************
	include_once './db/db.php';

	// Common Functions
	// ***************************************************************************

	/**
	* Check whether currently debugging.
	* @return {boolean} Returns true, if debugging; false, if not debugging.
	*/
	function is_debugging() {
		return (array_key_exists('debugging', $_GET) || !empty($_GET['debugging'])) && $_GET['debugging'] == true;
	}


	/**
	* Checks whether client has required level of access; deny request if insufficient access level.
	* @param {integer} required_level Level of access required.
	* @return {boolean} Returns nothing.
	*/
	function access_check($required_level=0) {
		// Initialize session if not already
	  if (!isset($_SESSION)) {
	    session_start();
	  }

		// Check if client isn't in a session
		if ((!array_key_exists('user', $_SESSION) || empty($_SESSION['user'])) && $_SESSION["user"]["access"] >= $required_level) {
			$GLOBALS['response']['status'] = 'failure';
			$GLOBALS['response']['reason'] = 'access not granted';

			send_response();
		}
	}


	/**
	* Echo current resopnse end exit.
	* @return {undefined} Returns nothing.
	*/
	function send_response() {
		// Close connection to SQL database if any
		if ((array_key_exists('conn', $GLOBALS) || !empty($GLOBALS['conn'])) && mysqli_connect_errno() && mysqli_ping($link)) {
			conn_close();
		}

		// Echo response
		echo json_encode($GLOBALS['response']);

		// Kill PHP
		die();
	}

	// Call according to client's request
	// ***************************************************************************

	if (count($routes) <= 0) {
    $GLOBALS['response']['status'] = 'failure';
    $GLOBALS['response']['reason'] = 'no valid endpoint given';
	} else if ($routes[0] === 'database') {
		include_once './endpoints/database.php';

		if (count($routes) === 2 && $_SERVER['REQUEST_METHOD'] === 'POST') {
			switch ($routes[1]) {
				case 'setup':
					database_setup();
					break;
			}
		}
	} else if ($routes[0] === 'users') {
		include_once './endpoints/users.php';

		if (count($routes) === 2 && $_SERVER['REQUEST_METHOD'] === 'POST') {
			switch ($routes[1]) {
				case 'login':
					user_login();
					break;
				case 'logout':
					user_logout();
					break;
			}
		}
	} else {
    $GLOBALS['response']['status'] = 'failure';
    $GLOBALS['response']['reason'] = '`{$routes[0]}` endpoint not found';
	}

	send_response();

?>
