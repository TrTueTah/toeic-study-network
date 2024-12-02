// A Set to store selected files (store File objects instead of names)
const selectedFiles = new Set();

// Handle image selection and display preview
document.getElementById("upload-image-button").addEventListener("click", function () {
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";
    fileInput.multiple = true; // Allow multiple file uploads
    fileInput.click();

    fileInput.addEventListener("change", function () {
        const files = fileInput.files;
        const imageHolder = document.getElementById("image-holder");

        for (const file of files) {
            // Skip files that are already in the Set
            if ([...selectedFiles].some((f) => f.name === file.name)) continue;
            selectedFiles.add(file);

            const reader = new FileReader();
            reader.onload = function (e) {
                const img = document.createElement("img");
                img.src = e.target.result;
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

                const imgContainer = document.createElement("div");
                imgContainer.style.position = "relative";
                imgContainer.style.display = "inline-block";
                imgContainer.appendChild(img);
                imgContainer.appendChild(cancelIcon);

                // Remove the image and its reference from the Set
                cancelIcon.addEventListener("click", function () {
                    imageHolder.removeChild(imgContainer);
                    selectedFiles.delete(file);
                });

                imageHolder.appendChild(imgContainer);
            };
            reader.readAsDataURL(file);
        }
    });
});

// Handle form submission
document.getElementById("postForm").addEventListener("submit", async function (event) {
    event.preventDefault(); // Prevent default form submission

    const form = event.target;
    const formData = new FormData(form);

    // Append all selected files to FormData
    for (const file of selectedFiles) {
        formData.append("files", file);
    }

    // Append the post content
    const content = document.getElementById("postContent").value;
    formData.append("content", content);

    // Debug FormData (optional)
    for (let [key, value] of formData.entries()) {
        console.log(`${key}:`, value);
    }

    try {
        const response = await fetch(form.action, {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            alert("Bài viết đã được đăng thành công!");
            window.location.reload();
        } else {
            const error = await response.text();
            console.error("Lỗi khi đăng bài viết:", error);
            alert("Không thể đăng bài viết.");
        }
    } catch (error) {
        console.error("Lỗi kết nối:", error);
        alert("Có lỗi xảy ra trong khi đăng bài.");
    }
});
