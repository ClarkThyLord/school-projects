<?php

	// Response object that will be sent back
	$response = array("status" => "unknown", "reason" => "initial response", "extra" => array("querry" => $_GET));

	// Rest API
	// Endpoints ~
	// Main:      | Sub:     | Methods:
	// ----------------------------------------
	// landmarks  |          | GET, POST, DELETE
	//            | key      | GET
	//            | update   | POST
	// ****************************************

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
	// Remove bad "routes" from the URL array and add them to valid routes
	foreach ($url as $route){
        if(trim($route) != '') // If it's an empty string and it's not this file
            if ($route == "server.php"){ // Check if is head
				$head = true;
			}
			else if ($route == true){ // This is a route
				array_push($routes, $route);
			}
    }
	
	// FOR DEBUGGING
	$GLOBALS["response"]["routes"] = $routes;
	
	// See what route the request corresponds to
	if (count($routes) <= 0){ // No routes could be found in request
        $GLOBALS["response"]["status"] = "error";
        $GLOBALS["response"]["reason"] = "no endpoint given";
	}
	else if ($routes[0] == "landmarks"){
		// Load endpoint
		include "landmarks.php";
        
        if(count($routes) == 1){ // Requesting acess to LANDMARKS main endpoint
            if ($method == "GET"){
                $GLOBALS["response"]["status"] = "received";
                $GLOBALS["response"]["reason"] = "GET request to /landmarks";

                landmarksGet();
            }
            else if ($method == "POST"){
                $GLOBALS["response"]["status"] = "received";
                $GLOBALS["response"]["reason"] = "POST request to /landmarks";

                landmarksPost();
            }
            else if ($method == "DELETE"){
                $GLOBALS["response"]["status"] = "received";
                $GLOBALS["response"]["reason"] = "DELETE request to /landmarks";

                landmarksDelete();
            }
            else{ // Method for endpoint isn't supported
                $GLOBALS["response"]["status"] = "error";
                $GLOBALS["response"]["reason"] = "request method not supported";
            }
        }
        else if (count($routes) == 2){ // Requesting acess to one of landmarks sub endpoints
            if ($routes[1] == "key"){ // LANDMARKS KEY Endpoint
                if ($method == "GET"){
                    $GLOBALS["response"]["status"] = "received";
                    $GLOBALS["response"]["reason"] = "GET request to /landmarks/key";

                    keyGet();
                }
                else{ // Method for endpoint isn't supported
                    $GLOBALS["response"]["status"] = "error";
                    $GLOBALS["response"]["reason"] = "request method not supported";
                }
            }
            if ($routes[1] == "update"){ // LANDMARKS UPDATE Endpoint
                if ($method == "POST"){
                    $GLOBALS["response"]["status"] = "received";
                    $GLOBALS["response"]["reason"] = "POST request to /landmarks/update";

                    landmarksPatch();
                }
                else{ // Method for endpoint isn't supported
                    $GLOBALS["response"]["status"] = "error";
                    $GLOBALS["response"]["reason"] = "request method not supported";
                }
            }
            else{ // Sub endpoint not found
                $GLOBALS["response"]["status"] = "error";
                $GLOBALS["response"]["reason"] = "no main or sub endpoint found";
            }
        }
		else{ // Endpoint not found
            $GLOBALS["response"]["status"] = "error";
            $GLOBALS["response"]["reason"] = "no main or sub endpoint found";
		}
	}
	else{ // Main route requested for wasn't found
        $GLOBALS["response"]["status"] = "error";
        $GLOBALS["response"]["reason"] = "no " . json_encode($routes[0]) . " endpoint found";
	}

    echo json_encode($GLOBALS["response"]);

?>