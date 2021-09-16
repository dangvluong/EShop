/*Contact Section*/
function addWards(districtId, selectedAddress = null) {    
    var wardElement = $('div.modal.show #WardId');   
    $(wardElement).empty();
    $.post('/contact/GetWardsByDistrict', { 'districtId': districtId }, data => {
        for (var i in data) {
            if (selectedAddress == null || data[i].wardName != selectedAddress[1].trim()) {
                $(wardElement).append(`<option value="${data[i].wardId}">${data[i].wardName}</option>`);
            }
            else {
                $(wardElement).append(`<option value="${data[i].wardId}" selected>${data[i].wardName}</option>`);
            }
        }
    });
}
function addDistricts(provinceId, selectedAddress = null) {
    var wardElement = $('div.modal.show #DistrictId');   
    $(wardElement).empty();
    $.post(`/contact/GetDistrictsByProvince/`, { 'provinceId': provinceId }, districts => {
        for (var i in districts) {
            if (selectedAddress == null || districts[i].districtName != selectedAddress[2].trim()) {
                $(wardElement).append(`<option value="${districts[i].districtId}">${districts[i].districtName}</option>`);
            } else {
                $(wardElement).append(`<option value="${districts[i].districtId}" selected>${districts[i].districtName}</option>`);
            }
        }
        var districtid = $(wardElement).val();
        addWards(districtid, selectedAddress);
    });
}

$(document).on('show.bs.modal', '.editModal >.modal', function () {
    var targetRow = ($(this).attr('id')).replace('editModal', '');
    var selectedAddress = $(`#${targetRow}`).find('.address').text().split(',');
    $.post('/contact/GetProvinces', provinces => {
        for (var i in provinces) {
            if (provinces[i].provinceName == selectedAddress[3].trim()) {
                $(ProvinceId).append(`<option value="${provinces[i].provinceId}" selected>${provinces[i].provinceName}</option>`);
            } else {
                $(ProvinceId).append(`<option value="${provinces[i].provinceId}">${provinces[i].provinceName}</option>`);
            }
        }
        var provinceId = $(ProvinceId).val();
        addDistricts(provinceId, selectedAddress);
    });
})

$(document).on('click', '.defaultAddress', function () {
    $('.defaultAddress').prop('checked', false);
    $(this).prop('checked', true);
    var contactId = $(this).attr('value');    
    $.post('/contact/UpdateDefaultContact', { 'contactId': contactId }, (data) => {        
        location.reload();
    });
});

/*Cart and Checkout Section*/
$(document).on('change', '#ProvinceId', function () {
    //var p = $(ProvinceId).val();
    var p = $('div.modal.show #ProvinceId').val();   
    addDistricts(p);
});

$(document).on('change', '#DistrictId', function () {
    var districtid = $('div.modal.show #DistrictId').val();          
    addWards(districtid);
});

$(document).on('change', 'input[id="quantityInCart"]', function () {
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

/*Add contact form*/
$(document).on('click', 'input[name="Contact"]', function () {
    //$('input[name="Contact"]').click(function () {
    var contactId = $(this).val();
    $('input[name="ContactId"]').val(contactId);
});
$(document).on('show.bs.modal', '.addModal > .modal', function () {
    $.post('/contact/GetProvinces', provinces => {
        for (var i in provinces) {
            $(ProvinceId).append(`<option value="${provinces[i].provinceId}">${provinces[i].provinceName}</option>`);
        }
        var provinceId = $(ProvinceId).val();
        addDistricts(provinceId);
    });
});
$(document).on('submit', 'form[name="addContact"]', function (e) {
    //$('form[name="addContact"]').submit(function (e) {
    e.preventDefault();
    var fullName = $('div.modal.show input[name="FullName"]').val();
    var phoneNumber = $('div.modal.show input[name="PhoneNumber"]').val();
    var addressHome = $('div.modal.show textarea[name="AddressHome"]').val();
    var provinceId = $('div.modal.show select[name="ProvinceId"]').val();
    var districtId = $('div.modal.show select[name="DistrictId"]').val();
    var wardId = $('div.modal.show select[name="WardId"]').val();
    $.post('/contact/add', {
        'FullName': fullName,
        'PhoneNumber': phoneNumber,
        'AddressHome': addressHome,
        'ProvinceId': provinceId,
        'DistrictId': districtId,
        'WardId': wardId
    }, result => {
        location.reload();
    });
});

$(document).on('submit', 'form[name="editContact"]', function (e) {
    //$('form[name="addContact"]').submit(function (e) {
    e.preventDefault();
    var contactId = $('div.modal.show input[name="ContactId"]').val();
    var fullName = $('div.modal.show input[name="FullName"]').val();
    var phoneNumber = $('div.modal.show input[name="PhoneNumber"]').val();
    var addressHome = $('div.modal.show textarea[name="AddressHome"]').val();
    var provinceId = $('div.modal.show select[name="ProvinceId"]').val();
    var districtId = $('div.modal.show select[name="DistrictId"]').val();
    var wardId = $('div.modal.show select[name="WardId"]').val();
    $.post('/contact/UpdateContact', {
        'ContactId':contactId,
        'FullName': fullName,
        'PhoneNumber': phoneNumber,
        'AddressHome': addressHome,
        'ProvinceId': provinceId,
        'DistrictId': districtId,
        'WardId': wardId
    }, result => {
        location.reload();
    });
});
