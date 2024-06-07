// wwwroot/toggleDarkMode.js
window.toggleDarkMode = function (isDarkMode) {
    var element = document.body;
    if (isDarkMode) {
        element.classList.add("dark-mode");
    } else {
        element.classList.remove("dark-mode");
    }
};
