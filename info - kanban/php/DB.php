<?php

  /**
   * Login with credentials.
   * @return {undefined} Returns nothing.
   */
  function login () {
    // Access level
    // 0 - viewer
    // 1 - worker
    // 2 - admin

    $response["data"]["login"] = array("granted" => 0, "reasons" => array("username" => false, "password" => false));

    // Initialize session if not already
    if(!isset($_SESSION)) {
      session_start();
    }

    // TODO connect to SQL and check user DB

    // Check username
    if ($_POST["username"] == "admin") {
      $GLOBALS["response"]["data"]["login"]["granted"] += 0.5;
      $GLOBALS["response"]["data"]["login"]["reasons"]["username"] = true;
    }

    // Check password
    if ($_POST["password"] == "root") {
      $GLOBALS["response"]["data"]["login"]["granted"] += 0.5;
      $GLOBALS["response"]["data"]["login"]["reasons"]["password"] = true;
    }

    // Check if access granted
    if ($response["granted"] == 1) {
      $_SESSION["user_data"] = array("user" => "admin", "access" => 2);

      $GLOBALS["response"]["status"] = "logged in";
      $GLOBALS["response"]["reason"] = "sucesfully logged in";
    } else {
      $GLOBALS["response"]["status"] = "not logged in";
      $GLOBALS["response"]["reason"] = "unsucesfully logged in";
    }
  }

?>
