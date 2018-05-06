<?php

	/**
	* Log anything with ease.
	* @param responsible [string] Who is responsible for given action.
	* @param action [string] Action to be logged.
	* @param asset_type [string] To what the action is being applied.
	* @param asset_id [string] Asset ID of what's being iteracted with.
	* @return {undefined} Returns nothing.
	*/
	function qlog($responsible='desconocido', $action='desconocido', $asset_type='', $asset_id='') {
		log_add(array('responsible' => $responsible, 'action' => $action, 'asset_type' => $asset_type, 'asset_id' => $asset_id));
	}


	/**
	* Get log(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get log(s) with.
	* @param options [array] Options to get user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function log_get($filter=array(), $options=array()) {
		$filter_sql = ' WHERE 1';
		foreach ($filter as $key => $value) {
			if ($key === 'password') { continue; }
			$filter_sql .= " AND `{$key}` = '{$value}'";
		}

		$sql = 'SELECT * FROM `logs`';

		if ($filter_sql !== ' WHERE 1') { $sql .= $filter_sql; }

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		$logs = $GLOBALS['conn']->query($sql);
		if ($logs->num_rows > 0) {
			$GLOBALS['response']['data']['dump'] = array();
			while($log = $logs->fetch_assoc()) {
				array_push($GLOBALS['response']['data']['dump'], $log);
			}

			response_status(true, 'se encontró registro(s) válido');
		} else {
			response_status(false, 'no se encontró registro(s) válido');
		}
	}


	/**
	* Add a log to the SQL database with the given data.
	* @param data [array] Data to create log with.
	* @return {undefined} Returns nothing.
	*/
	function log_add($data=array()) {
		$data = array_merge(array('responsible' => 'desconocido', 'action' => 'desconocido', 'asset_type' => 'desconocido', 'asset_id' => 'desconocido'), $data);

		$sql = "INSERT INTO `logs` (`created`, `responsible`, `action`, `asset_type`, `asset_id`) VALUES (CURRENT_TIMESTAMP, '{$data["responsible"]}', '{$data["action"]}', '{$data["asset_type"]}', '{$data["asset_id"]}')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) === TRUE) {
			response_status(true, 'registro agregado exitosamente');
		} else {
			response_status(false, 'registro agregado sin éxito');
		}
	}


	/**
	* Clears all logs from the log.
	* @return {undefined} Returns nothing.
	*/
	function log_clear() {
		access_check(2);

		$sql = "DELETE FROM `logs` WHERE 1";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) === TRUE) {
			// LOG
			qlog($_SESSION['user']['username'], 'registro borrado', 'registro', '*');

			log_get();

			response_status(true, 'registro limpiado con sin éxito');
		} else {
			response_status(false, 'unsucesfully cleared log');
		}
	}

?>
