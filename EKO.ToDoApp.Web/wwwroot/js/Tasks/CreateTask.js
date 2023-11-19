document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelector("#createTask");

    button.addEventListener("click", async function () {
        await CreateTask();
    });

    const cancel = document.querySelector("#cancelTask");

    cancel.addEventListener("click", function () {
        window.location.href = "/";
    });
});

async function CreateTask() {
    const taskTitle = document.querySelector("#taskTitle").value;
    const taskDescription = document.querySelector("#taskDescription").value;
    const taskDueBy = document.querySelector("#taskDueBy").value;
    const selectUrgency = document.querySelector("#selectUrgency").value;
    const selectList = document.querySelector("#selectList").value;

    const formData = new FormData();

    formData.append("title", taskTitle);
    formData.append("description", taskDescription);
    formData.append("dueBy", taskDueBy);
    formData.append("urgencyLevel", selectUrgency);
    formData.append("AssociatedList", selectList);

    const response = await fetch("/create-task", {
        method: "POST",
        body: formData,
    });

    if (response.ok) {
        window.location.href = "/";
    } else {
        alert("Error!")
    }
}