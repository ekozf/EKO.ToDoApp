function BuildToast(message, failed) {

    const toast = document.createElement('div');

    toast.classList.add('toast');
    toast.setAttribute('role', 'alert');
    toast.setAttribute('aria-live', 'assertive');
    toast.setAttribute('aria-atomic', 'true');

    const toastHeader = document.createElement('div');

    toastHeader.classList.add('toast-header');

    const toastStrong = document.createElement('strong');

    toastStrong.classList.add('me-auto');

    if (failed) {
        toastStrong.classList.add('text-danger');
        toastStrong.textContent = 'Error';
    } else {
        toastStrong.classList.add('text-success');
        toastStrong.textContent = 'Success';
    }


    const toastButton = document.createElement('button');

    toastButton.setAttribute('type', 'button');
    toastButton.classList.add('btn-close');
    toastButton.setAttribute('data-bs-dismiss', 'toast');
    toastButton.setAttribute('aria-label', 'Close');

    const toastBody = document.createElement('div');

    toastBody.classList.add('toast-body');
    toastBody.textContent = message;

    toastHeader.appendChild(toastStrong);
    toastHeader.appendChild(toastButton);

    toast.appendChild(toastHeader);
    toast.appendChild(toastBody);

    return toast;
}

export default BuildToast;