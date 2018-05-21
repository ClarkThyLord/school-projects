<?php

	/**
	* Login in client to user with the given credentials.
	* @param username [string] Username to identify user(s) with.
	* @param password [string] Password to confirm client's identification.
	* @return {undefined} Returns nothing.
	*/
	function user_login($username='', $password='') {
		// Get all users with given username
		$sql = "SELECT * FROM `users` WHERE `username` =  '{$username}'";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		$result = $GLOBALS['conn']->query($sql);
		if ($result->num_rows > 0) {
			$GLOBALS['response']['data']['username'] = true;
			while ($user = $result->fetch_assoc()) {
				if ($password === $user['password']) {
					$GLOBALS['response']['data']['password'] = true;

					// Setup user's data for session
					$_SESSION['user'] = array('id' => $user['id'], 'username' => $user['username'], 'access' => $user['access']);

					// LOG
					qlog($_SESSION['user']['id'], 'el usuario se ha conectado');

					response_send(true, 'sesión se pudo iniciar');
				}
			}
		}

		response_send(false, 'sesión no se pudo iniciar');
	}


	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function user_logout() {
		// LOG
		qlog($_SESSION['user']['id'], 'el usuario se ha desconectado');

		session_unset();

		response_send(true, 'sesión cerrada exitosamente');
	}


	/**
	* Get user(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get user(s) with.
	* @param options [array] Options to get user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function user_get($filter=array(), $options=array()) {
		$filter_sql = '';
		foreach ($filter as $key => $value) {
			if ($key === 'password') { continue; }
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

		$sql = "SELECT `id`, `created`, `username`, `access` FROM `users` WHERE 1 {$filter_sql} ORDER BY `created` DESC {$options_sql}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $users = $GLOBALS['conn']->query($sql);
    if ($users->num_rows > 0) {
      $GLOBALS['response']['data']['dump'] = array();
      while($user = $users->fetch_assoc()) {
        array_push($GLOBALS['response']['data']['dump'], $user);
      }

			response_status(true, 'se encontró usuario(s) válido(s)');
    } else {
			response_status(true, 'no se encontró usuario(s) válido(s)');
    }
	}


	/**
	* Add a user to the SQL database with the given data.
	* @param data [array] Data to create user with.
	* @return {undefined} Returns nothing.
	*/
	function user_add($data=array()) {
    access_check(2);

		$data = array_merge(array('username' => 'Nuevo Usuario', 'password' => '', 'access' => 0), $data);

    $sql = "INSERT INTO `users` (`id`, `created`, `username`, `password`, `access`) VALUES (NULL, CURRENT_TIMESTAMP, '{$data["username"]}', '{$data["password"]}', '{$data["access"]}')";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    if ($GLOBALS['conn']->query($sql) === TRUE) {
      $insert_id = $GLOBALS['conn']->insert_id;

      $sql = "SELECT `id`, `username` FROM `users` WHERE `id` = '{$insert_id}' LIMIT 1";

			// FOR DEBUGGING
			if (is_debugging()) {
				array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
			}

      $GLOBALS['response']['data']['user'] = $GLOBALS['conn']->query($sql)->fetch_assoc();

			// LOG
			qlog($_SESSION['user']['id'], 'usuario creado', 'users', "{$insert_id}");

			user_get();

			response_send(true, 'usuario agregado exitosamente');
    } else {
			response_send(false, 'usuario agregado sin éxito');
    }
	}


	/**
	* Modify a user in the SQL database.
	* @param user_id [string] ID of user to be modified.
	* @param data [array] Data to modify user with.
	* @return {undefined} Returns nothing.
	*/
	function user_modify($user_id=0, $data=array()) {
		if (isset($_SESSION['user']) && $user_id !== $_SESSION['user']['id'] && !access_level_check(2)) {
			access_check(2);
	 	}
		if ($user_id <= 0) {
			response_send(false, 'la identificación del usuario no fue dada');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'datos, para modificar el usuario con, no se dio');
		}

		$data_sql = array();
		$valid_keys = array('username', 'password', 'access');
		foreach ($data as $key => $value) {
			if (!in_array($key, $valid_keys) || ($key === 'access' && !access_level_check(2))) { continue; } else {
				array_push($data_sql, "`{$key}` = '{$value}'");
			}
		}

		$sql = "UPDATE `users` SET " . join(', ', $data_sql) . " WHERE `id` = {$user_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS["conn"]->query($sql) === true) {
			// LOG
			qlog($_SESSION['user']['id'], 'usuario modificado', 'users', "{$user_id}");

			user_get();

			response_send(true, 'usuario modificado con éxito');
		} else {
			response_send(false, 'usuario modificado sin éxito');
		}
	}


	/**
	* Remove a user from the SQL database.
	* @param user_id [string] ID of user to be removed.
	* @return {undefined} Returns nothing.
	*/
	function user_remove($user_id=0) {
		if (isset($_SESSION['user']) && $user_id !== $_SESSION['user']['id'] && !access_level_check(2)) {
		 access_check(2);
		}	else if ($user_id === 0) {
		 response_send(false, 'la identificación del usuario no fue dada');
		}

    $sql = "DELETE FROM `users` WHERE `id` = {$user_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $user_id;

			// LOG
			qlog($_SESSION['user']['id'], 'usuario eliminado', 'users', "{$user_id}");

			user_get();

			response_send(true, 'usuario eliminado con éxito');
    } else {
			response_send(false, 'usuario eliminado sin éxito');
    }
	}

?>
