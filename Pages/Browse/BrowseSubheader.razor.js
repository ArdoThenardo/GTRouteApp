var tooltipGallery;
var tooltipGrid;

export function initializeTooltips(galleryElement, gridElement) {
    tooltipGallery = new bootstrap.Tooltip(galleryElement);
    tooltipGrid = new bootstrap.Tooltip(gridElement);
}

export function hideTooltips() {
    tooltipGallery.hide();
    tooltipGrid.hide();
}