const selectedFiles = new Set();

document.addEventListener('DOMContentLoaded', function () {
  let isLoading = false;
  let pageIndex = 1;

  const loadPosts = async () => {
    if (isLoading) return;

    isLoading = true;
    document.getElementById("loader").style.display = "block";

    try {
      const response = await fetch(`/Forum/liked?pageIndex=${pageIndex}&pageSize=5`, {
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
})
