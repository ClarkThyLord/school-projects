<?php

  // Initialize session if not already
  if (!isset($_SESSION)) {
    session_start();
  }


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
			return false;
		}
   }


   // USER FUNCTIONS
   // **************************************************************************


  /**
   * Login with credentials.
   * @return {undefined} Returns nothing.
   */
  function user_login () {
    // Access level
    // 0 - viewer
    // 1 - worker
    // 2 - admin

    $GLOBALS["response"]["data"]["login"] = array("granted" => 0, "reasons" => array("username" => false, "password" => false));

    $sql = 'SELECT * FROM `users` WHERE `username` = "' . $_POST["username"] . '"';

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    $result = $GLOBALS["conn"]->query($sql);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["login"]["reasons"]["username"] = true;
      while($row = $result->fetch_assoc()) {
        if ($_POST["password"] == $row["password"]) {
          $GLOBALS["response"]["data"]["login"]["granted"] = 1;
          $GLOBALS["response"]["data"]["login"]["reasons"]["password"] = true;

          // Setup user data for session
          $_SESSION["user_data"] = array("id" => $row["id"], "username" => $row["username"], "access" => $row["access"]);

          $GLOBALS["response"]["status"] = "success";
          $GLOBALS["response"]["reason"] = "sucesfully logged in";
          break;
        } else {
          $GLOBALS["response"]["status"] = "failure";
          $GLOBALS["response"]["reason"] = "unsucesfully logged in";
        }
      }
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully logged in";
    }
    $GLOBALS["conn"]->close();
  }


  /**
  * Create a user.
  * @return {undefined} Returns nothing.
  */
  function user_create () {
    // Check for access
    if (!check_access(2)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $sql = "INSERT INTO `users` (`id`, `username`, `password`, `access`) VALUES (NULL, '" . $_POST["username"] . "', '" . $_POST["password"] . "', '" . $_POST["access"] . "')";

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    if ($GLOBALS["conn"]->query($sql) == True) {
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created user";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created user";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Remove a user.
  * @return {undefined} Returns nothing.
  */
  function user_remove () {
   // Check for access
   if (!(check_access(2) || $_POST["user_id"] == $_SESSION["id"])) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `users` WHERE `users`.`id` = " . $_["user_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully removed user";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully removed user";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Modify a user.
  * @return {undefined} Returns nothing.
  */
  function user_modify () {
   // Check for access
   if (!(check_access(2) || $_POST["user_id"] == $_SESSION["id"])) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

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

   $sql = "UPDATE `users` SET " . $changes . " WHERE `users`.`id` = " . $_POST["user_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully modified user";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully modified user";
   }

   $GLOBALS["conn"]->close();
  }

?>
