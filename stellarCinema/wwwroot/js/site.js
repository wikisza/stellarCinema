document.addEventListener("DOMContentLoaded", function () {
    const slides = document.querySelectorAll(".slide");
    let currentIndex = 0;
    const intervalTime = 15000; // 5 sekund
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

function displayMovies(movies, sectionId) {
    const section = document.getElementById(sectionId);
    section.innerHTML = movies.map(movie => `
        <div class="movie">
            <img src="${movie.posterUrl}" alt="${movie.title} Poster" />
            <h3>${movie.title}</h3>
            <p>${movie.description}</p>
            <p>${movie.duration} min | ${movie.genre}</p>
            <button onclick="buyTicket(${movie.id})">Zobacz godziny</button>
        </div>
    `).join('');
    section.classList.remove('hidden');
}


});