function login(username, password) {
  // This probably isn't safe >_>
  $.post({
    url: "./php/login.php",
    data: {
      "username": username,
      "password": password
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.granted === 1) {
        window.location.href = "./index";
      } else {
        msg = "";
        if (response.reasons.username === false) {
          msg += "username doesn't exist!";
        }
        if (response.reasons.password === false) {
          msg += "\npassword isn't correct!";
        }
        alert(msg);
      }
    }
  });
}

function logout() {
  $.ajax({
    url: "./php/logout.php",
    success: function(response) {
      window.location.href = "./login";
    }
  });
}