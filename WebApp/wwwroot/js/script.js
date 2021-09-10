/*Contact Section*/
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

/*Cart and Checkout Section*/
$(document).on('change', '#ProvinceId', function () {
    var p = $(ProvinceId).val();
    console.log(p);
    addDistricts(p);
});

$(document).on('change', '#DistrictId', function () {
    var districtid = $(DistrictId).val();
    console.log(districtid);
    addWards(districtid);
});

console.log("abc");
$(document).on('change', 'input[id="quantityInCart"]', function () {
    console.log("ab");
    var listNode = $(this).parent().prevAll();
    var pid = $(listNode[2]).attr('name');
    var cid = $(listNode[1]).attr('name');
    var sid = $(listNode[0]).attr('name');
    var qty = $(this).val();
    $.post('/cart/editcart', { 'ProductId': pid, 'ColorId': cid, 'SizeId': sid, 'Quantity': qty }, result => {
        location.reload();
    })
});
$(document).on('click', '.DeleteCart', function () {
    console.log("abc");
    if (confirm('Are you sure delete?')) {
        var listNode = $(this).parent().prevAll();
        var pid = $(listNode[5]).attr('name');
        var cid = $(listNode[4]).attr('name');
        var sid = $(listNode[3]).attr('name');
        $.post('/cart/deletecart', { 'ProductId': pid, 'ColorId': cid, 'SizeId': sid }, result => {
            location.reload();
        });
    }
});

/**/