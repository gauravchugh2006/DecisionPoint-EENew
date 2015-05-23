//--------------------------------------------------------------------------------------//
//----------Global Declaration Section for JCR - Job Compliance Requirement-------------//
//--------------------------------------------------------------------------------------//

var ERRORMESSAGE = 'Please select';
var IsStaffTitle = false;
//used for check all vendor types details
function CheckAllVendorType(id) {
    if ($('#' + id).prop("checked")) {
        $('.chkVendorType').each(function () {
            $(this).prop("checked", true);
        });
    }
    else {
        $('.chkVendorType').each(function () {
            $(this).prop("checked", false);
        });
    }
    SetICTypeAndClientsForBgCheck(0);
}
//used for show compliance sections
function ShowCompliance(type) {
    if (type == 0) {
        Background();
    }
    else if (type == 1) {
        ProfessionalLicense();
    }
    else if (type == 2) {
        Insurance();
    }
    else if (type == 3) {
        $('#financialDiv').show();
        $('#backgroundDiv').hide();
        $('#profLicenseDiv').hide();
        $('#insuranceDiv').hide();
        $('#additionalReqDiv').hide();
    }
    else if (type == 4) {
        AdditionalReqiurement();
    }

    // for insurance tab
    ToggleInsuranceFields();
    GetJCRComplianceSectionDetails();

}
function ToggleInsuranceFields() {

    if ($('input[name="rad_JCRfor"]:checked').val() === 'Staff') {
        $('#labelselfRepAddIns').show();
    } else {
        $('#labelselfRepAddIns').hide();
    }
    $('#labelselfRepAddIns').show();
}
//used for set all checkbox value
function SetAllCheckBox() {
    $('#valMsgForIcType').html("");
    var chkCount = 0;
    var vendorTypeCount = $('.chkVendorType').length;
    $('.chkVendorType').each(function () {
        if ($(this).prop("checked")) {
            chkCount = chkCount + 1;
        }
    });
    if (vendorTypeCount == chkCount) {
        $("#chkAllVType").prop("checked", true);
    }
    else {
        $("#chkAllVType").prop("checked", false);
    }
    SetICTypeAndClientsForBgCheck(0);
}

