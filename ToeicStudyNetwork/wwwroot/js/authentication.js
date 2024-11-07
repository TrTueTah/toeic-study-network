function saveLoginInfoToCookies(token) {
    if (token) {
        document.cookie = "token=" + encodeURIComponent(token) + "; path=/";
        alert("Token saved to cookies.");
    } else {
        alert("Failed to save Token to cookies.");
    }
}

function sendLoginInfoToBackend(email, password) {
    fetch("http://localhost:5112/api/account/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ email: email, password: password }),
    })
        .then((response) => response.json())
        .then((data) => {
            alert("Login successful");
            saveLoginInfoToCookies(data.token);
            window.location.href = "/Home";
            // Redirect or perform other actions on successful login
        })
        .catch((error) => {
            console.error("Error:", error);
            alert("An error occurred while logging in.");
        });
}

// Example usage: Call this function when the user submits the login form
document
    .querySelector('button[type="button"]')
    .addEventListener("click", function () {
        var email = document.getElementById("form2Example11").value;
        var password = document.getElementById("form2Example22").value;
        sendLoginInfoToBackend(email, password);
    });
