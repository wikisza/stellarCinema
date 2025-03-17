
    let currentSlide = 0;
    const slides = [
    {photo: 'Film 1', desc: 'Opis filmu 1' },
    {photo: 'Film 2', desc: 'Opis filmu 2' }
    ];

    function updateSlide() {
        document.querySelector('.movie-photo').textContent = slides[currentSlide].photo;
    document.querySelector('.movie-description p').textContent = slides[currentSlide].desc;
        }
        
        document.querySelector('.next').addEventListener('click', () => {
        currentSlide = (currentSlide + 1) % slides.length;
    updateSlide();
        });
        document.querySelector('.prev').addEventListener('click', () => {
        currentSlide = (currentSlide - 1 + slides.length) % slides.length;
    updateSlide();
        });
        setInterval(() => {
        currentSlide = (currentSlide + 1) % slides.length;
    updateSlide();
        }, 5000);
        
        document.querySelectorAll('.tabs button').forEach(button => {
        button.addEventListener('click', () => {
            document.querySelectorAll('.tabs button').forEach(b => b.classList.remove('active'));
            button.classList.add('active');
            document.querySelectorAll('.movies').forEach(m => m.classList.add('hidden'));
            document.getElementById(button.dataset.tab).classList.remove('hidden');
        });
        });
