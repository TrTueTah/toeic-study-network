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
  { groupId: 82,questions: [128] },
  { groupId: 83,questions: [129] },
  { groupId: 84,questions: [130] },
  { groupId: 85, questions: [131, 132, 133, 134], media: 'image' },
  { groupId: 86, questions: [135, 136, 137, 138], media: 'image' },
  { groupId: 87, questions: [139, 140, 141, 142], media: 'image' },
  { groupId: 88, questions: [143, 144, 145, 146], media: 'image' },
  { groupId: 89, questions: [147, 148], media: 'image' },
  { groupId: 90, questions: [149, 150], media: 'image' },
  { groupId: 91, questions: [151, 152], media: 'image' },
  { groupId: 92, questions: [153, 154], media: 'image' },
  { groupId: 93, questions: [155, 156, 157], media: 'image' },
  { groupId: 94, questions: [158, 159, 160], media: 'image' },
  { groupId: 95, questions: [161, 162, 163], media: 'image' },
  { groupId: 96, questions: [164, 165, 166, 167], media: 'image' },
  { groupId: 97, questions: [168, 169, 170, 171], media: 'image' },
  { groupId: 98, questions: [172, 173, 174, 175], media: 'image' },
  { groupId: 99, questions: [176, 177, 178, 179, 180], media: 'image' },
  { groupId: 100, questions: [181, 182, 183, 184, 185], media: 'image' },
  { groupId: 101, questions: [186, 187, 188, 189, 190], media: 'image' },
  { groupId: 102, questions: [191, 192, 193, 194, 195], media: 'image' },
  { groupId: 103, questions: [196, 197, 198, 199, 200], media: 'image' }
];

document.addEventListener('DOMContentLoaded', async function () {
  const examId = localStorage.getItem('examId');
  const response = await fetch(`http://localhost:5112/api/v1/exam/getExamById/${examId}`);
  const examData = await response.json();
  
  if (examData.questionGroups.length > 0) {
    examData.questionGroups.forEach(group => {
      group.questions.forEach((questionItem, index) => {
        const questionId = questionItem.id;
        const questionElement = document.createElement('div');
        questionElement.id = `test-question-${questionId}`;
        questionElement.className = 'pb-2';

        questions.push({ id: questionId, title: '', description: '', answers: [] });

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
              <span for="test-question-input" class="label-text label-required">Question ${questionItem.questionNumber}</span>
              <input type="text" placeholder="Enter question here" required="" class="form-input" id="test-question-input-${questionId}" value="${questionItem.title}">
            </label>
            <label class="form-label">
              <span for="test-answer-input" class="label-text label-required">Answer options</span>
              <div class="d-flex flex-column gap-2" id="test-answer-input-${questionId}">
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="A" id="radio-answerA-${questionId}" />
                      <span>A.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerA-input-${questionId}">${questionItem.answerA}</textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="B" id="radio-answerB-${questionId}" />
                      <span>B.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerB-input-${questionId}">${questionItem.answerB}</textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="C" id="radio-answerC-${questionId}" />
                      <span>C.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerC-input-${questionId}">${questionItem.answerC}</textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="D" id="radio-answerD-${questionId}" />
                      <span>D.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerD-input-${questionId}">${questionItem.answerD}</textarea>
                  </div>
              </div>
          </label>

          </div>
        </form>
      `;

        document.getElementById('test-question-list').appendChild(questionElement);

        const questionListItem = `
        <span class="test-questions-listitem" data-qid="${questionId}" id="test-questions-listitem-${questionId}">${questionItem.questionNumber}</span>
      `;
        document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);
      });
    });
    
  } else {
    questionGroups.forEach(group => {
      group.questions.forEach((questionId, index) => {
        const questionElement = document.createElement('div');
        questionElement.id = `test-question-${questionId}`;
        questionElement.className = 'pb-2';

        questions.push({ id: questionId, title: '', description: '', answers: [] });

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
              <div class="d-flex flex-column gap-2" id="test-answer-input-${questionId}">
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="A" id="radio-answerA-${questionId}" />
                      <span>A.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerA-input-${questionId}"></textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="B" id="radio-answerB-${questionId}" />
                      <span>B.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerB-input-${questionId}"></textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="C" id="radio-answerC-${questionId}" />
                      <span>C.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerC-input-${questionId}"></textarea>
                  </div>
                  <div class="test-answer-item">
                      <input type="radio" name="test-answer-${questionId}" value="D" id="radio-answerD-${questionId}" />
                      <span>D.</span>
                      <textarea placeholder="Enter answer options here" required="" class="form-textarea" rows="1" wrap="hard" id="test-answerD-input-${questionId}"></textarea>
                  </div>
              </div>
          </label>

          </div>
        </form>
      `;

        document.getElementById('test-question-list').appendChild(questionElement);

        const questionListItem = `
        <span class="test-questions-listitem" data-qid="${questionId}" id="test-questions-listitem-${questionId}">${questionId}</span>
      `;
        document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);
      });
    });
  }

  document.querySelectorAll('.test-questions-listitem').forEach(item => {
    item.addEventListener('click', function () {
      console.log('clicked');
      const qid = this.dataset.qid;
      const targetQuestion = document.getElementById(`test-question-${qid}`);
      if (targetQuestion) {
        targetQuestion.scrollIntoView({ behavior: 'smooth', block: "center" });
      }

      const targetImage = document.getElementById(`upload-question-image-${qid}`);
      if (targetQuestion) {
        targetImage.scrollIntoView({ behavior: 'smooth', block: 'center' });
      }

      const targetAudio = document.getElementById(`upload-question-audio-${qid}`);
      if (targetQuestion) {
        targetAudio.scrollIntoView({ behavior: 'smooth', block: 'center' });
      }
    });
  });
});

