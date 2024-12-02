function updateDropdownButtonText(dropdownButtonId, text) {
  const button = document.getElementById(dropdownButtonId);
  if (button) button.textContent = text;
}

function createDropdownItem(text, value, onClickHandler) {
  const item = document.createElement("li");
  const anchor = document.createElement("a");
  anchor.className = "dropdown-item";
  anchor.href = "#";
  anchor.textContent = text;
  anchor.dataset.value = value;
  anchor.addEventListener("click", onClickHandler);
  item.appendChild(anchor);
  return item;
}

function handleFetchError(error) {
  console.error("Lỗi khi gọi API:", error);
}

function renderDropdownMenu(data, containerId, onClickHandler) {
  const dropdownMenu = document.getElementById(containerId);
  dropdownMenu.innerHTML = ""; 
  data.forEach(item => {
    const listItem = createDropdownItem(item.name, item.id, onClickHandler);
    dropdownMenu.appendChild(listItem);
  });
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

      container.innerHTML = data && data.length > 0
        ? data.map(test => createTestCard(test)).join('')
        : "<p>Không có đề thi nào.</p>";
    })
    .catch(handleFetchError);
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
        <span class="test-card-subtitle-part">${test.examSeries.name}</span>
      </div>
    </div>`;
}

function saveNewTest(apiUrl, modal, getAllExams) {
  const testName = document.getElementById('testName').value.trim();
  const testType = document.getElementById('testTypeDropdown').getAttribute('data-value');

  if (!testName || !testType) {
    alert('Vui lòng nhập đầy đủ thông tin.');
    return;
  }

  const requestBody = {
    title: testName,       
    examSeriesId: testType
  };


  fetch(apiUrl, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(requestBody),
  })
    .then(response => response.json())
    .then(data => {
      localStorage.setItem('newTestData', JSON.stringify(data));
      window.location.href = '/Admin/CreateTest';
      modal.hide();
      getAllExams();
    })
    .catch(handleFetchError);
}

function addTestType(newType, dropdownMenu) {
  const newItem = createDropdownItem(newType, newType, () => updateDropdownButtonText('testTypeDropdown', newType));
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
      const selectedTestType = item.textContent.trim();
      updateDropdownButtonText('testTypeDropdown', selectedTestType);
      document.getElementById('testTypeDropdown').setAttribute('data-value', item.dataset.value);
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

document.getElementById("testTypeDropdown").addEventListener("click", function () {
  fetch("http://localhost:5112/api/v1/examSeries/getAllExamSeries", {
    method: "GET",
    headers: {
      "Accept": "text/plain"
    }
  })
    .then(response => response.json())
    .then(data => renderDropdownMenu(data, "testTypeDropdownMenu", function (event) {
      updateDropdownButtonText('testTypeDropdown', event.target.textContent);
    }))
    .catch(handleFetchError);
});

document.getElementById("testTypeDropdown").addEventListener("click", function () {
  fetch("http://localhost:5112/api/v1/examSeries/getAllExamSeries", {
    method: "GET",
    headers: {
      "Accept": "application/json"
    }
  })
    .then(response => response.json())
    .then(data => renderDropdownMenu(data, "testTypeDropdownMenu", function (event) {
      updateDropdownButtonText('testTypeDropdown', event.target.textContent);
      document.getElementById('testTypeDropdown').setAttribute('data-value', event.target.dataset.value);
    }))
    .catch(handleFetchError);
});
