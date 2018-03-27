<?php

  // Access level
  // 0 - viewer
  // 1 - worker
  // 2 - admin

  $response = array("granted" => 0, "reasons" => array("username" => false, "password" => false));

  // Initialize session if not already
  if(!isset($_SESSION)) {
    session_start();
  }

  // TODO connect to SQL and check user DB

  // Check username
  if ($_POST["username"] == "admin") {
    $GLOBALS["response"]["granted"] += 0.5;
    $GLOBALS["response"]["reasons"]["username"] = true;
  }

  // Check password
  if ($_POST["password"] == "root") {
    $GLOBALS["response"]["granted"] += 0.5;
    $GLOBALS["response"]["reasons"]["password"] = true;
  }

  // Check if access granted
  if ($response["granted"] == 1) {
    $_SESSION["user_data"] = array("user" => "admin", "access" => 2);
  }

  // Echo response
  echo json_encode($GLOBALS["response"]);

?>
