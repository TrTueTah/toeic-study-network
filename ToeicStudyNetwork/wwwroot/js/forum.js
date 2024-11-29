document.querySelectorAll(".media-action-button").forEach(function (button) {
    button.addEventListener("click", function () {
        const icon = this.querySelector("i");
        const totalText = this.nextElementSibling;

        if (icon.classList.contains("bi-heart")) {
            icon.classList.remove("bi-heart");
            icon.classList.add("bi-heart-fill");
            this.classList.add("liked");
            totalText.classList.add("liked");
        } else {
            icon.classList.remove("bi-heart-fill");
            icon.classList.add("bi-heart");
            this.classList.remove("liked");
            totalText.classList.remove("liked");
        }
    });
});


document.getElementById("upload-image-button").addEventListener("click", function () {
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";
    fileInput.click();

    fileInput.addEventListener("change", function () {
        const file = fileInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const img = document.createElement("img");
                img.src = e.target.result;
                const imageHolder = document.getElementById("image-holder");
                img.style.width = "60px";
                img.style.height = "60px";
                img.style.border = "1px solid #ccc";
                img.style.position = "relative";
                img.style.display = "inline-block";
                img.style.margin = "5px";
                img.style.borderRadius = "5px";

                const cancelIcon = document.createElement("span");
                cancelIcon.innerHTML = "&times;";
                cancelIcon.style.position = "absolute";
                cancelIcon.style.top = "0";
                cancelIcon.style.left = "0";
                cancelIcon.style.background = "#fff";
                cancelIcon.style.border = "1px solid #ccc";
                cancelIcon.style.borderRadius = "50%";
                cancelIcon.style.cursor = "pointer";
                
                cancelIcon.style.fontSize = "12px";
                cancelIcon.style.width = "16px";
                cancelIcon.style.height = "16px";
                cancelIcon.style.lineHeight = "16px";
                cancelIcon.style.textAlign = "center";

                cancelIcon.addEventListener("click", function () {
                    imageHolder.removeChild(imgContainer);
                });

                const imgContainer = document.createElement("div");
                imgContainer.style.position = "relative";
                imgContainer.style.display = "inline-block";
                imgContainer.appendChild(img);
                imgContainer.appendChild(cancelIcon);

                imageHolder.appendChild(imgContainer);
            };
            reader.readAsDataURL(file);

            // Append the file to the form data
            formData.append("MediaFiles", file);
        }
    });
});

// Handle form submission
document.getElementById("postForm").addEventListener("submit", function (e) {
    e.preventDefault();

    const formData = window.postFormData || new FormData(this);

    fetch(this.action, {
        method: this.method,
        body: formData,
    })
    .then(response => response.json())
    .then(data => {
        // Handle the response data
        console.log(data);
    })
    .catch(error => {
        console.error("Error:", error);
    });
});