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

////////////////// HALL AVAILABLE CHECK //////////////////

//$(document).ready(function () {
//    $("#hallsFiller, #ShowtimeDate").change(function () {
//        var hallId = $("#hallsFiller").val();
//        var showtimeDate = $("#ShowtimeDate").val();

//        if (hallId && showtimeDate) {
//            $.ajax({
//                url: "/Showtime/CheckHallAvailability",
//                type: "GET",
//                data: { hallId: hallId, showtimeDate: showtimeDate },
//                success: function (response) {
//                    if (!response.available) {
//                        alert("Ta sala jest już zajęta w wybranym terminie!");
//                        $("#ShowtimeDate").val(""); 
//                    }
//                }
//            });
//        }
//    });
//});