function Background() {
    ResetRecord(0, 0, 1);
    $('#backgroundDiv').show();
    $('#profLicenseDiv').hide();
    $('#insuranceDiv').hide();
    $('#financialDiv').hide();
    $('#additionalReqDiv').hide();
    SetICTypeAndClientsForBgCheck(0);
    SetICTypeAndClientsForBgCheck(1);
    $('.lblVendorType').each(function () {
        var checkbox = $(this).find('.chkVendorType');
        var spanText = $(this).find('span').html().trim();
        if (checkbox.prop("checked") == true)
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" checked="' + checkbox.attr("checked") + '"/><span class="MarginL">' + spanText + '</span></span>');
        else
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" /><span class="MarginL">' + spanText + '</span></span>');
    });
    RebindStaffList();
    $('#allChkLi').show();
    $('#dvStaffTitleList #allChkLi').show();
    DisabledInEdit(false);
}
// Rebind staff titles list for JCR
function RebindStaffList() {
    $('.lblStaffTitle').each(function () {
        var checkbox = $(this).find('.chkTitle');
        var spanText = $(this).find('span').html().trim();
        if (checkbox.prop("checked") == true)
            $(this).replaceWith('<span class="lblStaffTitle"><input type="checkbox" name="chkTitle" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '"  checked="' + checkbox.attr("checked") + '"/><span class="MarginL">' + spanText + '</span></span>');
        else
            $(this).replaceWith('<span class="lblStaffTitle"><input type="checkbox" name="chkTitle" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '"  /><span class="MarginL">' + spanText + '</span></span>');
    });
    $('.chkTitle').click(function (e) {
        ChkTitleClick(e);

    });
}
//Description   :  function to handle checkboxes' click of staff title
//Parameters    :  e - event raised by element
//Created by    : Gaurav Chugh
function ChkTitleClick(e) {
    //------------------
    var isAnyChecked = true;
    $('.chkTitle').each(function () {
        if ($(this).prop("checked") && isAnyChecked) {
            isAnyChecked = true;
        } else {
            isAnyChecked = false;
        }
    });
    $('input[name="chkAllTitleList"]').prop('checked', isAnyChecked);
    SetBackgroundStaffTitles(e);

}
//Description   : function to set Staff titles to background
//Parameters    :  e - event raised by element
//Created by    : Gaurav Chugh
function SetBackgroundStaffTitles(e) {
    // set background staff title divs
    $('#icTypeDiv').hide();
    $('#staffTitleDiv').show();
    $('#staffTitleDiv').empty();
    var titleStr = "";
    var staffTitleStr = "<ul style='list-style: none;'>";
    $('.chkTitle').each(function () {
        if ($(this).prop("checked")) {
            titleStr = titleStr + "<li>" + $(this).parent().find('span').html() + "</li>";
        }
    });
    staffTitleStr = staffTitleStr + titleStr + "</ul>";
    console.log('::' + staffTitleStr);
    $('#staffTitleDiv').append(staffTitleStr);
    titleStr = "";
}

// Replace Staff checkbox as radio button for professional license
function ReplaceStaffCheckboxToRadio() {
    $('.lblStaffTitle').each(function () {
        var checkbox = $(this).find('.chkTitle');
        var spanText = $(this).find('span').html().trim();
        $(this).replaceWith('<span class="lblStaffTitle"><input type="radio" name="chkTitle" id="' + checkbox.attr("id") + '"  class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '"><span class="MarginL">' + spanText + '</span></span>');
    });
    $('input[name="chkAllTitleList"]').prop('checked', false);
}
// function to set fields as per toggle professional license partial view
function ProfessionalLicense() {
    ResetRecord(1, 0, 1);
    $('#profLicenseDiv').show();
    $('#backgroundDiv').hide();
    $('#insuranceDiv').hide();
    $('#financialDiv').hide();
    $('#additionalReqDiv').hide();
    $('.lblVendorType').each(function () {
        var checkbox = $(this).find('.chkVendorType');
        var spanText = $(this).find('span').html().trim();
        $(this).replaceWith('<span class="lblVendorType"><input type="radio" name="rdoIcType" id="' + checkbox.attr("id") + '"  class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '"><span class="MarginL">' + spanText + '</span></span>');
    });
    ReplaceStaffCheckboxToRadio();
    $('#chkAllVType').prop("checked", false);
    //$('#chkAllVType').click();
    $('#allChkLi').hide();
    $('#dvStaffTitleList #allChkLi').hide();
    DisabledInEdit(false);
}
function Insurance() {
    ResetRecord(2, 0, 1);
    $('#insuranceDiv').show();
    $('#backgroundDiv').hide();
    $('#profLicenseDiv').hide();
    $('#financialDiv').hide();
    $('#additionalReqDiv').hide();
    $('.lblVendorType').each(function () {
        var checkbox = $(this).find('.chkVendorType');
        var spanText = $(this).find('span').html().trim();
        if (checkbox.prop("checked") == true)
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" checked="' + checkbox.attr("checked") + '"/><span class="MarginL">' + spanText + '</span></span>');
        else
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" /><span class="MarginL">' + spanText + '</span></span>');
    });
    RebindStaffList();
    $('#allChkLi').show();
    $('#dvStaffTitleList #allChkLi').show();
    DisabledInEdit(false);

}
function AdditionalReqiurement() {
    ResetRecord(3, 0, 1);
    $('#additionalReqDiv').show();
    $('#backgroundDiv').hide();
    $('#profLicenseDiv').hide();
    $('#insuranceDiv').hide();
    $('#financialDiv').hide();
    $('.lblVendorType').each(function () {
        var checkbox = $(this).find('.chkVendorType');
        var spanText = $(this).find('span').html().trim();
        if (checkbox.prop("checked") == true)
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" checked="' + checkbox.attr("checked") + '"/><span class="MarginL">' + spanText + '</span></span>');
        else
            $(this).replaceWith('<span class="lblVendorType"><input type="checkbox" name="rdoIcType" id="' + checkbox.attr("id") + '" class="' + checkbox.attr("class") + '" value="' + checkbox.attr("value") + '" onchange="SetAllCheckBox()" /><span class="MarginL">' + spanText + '</span></span>');
    });
    RebindStaffList();
    $('#allChkLi').show();
    $('#dvStaffTitleList #allChkLi').show();
    DisabledInEdit(false);
}
function ValidateComplianceSections() {
    var isValidateForIcType = false;
    var isValidateForClient = false;
    var isValidateForTitles = false;
    if ($('input[name="rad_JCRfor"][value="IC"]').prop("checked")) {
        $('.chkVendorType').each(function () {
            if ($(this).prop("checked")) {
                isValidateForIcType = true;
                return false;
            }
        });
    } else if ($('input[name="rad_JCRfor"][value="Staff"]').prop("checked")) {
        $('.chkTitle').each(function () {
            if ($(this).prop("checked")) {
                isValidateForTitles = true;
                return false;
            }
        });
    }
    if ($('#internalrdo').prop("checked")) {
        isValidateForClient = true
    }
    else if ($('#subclientrdo').prop("checked")) {
        $('.chkSubClients').each(function () {
            isValidateForClient = true;
            return false;
        })
    }
    // alert(isValidateForIcType)
    // alert(isValidateForClient)
    if (!isValidateForIcType) { $('#valMsgForIcType').html("Please select ic type"); } else { $('#valMsgForIcType').html(""); }
    if (!isValidateForTitles) { $('#valMsgForTitles').html("Please select titles"); } else { $('#valMsgForTitles').html(""); }
    if (!isValidateForClient) { $('#valMsgForClients').html("Please select reqiured by"); } else { $('#valMsgForClients').html(""); }
    if (isValidateForClient && (isValidateForIcType || isValidateForTitles)) { return true; } else { return false; }
}
function SetICTypeAndClientsForBgCheck(type) {
    if ($('#BgCheck').prop("checked")) {
        var Str = "";
        var titleStr = "";
        if (type == 0) {
            $('#icTypeDiv').hide();
            if ($('input[name="rad_JCRfor"][value="IC"]').prop("checked")) {
                $('#icTypeDiv').show();
                $('#staffTitleDiv').hide();
                $('#icTypeDiv').empty();
                var icTypeStr = "<ul style='list-style: none;'>";

                $('.chkVendorType').each(function () {
                    if ($(this).prop("checked")) {
                        Str = Str + "<li>" + $(this).parent().find('span').html() + "</li>";
                    }
                });

                icTypeStr = icTypeStr + Str + "</ul>";
                $('#icTypeDiv').append(icTypeStr);
                Str = "";
            }
        }
        else {
            $('#clientsDiv').empty();
            var ClientsStr = "<ul style='list-style: none;'>";
            //Set Cleints Ids
            if ($('#internalrdo').prop("checked")) {
                Str = Str + "<li>" + $('#intercompname').html() + "</li>";
            }
            else if ($('#subclientrdo').prop("checked")) {
                $('.chkSubClients').each(function () {
                    if ($(this).prop("checked")) {
                        Str = Str + "<li>" + $(this).parent().find('span').html() + "</li>";
                    }
                })
            }
            ClientsStr = ClientsStr + Str + "</ul>";

            $('#clientsDiv').append(ClientsStr);
            Str = "";
        }
    }
}
function GetClientIds(UserId, CompanyId) {
    var clientIds = [];
    //Set Cleints Ids
    if ($('#internalrdo').prop("checked")) {
        clientIds.push(UserId + ":" + CompanyId);
    }
    else if ($('#subclientrdo').prop("checked")) {
        $('.chkSubClients').each(function () {
            if ($(this).prop("checked")) {
                clientIds.push($(this).val());
            }
        });
    }
    return clientIds;
}
function GetICTypeStaffTitleIds() {
    var icTypeStaffTitleIds = [];
    //set IC Type Ids
    if ($('input[type=radio][name="rad_JCRfor"][value="Staff"]').prop('checked')) {
        $('.chkTitle').each(function () {
            if ($(this).prop("checked")) {
                icTypeStaffTitleIds.push(parseInt($(this).val()));
            }
        });

    } else {
        $('.chkVendorType').each(function () {
            if ($(this).prop("checked")) {
                icTypeStaffTitleIds.push(parseInt($(this).val()));
            }
        });

    }

    return icTypeStaffTitleIds;
}
//Add more ack
function AddMoreAck(ids) {

    var id = $('#' + ids + 'Tbl tr').length;

    if (validatereqaction(ids)) {
        //$('#' + ids + 'Tbl tr:last').after('<tr><td><input id="' + ids + 'Chk_' + id + '" type="checkbox" style="margin-top:-13px;" checked="checked" /></td> ' +
        $('#' + ids + 'Tbl tr:last').after('<tr><td><input id="' + ids + 'Chk_' + id + '" type="checkbox" style="margin-top:-13px;"  /></td> ' +
   '<td><input id="' + ids + 'Txt_' + id + '" class="span12" placeholder="Add Acknowledgements..." type="text" maxlength="250" style="margin-top:-1px;" /></td>' +
   '<td><img id="' + ids + '_Del_' + id + '"  src="/Content/images/cross-button.png" style="margin-left:6px;cursor:pointer;margin-top:-11px;" title="Remove Ack" onclick="RemoveAction(this.id)">' +
        '<input type="hidden" id="' + ids + 'Hid_' + id + '" value="0"/></td></tr>');
        $('#' + ids + 'Txt_' + id + '').focus();

    }
}
//Validate Ack
function validatereqaction(id) {
    var ret = true;
    if ($('#' + id + 'Tbl tbody tr').length > 0) {
        $('#' + id + 'Tbl tbody tr').each(function (index) {
            var lnk = $(this).find('input[type="text"]');
            if (lnk.length) {
                if (lnk.val().trim() == null || lnk.val().trim() == "") {
                    lnk.addClass('errorClass');
                    ret = false;
                }
                else if (lnk.val().trim() != null || lnk.val().trim() != "") {
                    lnk.removeClass('errorClass');
                    ret = true;
                }
            }
        });
    }
    return ret;
}
//Remove Actions
function RemoveAction(id) {
    var idIndex1 = id.split('_')[2];
    var idIndex2 = id.split('_')[0];
    if ($('#' + idIndex2 + 'Tbl tbody tr').length == 1) {
        $('#' + idIndex2 + 'Txt_' + idIndex1 + '').val('');
    }
    else {
        $('#' + idIndex2 + 'Txt_' + idIndex1 + '').closest('tr').remove();
    }
}
function CheckInternal() {

    var chkCount = 0;
    var subClientCount = $('.chkSubClients').length;
    $('.chkSubClients').each(function () {
        if ($(this).prop("checked") == false) {
            chkCount = chkCount + 1;
        }
    });
    if (subClientCount == chkCount) {
        $("#subclientrdo").prop("checked", false);
    }
    else {
        $("#subclientrdo").prop("checked", true);
    }
    SetICTypeAndClientsForBgCheck(1);
}

//Used for save background check details
function SaveBackgroundDetails(ROOT, UserId, CompanyId) {
    var icTypeStaffTitleIds = [];
    icTypeStaffTitleIds = GetICTypeStaffTitleIds();
    var clientsIds = [];
    clientsIds = GetClientIds(UserId, CompanyId);
    var operationType = 0;
    if ($('#backgroundSave').attr('title') == "Edit") {
        operationType = 1;
    }

    if (ValidateComplianceSections()) {
        $.ajax({
            url: ROOT + 'JobComplianceRequirement/SaveJobComplianceReqiurements',
            cache: false,
            type: 'POST',
            data: JSON.stringify({
                ClientsIds: clientsIds,
                ICTypeStaffTitleIds: icTypeStaffTitleIds,
                BgCheckPkgId: $('#BgCheckPkgId').val(),
                OperationType: operationType,
                Type: 0,
                Source: $('#hidBGCheckSource').val()
            }),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                //window.location.reload();
                $('#currentBGCheckDetails').html($(data).find("#currentBGCheckDetails").html().trim());
                Background();
                $('#divManualInviteOuter').hide();
                $('#divManualInviteInner').hide();

                //GetJCRDetails(0, ROOT);
            },
            beforeSend: function (jqXHR, obj) {

                $('#divManualInviteOuter').show();
                $('#divManualInviteInner').show();
                $('#dvPopUpMsg').text('Please wait...');

            },
            error: function (data) {

            }
        });
    }
}
function GetJCRComplianceSection(type, ROOT) {
    IsStaffTitle = $('input[type=radio][name="rad_JCRfor"][value="Staff"]').prop('checked');
    $.ajax({
        url: ROOT + 'JobComplianceRequirement/GetJCRComplianceSection',
        data: {
            jsComplianceSection: type,
            jsIsStaffTitle: IsStaffTitle
        },
        cache: false,
        type: 'GET',
        async: true,
        success: function (data) {

            if (type == 0) {
                $('#currentBGCheckDetails').html($(data).find("#currentBGCheckDetails").html().trim());
            }
            if (type == 1) {
                $('#currentProflicDetails').html($(data).find("#currentProflicDetails").html().trim());
            }
            if (type == 2) {
                $('#insuranceDiv').html($(data).find("#insuranceDiv").html().trim());
            }
            if (type == 3) {
                $('#currentAddReqDetails').html($(data).find("#currentAddReqDetails").html().trim());
            }
            ResetRecord(type, 0, 0);
            $('#divManualInviteOuter').hide();
            $('#divManualInviteInner').hide();
        },
        error: function (data) {

        }
    });
}
function GetJCRDetails(type, ROOT) {
    $.ajax({
        url: ROOT + 'JobComplianceRequirement/JobComplianceReqiurements',
        cache: false,
        type: 'GET',
        async: true,
        success: function (data) {

            if (type == 0) {
                $('#backgroundDiv').html($(data).find("#backgroundDiv").html().trim());
            }
            if (type == 1) {
                $('#profLicenseDiv').html($(data).find("#profLicenseDiv").html().trim());
            }
            if (type == 2) {
                $('#insuranceDiv').html($(data).find("#insuranceDiv").html().trim());
            }
            if (type == 3) {
                $('#additionalReqDiv').html($(data).find("#additionalReqDiv").html().trim());
            }
            ResetRecord(type, 0, 0);
            $('#divManualInviteOuter').hide();
            $('#divManualInviteInner').hide();
        },
        error: function (data) {

        }
    });
}
function SetBgCheckPkgId(value, title, type) {
    $('#BgCheckPkgId').val(0);
    $('.chkBGCheck').each(function () {
        //alert($(this).val());
        if ($(this).prop("checked")) {
            // alert($(this).val());
            if ($('#BgCheckPkgId').val() == 0) {
                $('#BgCheckPkgId').val($(this).val());
            } else {
                $('#BgCheckPkgId').val($(this).val() + "," + $('#BgCheckPkgId').val());
            }
        }
    });


    if (value == 3)//static id for premium package from [DP_BGCheckPackageMaster]
    {
        $('#4').prop("checked", false);
        $('#4').prop("disabled", true); //static id for professional license package from [DP_BGCheckPackageMaster]
    }
    else if (type != 1) {
        $('#4').removeAttr("disabled");//static id for professional license package from [DP_BGCheckPackageMaster]
    }
    $('#hidBGCheckSource').val(title);
}
function setAcks(id) {
    var Ack = [];
    $('#' + id + 'Tbl tr').each(function (index) {
        if ($(this).find("#" + id + "Chk_" + index).is(":checked")) {

            var req = $(this).find("#" + id + "Txt_" + index).val().trim();
            Ack.push(req);
        }
    })
    // alert(Ack);
    return Ack;
}
function SaveProfeLicenseDetails(ROOT, UserId, CompanyId) {
    var icTypeStaffTitleIds = [];
    icTypeStaffTitleIds = GetICTypeStaffTitleIds();
    var clientsIds = [];
    clientsIds = GetClientIds(UserId, CompanyId);

    var Ack = setAcks("licenseAck");
    var source = "Sterling";
    if ($('#isVerReq').prop("checked") == false) {
        source = "Other";
    }

    if (ValidateComplianceSections()) {
        IsStaffTitle = $('input[type=radio][name="rad_JCRfor"][value="Staff"]').prop('checked');
        $.ajax({
            url: ROOT + 'JobComplianceRequirement/SaveJobComplianceReqiurements',
            cache: false,
            type: 'POST',
            data: JSON.stringify({
                ClientsIds: clientsIds,
                ICTypeStaffTitleIds: icTypeStaffTitleIds,
                IsStaffTitle: IsStaffTitle,
                LicenseType: $('#profLicenseType option:selected').text(),
                Title: $('#profLicenseTitle').val(),
                Acknowleagement: Ack,
                Type: 1,
                OperationType: 1,
                Source: source,
                ProfLicTblId: $('#hidProfLicId').val()//0 for save and 1 for update
            }),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                //window.location.reload();
                $('#currentProflicDetails').html($(data).find("#currentProflicDetails").html().trim());
                ProfessionalLicense();
                $('#divManualInviteOuter').hide();
                $('#divManualInviteInner').hide();
                // GetJCRDetails(1, ROOT);
            },
            beforeSend: function (jqXHR, obj) {

                $('#divManualInviteOuter').show();
                $('#divManualInviteInner').show();
                var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
                var poistion = scrollTop;
                $('#divManualInviteInner').css("top", poistion);
                $('#dvPopUpMsg').text('Please wait...');

            },
            error: function (data) {

            }
        });
    }
}
function SaveInsuranceDetails(ROOT, UserId, CompanyId) {
    var icTypeStaffTitleIds = [];
    icTypeStaffTitleIds = GetICTypeStaffTitleIds();
    var clientsIds = [];
    clientsIds = GetClientIds(UserId, CompanyId);

    var insSource = '';
    var insVerType = '';
    if ($('#verifiedIns').prop("checked") == true) {
        insSource = "Sterling";
        insVerType = $('#verifiedIns').val();
    }
    else if ($('#selfRepProfIns').prop("checked") == true) {
        insSource = "Other";
        insVerType = $('#selfRepProfIns').val();
    }
    if ($('#selfRepDeclIns').prop("checked") == true) {
        insSource = "Other";
        insVerType = $('#selfRepDeclIns').val();
    }
    var Ack = setAcks("insuranceAck");
    if (ValidateComplianceSections()) {
        $.ajax({
            url: ROOT + 'JobComplianceRequirement/SaveJobComplianceReqiurements',
            cache: false,
            type: 'POST',
            data: JSON.stringify({
                ClientsIds: clientsIds,
                ICTypeStaffTitleIds: icTypeStaffTitleIds,
                InsuranceType: $('#insuranceType option:selected').text(),
                Title: $('#insuranceTitleTxt').val(),
                Acknowleagement: Ack,
                Type: 2,
                OperationType: 1,
                InsTblId: $('#hidInsId').val(),
                InsuranceVerType: insVerType,
                Source: insSource
            }),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#insuranceDiv').html($(data).find("#insuranceDiv").html().trim());
                Insurance();
                $('#divManualInviteOuter').hide();
                $('#divManualInviteInner').hide();
                //window.location.reload();
                // GetJCRDetails(2, ROOT);
            },
            beforeSend: function (jqXHR, obj) {

                $('#divManualInviteOuter').show();
                $('#divManualInviteInner').show();
                $('#dvPopUpMsg').text('Please wait...');

            },
            error: function (data) {

            }
        });
    }
}

function SaveAdditionalReqiurement(ROOT, UserId, CompanyId) {
    // alert(0);
    var icTypeStaffTitleIds = [];
    icTypeStaffTitleIds = GetICTypeStaffTitleIds();
    var clientsIds = [];
    clientsIds = GetClientIds(UserId, CompanyId);
    // alert(clientsIds+" hsgd");
    //var operationType = 0;
    //if ($('#additionalReqSave').attr('title') == "Edit") {
    //    operationType = 1;
    //}
    var Ack = setAcks("additionalReq");
    var docLocation = '';
    $('#addReqDocFiles a').each(function () {
        docLocation += $(this).attr('href') + '#' + $(this).html().split('_')[1] + ';';
    });
    if (ValidateComplianceSections()) {
        IsStaffTitle = $('input[type=radio][name="rad_JCRfor"][value="Staff"]').prop('checked');
        $.ajax({
            url: ROOT + 'JobComplianceRequirement/SaveJobComplianceReqiurements',
            cache: false,
            type: 'POST',
            data: JSON.stringify({
                ClientsIds: clientsIds,
                ICTypeStaffTitleIds: icTypeStaffTitleIds,
                Title: $('#addiReqTitle').val(),
                Acknowleagement: Ack,
                Review: $('#gretake').val(),
                UploadedBy: $('#addiReqUploaded').val(),
                Type: 3,
                OperationType: 1,
                AdditionalReqTblId: $('#hidAddReqId').val(),
                UploadedDocs: docLocation,
                ExpirationDate: $('#txtAddReqExpirationDate').val(),
                IsStaffTitle: IsStaffTitle
            }),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                alert($(data).find("#currentAddReqDetails").html().trim());
                ////window.location.reload();
                ////$('#additionalReqDiv').html(data);
                $('#currentAddReqDetails').html($(data).find("#currentAddReqDetails").html().trim());
                $('#divManualInviteOuter').hide();
                $('#divManualInviteInner').hide();
                AdditionalReqiurement();
                //GetJCRDetails(3, ROOT);
            },
            beforeSend: function (jqXHR, obj) {

                $('#divManualInviteOuter').show();
                $('#divManualInviteInner').show();
                $('#dvPopUpMsg').text('Please wait...');

            },
            error: function (data) { }
        });
    }
}
function GetBGDetailForEdit(requiredByCompanyId, icTypeId, bgCheckId, loginUserCompanyId) {
    //reset record
    ResetRecord(0, 1, 0);
    //set reqiured by
    if (requiredByCompanyId == loginUserCompanyId) {
        $('#internalrdo').prop("checked", true);
    }
    else {
        $('#subclientrdo').prop("checked", true);
        $('.chkSubClients').each(function () {
            if ($(this).val().split(':')[1] == requiredByCompanyId) {
                $(this).prop("checked", true);
            }
        });
    }

    //set ic type
    $('.chkVendorType').each(function () {
        if ($(this).val() == icTypeId) {
            $(this).prop("checked", true);
        }
    });
    //set BG attrbute
    var pkgIds = [];
    pkgIds = bgCheckId.split(',');
    $(pkgIds).each(function (val) {
        var pkgId = pkgIds[val];
        $('.chkBGCheck').each(function () {
            if ($(this).val() == pkgId) {
                $(this).prop("checked", true);
                $(this).trigger('click');
            }
        });
    });
    $('#BgCheckPkgId').val(bgCheckId);
    //set ic type and reqiured by column values
    SetICTypeAndClientsForBgCheck(0);
    SetICTypeAndClientsForBgCheck(1);
    $('#backgroundSave').attr('title', "Edit");
    $('#bgCheckCancel').show();
    DisabledInEdit(true);
}
function GetAddiReqForEdit(requiredByCompanyId, icTypeId, title, addReqId, review, uploadedBy, loginUserCompanyId) {
    //reset record
    ResetRecord(3, 1, 0);
    //set reqiured by
    if (requiredByCompanyId == loginUserCompanyId) {
        $('#internalrdo').prop("checked", true);
    }
    else {
        $('#subclientrdo').prop("checked", true);
        $('.chkSubClients').each(function () {
            if ($(this).val().split(':')[1] == requiredByCompanyId) {
                $(this).prop("checked", true);
            }
        });
    }
    //set ic type
    $('.chkVendorType').each(function () {
        if ($(this).val() == icTypeId) {
            $(this).prop("checked", true);
        }
    });
    //set uploaded by and uploaded title
    $("#addiReqUploaded option").each(function () {
        if ($(this).val() == uploadedBy) {
            $(this).attr('selected', 'selected');
        }
    });
    $('#addiReqTitle').val(title);
    //set review
    $("#gretake option").each(function () {
        if ($(this).val() == review) {
            $(this).attr('selected', 'selected');
        }
    });
    //set add req Id
    $('#hidAddReqId').val(addReqId);
    $('#additionalReqSave').attr("title", "Edit");
    $('#additionalReqSave').css("margin-right", "4px");
    GetAckDetails(2, addReqId);
    $('#addReqCancel').show();
    DisabledInEdit(true);
    GetUploadedDocDetails(2, addReqId, "");
}
function GetInsForEdit(requiredByCompanyId, icTypeId, insuranceType, insId, loginUserCompanyId, InsVer) {
    //reset record
    ResetRecord(2, 1, 0);
    //set reqiured by
    if (requiredByCompanyId == loginUserCompanyId) {
        $('#internalrdo').prop("checked", true);
    }
    else {
        $('#subclientrdo').prop("checked", true);
        $('.chkSubClients').each(function () {
            if ($(this).val().split(':')[1] == requiredByCompanyId) {
                $(this).prop("checked", true);
            }
        });
    }
    //set ic type
    $('.chkVendorType').each(function () {
        if ($(this).val() == icTypeId) {
            $(this).prop("checked", true);
        }
    });
    //set insurance type and upload title
    $("#insuranceType option").each(function () {
        if (!($(this).html().indexOf('&amp;') === -1)) {
            if ($(this).html().replace('&amp;', '&') == insuranceType) {
                $(this).attr('selected', 'selected');
            }

        }
        else {
            if ($(this).html() == insuranceType) {
                $(this).attr('selected', 'selected');
            }
        }
    });
    $('#insuranceTitleTxt').val(insuranceType);
    //set insurance Id
    $('#hidInsId').val(insId);
    $('#insuranceSave').attr("title", "Edit");
    GetAckDetails(1, insId);
    $('#insCancel').show();
    DisabledInEdit(true);
    //set insurance type and upload title

    $(".insVer").each(function () {
        //alert(InsVer);
        if ($(this).val() == InsVer) {
            $(this).prop('checked', true);
        }
    });

}
function GetProfLiceForEdit(requiredByCompanyId, icTypeId, licenseType, licInsId, loginUserCompanyId, source) {
    //reset record
    ResetRecord(1, 1, 0);
    //set reqiured by
    if (requiredByCompanyId == loginUserCompanyId) {
        $('#internalrdo').prop("checked", true);
    }
    else {
        $('#subclientrdo').prop("checked", true);
        $('.chkSubClients').each(function () {
            if ($(this).val().split(':')[1] == requiredByCompanyId) {
                $(this).prop("checked", true);
            }
        });
    }
    //set ic type
    $('.chkVendorType').each(function () {
        if ($(this).val() == icTypeId) {
            $(this).prop("checked", true);
        }
    });
    if (source == "Sterling") {
        $('#isVerReq').prop("checked", true);
    } else {
        $('#isVerReq').prop("checked", false);
        $('#isSelfRep').prop("checked", true);
    }

    $('#profLicenseTitle').val(licenseType);
    //set source
    $("#profLicenseType option").each(function () {
        if ($(this).html() == licenseType) {
            $(this).attr('selected', 'selected');
        }
    });
    //set prof license Id
    $('#hidProfLicId').val(licInsId);
    $('#profLicenseSave').attr("title", "Edit");
    GetAckDetails(0, licInsId);
    $('#profLicenseCancel').show();
    DisabledInEdit(true);
}

