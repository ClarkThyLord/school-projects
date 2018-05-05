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

					// Setup user data for session
					$_SESSION['user'] = array('id' => $user['id'], 'username' => $user['username'], 'access' => $user['access']);

					response_send(true, 'sucesfully logged in');
				}
			}
		}

		response_status(false, 'unsucesfully logged in');
	}


	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function user_logout() {
		session_unset();

		response_status(true, 'sucesfully logged out');
	}


	/**
	* Get user(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function user_get($filter=array()) {
    $filter_sql = ' WHERE 1';
    foreach ($filter as $key => $value) {
			if ($key === 'password') { continue; }
      $filter_sql .= " AND `{$key}` = '{$value}'";
    }

		$sql = 'SELECT `id`, `created`, `username`, `access` FROM `users`';

		if ($filter_sql !== ' WHERE 1') { $sql .= $filter_sql; }

		// FOR DEBUGGING
		if (is_debugging()) {
			array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

    $users = $GLOBALS['conn']->query($sql);
    if ($users->num_rows > 0) {
      $GLOBALS['response']['data']['users'] = array();
      while($user = $users->fetch_assoc()) {
        $GLOBALS['response']['data']['users'][$user['id']] = $user;
      }

			response_status(true, 'found valid user(s)');
    } else {
			response_status(false, 'found no valid user(s)');
    }
	}


	/**
	* Add a user to the SQL database with the given data.
	* @param data [array] Data to create user with.
	* @return {undefined} Returns nothing.
	*/
	function user_add($data=array()) {
    access_check(2);

		$data = array_merge(array("username" => "New User", "password" => "", "access" => 0), $data);

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

			response_status(true, 'sucesfully created user');
    } else {
			response_status(false, 'unsucesfully added user');
    }
	}


	/**
	* Modify a user in the SQL database.
	* @param user_id [string] ID of user to be modified.
	* @param data [array] Data to modify user with.
	* @return {undefined} Returns nothing.
	*/
	function user_modify($user_id=0, $data=array()) {
		if ($user_id !== $_SESSION['user']['id'] && !access_level_check(2)) {
			response_send(false, 'only admins, access level 2^, can modify other users');
	 	}
		if ($user_id <= 0) {
			response_send(false, 'user ID wasn\'t given');
		}
		if (sizeof($data) === 0) {
			response_send(false, 'data to modify user with wasn\'t given');
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
			response_status(true, 'sucesfully modified user');
		} else {
			response_status(false, 'unsucesfully modified user');
		}
	}


	/**
	* Remove a user from the SQL database.
	* @param user_id [string] ID of user to be removed.
	* @return {undefined} Returns nothing.
	*/
	function user_remove($user_id=0) {
		if ($user_id !== $_SESSION['user']['id'] && !access_level_check(2)) {
		 response_send(false, 'only admins, access level 2^, can modify other users');
		}	else if ($user_id === 0) {
		 response_send(false, 'user ID wasn\'t given');
		}

    $sql = "DELETE FROM `users` WHERE `id` = {$user_id}";

		// FOR DEBUGGING
		if (is_debugging()) {
		 array_push($GLOBALS['response']['debug']['database']['sql'], $sql);
		}

		if ($GLOBALS['conn']->query($sql) == True) {
      $GLOBALS['response']['data']['id'] = $user_id;

			response_status(true, 'sucesfully removed user');
    } else {
			response_status(false, 'unsucesfully removed user');
    }
	}

?>
