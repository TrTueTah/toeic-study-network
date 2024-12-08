const selectedFiles = new Set();

document.addEventListener('DOMContentLoaded', function () {
  document.querySelectorAll(".media-image img, .comment-image img").forEach(img => {
    img.addEventListener("click", function () {
      const modalImage = document.getElementById("modalImage");
      modalImage.src = this.src;
      const imageModal = new bootstrap.Modal(document.getElementById("imageModal"));
      imageModal.show();
    });
  });

  document.getElementById("upload-image-button").addEventListener("click", function () {
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";
    fileInput.multiple = true; 
    fileInput.click();

    fileInput.addEventListener("change", function () {
      const files = fileInput.files;
      const imageHolder = document.getElementById("image-holder");

      for (const file of files) {
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

  document.getElementById("commentForm").addEventListener("submit", async function (event) {
    event.preventDefault()

    const form = event.target;
    const formData = new FormData(form);

    for (const file of selectedFiles) {
      formData.append("files", file);
    }

    const content = document.getElementById("commentContent").value;
    formData.append("content", content);

    const postId = document.getElementById("postId").value;
    formData.append("postId", postId);

    try {
      const response = await fetch("/Forum/CreateComment", {
        method: "POST",
        body: formData,
      });

      if (response.ok) {
        window.location.reload();
      } else {
        const error = await response.text();
        console.error("Lỗi khi đăng bài viết:", error);
      }
    } catch (error) {
      console.error("Lỗi kết nối:", error);
    }
  });
})
