const selectedFiles = new Set();

document.addEventListener('DOMContentLoaded', function () {

  let isLoading = false;
  let pageIndex = 1;

  const loadPosts = async () => {
    if (isLoading) return;

    isLoading = true;
    document.getElementById("loader").style.display = "block";

    try {
      const response = await fetch(`/Forum?pageIndex=${pageIndex}&pageSize=5`, {
        method: "GET",
        headers: {
          'X-Requested-With': 'XMLHttpRequest',
        },
      });
      if (!response.ok) throw new Error("Failed to load posts");

      const posts = await response.text();
      if (!posts.trim()) {
        window.removeEventListener("scroll", onScroll);
        document.getElementById("loader").style.display = "none";
        return;
      }

      document.getElementById("post-container").insertAdjacentHTML("beforeend", posts);
      pageIndex++;
    } catch (error) {
      console.error(error);
    } finally {
      isLoading = false;
      document.getElementById("loader").style.display = "none";
    }
  };

  const onScroll = () => {
    const { scrollTop, scrollHeight, clientHeight } = document.documentElement;
    if (scrollTop + clientHeight >= scrollHeight - 50) {
      loadPosts();
    }
  };

  window.addEventListener("scroll", onScroll);
  loadPosts();

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

  document.querySelectorAll("#comment-button").forEach(button => {
    button.addEventListener("click", function () {
      const postId = this.getAttribute("data-post-id");
      const url = `${this.getAttribute("data-url")}`.replace("__postId__", postId);
      const currentUrl = window.location.pathname;
      const modal = document.getElementById(`commentModal-${postId}`)

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
    });
  });
})
