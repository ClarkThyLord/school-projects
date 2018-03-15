<?php
// Include this PHP file to any page which requires credentials
session_start();
if (array_key_exists("user_name", $_POST) && array_key_exists("user_password", $_POST)) {
  if ($_POST["user_name"] == "admin" && $_POST["user_password"] == "123") {
    $_SESSION["user_data"] = array("user_name" => "admin", "access" => 3);
  }
}

if (!array_key_exists("user_data", $_POST)) {
  header("Location: ./pages/login.php?site=index");
}
else if ($_SESSION["user_data"]["access"] >= $access_level) {
  echo "hello world!";
}
?>
