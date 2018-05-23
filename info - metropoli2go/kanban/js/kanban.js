var GLOBALS = {
  dad: undefined
};

window.onload = function() {
  // // Setup Dragula.js
  // var dad = dragula({
  //   isContainer: function(el) {
  //     return el.classList.contains("dragula-container");
  //   }
  // });
  //
  // dad.on("drop", function(el, target, source, sibling) {
  //   $(el).attr("data-table-id", $(target).attr("data-table-id"));
  //
  //   modifyTask($(el).attr("data-task-id"), {
  //     "table_id": $(target).attr("data-table-id")
  //   });
  // });
  //
  // // Setup FileDrop.js
  // $("#task_file_dropzone").filedrop({
  //   fallback_id: "new_files",
  //   fallback_dropzoneClick: true,
  //   withCredentials: true,
  //   data: {
  //     "task_id": function() {
  //       return current_task;
  //     }
  //   },
  //   url: "./php/API.php/files/create",
  //   error: function(err, file) {
  //     console.log(err);
  //     switch (err) {
  //       case 'BrowserNotSupported':
  //         alert('browser does not support HTML5 drag and drop');
  //         break;
  //       case 'TooManyFiles':
  //         break;
  //       case 'FileTooLarge':
  //         break;
  //       case 'FileTypeNotAllowed':
  //         break;
  //       case 'FileExtensionNotAllowed':
  //         break;
  //       default:
  //         break;
  //     }
  //   },
  //   uploadStarted: function(i, file, len) {},
  //   uploadFinished: function(i, file, response, time) {
  //     if (response.status === "success") {
  //       for (var file in Object.keys(response.data.files)) {
  //         file = response.data.files[file];
  //         $("#task_file_preview").append('<div onclick="current_file = $(this).attr(\'data-file-id\');" class="file" data-table-id="' + current_table + '" data-task-id="' + current_task + '" data-file-id="' + file.id + '" data-file-url="' + file.url + '"> <span class="name selectable" onclick="$(\'.file-preview\').attr(\'src\',\'' + file.url + '\'); $(\'#file_preview\').dialog(\'open\');"> ' + file.name + ' </span> <input type="button" value="&#10006;" onclick="removeFile(' + file.id + ');" /> </div>');
  //       }
  //
  //       if (prevent_popups == false) {
  //         alert(response.reason);
  //       }
  //     } else {
  //       alert(response.reason);
  //     }
  //   },
  //   progressUpdated: function(i, file, progress) {},
  //   globalProgressUpdated: function(progress) {},
  //   speedUpdated: function(i, file, speed) {},
  //   rename: function(name) {},
  //   beforeEach: function(file) {},
  //   beforeSend: function(file, i, done) {
  //     done();
  //   },
  //   afterAll: function() {}
  // });
};

// CONTENT Functions
// *****************************************************************************

/**
 * Scroll Kanban content to the left.
 * @return {undefined} Returns nothing.
 */
function scroll_left() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position - 150
  }, 100);
}


/**
 * Scroll Kanban content to the right.
 * @return {undefined} Returns nothing.
 */
function scroll_right() {
  var new_position = $(".kanban").scrollLeft();

  $(".kanban").animate({
    scrollLeft: new_position + 150
  }, 100);
}