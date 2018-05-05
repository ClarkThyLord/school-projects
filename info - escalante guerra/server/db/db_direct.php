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
		exit("connection to SQL server couldn't be initialize\n{$conn->connect_error}");
  }

	// Check for database, and create it if it's not available
	if ($GLOBALS['conn']->select_db($db_name) === false) {

		if ($conn->query("CREATE DATABASE {$db_name}") === true) {
			$GLOBALS['conn']->select_db($db_name);

			// Temporary variable, used to store current query
			$sql = '';
			// Read the entire file
			$lines = file('./structure.sql');
			// Loop through each line
			foreach ($lines as $line) {
				// Skip it if it's a comment
				if (substr($line, 0, 2) == '--' || $line == '') { continue; }

				// Add this line to the current segment
				$sql .= $line;
				// If it has a semicolon at the end, it's the end of the query
				if (substr(trim($line), -1, 1) == ';') {
					// Perform the query
					$GLOBALS["conn"]->query($sql) or print('Error performing query \'<strong>' . $sql . '\': ' . mysql_error() . '<br /><br />');

					// Reset temp variable to empty
					$sql = '';
				}
			}
		} else {
			exit('SQL database couldn\'t be setup');
		}
	}

	// Functions relative to SQL database
  // ***************************************************************************

	/**
	* Close connection to SQL database.
	* @return {undefined} Returns nothing.
	*/
	function conn_close() {
		$GLOBALS['conn']->close();
	}

?>
