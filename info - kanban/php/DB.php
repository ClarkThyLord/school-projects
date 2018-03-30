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

    $sql = 'SELECT * FROM `users` WHERE `name` = "' . $_POST["name"] . '"';

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    $result = $GLOBALS["conn"]->query($sql);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["login"]["reasons"]["name"] = true;
      while($row = $result->fetch_assoc()) {
        if ($_POST["password"] == $row["password"]) {
          $GLOBALS["response"]["data"]["login"]["granted"] = 1;
          $GLOBALS["response"]["data"]["login"]["reasons"]["password"] = true;

          // Setup user data for session
          $_SESSION["user_data"] = array("id" => $row["id"], "name" => $row["name"], "access" => $row["access"]);

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

    $sql = "INSERT INTO `users` (`id`, `name`, `password`, `access`) VALUES (NULL, '" . $_POST["name"] . "', '" . $_POST["password"] . "', " . $_POST["access"] . ")";

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    if ($GLOBALS["conn"]->query($sql) === TRUE) {
      $GLOBALS["response"]["data"]["user"] = $GLOBALS["conn"]->query("SELECT * FROM `users` WHERE `name` = '" . $_POST["name"] . "' AND `password` = '" . $_POST["password"] . "' LIMIT 1")->fetch_assoc();

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
   if (!(check_access(2) || $_POST["user_id"] == $_SESSION["user_data"]["id"])) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `users` WHERE `users`.`id` = " . $_POST["user_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["data"]["user_id"] = $_POST["user_id"];

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
   if (!(check_access(2) || ($_POST["user_id"] == $_SESSION["user_data"]["id"] && !array_key_exists("access", $_POST)))) {
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


  // TABLE FUNCTIONS
  // ***************************************************************************


  /**
  * Create a table.
  * @return {undefined} Returns nothing.
  */
  function table_create () {
    // Check for access
    if (!check_access(1)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $sql = "INSERT INTO `tables` (`id`, `name`) VALUES (NULL, '" . $_POST["table_name"] . "')";

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    if ($GLOBALS["conn"]->query($sql) == True) {
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created table";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created table";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Remove a table.
  * @return {undefined} Returns nothing.
  */
  function table_remove () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `tables` WHERE `tables`.`id` = " . $_["table_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully removed table";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully removed table";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Modify a table.
  * @return {undefined} Returns nothing.
  */
  function table_modify () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $another = False;
   $changes = "";
   foreach ($_POST as $key => $value) {
     if ($key == "id" || $key == "table_id") {
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

   $sql = "UPDATE `tables` SET " . $changes . " WHERE `tables`.`id` = " . $_POST["table_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully modified table";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully modified table";
   }

   $GLOBALS["conn"]->close();
  }


  // TASK FUNCTIONS
  // ***************************************************************************


  /**
  * Create a task.
  * @return {undefined} Returns nothing.
  */
  function task_create () {
    // Check for access
    if (!check_access(1)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $sql = "INSERT INTO `tasks` (`id`, `table_id`, `name`) VALUES (NULL, '" . $_POST["table_id"] . "', '" . $_POST["task_name"] . "')";

    // FOR DEBUGGING
    $GLOBALS["response"]["sql"] = $sql;

    if ($GLOBALS["conn"]->query($sql) == True) {
      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created task";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created task";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Remove a task.
  * @return {undefined} Returns nothing.
  */
  function task_remove () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `tasks` WHERE `tasks`.`id` = " . $_["tasks_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully removed task";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully removed task";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Modify a task.
  * @return {undefined} Returns nothing.
  */
  function task_modify () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $another = False;
   $changes = "";
   foreach ($_POST as $key => $value) {
     if ($key == "id" || $key == "task_id") {
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

   $sql = "UPDATE `tasks` SET " . $changes . " WHERE `tasks`.`id` = " . $_POST["task_id"];

   // FOR DEBUGGING
   $GLOBALS["response"]["sql"] = $sql;

   if ($GLOBALS["conn"]->query($sql) == True) {
     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully modified task";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully modified task";
   }

   $GLOBALS["conn"]->close();
  }

?>
