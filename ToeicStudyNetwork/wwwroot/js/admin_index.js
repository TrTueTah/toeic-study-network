let questionCounter = 2;
let questions = [];

document.addEventListener('DOMContentLoaded', function () {
  questions.push({id: 1, title: '', description: ''});
});
document.addEventListener('DOMContentLoaded', function () {
  for (let i = 2; i <= 200; i++) {
    questions.push({ id: i, title: '', description: '' });

    const newQuestionElement = document.createElement('div');
    newQuestionElement.id = `test-question-${i}`;
    newQuestionElement.className = 'pb-2';
    newQuestionElement.innerHTML = `
      <div class="section-content-container" style="display: none;">
        <div class="d-flex flex-column">
          <b id="test-question-title-${i}"></b>
          <span>
            <ul id="test-answer-options-${i}" style="padding-left: 1rem; list-style: none"></ul>
          </span>
          <div class="button-group button-group-absolute">
            <button class="button-group-item" id="edit-question-button-${i}">
              <i class="fa fa-edit" style="margin-top: -2px; padding-right: 1px; color: var(--black-500);"></i>
            </button>
          </div>
        </div>
      </div>
      <form class="edit-form" style="display: block;">
        <div>
          <div style="position: relative">
            <label class="form-label" style="width: 350px">
              <span class="label-text label-required">Question</span>
              <input type="text" placeholder="Enter question here" required="" class="form-input" id="test-question-input-${i}" value="">
              <span id="error-message-${i}" class="error-message" style="color: red; font-size: 12px; display: none;"></span>
            </label>
            <label class="form-label">
              <span class="label-text label-required">Answer options</span>
              <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="4" wrap="hard" id="test-answer-input-${i}"></textarea>
            </label>
          </div>
        </div>
      </form>`;

    document.getElementById('test-question-list').appendChild(newQuestionElement);

    const questionListItem = `
      <span class="test-questions-listitem" data-qid="${i}" id="test-questions-lisitem-${i}">${i}</span>
    `;
    document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);
  }
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

function saveTestInfo() {
  const infoText = document.getElementById('test-info-title-input').value;
  const descriptionText = document.getElementById('test-info-description-input').value;

  document.getElementById('test-info-title').innerText = infoText;
  document.getElementById('test-info-description').innerText = descriptionText;

  const testInfoSection = document.getElementById('test-info-section');
  toggleEditMode(testInfoSection, false);
}

function saveQuestions() {
  console.log(questions);
  let isValid = true;

  questions.forEach((question) => {
    const questionText = document.getElementById(`test-question-input-${question.id}`).value;
    const optionsText = document.getElementById(`test-answer-input-${question.id}`).value;

    if (!questionText.trim()) {
      isValid = false;
      // alert(`Question ${question.id} is missing required fields.`);
      return;
    }

    question.title = questionText;
    question.description = optionsText;

    document.getElementById(`test-question-title-${question.id}`).innerText = `${question.id}. ${questionText}`;
    document.getElementById(`test-answer-options-${question.id}`).innerHTML = optionsText.split('\n').map((option, index) => {
      return `<li>(${String.fromCharCode(65 + index)}) ${option}</li>`;
    }).join('');

    const questionSection = document.getElementById(`test-question-${question.id}`);
    toggleEditMode(questionSection, false);
  });

  if (isValid) {
    const testQuestionSection = document.getElementById('test-question-section');
    toggleEditMode(testQuestionSection, false);
  }
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

document.getElementById('edit-info-button-1').addEventListener('click', function () {
  const testInfoSection = document.getElementById('test-info-section');
  toggleEditMode(testInfoSection, true);
});

document.getElementById('edit-question-button-1').addEventListener('click', function () {
  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, true);

  questions.map((question) => {
    console.log(question)
    if (question.id > 1) {
      const questionSection = document.getElementById(`test-question-${question.id}`);
      toggleEditMode(questionSection, true);
    }
  })
});

document.getElementById('edit-info-button-1').addEventListener('click', function () {
  const testInfoSection = document.getElementById('test-info-section');
  toggleEditMode(testInfoSection, true);
});

document.getElementById('edit-question-button-1').addEventListener('click', function () {
  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, true);

  questions.map((question) => {
    console.log(question)
    if (question.id > 1) {
      const questionSection = document.getElementById(`test-question-${question.id}`);
      toggleEditMode(questionSection, true);
    }
  })
});


