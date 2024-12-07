function practiceTabActive () {
  const practiceTabButton = document.getElementById("practice-tab-button");
  const fullTestTabButton = document.getElementById("full-test-tab-button");
  const practiceTab = document.getElementById("practice-tab");
  const fullTestTab = document.getElementById("full-test-tab");

  practiceTabButton.classList.add("active");
  fullTestTabButton.classList.remove("active");
  practiceTab.classList.remove("d-none");
  fullTestTab.classList.add("d-none");
}

function fullTestTabActive () {
  const practiceTabButton = document.getElementById("practice-tab-button");
  const fullTestTabButton = document.getElementById("full-test-tab-button");
  const practiceTab = document.getElementById("practice-tab");
  const fullTestTab = document.getElementById("full-test-tab");

  practiceTabButton.classList.remove("active");
  fullTestTabButton.classList.add("active");
  practiceTab.classList.add("d-none");
  fullTestTab.classList.remove("d-none");
}

