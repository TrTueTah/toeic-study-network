document.addEventListener("DOMContentLoaded", () => {
  initializeUnloadWarning();
  initializeTabClicks();
  initializePlayer();
  initializeRadioAnswerChange();
  initializeQuestionListItemClick();
});

function initializeUnloadWarning() {
  window.addEventListener('beforeunload', function (event) {
    const message = "Bạn có chắc chắn muốn tải lại trang? Mọi dữ liệu chưa được lưu sẽ bị mất.";
    event.returnValue = message;
    return message;
  });
}

function initializeTabClicks() {
  const testTabs = document.querySelectorAll("#testTabs .nav-link");
  const questionContainer = document.querySelector("#question-container");

  testTabs.forEach((tab) => {
    tab.addEventListener("click", async (e) => {
      e.preventDefault();
      const partId = tab.getAttribute("data-bs-target");
      await loadQuestions(partId, questionContainer);
    });
  });
}

async function loadQuestions(partId, questionContainer) {
  const response = await fetch(`/Test/${partId}/questions`);
  questionContainer.innerHTML = await response.text();
}

function initializePlayer() {
  new Plyr('#player', {
    controls: ['play', 'progress', 'current-time', 'duration', 'mute', 'volume'],
    loop: { active: true },
  });
}

function initializeRadioAnswerChange() {
  document.querySelectorAll('.question-answers input[type="radio"]').forEach((input) => {
    input.addEventListener('change', () => {
      const questionNumber = input.name.split('-')[1];
      markQuestionAsDone(questionNumber);
    });
  });
}

function markQuestionAsDone(questionNumber) {
  const listItem = document.querySelector(`#test-questions-lisitem-${questionNumber}`);
  listItem?.classList.add('done');
}

function initializeQuestionListItemClick() {
  document.querySelectorAll('.test-questions-listitem').forEach((item) => {
    item.addEventListener('click', () => {
      const questionNumber = item.getAttribute('data-qid');
      scrollToQuestion(questionNumber);
    });
  });
}

function scrollToQuestion(questionNumber) {
  const questionWrapper = document.querySelector(`#question-wrapper-${questionNumber}`);
  const partWrapper = questionWrapper?.closest('.tab-pane');

  if (partWrapper) {
    const partId = partWrapper.id;
    switchTabIfNeeded(partId);
    questionWrapper?.scrollIntoView({ behavior: 'smooth', block: 'center' });
  }
}

function switchTabIfNeeded(partId) {
  const activeTab = document.querySelector(`#testTabs .nav-link.active`);
  if (activeTab && activeTab.getAttribute('data-bs-target') !== `#${partId}`) {
    const tab = document.querySelector(`#testTabs .nav-link[data-bs-target="#${partId}"]`);
    tab?.click();
  }
}
