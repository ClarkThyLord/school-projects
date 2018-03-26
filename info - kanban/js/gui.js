$(function() {
  /* Setup dialogs(menus)
   * Configuration_Menu
   * Login_Menu
   * Category_Menu
   * Task_Menu
   */
  $(".dialog-menu").each(function(index) {
    $(this).dialog({
      // autoOpen: false,
      width: window.innerWidth / 2,
      height: window.innerHeight / 2,
      modal: true,
      resizable: true,
      draggable: false
    });
  });
});