document.addEventListener("DOMContentLoaded", () => {
  initializeUnloadWarning();
  initializeTabClicks();
  initializePlayer();
  initializeRadioAnswerChange();
  initializeQuestionListItemClick();
  initializeCountdownTimer(120 * 60);
  handleExit();
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
      const timeTaken = Math.floor(parseFloat(document.getElementById('timeleft').getAttribute('data-totaltime')));

      const requestData = {
        userId: 'ee74d453-9307-4c91-8031-ddb934b9313d',
        examId: examId,
        timeTaken: timeTaken,
        type: 'Full Test',
        "answers": {
          "1": "D",
          "2": "D",
          "3": "C",
          "4": "C",
          "5": "A",
          "6": "C",
          "7": "B",
          "8": "B",
          "9": "B",
          "10": "C",
          "11": "C",
          "12": "D",
          "13": "C",
          "14": "A",
          "15": "C",
          "16": "B",
          "17": "D",
          "18": "B",
          "19": "D",
          "20": "C",
          "21": "A",
          "22": "A",
          "23": "A",
          "24": "A",
          "25": "B",
          "26": "D",
          "27": "A",
          "28": "A",
          "29": "D",
          "30": "D",
          "31": "A",
          "32": "A",
          "33": "C",
          "34": "A",
          "35": "B",
          "36": "A",
          "37": "A",
          "38": "A",
          "39": "B",
          "40": "B",
          "41": "D",
          "42": "D",
          "43": "A",
          "44": "B",
          "45": "D",
          "46": "B",
          "47": "D",
          "48": "B",
          "49": "C",
          "50": "A",
          "51": "A",
          "52": "B",
          "53": "C",
          "54": "C",
          "55": "A",
          "56": "C",
          "57": "C",
          "58": "C",
          "59": "C",
          "60": "A",
          "61": "C",
          "62": "B",
          "63": "D",
          "64": "C",
          "65": "B",
          "66": "D",
          "67": "B",
          "68": "C",
          "69": "D",
          "70": "C",
          "71": "D",
          "72": "A",
          "73": "C",
          "74": "A",
          "75": "C",
          "76": "B",
          "77": "D",
          "78": "A",
          "79": "B",
          "80": "D",
          "81": "B",
          "82": "B",
          "83": "D",
          "84": "D",
          "85": "B",
          "86": "C",
          "87": "C",
          "88": "C",
          "89": "D",
          "90": "D",
          "91": "B",
          "92": "C",
          "93": "A",
          "94": "C",
          "95": "C",
          "96": "B",
          "97": "D",
          "98": "A",
          "99": "A",
          "100": "D",
          "101": "B",
          "102": "D",
          "103": "B",
          "104": "A",
          "105": "B",
          "106": "C",
          "107": "A",
          "108": "C",
          "109": "B",
          "110": "D",
          "111": "D",
          "112": "A",
          "113": "C",
          "114": "A",
          "115": "B",
          "116": "B",
          "117": "D",
          "118": "D",
          "119": "C",
          "120": "D",
          "121": "C",
          "122": "D",
          "123": "A",
          "124": "D",
          "125": "D",
          "126": "C",
          "127": "D",
          "128": "C",
          "129": "A",
          "130": "C",
          "131": "D",
          "132": "C",
          "133": "B",
          "134": "B",
          "135": "B",
          "136": "A",
          "137": "B",
          "138": "A",
          "139": "A",
          "140": "B",
          "141": "A",
          "142": "C",
          "143": "A",
          "144": "D",
          "145": "B",
          "146": "D",
          "147": "C",
          "148": "B",
          "149": "A",
          "150": "C",
          "151": "D",
          "152": "C",
          "153": "A",
          "154": "D",
          "155": "C",
          "156": "B",
          "157": "B",
          "158": "C",
          "159": "D",
          "160": "D",
          "161": "C",
          "162": "B",
          "163": "A",
          "164": "A",
          "165": "A",
          "166": "D",
          "167": "C",
          "168": "C",
          "169": "A",
          "170": "A",
          "171": "B",
          "172": "B",
          "173": "C",
          "174": "B",
          "175": "B",
          "176": "C",
          "177": "A",
          "178": "B",
          "179": "C",
          "180": "C",
          "181": "A",
          "182": "A",
          "183": "B",
          "184": "A",
          "185": "D",
          "186": "D",
          "187": "D",
          "188": "A",
          "189": "D",
          "190": "B",
          "191": "A",
          "192": "C",
          "193": "D",
          "194": "A",
          "195": "A",
          "196": "B",
          "197": "C",
          "198": "C",
          "199": "D",
          "200": "A"
        }

      };

      $.ajax({
        url: 'http://localhost:5112/api/v1/result/submit',
        type: 'POST',
        contentType: 'application/json-patch+json',
        data: JSON.stringify(requestData),
        success: function(response) {
          alert('Test submitted successfully!');
        },
        error: function(xhr, status, error) {
          alert('There was an error submitting the test: ' + error);
        }
      });
    });
  }
}

