var DEBUGGING = {
  console: true,
  popups: false,
  server: true
};

window.onload = function() {
  console.log('PAGE LOADED\n---');
};

/**
 * Log in with given credentials.
 * @param {string} username Username to login with.
 * @param {string} password Password to login with.
 * @return {undefined} Returns nothing.
 */
function login(username, password) {
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/users/login?debug=' + DEBUGGING.server,
    data: {
      username: username,
      password: password
    },
    success: function(response) {
      response = JSON.parse(response);

      $('body').waitMe('hide');

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
  $('body').waitMe({
    waitTime: -1,
    effect: 'stretch',
    text: 'Cargando...',
    bg: 'rgba(255, 255, 255, 0.7)',
    color: 'rgba(0, 0, 0)',
  });

  $.post({
    url: './server/api.php/users/logout?debug=' + DEBUGGING.server,
    success: function(response) {
      window.location.href = './index.php';
    }
  });
}