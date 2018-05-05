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

					$GLOBALS['response']['status'] = 'success';
					$GLOBALS['response']['reason'] = 'sucesfully logged in';

					break;
				} else {
					$GLOBALS['response']['status'] = 'failure';
					$GLOBALS['response']['reason'] = 'unsucesfully logged in';
				}
			}
		} else {
			$GLOBALS['response']['status'] = 'failure';
			$GLOBALS['response']['reason'] = 'unsucesfully logged in';
		}
	}


	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function user_logout() {
		session_unset();

		$GLOBALS['response']['status'] = 'success';
		$GLOBALS['response']['reason'] = 'sucesfully logged out';
	}


	/**
	* Get user(s) from SQL datbase that fit the given filter.
	* @param filter [array] Properties to get user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function user_get($filter=array()) {}


	/**
	* Add a user to the SQL database with the given data.
	* @param data [array] Data to create user with.
	* @return {undefined} Returns nothing.
	*/
	function user_add($data=array()) {}


	/**
	* Modify a user in the SQL database.
	* @param identifier [array] Properties to identify user(s) with.
	* @param data [array] Data to modify user with.
	* @return {undefined} Returns nothing.
	*/
	function user_modify($identifier=array(), $data=array()) {}


	/**
	* Remove a user in the SQL database.
	* @param identifier [array] Properties to identify user(s) with.
	* @return {undefined} Returns nothing.
	*/
	function user_remove($identifier=array()) {}

?>
