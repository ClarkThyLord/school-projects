<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:  |
	// -----------------------------------
	// user       | login     | POST
	// user       | logout    | POST
	// ***********************************

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
	    if ($route == basename(__FILE__)) {
				$head = true;
			}
			// If true then route is valid
			else if ($head == true) {
				array_push($routes, $route);
			}
    }
	}

	// FOR DEBUGGING
	if ($_SERVER['debugging'] === true) {
		// Setup debug spot in response
		$GLOBALS['response']['debug'] = Array();

		$GLOBALS['response']['debug']['method'] = $_SERVER['REQUEST_METHOD'];
		$GLOBALS['response']['debug']['routes'] = $routes;
	}

	// Call according to client's request
	// ***************************************************************************

	if (count($routes) <= 0){
    $GLOBALS['response']['status'] = 'failure';
    $GLOBALS['response']['reason'] = 'no valid endpoint given';
	} else if ($routes[0] == 'user'){
		if (count($routes) == 2) {
			if ($routes[1] == 'login' && $_SERVER['REQUEST_METHOD'] == 'POST') {
				include 'DB.php';

				user_login();
			}

			if ($routes[1] == 'logout' && $_SERVER['REQUEST_METHOD'] == 'POST') {
				  session_unset();

			    $GLOBALS['response']['status'] = 'success';
			    $GLOBALS['response']['reason'] = 'sucesfully logged out';
			}
		}
	} else {
    $GLOBALS['response']['status'] = 'failure';
    $GLOBALS['response']['reason'] = '`' . json_encode($routes[0]) . '` endpoint not found';
	}

	send_response();

	/**
	* Echo current resopnse end exit.
	* @return {undefined} Returns nothing.
	*/
	function send_response() {
		echo json_encode($GLOBALS['response']);
		die();
	}

?>
