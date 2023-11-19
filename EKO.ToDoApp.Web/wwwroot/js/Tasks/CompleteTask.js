import BuildToast from "../Shared/ToastBuilder.js";

document.addEventListener("DOMContentLoaded", function () {
    const tasks = document.querySelectorAll("[data-task]");

    tasks.forEach(task => {
        const input = task.querySelector("input[type='checkbox']");

        input.addEventListener("change", async function () {
            await CompleteTask(task);
        });
    });
});

async function CompleteTask(task) {
    const input = task.querySelector("input[type='checkbox']");

    if (!input.checked) {
        return;
    }

    const formData = new FormData();
    formData.append("id", input.name);

    const response = await fetch("/complete", {
        method: "POST",
        body: formData
    });


    if (!response.ok) {
        const toast = BuildToast("Error completing task!", true);

        const toastContainer = document.querySelector("#toasts");

        toastContainer.appendChild(toast);

        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

        toastBootstrap.show();

        input.checked = false;

        return;
    }

    const taskTitle = task.querySelector("[data-task-title]").textContent;

    const toast = BuildToast(`Task '${taskTitle}' was completed!`, false);

    const toastContainer = document.querySelector("#toasts");

    toastContainer.appendChild(toast);

    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

    toastBootstrap.show();
}