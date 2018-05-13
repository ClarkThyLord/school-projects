<?php

	/**
	* Get job(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get job(s) with.
	* @param options [array] Options to get job(s) with.
	* @return {undefined} Returns nothing.
	*/
	function job_get($filter=array(), $options=array()) {
    $filter_sql = ' WHERE 1';
    foreach ($filter as $key => $value) {
      $filter_sql .= " AND `{$key}` = '{$value}'";
    }

		$sql = 'SELECT * FROM `jobs`';

		if ($filter_sql !== ' WHERE 1') { $sql .= $filter_sql; }

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $jobs = $GLOBALS['conn']->query($sql);
    if ($jobs->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($job = $jobs->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $job);
      }

			response_status(true, 'se encontró puesto(s) válido(s)');
    } else {
			response_status(false, 'no se encontró puesto(s) válido(s)');
    }
	}


	/**
	* Add a job to the SQL database with the given data.
	* @param data [array] Data to create job with.
	* @return {undefined} Returns nothing.
	*/
	function job_add($data=array()) {
    access_check(1);

		$data = array_merge(array('title' => 'Nueva Puesto', 'description' => 'Nueva posición abierta!'), $data);

    $sql = "INSERT INTO `jobs` (`id`, `created`, `title`, `description`, `active`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["title"]}', '{$data["description"]}', '1')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT * FROM `jobs` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['job'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'puesto creado', 'jobs', "{$insert_id}");

			job_get();

			response_send(true, 'puesto agregado exitosamente');
    } else {
			response_send(false, 'puesto agregado sin éxito');
    }
	}


	/**
	* Modify a job in the SQL database.
	* @param job_id [string] ID of job to be modified.
	* @param data [array] Data to modify job with.
	* @return {undefined} Returns nothing.
	*/
	function job_modify($job_id=0, $data=array()) {
		access_check(1);
		if ($job_id <= 0) {
			response_send(false, 'la identificación del puesto no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el puesto con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('title', 'description', 'active');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys)) { continue; } else {
				array_push($data_sql, "`{$key}` = '{$value}'");
			}
		}

		$sql = "UPDATE `jobs` SET " . join(', ', $data_sql) . " WHERE `id` = {$job_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'puesto modificado', 'jobs', "{$job_id}");

			job_get();

			response_send(true, 'puesto modificado con éxito');
		} else {
			response_send(false, 'puesto modificado sin éxito');
		}
	}


	/**
	* Remove a job from the SQL database.
	* @param job_id [string] ID of job to be removed.
	* @return {undefined} Returns nothing.
	*/
	function job_remove($job_id=0) {
		access_check(1);
		if ($job_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `jobs` WHERE `id` = {$job_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $job_id;

			// LOG
			qlog($_SESSION['user']['id'], 'puesto eliminado', 'jobs', "{$job_id}");

			job_get();

			response_send(true, 'puesto eliminado con éxito');
    } else {
			response_send(false, 'puesto eliminado sin éxito');
    }
	}

?>
