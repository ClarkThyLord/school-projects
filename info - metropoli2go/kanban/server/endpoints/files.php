<?php

	/**
	* Get file(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get file(s) with.
	* @param options [array] Options to get file(s) with.
	* @return {undefined} Returns nothing.
	*/
	function file_get($filter=array(), $options=array()) {
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

		$sql = "SELECT * FROM `files` WHERE 1 {$filter_sql} ORDER BY `created` DESC {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $files = $GLOBALS['conn']->query($sql);
    if ($files->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($file = $files->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $file);
      }

			response_status(true, 'se encontró archivo(s) válido(s)');
    } else {
			response_status(true, 'no se encontró archivo(s) válido(s)');
    }
	}


	/**
	* Add a file to the SQL database with the given data.
	* @param task_id [String] Task's ID to which this file belongs to.
	* @param file [File] File object to save.
	* @return {undefined} Returns nothing.
	*/
	function file_add($task_id='', $file) {
    access_check(1);

    $sql = "INSERT INTO `files` (`id`, `created`, `landmark`, `visual name`, `unique name`, `url`) VALUES (NULL, CURRENT_TIMESTAMP, '{$task_id}', '{$file["name"]}', '', '')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

			// Create file on server
			$file_content = file_get_contents($file["tmp_name"]);
			$file_extension = pathinfo($file["name"], PATHINFO_EXTENSION);
      file_put_contents("./files/" . $insert_id . "." . pathinfo($file["name"], PATHINFO_EXTENSION), $file_content);

			// Update file in database
      $sql = "UPDATE `files` SET `unique name`='{$insert_id}.{$file_extension}', `url`='" . ((isset($_SERVER['HTTPS']) && $_SERVER['HTTPS'] && $_SERVER['HTTPS'] != "off") ? "https" : "http") . "://{$_SERVER["SERVER_NAME"]}{$_SERVER["REQUEST_URI"]}/../../../files/{$insert_id}.{$file_extension}' WHERE `id` = '{$insert_id}'";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['conn']->query($sql);

			// Retrieve all files
      $sql = "SELECT * FROM `files` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['file'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'archivo creado', 'files', "{$insert_id}");

			kanban_get();

			response_send(true, 'archivo agregado exitosamente');
    } else {
			response_send(false, 'archivo agregado sin éxito');
    }
	}


	/**
	* Modify a file in the SQL database.
	* @param file_id [string] ID of file to be modified.
	* @param data [array] Data to modify file with.
	* @return {undefined} Returns nothing.
	*/
	// function file_modify($file_id=0, $data=array()) {
	// 	access_check(1);
	// 	if ($file_id <= 0) {
	// 		response_send(false, 'la identificación del archivo no fue dada');
	// 	}
	// 	if (sizeof($data) === 0) {
	// 		response_send(false, 'datos, para modificar el archivo con, no se dio');
	// 	}
	//
	// 	$data_sql = array();
	// 	$valid_keys = array('title', 'description', 'active');
	// 	foreach ($data as $key => $value) {
	// 		if (!in_array($key, $valid_keys)) { continue; } else {
	// 			if (gettype($value) === 'array') {
	// 				array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", json_encode($value)) . "'");
	// 			} else {
	// 				array_push($data_sql, "`{$key}` = '" . str_replace("'", "''", $value) . "'");
	// 			}
	// 		}
	// 	}
	//
	// 	$sql = "UPDATE `files` SET " . join(', ', $data_sql) . " WHERE `id` = {$file_id}";
	//
	// 	// FOR DEBUGGING
	// 	if (is_debugging()) {
	// 		array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
	// 	}
	//
	// 	if ($GLOBALS["conn"]->query($sql) === true) {
	// 		// LOG
	// 		qlog($_SESSION['user']['id'], 'archivo modificado', 'files', "{$file_id}");
	//
	// 		file_get();
	//
	// 		response_send(true, 'archivo modificado con éxito');
	// 	} else {
	// 		response_send(false, 'archivo modificado sin éxito');
	// 	}
	// }


	/**
	* Remove a file from the SQL database.
	* @param file_id [string] ID of file to be removed.
	* @return {undefined} Returns nothing.
	*/
	function file_remove($file_id=0) {
		access_check(1);
		if ($file_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "SELECT * FROM `files` WHERE `id` = {$file_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		$file = $GLOBALS['conn']->query($sql)->fetch_assoc();
		if (count($file) > 0) {
      $GLOBALS['response']['data']['id'] = $file_id;

			@unlink("./files/{$file["unique name"]}");

			$sql = "DELETE FROM `files` WHERE `id` = {$file_id}";

			// FOR DEBUGGING
			if (is_debugging()) {
			 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

			$GLOBALS['conn']->query($sql);

			// LOG
			qlog($_SESSION['user']['id'], 'archivo eliminado', 'files', "{$file_id}");

			kanban_get();

			response_send(true, 'archivo eliminado con éxito');
    } else {
			response_send(false, 'archivo eliminado sin éxito');
    }
	}

?>