document.addEventListener('DOMContentLoaded', function () {
  
});

document.addEventListener('scroll', function () {
  const subContainer = document.querySelector('.sub-container');
  const scrollThreshold = 100;

  if (window.scrollY > scrollThreshold) {
    subContainer.classList.add('sticky');
    subContainer.style.top = '13vh';
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

function previewQuestions() {
  questions.forEach((question) => {
    const questionText = document.getElementById(`test-question-input-${question.id}`).value;
    const optionsAText = document.getElementById(`test-answerA-input-${question.id}`).value;
    const optionsBText = document.getElementById(`test-answerB-input-${question.id}`).value;
    const optionsCText = document.getElementById(`test-answerC-input-${question.id}`).value;
    const optionsDText = document.getElementById(`test-answerD-input-${question.id}`).value;

    question.title = questionText;
    question.description = {
      A: optionsAText,
      B: optionsBText,
      C: optionsCText,
      D: optionsDText
    };

    const optionsHtml = [];
    if (optionsAText) optionsHtml.push(`<li>(A) ${optionsAText}</li>`);
    if (optionsBText) optionsHtml.push(`<li>(B) ${optionsBText}</li>`);
    if (optionsCText) optionsHtml.push(`<li>(C) ${optionsCText}</li>`);
    if (optionsDText) optionsHtml.push(`<li>(D) ${optionsDText}</li>`);

    document.getElementById(`test-question-title-${question.id}`).innerText = `${questionText}`;
    document.getElementById(`test-answer-options-${question.id}`).innerHTML = optionsHtml.join('');

    const questionSection = document.getElementById(`test-question-${question.id}`);
    toggleEditMode(questionSection, false);
  });

  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, false);
}

function collectQuestionData(questionGroups, status) {
  if (status === 'uploaded') {
    return questionGroups.map(group => {
      return group.questions.map(question => {
        const questionId = question.id;
        const questionTitle = document.getElementById(`test-question-input-${questionId}`).value;
        const answerA = document.getElementById(`test-answerA-input-${questionId}`).value;
        const answerB = document.getElementById(`test-answerB-input-${questionId}`).value;
        const answerC = document.getElementById(`test-answerC-input-${questionId}`).value;
        const answerD = document.getElementById(`test-answerD-input-${questionId}`).value;

        // const correctAnswer = [
        //   'A', 'B', 'C', 'D'
        // ].find(letter => document.getElementById(`test-answer${letter}-${questionId}`).checked);

        return {
          title: questionTitle,
          answerA: answerA,
          answerB: answerB,
          answerC: answerC,
          answerD: answerD,
          correctAnswer: "A",
          questionNumber: questionId
        };
      });
    }).flat();
  } else {
    return questionGroups.map(group => {
      return group.questions.map(questionId => {
        const questionTitle = document.getElementById(`test-question-input-${questionId}`).value;
        const answerA = document.getElementById(`test-answerA-input-${questionId}`).value;
        const answerB = document.getElementById(`test-answerB-input-${questionId}`).value;
        const answerC = document.getElementById(`test-answerC-input-${questionId}`).value;
        const answerD = document.getElementById(`test-answerD-input-${questionId}`).value;

        // const correctAnswer = [
        //   'A', 'B', 'C', 'D'
        // ].find(letter => document.getElementById(`test-answer${letter}-${questionId}`).checked);

        return {
          title: questionTitle,
          answerA: answerA,
          answerB: answerB,
          answerC: answerC,
          answerD: answerD,
          correctAnswer: "A",
          questionNumber: questionId
        };
      });
    }).flat();
  }
}

async function saveQuestions() {
  const examId = localStorage.getItem('examId');
  const response = await fetch(`http://localhost:5112/api/v1/exam/getExamById/${examId}`);
  const examData = await response.json();
  let questionsData = []
  if (examData.questionGroups.length > 0) {
    return examData;
  } else {
    questionsData = collectQuestionData(questionGroups, "pending");
    try {
      const response = await fetch('http://localhost:5112/api/v1/question/createQuestionList', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json-patch+json',
        },
        body: JSON.stringify({
          examId: examId,
          questions: questionsData
        })
      });

      if (!response.ok) {
        throw new Error(`Failed to save questions: ${response.statusText}`);
      }

      return response.json();
    } catch (error) {
      console.error('Error submitting questions:', error);
      throw error;
    }
  }
}

