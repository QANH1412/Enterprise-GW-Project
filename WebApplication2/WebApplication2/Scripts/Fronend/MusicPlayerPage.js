//================= Display Images and Music in Card =============//
new Vue({
    el: "#app",
    data() {
        return {
            audio: null,
            circleLeft: null,
            barWidth: null,
            duration: null,
            currentTime: null,
            isTimerPlaying: false,
            tracks: [
                {
                    name: "Stuck On You(LOUVRE REMIX)",
                    artist: "Nowlu",
                    cover: "/Content/Frontend/Images/Nowlu.png",
                    source: "/Content/Frontend/MP3/Nowlu(remix).mp3",
                    url: "https://www.youtube.com/watch?v=ARyOrzLry_Q",
                    favorited: true,
                },
                {
                    name: "Stuck On You『夫婦以上、恋人未満。",
                    artist: "Nowlu",
                    cover: "/Content/Frontend/Images/StuckOnYou.jpg",
                    source: "/Content/Frontend/MP3/StuckOnYou.mp3",
                    url: "https://www.youtube.com/watch?v=w_IQ7r5xgpc",
                    favorited: false,
                },
                {
                    name: "Sleepless Nights",
                    artist: "Aimer - 星屑ビーナス",
                    cover: "/Content/Frontend/Images/Sleepless.jpeg",
                    source: "/Content/Frontend/MP3/Sleepless Night - Aimer.mp3",
                    url: "https://www.youtube.com/watch?v=xotYGvOt35Y&list=RDGMEMXdNDEg4wQ96My0DhjI-cIg&index=4",
                    favorited: false,
                },
                {
                    name: "失恋ソング沢山聴いて 泣いてばかりの私はもう。",
                    artist: "Riria",
                    cover: "/Content/Frontend/Images/SummerTime.jpg",
                    source: "/Content/Frontend/MP3/SummerTimeRendering.mp3",
                    url: "https://www.youtube.com/watch?v=xSxjhvy8vrU&list=RDGMEMXdNDEg4wQ96My0DhjI-cIg&index=5",
                    favorited: false,
                },
                {
                    name: "I'm Still Alive Today(Acoustic Ver.)",
                    artist: "EIKO Starring 96猫",
                    cover: "/Content/Frontend/Images/Eiko.jpg",
                    source: "/Content/Frontend/MP3/I'm Still Alive Today(Acoustic Ver).mp3",
                    url: "https://www.youtube.com/watch?v=8YqIg09xYng",
                    favorited: false,
                },
                {
                    name: "Orion-Acoustic Arrange ~〔歌ってみた〕",
                    artist: "Kenshi Yonezu",
                    cover: "/Content/Frontend/Images/SGD.jpg",
                    source: "/Content/Frontend/MP3/Orion-Acoustic Arrange.mp3",
                    url: "https://www.youtube.com/watch?v=knUMag0Iu0g&list=RDGMEMXdNDEg4wQ96My0DhjI-cIg&index=7",
                    favorited: false,
                },
                {
                    name: "[Avid] covered by Mizuki",
                    artist: "SawanoHiroyuki[nZk]",
                    cover: "/Content/Frontend/Images/86.jpg",
                    source: "/Content/Frontend/MP3/86.mp3",
                    url: "https://www.youtube.com/watch?v=YGOX7Tj0dMY",
                    favorited: false,
                },
                {
                    name: "心做し (Kokoronashi)",
                    artist: "Kohana Lam】",
                    cover: "/Content/Frontend/Images/Kokoronashi.jpg",
                    source: "/Content/Frontend/MP3/Kokoronashi.mp3",
                    url: "https://www.youtube.com/watch?v=Yp1-6C6irLU&list=RDGMEMXdNDEg4wQ96My0DhjI-cIg&index=17",
                    favorited: false,
                },
            ],
            currentTrack: null,
            currentTrackIndex: 0,
            transitionName: null,
        };
    },
    methods: {
        play() {
            if (this.audio.paused) {
                this.audio.play();
                this.isTimerPlaying = true;
            } else {
                this.audio.pause();
                this.isTimerPlaying = false;
            }
        },
        generateTime() {
            let width = (100 / this.audio.duration) * this.audio.currentTime;
            this.barWidth = width + "%";
            this.circleLeft = width + "%";
            let durmin = Math.floor(this.audio.duration / 60);
            let dursec = Math.floor(this.audio.duration - durmin * 60);
            let curmin = Math.floor(this.audio.currentTime / 60);
            let cursec = Math.floor(this.audio.currentTime - curmin * 60);
            if (durmin < 10) {
                durmin = "0" + durmin;
            }
            if (dursec < 10) {
                dursec = "0" + dursec;
            }
            if (curmin < 10) {
                curmin = "0" + curmin;
            }
            if (cursec < 10) {
                cursec = "0" + cursec;
            }
            this.duration = durmin + ":" + dursec;
            this.currentTime = curmin + ":" + cursec;
        },
        updateBar(x) {
            let progress = this.$refs.progress;
            let maxduration = this.audio.duration;
            let position = x - progress.offsetLeft;
            let percentage = (100 * position) / progress.offsetWidth;
            if (percentage > 100) {
                percentage = 100;
            }
            if (percentage < 0) {
                percentage = 0;
            }
            this.barWidth = percentage + "%";
            this.circleLeft = percentage + "%";
            this.audio.currentTime = (maxduration * percentage) / 100;
            this.audio.play();
        },
        clickProgress(e) {
            this.isTimerPlaying = true;
            this.audio.pause();
            this.updateBar(e.pageX);
        },
        prevTrack() {
            this.transitionName = "previous-track";
            this.isShowCover = false;
            if (this.currentTrackIndex > 0) {
                this.currentTrackIndex--;
            } else {
                this.currentTrackIndex = this.tracks.length - 1;
            }
            this.currentTrack = this.tracks[this.currentTrackIndex];
            this.resetPlayer();
        },
        nextTrack() {
            this.transitionName = "next-track";
            this.isShowCover = false;
            if (this.currentTrackIndex < this.tracks.length - 1) {
                this.currentTrackIndex++;
            } else {
                this.currentTrackIndex = 0;
            }
            this.currentTrack = this.tracks[this.currentTrackIndex];
            this.resetPlayer();
        },
        resetPlayer() {
            this.barWidth = 0;
            this.circleLeft = 0;
            this.audio.currentTime = 0;
            this.audio.src = this.currentTrack.source;
            setTimeout(() => {
                if (this.isTimerPlaying) {
                    this.audio.play();
                } else {
                    this.audio.pause();
                }
            }, 1000);
        },
        favorite() {
            this.tracks[this.currentTrackIndex].favorited =
                !this.tracks[this.currentTrackIndex].favorited;
        },
    },
    created() {
        let vm = this;
        this.currentTrack = this.tracks[0];
        this.audio = new Audio();
        this.audio.src = this.currentTrack.source;
        this.audio.ontimeupdate = function () {
            vm.generateTime();
        };
        this.audio.onloadedmetadata = function () {
            vm.generateTime();
        };
        this.audio.onended = function () {
            vm.nextTrack();
            this.isTimerPlaying = true;
        };

        // this is optional (for preload covers)
        for (let index = 0; index < this.tracks.length; index++) {
            const element = this.tracks[index];
            let link = document.createElement("link");
            link.rel = "prefetch";
            link.href = element.cover;
            link.as = "image";
            document.head.appendChild(link);
        }
    },
});

//=================Rounded Navbar==============//

const nav = document.querySelector("nav");
const toggleBtn = document.querySelector(".toggle-btn");

toggleBtn.addEventListener("click", () => {
    nav.classList.toggle("open");
});

//================= cursor animation zoom in =============//

var cursor = document.querySelector(".cursor"),
    cursorScale = document.querySelectorAll(".cursor-scale"),
    mouseX = 0,
    mouseY = 0;

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

window.addEventListener("mousemove", function (e) {
    mouseX = e.clientX;
    mouseY = e.clientY;
});

cursorScale.forEach(link => {
    link.addEventListener("mouseleave", () => {
        cursor.classList.remove("grow");
        cursor.classList.remove("grow-small");
    });
    link.addEventListener("mousemove", () => {
        cursor.classList.add("grow");
        if (link.classList.contains("small")) {
            cursor.classList.remove("grow");
            cursor.classList.add("grow-small");
        }
    });
});

//========== Loader  ============//
window.addEventListener("load", () => {
    const loader_bg = document.querySelector(".loader_bg");
    loader_bg.classList.add("loader_bg-finish");
});
