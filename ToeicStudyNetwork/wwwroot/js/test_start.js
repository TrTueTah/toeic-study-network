document.addEventListener("DOMContentLoaded", () => {
  initializeUnloadWarning();
  initializeTabClicks();
  initializePlayer();
  initializeRadioAnswerChange();
  initializeQuestionListItemClick();
  initializeCountdownTimer(120 * 60);
  handleExit();
  handleExitPractice();
  initializeSubmitTest();
});

function handleExit() {
  const exitButton = document.getElementById("exit-button");

  if (exitButton) {
    exitButton.addEventListener("click", (event) => {
      event.preventDefault();

      const confirmExit = confirm("Bạn có chắc chắn muốn thoát? Mọi dữ liệu chưa nộp sẽ bị mất.");
      if (confirmExit) {
        const currentUrl = window.location.href;
        const baseUrl = currentUrl.split("/start")[0];
        window.location.href = baseUrl;
      }
    });
  }
}

function handleExitPractice() {
  const exitPracticeButton = document.getElementById("exit-practice-button");

  if (exitPracticeButton) {
    exitPracticeButton.addEventListener("click", (event) => {
      event.preventDefault();

      const confirmExit = confirm("Bạn có chắc chắn muốn thoát? Mọi dữ liệu chưa nộp sẽ bị mất.");
      if (confirmExit) {
        const currentUrl = window.location.href;
        const baseUrl = currentUrl.split("/practice")[0];
        window.location.href = baseUrl;
      }
    });
  }
}

function initializeCountdownTimer(initialTime) {
  let totalTime = initialTime;
  const timeLeftSpan = document.getElementById('timeleft');
  timeLeftSpan.setAttribute('data-totaltime', totalTime); 

  const updateTime = () => {
    const hours = Math.floor(totalTime / 3600).toString().padStart(2, "0");
    const minutes = Math.floor((totalTime % 3600) / 60).toString().padStart(2, "0");
    const seconds = (totalTime % 60).toString().padStart(2, "0");

    timeLeftSpan.textContent = `${hours}:${minutes}:${seconds}`;

    timeLeftSpan.setAttribute('data-totaltime', totalTime);

    if (totalTime <= 0) {
      clearInterval(intervalId);
      alert("Đã hết thời gian làm bài !");
    } else {
      totalTime--;
    }
  };

  const intervalId = setInterval(updateTime, 1000);
  updateTime();
}


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

function initializeSubmitTest() {
  const submitButton = document.getElementById("submit-test");

  if (submitButton) {
    submitButton.addEventListener('click', (event) => {
      event.preventDefault();

      const answers = {};
      $('input[type="radio"]:checked').each(function() {
        const questionNumber = $(this).closest('.question-wrapper').data('qid');
        answers[questionNumber] = $(this).val();
      });

      const examId = document.getElementById("exam-id").value;
      const examType = document.getElementById("exam-type").value;
      const timeTaken = Math.floor(parseFloat(document.getElementById('timeleft').getAttribute('data-totaltime')));
      const userId = Cookies.get('userId')
      const requestData = {
        userId: userId,
        examId: examId,
        timeTaken: timeTaken,
        type: examType,
        answers: answers
      };

      $.ajax({
        url: 'http://localhost:5112/api/v1/result/submit',
        type: 'POST',
        contentType: 'application/json-patch+json',
        data: JSON.stringify(requestData),
        success: function(response) {
          window.location.href = `/Test/${examId}/result/${response.result.resultId}`;
        },
        error: function(xhr, status, error) {
          alert('There was an error submitting the test: ' + error);
        }
      });
    });
  }
}

