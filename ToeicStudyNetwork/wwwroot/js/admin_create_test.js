let questionCounter = 1;
let questions = [];

const questionGroups = [
  { groupId: 1, questions: [1], media: 'both' },
  { groupId: 2, questions: [2], media: 'both' },
  { groupId: 3, questions: [3], media: 'both' },
  { groupId: 4, questions: [4], media: 'both' },
  { groupId: 5, questions: [5], media: 'both' },
  { groupId: 6, questions: [6], media: 'both' },
  { groupId: 7, questions: [7], media: 'audio' },
  { groupId: 8, questions: [8], media: 'audio' },
  { groupId: 9, questions: [9], media: 'audio' },
  { groupId: 10, questions: [10], media: 'audio' },
  { groupId: 11, questions: [11], media: 'audio' },
  { groupId: 12, questions: [12], media: 'audio' },
  { groupId: 13, questions: [13], media: 'audio' },
  { groupId: 14, questions: [14], media: 'audio' },
  { groupId: 15, questions: [15], media: 'audio' },
  { groupId: 16, questions: [16], media: 'audio' },
  { groupId: 17, questions: [17], media: 'audio' },
  { groupId: 18, questions: [18], media: 'audio' },
  { groupId: 19, questions: [19], media: 'audio' },
  { groupId: 20, questions: [20], media: 'audio' },
  { groupId: 21, questions: [21], media: 'audio' },
  { groupId: 22, questions: [22], media: 'audio' },
  { groupId: 23, questions: [23], media: 'audio' },
  { groupId: 24, questions: [24], media: 'audio' },
  { groupId: 25, questions: [25], media: 'audio' },
  { groupId: 26, questions: [26], media: 'audio' },
  { groupId: 27, questions: [27], media: 'audio' },
  { groupId: 28, questions: [28], media: 'audio' },
  { groupId: 29, questions: [29], media: 'audio' },
  { groupId: 30, questions: [30], media: 'audio' },
  { groupId: 31, questions: [31], media: 'audio' },
  { groupId: 32, questions: [32, 33, 34], media: 'audio' },
  { groupId: 33, questions: [35, 36, 37], media: 'audio' },
  { groupId: 34, questions: [38, 39, 40], media: 'audio' },
  { groupId: 35, questions: [41, 42, 43], media: 'audio' },
  { groupId: 36, questions: [44, 45, 46], media: 'audio' },
  { groupId: 37, questions: [47, 48, 49], media: 'audio' },
  { groupId: 38, questions: [50, 51, 52], media: 'audio' },
  { groupId: 39, questions: [53, 54, 55], media: 'audio' },
  { groupId: 40, questions: [56, 57, 58], media: 'audio' },
  { groupId: 41, questions: [59, 60, 61], media: 'audio' },
  { groupId: 42, questions: [62,63,64], media: 'both' },
  { groupId: 43, questions: [65,66,67], media: 'both' },
  { groupId: 44, questions: [68,69,70], media: 'both' },
  { groupId: 45, questions: [71, 72, 73], media: 'audio' },
  { groupId: 46, questions: [74, 75, 76], media: 'audio' },
  { groupId: 47, questions: [77, 78, 79], media: 'audio' },
  { groupId: 48, questions: [80, 81, 82], media: 'audio' },
  { groupId: 49, questions: [83, 84, 85], media: 'audio' },
  { groupId: 50, questions: [86, 87, 88], media: 'audio' },
  { groupId: 51, questions: [89, 90, 91], media: 'audio' },
  { groupId: 52, questions: [92, 93, 94], media: 'audio' },
  { groupId: 53,questions: [95,96,97], media: 'both' },
  { groupId: 54,questions: [98,99,100], media: 'both' },
  { groupId: 55,questions: [101]  },
  { groupId: 56,questions: [102] },
  { groupId: 57,questions: [103] },
  { groupId: 58,questions: [104] },
  { groupId: 59,questions: [105] },
  { groupId: 60,questions: [106] },
  { groupId: 61,questions: [107] },
  { groupId: 62,questions: [108] },
  { groupId: 63,questions: [109] },
  { groupId: 64,questions: [110] },
  { groupId: 65,questions: [111] },
  { groupId: 66,questions: [112] },
  { groupId: 67,questions: [113] },
  { groupId: 68,questions: [114] },
  { groupId: 69,questions: [115] },
  { groupId: 70,questions: [116] },
  { groupId: 71,questions: [117] },
  { groupId: 72,questions: [118] },
  { groupId: 73,questions: [119] },
  { groupId: 74,questions: [120] },
  { groupId: 75,questions: [121] },
  { groupId: 76,questions: [122] },
  { groupId: 77,questions: [123] },
  { groupId: 78,questions: [124] },
  { groupId: 79,questions: [125] },
  { groupId: 80,questions: [126] },
  { groupId: 81,questions: [127] },
  { groupId: 83,questions: [128] },
  { groupId: 84,questions: [129] },
  { groupId: 85,questions: [130] },
  { groupId: 86, questions: [131, 132, 133, 134], media: 'image' },
  { groupId: 87, questions: [135, 136, 137, 138], media: 'image' },
  { groupId: 88, questions: [139, 140, 141, 142], media: 'image' },
  { groupId: 89, questions: [143, 144, 145, 146], media: 'image' },
  { groupId: 90, questions: [147, 148, 149, 150], media: 'image' },
  { groupId: 91, questions: [147, 148], media: 'image' },
  { groupId: 92, questions: [149, 150], media: 'image' },
  { groupId: 93, questions: [151, 152], media: 'image' },
  { groupId: 94, questions: [153, 154], media: 'image' },
  { groupId: 95, questions: [155, 156, 157], media: 'image' },
  { groupId: 96, questions: [158, 159, 160], media: 'image' },
  { groupId: 97, questions: [161, 162, 163], media: 'image' },
  { groupId: 98, questions: [164, 165, 166, 167], media: 'image' },
  { groupId: 99, questions: [168, 169, 170, 171], media: 'image' },
  { groupId: 100, questions: [172, 173, 174, 175], media: 'image' },
  { groupId: 101, questions: [176, 177, 178, 179, 180], media: 'image' },
  { groupId: 102, questions: [181, 182, 183, 184, 185], media: 'image' },
  { groupId: 103, questions: [186, 187, 188, 189, 190], media: 'image' },
  { groupId: 104, questions: [191, 192, 193, 194, 195], media: 'image' },
  { groupId: 105, questions: [196, 197, 198, 199, 200], media: 'image' }
];

