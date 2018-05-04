<?php

	// Credentials
  // ***************************************************************************

	// Common Credentials
  $server_name = 'localhost';

	// Local Credentials
	$user_name = 'root';
  $password = '';
  $db_name = 'eg';

	// Server Credentials
  // $user_name = '';
  // $password = '';
  // $db_name = '';

	// Initialize connection to SQL database
  // ***************************************************************************

	$GLOBALS['conn'] = new mysqli($server_name, $user_name, $password);

	// Check connection
  if ($GLOBALS['conn']->connect_error) {
		$GLOBALS['response']['status'] = 'failure';
		$GLOBALS['response']['reason'] = "connection to SQL database couldn't be initialize\n{$conn->connect_error}";

		send_response();
  }

	// Check for database, and create it if it's not available
	if ($GLOBALS['conn']->select_db($db_name) === false) {
		if ($conn->query("CREATE DATABASE {$db_name}") === true) {
			// Temporary variable, used to store current query
			$templine = '';
			// Read the entire file
			$lines = file('./db_structure.sql');
			// Loop through each line
			foreach ($lines as $line) {
				// Skip it if it's a comment
				if (substr($line, 0, 2) == '--' || $line == '') { continue; }

				// Add this line to the current segment
				$templine .= $line;
				// If it has a semicolon at the end, it's the end of the query
				if (substr(trim($line), -1, 1) == ';') {
						// Perform the query
						mysql_query($templine) or print('Error performing query \'<strong>' . $templine . '\': ' . mysql_error() . '<br /><br />');
						// Reset temp variable to empty
						$templine = '';
				}
			}
		} else {
				$GLOBALS['response']['status'] = 'failure';
				$GLOBALS['response']['reason'] = "SQL database couldn't be setup";

				send_response();
		}
	}

 	// Functions relative to SQL database
   // ***************************************************************************

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

	// Functions relative to SQL database
  // ***************************************************************************

	/**
	* Close connection to SQL database.
	* @return {undefined} Returns nothing.
	*/
	function conn_close() {
		// FOR DEBUGGING
		if ((array_key_exists('debugging', $_GET) || !empty($_GET['debugging'])) && $_GET['debugging'] == true) {
			$GLOBALS['response']['debug']['SQL'] = 'connection closed';
		}

		$GLOBALS['conn']->close();
	}


	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function database_setup() {
		access_check(2);

		// Temporary variable, used to store current query
		$templine = '';
		// Read the entire file
		$lines = file('./db_structure.sql');
		// Loop through each line
		foreach ($lines as $line) {
			// Skip it if it's a comment
			if (substr($line, 0, 2) == '--' || $line == '') { continue; }

			// Add this line to the current segment
			$templine .= $line;
			// If it has a semicolon at the end, it's the end of the query
			if (substr(trim($line), -1, 1) == ';') {
					// Perform the query
					mysql_query($templine) or print('Error performing query \'<strong>' . $templine . '\': ' . mysql_error() . '<br /><br />');
					// Reset temp variable to empty
					$templine = '';
			}
		}
	}

?>
