document.addEventListener("DOMContentLoaded", function () {
    document.title = "EduTrack API Docs 🚀";

    let swaggerContainer = document.querySelector(".swagger");
    if (swaggerContainer) {
        swaggerContainer.style.opacity = "0";
        setTimeout(() => {
            swaggerContainer.style.transition = "opacity 1s ease-in-out";
            swaggerContainer.style.opacity = "1";
        }, 500);
    }

   
});
