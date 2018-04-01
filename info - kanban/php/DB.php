<?php

  // Initialize session if not already
  if (!isset($_SESSION)) {
    session_start();
  }


  // Credentials
  // ***************************************************************************


  // URL
  $server_url = $_SERVER['SERVER_NAME'];

  // DB Credentials
  $server_name = "localhost";
  $user_name = "root";
  $password = "";
  $db_name = "kanban";

  // Create connection to DB
  $conn = new mysqli($server_name, $user_name, $password, $db_name);
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
  * Get users via conditions.
  * @return {undefined} Returns nothing.
  */
  function user_get () {

    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }

      $conditions .= "`" . $key . "` = '" . $value . "'";
    }
    $result = $GLOBALS["conn"]->query("SELECT `id`, `name`, `access` FROM `users`" . $conditions);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["users"] = array();
      while($row = $result->fetch_assoc()) {
        $GLOBALS["response"]["data"]["users"][$row["id"]] = $row;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid user(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid user(s)";
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

    if ($GLOBALS["conn"]->query($sql) === TRUE) {
      log_create("sucesfully created user `id` : " . $GLOBALS["conn"]->insert_id);
      
      $GLOBALS["response"]["data"]["user"] = $GLOBALS["conn"]->query("SELECT `id`, `name` FROM `users` WHERE `id` = '" . $GLOBALS["conn"]->insert_id . "' LIMIT 1")->fetch_assoc();

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

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully removed user `id` : " . $_POST["user_id"]);

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

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully modified user `id` : " . $_POST["user_id"]);

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
  * Get tables via conditions.
  * @return {undefined} Returns nothing.
  */
  function table_get () {

    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }

      $conditions .= "`" . $key . "` = '" . $value . "'";
    }
    $result = $GLOBALS["conn"]->query("SELECT * FROM `tables`" . $conditions);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["tables"] = array();
      while($row = $result->fetch_assoc()) {
        $GLOBALS["response"]["data"]["tables"][$row["id"]] = $row;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid table(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid table(s)";
    }

    $GLOBALS["conn"]->close();
  }


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

    $sql = "INSERT INTO `tables` (`id`, `name`) VALUES (NULL, '" . $_POST["name"] . "')";

    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully created table `id` : " . $GLOBALS["conn"]->insert_id);

      $GLOBALS["response"]["data"]["table"] = $GLOBALS["conn"]->query("SELECT * FROM `tables` WHERE `id` = " . $GLOBALS["conn"]->insert_id . " LIMIT 1")->fetch_assoc();

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

   $sql = "DELETE FROM `tables` WHERE `tables`.`id` = " . $_POST["table_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully removed table `id` : " . $_POST["table_id"]);

     $GLOBALS["response"]["data"]["table_id"] = $_POST["table_id"];

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

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully modified table `id` : " . $_POST["table_id"]);

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
  * Get tasks via conditions.
  * @return {undefined} Returns nothing.
  */
  function task_get () {

    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }

      $conditions .= "`" . $key . "` = '" . $value . "'";
    }
    $result = $GLOBALS["conn"]->query("SELECT * FROM `tasks`" . $conditions);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["tasks"] = array();
      while($row = $result->fetch_assoc()) {
        $GLOBALS["response"]["data"]["tasks"][$row["id"]] = $row;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid task(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid task(s)";
    }

    $GLOBALS["conn"]->close();
  }


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

    $sql = "INSERT INTO `tasks` (`id`, `table_id`, `name`) VALUES (NULL, '" . $_POST["table_id"] . "', '" . $_POST["name"] . "')";

    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully created task `id` : " . $GLOBALS["conn"]->insert_id);

      $GLOBALS["response"]["data"]["task"] = $GLOBALS["conn"]->query("SELECT * FROM `tasks` WHERE `id` = " . $GLOBALS["conn"]->insert_id . " LIMIT 1")->fetch_assoc();

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

   $sql = "DELETE FROM `tasks` WHERE `tasks`.`id` = " . $_POST["task_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
    log_create("sucesfully removed task `id` : " . $_POST["task_id"]);

    $GLOBALS["response"]["data"]["table_id"] = $_POST["table_id"];
    $GLOBALS["response"]["data"]["task_id"] = $_POST["task_id"];

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

   if (count($_FILES) > 0) {
     $GLOBALS["response"]["data"]["files"] = array();

     foreach ($_FILES as $file){
       $result = file_create($file);
       $GLOBALS["response"]["data"]["files"][$file["name"]] = $result;
     }
   }

   $sql = "UPDATE `tasks` SET " . $changes . " WHERE `tasks`.`id` = " . $_POST["task_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
    log_create("sucesfully modified task `id` : " . $_POST["task_id"]);

    $GLOBALS["response"]["status"] = "success";
    $GLOBALS["response"]["reason"] = "sucesfully modified task";
   } else {
    $GLOBALS["response"]["status"] = "failure";
    $GLOBALS["response"]["reason"] = "unsucesfully modified task";
   }

   $GLOBALS["conn"]->close();
  }


  // FILE FUNCTIONS
  // ***************************************************************************


  /**
  * Get files via conditions.
  * @return {undefined} Returns nothing.
  */
  function file_get () {

    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }

      $conditions .= "`" . $key . "` = '" . $value . "'";
    }
    $result = $GLOBALS["conn"]->query("SELECT * FROM `files`" . $conditions);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["files"] = array();
      while($row = $result->fetch_assoc()) {
        $GLOBALS["response"]["data"]["files"][$row["id"]] = $row;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid file(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid file(s)";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Create a file.
  * @param {object} $file File to setup in server.
  * @return {array} Returns on success data related to file; on failer returns a empty array.
  */
  function file_create ($file) {
    $file_content = file_get_contents($file["tmp_name"]);
    file_put_contents("./landmarks/" . $file["name"], $file_content);

    $sql = "INSERT INTO `files` (`id`, `data`, `name`, `url`) VALUES (NULL, NULL, '" . $file["name"] . "', '" . $GLOBALS["server_url"] . $file["name"] . "')";

    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully created a file `id` : " . $GLOBALS["conn"]->insert_id);

      return $GLOBALS["conn"]->query("SELECT * FROM `files` WHERE `id` = " . $GLOBALS["conn"]->insert_id . " LIMIT 1")->fetch_assoc();
    } else {
      return array();
    }
  }


  /**
  * Creates files.
  * @return {undefined} Returns nothing.
  */
  function file_create_direct () {
    // Check for access
    if (!check_access(2)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $GLOBALS["response"]["data"]["files"] = array();

    if (count($_FILES) > 0) {
      foreach ($_FILES as $file){
        $result = file_create($file);
        $GLOBALS["response"]["data"]["files"][$file["name"]] = $result;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created file(s)";
    }
    else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "no file(s) were uploaded";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Remove a file.
  * @return {undefined} Returns nothing.
  */
  function file_remove () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `files` WHERE `files`.`id` = " . $_POST["file_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully removed file `id` : " . $_POST["file_id"]);

     $GLOBALS["response"]["data"]["table_id"] = $_POST["table_id"];
     $GLOBALS["response"]["data"]["file_id"] = $_POST["file_id"];

     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully removed file";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully removed file";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Modify a file.
  * @return {undefined} Returns nothing.
  */
  function file_modify () {
   // Check for access
   if (!check_access(1)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $another = False;
   $changes = "";
   foreach ($_POST as $key => $value) {
     if ($key == "id" || $key == "file_id") {
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

   $sql = "UPDATE `files` SET " . $changes . " WHERE `files`.`id` = " . $_POST["file_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully modified file `id` : " . $_POST["file_id"]);

     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully modified file";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully modified file";
   }

   $GLOBALS["conn"]->close();
  }


  // LOG FUNCTIONS
  // ***************************************************************************


  /**
  * Get log via conditions.
  * @return {undefined} Returns nothing.
  */
  function log_get () {

    $first = True;
    $conditions = "";
    foreach ($_GET as $key => $value) {
      if ($first === True) {
        $first = False;
        $conditions .= " WHERE ";
      }

      $conditions .= "`" . $key . "` = '" . $value . "'";
    }
    $result = $GLOBALS["conn"]->query("SELECT * FROM `logs`" . $conditions);

    if ($result->num_rows > 0) {
      $GLOBALS["response"]["data"]["log"] = array();
      while($row = $result->fetch_assoc()) {
        $GLOBALS["response"]["data"]["log"][$row["id"]] = $row;
      }

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "found valid log(s)";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "found no valid log(s)";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Create a log.
  * @param {string} msg Msg to be logged.
  * @return {undefined} Returns nothing.
  */
  function log_create ($msg) {
    $sql = "INSERT INTO `logs` (`id`, `date`, `user`, `msg`) VALUES (NULL, NULL, '" . $_SERVER["user_data"]["id"] . "', '" . $msg . "')";

    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully created log `id` : " . $GLOBALS["conn"]->insert_id);

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created log";

      return $GLOBALS["conn"]->query("SELECT * FROM `logs` WHERE `id` = " . $GLOBALS["conn"]->insert_id . " LIMIT 1")->fetch_assoc();
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created log";

      return array()
    }
  }


  /**
  * Create a log.
  * @return {undefined} Returns nothing.
  */
  function log_create_direct () {
    // Check for access
    if (!check_access(2)) {
      $GLOBALS["response"]["status"] = "access denied";
      $GLOBALS["response"]["reason"] = "insufficient access level";
      send_response();
    }

    $sql = "INSERT INTO `logs` (`id`, `date`, `user`, `msg`) VALUES (NULL, NULL, '" . $_SERVER["user_data"]["id"] . "', '" . $_POST["msg"] . "')";

    if ($GLOBALS["conn"]->query($sql) == True) {
      log_create("sucesfully created log `id` : " . $GLOBALS["conn"]->insert_id);

      $GLOBALS["response"]["data"]["log"] = $GLOBALS["conn"]->query("SELECT * FROM `logs` WHERE `id` = " . $GLOBALS["conn"]->insert_id . " LIMIT 1")->fetch_assoc();

      $GLOBALS["response"]["status"] = "success";
      $GLOBALS["response"]["reason"] = "sucesfully created log";
    } else {
      $GLOBALS["response"]["status"] = "failure";
      $GLOBALS["response"]["reason"] = "unsucesfully created log";
    }

    $GLOBALS["conn"]->close();
  }


  /**
  * Remove a log.
  * @return {undefined} Returns nothing.
  */
  function log_clear () {
   // Check for access
   if (!check_access(2)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `logs`";

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully cleared log");

     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully cleared log";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully cleared log";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Remove a log.
  * @return {undefined} Returns nothing.
  */
  function log_remove () {
   // Check for access
   if (!check_access(2)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $sql = "DELETE FROM `logs` WHERE `logs`.`id` = " . $_POST["log_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully removed log `id` : " . $_POST["log_id"]);

     $GLOBALS["response"]["data"]["table_id"] = $_POST["table_id"];
     $GLOBALS["response"]["data"]["log_id"] = $_POST["log_id"];

     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully removed log";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully removed log";
   }

   $GLOBALS["conn"]->close();
  }


  /**
  * Modify a log.
  * @return {undefined} Returns nothing.
  */
  function log_modify () {
   // Check for access
   if (!check_access(2)) {
     $GLOBALS["response"]["status"] = "access denied";
     $GLOBALS["response"]["reason"] = "insufficient access level";
     send_response();
   }

   $another = False;
   $changes = "";
   foreach ($_POST as $key => $value) {
     if ($key == "id" || $key == "log_id" || $key == "user") {
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

   $sql = "UPDATE `logs` SET " . $changes . " WHERE `logs`.`id` = " . $_POST["log_id"];

   if ($GLOBALS["conn"]->query($sql) == True) {
     log_create("sucesfully modified log `id` : " . $_POST["log_id"]);

     $GLOBALS["response"]["status"] = "success";
     $GLOBALS["response"]["reason"] = "sucesfully modified log";
   } else {
     $GLOBALS["response"]["status"] = "failure";
     $GLOBALS["response"]["reason"] = "unsucesfully modified log";
   }

   $GLOBALS["conn"]->close();
  }

?>