document.addEventListener('DOMContentLoaded', function () {
  questionGroups.forEach(group => {
    group.questions.forEach((questionId, index) => {
      const questionElement = document.createElement('div');
      questionElement.id = `test-question-${questionId}`;
      questionElement.className = 'pb-2';

      questions.push({ id: questionId, title: '', description: '' });
      
      questionElement.innerHTML = `
        <div class="section-content-container" style="display: none;">
          <div class="d-flex flex-column">
            <b id="test-question-title-${questionId}"></b>
            <span>
              <ul id="test-answer-options-${questionId}" style="padding-left: 1rem; list-style: none"></ul>
            </span>
          </div>
        </div>
        <form class="edit-form" style="display: block;">
          <div>          
            <label class="form-label">
              <span for="test-question-input" class="label-text label-required">Question ${questionId}</span>
              <input type="text" placeholder="Enter question here" required="" class="form-input" id="test-question-input-${questionId}" value="">
            </label>
            <label class="form-label">
              <span for="test-answer-input" class="label-text label-required">Answer options</span>
              <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="4" wrap="hard" id="test-answer-input-${questionId}"></textarea>
            </label>
          </div>
        </form>
      `;

      document.getElementById('test-question-list').appendChild(questionElement);
      
      const questionListItem = `
        <span class="test-questions-listitem" data-qid="${questionId}" id="test-questions-lisitem-${questionId}">${questionId}</span>
      `;
      document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);
    });
  });
});

document.addEventListener('DOMContentLoaded', function () {
  document.querySelectorAll('.test-questions-listitem').forEach(item => {
    item.addEventListener('click', function () {
      const qid = this.dataset.qid;
      const targetQuestion = document.getElementById(`test-question-${qid}`);
      if (targetQuestion) {
        targetQuestion.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }
    });
  });
});

document.addEventListener('scroll', function () {
  const subContainer = document.querySelector('.sub-container');
  const scrollThreshold = 100;

  if (window.scrollY > scrollThreshold) {
    subContainer.classList.add('sticky');
    subContainer.style.top = '15vh';
  } else {
    subContainer.classList.remove('sticky');
    subContainer.style.top = '12vh';
  }
});

function toggleEditMode(section, isEditMode) {
  const form = section.querySelector('.edit-form');
  const contentContainer = section.querySelector('.section-content-container');
  if (isEditMode) {
    section.classList.add('edit-mode');
    form.style.display = 'block';
    contentContainer.style.display = 'none';
  } else {
    section.classList.remove('edit-mode');
    form.style.display = 'none';
    contentContainer.style.display = 'block';
  }
}

function saveQuestions() {
  questions.forEach((question) => {
    const questionText = document.getElementById(`test-question-input-${question.id}`).value;
    const optionsText = document.getElementById(`test-answer-input-${question.id}`).value;

    question.title = questionText;
    question.description = optionsText;

    document.getElementById(`test-question-title-${question.id}`).innerText = `${question.id}. ${questionText}`;
    document.getElementById(`test-answer-options-${question.id}`).innerHTML = optionsText.split('\n').map((option, index) => {
      return `<li>(${String.fromCharCode(65 + index)}) ${option}</li>`;
    }).join('');

    const questionSection = document.getElementById(`test-question-${question.id}`);
    toggleEditMode(questionSection, false);
  });

  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, false);
}

