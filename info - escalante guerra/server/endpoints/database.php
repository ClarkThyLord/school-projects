<?php

	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function database_setup() {
		access_check(2);

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
		}
	}

?>
