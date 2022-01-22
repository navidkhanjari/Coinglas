

function changePage(Page) {
    var url = new URL(window.location.href);
    var SerachParams = url.searchParams;
    //Change PageId
    SerachParams.set('Page', Page);
    url.search = SerachParams.toString();

    //new Url String
    var newUrlString = url.toString();
    window.location.replace(newUrlString);
}
$("#TakeEntity").on('change', function () {
    $("#filter-form").submit();
});
$('#Edit-Password').on('change', function () {
    $('#Edit-Password').append(`<p class='text text-danger'>درصورت پر کردن این فیلد کلمه عبور کاربر تغییر خواهد کرد!</p>`);
    $('#Edit-Password > p ').fadeOut(8000);
});
$('#Edit-Email').on('change', function () {
    $('#Edit-Email').append(`<p class='text text-danger'>اگر قصد تغيير ايميل را داريد توجه نماييد درصورتي كه ايميل وارد شده قبلا در سيستم وجود داشته باشد سيستم با خطا مواجه خواهد شد</p>`);
    $('#Edit-Email > p ').fadeOut(11000);
});
$('#Edit-UserName').on('change', function () {
    $('#Edit-UserName').append(`<p class='text text-danger'>اگر قصد تغيير نام كاربري را داريد توجه نماييد درصورتي كه نام كاربري وارد شده قبلا در سيستم وجود داشته باشد سيستم با خطا مواجه خواهد شد</p>`);
    $('#Edit-UserName > p ').fadeOut(11000);
});