function GetAckDetails(type, tblMapId) {
    $.ajax({
        url: '/JobComplianceRequirement/GetAckByLicInsId',
        cache: false,
        type: 'GET',
        data: {
            tblMapId: tblMapId,
            type: type
        },
        async: true,
        success: function (result) {
            result = $.parseJSON(result);
            var tblId = "licenseAck";
            if (type == 1) {
                tblId = "insuranceAck";
            }
            else if (type == 2) {
                tblId = "additionalReq";
            }
            SetAck(result, tblId);
        },
        error: function (result) { }
    });
}

function SetAck(result, tblId) {
    debugger;
    var str = '';
    $('#' + tblId + 'Tbl').empty();
    if (result.length > 0) {
        $.each(result, function (id, item) {
            //var id = "";
            //id = $('#' + tblId + 'Tbl tr').length;
            //str = str + '<tr><td><input id="' + tblId + 'Chk_' + id + '" type="checkbox" style="margin-top:-13px;" checked="checked" /> </td>' +
            item = '';
            str = str + '<tr><td><input id="' + tblId + 'Chk_' + id + '" type="checkbox" style="margin-top:-13px;" /> </td>' +
  '<td><input id="' + tblId + 'Txt_' + id + '" class="span12" placeholder="Add Acknowledgements..." type="text" value="" maxlength="250" style="margin-top:-1px;" /></td>' +
  '<td><img id="' + tblId + '_Del_' + id + '"  src="/Content/images/cross-button.png" style="margin-left:6px;cursor:pointer;margin-top:-11px;" title="Remove Ack" onclick="RemoveAction(this.id)">' +
        '<input type="hidden" id="' + tblId + 'Hid_' + id + '" value="0"/></td></tr>'

        });
    }
    else {
        $('#' + tblId + 'Tbl').append("<tr><td>No Acknowledgements</td></tr>");
    }
    $('#' + tblId + 'Tbl').append(str);
}

