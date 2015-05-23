$(document).ready(function() {

  $("[rel='tooltip']").tooltip();
  $(".tooltip").tooltip();
  $('.carousel').carousel({
    interval: 3500
  })
  
  $('#my_tabs a').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
  })

  $('.datetimepicker').datetimepicker({
    language: 'en',
    pick12HourFormat: true
  });

  $(".chzn-select-deselect").chosen({allow_single_deselect:true});
  $(".chzn-select").chosen();
  $(".chosen-select").chosen();
  
  $('.pdf').media( { width: 930, height: 1070 } ); 
  $('.pdf-admin').media( { width: 300, height: 380 } ); 
  
  $("#assessment-check").click(function(e) {
    $('#assessment-form').fadeIn();
  })

  
  $(".filter-skill").click(function(e) {
    $('#filter-states').fadeIn();
    $('#vendor-no-results').hide();
    $('#vendor-results').fadeIn();
  })
  $(".filter-state").click(function(e) {
    $('#filter-counties').fadeIn();
    $('#vendor-results').fadeOut();
    $('#vendor-results').fadeIn();
  })
  $(".filter-county").click(function(e) {
    $('#filter-cities').fadeIn();
    $('#vendor-results').fadeOut();
    $('#vendor-results').fadeIn();
  })

  $(".filter-city").click(function(e) {
    $('#vendor-results').fadeOut();
    $('#vendor-results').fadeIn();
  })
  
  $('.check-all').click(function (e) {
    if ($(this).is(':checked')) {
      $(this).parent().parent().find('.check-filter').prop("checked", true);
    } else { 
      $(this).parent().parent().find('.check-filter').prop("checked", false);
    };
  })
  $('.check-filter').click(function (e) {
    if (!$(this).is(':checked')) {
      $(this).parent().parent().find('.check-all').prop("checked", false);
    }
  })
  
  $("#staff-list-inputs-button").click(function(e) {
    $('#staff-list-inputs').fadeIn();
  })
  
});