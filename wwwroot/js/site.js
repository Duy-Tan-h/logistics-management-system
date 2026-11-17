// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Header
function setHeaderOffset() {
    var header = document.querySelector('.site-header');
    if (header) {
        document.body.style.paddingTop = header.offsetHeight + 'px';
    }
}
window.addEventListener('load', setHeaderOffset);
window.addEventListener('resize', setHeaderOffset);

// ===== Hiệu ứng "nổi bật" khi hover dịch vụ =====
document.addEventListener('DOMContentLoaded', function () {
    /* Đợi toàn bộ HTML của trang tải xong rồi mới chạy đoạn code bên trong.
       Bắt buộc phải có — vì nếu thẻ <script> này được trình duyệt đọc tới
       TRƯỚC khi các thẻ .service-card tồn tại trong trang, querySelectorAll
       bên dưới sẽ tìm ra kết quả RỖNG, code sẽ không hoạt động gì cả */

    var grid = document.querySelector('.service-grid');
    if (!grid) return;
    /* Tìm khung chứa 3 card dịch vụ. Nếu trang hiện tại KHÔNG có khối này
       (ví dụ đang ở trang /News, /Contact...), "grid" sẽ là null,
       dừng luôn tại đây — tránh lỗi "Cannot read property of null"
       ở các dòng bên dưới khi chạy trên những trang không liên quan */

    var cards = grid.querySelectorAll('.service-card');
    if (cards.length === 0) return;
    /* Tương tự — phòng trường hợp khung grid tồn tại nhưng rỗng
       (ví dụ Admin chưa tích "Nổi bật" cho dịch vụ nào) */

    cards.forEach(function (card) {
        card.addEventListener('mouseenter', function () {
            /* "mouseenter" = sự kiện khi con trỏ chuột DI CHUYỂN VÀO
               bên trong card này (chỉ bắn 1 lần khi vừa chạm vào,
               khác với "mouseover" sẽ bắn lặp lại nhiều lần khi
               chuột di chuyển lung tung bên trong card con cháu) */

            cards.forEach(function (c) { c.classList.remove('featured'); });
            /* Gỡ class "featured" khỏi TẤT CẢ các card trước —
               kể cả card đang được hover (sẽ thêm lại ngay dòng dưới).
               Làm vậy để đảm bảo LUÔN CHỈ CÓ ĐÚNG 1 card có class này
               tại bất kỳ thời điểm nào, không bao giờ bị 2 card cùng
               nổi bật một lúc */

            card.classList.add('featured');
            /* Thêm "featured" cho ĐÚNG card vừa được hover vào */
        });
    });

    grid.addEventListener('mouseleave', function () {
        /* Gắn sự kiện này vào CẢ KHUNG GRID (không phải từng card riêng) —
           chỉ kích hoạt khi chuột rời khỏi TOÀN BỘ khu vực chứa 3 card,
           không bị kích hoạt nhầm khi chuột di chuyển TỪ card này
           SANG card kia (vẫn còn nằm trong grid) */

        cards.forEach(function (c) { c.classList.remove('featured'); });
        cards[0].classList.add('featured');
        /* Khi chuột rời hẳn khỏi khu vực 3 card: gỡ "featured" khỏi
           tất cả, rồi gắn lại cho card ĐẦU TIÊN (cards[0]) — đúng về
           trạng thái mặc định ban đầu */
    });
});
/************DuAn***************/
(function () {
    var PAUSE_MS = 1500; // đứng yên bao lâu giữa 2 lần chuyển (1000 = 1s)
    var SLIDE_MS = 900;  // một lần chuyển kéo dài bao lâu (số càng lớn càng chậm)
    var reduceMotion = window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    document.querySelectorAll('.project-carousel').forEach(initCarousel);

    function initCarousel(root) {
        var track = root.querySelector('.project-slider');
        var prevBtn = root.querySelector('.project-nav-prev');
        var nextBtn = root.querySelector('.project-nav-next');
        var cards = track ? track.querySelectorAll('.project-card') : [];
        if (!track || cards.length === 0) return;

        var timer = null, touchTimer = null, raf = 0;
        var hovering = false;
        var down = false, moved = false, startX = 0, startLeft = 0;

        function stepSize() {
            var cs = getComputedStyle(track);
            var gap = parseFloat(cs.columnGap || cs.gap) || 0;
            return cards[0].getBoundingClientRect().width + gap;
        }
        function maxScroll() { return Math.max(0, track.scrollWidth - track.clientWidth); }
        function hasOverflow() { return maxScroll() > 2; }

        var animId = 0;
        function cancelAnim() {
            cancelAnimationFrame(animId);
            track.classList.remove('is-animating');
        }
        function goTo(left) {
            var target = Math.min(Math.max(left, 0), maxScroll());
            var from = track.scrollLeft, dist = target - from;
            cancelAnim();
            if (reduceMotion || Math.abs(dist) < 1) { track.scrollLeft = target; return; }
            var t0 = null;
            track.classList.add('is-animating'); // tắt snap trong lúc chạy để không bị giật
            function step(ts) {
                if (t0 === null) t0 = ts;
                var p = Math.min((ts - t0) / SLIDE_MS, 1);
                var e = p < 0.5 ? 4 * p * p * p : 1 - Math.pow(-2 * p + 2, 3) / 2; // easeInOutCubic
                track.scrollLeft = from + dist * e;
                if (p < 1) animId = requestAnimationFrame(step);
                else track.classList.remove('is-animating');
            }
            animId = requestAnimationFrame(step);
        }
        function slide(dir) {
            var s = stepSize();
            goTo((Math.round(track.scrollLeft / s) + dir) * s);
        }
        function snapNearest() {
            var s = stepSize();
            goTo(Math.round(track.scrollLeft / s) * s);
        }

        function updateUi() {
            raf = 0;
            var overflow = hasOverflow();
            root.classList.toggle('no-overflow', !overflow);
            if (prevBtn) prevBtn.disabled = track.scrollLeft <= 2;
            if (nextBtn) nextBtn.disabled = track.scrollLeft >= maxScroll() - 2;
            if (!overflow) stop();
        }
        function queueUpdate() { if (!raf) raf = requestAnimationFrame(updateUi); }

        function next() {
            if (track.scrollLeft >= maxScroll() - 2) goTo(0);
            else slide(1);
        }
        function start() {
            if (reduceMotion || timer || hovering || down || document.hidden || !hasOverflow()) return;
            timer = setInterval(next, PAUSE_MS + SLIDE_MS);
        }
        function stop() { clearInterval(timer); timer = null; }

        if (prevBtn) prevBtn.addEventListener('click', function () { slide(-1); });
        if (nextBtn) nextBtn.addEventListener('click', function () { slide(1); });

        // Kéo bằng chuột (cảm ứng đã vuốt được sẵn)
        track.addEventListener('pointerdown', function (e) {
            if (e.pointerType !== 'mouse' || e.button !== 0) return;
            cancelAnim();   
            down = true; moved = false;
            startX = e.clientX; startLeft = track.scrollLeft;
            window.addEventListener('pointermove', onMove);
            window.addEventListener('pointerup', onUp);
            window.addEventListener('pointercancel', onUp);
        });
        function onMove(e) {
            var dx = e.clientX - startX;
            if (!moved && Math.abs(dx) > 5) {
                moved = true;
                track.classList.add('is-dragging');
                stop();
            }
            if (moved) track.scrollLeft = startLeft - dx;
        }
        function onUp() {
            window.removeEventListener('pointermove', onMove);
            window.removeEventListener('pointerup', onUp);
            window.removeEventListener('pointercancel', onUp);
            down = false;
            if (moved) {
                track.classList.remove('is-dragging');
                snapNearest();
            }
            start();
        }
        // Vừa kéo xong thì nuốt cú click, tránh vô tình mở dự án
        track.addEventListener('click', function (e) {
            if (moved) { e.preventDefault(); e.stopPropagation(); moved = false; }
        }, true);
        track.addEventListener('dragstart', function (e) { e.preventDefault(); });

        // Tạm dừng tự lướt khi người dùng tương tác
        root.addEventListener('pointerenter', function (e) {
            if (e.pointerType === 'mouse') { hovering = true; stop(); }
        });
        root.addEventListener('pointerleave', function (e) {
            if (e.pointerType === 'mouse') { hovering = false; start(); }
        });
        root.addEventListener('focusin', stop);
        root.addEventListener('focusout', start);
        track.addEventListener('touchstart', function () { clearTimeout(touchTimer); stop(); }, { passive: true });
        cancelAnim();   
        track.addEventListener('touchend', function () { touchTimer = setTimeout(start, 3000); });
        document.addEventListener('visibilitychange', function () { document.hidden ? stop() : start(); });

        track.addEventListener('scroll', queueUpdate, { passive: true });
        window.addEventListener('resize', function () { queueUpdate(); start(); });

        updateUi();
        start();
    }
})();


/************QuyTrinh***************/
(function () {
    var steppers = document.querySelectorAll('.process-stepper');
    if (!steppers.length) return;

    var reduceMotion = window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    if (reduceMotion || !('IntersectionObserver' in window)) return; // không animate: giữ nguyên trạng thái sáng sẵn

    var io = new IntersectionObserver(function (entries) {
        entries.forEach(function (en) {
            if (en.isIntersecting) {
                en.target.classList.add('is-visible'); // bắt đầu chạy đường nối
                io.unobserve(en.target);
            }
        });
    }, { threshold: 0.2, rootMargin: '0px 0px -10% 0px' });

    steppers.forEach(function (el) {
        el.classList.add('is-animated'); // đưa về trạng thái chưa sáng, chờ cuộn tới
        io.observe(el);
    });
})();

