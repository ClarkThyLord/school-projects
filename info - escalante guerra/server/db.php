<?php

	// Check if current client has valid access
	// ***************************************************************************

	// Initialize session if not already
  if (!isset($_SESSION)) {
    session_start();
  }

	// Check if user isn't in a session
	if (!array_key_exists('user', $_SESSION) || empty($_SESSION['user'])) {
		$GLOBALS['response']['status'] = 'failure';
		$GLOBALS['response']['reason'] = 'access not granted';

		send_response();
	}

	// Credentials
  // ***************************************************************************

	// Common Credentials
  $server_name = 'localhost';

	// Local Credentials
	$user_name = 'root';
  $password = '';
  $db_name = 'eg';

	// Server Credentials
  // $user_name = '';
  // $password = '';
  // $db_name = '';

	// Initialize connection to SQL database
  // ***************************************************************************

	$conn = new mysqli($server_name, $user_name, $password, $db_name);
  // Check connection
  if ($conn->connect_error) {
		$GLOBALS['response']['status'] = 'failure';
		$GLOBALS['response']['reason'] = "connection to SQL database couldn\'t be initialize {$conn->connect_error}";

		send_response();
  }

?>
