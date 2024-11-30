function updateDropdownButtonText(dropdownButtonId, text) {
  const button = document.getElementById(dropdownButtonId);
  if (button) button.textContent = text;
}

function fetchAndRenderExams(apiUrl, containerId) {
  fetch(apiUrl)
    .then(response => response.json())
    .then(data => {
      const container = document.getElementById(containerId);
      if (!container) {
        console.error(`Không tìm thấy phần tử #${containerId}`);
        return;
      }

      container.innerHTML = '';

      if (Array.isArray(data) && data.length > 0) {
        data.forEach(test => {
          container.innerHTML += createTestCard(test);
        });
      } else {
        container.innerHTML = "<p>Không có đề thi nào.</p>";
      }
    })
    .catch(error => console.error('Lỗi khi gọi API:', error));
}

function createTestCard(test) {
  const createdDate = new Date(test.createdAt).toLocaleDateString();
  return `
    <div class="test-card">
      <div class="test-card-box" aria-labelledby="test-card-title-${test.id}">
        <div class="test-card-box-icon">📜</div>
        <div class="test-action-button">
          <div type="button" id="filterDropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fa fa-ellipsis-v"></i>
          </div>
          <ul class="dropdown-menu" aria-labelledby="filterDropdown">
            <li><a class="dropdown-item" href="#" onclick="deleteTest('${test.id}')">
              <i class="far fa-trash-alt me-2"></i>Xoá</a>
            </li>
            <li><a class="dropdown-item" href="#" onclick="editTest('${test.id}')">
              <i class="far fa-edit me-2"></i>Chỉnh sửa tiêu đề</a>
            </li>
          </ul>
        </div>
      </div>
      <div class="test-card-title-container">
        <span class="test-card-title" id="test-card-title-${test.id}">${test.title}</span>
      </div>
      <div class="test-card-subtitle">
        <span class="test-card-subtitle-part">${createdDate}</span>
        <span class="test-card-subtitle-middle-dot">·</span>
        <span class="test-card-subtitle-part">ETS 2024</span>
      </div>
    </div>`;
}

function saveNewTest(apiUrl, modal, getAllExams) {
  const testName = document.getElementById('testName').value.trim();
  const testType = document.getElementById('testType').value;

  if (!testName || !testType) {
    alert('Vui lòng nhập đầy đủ thông tin.');
    return;
  }

  fetch(apiUrl, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title: testName, type: testType }),
  })
    .then(response => response.json())
    .then(data => {
      if (data.success) {
        alert('Đề thi đã được tạo thành công!');
        modal.hide();
        getAllExams(); 
      } else {
        alert('Có lỗi xảy ra khi tạo đề thi.');
      }
    })
    .catch(error => {
      console.error('Lỗi khi tạo đề thi:', error);
      alert('Không thể tạo đề thi. Vui lòng thử lại sau.');
    });
}

function addTestType(newType, dropdownMenu) {
  const newItem = document.createElement('li');
  newItem.innerHTML = `<a class="dropdown-item" href="#" data-value="${newType}">${newType}</a>`;
  dropdownMenu.appendChild(newItem);
}

document.addEventListener('DOMContentLoaded', function () {
  const apiUrlGetExams = 'http://localhost:5112/api/v1/exam/getAllExams';
  const apiUrlCreateExam = 'http://localhost:5112/api/v1/exam/createExam';
  const testListContainerId = 'test-list-container';

  const createNewTestButton = document.getElementById('create-new-test');
  const saveTestButton = document.getElementById('saveTestBtn');
  const createTestModal = new bootstrap.Modal(document.getElementById('createTestModal'));

  const testTypeDropdownMenu = document.getElementById('testTypeDropdownMenu');
  const addTestTypeButton = document.getElementById('addTestTypeBtn');
  const saveTestTypeButton = document.getElementById('saveTestTypeBtn');
  const addTestTypeModal = new bootstrap.Modal(document.getElementById('addTestTypeModal'));
  const newTestTypeInput = document.getElementById('newTestType');
  const testTypeDropdownButton = document.getElementById('testTypeDropdown');

  fetchAndRenderExams(apiUrlGetExams, testListContainerId);

  createNewTestButton.addEventListener('click', () => {
    createTestModal.show();
  });

  saveTestButton.addEventListener('click', () =>
    saveNewTest(apiUrlCreateExam, createTestModal, () =>
      fetchAndRenderExams(apiUrlGetExams, testListContainerId)
    )
  );

  testTypeDropdownMenu.addEventListener('click', function (event) {
    const item = event.target.closest('.dropdown-item');
    if (item) {
      const selectedTestType = item.getAttribute('data-value');
      updateDropdownButtonText('testTypeDropdown', selectedTestType);
    }
  });

  addTestTypeButton.addEventListener('click', () => {
    createTestModal.hide();
    addTestTypeModal.show();
  });

  saveTestTypeButton.addEventListener('click', function () {
    const newTestType = newTestTypeInput.value.trim();
    if (!newTestType) {
      alert('Vui lòng nhập tên loại đề thi.');
      return;
    }
    addTestType(newTestType, testTypeDropdownMenu);
    newTestTypeInput.value = '';
    addTestTypeModal.hide();
  });

  addTestTypeModal._element.addEventListener('hidden.bs.modal', function () {
    createTestModal.show();
  });
});
