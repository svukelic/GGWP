var rect = document.getElementById("rect");
var username = document.getElementById("username");
var password = document.getElementById("password");

function handle1() {
    rect.setAttribute("class", "rect2");
}

function handle2() {
    rect.setAttribute("class", "rect1");
}

setTimeout(() => {
    password.focus();
}, 500);

setTimeout(() => {
    username.focus();
}, 1500);