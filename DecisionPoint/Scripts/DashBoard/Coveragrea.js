//Used for get the all state details
function getAllStates(ROOT, coverageAreaFor) {
    //  alert('cll');
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: ROOT + 'Company/getAllStates',
        data: { 'type': parseInt(coverageAreaFor) },
        cache: false,
        success: function (data) {
            if (data.length > 0) {
                inHTML1 = "";
                var State = $('#StateList');
                // alert($('#coverageAreaFor').val());
                if ($('.coverageAreaType').length) {
                    if ($('.coverageAreaType').val() != 1) {
                        State.empty();
                    }
                } else {
                    State.empty();
                }


                $.each(data, function (i, item) {
                    var isExist = CheckStateExistOrNot('' + item.Abbrebiation + '');
                    if (!isExist) {

                        inHTML1 += '<div id="' + item.Abbrebiation + '" ><input style="margin: 7px 32px;" type="checkbox" class="AllState" id="rdAllState_' + item.Abbrebiation + '" value="' + item.Abbrebiation + '" name="StateAllPartial" onchange="SetValesAsPerSeclection(0,this.id,' + coverageAreaFor + ',\'' + ROOT + '\')"/><input type="checkbox" style="margin: 7px -15px;" class="PartialStates" id="rdPartialState_' + item.Abbrebiation + '" value="' + item.Abbrebiation + '" name="StateAllPartial" onchange="GetValesAsPerSeclection(0,this.value,this.id,' + coverageAreaFor + ')"/><span style="margin-left:22px;">' + item.SateName + '</span></div>';
                    }
                })
                inHTML1 = inHTML1 + '<div id="cover"></div>';
                if ($('.coverageAreaType').length) {
                    if ($('.coverageAreaType').val() != 1) {
                        $('#StateList').empty();
                    }
                } else {
                    $('#StateList').empty();
                }

                $('#StateList').append(inHTML1);

            }
        },
        async: false
    });
}

