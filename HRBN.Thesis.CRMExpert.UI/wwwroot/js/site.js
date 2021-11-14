// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let formularz = document.querySelector(".d-flex.ms-auto");

formularz.onsubmit = (e) => {
    e.preventDefault();
    let baton = e.target.children;
    for (var i = 0; i < baton.length; i++) {
        if (baton[i].tagName.toLowerCase() === "input") {
            baton[i].value = "chuj";
        }
    }
};