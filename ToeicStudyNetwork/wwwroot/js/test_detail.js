const practiceTabs = document.querySelectorAll("#practice-tabs .nav-link");
practiceTabs.forEach((tab) => {
	tab.addEventListener("click", (e) => {
		practiceTabs.forEach((item) => item.classList.remove("active")); 
		e.target.classList.add("active");
	});
});

const examInfoTabs = document.querySelectorAll("#exam-info-tabs .nav-link");
examInfoTabs.forEach((tab) => {
	tab.addEventListener("click", (e) => {
		examInfoTabs.forEach((item) => item.classList.remove("active"));
		e.target.classList.add("active");
	});
});