function ResetRecord(radioType, operationType, type) {
    var ackTblId = "licenseAck";
    if (type == 0) {
        $('.chkVendorType').each(function () {
            $(this).prop("checked", false);
        });
        $('.chkSubClients').each(function () {
            $(this).prop("checked", false);
        });
        $('#internalrdo').prop("checked", false);
        $('#subclientrdo').prop("checked", false);
        $('#chkAllVType').prop("checked", false);

    }

    if (radioType == 0) {
        $('.chkBGCheck').each(function () {
            $(this).prop("checked", false);
        });
        $('#backgroundSave').css("margin-right", "4px");
        $('#backgroundSave').attr('title', "Save");
        $('#bgCheckCancel').hide();
        $('#BgCheckPkgId').val(0);
    }
    if (radioType == 1) { $('#profLicenseCancel').hide(); $('#profLicenseType').val("0"); $('#profLicenseSave').attr("title", "Save"); }
    if (radioType == 2) { ackTblId = "insuranceAck"; $('#insCancel').hide(); $('#insuranceType').val("0"); $('#insuranceSave').attr("title", "Save"); }
    else if (radioType == 3) {
        ackTblId = "additionalReq";
        $('#additionalReqSave').css("margin-right", "73px");
        $('#addReqCancel').hide();
        $('#addiReqTitle').val('');
        $('#txtAddReqExpirationDate').val('');
        $('#addiReqUploaded').val("0");
        $('#gretake').val("0");
        $('#additionalReqSave').attr("title", "Save");
    }
    $('#' + ackTblId + 'Tbl').empty();
    if (radioType != 0 && operationType != 1) {
        //$('#' + ackTblId + 'Tbl').append('<tr><td style="padding-bottom: 8px;"><input type="checkbox" id="' + ackTblId + 'Chk_0" style="margin-top: -7px;" checked="checked"></td>' +
        $('#' + ackTblId + 'Tbl').append('<tr><td style="padding-bottom: 8px;"><input type="checkbox" id="' + ackTblId + 'Chk_0" style="margin-top: -7px;" ></td>' +
     '<td><input id="' + ackTblId + 'Txt_0" class="span12" type="text" placeholder="Add Acknowledgements..." value="" maxlength="250" />' +
       '</td><td><input type="hidden" id="' + ackTblId + 'Hid_0" value="0" />&nbsp;</td></tr>');
    }
    DisabledInEdit(false);
}

