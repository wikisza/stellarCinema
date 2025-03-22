document.addEventListener("DOMContentLoaded", function () {
    const slidesContainer = document.querySelector(".slides-container");
    const slides = document.querySelectorAll(".slide");
    let currentIndex = 0;
    const intervalTime = 15000; 
    let slideInterval;

    function updateSlidePosition() {
        const offset = -currentIndex * 100; 
        slidesContainer.style.transform = `translateX(${offset}%)`;
    }

    function nextSlide() {
        currentIndex = (currentIndex + 1) % slides.length; 
        updateSlidePosition();
    }

    function prevSlide() {
        currentIndex = (currentIndex - 1 + slides.length) % slides.length; 
        updateSlidePosition();
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

////////////////// MOVIE SEARCH BAR //////////////////

$(document).ready(function () {
    $("#movieSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Movies/GetMovieSuggestions",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.title,
                            value: item.title,
                            id: item.idMovie
                        };
                    }));
                }
            });
        },
        select: function (event, ui) {
            $("#selectedMovieId").val(ui.item.id);
        }
    });
});
