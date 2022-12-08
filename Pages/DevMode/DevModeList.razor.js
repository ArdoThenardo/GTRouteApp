var itemButtonsTooltip;

export function initializeTooltips(element) {
    itemButtonsTooltip = new bootstrap.Tooltip(element);
}

export function hideTooltips() {
    itemButtonsTooltip.hide();
}