function DisabledInEdit(type) {
    if (type) {
        $('#internalrdo').prop("disabled", true);
        $('#subclientrdo').prop("disabled", true);
        $('.chkSubClients').each(function () {
            $(this).prop("disabled", true);
        });
        $('.chkVendorType').each(function () {
            $(this).prop("disabled", true);
        });
        $("#chkAllVType").prop("disabled", true);

    } else {
        $('#internalrdo').prop("disabled", false);
        $('#subclientrdo').prop("disabled", false);
        $('.chkSubClients').each(function () {
            $(this).prop("disabled", false);
        });
        $('.chkVendorType').each(function () {
            $(this).prop("disabled", false);
        });
        $("#chkAllVType").prop("disabled", false);
    }
}

function GetAndBindActiveOrDeactiveJCR(type, ROOT, operationType) {
    $.ajax({
        url: ROOT + 'JobComplianceRequirement/JobComplianceReqiurements',
        cache: false,
        type: 'GET',
        async: true,
        success: function (data) {
            if (type == 0) {
                if (operationType) {
                    $('#pastBGCheckDetails').html($(data).find("#pastBGCheckDetails").html().trim());
                } else {
                    $('#currentBGCheckDetails').html($(data).find("#currentBGCheckDetails").html().trim());
                }
            }
            if (type == 1) {
                if (operationType) {
                    $('#pastProflicDetails').html($(data).find("#pastProflicDetails").html().trim());
                } else {
                    $('#currentProflicDetails').html($(data).find("#currentProflicDetails").html().trim());
                }
            }
            if (type == 2) {
                if (operationType) {
                    $('#pastInsDetails').html($(data).find("#pastInsDetails").html().trim());
                } else {
                    $('#currentInsDetails').html($(data).find("#currentInsDetails").html().trim());
                }
            }
            if (type == 3) {
                if (operationType) {
                    $('#pastAddReqDetails').html($(data).find("#pastAddReqDetails").html().trim());
                } else {
                    $('#currentAddReqDetails').html($(data).find("#currentAddReqDetails").html().trim());
                }
            }
            $('#divManualInviteOuter').hide();
            $('#divManualInviteInner').hide();
        },
        error: function (data) {

        }
    });
}

