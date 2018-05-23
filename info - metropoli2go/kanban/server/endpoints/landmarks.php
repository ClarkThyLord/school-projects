<?php

	/**
	* Get landmark(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get landmark(s) with.
	* @param options [array] Options to get landmark(s) with.
	* @return {undefined} Returns nothing.
	*/
	function landmark_get($filter=array(), $options=array()) {
		$filter_sql = '';
		foreach ($filter as $key => $value) {
			$filter_sql .= " AND `{$key}` = '{$value}'";
		}

		$options_sql = '';
		foreach ($options as $key => $value) {
			switch ($key) {
				case 'limit':
				$options_sql .= " LIMIT {$value}";
					break;
			}
		}

		$sql = "SELECT * FROM `landmarks` WHERE 1 {$filter_sql} ORDER BY `created` DESC {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $landmarks = $GLOBALS['conn']->query($sql);
    if ($landmarks->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($landmark = $landmarks->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $landmark);
      }

			response_status(true, 'se encontró landmark(s) válida(s)');
    } else {
			response_status(true, 'no se encontró landmark(s) válida(s)');
    }
	}


	/**
	* Add a landmark to the SQL database with the given data.
	* @param data [array] Data to create landmark with.
	* @return {undefined} Returns nothing.
	*/
	function landmark_add($data=array()) {
    access_check(1);

		$data = array_merge(array('name' => 'Nuevo Landmark', 'classification' => '', 'latitude' => '', 'longitude' => '', 'summary' => '', 'urls' => ''), $data);

		foreach ($data as $key => $value) {
			if (gettype($value) === 'string') {
				$data[$key] = str_replace("'", "''", $value);
			}
		}

    $sql = "INSERT INTO `landmarks` (`id`, `created`, `section`, `name`, `classification`, `latitude`, `longitude`, `summary`, `urls`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["section"]}', '{$data["name"]}', '{$data["classification"]}', {$data["latitude"]}, {$data["longitude"]}, {$data["summary"]}, {$data["urls"]})";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `landmarks` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['landmark'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'landmark creado', 'landmarks', "{$insert_id}");

			landmark_get();

			response_send(true, 'landmark agregado exitosamente');
    } else {
			response_send(false, 'landmark agregado sin éxito');
    }
	}


	/**
	* Modify a landmark in the SQL database.
	* @param landmark_id [string] ID of landmark to be modified.
	* @param data [array] Data to modify landmark with.
	* @return {undefined} Returns nothing.
	*/
	function landmark_modify($landmark_id=0, $data=array()) {
		access_check(1);
		if ($landmark_id <= 0) {
			response_send(false, 'la identificación del landmark no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el landmark con, no se dio');
		}

		if (count($_FILES) > 0) {
      $GLOBALS["response"]["data"]["files"] = array();

      foreach ($_FILES as $file) {
        $result = file_add($landmark_id, $file);
      }
    }

		$data_sql = array();
		$valid_keys = array('name', 'classification', 'latitude', 'longitude', 'summary', 'urls');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				if (gettype($value) === 'array') {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", json_encode($value)) . "'");
				} else {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", $value) . "'");
				}
			}
		}

		$sql = "UPDATE `landmarks` SET " . join(', ', $data_sql) . " WHERE `id` = {$landmark_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'landmark modificado', 'landmarks', "{$landmark_id}");

			landmark_get();

			response_send(true, 'landmark modificado con éxito');
		} else {
			response_send(false, 'landmark modificado sin éxito');
		}
	}


	/**
	* Remove a landmark from the SQL database.
	* @param landmark_id [string] ID of landmark to be removed.
	* @return {undefined} Returns nothing.
	*/
	function landmark_remove($landmark_id=0) {
		access_check(1);
		if ($landmark_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `landmarks` WHERE `id` = {$landmark_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $landmark_id;

			// LOG
			qlog($_SESSION['user']['id'], 'landmark eliminado', 'landmarks', "{$landmark_id}");

			landmark_get();

			response_send(true, 'landmark eliminado con éxito');
    } else {
			response_send(false, 'landmark eliminado sin éxito');
    }
	}

?>
