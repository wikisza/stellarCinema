document.addEventListener("DOMContentLoaded", async function () {
    const seatCheckboxes = document.querySelectorAll(".seat-checkbox");
    const selectedSeatsSpan = document.getElementById("selectedSeats");
    const totalPriceSpan = document.getElementById("totalPrice");
    const hiddenPriceInput = document.getElementById("hiddenPrice");
    const showtimeId = document.getElementById("showtimeId").value;
    let seatsList = document.getElementById("seatsList");
    let seatPrice = 0;

    async function fetchSeatPrice() {
        try {
            let response = await fetch(`/Reservations/GetSeatPrice`);
            seatPrice = await response.json();
        } catch (error) {
            console.error("Błąd pobierania ceny:", error);
            seatPrice = 0;
        }
    }



    function updateSummary() {
        let selectedSeats = [...seatCheckboxes].filter(checkbox => checkbox.checked).map(checkbox => {
            let seatLabel = checkbox.closest(".seatLabel").querySelector(".seat").textContent;
            return seatLabel;
        });

        let selectedIds = [...seatCheckboxes].filter(checkbox => checkbox.checked).map(checkbox => checkbox.value);


        selectedSeatsSpan.textContent = selectedSeats.length > 0 ? selectedSeats.join(", ") : "Brak";
        const total = selectedSeats.length * seatPrice;
        totalPriceSpan.textContent = (total).toFixed(2); + " zł";

        hiddenPriceInput.value = total;

        seatsList.value = selectedIds.join(",");
    }

    async function fetchTakenSeats(showtimeId) {
        try {
            const response = await fetch(`/Reservations/GetTakenSeats?IdShowtime=${showtimeId}`);
            const takenSeats = await response.json();

            takenSeats.forEach(takenSeatId => {
                const checkbox = document.querySelector(`.seat-checkbox[value="${takenSeatId}"]`);
                if (checkbox) {
                    checkbox.disabled = true;
                    const seatElement = checkbox.closest(".seatLabel").querySelector(".seat");
                    seatElement.classList.add("taken");
                }
            });
        } catch (error) {
            console.error("Błąd pobierania zajętych miejsc:", error);
        }
    }

    await fetchSeatPrice();
    await fetchTakenSeats(showtimeId);
    await fetchSeatPrice();

    seatCheckboxes.forEach(checkbox => {
        checkbox.addEventListener("change", updateSummary);
    });

    document.querySelectorAll(".seat-label").forEach(label => {
        label.addEventListener("click", function () {
            let checkbox = this.querySelector(".seat-checkbox");
            checkbox.checked = !checkbox.checked;
            this.querySelector(".seat").classList.toggle("selected", checkbox.checked);
            updateSummary();
        });
    });


});