function ActiveOrDeactivateJCR(operationType, ROOT, type, tblId) {

    $.ajax({
        url: ROOT + 'JobComplianceRequirement/ActiveOrDeactivateJCR',
        cache: false,
        type: 'GET',
        data: {
            tblId: tblId,
            status: operationType,
            type: type
        },
        async: true,
        success: function (result) {
            GetAndBindActiveOrDeactiveJCR(type, ROOT, operationType);
        },
        beforeSend: function (jqXHR, obj) {

            $('#divManualInviteOuter').show();
            $('#divManualInviteInner').show();
            //var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
            //var poistion = scrollTop;
            //alert(poistion);
            //$('#divManualInviteInner').css("top", poistion);
            $('#dvPopUpMsg').text('Please wait...');

        },
        error: function (result) { }
    });
}
function ShowCurrentOrPastDetails(type, operationType) {
    if (operationType) { operationType = false; } else { operationType = true; }
    GetAndBindActiveOrDeactiveJCR(type, "/", operationType);
    if (type == 0) {
        if (operationType) {

            $('#currentBGCheckDetails').hide();
            $('#pastBGCheckDetails').show();
        } else {
            $('#pastBGCheckDetails').hide();
            $('#currentBGCheckDetails').show();


        }
    }
    if (type == 1) {
        if (operationType) {
            $('#currentProflicDetails').hide();
            $('#pastProflicDetails').show();
        } else {
            $('#pastProflicDetails').hide();
            $('#currentProflicDetails').show();
        }
    }
    if (type == 2) {
        if (operationType) {
            $('#currentInsDetails').hide();
            $('#pastInsDetails').show();
        } else {
            $('#pastInsDetails').hide();
            $('#currentInsDetails').show();
        }
    }
    if (type == 3) {
        if (operationType) {
            $('#currentAddReqDetails').hide();
            $('#pastAddReqDetails').show();
        } else {
            $('#pastAddReqDetails').hide();
            $('#currentAddReqDetails').show();
        }
    }

}