//get county list as per state
function stateChangeAll(lst, userType) {
    //alert(ROOT);
    var idsval = ''
    if (lst != '') {
        $.ajax({
            url: '/Company/GetCountyList',
            cache: false,
            type: 'GET',
            data: { 'StateAbbre': lst, 'type': parseInt(userType) },
            async: true,
            success: function (data) {
                if (data != null && data != '') {
                    inHTML1 = "";
                    HideCounty(lst);
                    // HideCity();
                    var County = $('#CountyList');

                    $.each(data, function (i, item) {
                        idsval = item.OptionsVal;
                        if (idsval == null || idsval == '') {
                            idsval = item.CountyId + '_' + item.StateAbbre;
                        }
                        var isexit = CheckCountyExistOrNot('' + idsval + '');
                        if (!isexit) {

                            //if ($('#coverageAreaFor').val() != 1) {
                            //    inHTML1 += '<div id="' + item.CountyId + '_' + item.StateAbbre + '" ><input style="margin: 7px 32px;" type="checkbox" class="AllCounty" id="rdAllCounty_' + item.CountyId + '_' + item.StateAbbre + '" value="' + item.CountyId + '_' + item.StateAbbre + '" name="CountyAllPartial" onchange="SetValesAsPerSeclection(1,this.id,\'' + userType + '\',\'' + ROOT + '\')" /><input type="checkbox" style="margin: 7px -15px;" class="PartialCounty"id="rdPartialCounty_' + item.CountyId + '_' + item.StateAbbre + '" value="' + item.CountyId + '_' + item.StateAbbre + '" name="CountyAllPartial" onchange="GetValesAsPerSeclection(1,this.value,this.id,\'' + userType + '\')"/><span style="margin-left:22px;">' + item.CountyName + '</span></div>';
                            // }
                            // else if ($('#coverageAreaFor').val() == 1) {
                            //inHTML1 += '<div id="' + item.OptionsVal + '" ><input style="margin: 7px 32px;" type="checkbox" class="AllCounty" id="rdAllCounty_' + item.OptionsVal + '" value="' + item.OptionsVal + '" name="CountyAllPartial" onchange="SetValesAsPerSeclection(1,this.id,\'' + userType + '\',\'' + ROOT + '\')" /><input type="checkbox" style="margin: 7px -15px;" class="PartialCounty"id="rdPartialCounty_' + item.OptionsVal + '" value="' + item.OptionsVal + '" name="CountyAllPartial" onchange="GetValesAsPerSeclection(1,this.value,this.id,\'' + userType + '\')"/><span style="margin-left:22px;">' + item.CountyName + '</span></div>';
                            // }
                            inHTML1 += '<div id="' + idsval + '" ><input style="margin: 7px 32px;" type="checkbox" class="AllCounty" id="rdAllCounty_' + idsval + '" value="' + idsval + '" name="CountyAllPartial" onchange="SetValesAsPerSeclection(1,this.id,\'' + userType + '\',\'' + ROOT + '\')" /><input type="checkbox" style="margin: 7px -15px;" class="PartialCounty"id="rdPartialCounty_' + idsval + '" value="' + idsval + '" name="CountyAllPartial" onchange="GetValesAsPerSeclection(1,this.value,this.id,\'' + userType + '\')"/><span style="margin-left:22px;">' + item.CountyName + '</span></div>';
                        }

                    })
                    County.append(inHTML1);
                }
                else {
                    
                    HideCounty(lst);
                    $('#validatemsg').html('No county in this state.');
                   // $('#rdPartialState_' + lst).prop("checked", false);
                    //$('#Counties').hide();
                    setTimeout(function () {
                        $('#validatemsg').hide();
                    }, 2500);
                }
            },
            error: function (data) {

            }
        })
    }

}
//get city as per county selection
function GetCityListAsPerCounty(filterValue, userType) {
    //alert(filterValue);
    if (filterValue != '') {
        $.ajax({
            url: '/Company/GetCityList',
            cache: false,
            type: 'GET',
            data: { CountyId: filterValue, type: parseInt(userType) },
            async: true
        })
                   .success(function (data) {
                       if (data.length > 0) {
                           inHTML2 = "";
                           HideCity(filterValue);
                           var City = $('#CityList');
                           $.each(data, function (i, item) {
                               idsval = item.OptionsVal;
                               if (idsval == null || idsval == '') {
                                   idsval = item.CityId + '_' + item.CountyId + '_' + item.StateName;
                               }

                               var isexist = CheckCityExistOrNot('' + idsval + '');

                               if (!isexist) {


                                   //if ($('#coverageAreaFor').val() != 1) {
                                   //    inHTML2 += '<div id="' + item.CityId + '_' + item.CountyId + '_' + item.StateName + '" ><input type="checkbox" style="margin: 7px 3px;" class="PartialCity" id="rdPartialState_' + item.CityId + '" value="' + item.CityId + '_' + item.CountyId + '_' + item.StateName + '" name="StateAllPartial" /><span style="margin-left:3px;">' + item.CityName + '</span></div>';
                                   //}
                                   //else if ($('#coverageAreaFor').val() == 1) {
                                   //    inHTML2 += '<div id="' + item.OptionsVal + '" ><input type="checkbox" style="margin: 7px 3px;" class="PartialCity" id="rdPartialState_' + item.CityId + '" value="' + item.OptionsVal + '" name="StateAllPartial" /><span style="margin-left:3px;">' + item.CityName + '</span></div>';
                                   //}
                                   inHTML2 += '<div id="' + idsval + '" style="margin-left:8px;"><input type="checkbox" style="margin: 7px 3px;" class="PartialCity" id="rdPartialState_' + idsval + '" value="' + idsval + '" name="StateAllPartial" onchange="CitySeclection(this.id)" /><span style="margin-left:3px;">' + item.CityName + '</span></div>';
                               }


                           })
                           City.append(inHTML2);
                       }

                   })
                   .error(function (data) {

                   })
    }

}
//hide the city if exist in div
function HideCity(countyId) {
    var tst1 = $("#CityList>div").map(function (index) {
        // alert("IDDD  "+$(this).attr('id').split('_')[1] + "_" + $(this).attr('id').split('_')[2]);
        // alert("COUNTYIDDD "+countyId);
        if (($(this).attr('id').split('_')[1] + "_" + $(this).attr('id').split('_')[2]) != countyId) {
            $(this).hide();
        }
    });
}
//hide the county if exist in div
function HideCounty(stateabbe) {
    //alert(stateabbe);
    var tst1 = $("#CountyList>div").map(function (index) {
        if ($(this).attr('id').indexOf(stateabbe) === -1) {
            $(this).hide();
        }
    });
}
//disabled the county if other county is in edit mode
function DisabledCounty() {
    //alert(stateabbe);
    var tst1 = $("#CountyList>div").map(function (index) {
        var id = $(this).attr('id');
        var allChkBoxId = "rdAllCounty_" + id;
        var partialChkBoxId = "rdPartialCounty_" + id;
        if ($('#' + allChkBoxId).prop("checked") == true) {
            $('#' + allChkBoxId).prop("disabled", true);
            $('#' + partialChkBoxId).prop("disabled", true);
        }
        else if ($('#' + partialChkBoxId).prop("checked") == true) {
            $('#' + allChkBoxId).prop("disabled", true);
            $('#' + partialChkBoxId).prop("disabled", true);
        }
    });
}
//check that city is exist in div or not if exist than show it
function CheckCityExistOrNot(filtervalue) {
    //alert("FILTERVALUE  " + filtervalue);
    var isexist = false;

    $('#CityList div').each(function (index) {
        var dividprefix = $(this).attr('id');
        if (filtervalue == dividprefix) {
            $(this).show();
            isexist = true;
            //return false;
        }
    })
    return isexist;
}
//check that county is exist in div or not if exist than show it
function CheckCountyExistOrNot(filtervalue) {
    var isexist = false;
    //if ($('.coverageAreaType').length) {
    //    if ($('.coverageAreaType').val() != 1) {
    //        HideCounty();
    //    }
    //} else { HideCounty(); }
    $('#CountyList div').each(function (index) {

        var dividprefix = $(this).attr('id');
        if (filtervalue == dividprefix) {

            if ($('#edit_' + filtervalue).length) {
            } else { $(this).find('.PartialCounty').prop("checked", false); }
            $(this).show();
            isexist = true;
            //return false;
        }


    })
    //alert(isexist);
    return isexist;
}
//check that state is exist in div or not if exist than show it
function CheckStateExistOrNot(filtervalue) {

    var isexist = false;
    $('.sss div').each(function (index) {
        // alert(index);
        var dividprefix = $(this).attr('id');
        // alert(dividprefix + "  " + filtervalue);
        if (filtervalue == dividprefix) {
            // alert(12);
            $(this).show();
            isexist = true;
        }
    })

    return isexist;
}
//get county or city details as per state selection
function GetValesAsPerSeclection(type, filterValue, id, userType) {

    var chkboxStatus = true;
    var indexid = '';
    //alert(filterValue);
    if ($("#" + id).is(':checked')) {
        //0 for state
        if (type == 0) {
            $('#isPState').val(1);
            indexid = id.split('_')[1];
            $('#Counties').show();
            var lst = filterValue;
            // alert(lst + "  " + userType);
            stateChangeAll(lst, userType);
            chkboxStatus = true;
            $('#rdAllState_' + indexid).prop("checked", false);
            if ($('#edit_' + indexid).length) { $('#edit_' + indexid).remove(); SetCssForEdit('rdAllState_' + indexid, id, indexid, 2); }
            // indexid = 'rdAllState_' + lastindex;
        }
        //0 for county
        if (type == 1) {
            $('#isPCounty').val(1);
            indexid = id.split('_')[1] + "_" + id.split('_')[2];
            $('#City').show();
            // alert(filterValue);
            GetCityListAsPerCounty(filterValue, userType);
            chkboxStatus = true;
            $('#rdAllCounty_' + indexid).prop("checked", false);
            if ($('#edit_' + indexid).length) { $('#edit_' + indexid).remove(); SetCssForEdit('rdAllCounty_' + indexid, id, indexid, 2); }
            //indexid = 'rdAllCounty_' + lastindex;
        }
    }
    else {

        if (type == 0) {
            $('#isPState').val(0);
            indexid = id.split('_')[1];
            //if state unselect from partial
            RemoveFromListAsPerUnSelection(filterValue, 0);
            chkboxStatus = false;
            if ($('#rdAllState_' + indexid).prop("checked") == false) {
                $('#edit_' + indexid).remove(); SetCssForEdit('rdAllState_' + indexid, 'rdPartialState_' + indexid, indexid, 1);
            }
            //indexid = 'rdAllState_' + lastindex;
        }
        if (type == 1) {
            $('#isPCounty').val(0);
            
            indexid = id.split('_')[1] + "_" + id.split('_')[2];
            //if state unselect from partial
            RemoveFromListAsPerUnSelection(filterValue, 1);
            chkboxStatus = false;
            if ($('#rdAllCounty_' + indexid).prop("checked") == false) {
                $('#edit_' + indexid).remove(); SetCssForEdit('rdAllCounty_' + indexid, 'rdPartialCounty_' + indexid, indexid, 1);
            }
            //indexid = 'rdAllCounty_' + lastindex;
        }
    }
    DisOrEnsCheckBox(type, indexid, chkboxStatus, 'partial');
}
//reset the details if user unselect the any state
function SetValesAsPerSeclection(type, id, userType, ROOT) {
    var chkboxStatus = true;
    var indexid = '';
    var edittype = "all";

    if ($("#" + id).is(':checked')) {
        $('#' + id).prop("disabled", true);
        if (type == 0) {
            $('#isAState').val(1);
            indexid = id.split('_')[1];
            $('#rdPartialState_' + indexid).prop("checked", false);
            $('#rdPartialState_' + indexid).prop("disabled", true);

            RemoveFromListAsPerUnSelection(indexid, type);
            GetValesAsPerSeclection(type, id, "rdPartialState_" + indexid, userType);
            if ($('#' + indexid).find('#edit_' + indexid).length) {

            } else {
                $('#' + indexid).prepend('<a id="edit_' + indexid + '" class="editlnk" onclick="EditCoverageArea(this.id,0,\'' + edittype + '\',\'' + userType + '\')">edit</a>');
                SetCssForEdit(id, 'rdPartialState_' + indexid, indexid, 0);
            }
            HideCounty(indexid);
        }
        if (type == 1) {
            
            $('#isACounty').val(1);
            $('#isCitySelected').val(1)
            indexid = id.split('_')[1] + "_" + id.split('_')[2];

            $('#rdPartialCounty_' + indexid).prop("checked", false);
            $('#rdPartialCounty_' + indexid).prop("disabled", true);
            RemoveFromListAsPerUnSelection(indexid, type);
            //alert("3" + $('#isPCounty').val());
            GetValesAsPerSeclection(type, id, "rdPartialCounty_" + indexid, userType);
            //alert("4" + $('#isPCounty').val());
            if ($('#' + indexid).find('#edit_' + indexid).length) {

            } else {
                $('#' + indexid).prepend('<a id="edit_' + indexid + '" class="editlnk" onclick="EditCoverageArea(this.id,1,\'' + edittype + '\',\'' + userType + '\')">edit</a>');
                SetCssForEdit(id, 'rdPartialCounty_' + indexid, indexid, 0);
            }
        }



        
        chkboxStatus = true;
    } else {
        
        if (type == 0) {
            $('#isAState').val(0);
            indexid = id.split('_')[1];
            $('#rdPartialState_' + indexid).prop("disabled", false);
            if ($('#rdPartialState_' + indexid).prop("checked") == false) {
                $('#edit_' + indexid).remove(); SetCssForEdit('rdAllState_' + indexid, 'rdPartialState_' + indexid, indexid, 1);
            }
        }
        if (type == 1) {
            $('#isACounty').val(0);
            $('#isCitySelected').val(0)
            indexid = id.split('_')[1] + "_" + id.split('_')[2]; $('#rdPartialCounty_' + indexid).prop("disabled", false);
            if ($('#rdPartialCounty_' + indexid).prop("checked") == false) {
                $('#edit_' + indexid).remove(); SetCssForEdit('rdAllCounty_' + indexid, 'rdPartialCounty_' + indexid, indexid, 1);
            }
        }
        chkboxStatus = false;

    }
    
    DisOrEnsCheckBox(type, indexid, chkboxStatus, 'all');
    
}
//disabled/Enabled the checkboxes if user in edit mode
function DisOrEnsCheckBox(type, id, chkboxStatus, filtertype) {

    if (type == 0) {
        //0 for all checkbox
        if (filtertype == 'partial') {
            $('#StateList div').each(function (index) {
                var PcheckboxId = $(this).find('.PartialStates').attr('id');
                var AcheckboxId = $(this).find('.AllState').attr('id');
                var editlnkId = $(this).find('.editlnk').attr('id');

                if (chkboxStatus) {

                    $('#' + editlnkId).attr("readonly", "readonly");
                    $('#' + editlnkId).css('color', '#787878')
                } else { $('#' + editlnkId).removeAttr("readonly"); $('#' + editlnkId).css('color', '') }
                if ($('#' + AcheckboxId).prop("checked") == false) {
                    if ($('#' + PcheckboxId).prop("checked") == false || chkboxStatus == true) {
                        if ($('#' + PcheckboxId).attr('id') != "rdPartialState_" + id) {
                            $('#' + PcheckboxId).attr("disabled", chkboxStatus);

                        }
                        $('#' + AcheckboxId).attr("disabled", chkboxStatus);
                    }
                }
            })

        }
        else {
            // $('#' + id).attr("disabled", chkboxStatus);
        }
    }
    if (type == 1) {
        if (filtertype == 'partial') {
            $('#CountyList div').each(function (index) {
                var PcheckboxId = $(this).find('.PartialCounty').attr('id');
                var AcheckboxId = $(this).find('.AllCounty').attr('id');
                var editlnkId = $(this).find('.editlnk').attr('id');
                if (chkboxStatus) {

                    $('#' + editlnkId).attr("readonly", "readonly");
                    $('#' + editlnkId).css('color', '#787878')
                } else { $('#' + editlnkId).removeAttr("readonly"); $('#' + editlnkId).css('color', '') }
                if ($('#' + AcheckboxId).prop("checked") == false) {
                    if ($('#' + PcheckboxId).prop("checked") == false || chkboxStatus == true) {

                        if ($('#' + PcheckboxId).attr('id') != "rdPartialCounty_" + id) {
                            $('#' + PcheckboxId).attr("disabled", chkboxStatus);
                        }
                        $('#' + AcheckboxId).attr("disabled", chkboxStatus);
                    }
                }
            });
        } else {
            // $('#' + id).attr("disabled", chkboxStatus);
        }
    }

}
//remove the duplicate record from list
function removeDuplicateFromList(id) {
    var seen = {};
    $('#' + id + ' option').each(function () {
        var txt = $(this).text();
        if (seen[txt])
            $(this).remove();
        else
            seen[txt] = true;
    });
    return false;
}
//remove the list as per unselect the checkbox
function RemoveFromListAsPerUnSelection(filtervalue, type) {
    var chkstatestatus = false;
    var chkcountystatus = false;
    var RemoveStateAbb = filtervalue;
    //if state unselect
    if (type == 0) {
        if ($("#CountyList").length) {
            //remove county
            var tst1 = $("#CountyList>div").map(function (index) {
                if (($(this).attr('id').split('_')[1]) == RemoveStateAbb) {
                    $(this).remove();
                }
            });
        }
        //remove city
        if ($("#CityList").length) {

            var tst1 = $("#CityList>div").map(function (index) {
                var id = $(this).attr('id').split('_')[2];

                if (id == RemoveStateAbb) {
                    $(this).remove();
                }
            });
        }
        //check all state unselect or not
        $(".PartialStates").each(function () {
            if ($(this).is(':checked')) {
                chkstatestatus = true;
                return false;
            }
        });
        if (!chkstatestatus) {
            $('#Counties').hide();
            $('#City').hide();
        }
    }
    //if county unselect
    if (type == 1) {
        if ($("#CityList").length) {
            var tst1 = $("#CityList>div").map(function (index) {
                var id = $(this).attr('id').split('_')[1] + "_" + $(this).attr('id').split('_')[2];
                if (id == RemoveStateAbb) {
                    $(this).remove();
                }
            });
        }
        //check all county unselect or not
        $(".PartialCounty").each(function () {
            if ($(this).is(':checked')) {
                chkcountystatus = true;
                return false;
            }
        });
        if (!chkcountystatus) {
            $('#City').hide();
        }
    }
}

