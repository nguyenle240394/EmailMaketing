$(function () {
    $('#summernote').summernote();
    
})


$('#ShowTime').on('click', function () {
    if (this.checked) {
        document.getElementById("ScheduleJob").disabled = false;
        document.getElementById("ContentEmail_Day").style.display = 'block';
    } else {
        document.getElementById("ScheduleJob").disabled = true;
        document.getElementById("ContentEmail_Day").style.display = 'none';
    }
})