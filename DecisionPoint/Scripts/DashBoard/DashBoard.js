//funtion for set the filter value in filter drop down on invite details pages
function SetFilterValue(filtervalue) {
    if (filtervalue == "All") {
        $('#recordtypedd').html("Status");
    }
    else if (filtervalue == "IReceived") {
        $('#recordtypedd').html("Received");
    }
    else if (filtervalue == "pendingO") {
        $('#recordtypedd').html("Pending");
    }
    else if (filtervalue == "IAccepted") {
        $('#recordtypedd').html("Accepted");
    }
    else if (filtervalue == "IDenied") {
        $('#recordtypedd').html("Denied");
    }
    else if (filtervalue == "IAccepted") {
        $('#recordtypedd').html("Accepted");
    }
    else if (filtervalue == "pendingO") {
        $('#recordtypedd').html("Pending");
    }
    
}

//set the on blank line in grid on invite view for IC and Company
function SetTheSeparationInGridOnInvitePage(id) {
    $("#"+id+" tbody tr td").each(function (index) {
        // alert(55);
        var userid = $(this).find('#userid').val();
       
        if (userid == 0) {
            $(this).parent().addClass("setwidth");

        }
    });
}

function ShowEditPopUpForStaffAndICOnManaScr(Id, fname, lname, emailid, phone, role, title, permission, usertype, invitationStatus)
{
   
    if (usertype == 'IC') {
        if (invitationStatus) {
            $('#FName').prop("disabled", true);
            $('#LName').prop("disabled", true);
            $('#Email').prop("disabled", true);
            $('#Phone').prop("disabled", true);
            $("#ddlTitle").prop("disabled", true);
        }
        else {
            $('#FName').prop("disabled", false);
            $('#LName').prop("disabled", false);
            $('#Email').prop("disabled", false);
            $('#Phone').prop("disabled", false);
            $("#ddlTitle").prop("disabled", false);
        }
    }
    $('#editstaffid').val(Id);
    $('#EditCsv').show();
    $('#popupCSV').show();
    var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
    var poistion = parseInt(scrollTop) + 10
    $('#popupCSV').css("top", poistion);
    $('#FName').val(fname);
    $('#LName').val(lname);
    $('#Email').val(emailid);
    $('#Phone').val(phone);


    //set permission
    $("#ddlPermission option").each(function () {
        if ($(this).text() == permission) {
            $(this).attr('selected', 'selected');
        }
    });
    //set title
    $("#ddlTitle option").each(function () {
        if ($(this).text() == title) {
            $(this).attr('selected', 'selected');
        }
    });
}

function SaveUserDetailsFromMnaScrForStaffAndIC(ROOT,type) {
    if (ValiDateUDetailOnManaScr()) {
      
        if ($('#editstaffid').val() != '') {
            
            //if  $('#userType').val() is IC than title id used as a vendor type ID 
            $.ajax({
                url:ROOT+ "CompanyDashBoard/UpdateInternalStaff/",
                data: JSON.stringify({
                    'id': parseInt($('#editstaffid').val()),
                    'titleid': parseInt($('#ddlTitle').val()),
                    'permissionid': parseInt($('#ddlPermission').val()),
                    'fName': $('#FName').val(),
                    'lName': $('#LName').val(),
                    'phone': $('#Phone').val(),
                    'emailId': $('#Email').val(),
                    'UserType': $('#userType').val(),
                    'CompanyId':$('#companyId').val()
                }),
            contentType: 'application/json; charset=utf-8',
            cache: false,
            type: "POST",
            success: function (result) {
                if (type==0){
                GetStaffDetails("All");
                }
                else {
                    GetICRecord("All");
                }
                $('#btnClosePopup').click();
            }
        });


    }
}
}

function ValiDateUDetailOnManaScr() {
    //alert(1);
    var fnameval = true;
    var lnameval = true;
    var officephnval = true;
    var emailidval = true;
    if ($('#FName').val().trim() == '') {
        // alert(1);
        $('#FName').addClass("errorClass");
        fnameval = false;
    }
    if ($('#FName').val().trim() != '') {
        //  alert(2);
        $('#FName').removeClass("errorClass");

    }

    if ($('#LName').val().trim() == '') {
        $('#LName').addClass("errorClass");
        lnameval = false;
    }
    if ($('#LName').val().trim() != '') {
        $('#LName').removeClass("errorClass");

    }
    if ($('#Email').val() != '') {
        if (!validateEmail($('#Email').val())) {
            $('#Email').addClass('errorclass');
            $('#emailerrormsg').html("Enter correct email id");
            return false;
        }
    }
    if ($('#Email').val() == '') {
        $('#Email').addClass('errorclass');
        emailidval = false;
    }

    if ($('#Phone').val() != '') {

        if (!validatePhone($('#Phone').val())) {

            $('#Phone').addClass('errorclass');
            $('#phoneerrormsg').html("Enter correct phone number");
            officephnval = false;
        }
    }

    if (fnameval && emailidval && officephnval && lnameval) {
      return true;
    }
    else {
      return false;
    }

}

