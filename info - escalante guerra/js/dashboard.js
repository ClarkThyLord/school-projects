/**
 * Change to content via their tag.
 * @param {string} content Content's tag.
 * @return {undefined} Returns nothing.
 */
function content_change(content) {
  // Close other content and open specified content
  $('.page-content').hide();
  $('#' + content).show();

  if (content === 'users') {
    $.get({
      url: './server/api.php/users/get?debug=' + DEBUGGING + '&filter=' + JSON.stringify({}) + '&options=' + JSON.stringify({}),
      success: function(response) {
        response = JSON.parse(response);

        console.log(response);
      }
    });
  }

  // Change current in MENU
  $('.nav-item > .active').removeClass('active');
  $('.nav-item[data-location="' + content + '"] > a').addClass('active');
}

/**
 * Portrait given user and users in the page.
 * @param {object} user User belonging to client.
 * @param {object} users Users to portrait.
 * @return {undefined} Returns nothing.
 */
function user_table(user, users) {}