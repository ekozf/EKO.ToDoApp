document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelector("#updateTask");

    button.addEventListener("click", async function () {
        await UpdateTask();
    });

    const cancel = document.querySelector("#cancelTask");

    cancel.addEventListener("click", function () {
        window.location.href = "/";
    });

    const deleteTask = document.querySelector("#deleteTask");

    deleteTask.addEventListener("click", async function () {
        await DeleteTask();
    });
});

async function UpdateTask() {
    const taskTitle = document.querySelector("#taskTitle").value;
    const taskDescription = document.querySelector("#taskDescription").value;
    const taskDueBy = document.querySelector("#taskDueBy").value;
    const selectUrgency = document.querySelector("#selectUrgency").value;
    const selectList = document.querySelector("#selectList").value;

    const formData = new FormData();

    const input = document.querySelector("#taskId");

    formData.append("id", input.value);
    formData.append("title", taskTitle);
    formData.append("description", taskDescription);
    formData.append("dueBy", taskDueBy);
    formData.append("urgency", selectUrgency);
    formData.append("taskListId", selectList);
    formData.append("isCompleted", false);

    const response = await fetch("/update", {
        method: "PUT",
        body: formData,
    });

    if (response.ok) {
        window.location.href = "/";
    } else {
        const toast = BuildToast("Error creating task!", true);

        const toastContainer = document.querySelector("#toasts");

        toastContainer.appendChild(toast);

        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

        toastBootstrap.show();

        input.checked = false;

        return;
    }

    window.location.href = "/";
}

async function DeleteTask() {
    const input = document.querySelector("#taskId");

    const taskId = input.value;

    const formData = new FormData();
    formData.append("id", taskId);

    const response = await fetch("/delete", {
        method: "DELETE",
        body: formData
    });

    if (!response.ok) {
        const toast = BuildToast("Error deleting task!", true);

        const toastContainer = document.querySelector("#toasts");

        toastContainer.appendChild(toast);

        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

        toastBootstrap.show();

        input.checked = false;

        return;
    }

    window.location.href = "/";
}