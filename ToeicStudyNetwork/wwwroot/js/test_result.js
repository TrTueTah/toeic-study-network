function loadQuestionDetail(questionId) {
  const modalContent = document.getElementById('result-modal-content');
  modalContent.innerHTML = '<p>Loading...</p>'; 

  fetch(`http://localhost:5112/api/v1/result/getQuestionDetailResult/${questionId}`)
    .then(response => {
      if (!response.ok) {
        throw new Error('Failed to fetch question details.');
      }
      return response.json();
    })
    .then(questionGroup => {
      const audioContent = questionGroup.audioFilesUrl && questionGroup.audioFilesUrl.length > 0
        ? `<audio controls>
             <source src="${questionGroup.audioFilesUrl[0]}" type="audio/mp3" />
             Your browser does not support the audio element.
           </audio>`
        : '';

      const imageContent = questionGroup.imageFilesUrl && questionGroup.imageFilesUrl.length > 0
        ? `<div class="context-content context-image">
             <img class="lazyel entered loaded" src="${questionGroup.imageFilesUrl[0]}" alt="Question Group Image" />
           </div>`
        : '';

      const answersHtml = ['A', 'B', 'C', 'D'].map(option => {
        const isUserAnswer = questionGroup.userAnswer === option;
        const isCorrectAnswer = questionGroup.correctAnswer === option;
        const answerFeedback = isUserAnswer
          ? `<span>
               <span class="text-answerkey">${questionGroup.correctAnswer}</span>:
               ${questionGroup.isCorrect ? `<i class="mr-1">${questionGroup.userAnswer}</i><span class="text-correct bi bi-check-lg"></span>` : questionGroup.userAnswer == null ? `<i class="mr-1">chưa trả lời</i><span class="text-unanswer bi bi-dash"></span>` : `<i class="text-line-through mr-1">${questionGroup.userAnswer}</i><span class="text-wrong bi bi-x"></span>`}
             </span>`
          : '';
        return `<div class="form-check">
                  <input class="form-check-input" type="radio" name="question-${questionGroup.questionNumber}" id="question-${questionGroup.questionNumber}-${option}" value="${option}" ${isUserAnswer ? 'checked' : ''} disabled />
                  <label class="form-check-label" for="question-${questionGroup.questionNumber}-${option}">
                    ${option}. ${questionGroup[`answer${option}`] || ''}
                  </label>
                  ${answerFeedback}
                </div>`;
      }).join('');

      modalContent.innerHTML = `
        <div class="question-group-wrapper">
          <div class="context-wrapper">
            ${audioContent}
            ${imageContent}
          </div>
          <div class="question-wrapper" data-qid="${questionGroup.questionNumber}" id="question-wrapper-${questionGroup.questionNumber}">
            <div class="question-number" data-qid="${questionGroup.questionNumber}" data-markable="true">
              <strong>${questionGroup.questionNumber}</strong>
            </div>
            <div class="question-content text-highlightable">
              <div class="question-text">
                ${questionGroup.title || ''}
              </div>
              <div class="question-answers">
                ${answersHtml}
              </div>
            </div>
          </div>
        </div>`;
    })
    .catch(error => {
      modalContent.innerHTML = `<p>Error: ${error.message}</p>`;
    });
}
