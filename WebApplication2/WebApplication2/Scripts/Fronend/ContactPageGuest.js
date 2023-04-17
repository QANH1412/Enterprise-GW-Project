const form = document.querySelector("form");
const button = document.querySelector("#signup-button");

// Create a new element for the message
const message = document.createElement("div");
message.style.display = "none";
message.classList.add("message");
button.parentNode.insertBefore(message, button);

// Execute animation on form submit
form.addEventListener("submit", function (event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    // Check condition of all required fields using checkValidity() method
    if (form.checkValidity()) {
        // Add the .animate class to the button element
        button.classList.add("animate");

        // Disable the button to prevent further clicks
        button.disabled = true;

        // Show the success message and reset the form to its initial stage
        setTimeout(function () {
            message.textContent =
                "Thank you for using our services, we appreciate your feedback.";
            message.style.display = "block";
            alert(
                "Your inquiry is successfully created and being sent! You can go back by using rounded menu."
            );
            form.reset();
        }, 4000);
    } else {
        // Show an error message if required fields are not filled in
        alert("Please provide your information in required fields");
    }
});

//================= cursor animation zoom in =============//

// Select the cursor element and all elements with the "cursor-scale" class
var cursor = document.querySelector(".cursor"),
    cursorScale = document.querySelectorAll(".cursor-scale"),
    mouseX = 0,
    mouseY = 0;

// Move the cursor to the mouse position every 16ms using GreenSock Animation Platform (gsap)
gsap.to({}, 0.016, {
    repeat: -1,

    onRepeat: function () {
        gsap.set(cursor, {
            css: {
                left: mouseX,
                top: mouseY,
            },
        });
    },
});

// Update the mouse position variables every time the mouse moves
window.addEventListener("mousemove", function (e) {
    mouseX = e.clientX;
    mouseY = e.clientY;
});

// Add event listeners to all elements with the "cursor-scale" class
cursorScale.forEach(link => {
    // When the mouse leaves the element, remove the "grow" and "grow-small" classes from the cursor element
    link.addEventListener("mouseleave", () => {
        cursor.classList.remove("grow");
        cursor.classList.remove("grow-small");
    });
    // When the mouse moves over the element, add the "grow" or "grow-small" class to the cursor element depending on whether the element has the "small" class
    link.addEventListener("mousemove", () => {
        cursor.classList.add("grow");
        if (link.classList.contains("small")) {
            cursor.classList.remove("grow");
            cursor.classList.add("grow-small");
        }
    });
});

//========== Loader  ============//

// When the page finishes loading, add the "loader_bg-finish" class to the element with class "loader_bg"
window.addEventListener("load", () => {
    const loader_bg = document.querySelector(".loader_bg");
    loader_bg.classList.add("loader_bg-finish");
});

//=================Rounded Navbar==============//

const nav = document.querySelector("nav");
const toggleBtn = document.querySelector(".toggle-btn");

// When the toggle button is clicked, toggle the "open" class on the "nav" element
toggleBtn.addEventListener("click", () => {
    nav.classList.toggle("open");
});
