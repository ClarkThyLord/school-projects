<?php

  // CONNECT TO DB
  // ***************************************************************************


  // DB Credentials
  $servername = "localhost";
  $username = "root";
  $password = "";
  $dbname = "kanban";

  // Create connection to DB
  $conn = new mysqli($servername, $username, $password, $dbname);
  // Check connection
  if ($conn->connect_error) {
    die("Connection Failed: " . $conn->connect_error);
  }


  // COMMON FUNCTIONS
  // ***************************************************************************


	/**
	 * Checks whether current caller has required level of access.
	 * @param {integer} required_level Level of access required.
	 * @return {boolean} True, has required level of access; false, doesn' have required level of access.
	 */
	function check_access($required_level=0) {
    // Initialize session if not already
    if(!isset($_SESSION)) {
      session_start();
    }

		if ((array_key_exists("user_data", $_SESSION) && !empty($_SESSION["user_data"])) && $_SESSION["user_data"]["access"] >= $required_level) {
			return true;
		} else {
		 	// TODO PROMPT ADMIN VIA LOG OF ATTEMP TO ACCESS API
      // $GLOBALS["response"]["status"] = "access denied";
      // $GLOBALS["response"]["reason"] = "insufficient access level";
			return false;
		}
   }


   // USER FUNCTIONS
   // **************************************************************************


  /**
   * Login with credentials.
   * @return {undefined} Returns nothing.
   */
  function login () {
    // Access level
    // 0 - viewer
    // 1 - worker
    // 2 - admin

    $GLOBALS["response"]["data"]["login"] = array("granted" => 0, "reasons" => array("username" => false, "password" => false));

    // Initialize session if not already
    if(!isset($_SESSION)) {
      session_start();
    }

    // TODO connect to SQL and check user DB
    $sql = 'SELECT * FROM `users` WHERE `username` = "' . $_POST["username"] . '"';
    $result = $GLOBALS["conn"]->query($sql);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["login"]["reasons"]["username"] = true;
      while($row = $result->fetch_assoc()) {
        if ($_POST["password"] == $row["password"]) {
          $GLOBALS["response"]["data"]["login"]["granted"] = 1;
          $GLOBALS["response"]["data"]["login"]["reasons"]["password"] = true;

          // Setup user data for session
          $_SESSION["user_data"] = array("id" => $row["id"], "username" => $row["username"], "access" => $row["access"]);

          $GLOBALS["response"]["status"] = "logged in";
          $GLOBALS["response"]["reason"] = "sucesfully logged in";
          break;
        } else {
          $GLOBALS["response"]["status"] = "not logged in";
          $GLOBALS["response"]["reason"] = "unsucesfully logged in";
        }
      }
    } else {
      $GLOBALS["response"]["status"] = "not logged in";
      $GLOBALS["response"]["reason"] = "unsucesfully logged in";
    }
    $GLOBALS["conn"]->close();
  }

?>
