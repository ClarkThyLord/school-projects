let histBtn = document.getElementById("add-hist")
let invBtn = document.getElementById("add-inv")
let prodBtn = document.getElementById("add-prod")

let str = '<div class="board-item"><div class="board-item-content"><span>Item #</span>1</div></div>'

histBtn.addEventListener("click", add);
// invBtn.addEventListener("click", add);
// prodBtn.addEventListener("click", add);

function add(){
  $("before-this-hist").before(str)
}