//used to show and hide the tabs
function ShowHideTabs(tabName) {
    if (tabName == "mylib") {
        $('.historyli').addClass("active2");
    }
    else { $('.historyli').removeClass("active2"); }
    
    if (tabName == "mycommunication") {
        $('.messagedocli').addClass("active2");
    } else { $('.messagedocli').removeClass("active2"); }
    if (tabName == "invite") {
       
        $('#myAdmin').click();
        $('.inviteli').addClass("active2");
    } else { $('.inviteli').removeClass("active2"); }
    
    if (tabName == "inbox") {
        $('.incommli').addClass("active2");
        $('#myAdmin').click();
    } else { $('.incommli').removeClass("active2"); }
    
    $('.reportingli').removeClass("active2");
    $('.accountprofile').removeClass("active2");

    if (tabName == "communication") {
        $('.commli').addClass("active2");
        $('#myAdmin').click();
    } else { $('.commli').removeClass("active2"); }
    if (tabName == "administration") {
        $('.administrationli').addClass("active2");
        $('#myAdmin').click();
    } else { $('.administrationli').removeClass("active2"); }
    if (tabName == "globallib") {
        $('.globallibraryli').addClass("active2");
        $('#myAdmin').click();
    } else { $('.globallibraryli').removeClass("active2"); }

    if (tabName == "mypartner") {
        $('.partnersli').addClass("active2");
    } else { $('.partnersli').removeClass("active2"); }
    
    if (tabName == "mycalender") {
        $('.mydashboardli').addClass("active2");
    } else { $('.mydashboardli').removeClass("active2"); }
    if (tabName == "contract") {
        $('.contractli').addClass("active2");
        $('#myAdmin').click();
    } else { $('.contractli').removeClass("active2"); }
    if (tabName == "mycontract") {       
        $('.mycontractli').addClass("active2");
    } else { $('.mycontractli').removeClass("active2"); }
    if (tabName == "icclient") {
        $('.clientlistli').addClass("active2");
    } else { $('.clientlistli').removeClass("active2"); }
    if (tabName == "icinvite") {
        $('.inviteli').addClass("active2");
    } else { $('.inviteli').removeClass("active2"); }
    if (tabName == "addinvite") {
        $('.liaddinvite').addClass("active2");
    } else { $('.liaddinvite').removeClass("active2"); }
    if (tabName == "MyIC") {
        $('.limyic').addClass("active2");
    } else { $('.limyic').removeClass("active2"); }
    if (tabName == "liguestapproval") {
        $('.liguestapproval').addClass("active2");
    } else { $('.liguestapproval').removeClass("active2"); }
    
}



function ValiDateUDetailOnManavendor() {

    var officephnval = true;
    var emailidval = true;


    if ($('#emailid').val() != '') {
        if (!validateEmail($('#emailid').val())) {
            $('#emailid').addClass('errorclass');
            $('#emailerrormsg').html("Enter correct email id");
            return false;
        }
    }
    if ($('#emailid').val() == '') {
        $('#emailid').addClass('errorclass');
        emailidval = false;
    }

    if ($('#Phone').val() != '') {

        if (!validatePhone($('#Phone').val())) {

            $('#Phone').addClass('errorclass');
            $('#phoneerrormsg').html("Enter correct phone number");
            officephnval = false;
        }
    }

    if (emailidval && officephnval) {
        return true;
    }
    else {
        return false;
    }

}

function EditClientVendorFromManageScr(ROOT) {
    if (ValiDateUDetailOnManavendor()) {
        $.ajax({
            url: ROOT+"CompanyDashBoard/UpdateDocFlow/",
            data: {
                'FlowTblId': parseInt($('#typeforedit').val()),
                'UserId': parseInt($('#userId').val()),
                'FlowId': parseInt($('#ddFlow').val()),
                'CompanyName': $('#CompanyName').val(),
                'Phone': $('#Phone').val(),
                'emailid': $('#emailid').val(),
                'fName': $('#Contact').val(),
                'lName': $('#LastName').val()
            },
        cache: false,
        type: "POST",
        datatype: 'json',
        success: function (result) {
            //window.location.reload();
            filterrecord("All");
            $('#EditCsv').hide();
            $('#popupCSV').hide();
        }
    });
}
}