//used for clo9se the child window and reload the parent page
function CloseChildWindow() {
    window.opener.document.location.reload(true);
    window.open("", "_top", "", "true ");
    window.close();
}

function ShowPopup(innerId, outerId) {
    $('#' + innerId).slideDown('fast');
    $('#' + outerId).slideDown('fast');
    $('body').css("overflow-y", "hidden");
}
//Used for close the popup
function CloseOpenPopup(innerId, outerId) {
    $('#' + innerId).slideUp('fast');
    $('#' + outerId).slideUp('fast');
    $('body').css("overflow-y", "auto");
}
//used for save the coverage area
function EditCoverageArea(mainid, type, edittype, coverageAreaFor) {

    var id = '';
    if (type == 0) {
        id = mainid.split('_')[1];
        $('#City').hide();
        $('#Counties').hide();
        DisabledCounty();
        $('#StateList div').each(function (index) {
            var PcheckboxId = $(this).find('.PartialStates').attr('id');
            var AcheckboxId = $(this).find('.AllState').attr('id');

            if (PcheckboxId != 'rdPartialState_' + id) {
                $('#' + PcheckboxId).prop("disabled", true);
                $('#' + AcheckboxId).prop("disabled", true);
            } else { $('#' + PcheckboxId).prop("disabled", false); $('#' + AcheckboxId).prop("disabled", false); }
            if ($('#edit_' + id).length) {
            } else { $(this).find('.PartialState').prop("checked", false); }
        });
    }
    else if (type == 1) {
        $('#City').hide();

        var id = mainid.split('_')[1] + "_" + mainid.split('_')[2];
        $('#CountyList div').each(function (index) {
            var PcheckboxId = $(this).find('.PartialCounty').attr('id');
            var AcheckboxId = $(this).find('.AllCounty').attr('id');

            if (PcheckboxId != 'rdPartialCounty_' + id) {
                $('#' + PcheckboxId).prop("disabled", true);
                $('#' + AcheckboxId).prop("disabled", true);
            } else { $('#' + PcheckboxId).prop("disabled", false); $('#' + AcheckboxId).prop("disabled", false); }
            if ($('#edit_' + id).length) {
            } else { $(this).find('.PartialCounty').prop("checked", false); }
        });
    }
    if (edittype == "partial") {
        if (type == 0) {
            $('#Counties').show();
            // alert(id + "  " + coverageAreaFor);
            stateChangeAll(id, coverageAreaFor);

        }
        if (type == 1) {
            $('#City').show();
            GetCityListAsPerCounty(id, coverageAreaFor);
        }
    }
    else if (edittype == "all") {

        //$('#edit_' + id).remove();
        if (type == 0) {

            $('#rdPartialState_' + id).prop("disabled", false);
            $('#rdAllState_' + id).prop("disabled", false);
        } else if (type == 1) {

            $('#rdPartialCounty_' + id).prop("disabled", false);
            $('#rdAllCounty_' + id).prop("disabled", false);
        }
    }
}
//set the CSS for check when edit link in added before the checkbox
function SetCssForEdit(firstchk, secondchk, spanId, type) {
    //if edit is exist
    if (type == 0) {
        $('#' + secondchk).css('margin', '7px 11px');
        $('#' + firstchk).css('margin', '3px 5px');
        $('#' + spanId).find('span').css('margin-left', '-5px');
    }
        //if ediit link is removed
    else if (type == 1) {
        $('#' + secondchk).css('margin', '7px -15px');
        $('#' + firstchk).css('margin', '7px 32px');
        $('#' + spanId).find('span').css('margin-left', '22px');
    }
    else if (type == 2) {
        $('#' + secondchk).css('margin', '7px -18px');
        $('#' + firstchk).css('margin', '7px 32px');
        $('#' + spanId).find('span').css('margin-left', '22px');
    }
}
//added multiple zip code manually 
function AddedZipManually() {
    var aa = false;
    var inHTML1 = '';
    $('#myCheckList ul li').each(function (index) {

        var checkboxid = $(this).find('input[type="checkbox"]').attr('id');

        if ($('#' + checkboxid).prop("checked") == true) {
            // alert(checkboxid);
            aa = CheckZipAlreadyExistOrNot(checkboxid);
            // alert(aa);
            if (!aa) {
                inHTML1 += '<div id="' + checkboxid + '" ><input style="margin: 7px 8px;" type="checkbox" checked="checked" class="ZipList" id="rdZipCode_' + checkboxid + '" value="' + checkboxid + '" name="ZipCode" /><span>' + checkboxid + '</span></div>';
            }
        }


    })
    if (inHTML1 != '') {
        $('#ZipList2').append(inHTML1);
    }
    else if (!aa) {
        $('#manualzipcode').html('Please added zip code manually.');
        $('#txtcsvs').addClass('errorClass');
    }
    $('#myCheckList').empty();
    $('#txtcsvs').val('');
    $('#outerpopup').hide();
}

