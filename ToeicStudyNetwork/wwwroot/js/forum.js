document.addEventListener('DOMContentLoaded', function () {
  document.getElementById("post-container").addEventListener("click", async (event) => {
    const target = event.target;

    if (target.closest("#like-button")) {
      const button = target.closest("#like-button");
      const postId = button.getAttribute("data-post-id");

      if (!postId) {
        console.error("Missing postId");
        return;
      }

      const payload = { postId };

      try {
        const response = await fetch("Forum/ToggleLike", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(payload)
        });

        if (!response.ok) {
          console.error("Error toggling like");
          return;
        }

        const result = await response.json();
        console.log("Successfully toggled like:", result);

        const mediaActionButton = button.querySelector(".media-action-button");
        const likeIcon = button.querySelector("#likeIcon");
        const likeCountElement = button.querySelector(".total-text");
        mediaActionButton.classList.toggle("liked");
        likeIcon.classList.toggle("bi-heart");
        likeIcon.classList.toggle("bi-heart-fill");

        if (result.isLiked) {
          likeCountElement.textContent = parseInt(likeCountElement.textContent) + 1;
        } else {
          likeCountElement.textContent = parseInt(likeCountElement.textContent) - 1;
        }
      } catch (error) {
        console.error("Network or server error:", error);
      }
    }

    if (target.closest("#comment-button")) {
      const button = target.closest("#comment-button");
      const postId = button.getAttribute("data-post-id");
      const url = `${button.getAttribute("data-url")}`.replace("__postId__", postId);
      const currentUrl = window.location.pathname;
      const modal = document.getElementById(`commentModal-${postId}`);

      if (modal) {
        if (url === currentUrl) {
          const commentModal = new bootstrap.Modal(modal);
          commentModal.show();
        } else {
          window.location.href = url;
        }
      } else {
        console.error(`Modal with ID commentModal-${postId} not found.`);
      }
    }

    if (target.matches(".media-image img, .comment-image img")) {
      const modalImage = document.getElementById("modalImage");
      const postId = modalImage.getAttribute("data-post-id");
      modalImage.src = target.src;
      const imageModal = new bootstrap.Modal(document.getElementById(`imageModal-${postId}`));
      imageModal.show();
    }
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

  document.getElementById("postForm").addEventListener("submit", async function (event) {
    event.preventDefault()

    const form = event.target;
    const formData = new FormData(form);

    for (const file of selectedFiles) {
      formData.append("files", file);
    }

    const content = document.getElementById("postContent").value;
    formData.append("content", content);

    try {
      const response = await fetch("/Forum/CreatePost", {
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

  document.querySelectorAll("#media-caption").forEach(content => {
    content.addEventListener("click", function () {
      var url = `${this.getAttribute('data-url')}`
      window.location.href = url;
    })
  })
})
