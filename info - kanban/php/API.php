<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:
	// ----------------------------------------
	//            |          |
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
	// Append all routes found to response
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
	// Append all routes found to response
	$GLOBALS["response"]["routes"] = $routes;

	// if (check_access() == true) {
	//
	// } else {
	// 	// TODO PROMPT ADMIN VIA LOG OF ATTEMP TO ACCESS API
  //   $GLOBALS["response"]["status"] = "access denied";
  //   $GLOBALS["response"]["reason"] = "insufficient access level";
	// }

	// See what route the request corresponds to
	if (count($routes) <= 0){ // No routes could be found in request
    $GLOBALS["response"]["status"] = "invalid request";
    $GLOBALS["response"]["reason"] = "no valid endpoint given";
	}
	else if ($routes[0] == "login" && $method == "POST"){
		// Load endpoint
		include "DB.php";

		login();
	}
	else if ($routes[0] == "logout"){
	  session_unset();
    $GLOBALS["response"]["status"] = "logged out";
    $GLOBALS["response"]["reason"] = "sucesfully logged out";
	} else { // Main route requested for wasn't found
    $GLOBALS["response"]["status"] = "invalid main endpoint";
    $GLOBALS["response"]["reason"] = "`" . json_encode($routes[0]) . "` endpoint not found";
	}

	echo json_encode($GLOBALS["response"]);

	// FUNCTIONS

	/**
	 * Checks whether current caller has required level of access.
	 * @param {integer} required_level Level of access required.
	 * @return {boolean} True, has required level of access; false, doesn' have required level of access.
	 */
	function check_access($required_level=0) {
			if ((array_key_exists("user_data", $_SESSION) && !empty($_SESSION["user_data"])) && $_SESSION["user_data"]["access"] >= $required_level) {
				return true;
			} else {
				return false;
			}
	}

?>
