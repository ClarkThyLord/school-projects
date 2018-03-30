function login(username, password) {
  $.post({
    url: "./php/API.php/user/login",
    data: {
      "name": username,
      "password": password
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.data.login.granted === 1) {
        window.location.href = "./index";
      } else {
        msg = "";
        if (response.data.login.reasons.username === false) {
          msg += "username doesn't exist!";
        }
        if (response.data.login.reasons.password === false) {
          msg += "\npassword isn't correct!";
        }
        alert(msg);
      }
    }
  });
}

function logout() {
  $.ajax({
    url: "./php/API.php/user/logout",
    success: function(response) {
      window.location.href = "./login";
    }
  });
}