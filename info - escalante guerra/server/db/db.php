<?php

	// FOR DEBUGGING
	if (is_debugging()) {
		$GLOBALS['response']['debug']['database'] = array('connection' => false, 'msg' => '', 'sql' => array(), 'setup' => array('database' => false, 'tables' => false));
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
		response_send(false, "connection to SQL server couldn't be initialize\n{$conn->connect_error}");
  }

	// FOR DEBUGGING
	if (is_debugging()) {
		$GLOBALS['response']['debug']['database']['connection'] = true;
	}

	// Connect to databse, or setup database if not found
	if ($GLOBALS['conn']->select_db($db_name) === false) {
		// FOR DEBUGGING
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['msg'] = 'started setting up SQL database';
		}

		if ($conn->query("CREATE DATABASE {$db_name}") === true) {
			$GLOBALS['conn']->select_db($db_name);

			// FOR DEBUGGING
			if (is_debugging()) {
				$GLOBALS['response']['debug']['database']['setup']['database'] = true;
			}
		} else {
			response_send(false, 'SQL database couldn\'t be setup');
		}
	}

	// Check whether database has tables setup; if not setup
	$sql = "SHOW TABLES FROM `{$db_name}`";

	// FOR DEBUGGING
	if (is_debugging()) {
		array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
	}

	if (!$GLOBALS['conn']->query($sql)->num_rows > 0) {
		// FOR DEBUGGING
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['msg'] = 'started setting up SQL tables';
		}

		// Temporary variable, used to store current query
		$sql = '';
		// Read the entire file
		$lines = file('./db/structure.sql');
		// Loop through each line
		foreach ($lines as $line) {
			// Skip it if it's a comment
			if (substr($line, 0, 2) == '--' || $line == '') { continue; }

			// Add this line to the current segment
			$sql .= $line;
			// If it has a semicolon at the end, it's the end of the query
			if (substr(trim($line), -1, 1) == ';') {
				// Perform the query
				$GLOBALS["conn"]->query($sql) or response_send(false, 'Error Performing SQL Query: \'<strong>' . $sql . '\': ' . mysql_error() . '<br /><br />');

				// FOR DEBUGGING
				if (is_debugging()) {
					array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
				}

				// Reset temp variable to empty
				$sql = '';
			}
		}

		// FOR DEBUGGING
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['setup']['tables'] = true;
		}
	}

	// Check whether current client's user, if any, is valid
	if (isset($_SESSION['user'])) {
		$sql = "SELECT `id`, `created`, `username`, `access` FROM `users` WHERE `id` =  '{$_SESSION["user"]["id"]}' LIMIT 1";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		$result = $GLOBALS['conn']->query($sql);
		if ($result->num_rows > 0) {
			$user = $result->fetch_assoc();

			// Update user's data for session
			$_SESSION['user'] = array('id' => $user['id'], 'username' => $user['username'], 'access' => $user['access']);
		} else {
			session_unset();
			
			header('Location: ../../../index.php');

			response_send(false, 'usuario ya no es válido');
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
