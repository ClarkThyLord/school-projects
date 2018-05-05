<?php

	/**
	* Setup SQL database's tables.
	* @return {undefined} Returns nothing.
	*/
	function user_login() {
		// Get all users with given username
		$sql = "SELECT * FROM `users` WHERE `username` =  '{$_POST['username']}'";

		if (is_debugging()) {
			$GLOBALS['response']['debug']['sql'] = $sql;
		}

		$result = $GLOBALS['conn']->query($sql);
		if ($result->num_rows > 0) {
			$GLOBALS['response']['data']['username'] = true;
			while ($user = $result->fetch_assoc()) {
				if ($_POST['password'] === $user['password']) {
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

?>