function deleteQuestion(event, id) {
  event.preventDefault();

  const questionElement = document.getElementById(`test-question-${id}`);
  if (questionElement) {
    questionElement.remove();
  }

  questions = questions.filter(question => question.id !== id);

  const questionListItem = document.getElementById(`test-questions-lisitem-${id}`);
  if (questionListItem) {
    questionListItem.remove();
  }

  const questionList = Array.from(document.getElementById('test-question-list').children);

  questionList.forEach((question, index) => {
    const newQuestionId = index + 1;

    question.id = `test-question-${newQuestionId}`;

    const inputs = question.querySelectorAll('input, textarea');
    inputs.forEach(input => {
      input.id = input.id.replace(/\d+$/, newQuestionId);
    });

    const options = question.querySelectorAll('ul');
    options.forEach(option => {
      option.id = option.id.replace(/\d+$/, newQuestionId);
    });

    const titles = question.querySelectorAll('b');
    titles.forEach(title => {
      title.id = title.id.replace(/\d+$/, newQuestionId);
    });

    const listItem = document.getElementById(`test-questions-lisitem-${index + 2}`);
    if (listItem) {
      listItem.id = `test-questions-lisitem-${newQuestionId}`;
      listItem.innerText = newQuestionId;
    }

    const updatedQuestion = questions.find(q => q.id === (newQuestionId + 1));
    if (updatedQuestion) {
      updatedQuestion.id = newQuestionId;
    }

    const deleteButton = question.querySelector('.button-group-item-delete');
    if (deleteButton) {
      deleteButton.setAttribute('onclick', `deleteQuestion(event, ${newQuestionId})`);
    }
  });

  questionCounter = questions.length + 1;
}

function addMoreQuestion(e) {
  e.preventDefault();

  const questionId = questionCounter++;

  const newQuestionElement = document.createElement('div');
  newQuestionElement.id = `test-question-${questionId}`;
  newQuestionElement.className = 'py-2';
  newQuestionElement.style.borderTop = `1px solid #d8d8d8`

  newQuestionElement.innerHTML =
    `<div id="test-section-container-${questionId}" class="section-content-container" style="display: none;">
      <div class="d-flex flex-column">
        <b id="test-question-title-${questionId}"></b>
        <span>
                <ul id="test-answer-options-${questionId}" style="padding-left: 1rem; list-style: none"></ul>
            </span>

      </div>
    </div>
  <form class="edit-form" style="display: block;">
    <div>
      <div style="position: relative">
        <label class="form-label" style="width: 350px">
          <span class="label-text label-required">Question</span>
          <input type="text" placeholder="Enter question here" required="" class="form-input" id="test-question-input-${questionId}" value="">
            <span id="error-message-${questionId}" class="error-message" style="color: red; font-size: 12px; display: none;"></span>
        </label>
        <label class="form-label">
          <span class="label-text label-required">Answer options</span>
          <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="4" wrap="hard" id="test-answer-input-${questionId}"></textarea>
        </label>
        <div class="button-group-absolute">
          <button class="button-group-item button-group-item-delete" onclick="deleteQuestion(event, ${questionId})">
            <i class="fa fa-trash" style="margin-top: -2px; padding-right: 1px; color: var(--red-500);"></i>
          </button>
        </div>
      </div>
    </div>
  </form>`;

  document.getElementById('test-question-list').appendChild(newQuestionElement);

  const questionListItem =
    `<span class="test-questions-listitem" data-qid="${questionId}" id="test-questions-lisitem-${questionId}">${questionId}</span>`
  ;
  document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);

  questions.push({ id: questionId, title: '', description: '' });
}

document.querySelectorAll('.cancel-button').forEach(button => {
  button.addEventListener('click', function (event) {
    event.preventDefault();
    const section = button.closest('.section');
    toggleEditMode(section, false);
  });
});

document.querySelectorAll('.button-group-item').forEach(button => {
  button.addEventListener('click', function (event) {
    event.preventDefault();
    const section = button.closest('.section');
    toggleEditMode(section, true);
  });
});

document.getElementById('edit-question-button-1').addEventListener('click', function () {
  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, true);
  console.log(testQuestionSection);
  questions.map((question) => {
    const questionSection = document.getElementById(`test-question-${question.id}`);
    toggleEditMode(questionSection, true);
  })
});

document.addEventListener('DOMContentLoaded', function () {
  const newTestData = JSON.parse(localStorage.getItem('newTestData'));
  if (newTestData) {
    const testTitleElement = document.getElementById('test-info-title');
    if (testTitleElement) {
      testTitleElement.textContent = newTestData.title;
    }
    console.log(newTestData);
  } else {
    alert('Không có dữ liệu đề thi mới.');
  }
});



