$(function () {
    $('#summernote').summernote();
    
})

$(document).ready(function () {
    
})


$('#ShowTime').on('click', function () {
    if (this.checked) {
        document.getElementById("ContentEmail_Schedule").style.display = 'block';
    } else {
        document.getElementById("ContentEmail_Schedule").style.display = 'none';
    }
})