function GetUploadedDocDetails(type, tblMapId, addReqCondition) {
    $.ajax({
        url: '/JobComplianceRequirement/GetUploadedDocs',
        cache: false,
        type: 'GET',
        data: {
            tblMapId: tblMapId,
            type: type
        },
        async: true,
        success: function (result) {

            result = $.parseJSON(result);
            var tblId = "uploadedprofLicfiles";
            if (type == 1) {
                tblId = "uploadedinsdocfiles";
            }
            else if (type == 2) {
                tblId = "uploadedaddreqdocfiles";
            }
            SetUploadedDocDetails(result, tblId, addReqCondition);
        },
        error: function (result) { }
    });
}
function SetUploadedDocDetails(result, tblId, addReqCondition) {
    $('#' + tblId).html("");
    var count = "";
    var fileurl = "";
    var url1 = "";
    var UploadedDocDate = "";
    $.each(result, function (i, item) {
        UploadedDocDate = new Date(parseInt(item.UploadedDocDate.substr(6)));
        //alert(UploadedDocDate);
        if (item.UploadedDocDate != null) {
            UploadedDocDate = '_' + dateFormat(new Date(UploadedDocDate), 'mm/dd/yyyy');
        }
        else {
            UploadedDocDate = '';
        }
        count = $('#' + tblId + ' div').length;
        url1 = item.DocLoc;
        // alert(tblId, +"  " + addReqCondition);UploadedDocDate
        var title = url1.replace(/^.*[\\\/]/, '').split('&')[0].split('.')[0];
        //alert($('#hid_ParentUserid').val());
        if (addReqCondition != "Company" && ($('#hid_ParentUserid').val() == null || $('#hid_ParentUserid').val() == '')) {
            $("#" + tblId).append('<div id="div_' + count + '" style="border-radius: 5px 5px 5px 5px;  width:auto;height:auto;float:left;"><a id="docfile_' + count + '" href="' + url1 + '" target="_blank" title=' + title + '_' + item.DocSeq + '<style="cursor:pointer;margin-left:6px;" >' + title.replace(/\s/g, '') + UploadedDocDate + '</a><img id="Eimg' + count + '" src="@Url.Content("~/Content/images/cross-button.png")" class="delDoc" style="margin-left:6px;cursor:pointer;" title="remove document" onclick="deletedoc(' + count + ')">' +
                                   '<input type="hidden" id="doccontentid_' + count + '" value="' + item.DocTblId + '" /></div>');
        } else {
            $("#" + tblId).append('<div id="div_' + count + '" style="border-radius: 5px 5px 5px 5px;  width:auto;height:auto;float:left;"><a id="docfile_' + count + '" href="' + url1 + '" target="_blank" title=' + title + '_' + item.DocSeq + '<style="cursor:pointer;margin-left:6px;" >' + title.replace(/\s/g, '') + UploadedDocDate + '</a>' +
                                       '<input type="hidden" id="doccontentid_' + count + '" value="' + item.DocTblId + '" /></div>');
        }

    });
}
// handler to set values on document load event
$(document).on("load", function (e) {
    $('input[type="text"][placeholder="Add Reqiured Action..."]').prop('placeholder', 'Add Acknowledgements...');
});
// handler to set values on document(DOM) ready event
$(document).ready(function (e) {
    $('#dvStaffTitleList').hide();
    $('input[type="text"][placeholder="Add Reqiured Action..."]').prop('placeholder', 'Add Acknowledgements...');
    $('input[name="rad_JCRfor"][value="IC"]').click();

    // function for to handle radio button IC and Staff click
    $('input[name="rad_JCRfor"]').click(function (e) { ///$('input[name="rad_JCRfor"]:checked')
        //var t = IsValidBackgroundProperties();

        if ($(this).val() == 'IC') {
            $('#valMsgForIcType').html("");
            $('input[name="complianceSec"][value="IN"]').parent().show();
            $('#dvHeaderForIC_Staff').html('<strong>IC Type</strong>');
            $('#dvStaffTitleList').hide();
            $('#dvICTypeDetails').show();
            $('#userTypeLbl').html('IC type(s)');
            $('.span4 #ulPackageList').show();
            $('#backgroundSave').show();
            $('#dvBackgroundAttributes').show();
            $('#staffTitleDiv').hide();
            $('#icTypeDiv').show();
            if ($('input[name="complianceSec"][value="IN"]').prop('checked')) {
                $('div #insuranceDiv').show();
            }
        } else if ($(this).val() == 'Staff') {
            $('input[name="complianceSec"][value="IN"]').parent().hide();
            $('#valMsgForTitles').html("");
            $('#dvHeaderForIC_Staff').html('<strong>Staff Titles</strong>');
            $('#staffTitleDiv').show();
            $('#icTypeDiv').hide();
            $('#dvStaffTitleList').show();
            $('#dvICTypeDetails').hide();
            $('#userTypeLbl').html('Staff title(s)');
            $('.span4 #ulPackageList').hide();
            $('#backgroundSave').hide();
            $('#dvBackgroundAttributes').hide();
            $('div #insuranceDiv').hide();
        }

        if ($('input[type=radio][name="rad_JCRfor"][value="Staff"]').prop('checked')) {

            $('#addiReqUploaded option[value="IC"]').remove();
            $('#addiReqUploaded option[value="Company"]').remove();
            $('#addiReqUploaded option[value="Staff"]').remove();
            $('#addiReqUploaded option[value="HR"]').remove();
            $('#addiReqUploaded')
                .append($("<option></option>")
                .attr("value", "Staff")
                .text("Uploaded by Staff"));
            $('#addiReqUploaded')
                .append($("<option></option>")
                .attr("value", "HR")
                .text("Uploaded by HR"));
            $('#dvAddReqReview').hide();
        } else if ($('input[type=radio][name="rad_JCRfor"][value="IC"]').prop('checked')) {

            $('#addiReqUploaded option[value="Staff"]').remove();
            $('#addiReqUploaded option[value="HR"]').remove();
            $('#addiReqUploaded option[value="IC"]').remove();
            $('#addiReqUploaded option[value="Company"]').remove();
            $('#addiReqUploaded')
                .append($("<option></option>")
                .attr("value", "IC")
                .text("Uploaded by IC"));
            $('#addiReqUploaded')
                .append($("<option></option>")
                .attr("value", "Company")
                .text("Uploaded by Client"));
            $('#dvAddReqReview').show();
        }
        // for insurance tab fields
        ToggleInsuranceFields();
        GetJCRComplianceSectionDetails();
        
    });
});

