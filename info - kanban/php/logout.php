<?php

  // Initialize session if not already
  if(!isset($_SESSION)) {
    session_start();
  }

  session_unset();

?>
