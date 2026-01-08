// Site-wide JavaScript functionality

// Confirm delete actions
function confirmDelete(message) {
    return confirm(message || 'Are you sure you want to delete this item?');
}

// Show success message
function showSuccessMessage(message) {
    alert(message);
}

// Show error message
function showErrorMessage(message) {
    alert(message);
}
