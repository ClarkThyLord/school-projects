window.onload = function() {
  $.getJSON('./assets/forms/curriculum.json',
    function(data) {
      console.log(data);

      create_form_html(data);
    });
};

function create_form_html(data) {
  var form = $('<form></form>');
  for (var key in data) {
    value = data[key];

    console.log(value);

    var dom;
    switch (value.type) {
      case 'break':
        dom = $('<hr />');
        break;
      case 'text':
        dom = $('<input type="text" placeholder="Inserte aquí..." />');
        break;
      case 'textarea':
        dom = $('<textarea placeholder="Inserte aquí..."></textarea>');
        break;
      case 'email':
        dom = $('<input type="email" placeholder="ejemplo@gmail.com" />');
        break;
      case 'phonenumber':
        dom = $('<input type="tel" placeholder="(###) ### - ####" />');
        break;
      case 'dropdown':
        var dom_string = '<select>';

        for (var sub_key in value.extra.options) {
          dom_string += '<option value="' + value.extra.options[sub_key] + '">' + value.extra.options[sub_key] + '</option>';
        }

        dom_string += '</select>';

        dom = $(dom_string);
        break;
      case 'file':
        dom = $('<input type="file" />');
        break;
      case 'files':
        dom = $('<input type="file" multiple />');
        break;
      default:
        continue;
    }

    if (value.label) dom = $(dom).wrap('<label>' + value.label + ': ' + (value.extra && value.extra.comment ? ('<span style="font-size: 10px; opacity: 0.7;">' + value.extra.comment + '</span>') : '') + '</label>').parent()[0];

    if (value.required) $(dom).prop("required", true);

    console.log(dom);
    console.log('---');

    form.append(dom);
  }

  console.log(form);

  var w = window.open();
  $(w.document.body).html(form);
}