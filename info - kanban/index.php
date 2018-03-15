<?php

  session_start();
  if (!$_SESSION["user_data"]) {
      header("Location: ./html/login.html");
  }

?>
