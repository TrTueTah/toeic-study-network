const usernameWrapper = document.getElementById("username-wrapper");
const usernameElement = document.getElementById("username");
const changeNameButton = document.getElementById("change-name-button");
const changeImageButton = document.getElementById("change-image-button");

changeNameButton.addEventListener("click", () => {
  const currentUsername = usernameElement.textContent;

  usernameWrapper.classList.add("username-wrapper-column");
  usernameWrapper.innerHTML = `    
            <input type="text" id="username-input" class="form-control me-2" value="${currentUsername}">
            <div class="d-flex justify-content-between mt-2 w-100">
                <a class="btn btn-success btn-sm me-2" id="confirm-button">Xác nhận</a>
                <a class="btn btn-danger btn-sm" id="cancel-button">Huỷ</a>
            </div>
        `;

  const confirmButton = document.getElementById("confirm-button");
  confirmButton.addEventListener("click", () => {
    const newUsername = document.getElementById("username-input").value;
    const formData = new FormData();
    formData.append("username", newUsername);
    fetch('/Personal/UpdateUserInfor', {
      method: 'POST',
      body: formData
    })
      .then(response => {
        if (response.ok) {
          alert("Tên người dùng đã được cập nhật thành công!");

          usernameWrapper.classList.add("flex-row");
          usernameWrapper.classList.add("justify-content-center");
          usernameWrapper.classList.remove("flex-column");

          usernameWrapper.innerHTML = `
                        <h2 class="mt-4 text-black fw-bold" id="username">${newUsername}</h2>
                        <a class="hover-pointer justify-content-between align-items-center ms-2 mt-3">
                            <i class="bi bi-pencil" id="change-name-button"></i>
                        </a>
                    `;

          document.getElementById("change-name-button").addEventListener("click", () => {
            changeNameButton.click();
          });
        } else {
          alert("Cập nhật thất bại. Vui lòng thử lại.");
        }
      })
      .catch(error => {
        console.error("Lỗi:", error);
        alert("Đã xảy ra lỗi khi cập nhật tên người dùng.");
      });
  });

  const cancelButton = document.getElementById("cancel-button");
  cancelButton.addEventListener("click", () => {
    usernameWrapper.classList.add("flex-row");
    usernameWrapper.classList.add("justify-content-center");
    usernameWrapper.classList.remove("flex-column");
    usernameWrapper.innerHTML = `
                <h2 class="mt-4 text-black fw-bold" id="username">${currentUsername}</h2>
                <a class="hover-pointer justify-content-between align-items-center ms-2 mt-3">
                    <i class="bi bi-pencil" id="change-name-button"></i>
                </a>
            `;

    document.getElementById("change-name-button").addEventListener("click", () => {
      changeNameButton.click();
    });
  });
});

changeImageButton.addEventListener("click", () => {
    console.log("Button clicked");
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";
    fileInput.click();
    fileInput.addEventListener("change", () => {
      const file = fileInput.files[0];
      if (file) {

        const formData = new FormData();
        formData.append("imageFile", file);

        fetch('/Personal/UpdateUserInfor', {
          method: 'POST',
          body: formData
        })
          .then(response => {
            if (response.ok) {
              alert("Ảnh đại diện đã được cập nhật thành công!");
              // Optionally, update the displayed image here
            } else {
              alert("Cập nhật ảnh đại diện thất bại. Vui lòng thử lại.");
            }
          })
          .catch(error => {
            console.error("Lỗi:", error);
            alert("Đã xảy ra lỗi khi cập nhật ảnh đại diện.");
          });
      }
    });
});
