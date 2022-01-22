function readURL(input, priviewImg) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(priviewImg).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("[ImageInput]").change(function () {
    var x = $(this).attr("ImageInput");
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("[ImageFile=" + x + "]").attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});

function CopyToClipboardByElementId(elementId) {
    var $temp = $("<input>");
    $("body").append($temp);
    var el = $("#" + elementId);
    $temp.val($(el).text()).select();
    document.execCommand("copy");
    $temp.remove();
    ShowMessage('عملیات موفق', 'اطلاعات مورد نظر با موفقیت کپی شد');
}


function CopyToClipboardText(text) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val(text).select();
    document.execCommand("copy");
    $temp.remove();
    ShowMessage('عملیات موفق', 'اطلاعات مورد نظر با موفقیت کپی شد');
}

$(document).ready(function () {

    // add tags input when we have data-role='tagsinput' attribute
    var tagsInputs = $("[data-role='tagsinput']");
    if (tagsInputs.length > 0) {
        $('<link/>', { rel: 'stylesheet', type: 'text/css', href: '/assets/TagInput/bootstrap-tagsinput.css' })
            .appendTo('head');
        $.getScript("/assets/TagInput/bootstrap-tagsinput.js", function (script, textStatus, jqXHR) { });
    }

    // set ckeditor for textareas where they have ckeditor attribute
    var editors = $("[ckeditor]");
    if (editors.length > 0) {
        $.getScript("/lib/Js/ckeditor.js",
            function (script, textStatus, jqXHR) {
                $(editors).each(function (index, value) {
                    var id = $(value).attr('ckeditor');
                    ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                        {
                            toolbar: {
                                items: [
                                    'heading',
                                    '|',
                                    'bold',
                                    'italic',
                                    'link',
                                    '|',
                                    'fontSize',
                                    'fontColor',
                                    '|',
                                    'imageUpload',
                                    'blockQuote',
                                    'insertTable',
                                    'undo',
                                    'redo',
                                    'codeBlock'
                                ]
                            },
                            language: 'fa',
                            table: {
                                contentToolbar: [
                                    'tableColumn',
                                    'tableRow',
                                    'mergeTableCells'
                                ]
                            },
                            licenseKey: '',
                            simpleUpload: {
                                // The URL that the images are uploaded to.
                                uploadUrl: '/Uploader/UploadImage'
                            }

                        }).then(editor => {
                            window.editor = editor;
                        }).catch(error => {
                            console.error(error);
                        });
                });
            });
    }

    // add date picker to inputs that has DatePicker Attribute
    var datePickers = $("[DatePicker]");
    if (datePickers.length > 0) {
        $('<link/>',
            { rel: 'stylesheet', type: 'text/css', href: '/lib/Percian-Calender/style/kamadatepicker.min.css' }).appendTo('head');
        $.getScript("/lib/Percian-Calender/src/kamadatepicker.min.js", function (script, textStatus, jqXHR) { });
    }
});

// fill pageid for pagging
function FillPageId(id) {
    $("#PageId").val(id);
    $("#filter-search").submit();
}

// submit form with filter-search id on change radio buttons
$('input[type=radio].submit-radio').change(function () {
    $("#pageId").val(1);
    $("#filter-search").submit();
});

function open_waiting(selector = 'body') {
    $(selector).waitMe({
        effect: 'win8',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}

function close_waiting(selector = 'body') {
    $(selector).waitMe('hide');
}

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 5000,
        theme: theme !== '' ? theme : 'success',
    })({
        title: title !== '' ? title : 'اعلان',
        message: text
    });
}

function reloadPageAfterSeconds(seconds) {
    setTimeout(function () {
        location.reload();
    }, seconds);
}

function AddOwlCarousel(selector, config) {
    $(selector).owlCarousel(config);
}


$('[operate-ajax-button]').on('click', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    var removeElementId = $(this).attr('operate-ajax-button');
    swal({
        title: 'اخطار',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "بله",
        cancelButtonText: "خیر",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((result) => {
        if (result.value) {
            open_waiting('body');
            $.get(url).then(res => {
                if (removeElementId !== null && removeElementId !== undefined && removeElementId !== '') {
                    $('[operate-ajax-item="' + removeElementId + '"]').hide(300);
                    close_waiting('body');
                } else {
                    location.reload();
                }
            });
        } else if (result.dismiss === swal.DismissReason.cancel) {
            swal('اعلام', 'عملیات لغو شد', 'error');
        }
    });
});


$('[remove-ajax-button]').on('click', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    var removeElementId = $(this).attr('remove-ajax-button');
    swal({
        title: 'اخطار',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "بله",
        cancelButtonText: "خیر",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((result) => {
        if (result.value) {
            open_waiting('body');
            $.get(url).then(res => {
                if (removeElementId !== null && removeElementId !== undefined && removeElementId !== '') {
                    $('[remove-ajax-item=' + removeElementId + ']').hide(300);
                    close_waiting('body');
                } else {
                    location.reload();
                }
            });
        } else if (result.dismiss === swal.DismissReason.cancel) {
            swal('اعلام', 'عملیات لغو شد', 'error');
        }
    });
});

var cardHeight = $('[card-height]');
if (cardHeight.length > 0) {
    $.each(cardHeight,
        function (index, value) {
            var cardHeightValue = $(value).attr('card-height');
            if (cardHeightValue !== undefined &&
                cardHeightValue !== "" &&
                cardHeightValue !== null &&
                (cardHeightValue > 0 || cardHeightValue <= 100)) {
                var cssHeightValue = 'calc(' + cardHeightValue + '% - 30px)';
                $(value).css('height', cssHeightValue);
            }
        });
}

var cursorPointerElements = $('[cusror-pointer]');
if (cursorPointerElements.length > 0) {
    $.each(cursorPointerElements,
        function (index,value) {
            $(value).css('cursor', 'pointer');
        });
}