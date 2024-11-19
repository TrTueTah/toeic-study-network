let questionCounter = 2;
let questions = [];

document.addEventListener('DOMContentLoaded', (event) => {
  questions.push({ id: 1, title: '', description: '' });
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

function saveTestInfo(event) {
  const infoText = document.getElementById('test-info-title-input').value;
  const descriptionText = document.getElementById('test-info-description-input').value;

  document.getElementById('test-info-title').innerText = infoText;
  document.getElementById('test-info-description').innerText = descriptionText;

  const testInfoSection = document.getElementById('test-info-section');
  toggleEditMode(testInfoSection, false);
}


function saveQuestions() {
  questions.forEach((question, index) => {
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
function deleteQuestion(event, questionId) {
  event.preventDefault();

  const questionElement = document.getElementById(`test-question-${questionId}`);
  if (questionElement) {
    questionElement.remove();
  }

  questions = questions.filter(question => question.id !== questionId);

  questions.forEach((question, index) => {
    question.id = index + 1;
  });

  console.log(questions); 
}



function addMoreQuestion(e) {
  e.preventDefault();

  const questionId = questionCounter++;

  const newQuestionElement = document.createElement('div');
  newQuestionElement.id = `test-question-${questionId}`;
  newQuestionElement.className = 'mb-4';

  let questionDivider;
  if (questionId > 1) {
    questionDivider = '<div class="question-divider"></div>'
  }

  newQuestionElement.innerHTML =
    `${questionDivider}
    <div id="test-section-container-${questionId}" class="section-content-container" style="display: none;">
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
                    <span class="label-text label-required">Title</span>
                    <input type="text" placeholder="Enter question title" required="" class="form-input" id="test-question-input-${questionId}" value="">
                </label>
                <label class="form-label">
                    <span class="label-text label-required">Description</span>
                    <textarea placeholder="" required="" class="form-textarea" id="test-answer-input-${questionId}"></textarea>
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

  questions.push({ id: questionId, title: '', description: '' });
}

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
