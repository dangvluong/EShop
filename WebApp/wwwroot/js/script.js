function addWards(districtId, selectedAddress = null) {
    $(WardId).empty();
    $.post('/contact/GetWardsByDistrict', { 'districtId': districtId }, data => {
        for (var i in data) {
            if (selectedAddress == null || data[i].wardName != selectedAddress[1].trim()) {
                $(WardId).append(`<option value="${data[i].wardId}">${data[i].wardName}</option>`);
            }              
             else {
                $(WardId).append(`<option value="${data[i].wardId}" selected>${data[i].wardName}</option>`);
            }
        }
    });
}
function addDistricts(provinceId, selectedAddress = null) {
    $(DistrictId).empty();
    $.post(`/contact/GetDistrictsByProvince/`, { 'provinceId': provinceId }, districts => {
        //console.log(districts)                
        for (var i in districts) {
            if (selectedAddress == null || districts[i].districtName != selectedAddress[2].trim()) {
                $(DistrictId).append(`<option value="${districts[i].districtId}">${districts[i].districtName}</option>`);
            } else {
                $(DistrictId).append(`<option value="${districts[i].districtId}" selected>${districts[i].districtName}</option>`);
            }                
        }
        var districtid = $(DistrictId).val();
        addWards(districtid, selectedAddress);
    });
}

$(document).on('change', '#ProvinceId', function () {
//$(ProvinceId).change(() => {
    var p = $(ProvinceId).val();
    console.log(p);
    addDistricts(p);
});

$(document).on('change', '#DistrictId', function () {
//$(DistrictId).change(() => {
    var districtid = $(DistrictId).val();
    console.log(districtid);
    addWards(districtid);
});