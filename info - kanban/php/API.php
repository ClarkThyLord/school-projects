<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:  | Access:
	// -------------------------------------------
	// user       |          | GET       | none
	// user       | login    | POST      | none
	// user       | logout   | any       | none
	// user       | create   | POST      | 2
	// user       | remove   | POST      | anyone can remove their own user; but only admins, access: 2, can remove other users
	// user       | modify   | POST      | anyone can modify their own user; but only admins, access: 2, can modify other users
	// table      |          | GET       | none
	// table      | create   | POST      | 1
	// table      | remove   | POST      | 1
	// table      | modify   | POST      | 1
	// task       |          | GET       | none
	// task       | create   | POST      | 1
	// task       | remove   | POST      | 1
	// task       | modify   | POST      | 1
	// file       |          | GET       | none
	// file       | create   | POST      | 2
	// file       | remove   | POST      | 1
	// file       | modify   | POST      | 1
	// log        |          | GET       | none
	// log        | create   | POST      | 2
	// log        | clear    | POST      | 2
	// log        | remove   | POST      | 2
	// log        | modify   | POST      | 2
	// ****************************************

	// Response object that will be sent back
	$response = array("status" => "received", "reason" => "initial response", "data" => array());

  // Initialize session if not already
  if(!isset($_SESSION)) {
    session_start();
  }
	// Method of request: GET, POST and etc.
	$method = $_SERVER["REQUEST_METHOD"];

	// FOR DEBUGGING
	$GLOBALS["response"]["method"] = $method;

	// Stripping the base URL and getting all the "routes"
	// **********
	// Get the base URL and remove it from the request URL
	$url = substr($_SERVER['REQUEST_URI'], strlen(implode('/', array_slice(explode('/', "server.php"), 0, -1)) . '/'));
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
    if (trim($route) != "") {
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
	$GLOBALS["response"]["routes"] = $routes;

	// See what route the request corresponds to
	if (count($routes) <= 0){ // No routes could be found in request

    $GLOBALS["response"]["status"] = "invalid request";
    $GLOBALS["response"]["reason"] = "no valid endpoint given";

	}
	else if ($routes[0] == "user"){

		if ($routes[1] == "logout") {
		  session_unset();
	    $GLOBALS["response"]["status"] = "logged out";
	    $GLOBALS["response"]["reason"] = "sucesfully logged out";
		} else{
			include "DB.php";

			if ($method == "GET") { user_get(); }

			if ($routes[1] == "login" && $method == "POST") { user_login(); }

			if ($routes[1] == "create" && $method == "POST") { user_create(); }

		 	if ($routes[1] == "remove" && $method == "POST") { user_remove(); }

		 	if ($routes[1] == "modify" && $method == "POST") { user_modify(); }
		}

	}
	else if ($routes[0] == "table"){

		include "DB.php";

		if ($method == "GET") { table_get(); }

		if ($routes[1] == "create" && $method == "POST") { table_create(); }

	 	if ($routes[1] == "remove" && $method == "POST") { table_remove(); }

	 	if ($routes[1] == "modify" && $method == "POST") { table_modify(); }

	}
	else if ($routes[0] == "task"){

		include "DB.php";

		if ($method == "GET") { task_get(); }

		if ($routes[1] == "create" && $method == "POST") { task_create(); }

	 	if ($routes[1] == "remove" && $method == "POST") { task_remove(); }

	 	if ($routes[1] == "modify" && $method == "POST") { task_modify(); }

	}
	else if ($routes[0] == "file"){

		include "DB.php";

		if ($method == "GET") { file_get(); }

		if ($routes[1] == "create" && $method == "POST") { file_create_direct(); }

	 	if ($routes[1] == "remove" && $method == "POST") { file_remove(); }

	 	if ($routes[1] == "modify" && $method == "POST") { file_modify(); }

	}
	else if ($routes[0] == "log"){

		include "DB.php";

		if ($method == "GET") { log_get(); }

		if ($routes[1] == "create" && $method == "POST") { log_create_direct(); }

	 	if ($routes[1] == "clear") { log_clear(); }

	 	if ($routes[1] == "remove" && $method == "POST") { log_remove(); }

	 	if ($routes[1] == "modify" && $method == "POST") { log_modify(); }

	}
	else { // Main route requested for wasn't found

    $GLOBALS["response"]["status"] = "invalid main endpoint";
    $GLOBALS["response"]["reason"] = "`" . json_encode($routes[0]) . "` endpoint not found";

	}

	send_response();

	/**
	* Send response back to caller.
	* @return {undefined} Returns nothing.
	*/
	function send_response() {
		echo json_encode($GLOBALS["response"]);
		die();
	}

?>
