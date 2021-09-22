/*Contact Section*/
function addWards(districtId) {    
    var wardElement = $('div.modal.show #WardId');   
    $(wardElement).empty();
    $.post('/contact/GetWardsByDistrict', { 'districtId': districtId }, data => {
        for (var i in data) {           
                $(wardElement).append(`<option value="${data[i].wardId}">${data[i].wardName}</option>`);           
        }
    });
}
function addDistricts(provinceId) {
    var districtElement = $('div.modal.show #DistrictId');   
    $(districtElement).empty();
    $.post(`/contact/GetDistrictsByProvince/`, { 'provinceId': provinceId }, districts => {
        for (var i in districts) {            
                $(districtElement).append(`<option value="${districts[i].districtId}">${districts[i].districtName}</option>`);           
        }
        var districtid = $(districtElement).val();
        addWards(districtid);
    });
}

$(document).on('click', '.defaultAddress', function () {
    $('.defaultAddress').prop('checked', false);
    $(this).prop('checked', true);
    var contactId = $(this).attr('value');    
    $.post('/contact/UpdateDefaultContact', { 'contactId': contactId }, (data) => {        
        location.reload();
    });
});

$(document).on('change', '#ProvinceId', function () {   
    var p = $('div.modal.show #ProvinceId').val();   
    addDistricts(p);
});

$(document).on('change', '#DistrictId', function () {
    var districtid = $('div.modal.show #DistrictId').val();          
    addWards(districtid);
});
/*Update cart*/
$(document).on('change', 'input[id="quantityInCart"]', function () {
    var listNode = $(this).parent().prevAll();
    var pid = $(listNode[3]).attr('name');
    var cid = $(listNode[2]).attr('name');
    var sid = $(listNode[1]).attr('name');
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

/*Choose contact when checkout*/
$(document).on('click', '#selectContactToCheckout input[name="Contact"]', function () {
    var contactId = $(this).val();
    $('form[name="formCheckout"] input[name="ContactId"]').val(contactId);
});
/*Add contact modal*/
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


function formatCurrency(input) {
    return input.toLocaleString({ style: "currency", currency: "VND" }).replace(/,/g, ".") + " ₫";
}
//add total cost of invoice to cart and checkout page
$(document).ready(function () {
    $('#totalCost').text(formatCurrency(parseInt($('#totalProductCost').text().replace(".", "").replace("đ", "")) + parseInt($('#shipCost').text().replace(".", "").replace("đ", ""))));
});