async function handleMoveToNextPage () {
  const uploadQuestionSection = document.getElementById("upload-question-section");
  const uploadImageSection = document.getElementById("upload-image-section");
  const uploadAudioSection = document.getElementById("upload-audio-section");
  const previousPageButton = document.getElementById("previous-page-button");
  const editQuestionButton = document.getElementById("edit-question-button");
  const previewQuestionButton = document.getElementById("preview-question-button");
  const saveQuestionButton = document.getElementById("save-question-button");
  const nextPageLoading = document.getElementById("next-page-loading");
  const nextPageArrow = document.getElementById('next-page-arrow');
  try {
    nextPageLoading.classList.remove("d-none");
    nextPageArrow.classList.add("d-none");
    
    const data = await saveQuestions();

    if (data.questionGroups) {
      const updatedGroups = questionGroups.map(group => {
        const newGroup = data.questionGroups[group.groupId - 1];
        console.log("newGroup", newGroup);
        if (newGroup) {
          return {
            groupId: newGroup.questions[0].groupId,
            questions: group.questions,
            media: group.media
          };
        }

        return group;
      });
      updatedGroups.forEach(group => {
        renderUploadMediaSection(group);
      });
    }
    nextPageLoading.classList.add("d-none");
    nextPageArrow.classList.remove("d-none");

    this.classList.add("d-none");
    previousPageButton.classList.remove("d-none");
    uploadQuestionSection.classList.add("d-none");
    uploadImageSection.classList.remove("d-none");
    uploadAudioSection.classList.remove("d-none");

    editQuestionButton.classList.add("d-none");
    previewQuestionButton.classList.add("d-none");
    saveQuestionButton.classList.remove("d-none");
  } catch (error) {
    nextPageLoading.classList.add("d-none");
    nextPageArrow.classList.remove("d-none");
    console.error("Error saving questions or rendering media section:", error);
  }
}

function handleEditQuestion () {
  const testQuestionSection = document.getElementById('test-question-section');
  toggleEditMode(testQuestionSection, true);
  questions.map((question) => {
    const questionSection = document.getElementById(`test-question-${question.id}`);
    toggleEditMode(questionSection, true);
  })
}

function handleMoveToPreviousPage () {
  const uploadQuestionSection = document.getElementById("upload-question-section")
  const uploadImageSection = document.getElementById("upload-image-section")
  const uploadAudioSection = document.getElementById("upload-audio-section")
  const nextPageButton = document.getElementById("next-page-button");
  const editQuestionButton = document.getElementById("edit-question-button");
  const previewQuestionButton  = document.getElementById("preview-question-button");
  const saveQuestionButton = document.getElementById("save-question-button");

  this.classList.add("d-none")
  nextPageButton.classList.remove("d-none");
  uploadQuestionSection.classList.remove("d-none");
  uploadImageSection.classList.add("d-none");
  uploadAudioSection.classList.add("d-none");

  editQuestionButton.classList.remove("d-none");
  previewQuestionButton.classList.remove("d-none");
  saveQuestionButton.classList.add("d-none");
}

