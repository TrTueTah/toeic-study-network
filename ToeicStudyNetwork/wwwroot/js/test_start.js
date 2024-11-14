document.addEventListener("DOMContentLoaded", function () {
    const testTabs = document.querySelectorAll("#testTabs .nav-link");
    const questionContainer = document.querySelector("#question-container");

    testTabs.forEach((tab) => {
        tab.addEventListener("click", async function (e) {
            e.preventDefault();
            const partId = this.getAttribute("data-bs-target");
            const response = await fetch(`/Test/${partId}/questions`);
            const questionsHtml = await response.text();
            questionContainer.innerHTML = questionsHtml;
        });
    });
});