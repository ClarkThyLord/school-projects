<?php

	/**
	* Search database for data relative to search term.
	* @param term [array] Term used to search data with.
	* @return {undefined} Returns nothing.
	*/
	function search($term="") {
		access_check(0);

		$tables = [
			'jobs' => array('id', 'created', 'title', 'description', 'active'),
			'quotations' => array('id', 'created', 'company name', 'job', 'active'),
			'requisitions' => array('id', 'created', 'company name', 'active'),
			'candidates' => array('id', 'created', 'name', 'active'),
		];

		foreach ($tables as $table => $columbs) {
			$filters = array();
			foreach ($columbs as $columb) {
				array_push($filters, "`{$columb}` LIKE '%{$term}%' ORDER BY `created` DESC");
			}
			$sql = "SELECT * FROM `{$table}` WHERE " . join(' OR ', $filters);

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data'][$table] = array();

	    $results = $GLOBALS['conn']->query($sql);
	    if ($results->num_rows > 0) {
	      while($result = $results->fetch_assoc()) {
	        array_push($GLOBALS['response']['data'][$table], $result);
	      }
	    }
		}

		response_status(true, 'búsqueda fue posible');
	}

?>
