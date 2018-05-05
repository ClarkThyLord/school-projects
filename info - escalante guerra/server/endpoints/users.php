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

					response_status(true, 'sucesfully logged in');
					response_send();
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
	* @param identifier [array] Properties to identify user(s) with.
	* @param data [array] Data to modify user with.
	* @return {undefined} Returns nothing.
	*/
	function user_modify($identifier=array(), $data=array()) {
   access_level_check(2);

   $another = False;
   $changes = '';
   foreach ($_POST as $key => $value) {
     if ($key == 'id' || $key == 'user_id') {
       continue;
     } else {
       $changes .= "`{$key}` = '{$value}'";
       if ($another == True) {
         $changes .= ',';
       } else {
         $another = True;
       }
     }
	 }
	}


	/**
	* Remove a user in the SQL database.
	* @param identifier [array] Properties to identify user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function user_remove($identifier=array()) {
		access_level_check(2);

    $sql = 'DELETE FROM `users` WHERE `users`.`id` = ' . $_POST['user_id'];
    if ($GLOBALS['conn']->query($sql) == True) {
      log_create('sucesfully removed user `id` : ' . $_POST['user_id']);
      $GLOBALS['response']['data']['user_id'] = $_POST['user_id'];
      $GLOBALS['response']['status'] = 'success';
      $GLOBALS['response']['reason'] = 'sucesfully removed user';
    } else {
      $GLOBALS['response']['status'] = 'failure';
      $GLOBALS['response']['reason'] = 'unsucesfully removed user';
    }
	}

?>
