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

					break;
				} else {
					response_status(false, 'unsucesfully logged in');
				}
			}
		} else {
			response_status(false, 'unsucesfully logged in');
		}
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
    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }
      $conditions .= "`" . $key . "` = '" . $value . "'";
    }

    $users = $GLOBALS["conn"]->query("SELECT `id`, `name`, `access` FROM `users`" . $conditions);
    if ($users->num_rows > 0) {
      $GLOBALS["response"]["data"]["users"] = array();
      while($user = $users->fetch_assoc()) {
        $GLOBALS["response"]["data"]["users"][$user["id"]] = $user;
      }
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid user(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid user(s)";
    }
	}


	/**
	* Add a user to the SQL database with the given data.
	* @param data [array] Data to create user with.
	* @return {undefined} Returns nothing.
	*/
	function user_add($data=array()) {

    // Check for access
    if (!check_access(2)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $sql = "INSERT INTO `users` (`id`, `name`, `password`, `access`) VALUES (NULL, '" . $_POST["name"] . "', '" . $_POST["password"] . "', " . $_POST["access"] . ")";
    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;
    if ($GLOBALS["conn"]->query($sql) === TRUE) {
      $insert_id = $GLOBALS["conn"]->insert_id;
      log_create("sucesfully created user `id` : " . $insert_id);
      $sql = "SELECT `id`, `name` FROM `users` WHERE `id` = '" . $insert_id . "' LIMIT 1";
      // FOR DEBUGGING
      $GLOBALS["response"]["sub_sql"] = $sql;
      $GLOBALS["response"]["data"]["user"] = $GLOBALS["conn"]->query($sql)->fetch_assoc();
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created user";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created user";
    }
	}


	/**
	* Modify a user in the SQL database.
	* @param identifier [array] Properties to identify user(s) with.
	* @param data [array] Data to modify user with.
	* @return {undefined} Returns nothing.
	*/
	function user_modify($identifier=array(), $data=array()) {
   access_check(2);

   $another = False;
   $changes = "";
   foreach ($_POST as $key => $value) {
     if ($key == "id" || $key == "user_id") {
       continue;
     } else {
       $changes .= "`" . $key . "` = '" . $value . "'";
       if ($another == True) {
         $changes .= ",";
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
		access_check(2);

    $sql = "DELETE FROM `users` WHERE `users`.`id` = " . $_POST["user_id"];
    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully removed user `id` : " . $_POST["user_id"]);
      $GLOBALS["response"]["data"]["user_id"] = $_POST["user_id"];
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully removed user";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully removed user";
    }
	}

?>
