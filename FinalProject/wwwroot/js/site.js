// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.vote-button-up').click(function () {
    $('.vote-button-down').removeClass('active')
    $(this).toggleClass("active")
})

$('.vote-button-down').click(function () {
    $('.vote-button-up').removeClass('active')
    $(this).toggleClass("active")
})

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#upload-preview').attr('src', e.target.result);
            $('#upload-preview').removeAttr('hidden')
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$(".imageToUpload").change(function () {
    readURL(this);
});

function confirmDialog(message) {
    var x = false;
    x = confirm(message);
    
}