$('input[name="complianceSec"]').click(function (e) {
    alert(e.target.value);
    GetJCRComplianceSectionDetails(e);
});
// function to get jcr compliance section details
function GetJCRComplianceSectionDetails() {
    
    // get partial view for selected JCR (Staff OR IC)
    $('input[name="complianceSec"]').each(function () {
        if ($(this).prop("checked")) {
            var complianceSecValue = $(this).prop("value");
            switch (complianceSecValue) {

                case 'BG': GetJCRComplianceSection(type = 0, ROOT);
                    break;
                case 'PL': GetJCRComplianceSection(type = 1, ROOT);
                    break;
                case 'IN': GetJCRComplianceSection(type = 2, ROOT);
                    break;
                case 'AC': GetJCRComplianceSection(type = 3, ROOT);
                    break;
                case 'FI': GetJCRComplianceSection(type = 4, ROOT);
                    break;

                default:
                    break;
            }
        }
    });

}
// global namespace
var DPAPP = DPAPP || {};
// sub namespace
DPAPP.event = {};
DPAPP.JobComplianceRequirementModel = function (title, Type, operationType, licenseType) {
    this.ICTypeDetails = new Array();
    this.ClientsList = new Array();
    this.StateDetails = new Array();
    this.BackgroundPkgList = new Array();
    this.BackgroundMasterList = new Array();
    this.ProfLicMasterList = new Array();
    this.InsuranceMasterList = new Array();
    this.AddReqMasterList = new Array();
    this.ICTypeStaffTitleIds = new Array();
    this.Type = Type;
    this.OperationType = operationType;
    this.Title = title;
    this.LicenseType = licenseType;
}

var jobComplianceReqModel = new DPAPP.JobComplianceRequirementModel();
