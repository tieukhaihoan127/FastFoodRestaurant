filterId = document.querySelector(".main-panel .content-wrapper .home-tab .filter input[name='object-id']");
filterName = document.querySelector(".main-panel .content-wrapper .home-tab .filter input[name='object-name']");

if (filterId) {
    const filterButton = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper #filterButton");
    let url = new URL(window.location.href);
    url.searchParams.delete("pageNumber");
    url.searchParams.set("pageNumber", 1);
    filterButton.addEventListener("click", () => {
        const keyword = filterId.value;
        if (filterName) {
            const name = filterName.value;

            if (name) {
                url.searchParams.set("name", name);
            }
            else {
                url.searchParams.delete("name");
            }
        }

        if (keyword) {
            url.searchParams.set("keyword", keyword);
        }
        else {
            url.searchParams.delete("keyword");
        }

        window.location.href = url.href;
    });
}