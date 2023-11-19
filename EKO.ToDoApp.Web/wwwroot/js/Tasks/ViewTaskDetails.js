document.addEventListener("DOMContentLoaded", function () {
    const items = document.querySelectorAll("[data-task-info]");

    items.forEach(item => {
        item.addEventListener("click", function () {
            ViewTaskDetails(item);
        });
    });
});

function ViewTaskDetails(item) {
    window.location.href = "/" + item.dataset.taskInfo;
}