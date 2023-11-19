import BuildToast from "../Shared/ToastBuilder.js";

document.addEventListener("DOMContentLoaded", function () {

    const userEmail = localStorage.getItem("userEmail");

    if (userEmail) {
        const toast = BuildToast("Registered successfully!", false);

        const toastContainer = document.querySelector("#toasts");

        toastContainer.appendChild(toast);

        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

        toastBootstrap.show();

        const email = document.querySelector("#userEmail");

        email.value = userEmail;

        localStorage.removeItem("userEmail");
    }

    const button = document.querySelector("#loginButton");

    button.addEventListener("click", async () => {
        await LogInUser();
    });
});

async function LogInUser() {
    const email = document.querySelector("#userEmail").value;

    const password = document.querySelector("#userPassword").value;

    const data = {
        email: email,
        password: password
    };

    const response = await fetch("/account/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        window.location.href = "/"
    } else {
        const toast = BuildToast("Invalid log in details!", true);

        const toastContainer = document.querySelector("#toasts");

        toastContainer.appendChild(toast);

        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

        toastBootstrap.show();

        const email = document.querySelector("#userEmail");

        const userEmail = localStorage.getItem("userEmail");

        if (userEmail) {
            email.value = userEmail;

            localStorage.removeItem("userEmail");
        }
    }
}