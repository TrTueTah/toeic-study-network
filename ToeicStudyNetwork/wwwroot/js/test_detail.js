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

function checkPartSelection() {
  const partCheckboxes = document.querySelectorAll('#practice-tab input[name="partNumbers"]');
  const startButton = document.querySelector('#practice-tab button[type="submit"]');
  let isPartSelected = false;

  for (let i = 0; i < partCheckboxes.length; i++) {
    if (partCheckboxes[i].checked) {
      isPartSelected = true;
      break;
    }
  }

  startButton.disabled = !isPartSelected; 
}

document.addEventListener('DOMContentLoaded', function () {
  checkPartSelection(); 

  const partCheckboxes = document.querySelectorAll('#practice-tab input[name="partNumbers"]');
  partCheckboxes.forEach(checkbox => {
    checkbox.addEventListener('change', checkPartSelection);
  });

  document.getElementById('practice-tab-button').addEventListener('click', checkPartSelection);
  document.getElementById('full-test-tab-button').addEventListener('click', checkPartSelection);
});
