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

function renderUploadMediaSection(group) {
  const uploadImageList = document.getElementById('upload-image-list');
  const uploadAudioList = document.getElementById('upload-audio-list');

  const fragmentImage = document.createDocumentFragment();
  const fragmentAudio = document.createDocumentFragment();

  group.questions.forEach((questionId, index) => {
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
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${questionId}" style="margin-top: 0.5rem;">
          </label>`:
          `
      `;
      } else {
        imageUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through">Question ${group.questions.join('-')}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${questionId}" style="margin-top: 0.5rem;" disabled>
          </label>` :
          ``;
      }
    } else {
      if (group.media === 'image' || group.media === 'both') {
        imageUploadField =
          `<label class="form-label">
            <span class="label-text">Question ${questionId}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${questionId}" style="margin-top: 0.5rem;">
          </label>
      `;
      } else {
        imageUploadField =
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through">Question ${questionId}</span>
            <input type="file" accept="image/*" class="form-input" id="test-image-upload-${questionId}" style="margin-top: 0.5rem;" disabled>
          </label>`;
      }
    }

    if (group.questions.length > 1) {
      if (group.media === 'audio' || group.media === 'both') {
        audioUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text"> Question ${group.questions.join('-')}</span>
            <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${questionId}" style="margin-top: 0.5rem;">
          </label>`:
          `
      `;
      } else {
        audioUploadField = index === 0 ?
          `<label class="form-label">
            <span class="label-text" style="text-decoration: line-through"> Question ${group.questions.join('-')}</span>
            <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${questionId}" style="margin-top: 0.5rem;" disabled>
          </label>` :
          ``;
      }
    } else {
      if (group.media === 'audio' || group.media === 'both') {
        audioUploadField = `
        <label class="form-label">
          <span class="label-text"> Question ${questionId}</span>
          <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${questionId}" style="margin-top: 0.5rem;">
        </label>
      `;
      } else {
        audioUploadField = `
        <label class="form-label">
          <span class="label-text" style="text-decoration: line-through"> Question ${questionId}</span>
          <input type="file" accept="audio/*" class="form-input" id="test-audio-upload-${questionId}" style="margin-top: 0.5rem;" disabled>
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

function handleFileUpload(event, questionId, fileType) {
  const file = event.target.files[0];
  if (file) {
    console.log(`Uploaded ${fileType} for Question ${questionId}:`, file);
  }
}

document.addEventListener('DOMContentLoaded', function () {
  questionGroups.forEach(group => {
    renderUploadMediaSection(group);
    group.questions.forEach((questionId, index) => {
      if (index === 0) {
        const imageUploadElement = document.getElementById(`test-image-upload-${questionId}`);
        const audioUploadElement = document.getElementById(`test-audio-upload-${questionId}`);

        if (imageUploadElement) {
          imageUploadElement.addEventListener('change', function (event) {
            handleFileUpload(event, questionId, 'image');
          });
        } else {
          console.error(`Element test-image-upload-${questionId} not found.`);
        }

        if (audioUploadElement) {
          audioUploadElement.addEventListener('change', function (event) {
            handleFileUpload(event, questionId, 'audio');
          });
        } else {
          console.error(`Element test-audio-upload-${questionId} not found.`);
        }
      }

      const questionListItem = `
        <span class="test-questions-listitem" data-qid="${questionId}" id="test-questions-lisitem-${questionId}">${questionId}</span>
      `;
      document.querySelector('.test-questions-list-wrapper').insertAdjacentHTML('beforeend', questionListItem);
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
