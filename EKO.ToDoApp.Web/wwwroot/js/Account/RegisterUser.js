import BuildToast from "../Shared/ToastBuilder.js";


document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelector("#registerButton");

    button.addEventListener("click", async () => {
        const firstName = document.querySelector("#userFirstName").value;
        const lastName = document.querySelector("#userLastName").value;
        const email = document.querySelector("#userEmail").value;
        const password = document.querySelector("#userPassword").value;

        const data = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password
        };

        const response = await fetch("/account/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            localStorage.setItem("userEmail", email);

            window.location.href = "/account/login";
        } else {
            const toast = BuildToast("Failed to register! An user might already exist with that email...", true);

            const toastContainer = document.querySelector("#toasts");

            toastContainer.appendChild(toast);

            const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);

            toastBootstrap.show();
        }
    });
});