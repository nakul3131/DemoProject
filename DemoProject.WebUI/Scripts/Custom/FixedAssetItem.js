$(document).ready(function() {
  $('#cgst').keyup(function() {
    $('#sgst').val($(this).val());

    var cgst = $("#cgst").val();
    var sgst = $("#sgst").val();

    var igst = parseInt(cgst)+parseInt(sgst);
    $('#igst').val(igst);
  });
});