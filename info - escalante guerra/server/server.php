<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:  | Access:
	// -------------------------------------------
	// user       | login     | POST      | none
	// user       | logout    | POST      | none
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
	if ((array_key_exists('debugging', $_GET) || !empty($_GET['debugging'])) && $_GET['debugging'] == true) {
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
	} else if ($routes[0] === 'user'){
		if (count($routes) === 2 && $_SERVER['REQUEST_METHOD'] === 'POST') {
			switch ($routes[1]) {
				case 'login':
					include_once './db/db.php';

					// Get all users with given username
					$result = $GLOBALS['conn']->query("SELECT * FROM `users` WHERE `name` =  '{$_POST['username']}'");
					if ($result->num_rows > 0) {
						$GLOBALS['response']['data']['username'] = true;
						while ($user = $result->fetch_assoc()) {
							if ($_POST['password'] === $user['password']) {
								$GLOBALS['response']['data']['password'] = true;

								// Setup user data for session
								$_SESSION['user_data'] = array('id' => $user['id'], 'name' => $user['name'], 'access' => $user['access']);

								$GLOBALS['response']['status'] = 'success';
								$GLOBALS['response']['reason'] = 'sucesfully logged in';
								break;
							} else {
								$GLOBALS['response']['status'] = 'failure';
								$GLOBALS['response']['reason'] = 'unsucesfully logged in';
							}
						}
					} else {
						$GLOBALS['response']['status'] = 'failure';
						$GLOBALS['response']['reason'] = 'unsucesfully logged in';
					}

					break;
				case 'logout':
					session_unset();

					$GLOBALS['response']['status'] = 'success';
					$GLOBALS['response']['reason'] = 'sucesfully logged out';
					break;
			}
		}
	} else {
    $GLOBALS['response']['status'] = 'failure';
    $GLOBALS['response']['reason'] = '`{$routes[0]}` endpoint not found';
	}

	send_response();

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

?>