//check that zip alreay exist in zip list or not
function CheckZipAlreadyExistOrNot(checkboxid) {
    // alert("In METHOD" + checkboxid);
    var chkstatus = false;
    $("#ZipList2 div").each(function (index) {
        //alert("come"+checkboxid);
        var id = $(this).find('input[type="checkbox"]').attr('id').split('_')[1];
        // alert("sd "+checkboxid + "  " + id);
        if (id == checkboxid) {
            chkstatus = true;
            return false;
        }
    })
    return chkstatus;
}
//set the coverage as per state in edit mode
function SaveCoverageAreaAsPerStates(ROOT, type, completeStateTitle, completeCountyTitle, coverageAreaFor) {
    var statecol = '';
    var countycol = '';
    var citycol = '';
    var innerHTML = '';
    //var innerHTML = '<div class="span12" style="height:350px;overflow:auto;">';
    //Set States
    $('#StateList div').each(function (index1) {
        var StateId = $(this).attr('id');
        var PStateId = $(this).find('.PartialStates').attr('id');
        var AStateId = $(this).find('.AllState').attr('id');
        if ($('#' + PStateId).prop("checked") == true) {
            innerHTML += '<div id="' + StateId + '" class="collapse-state state-div "><a id="lnk-' + StateId + '" style="text-decoration:none;" class="collapse-state-a" onclick="CollapseDetails(this.id,0)">▼</a>' + $('#' + StateId).find('span').html() + '</div>';
            //state col for saved in database
            if (statecol != '') {
                statecol = statecol + "*" + StateId + "$" + $('#' + StateId).find('span').html().trim() + "$" + 1;
            } else {
                statecol = StateId + "$" + $('#' + StateId).find('span').html() + "$" + 1;
            }
            $('#CountyList div').each(function (index2) {
                var CountyId = $(this).attr('id');
                if (CountyId.split('_')[1] == StateId) {
                    var PCountyId = $(this).find('.PartialCounty').attr('id');
                    var ACountyId = $(this).find('.AllCounty').attr('id');
                    if ($('#' + PCountyId).prop("checked") == true) {
                        // alert("2 " + innerHTML);
                        innerHTML += '<div id="' + CountyId + '" class="Marginleft5 collapse-state county-div"><a class="collapse-state-a" style="text-decoration:none;" id="lnk-' + CountyId + '" onclick="CollapseDetails(this.id,1)">▼</a>' + $('#' + CountyId).find('span').html() + '</div>';
                        //county col for saved  in database
                        if (countycol != '') {
                            countycol = countycol + "*" + CountyId + "$" + $('#' + CountyId).find('span').html() + "$" + 1;
                        }
                        else {
                            countycol = CountyId + "$" + $('#' + CountyId).find('span').html() + "$" + 1;
                        }
                        $('#CityList div').each(function (index3) {
                            var CityId = $(this).attr('id');
                            if (CityId.split('_')[1] + "_" + CityId.split('_')[2] == CountyId) {
                                var CityCheckbox = $(this).find('.PartialCity').attr('id');
                                if ($('#' + CityCheckbox).prop("checked") == true) {
                                    innerHTML += '<div id="' + CityId + '" class="MarginLeft22 city-div">' + $('#' + CityId).find('span').html() + '</div>';
                                    if (citycol != '') {
                                        citycol = citycol + "*" + CityId + "$" + $('#' + CityId).find('span').html();
                                    }
                                    else {
                                        citycol = CityId + "$" + $('#' + CityId).find('span').html();
                                    }
                                }
                            }
                        })
                    }
                    else if ($('#' + ACountyId).prop("checked") == true) {
                        innerHTML += '<div id="' + CountyId + '" class="marginleft-top-24px-10 county-div">' + $('#' + CountyId).find('span').html() + completeCountyTitle + '</div>';
                        if (countycol != '') {
                            countycol = countycol + "*" + CountyId + "$" + $('#' + CountyId).find('span').html() + "$" + 0;
                        }
                        else {
                            countycol = CountyId + "$" + $('#' + CountyId).find('span').html() + "$" + 0;
                        }
                    }
                }
            })
        }
        else if ($('#' + AStateId).prop("checked") == true) {
            innerHTML += '<div id="' + StateId + '" class="marginleft21px state-div " >' + $('#' + StateId).find('span').html() + completeStateTitle + '</div>';
            if (statecol != '') {
                statecol = statecol + "*" + StateId + "$" + $('#' + StateId).find('span').html().trim() + "$" + 0;
            } else {
                statecol = StateId + "$" + $('#' + StateId).find('span').html() + "$" + 0;
            }
        }
    })
    // innerHTML += '</div>';
    // alert("final "+innerHTML);
    if (type == 0) {
        //Saved in database
        // alert("State " + statecol);
        // alert("County " + countycol);
        // alert("City " + citycol);
        SaveCoverageArea(ROOT, 0, statecol, countycol, citycol, '', coverageAreaFor)
    }

    $('#view').append(innerHTML);
    $('#view').show();
    $('#CVATitle').show();
    $('#save').hide();
    $('#btnedit').show();
    $("#btnclose").css("margin-right", "-4px");

    $('#btnmarkascomplete').hide();
    $('#btnahowascomplete').hide();

}
//collable the coverage area details
function CollapseDetails(mainid, type) {
    var id = mainid.split('-')[1];

    $('#view div').each(function (index) {
        //alert($(this).attr('id')+" "+id);
        if ($(this).attr('id') != id) {
            // alert(index + "  " + $(this).attr('id'));
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
//set the coverage as per zip code in edit mode
function SaveCoverageAreaAsPerZipCode(ROOT, coverageAreaFor, type) {
    //  alert("type " + type);
    //$('#view').empty();
    var lst = '';
    var zipcol = '';
    // var zipCodeId = '';
    //$('#myiframe').contents().find("#ZipList2 div").each(function (index) {
    //    alert(index);
    //    if ($(this).find('input[type="checkbox"]').prop("checked") == true) {

    //        if (lst == '') {
    //            lst = ($(this).attr('id'));
    //        } else {
    //            lst += ',' + ($(this).attr('id'));
    //        }
    //    }
    //});
    if (type == 0) {

        $('#myiframe').contents().find("#ZipList2 div").each(function (index) {
            //  alert(index);
            if ($(this).find('input[type="checkbox"]').prop("checked") == true) {

                if (lst == '') {
                    lst = ($(this).attr('id'));
                } else {
                    lst += ',' + ($(this).attr('id'));
                }
            }
        });
    }
    else {

        $("#ZipList2 div").each(function (index) {
            // alert(index);
            if ($(this).find('input[type="checkbox"]').prop("checked") == true) {

                if (lst == '') {
                    lst = ($(this).attr('id'));
                } else {
                    lst += ',' + ($(this).attr('id'));
                }
            }
        });

    }
    // lst = (lst.slice(0, -1));

    $.ajax({
        url: ROOT + 'Company/GetCityListByZip',
        type: 'Post',
        data: { 'ZipList': lst },
        async: true
    })
     .success(function (data) {
         if (data.length > 0) {
             var inHTML1 = '';
             inHTML1 = '<table class="table table-hover zipcode-table"><thead><tr class="span12" style="margin-top: 10px; margin-left: 0px;"><th class="span4 zipcode-header zipcode-td-width">Zip Code</th><th class="span4 zipcode-header zipcode-td-width">City Name</th><th class="span4 zipcode-header">State Name</th></tr></thead><tbody>';
             $.each(data, function (i, item) {
                 var tool = item.CityName + '-' + item.StateAbbre;
                 inHTML1 += '<tr id="' + item.ZipCode + '" class="span12" style="margin-top: 10px; margin-left: 0px;"><td class="span4 zipcode-td-width">' + item.ZipCode + '</td><td class="span4 zipcode-td-width">' + item.CityName + '</td><td class="span4">' + item.StateName + '</td></tr>';
             })
             inHTML1 += '</tbody></table>';

             if (type == 0) {
                 $('#view').append(inHTML1);
                 $('#view').show();
                 $('#CVATitle').show();
                 $('#save').hide();
                 $('#btnedit').show();
                 $("#btnclose").css("margin-right", "-4px");
                 $('#btnmarkascomplete').hide();
                 $('#btnahowascomplete').hide();

             }
             else {
                 $('body', window.parent.document).find('#view').append(inHTML1);
                 $('body', window.parent.document).find('#view').show();
                 $('body', window.parent.document).find('#CVATitle').show();
                 $('body', window.parent.document).find('#save').hide();
                 $('body', window.parent.document).find('#btnedit').show();
                 $('body', window.parent.document).find("#btnclose").css("margin-right", "-4px");
                 $('body', window.parent.document).find('#btnmarkascomplete').hide();
                 $('body', window.parent.document).find('#btnahowascomplete').hide();

             }
             zipcol = lst;
             if (type == 0) {
                 // alert('come41522');
                 SaveCoverageArea(ROOT, 1, '', '', '', zipcol, coverageAreaFor);
             }
         }

     })
     .error(function (data) {
         alert('error');
     })
.complete(function (data) {

    $('#ConfiramtionPopupouter').slideUp('fast');
    $('#ConfiramtionPopupinner').slideUp('fast');
    $('body').css("overflow-y", "auto");
    $('#dvMsg').css("color", "#000");
})


}
//save the coverage in database
function SaveCoverageArea(ROOT, type, statecol, countycol, citycol, zipcol, CoverageAreaFor) {
    //0 for coverage area as per state
    if (type == 0) {
        var coverAreamode = "save";
        if ($('.coverageAreaType').length) {
            if ($('.coverageAreaType').val() == 1) {
                coverAreamode = "update";
            }
        }
        $.ajax({
            type: "POST",
            url: ROOT + "Company/SaveCoverageArea",
            data: JSON.stringify({
                StateCol: statecol,
                CountyCol: countycol,
                CityCol: citycol,
                CoverageAreaMode: coverAreamode,
                CoverageAreaFor: CoverageAreaFor,
                CoverageAreaType: 'state'
            }),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('.coverageAreaType').val(1);
                $('#ConfiramtionPopupouter').hide();
                $('#ConfiramtionPopupinner').hide();
                $('#yesNoDiv').show();
                // alert('saved');

            },
            beforeSend: function (jqXHR, obj) {


                $('#ConfiramtionPopupouter').show();
                $('#ConfiramtionPopupinner').show();
                $('#yesNoDiv').hide();
                $('#dvMsg').text('Please wait...');
                //var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
                //var poistion = scrollTop +100;
                //$('#ConfiramtionPopupinner').css("top", poistion);
                

            },
        })
    }
        //1 for coverage area as per zip code
    else if (type == 1) {
        var coverAreamode = "save";
        if ($('#myiframe').contents().find('.coverageAreaType').length) {
            if ($('#myiframe').contents().find('.coverageAreaType').val() == 1) {
                coverAreamode = "update";
            }
        }
        $.ajax({
            type: "POST",
            url: ROOT + "Company/SaveCoverageArea",
            data: JSON.stringify({
                ZipCol: zipcol,
                CoverageAreaMode: coverAreamode,
                CoverageAreaFor: CoverageAreaFor,
                CoverageAreaType: 'zip'
            }),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('#myiframe').contents().find('.coverageAreaType').val(1);
                //alert($('.coverageAreaType').val() + '  saved');
            }
        })
    }
}

//set coverage if use select the all states or DNA
function SetCoverageAreaForAllOrDNA(ROOT, type, coverageAreaStatus) {
    $.ajax({
        type: "GET",
        url: ROOT + "Company/CoverageAreaDoesNotApply",
        cache: false,
        data: { 'type': type, 'coverageAreaStatus': coverageAreaStatus },
        success: function (result) {
            CloseChildWindow();
        }
    })
}
//validate the coverage area
function ValidateCoverageArea(type) {
    var valiDateStatus = false;
    if (type == 0) {
        //check in state
        $('#StateList div').each(function (index1) {
            var PStateId = $(this).find('.PartialStates').attr('id');
            var AStateId = $(this).find('.AllState').attr('id');
            if ($('#' + PStateId).prop("checked") == true) {
                valiDateStatus = true;
                return false;
            }
            else if ($('#' + AStateId).prop("checked") == true) {
                valiDateStatus = true;
                return false;
            }
        })
        //check in county
        if (!valiDateStatus) {
            $('#CountyList div').each(function (index1) {
                var PStateId = $(this).find('.PartialCounty').attr('id');
                var AStateId = $(this).find('.AllCounty').attr('id');
                if ($('#' + PStateId).prop("checked") == true) {
                    valiDateStatus = true;
                    return false;
                }
                else if ($('#' + AStateId).prop("checked") == true) {
                    valiDateStatus = true;
                    return false;
                }
            })
        }
        //check in city
        if (!valiDateStatus) {
            $('#CityList div').each(function (index1) {
                var PStateId = $(this).find('.PartialCity').attr('id');
                if ($('#' + PStateId).prop("checked") == true) {
                    valiDateStatus = true;
                    return false;
                }

            })
        }
    }
        //as per zip
    else if (type == 1) {
        $('#myiframe').contents().find("#ZipList2 div").each(function (index) {
            if ($(this).find('input[type="checkbox"]').prop("checked") == true) {
                valiDateStatus = true;
                return false;
            }
        })
    }
    return valiDateStatus;
}
//check that city is selected or not
function CitySeclection(id) {
    if ($("#" + id).is(':checked')) {
        $('#isCitySelected').val(1);

    }
    else {
        $('#isCitySelected').val(0);
    }
}
//valudate the list if user in partial selection
function ChkValASPerPartialSelection() {
    var vallevel = 0;
    if ($('#isAState').val() != 1)  {
        if ($('#isPState').val() == 1) {
            // alert("1 "+vallevel);
            if ($('#isPCounty').val() != 1 && $('#isACounty').val() != 1) {
                vallevel = 1;
                // alert("21 " + vallevel);
            }
            else {
                if ($('#isPCounty').val() == 1 || $('#isACounty').val() == 1) {
                    //alert("213 " + vallevel);
                    if ($('#isCitySelected').val() != 1) {
                        vallevel = 2;
                    }
                }
            }
        }
    }
   // alert(vallevel);
    return vallevel;
}
//get coverage area selection on communication recipient page
function setlocationfilter() {
    var locationfilter = '';
    var CAType = "0";
    var zipCol = '';
    var stateCol = '';
    var countyCol = '';
    var cityCol = '';
    if ($('#coverageareaiframe').contents().find(('.zipcode-table')).length) {
     
        CAType = "1";
        zipCol = '';
        $('#coverageareaiframe').contents().find("#myiframe").contents().find("#ZipList2 div").each(function (index) {
          
            if ($(this).find('input[type="checkbox"]').prop("checked") == true) {
                if (zipCol == '') {
                    zipCol = ($(this).attr('id'));
                } else {
                    zipCol += '*' + ($(this).attr('id'));
                }
            }
        });
        if (zipCol != '') {
            locationfilter = zipCol;
        }
    }
    else {
        
        stateCol = '';
        countyCol = '';
        cityCol = '';
        //state col
        $('#coverageareaiframe').contents().find('#StateList div').each(function (index1) {
         //if partial checkbox selected
            if ($(this).find('.PartialStates').prop("checked") == true) {
                if (stateCol == '') {
                    stateCol = $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 1;
                }
                else {
                    stateCol = stateCol + "*" + $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 1;
                }
            }
                //if all checkbox selected
            else if ($(this).find('.AllState').prop("checked") == true) {
                if (stateCol == '') {
                    stateCol = $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 0;
                }
                else {
                    stateCol = stateCol + "*" + $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 0;
                }
            }
           
        })
        //county col
        $('#coverageareaiframe').contents().find('#CountyList div').each(function (index1) {
          
            if ($(this).find('.PartialCounty').prop("checked")) {
                if (countyCol == '') {
                    countyCol = $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 1;
                }
                else {
                    countyCol = countyCol + "*" + $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 1;
                    //countyCol = countyCol + "*" + $(this).find('span').html();
                }
            }
            else if ($(this).find('.AllCounty').prop("checked")) {
                if (countyCol == '') {
                    countyCol = $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 0;
                }
                else {
                    countyCol = countyCol + "*" + $(this).attr('id') + ";" + $(this).find('span').html() + ";" + 0;
                   // countyCol = countyCol + "*" + $(this).find('span').html();
                }
            }
        })
        //city col
        $('#coverageareaiframe').contents().find('#CityList div').each(function (index1) {
           
            if ($(this).find('.PartialCity').prop("checked")) {
                if (cityCol == '') {
                    cityCol = $(this).attr('id') + ";" + $(this).find('span').html();
                }
                else {
                    cityCol = cityCol + "*" + $(this).attr('id') + ";" + $(this).find('span').html();
                   // cityCol = cityCol + "*" + $(this).find('span').html();
                }
              //alert(cityCol);
            }
            
        })
        if (stateCol != '') {
            locationfilter = stateCol + ":state";
            if (countyCol != '') {
                locationfilter = locationfilter + "$" + countyCol + ":county";
                if (cityCol != '') {
                    locationfilter = locationfilter + "$" + cityCol + ":city";
                }
            }
        }
    }

   
    locationfilter = CAType +"#"+ locationfilter;
    $('#locfill').val(locationfilter);
     filterrecord('');
}

function SetCoverageAreaCol(Id) {
    var col = ''
    if (col == '') {
        col = $('#' + Id).find('span').html();
    }
    else {
        col = col + "*" + $('#' + Id).find('span').html();
    }
    return col;
}