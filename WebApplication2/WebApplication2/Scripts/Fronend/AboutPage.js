// HERO SLIDER
var menu = [];
jQuery(".swiper-slide").each(function (index) {
    menu.push(jQuery(this).find(".slide-inner").attr("data-text"));
});
var interleaveOffset = 0.78;
var swiperOptions = {
    loop: true,
    speed: 1250,
    parallax: true,
    autoplay: {
        delay: 7000,
        disableOnInteraction: false,
    },

    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },

    on: {
        progress: function () {
            var swiper = this;
            for (var i = 0; i < swiper.slides.length; i++) {
                var slideProgress = swiper.slides[i].progress;
                var innerOffset = swiper.width * interleaveOffset;
                var innerTranslate = slideProgress * innerOffset;
                swiper.slides[i].querySelector(".slide-inner").style.transform =
                    "translate3d(" + innerTranslate + "px, 0, 0)";
            }
        },

        touchStart: function () {
            var swiper = this;
            for (var i = 0; i < swiper.slides.length; i++) {
                swiper.slides[i].style.transition = "";
            }
        },

        setTransition: function (speed) {
            var swiper = this;
            for (var i = 0; i < swiper.slides.length; i++) {
                swiper.slides[i].style.transition = speed + "ms";
                swiper.slides[i].querySelector(".slide-inner").style.transition =
                    speed + "ms";
            }
        },
    },
};

var swiper = new Swiper(".swiper-container", swiperOptions);

// DATA BACKGROUND IMAGE
var sliderBgSetting = $(".slide-bg-image");
sliderBgSetting.each(function (indx) {
    if ($(this).attr("data-background")) {
        $(this).css("background-image", "url(" + $(this).data("background") + ")");
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
