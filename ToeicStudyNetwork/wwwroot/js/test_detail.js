document.addEventListener("DOMContentLoaded", function () {
  const practiceTab = document.getElementById("practice-tab");
  const takeTestTab = document.getElementById("take-test-tab");
  const discussionTab = document.getElementById("discussion-tab");
  const contentDiv = document.getElementById("content-div");

  function loadPracticeTabContent() {
    contentDiv.innerHTML = `
            <!-- Pro Tips Section -->
            <div class="alert alert-pro-tips my-3">
                <i class="bi bi-lightbulb"></i>
                <strong>Pro tips:</strong> Hình thức luyện tập từng phần và chọn mức thời gian phù hợp sẽ giúp bạn tập
                trung
                vào giải từng các câu hỏi thay vì phải chịu áp lực hoàn thành bài thi.
            </div>

            <!-- Practice Options -->
            <div class="mb-3">
                <h6>Chọn phần thi bạn muốn làm:</h6>

                <!-- Part 1 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part1">
                    <label class="form-check-label" for="part1">Part 1 (6 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 1] Tranh tả người</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary">#[Part 1] Tranh tả vật</a>
                </div>

                <!-- Part 2 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part2">
                    <label class="form-check-label" for="part2">Part 2 (25 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi WHAT</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi WHO</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi WHERE</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi WHEN</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi HOW</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi WHY</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi YES/NO</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi đuôi</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu hỏi lựa chọn</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu yêu cầu, đề nghị</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 2] Câu trần thuật</a>
                </div>

                <!-- Part 3 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part3">
                    <label class="form-check-label" for="part3">Part 3 (39 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về chủ đề, mục đích</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về danh tính người
                        nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về chi tiết cuộc hội
                        thoại</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hành động tương
                        lai</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi kết hợp bảng biểu</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hàm ý câu nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Company - General Office
                        Work</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Company - Personnel</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Company - Event,
                        Project</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Company - Facility</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Shopping, Service</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Order, delivery</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Chủ đề: Transportation</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về yêu cầu, gợi ý</a>
                </div>

                <!-- Part 4 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part4">
                    <label class="form-check-label" for="part4">Part 4 (30 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chủ đề, mục đích</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về danh tính, địa
                        điểm</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chi tiết</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về hành động tương
                        lai</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi kết hợp bảng biểu</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hàm ý câu nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Telephone message -
                        Tin
                        nhắn thoại</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Announcement - Thông
                        báo</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: News report, Broadcast
                        -
                        Bản tin</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Talk - Bài phát biểu,
                        diễn
                        văn</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Excerpt from a meeting
                        -
                        Trích dẫn từ buổi họp</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi yêu cầu, gợi ý</a>
                </div>

                <!-- Part 5 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part5">
                    <label class="form-check-label" for="part5">Part 5 (30 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chủ đề, mục đích</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về danh tính, địa
                        điểm</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chi tiết</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về hành động tương
                        lai</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi kết hợp bảng biểu</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hàm ý câu nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Telephone message -
                        Tin
                        nhắn thoại</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Announcement - Thông
                        báo</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: News report, Broadcast
                        -
                        Bản tin</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Talk - Bài phát biểu,
                        diễn
                        văn</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Excerpt from a meeting
                        -
                        Trích dẫn từ buổi họp</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi yêu cầu, gợi ý</a>
                </div>

                <!-- Part 6 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part6">
                    <label class="form-check-label" for="part6">Part 6 (16 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chủ đề, mục đích</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về danh tính, địa
                        điểm</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chi tiết</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về hành động tương
                        lai</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi kết hợp bảng biểu</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hàm ý câu nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Telephone message -
                        Tin
                        nhắn thoại</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Announcement - Thông
                        báo</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: News report, Broadcast
                        -
                        Bản tin</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Talk - Bài phát biểu,
                        diễn
                        văn</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Excerpt from a meeting
                        -
                        Trích dẫn từ buổi họp</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi yêu cầu, gợi ý</a>
                </div>

                <!-- Part 7 -->
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="part7">
                    <label class="form-check-label" for="part7">Part 7 (54 câu hỏi)</label>
                </div>
                <div class="mb-2">
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chủ đề, mục đích</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về danh tính, địa
                        điểm</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về chi tiết</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi về hành động tương
                        lai</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi kết hợp bảng biểu</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 3] Câu hỏi về hàm ý câu nói</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Telephone message -
                        Tin
                        nhắn thoại</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Announcement - Thông
                        báo</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: News report, Broadcast
                        -
                        Bản tin</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Talk - Bài phát biểu,
                        diễn
                        văn</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Dạng bài: Excerpt from a meeting
                        -
                        Trích dẫn từ buổi họp</a>
                    <a href="#" class="btn btn-sm btn-outline-secondary me-1">#[Part 4] Câu hỏi yêu cầu, gợi ý</a>
                </div>
                <div class="form-floating my-3">
                    <select class="form-select" id="floatingSelect" aria-label="Floating label select example">
                        <option selected>Không giới hạn</option>
                        <option value="5">5 phút</option>
                        <option value="10">10 phút</option>
                        <option value="15">15 phút</option>
                        <option value="20">20 phút</option>
                        <option value="25">25 phút</option>
                        <option value="30">30 phút</option>
                        <option value="35">35 phút</option>
                        <option value="40">40 phút</option>
                        <option value="45">45 phút</option>
                        <option value="50">50 phút</option>
                        <option value="55">55 phút</option>
                        <option value="60">60 phút</option>
                    </select>
                    <label for="floatingSelect">Chọn thời gian</label>
                </div>
                <a class="btn mb-3 btn-primary">Luyện thi</a>
            </div>
        `;
  }

  function setActiveTab(tab) {
    document.querySelectorAll(".nav-link").forEach(function (navLink) {
      navLink.classList.remove("active");
    });
    tab.classList.add("active");
  }

  practiceTab.addEventListener("click", function (event) {
    event.preventDefault();
    loadPracticeTabContent();
    setActiveTab(practiceTab);
  });

  takeTestTab.addEventListener("click", function (event) {
    event.preventDefault();
    setActiveTab(takeTestTab);
    contentDiv.innerHTML = `
                <div class="alert alert-take-test my-3">
                <i class="bi bi-exclamation-circle"></i>
                Sẵn sàng để bắt đầu làm full test? Để đạt được kết quả tốt nhất, bạn cần dành ra 120 phút cho bài test này.
            </div>
            <a class="btn mb-3 btn-primary" asp-controller="Test" asp-route-id="@Model.Id" asp-action="StartExam">Bắt đầu thi</a>
            `;
  });

  discussionTab.addEventListener("click", function (event) {
    event.preventDefault();
    setActiveTab(discussionTab);
    contentDiv.innerHTML = `
                <div class="alert alert-warning my-3">
                    <i class="bi bi-chat-dots"></i>
                    <strong>Discussion:</strong> Tham gia thảo luận với các học viên khác để trao đổi kinh nghiệm và kiến thức.
                </div>
            `;
  });

  loadPracticeTabContent();
});
