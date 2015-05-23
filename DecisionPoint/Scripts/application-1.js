$(document).ready(function() {

  $("[rel='tooltip']").tooltip();
  // $(".collapse").collapse()
  $('.carousel').carousel({
    interval: 3500
  })
  $('#my_tabs a').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
  })

  $("#filter-button").click(function() {
    $(".filter-header-off").hide();
    $(".filter-header-on").fadeIn();
  });

  $('#datetimepicker').datetimepicker({
    language: 'en',
    pick12HourFormat: true
  });
    
  $('#calendar').fullCalendar({
    weekends: false,
    events: [
            {
                title  : 'Some Training',
                start  : '2013-03-07',
                allDay : true
            },
            {
                title  : 'Another Training Example',
                start  : '2013-03-26',
                allDay : true
            },
            {
                title  : 'EMS / First Aid',
                start  : '2013-3-25',
                allDay : true
            }
        ]    
  })

  var companies = ["Evanston Fire Department", "Highland Park Fire Department", "Evanston Police Department"];
  var staff     = ["John Lee","Jay Liss","Michael Rappaport","Don Connelly","Maurice Wilkinson","Mary Jane", "Peter Vanderbilt","Steve Bollinger","Michael Nolan","Jack Williams"];
  var trainings = ["Training Module 1","Training Module 2","Truck Maintenance 102","Advanced Sawing"];

  $('.company-search').typeahead({source: companies});
  $('.staff-search').typeahead({source: staff});
  $('.training-search').typeahead({source: trainings});

  $(".chzn-select-deselect").chosen({allow_single_deselect:true});
  $(".chzn-select").chosen();
  $(".chzn-skills").chosen();


  $('#training-category-select').change(function() {
    $(".step-2").fadeIn()    
  });
  $('#training-type-select').change(function () {
      var val = $(this).val();
      if (val == '1') { $('#certification-title').fadeIn(); $('#jpr-name').hide(); $(".step-3").hide() }
      if (val == '2') { $('#jpr-name').fadeIn(); $('#certification-title').hide(); $(".step-3").hide() }
      if (val == '3') { $('#jpr-name').hide(); $('#certification-title').hide(); $(".step-3").fadeIn() }
  });
  $('#jpr-name').change(function() { $(".step-3").fadeIn() });
  $('#certification-title').change(function() { $(".step-3").fadeIn() });
  
  $('.chzn-skills').change(function() {
    $("#select-sub-skills").fadeIn()
  });

  $("#add-skill").click(function () {
    $("#add-skill").before('<input class="span4" placeholder="Please type in skill name" type="text">')
    return false
  });

  $("#add-standard").click(function () {
    $("#add-standard").before('<div class="standard-entry"><input class="span2" placeholder="Reference ID" type="text"> <input class="span2" placeholder="Source" type="text"></div>').fadeIn();
    return false
  });

  $("#add-standard-big").click(function () {
    var numRand = Math.floor(Math.random()*101)
    var class_name = "standard-" + numRand
    var class_name = ""
    $("#add-source").before('<div class="standard" id="' + class_name + '"> <select style="width:110px"><option>Source</option><option>NFPA</option><option>OSHA</option><option>CUSTOM</option><option>Source 1</option><option>Source 2</option></select> <input style="width:70px" placeholder="Ref ID" type="text"> <input placeholder="Standard Description" style="width:540px" type="text"> <a class="remove-this" href="#" style="margin-left: 10px">Remove</a></div>');
    return false
  });
  $("#add-source").click(function () {
    $("#add-source").before('<div><select style="width:110px"><option>Source</option><option>NFPA</option><option>OSHA</option><option>CUSTOM</option><option>Source 1</option><option>Source 2</option></select> <input style="width:70px" placeholder="Ref ID" type="text"></div>');
    return false
  });

  $(".add-input").click(function () {
    $(this).before('<input class="span2" type="text"><br />').fadeIn();
    return false
  });

  $(".add-skill").click(function () {
    $("#add-sub-skill").before('<div class="skill"><input class="span3" placeholder="Enter a Skill" type="text"> <input placeholder="Rq Hrs" style="width: 45px" type="text"> <a class="remove-this" href="#" style="margin-left: 10px">Remove</a></div>').fadeIn();
    return false
  });
  
  $("#add-sub-skill").click(function () {
    $(this).before('<div class="sub-sill"><input class="span3" placeholder="Sub-skill name" type="text">').fadeIn();
    return false
  });
  
  $(".remove-this").click(function () {
    // var element=$(this).parent().attr('id')
    // $("#" + element).fadeOut()
    $(this).parent().fadeOut()
    return false    
  });

  $(".another-row").click(function () {
    $(".another-row").fadeIn();
    return false
  });

  $(".header-btn").click(function () {
    $(this).addClass("disabled")
  });

  $(".header-btn2").click(function () {
    $(".js-section").hide();
    var element=$(this).attr('id')
    $("#" + element + "-box").fadeIn()    
    return false;
  });

  $('.collapse').on('shown', function () {
    $(".disabled").removeClass("disabled")
  })
});



