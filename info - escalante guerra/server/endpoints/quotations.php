<?php

	/**
	* Get quotation(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get quotation(s) with.
	* @param options [array] Options to get quotation(s) with.
	* @return {undefined} Returns nothing.
	*/
	function quotation_get($filter=array(), $options=array()) {
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

		$sql = "SELECT * FROM `quotations` WHERE 1 {$filter_sql} {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $quotations = $GLOBALS['conn']->query($sql);
    if ($quotations->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($quotation = $quotations->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $quotation);
      }

			response_status(true, 'se encontró cotización(s) válido(s)');
    } else {
			response_status(true, 'no se encontró cotización(s) válido(s)');
    }
	}


	/**
	* Add a quotation to the SQL database with the given data.
	* @param data [array] Data to create quotation with.
	* @return {undefined} Returns nothing.
	*/
	function quotation_add($data=array()) {
    access_check(1);

		$data = array_merge(array('name' => 'Nueva cotización', 'data' => 'NULL'), $data);
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

    $sql = "INSERT INTO `quotations` (`id`, `created`, `company name`, `job`, `data`, `active`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["company name"]}', '{$data["job"]}', {$data["data"]}, '1')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `quotations` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['quotation'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'cotización creado', 'quotations', "{$insert_id}");

			quotation_get();

			response_send(true, 'cotización agregado exitosamente');
    } else {
			response_send(false, 'cotización agregado sin éxito');
    }
	}


	/**
	* Modify a quotation in the SQL database.
	* @param quotation_id [string] ID of quotation to be modified.
	* @param data [array] Data to modify quotation with.
	* @return {undefined} Returns nothing.
	*/
	function quotation_modify($quotation_id=0, $data=array()) {
		access_check(1);
		if ($quotation_id <= 0) {
			response_send(false, 'la identificación del cotización no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el cotización con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('company name', 'job', 'data', 'active');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				if (gettype($value) === 'array') {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", json_encode($value)) . "'");
				} else {
					array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", $value) . "'");
				}
			}
		}

		$sql = "UPDATE `quotations` SET " . join(', ', $data_sql) . " WHERE `id` = {$quotation_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'cotización modificado', 'quotations', "{$quotation_id}");

			quotation_get();

			response_send(true, 'cotización modificado con éxito');
		} else {
			response_send(false, 'cotización modificado sin éxito');
		}
	}


	/**
	* Remove a quotation from the SQL database.
	* @param quotation_id [string] ID of quotation to be removed.
	* @return {undefined} Returns nothing.
	*/
	function quotation_remove($quotation_id=0) {
		access_check(1);
		if ($quotation_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `quotations` WHERE `id` = {$quotation_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $quotation_id;

			// LOG
			qlog($_SESSION['user']['id'], 'cotización eliminado', 'quotations', "{$quotation_id}");

			quotation_get();

			response_send(true, 'cotización eliminado con éxito');
    } else {
			response_send(false, 'cotización eliminado sin éxito');
    }
	}

?>
