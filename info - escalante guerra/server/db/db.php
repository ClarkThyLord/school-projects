<?php

	// FOR DEBUGGING
	if (is_debugging()) {
		$GLOBALS['response']['debug']['database'] = array('sql' => array());

		$GLOBALS['response']['debug']['database']['setup'] = false;
	}

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
		$GLOBALS['response']['reason'] = "connection to SQL server couldn't be initialize\n{$conn->connect_error}";

		send_response();
  }

	// FOR DEBUGGING
	if (is_debugging()) {
		$GLOBALS['response']['debug']['database']['connection'] = true;
	}

	// Check for database, and create it if it's not available
	if ($GLOBALS['conn']->select_db($db_name) === false) {
		// FOR DEBUGGING
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['setup'] = 'started setting up SQL database and tables';
		}

		if ($conn->query("CREATE DATABASE {$db_name}") === true) {
			// FOR DEBUGGING
			if (is_debugging()) {
				$GLOBALS['response']['debug']['database']['setup'] = 'created SQL database';
			}

			// Temporary variable, used to store current query
			$templine = '';
			// Read the entire file
			$lines = file('./db/db_structure.sql');
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

			// FOR DEBUGGING
			if (is_debugging()) {
				$GLOBALS['response']['debug']['database']['setup'] = 'finished setting up SQL database and tables';
			}
		} else {
				$GLOBALS['response']['status'] = 'failure';
				$GLOBALS['response']['reason'] = 'SQL database couldn\'t be setup';

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
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['connection'] = false;
		}

		$GLOBALS['conn']->close();
	}

?>
