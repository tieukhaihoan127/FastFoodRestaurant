// Places API Input

const citySelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .city");
const districtSelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .district");
const wardSelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .ward");
const cityFilter = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .select .city");
const districtFilter = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .select .district");
const wardFilter = document.querySelector(".main-panel .content-wrapper .home-tab .btn-wrapper .select .ward");

const loadAPIProvinces = async (selector) => {
    try {
        const response = await fetch("https://vn-public-apis.fpo.vn/provinces/getAll?limit=-1");
        const data = await response.json();
        data.data.data.map(item => {
            const option = document.createElement('option');
            option.value = `${item.name}`;
            option.textContent = item.name;
            const data_code = item.code;
            option.setAttribute("data-code", data_code);
            selector.appendChild(option);
        });
    }
    catch (error) {
        console.error('Error fetching places:', error);
    }
}

const loadAPIDistrict = async (selector,code) => {
    try {
        const linkAPI = `https://vn-public-apis.fpo.vn/districts/getByProvince?provinceCode=${code}`
        const response = await fetch(linkAPI);
        const data = await response.json();
        data.data.data.map(item => {
            const option = document.createElement('option');
            option.value = `${item.name_with_type}`;
            option.textContent = item.name_with_type;
            const data_code = item.code;
            option.setAttribute("data-code", data_code);
            selector.appendChild(option);
        });
    }
    catch (error) {
        console.error('Error fetching places:', error);
    }
}

const loadAPIWard = async (selector,code) => {
    try {
        const linkAPI = `https://vn-public-apis.fpo.vn/wards/getByDistrict?districtCode=${code}`
        const response = await fetch(linkAPI);
        const data = await response.json();
        data.data.data.map(item => {
            const option = document.createElement('option');
            option.value = `${item.name_with_type}`;
            option.textContent = item.name_with_type;
            selector.appendChild(option);
        });
    }
    catch (error) {
        console.error('Error fetching places:', error);
    }
}

if (citySelect && districtSelect && wardSelect) {

    loadAPIProvinces(citySelect);
    citySelect.addEventListener('change', function (e) {
        const provinceSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIDistrict(districtSelect,provinceSelectedCode);
    });

    districtSelect.addEventListener('change', function (e) {
        const districtSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIWard(wardSelect,districtSelectedCode);
    });
}

if (cityFilter && districtFilter && wardFilter) {
 
    loadAPIProvinces(cityFilter);
    cityFilter.addEventListener('change', function (e) {
        const provinceSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIDistrict(districtFilter, provinceSelectedCode);
    });

    districtFilter.addEventListener('change', function (e) {
        const districtSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIWard(wardFilter, districtSelectedCode);
    });
}