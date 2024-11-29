document.querySelectorAll('.dropdown-item').forEach(item => {
  item.addEventListener('click', function (event) {
    const selectedText = event.target.textContent;

    const button = document.getElementById('filterDropdown');
    button.textContent = selectedText; 
  });
});

document.addEventListener('DOMContentLoaded', function() {
  fetch('http://localhost:5112/api/v1/exam/getAllExams')
    .then(response => response.json())
    .then(data => {
      const container = document.getElementById('test-list-container');

      if (!container) {
        console.error('Không tìm thấy phần tử test-list-container!');
        return;
      }

      if (Array.isArray(data) && data.length > 0) {
        data.forEach(test => {
          const card = `
                        <div class="test-card">
                            <div class="test-card-box" aria-labelledby="test-card-title-${test.id}">
                                <div class="test-card-box-icon">📜</div>
                                <div class="test-action-button">
                                    <div type="button" id="filterDropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <i class="fa fa-ellipsis-v"></i>
                                    </div>
                                    <ul class="dropdown-menu" aria-labelledby="filterDropdown">
                                        <li><a class="dropdown-item" href="#" onclick="deleteTest('${test.id}')"><i class="far fa-trash-alt me-2"></i>Xoá</a></li>
                                        <li><a class="dropdown-item" href="#" onclick="editTest('${test.id}')"><i class="far fa-edit me-2"></i>Chỉnh sửa tiêu đề</a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="test-card-title-container">
                                <span class="test-card-title" id="test-card-title-${test.id}">${test.title}</span>
                            </div>

                            <div class="test-card-subtitle">
                                <span class="test-card-subtitle-part" title="${new Date(test.createdAt).toLocaleString()}">${new Date(test.createdAt).toLocaleDateString()}</span>
                                <span class="test-card-subtitle-middle-dot">·</span>
                                <span class="test-card-subtitle-part">ETS 2024</span>
                            </div>
                        </div>
                    `;
          container.innerHTML += card;
        });
      } else {
        container.innerHTML = "<p>Không có đề thi nào.</p>";
      }
    })
    .catch(error => {
      console.error('Lỗi khi gọi API:', error);
    });
});
