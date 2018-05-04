/**
 * Log in with given credentials.
 * @param {string} username Username to login with.
 * @param {string} password Password to login with.
 * @return {undefined} Returns nothing.
 */
function login(username, password) {
  $.post({
    url: './server/server.php/user/login',
    data: {
      'username': username,
      'password': password
    },
    success: function(response) {
      response = JSON.parse(response);
      if (response.status === 'success') {
        window.location.href = './dashboard.php';
      } else {
        alert(((response.data.username === null || !response.data.username) ? 'nombre de usuario no existe!' : '') + ((response.data.password === null || !response.data.password) ? '\nla contraseña no esta correcta!' : ''));
      }
    }
  });
}


/**
 * Log out.
 * @return {undefined} Returns nothing.
 */
function logout() {
  $.ajax({
    url: './server/server.php/user/logout',
    success: function(response) {
      window.location.href = './index.php';
    }
  });
}