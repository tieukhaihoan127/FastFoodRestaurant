const imageShowing = document.querySelector('.image-preview');

if (imageShowing) {
    if (imageShowing.src.endsWith('.jpg')) {
        imageShowing.style.display = 'block';
    } else {
        imageShowing.style.display = 'none';
    }
}


const searchButton = document.querySelector(".main-panel .content-wrapper .home-tab .filter .search");
if (searchButton) {
    searchButton.addEventListener("click", (e) => {
        e.preventDefault();
        const currentUrl = new URL(window.location.href);
        currentUrl.searchParams.delete('pageNumber');
        const form = document.querySelector('.filter');
        const filterAddress = document.querySelector(".main-panel .content-wrapper .home-tab #filterAddress");
        if (filterAddress) {
            const city = filterAddress.querySelector("#city");
            const district = filterAddress.querySelector("#district");
            const ward = filterAddress.querySelector("#ward");

            if (city) {
                const citySelect = form.querySelector("input[name='city']");
                citySelect.value = city.value;
            }

            if (district) {
                const districtSelect = form.querySelector("input[name='district']");
                districtSelect.value = district.value;
            }

            if (ward) {
                const wardSelect = form.querySelector("input[name='ward']");
                wardSelect.value = ward.value;
            }
        }
       
        form.action = currentUrl.href;
        form.submit();
    });
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
            title: "Xóa đối tượng hiện tại?", 
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
                                text: "Xóa đối tượng hiện tại thành công!",
                                icon: "success"
                            }).then(() => {
                                const allCurrentObjects = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody td input[name='checkboxInputId']");
                                if (allCurrentObjects.length == 1) {
                                    let currentPage = parseInt(currentUrl.searchParams.get('pageNumber'));
                                    currentPage--;
                                    currentUrl.searchParams.set('pageNumber', currentPage);
                                }
                                window.location.href = `${currentUrl.href}`;
                            });
                        } else {
                            Swal.fire({
                                title: "Không thể xóa!",
                                text: "Đối tượng này có thể đang được sử dụng ở nơi khác.",
                                icon: "error"
                            }).then(() => {
                                window.location.href = `${currentUrl.href}`;
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

document.querySelectorAll('#reload').forEach(button => {
    button.addEventListener('click', (e) => {
        e.preventDefault();
        const id = button.getAttribute('data-id');
        const currentUrl = new URL(window.location.href);
        const url = currentUrl.pathname + '/Reload/' + id;
        console.log(url);

        Swal.fire({
            title: "Khôi phục đối tượng hiện tại?",
            text: "Hãy chắc chắn về lựa chọn trước khi khôi phục!",
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
                                title: "Đã khôi phục!",
                                text: "Khôi phục đối tượng hiện tại thành công!",
                                icon: "success"
                            }).then(() => {
                                const allCurrentObjects = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody td input[name='checkboxInputId']");
                                if (allCurrentObjects.length == 1) {
                                    let currentPage = parseInt(currentUrl.searchParams.get('pageNumber'));
                                    currentPage--;
                                    currentUrl.searchParams.set('pageNumber', currentPage);
                                }
                                window.location.href = `${currentUrl.href}`;
                            });
                        } else {
                            Swal.fire({
                                title: "Không thể khôi phục!",
                                text: "Đối tượng này có thể đang được sử dụng ở nơi khác.",
                                icon: "error"
                            }).then(() => {
                                window.location.href = `${currentUrl.href}`;
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

    if (inputCheckAll) {
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
    }

    if (inputsId.length > 0) {
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
}
// End Checkbox Multi

// Delete Multi
const deleteMultiButton = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .delete-multi");
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
                title: "Xóa nhiều các đối tượng hiện tại?",
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
                                    text: "Xóa đối tượng hiện tại thành công!",
                                    icon: "success"
                                }).then(() => {
                                    const allCurrentObjects = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody td input[name='checkboxInputId']");
                                    if (allCurrentObjects.length == 1) {
                                        let currentPage = parseInt(currentUrl.searchParams.get('pageNumber'));
                                        currentPage--;
                                        currentUrl.searchParams.set('pageNumber', currentPage);
                                    }
                                    window.location.href = `${currentUrl.href}`;
                                });
                            } else {
                                Swal.fire({
                                    title: "Không thể xóa!",
                                    text: "Đối tượng này có thể đang được sử dụng ở nơi khác.",
                                    icon: "error"
                                }).then(() => {
                                    window.location.href = `${currentUrl.href}`;
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
                title: "Không có đối tượng nào được chọn!",
                text: "Vui lòng chọn ít nhất một đối tượng để xóa.",
                icon: "warning"
            });
        }
    });
}
// End Delete Multi

// Reload Multi
const reloadMultiButton = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper #reload-multi");
if (reloadMultiButton) {
    reloadMultiButton.addEventListener("click", (e) => {
        e.preventDefault();

        const table = document.querySelector(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table");
        const inputsId = table.querySelectorAll("input[name='checkboxInputId']:checked");
        const currentUrl = new URL(window.location.href);
        const url = currentUrl.pathname + '/ReloadMulti/';

        if (inputsId.length > 0) {
            const idArr = [];

            inputsId.forEach(item => {
                const id = item.getAttribute("data-id");
                idArr.push(id);
            });

            Swal.fire({
                title: "Khôi phục nhiều đối tượng hiện tại ?",
                text: "Hãy chắc chắn về lựa chọn trước khi khôi phục!",
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
                                    title: "Đã khôi phục!",
                                    text: "Khôi phục đối tượng hiện tại thành công!",
                                    icon: "success"
                                }).then(() => {
                                    const allCurrentObjects = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody td input[name='checkboxInputId']");
                                    if (allCurrentObjects.length == 1) {
                                        let currentPage = parseInt(currentUrl.searchParams.get('pageNumber'));
                                        currentPage--;
                                        currentUrl.searchParams.set('pageNumber', currentPage);
                                    }
                                    window.location.href = `${currentUrl.href}`;
                                });
                            } else {
                                Swal.fire({
                                    title: "Không thể khôi phục!",
                                    text: "Đối tượng này có thể đang được sử dụng ở nơi khác.",
                                    icon: "error"
                                }).then(() => {
                                    window.location.href = `${currentUrl.href}`;
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
                title: "Không có đối tượng nào được chọn!",
                text: "Vui lòng chọn ít nhất một đối tượng để xóa.",
                icon: "warning"
            });
        }
    });
}
// End Reload Multi

// Redirect Back
const cancelButton = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample #cancel");
if (cancelButton) {

    cancelButton.addEventListener("click", () => {
        window.history.back();
    });
}

// Select Category
const selectCategory = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .select #dropdown");
if (selectCategory) {
    selectCategory.addEventListener("change", (e) => {
        const currentUrl = new URL(window.location.href);
        const form = document.querySelector('.select');
        form.action = currentUrl.href;
        form.submit();
    });
}

const selectCategory2 = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .select #dropdown2");
if (selectCategory2) {
    selectCategory2.addEventListener("change", (e) => {
        const currentUrl = new URL(window.location.href);
        const form = document.querySelector('#form-status');
        form.action = currentUrl.href;
        form.submit();
    });
}

// Lock Account
const lockButton = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody tr td #lock");
if (lockButton.length > 0) {
    lockButton.forEach(item => {

        item.addEventListener("click", () => {
            const id = item.getAttribute('data-id');
            const currentUrl = new URL(window.location.href);
            const url = currentUrl.pathname + '/ChangeStatus/' + id;

            Swal.fire({
                title: "Bạn có muốn thay đổi trạng thái hiện tại của tài khoản?",
                text: "Hãy chắc chắn về lựa chọn trước khi xóa!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Xác nhận!"
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
                                    title: "Đã thay đổi trạng thái tài khoản!",
                                    text: "Cập nhật trạng thái thành công!",
                                    icon: "success"
                                }).then(() => {
                                    const allCurrentObjects = document.querySelectorAll(".main-panel .content-wrapper .home-tab .table-sorter-wrapper table tbody td input[name='checkboxInputId']");
                                    if (allCurrentObjects.length == 1) {
                                        let currentPage = parseInt(currentUrl.searchParams.get('pageNumber'));
                                        currentPage--;
                                        currentUrl.searchParams.set('pageNumber', currentPage);
                                    }
                                    window.location.href = `${currentUrl.href}`;
                                });
                            } else {
                                Swal.fire({
                                    title: "Không thể thay đổi trạng thái tài khoản!",
                                    text: "Trạng thái tài khoản cập nhật không thành công",
                                    icon: "error"
                                }).then(() => {
                                    window.location.href = `${currentUrl.href}`;
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
}

// Select Menu
const menuDropdown = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select #menu");
let total = 0;

if (menuDropdown) {
    menuDropdown.addEventListener("change", (e) => {
        e.preventDefault();
        const id = e.target.value;
        const price = parseInt(e.target.options[e.target.selectedIndex].getAttribute('price'));
        const name = e.target.options[e.target.selectedIndex].getAttribute('data-name');
        const table = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body table tbody");

        let isExist = false;

        let tableContents = document.querySelectorAll(".main-panel .content-wrapper .home-tab .card .card-body table tbody tr");

        tableContents.forEach(item => {
            const idTable = item.querySelector("td input[name='MenuId']").value;
            if (idTable === id) {
                const quantityInput = item.querySelector("td input[name='Quantity']");
                const totalPriceInput = item.querySelector("td input[name='TotalPrice']");
                let quantity = parseInt(quantityInput.value);
                let totalPrice = parseInt(totalPriceInput.value);

                quantity += 1;
                totalPrice = quantity * price;

                quantityInput.value = quantity;
                totalPriceInput.value = totalPrice;
                item.querySelectorAll("td")[3].innerText = totalPrice;

                total += price;
                isExist = true;
            }
        });

        if (!isExist) {
            total += price;
            const newRow = `
                <tr>
                    <td>
                        <input type="hidden" name="MenuId" value="${id}" />
                        ${id}
                    </td>
                    <td>
                        <input type="hidden" name="Name" value="${name}" />
                        ${name}
                    </td>
                    <td>
                        <input type="number" name="Quantity" data-id="${id}" min="1" value="1" style="border:none; background-color: transparent; width:20%" />
                    </td>
                    <td>
                        <input type="hidden" name="TotalPrice" value="${price}" default-value="${price}"/>
                        <span>${price}</span>
                    </td>
                    <td>
                        <div class="delete" data-id="${id}">
                            <i class="fa-solid fa-trash"></i>
                        </div>
                    </td>
                </tr>`;
            table.insertAdjacentHTML("beforeend", newRow);
        }

        updateTotal();
        bindDeleteEvents();

        tableContents = document.querySelectorAll(".main-panel .content-wrapper .home-tab .card .card-body table tbody tr");

        const inputNumbers = table.querySelectorAll("tr td input[name='Quantity']");
        inputNumbers.forEach(input => {
            input.addEventListener("change", (e) => {
                const dataId = e.target.getAttribute('data-id');
                const newQuantity = parseInt(e.target.value);

                tableContents.forEach(item => {
                    const idTable = item.querySelector("td input[name='MenuId']").value;
                    if (idTable === dataId) {
                        const totalPriceInput = item.querySelector("td input[name='TotalPrice']");
                        const totalPriceDisplay = item.querySelector("td span");

                        const oldTotalPrice = parseInt(totalPriceInput.value);
                        const defaultPrice = totalPriceInput.getAttribute("default-value");
                        const newTotalPrice = newQuantity * defaultPrice;

                        total = total - oldTotalPrice + newTotalPrice;
                        totalPriceInput.value = newTotalPrice;
                        totalPriceDisplay.innerText = newTotalPrice;

                        updateTotal();
                    }
                });
            });
        });
    });

    const updateTotal = () => {
        const totalInputPrice = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .table-sorter-wrapper .total-price");
        const totalInput = document.querySelector(".form-group .input-group #total");
        totalInput.value = total;
        totalInputPrice.innerHTML = `<b>Tổng tiền: ${total} đ</b>`;
    };

    const bindDeleteEvents = () => {
        const deleteButtons = document.querySelectorAll(".main-panel .content-wrapper .home-tab .card .card-body table tbody tr td .delete");

        deleteButtons.forEach((deleteBtn) => {
            deleteBtn.removeEventListener("click", handleDeleteRow);
            deleteBtn.addEventListener("click", handleDeleteRow);
        });
    };

    const handleDeleteRow = (e) => {
        const row = e.target.closest("tr");
        const totalPriceInput = row.querySelector("td input[name='TotalPrice']");
        const rowTotalPrice = parseInt(totalPriceInput.value);

        total -= rowTotalPrice; 
        updateTotal();
        row.remove(); 
    };

    const submitButton = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body #submit");
    submitButton.addEventListener("click", () => {
        const currentUrl = new URL(window.location.href);
        const url = currentUrl.pathname + '/them';

        const idArr = [];

        tableContents = document.querySelectorAll(".main-panel .content-wrapper .home-tab .card .card-body table tbody tr");

        tableContents.forEach(item => {
            const allTD = item.querySelectorAll("td");
            const object =
            {
                MenuId: "",
                Name: "",
                Quantity: 0,
                Price: 0
            }
            const id = allTD[0].querySelector("input").value;
            const name = allTD[1].querySelector("input").value;
            const quantity = allTD[2].querySelector("input").value;
            const price = allTD[3].querySelector("input").value;
            object.MenuId = id;
            object.Name = name;
            object.Quantity = parseInt(quantity);
            object.Price = parseFloat(price);
            idArr.push(object);
        });


        const form = document.createElement('form');
        form.method = 'post';
        form.action = url;

        const hiddenField = document.createElement('input');
        hiddenField.type = 'hidden';
        hiddenField.name = 'idArr';
        hiddenField.value = idArr;

        form.appendChild(hiddenField);
        document.body.appendChild(form);


    });
}

// Add To Cart
const cartButton = document.querySelector('.cart-button');
if (cartButton) {
    document.querySelector('.cart-button').addEventListener('click', function () {
        document.getElementById('cartSubmit').submit();
    });
}

// Refresh Page
const refreshButton = document.querySelectorAll(".main-panel .content-wrapper .home-tab .statistics-table .static-element");
if (refreshButton.length > 0) {
    refreshButton[3].addEventListener("click", () => {
        window.location.reload();
    });
}

// Show Dropdown Avatar

const showDropdownIcon = document.querySelectorAll(".navbar .navbar-menu-wrapper .navbar-nav .nav-item .nav-link")[3];
if (showDropdownIcon) {
    showDropdownIcon.addEventListener("click", () => {
        const dropdown = document.querySelectorAll(".navbar .navbar-menu-wrapper .navbar-nav .nav-item.dropdown .navbar-dropdown")[3];
        dropdown.classList.add("show");
    });
}

