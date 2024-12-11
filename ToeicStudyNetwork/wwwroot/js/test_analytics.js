function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(';').shift();
  return null;
}

function handleTimeRangeChange() {
  const selectedValue = document.getElementById('timeRangeSelect').value;
  const userId = getCookie('userId');

  document.getElementById("loader").style.display = "block";
  
  fetch(`/Test/Analytics?userId=${userId}&timeRange=${selectedValue}`, {
    headers: {
      "X-Requested-With": "XMLHttpRequest",
    }
  })
    .then(response => {
      if (response.ok) {
        document.getElementById("loader").style.display = "none";
        return response.text(); 
      }
      throw new Error('Failed to fetch analysis data');
    })
    .then(html => {
      document.getElementById('analysis-section').innerHTML = html; 
    })
    .catch(error => console.error('Error updating analysis section:', error));
}

