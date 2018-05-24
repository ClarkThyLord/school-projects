<?php

	/**
	* Contstruct Kanban data with sections and landmarks in SQL database.
	* @return {undefined} Returns nothing.
	*/
	function kanban_get() {
		$sql = "SELECT * FROM `sections` WHERE 1 ORDER BY `id` ASC";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		// Setup data dump
		$GLOBALS['response']['data']['dump'] = array();

    $sections = $GLOBALS['conn']->query($sql);
    if ($sections->num_rows > 0) {
      while($section = $sections->fetch_assoc()) {
	      $section_index = array_push($GLOBALS['response']['data']['dump'], array_merge($section, array('data' => array()))) - 1;

				$sql = "SELECT * FROM `landmarks` WHERE `section`={$section["id"]} ORDER BY `id` ASC";

				// FOR DEBUGGING
				if (is_debugging()) {
					array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
				}

				$landmarks = $GLOBALS['conn']->query($sql);
		    if ($landmarks->num_rows > 0) {
					while($landmark = $landmarks->fetch_assoc()) {
						array_push($GLOBALS['response']['data']['dump'][$section_id], $landmark);
					}
				}
      }

			response_status(true, 'kanban recuperado con éxito');
    } else {
			response_status(true, 'kanban recuperado sin éxito');
    }
	}

?>
