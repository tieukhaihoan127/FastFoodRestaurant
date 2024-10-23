const imageShowing = document.querySelector('.image-preview');

if (imageShowing) {
    if (imageShowing.src.endsWith('.jpg')) {
        imageShowing.style.display = 'block';
    } else {
        imageShowing.style.display = 'none';
    }
}


if (document.getElementById('fileInput')) {
    document.getElementById('fileInput').addEventListener('change', function (event) {
        const fileName = this.value.split('\\').pop();
        const inputField = document.querySelector('.file-upload-info');
        const file = event.target.files[0];

        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imagePreview = document.querySelector('.image-preview');
                imagePreview.src = e.target.result;
                imagePreview.style.display = 'block';
            };
            reader.readAsDataURL(file);
        }

        inputField.value = fileName;

    });
}

document.querySelectorAll('#delete').forEach(button => {
    button.addEventListener('click', (e) => {
        e.preventDefault();
        const id = button.getAttribute('data-id');
        const currentUrl = new URL(window.location.href);
        const url = currentUrl.pathname + '/Delete/' + id;

        Swal.fire({
            title: "Xóa danh mục hiện tại?",
            text: "Hãy chắc chắn về lựa chọn trước khi xóa!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Xác nhận xóa!"
        }).then((result) => {
            if (result.isConfirmed) {
                const form = document.createElement('form');
                form.method = 'post';
                form.action = url; 
                const hiddenField = document.createElement('input');
                hiddenField.type = 'hidden';
                hiddenField.name = 'id';
                hiddenField.value = id;

                form.appendChild(hiddenField);
                document.body.appendChild(form);

                fetch(url, {
                    method: 'POST',
                    body: new URLSearchParams(new FormData(form))
                })
                    .then(response => {
                        if (response.ok) {
                            Swal.fire({
                                title: "Đã xóa!",
                                text: "Xóa danh mục hiện tại thành công!",
                                icon: "success"
                            }).then(() => {
                                window.location.href = `${currentUrl.pathname}/Index`;
                            });
                        } else {
                            Swal.fire({
                                title: "Không thể xóa!",
                                text: "Danh mục này có thể đang được sử dụng ở nơi khác.",
                                icon: "error"
                            }).then(() => {
                                window.location.href = `${currentUrl.pathname}/Index`;
                            });
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        Swal.fire({
                            title: "Có lỗi xảy ra!",
                            text: "Vui lòng thử lại sau.",
                            icon: "error"
                        });
                    });
            }
        });
    });
});

// Checkbox Multi
const table = document.querySelector(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table");

if (table) {
    const inputCheckAll = table.querySelector("input[name='checkboxInputAll']");
    const inputsId = table.querySelectorAll("input[name='checkboxInputId']");

    inputCheckAll.addEventListener("click", () => {
        if (inputCheckAll.checked) {
            inputsId.forEach((input) => {
                input.checked = true;
            });
        }
        else {
            inputsId.forEach((input) => {
                input.checked = false;
            });
        }
    });

    inputsId.forEach(inputs => {
        inputs.addEventListener("click", () => {
            const countChecked = table.querySelectorAll("input[name='checkboxInputId']:checked").length;
            if (countChecked == inputsId.length) {
                inputCheckAll.checked = true;
            }
            else {
                inputCheckAll.checked = false;
            }
        });
    });
}
// End Checkbox Multi

// Delete Multi
const deleteMultiButton = document.querySelector(".main-panel .content-wrapper .home-tab .filter .delete-multi");
if (deleteMultiButton) {
    deleteMultiButton.addEventListener("click", (e) => {
        e.preventDefault();

        const table = document.querySelector(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table");
        const inputsId = table.querySelectorAll("input[name='checkboxInputId']:checked");
        const currentUrl = new URL(window.location.href);
        const url = currentUrl.pathname + '/DeleteMulti/';

        if (inputsId.length > 0) {
            const idArr = [];

            inputsId.forEach(item => {
                const id = item.getAttribute("data-id");
                idArr.push(id);
            });

            Swal.fire({
                title: "Xóa nhiều các danh mục hiện tại?",
                text: "Hãy chắc chắn về lựa chọn trước khi xóa!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Xác nhận xóa!"
            }).then((result) => {
                if (result.isConfirmed) {
                    const form = document.createElement('form');
                    form.method = 'post';
                    form.action = url;

                    const hiddenField = document.createElement('input');
                    hiddenField.type = 'hidden';
                    hiddenField.name = 'idArr'; 
                    hiddenField.value = idArr; 

                    form.appendChild(hiddenField);
                    document.body.appendChild(form);

                    fetch(url, {
                        method: 'POST',
                        body: new FormData(form)
                    })
                        .then(response => {
                            if (response.ok) {
                                Swal.fire({
                                    title: "Đã xóa!",
                                    text: "Xóa danh mục hiện tại thành công!",
                                    icon: "success"
                                }).then(() => {
                                    window.location.href = `${currentUrl.pathname}/Index`;
                                });
                            } else {
                                Swal.fire({
                                    title: "Không thể xóa!",
                                    text: "Danh mục này có thể đang được sử dụng ở nơi khác.",
                                    icon: "error"
                                }).then(() => {
                                    window.location.href = `${currentUrl.pathname}/Index`;
                                });
                            }
                        })
                        .catch(error => {
                            console.error('Error:', error);
                            Swal.fire({
                                title: "Có lỗi xảy ra!",
                                text: "Vui lòng thử lại sau.",
                                icon: "error"
                            });
                        });
                }
            });
        } else {
            Swal.fire({
                title: "Không có danh mục nào được chọn!",
                text: "Vui lòng chọn ít nhất một danh mục để xóa.",
                icon: "warning"
            });
        }
    });
}
// End Delete Multi

