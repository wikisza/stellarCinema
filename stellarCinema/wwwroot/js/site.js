document.addEventListener("DOMContentLoaded", function () {
    const slides = document.querySelectorAll(".slide");
    let currentIndex = 0;
    const intervalTime = 15000; // 15 sekund
    let slideInterval;

    function showSlide(index) {
        slides.forEach((slide, i) => {
            slide.style.display = i === index ? "flex" : "none";
        });
    }

    function nextSlide() {
        currentIndex = (currentIndex === slides.length - 1) ? 0 : currentIndex + 1;
        showSlide(currentIndex);
    }

    function prevSlide() {
        currentIndex = (currentIndex === 0) ? slides.length - 1 : currentIndex - 1;
        showSlide(currentIndex);
    }

    document.querySelector(".prev").addEventListener("click", function () {
        prevSlide();
        resetInterval();
    });

    document.querySelector(".next").addEventListener("click", function () {
        nextSlide();
        resetInterval();
    });

    function startAutoSlide() {
        slideInterval = setInterval(nextSlide, intervalTime);
    }

    function resetInterval() {
        clearInterval(slideInterval);
        startAutoSlide();
    }

    showSlide(currentIndex);
    startAutoSlide();


document.querySelector('[data-tab="now"]').addEventListener("click", function () {
    fetch('/Movies/GetNowPlaying')
        .then(response => response.json())
        .then(movies => displayMovies(movies, "now"));
});

document.querySelector('[data-tab="soon"]').addEventListener("click", function () {
    fetch('/Movies/GetComingSoon')
        .then(response => response.json())
        .then(movies => displayMovies(movies, "soon"));
});



});