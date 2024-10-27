// Places API Input

const citySelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .city");
const districtSelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .district");
const wardSelect = document.querySelector(".main-panel .content-wrapper .home-tab .card .card-body .forms-sample .select .ward");

if (citySelect && districtSelect && wardSelect) {
    const loadAPIProvinces = async () => {
        try {
            const response = await fetch("https://vn-public-apis.fpo.vn/provinces/getAll?limit=-1");
            const data = await response.json();
            data.data.data.map(item => {
                const option = document.createElement('option');
                option.value = `N'${item.name}'`; 
                option.textContent = item.name;
                const data_code = item.code;
                option.setAttribute("data-code", data_code);
                citySelect.appendChild(option);
            });
        }
        catch (error) {
            console.error('Error fetching places:', error);
        }
    }

    const loadAPIDistrict = async (code) => {
        try {
            const linkAPI = `https://vn-public-apis.fpo.vn/districts/getByProvince?provinceCode=${code}`
            const response = await fetch(linkAPI);
            const data = await response.json();
            districtSelect.innerHTML = '<option value="" disabled selected>--Select District--</option>';
            data.data.data.map(item => {
                const option = document.createElement('option');
                option.value = `N'${item.name_with_type}'`;
                option.textContent = item.name_with_type;
                const data_code = item.code;
                option.setAttribute("data-code", data_code);
                districtSelect.appendChild(option);
            });
        }
        catch (error) {
            console.error('Error fetching places:', error);
        }
    }

    const loadAPIWard = async (code) => {
        try {
            const linkAPI = `https://vn-public-apis.fpo.vn/wards/getByDistrict?districtCode=${code}`
            const response = await fetch(linkAPI);
            const data = await response.json();
            wardSelect.innerHTML = '<option value="" disabled selected>--Select Ward--</option>';
            data.data.data.map(item => {
                const option = document.createElement('option');
                option.value = `N'${item.name_with_type}'`;
                option.textContent = item.name_with_type;
                wardSelect.appendChild(option);
            });
        }
        catch (error) {
            console.error('Error fetching places:', error);
        }
    }

    loadAPIProvinces();
    citySelect.addEventListener('change', function (e) {
        const provinceSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIDistrict(provinceSelectedCode);
    });

    districtSelect.addEventListener('change', function (e) {
        const districtSelectedCode = e.target.selectedOptions[0].getAttribute('data-code');
        loadAPIWard(districtSelectedCode);
    });

    
}