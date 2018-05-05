<?php

	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function database_setup() {
		access_check(2);

		// FOR DEBUGGING
		if (is_debugging()) {
			$GLOBALS['response']['debug']['database']['setup'] = 'started setting up SQL database and tables';
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
				$GLOBALS["conn"]->query($sql) or print('Error performing query \'<strong>' . $sql . '\': ' . mysql_error() . '<br /><br />');

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
			$GLOBALS['response']['debug']['database']['setup'] = 'finished setting up SQL database and tables';
		}
	}

?>
