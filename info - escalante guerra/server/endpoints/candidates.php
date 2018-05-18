<?php

	/**
	* Get candidate(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get candidate(s) with.
	* @param options [array] Options to get candidate(s) with.
	* @return {undefined} Returns nothing.
	*/
	function candidate_get($filter=array(), $options=array()) {
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

		$sql = "SELECT * FROM `candidates` WHERE 1 {$filter_sql} {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $candidates = $GLOBALS['conn']->query($sql);
    if ($candidates->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($candidate = $candidates->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $candidate);
      }

			response_status(true, 'se encontró candidato(s) válido(s)');
    } else {
			response_status(true, 'no se encontró candidato(s) válido(s)');
    }
	}


	/**
	* Add a candidate to the SQL database with the given data.
	* @param data [array] Data to create candidate with.
	* @return {undefined} Returns nothing.
	*/
	function candidate_add($data=array()) {
    access_check(1);

		$data = array_merge(array('name' => 'Nueva Candidato', 'data' => 'NULL'), $data);
		if ($data['data'] != 'NULL') {
  		if (json_decode($data['data']) == NULL) {
				$data['data'] = 'NULL';
			} else {
				$data['data'] = "'{$data["data"]}'";
			}
		}

    $sql = "INSERT INTO `candidates` (`id`, `created`, `name`, `data`, `active`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["name"]}', {$data["data"]}, '1')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `candidates` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['candidate'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'candidato creado', 'candidates', "{$insert_id}");

			candidate_get();

			response_send(true, 'candidato agregado exitosamente');
    } else {
			response_send(false, 'candidato agregado sin éxito');
    }
	}


	/**
	* Modify a candidate in the SQL database.
	* @param candidate_id [string] ID of candidate to be modified.
	* @param data [array] Data to modify candidate with.
	* @return {undefined} Returns nothing.
	*/
	function candidate_modify($candidate_id=0, $data=array()) {
		access_check(1);
		if ($candidate_id <= 0) {
			response_send(false, 'la identificación del candidato no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el candidato con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('title', 'description', 'active');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				array_push($data_sql, "`{$key}` = '{$value}'");
			}
		}

		$sql = "UPDATE `candidates` SET " . join(', ', $data_sql) . " WHERE `id` = {$candidate_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'candidato modificado', 'candidates', "{$candidate_id}");

			candidate_get();

			response_send(true, 'candidato modificado con éxito');
		} else {
			response_send(false, 'candidato modificado sin éxito');
		}
	}


	/**
	* Remove a candidate from the SQL database.
	* @param candidate_id [string] ID of candidate to be removed.
	* @return {undefined} Returns nothing.
	*/
	function candidate_remove($candidate_id=0) {
		access_check(1);
		if ($candidate_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `candidates` WHERE `id` = {$candidate_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $candidate_id;

			// LOG
			qlog($_SESSION['user']['id'], 'candidato eliminado', 'candidates', "{$candidate_id}");

			candidate_get();

			response_send(true, 'candidato eliminado con éxito');
    } else {
			response_send(false, 'candidato eliminado sin éxito');
    }
	}

?>
