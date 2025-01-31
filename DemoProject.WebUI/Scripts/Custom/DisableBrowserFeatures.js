$(document).ready(function ()
{

    // Nakul Pandit

    // Push a new state to the history
    history.pushState(null, null, window.location.href);

    // Listen for popstate event, which is triggered on back/forward navigation
    window.onpopstate = function () {
        // Re-push the state to prevent back navigation
        history.pushState(null, null, window.location.href);
    };
});

// Disable Right-Click (Context Menu)
document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
});

// Disable Certain Keys (F12, Ctrl+Shift+I, Ctrl+Shift+J, Ctrl+U)
document.onkeydown = function (e) {
    // Disable F12
    if (e.keyCode === 123) {
        return false;
    }
    // Disable Ctrl+Shift+I (Inspect)
    if (e.ctrlKey && e.shiftKey && e.keyCode === 73) {
        return false;
    }
    // Disable Ctrl+Shift+J (Console)
    if (e.ctrlKey && e.shiftKey && e.keyCode === 74) {
        return false;
    }
    // Disable Ctrl+U (View Source)
    if (e.ctrlKey && e.keyCode === 85) {
        return false;
    }
};

