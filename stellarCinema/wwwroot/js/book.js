document.addEventListener("DOMContentLoaded", async function () {
    const seatCheckboxes = document.querySelectorAll(".seat-checkbox");
    const selectedSeatsSpan = document.getElementById("selectedSeats");
    const totalPriceSpan = document.getElementById("totalPrice");
    const hiddenPriceInput = document.getElementById("hiddenPrice");
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
        let selectedSeats = [...seatCheckboxes].filter(checkbox => checkbox.checked).map(checkbox => checkbox.value);

        selectedSeatsSpan.textContent = selectedSeats.length > 0 ? selectedSeats.join(", ") : "Brak";
        const total = selectedSeats.length * seatPrice;
        totalPriceSpan.textContent = (total).toFixed(2); + " zł";

        hiddenPriceInput.value = total;
    }

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
