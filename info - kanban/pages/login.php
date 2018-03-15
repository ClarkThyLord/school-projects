<!DOCTYPE html>
<html>

<head>
  <title>Kanban</title>

  <!-- Meta -->
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />

  <!-- Import JS -->
  <script src="./js/lib/muuri.min.js"></script>

</head>

<body>

  <form action="../php/login.php" method="post">
    <label>User name: <input type="text" placeholder="User name..." name="user_name"/></label>
    <label>Password: <input type="password" placeholder="Password..." name="user_password"/></label>
    <input type="submit" value="Login" />
    <input type="reset" value="Reset" />
  </form>

</body>

</html>
