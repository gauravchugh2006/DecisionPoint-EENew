//disabled the other title checkbox when user select the one title
function DisabledTitleChk(chkId) {
    UncheckAllFuntionalPermissions();
    var titleChk = $(".titlechk").map(function (index) {
        var id = $(this).attr('id');
        if (id != chkId) {
            if ($('#' + chkId).prop("checked") == true) {
                $(this).prop("disabled", true);
            }
            else {
                $(this).prop("disabled", false);
            }
        }
    });
}
//used for saved the permission table as per title for internal staff only
function SavePermissionTableAsPerTitle(ROOT) {
    //set the title value
    var titleId = '';
    var chktitlestatus = false;
  
    $(".titlechk").map(function (index) {
        var id = $(this).attr('id');
        if ($('#' + id).prop("checked") == true) {
            if ($('#' + id).prop("disabled") == false) {
                chktitlestatus = true;
                titleId = id;
                if (!($('#' + id).parent().find('.editlnk').length)) {
                    $('#' + id).parent().prepend('<a id="edit_' + titleId + '" class="editlnk" onclick="EditPermissions(this.id)">edit</a>');
                }
                return false;
            }
        }

    });
    //set properties to title radio button
    SetAndResetRadioChkBoxes(titleId, chktitlestatus);
    //set the funtional permissions based on title
    var funPermissionId = '';
    $(".groupchk").map(function (index) {
        var id = $(this).attr('id');
        if ($('#' + id).prop("checked") == true) {
            if (funPermissionId == '') {
                funPermissionId = id.split('_')[1];
            } else {
                funPermissionId = funPermissionId + ':' + id.split('_')[1];
            }
            $('#' + id).prop("checked", false);
        }
    });

    titleId = titleId.split('_')[1];
  
    SavePermissionMappingInDataBaseAsTitle(ROOT, titleId, funPermissionId);
}
//set CSS with title radio button
function SetCSSToTitle(id) {
    $('#' + id).css('margin-left', '43px');
}
//Reset CSS with title radio button
function ResetCSSToTitle(id) {
    $('#' + id).css('margin-left', '11px');
}
//enable and disabled the title radio button
function SetAndResetRadioChkBoxes(titleId, chktitlestatus) {
    $(".titlechk").map(function (index) {
        var id = $(this).attr('id');
        if (chktitlestatus) {
            if (!$('#' + id.split('_')[1]).find('.editlnk').length) {
                SetCSSToTitle(id);
            }
            ResetCSSToTitle(titleId);
            if ($('#' + id).prop("checked") == false) {
                $('#' + id).prop("disabled", false);
            }
            $('#' + titleId).prop("disabled", true);
        }
    });
}

//save permission mapping in database
function SavePermissionMappingInDataBaseAsTitle(ROOT, titleId, funPermissionIds) {
    $.ajax({
        url: ROOT + "CompanyDashBoard/SaveFuntionalPermissionTableMapping",
        data: { 'titleId': titleId, 'funPermissionIds': funPermissionIds },
        type: "POST",
        cache: false,
        success: function (result) {
           
        }
    });
}

function UncheckAllFuntionalPermissions() {
    $(".groupchk").map(function (index) {
        var id = $(this).attr('id');
        $('#' + id).prop("checked", false);
    });
}
function DisabledTitleRadio() {
    $(".titlechk").map(function (index) {
        var id = $(this).attr('id');
        if ($('#' + id).prop("checked")==true) {
            $('#' + id).prop("disabled", true);
        }
    });
}
//Edit funtionalm permissions as per title
function EditPermissions(titleId) {
    UncheckAllFuntionalPermissions();
    DisabledTitleRadio();
    $('#chkperall').prop("checked", false);
    $('#' + titleId.split('_')[1] + "_"+titleId.split('_')[2]).prop("disabled", false);
    $.ajax({
        url: "/CompanyDashBoard/GetPermissionTableMapAsPerTitle",
        data: { 'titleId': parseInt(titleId.split('_')[2]) },
        type: "GET",
        cache: false,
        success: function (data) {
            // data=JSON.parse(data);
            $.each(data, function (i, item) {

                $(".groupchk").map(function (index) {
                    var id = $(this).attr('id');
                    if (id.split('_')[1] == item.TableId) {
                        $('#' + id).prop("checked", true);
                        return false;
                    }

                });
            })
            var count = 0;
          var companyPerCount = data.length;
            
            $(".groupchk").map(function (index) {
                count = count + 1;
            });
           // alert(companyPerCount+ "   " + count);
            if (companyPerCount == count) {
                $('#chkperall').prop("checked", true);
            }
        }
    });
}

//collable the funtional permissions as per title
function CollapseFunPerAsPerTitle(mainid) {
    var id = mainid.split('_')[1];

    $('#viewDiv div').each(function (index) {
        //alert($(this).attr('id')+" "+id);
        if ($(this).attr('id') != id) {
             if (!($(this).attr('id').indexOf(id) === -1)) {
                if ($('#' + mainid).html() == '▼') {
                    $(this).find('a').html('▲');
                    $(this).hide();

                }
                else if ($('#' + mainid).html() == '▲') {
                    $(this).find('a').html('▼');
                    $(this).show();

                }
            }
        }
    })
    if ($('#' + mainid).html() == '▲') {
        $('#' + mainid).html("▼");
    }
    else if ($('#' + mainid).html() == '▼') {
        $('#' + mainid).html("▲")
    }
}

//select and unselect the funtional permission checkboxes
function SelectAndUnSelectAllChkBox(mainId) {
    if ($('#' + mainId).prop("checked") == true) {
        $(".groupchk").map(function (index) {
            var id = $(this).attr('id');
            $('#' + id).prop("checked", true);
        });
    }
    else {
        $(".groupchk").map(function (index) {
            var id = $(this).attr('id');
            $('#' + id).prop("checked", false);
        });
    }
}

//Used for change funtinal permissions
function FuntionalPerChangeid(mainId, totlaFunPer) {
    var count = 0;
    if ($('#' + mainId).prop("checked") == true) {
      
        $(".groupchk").map(function (index) {
            var id = $(this).attr('id');
            if ($('#' + id).prop("checked") == true) {
                count = count + 1;
            }
        });
        if (totlaFunPer == count) {
            $('#chkperall').prop("checked", true);
        }
    }
    else {
        $('#chkperall').prop("checked", false);
    }
}