<?php

	// ~ API Endpoints ~
	// Main:      | Sub:     | Methods:
	// ----------------------------------------
	//            |          |
	// ****************************************

	// Response object that will be sent back
	$response = array("status" => "received", "reason" => "initial response", "response" => array());

  // Initialize session if not already
  if(!isset($_SESSION)) {
    session_start();
  }

	if ((array_key_exists("user_data", $_SESSION) && !empty($_SESSION["user_data"])) && $_SESSION["user_data"]["access"] >= 1) {
		// Method of request: GET, POST, etc.
		$method = $_SERVER["REQUEST_METHOD"];

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

		// FOR DEBUGGING
		// Append all routes found to response
		$GLOBALS["response"]["routes"] = $routes;

		// See what route the request corresponds to
		if (count($routes) <= 0){ // No routes could be found in request
      $GLOBALS["response"]["status"] = "invalid request";
      $GLOBALS["response"]["reason"] = "no valid endpoint given";
		}
		else if ($routes[0] == ""){
			// Load endpoint
			include "";
		} else { // Main route requested for wasn't found
      $GLOBALS["response"]["status"] = "invalid main endpoint";
      $GLOBALS["response"]["reason"] = "`" . json_encode($routes[0]) . "` endpoint not found";
		}
	} else {
		// TODO PROMPT ADMIN VIA LOG OF ATTEMP TO ACCESS API
    $GLOBALS["response"]["status"] = "access denied";
    $GLOBALS["response"]["reason"] = "insufficient access level";
	}

	echo json_encode($GLOBALS["response"]);

?>
