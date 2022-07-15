$(function () {
    $('#summernote').summernote();
    
})

$('#ShowTime').on('click', function () {
    if (this.checked) {
        
        document.getElementById("ContentEmail_Day").style.display = 'block';
    } else {
        
        document.getElementById("ContentEmail_Day").style.display = 'none';
    }
})