async function handleSaveMediaFile () {
  const saveLoading = document.getElementById("save-loading");
  const saveIcon = document.getElementById("save-icon");
  const audioInputs = document.querySelectorAll('input[type="file"][accept="audio/*"]');
  const imageInputs = document.querySelectorAll('input[type="file"][accept="image/*"]');
  const audioApiEndpoint = "http://localhost:5112/api/v1/questionGroup/uploadQuestionGroupAudio";
  const imageApiEndpoint = "http://localhost:5112/api/v1/questionGroup/uploadQuestionGroupImage";

  saveLoading.classList.remove('d-none');
  saveIcon.classList.add('d-none');
  try {
    const uploadData = {};

    audioInputs.forEach(input => {
      const groupId = input.dataset.groupId;
      const files = input.files;

      if (files.length > 0) {
        if (!uploadData[groupId]) {
          uploadData[groupId] = { audioFiles: [], imageFiles: [] };
        }
        uploadData[groupId].audioFiles.push(...files);
      }
    });

    imageInputs.forEach(input => {
      const groupId = input.dataset.groupId;
      const files = input.files;

      if (files.length > 0) {
        if (!uploadData[groupId]) {
          uploadData[groupId] = { audioFiles: [], imageFiles: [] };
        }
        uploadData[groupId].imageFiles.push(...files);
      }
    });

    for (const groupId in uploadData) {
      const formDataAudio = new FormData();
      formDataAudio.append("QuestionGroupId", groupId);

      uploadData[groupId].audioFiles.forEach(file => {
        formDataAudio.append("Files", file);
      });

      const audioResponse = await fetch(audioApiEndpoint, {
        method: "POST",
        body: formDataAudio,
      });

      if (!audioResponse.ok) {
        console.error(`Failed to upload audio for Group ID: ${groupId}`, await audioResponse.text());
      } else {
        console.log(`Uploaded audio successfully for Group ID: ${groupId}`);
      }

      const formDataImage = new FormData();
      formDataImage.append("QuestionGroupId", groupId);

      uploadData[groupId].imageFiles.forEach(file => {
        formDataImage.append("Files", file);
      });

      const imageResponse = await fetch(imageApiEndpoint, {
        method: "POST",
        body: formDataImage,
      });

      if (!imageResponse.ok) {
        console.error(`Failed to upload image for Group ID: ${groupId}`, await imageResponse.text());
      } else {
        console.log(`Uploaded image successfully for Group ID: ${groupId}`);
      }

      window.history.back();
      
    }
  } catch (error) {
    console.error("Error uploading files:", error);
    saveLoading.classList.add('d-none');
    saveIcon.classList.remove('d-none');
  } finally {
    saveLoading.classList.add('d-none');
    saveIcon.classList.remove('d-none');
  }
}

function renderUploadMediaSection(group) {
  const uploadImageList = document.getElementById('upload-image-list');
  const uploadAudioList = document.getElementById('upload-audio-list');

  const fragmentImage = document.createDocumentFragment();
  const fragmentAudio = document.createDocumentFragment();

  group.questions.forEach((questionId, index) => {
    console.log(questionId);
    const questionImageElement = document.createElement('div');
    questionImageElement.id = `upload-question-image-${questionId}`;
    questionImageElement.className = 'pb-2';

    const questionAudioElement = document.createElement('div');
    questionAudioElement.id = `upload-question-audio-${questionId}`;
    questionAudioElement.className = 'pb-2';

    let imageUploadField = '';
    let audioUploadField = '';

    if (group.questions.length > 1) {
      if (group.media === 'image' || group.media === 'both') {
        imageUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text">Question ${group.questions.join('-')}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;">
          </label>`:
          `
      `;
      } else {
        imageUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through">Question ${group.questions.join('-')}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;" disabled>
          </label>` :
          ``;
      }
    } else {
      if (group.media === 'image' || group.media === 'both') {
        imageUploadField =
          `<label class="form-label">
            <span class="label-text">Question ${questionId}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;">
          </label>
      `;
      } else {
        imageUploadField =
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through">Question ${questionId}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;" disabled>
          </label>`;
      }
    }

    if (group.questions.length > 1) {
      if (group.media === 'audio' || group.media === 'both') {
        audioUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text"> Question ${group.questions.join('-')}</span>
            <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;">
          </label>`:
          `
      `;
      } else {
        audioUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through"> Question ${group.questions.join('-')}</span>
            <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;" disabled>
          </label>` :
          ``;
      }
    } else {
      if (group.media === 'audio' || group.media === 'both') {
        audioUploadField = `
        <label class="form-label">
          <span class="label-text"> Question ${questionId}</span>
          <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;">
        </label>
      `;
      } else {
        audioUploadField = `
        <label class="form-label">
          <span class="label-text" style="text-decoration: line-through"> Question ${questionId}</span>
          <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${group.groupId}-${questionId}" 
            data-group-id="${group.groupId}"  style="margin-top: 0.5rem;" disabled>
        </label>
      `;
      }
    }

    questionImageElement.innerHTML = imageUploadField;
    questionAudioElement.innerHTML = audioUploadField;

    fragmentImage.appendChild(questionImageElement);
    fragmentAudio.appendChild(questionAudioElement);
  });

  uploadImageList.appendChild(fragmentImage);
  uploadAudioList.appendChild(fragmentAudio);
}

document.getElementById('edit-question-button').addEventListener('click', handleEditQuestion);

document.getElementById("next-page-button").addEventListener('click', handleMoveToNextPage);

document.getElementById("previous-page-button").addEventListener('click', handleMoveToPreviousPage)

document.getElementById("save-question-button").addEventListener("click", handleSaveMediaFile);
