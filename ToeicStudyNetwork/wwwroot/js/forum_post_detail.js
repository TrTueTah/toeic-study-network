const selectedFiles = new Set();


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
        const response = await fetch("/Forum/ToggleLike", {
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

    if (target.matches(".media-action")) {
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
})

document.addEventListener('DOMContentLoaded', function () {
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
