$(function () {
    $('#summernote').summernote();
    
})

$('#ShowTime').on('click', function () {
    if (this.checked) {
        document.getElementById("ContentEmail_Day").style.display = 'block';
        document.getElementById("ScheduleJob").disabled = false;
    } else {
        document.getElementById("ScheduleJob").disabled = true;
        document.getElementById("ContentEmail_Day").style.display = 'none';
    }
})