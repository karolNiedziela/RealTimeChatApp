$(function () {
    document.addEventListener("click", (event) => {
        const multiplicatorActive = document.querySelector(".multiplicator.active");

        // check if someone clicked beyond multiplicator
        if (
            multiplicatorActive !== null &&
            !event.target.classList.contains("multiplicator-col") &&
            !event.target.classList.contains("bx-dots-vertical-rounded")
        ) {
            multiplicatorActive.classList.remove("active");
        }
    });

    const verticalDots = document.querySelectorAll(".bx-dots-vertical-rounded");
    verticalDots.forEach((verticalDot) => {
        const verticalDotCoords = verticalDot.getBoundingClientRect();
        console.log(verticalDotCoords);
        const multiplicator = verticalDot
            .closest(".row.content")
            .querySelector(".multiplicator");

        multiplicator.style.right =
            document.body.clientWidth - verticalDotCoords.left + "px";
        verticalDot.addEventListener("click", (event) => {
            const multiplicator = verticalDot
                .closest(".row.content")
                .querySelector(".multiplicator");
            if (multiplicator.classList.contains("active")) {
                multiplicator.classList.remove("active");
            } else {
                closePreviousOpenedMultiplicator();
                multiplicator.classList.add("active");
            }
        });
    });

    function closePreviousOpenedMultiplicator() {
        const multiplicatorActive = document.querySelector(".multiplicator.active");

        if (multiplicatorActive !== null) {
            multiplicatorActive.classList.remove("active");
        }
    }

});

