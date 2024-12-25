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

function renderDropdownMenu(data, containerId, onClickHandler) {
  const dropdownMenu = document.getElementById(containerId);
  dropdownMenu.innerHTML = ""; 
  data.forEach(item => {
    const listItem = createDropdownItem(item.name, item.id, onClickHandler);
    dropdownMenu.appendChild(listItem);
  });
}

function extractExamIdFromUrl(url) {
  const urlObj = new URL(url); 
  const examId = urlObj.pathname.split('/')[2]; 
  return examId;
}

async function saveNewTest() {
  const testName = document.getElementById('testName').value.trim();
  const testType = document.getElementById('testTypeDropdown').getAttribute('data-value');

  if (!testName || !testType) {
    alert('Vui lòng nhập đầy đủ thông tin.');
    return;
  }
  
  await fetch("Admin/createExam", {
    method: 'POST',
    body: JSON.stringify({
      title: testName,
      examSeriesId: testType
    }),
    headers: { 'Content-Type': 'application/json' }
  }).then(response => {
    if (response.redirected) {
      const examId = extractExamIdFromUrl(response.url);
      localStorage.setItem("examId", examId);
      window.location.href = response.url;
    } else {
      return response.json();
    }
  })
    .catch (error => {
      console.error("Error create exam: ", error)
  })
}

function addTestType(newType, dropdownMenu) {
  const newItem = createDropdownItem(newType, newType, () => updateDropdownButtonText('testTypeDropdown', newType));
  dropdownMenu.appendChild(newItem);
}

document.addEventListener('DOMContentLoaded', function () {
  const createNewTestButton = document.getElementById('create-new-test');
  const saveTestButton = document.getElementById('saveTestBtn');
  const createTestModal = new bootstrap.Modal(document.getElementById('createTestModal'));

  const testTypeDropdownMenu = document.getElementById('testTypeDropdownMenu');
  const addTestTypeButton = document.getElementById('addTestTypeBtn');
  const saveTestTypeButton = document.getElementById('saveTestTypeBtn');
  const addTestTypeModal = new bootstrap.Modal(document.getElementById('addTestTypeModal'));
  const newTestTypeInput = document.getElementById('newTestType');


  createNewTestButton.addEventListener('click', () => {
    createTestModal.show();
  });

  saveTestButton.addEventListener('click', () =>
    saveNewTest()
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

  saveTestTypeButton.addEventListener('click', async function () {
    const newTestType = newTestTypeInput.value.trim();
    if (!newTestType) {
      alert('Vui lòng nhập tên loại đề thi.');
      return;
    }

    try {
      const response = await fetch("http://localhost:5112/api/v1/examSeries/createExamSeries", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Accept": "text/plain"
        },
        body: JSON.stringify({ name: newTestType })
      });

      if (response.ok) {
        const createdExamSeries = await response.json();
        addTestType(createdExamSeries.name, testTypeDropdownMenu);
        newTestTypeInput.value = '';
        addTestTypeModal.hide();
      } else {
        alert("Không thể thêm loại đề thi. Vui lòng thử lại.");
      }
    } catch (error) {
      console.error("Error adding new test type:", error);
      alert("Đã xảy ra lỗi. Vui lòng thử lại.");
    }
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
    .catch(error => {
      console.error(error);
    });
});

document.querySelectorAll(".test-card-title-container").forEach(container => {
  container.addEventListener('click', function () {
    const examId = this.getAttribute("data-exam-id");
    localStorage.setItem("examId", examId);
    const url = this.getAttribute("data-url")
    window.location.href = url;
  })
})

document.addEventListener('DOMContentLoaded', function () {
  localStorage.removeItem("examId");
})
