<?php

	/**
	* Get form(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get form(s) with.
	* @param options [array] Options to get form(s) with.
	* @return {undefined} Returns nothing.
	*/
	function form_get($filter=array(), $options=array()) {
    $filter_sql = ' WHERE 1';
    foreach ($filter as $key => $value) {
      $filter_sql .= " AND `{$key}` = '{$value}'";
    }

		$sql = 'SELECT * FROM `forms`';

		if ($filter_sql !== ' WHERE 1') { $sql .= $filter_sql; }

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $forms = $GLOBALS['conn']->query($sql);
    if ($forms->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($form = $forms->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $form);
      }

			response_status(true, 'se encontró forma(s) válido(s)');
    } else {
			response_status(false, 'no se encontró forma(s) válido(s)');
    }
	}


	/**
	* Add a form to the SQL database with the given data.
	* @param data [array] Data to create form with.
	* @return {undefined} Returns nothing.
	*/
	function form_add($data=array()) {
		$data = array_merge(array('title' => 'Nueva forma', 'description' => 'Nueva posición abierta!'), $data);

    $sql = "INSERT INTO `forms` (`id`, `created`, `type`, `data`) VALUES (NULL, CURRENT_TIMESTAMP, {$data["type"]}, {$data["data"]})";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `forms` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['form'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SERVER['REMOTE_ADDR'], 'forma creado', 'forms', "{$insert_id}");

			form_get();

			response_send(true, 'forma agregado exitosamente');
    } else {
			response_send(false, 'forma agregado sin éxito');
    }
	}


	/**
	* Modify a form in the SQL database.
	* @param form_id [string] ID of form to be modified.
	* @param data [array] Data to modify form with.
	* @return {undefined} Returns nothing.
	*/
	function form_modify($form_id=0, $data=array()) {
		access_check(1);
		if ($form_id <= 0) {
			response_send(false, 'la identificación de la forma no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar la forma con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('type', 'data');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				array_push($data_sql, "`{$key}` = '{$value}'");
			}
		}

		$sql = "UPDATE `forms` SET " . join(', ', $data_sql) . " WHERE `id` = {$form_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'forma modificado', 'forms', "{$form_id}");

			form_get();

			response_send(true, 'forma modificado con éxito');
		} else {
			response_send(false, 'forma modificado sin éxito');
		}
	}


	/**
	* Remove a form from the SQL database.
	* @param form_id [string] ID of form to be removed.
	* @return {undefined} Returns nothing.
	*/
	function form_remove($form_id=0) {
		access_check(1);
		if ($form_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `forms` WHERE `id` = {$form_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $form_id;

			// LOG
			qlog($_SESSION['user']['id'], 'forma eliminado', 'forms', "{$form_id}");

			form_get();

			response_send(true, 'forma eliminado con éxito');
    } else {
			response_send(false, 'forma eliminado sin éxito');
    }
	}

?>
