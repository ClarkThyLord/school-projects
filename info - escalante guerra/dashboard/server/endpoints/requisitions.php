<?php

	/**
	* Get requisition(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get requisition(s) with.
	* @param options [array] Options to get requisition(s) with.
	* @return {undefined} Returns nothing.
	*/
	function requisition_get($filter=array(), $options=array()) {
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

		$sql = "SELECT * FROM `requisitions` WHERE 1 {$filter_sql} ORDER BY `created` DESC {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $requisitions = $GLOBALS['conn']->query($sql);
    if ($requisitions->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($requisition = $requisitions->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $requisition);
      }

			response_status(true, 'se encontró requisición(s) válido(s)');
    } else {
			response_status(true, 'no se encontró requisición(s) válido(s)');
    }
	}


	/**
	* Add a requisition to the SQL database with the given data.
	* @param data [array] Data to create requisition with.
	* @return {undefined} Returns nothing.
	*/
	function requisition_add($data=array()) {
		$data = array_merge(array('job' => 'Desconocido', 'candidate' => 'Desconocido', 'data' => 'NULL',), $data);
		if (gettype($data['data']) !== 'array') {
			$data['data'] = 'NULL';
		}else {
			$data['data'] = json_encode($data["data"]);
		}

		foreach ($data as $key => $value) {
			if (gettype($value) === 'string') {
				$data[$key] = str_replace("'", "''", $value);
			}
		}

		if ($data['data'] !== 'NULL') {
			$data['data'] = "'" . $data['data'] . "'";
		}

    $sql = "INSERT INTO `requisitions` (`id`, `created`, `company name`, `data`, `active`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["company name"]}', {$data["data"]}, '1')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `requisitions` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['requisition'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			if (isset($_SESSION['user'])) {
				qlog($_SESSION['user']['id'], 'cotización creado', 'quotations', "{$insert_id}");
			} else {
				qlog($_SERVER['REMOTE_ADDR'], 'cotización creado', 'quotations', "{$insert_id}");
			}

			requisition_get();

			response_send(true, 'requisición agregado exitosamente');
    } else {
			// FOR DEBUGGING
			if (is_debugging()) {
				$GLOBALS['response']['debug']['database']['error'] = $GLOBALS['conn']->error;
			}

			response_send(false, 'requisición agregado sin éxito');
    }
	}


	/**
	* Modify a requisition in the SQL database.
	* @param requisition_id [string] ID of requisition to be modified.
	* @param data [array] Data to modify requisition with.
	* @return {undefined} Returns nothing.
	*/
	function requisition_modify($requisition_id=0, $data=array()) {
		access_check(1);
		if ($requisition_id <= 0) {
			response_send(false, 'la identificación del candidato no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el requisición con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('company name', 'data', 'active');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				if (gettype($value) === 'array') {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", json_encode($value)) . "'");
				} else {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", $value) . "'");
				}
			}
		}

		$sql = "UPDATE `requisitions` SET " . join(', ', $data_sql) . " WHERE `id` = {$requisition_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'requisición modificado', 'requisitions', "{$requisition_id}");

			requisition_get();

			response_send(true, 'requisición modificado con éxito');
		} else {
			response_send(false, 'requisición modificado sin éxito');
		}
	}


	/**
	* Remove a requisition from the SQL database.
	* @param requisition_id [string] ID of requisition to be removed.
	* @return {undefined} Returns nothing.
	*/
	function requisition_remove($requisition_id=0) {
		access_check(1);
		if ($requisition_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `requisitions` WHERE `id` = {$requisition_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $requisition_id;

			// LOG
			qlog($_SESSION['user']['id'], 'requisición eliminado', 'requisitions', "{$requisition_id}");

			requisition_get();

			response_send(true, 'requisición eliminado con éxito');
    } else {
			response_send(false, 'requisición eliminado sin éxito');
    }
	}

?>
