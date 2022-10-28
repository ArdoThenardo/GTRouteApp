var itemButtonsTooltip;

export function initializeTooltips(element) {
    itemButtonsTooltip = new bootstrap.Tooltip(element);
}

export function hideTooltips() {
    itemButtonsTooltip.hide();
}

export function confirmAlert(message) {
    if (confirm(message) == true) {
        return true;
    } else {
        return false;
    }
}