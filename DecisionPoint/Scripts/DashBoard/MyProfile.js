//Delete Uploaded File From Directory
function DeleteDocumentFromDir(counter, divId, typeId, type, imgid, ROOT) {
    var divId = divId;

    //if (!typeId.indexOf('$') === -1) {
    //    divId = "userdocfiles1_0";
    //}
    //  alert(0);
    $("#" + divId + " div").each(function (index) {
        // alert('cme');
        var divhtml = $(this);

        var divcouter = $(divhtml).attr('id').split('_')[1];


        // if (index == counter) {
        if (divcouter == counter) {

            var filename = $(this).find('#docfile_' + counter + '');
            var imgname = $(this).find('#' + imgid);
            var fileLocation = $(this).find('#docfile_' + counter).attr('href').split('?')[1].split('&')[0].split('=')[1];
            // alert(fileLocation);
            var fileID = $(this).find('#doccontentid_' + counter).val();

            $.ajax({
                url: ROOT + 'CompanyDashBoard/DeleteFileFromDir',
                cache: false,
                type: "POST",
                data: JSON.stringify({
                    FileLocation: fileLocation,
                    Type: parseInt(type),
                    FileLocType: parseInt(typeId),
                    FileId: parseInt(fileID)

                }),
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    imgname.remove();
                    filename.remove();
                    divhtml.remove();
                }
            });
        }
    });
}

//Click On Publish Button from file upload Popup typeId,counterId,fileId
function PublishUploadedFile(globalPath, specificPath, electPath, nonelecPath, additionalreqPath, ROOT, divId, typeId, imgId) {



    $('body').css("overflow-y", "auto");
    var divId = divId;


    var count = $('#' + divId + ' div').length;
    //var imgId = count;
    //if (typeId == 1 || typeId == 0) {
    //    imgId = divId.substring(0, 1) + "img" + count;
    //}
    //  alert("ffff" + imgId);
    if (count > 1) {
        var status = "0";
        $('#' + divId + ' div').each(function (index) {
            if (index != 0) {
                var id = "#docfile_" + index;

                if ($(id).html() == $('#docfile').val()) {

                    status = "1";
                }
            }
        });

        if (status != 1) {

            var filetitle = $('#docfile').val();
            var fileurl = globalPath + $('#docfile').val().replace(/\s/g, '') + '';
            if (typeId == 1) {
                fileurl = specificPath + $('#docfile').val().replace(/\s/g, '') + '';
            }
            else if (typeId == 2) {
                fileurl = electPath + $('#docfile').val().replace(/\s/g, '') + '';
            }
            else if (typeId == 3) {
                // alert('come3')
                fileurl = nonelecPath + $('#docfile').val().replace(/\s/g, '') + '';
            }
            else if (typeId == 4) {


                fileurl = additionalreqPath + $('#docfile').val().replace(/\s/g, '') + '';
            }
            var ext = fileurl.substr(fileurl.lastIndexOf('.') + 1);

            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "tif" || ext == "bmp") {
                        var url1 = fileurl;
                    }
                    else {
                        var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
                    }
                }
                else {
                    var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
                }
            }
            else {
                var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
            }
           

            $("#" + divId).append('<div id="div_' + count + '" style="border-radius: 5px 5px 5px 5px; width:auto;height:auto;float:left;"><a id="docfile_' + count + '" href="' + url1 + '" target="_blank" title=' + filetitle + ' style="cursor:pointer;margin-left:1px;" >' + filetitle.split('.')[0] + '</a>' +
                '<img id=' + imgId + ' src="' + ROOT + 'Content/images/cross-button.png" style="cursor:pointer;" title="remove document" onclick="deletedoc(this.id)"/>' +
                            '<input type="hidden" class="IsTemp" value="0" /></div>');
        }
    }
    else {

        var filetitle = $('#docfile').val();
        var fileurl = globalPath + $('#docfile').val().replace(/\s/g, '') + '';
        if (typeId == 1) {
            fileurl = specificPath + $('#docfile').val().replace(/\s/g, '') + '';
        }
        else if (typeId == 2) {
            fileurl = electPath + $('#docfile').val().replace(/\s/g, '') + '';
        }
        else if (typeId == 3) {
            // alert('come3')
            fileurl = nonelecPath + $('#docfile').val().replace(/\s/g, '') + '';
        }
        else if (typeId == 4) {

            fileurl = additionalreqPath + $('#docfile').val().replace(/\s/g, '') + '';
        }

        var ext = fileurl.substr(fileurl.lastIndexOf('.') + 1);

        if ($('#IsDriverLic').length) {
            if ($('#IsDriverLic').val() == 1) {
                if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "tif" || ext == "bmp") {
                    var url1 = fileurl;
                }
                else {
                    var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
                }
            }
            else {
                var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
            }
        }
        else {
            var url1 = "http://docs.google.com/viewer?url=" + fileurl + "&embedded=true&chrome=false";
        }
        $("#" + divId).append('<div id="div_' + count + '" style="border-radius: 5px 5px 5px 5px; width:auto;height:auto;float:left;"><a id="docfile_' + count + '" href="' + url1 + '" target="_blank" title=' + filetitle + ' style="cursor:pointer;margin-left:1px;" >' + filetitle.split('.')[0] + '</a>' +
               '<img id=' + imgId + ' src="' + ROOT + 'Content/images/cross-button.png" style="cursor:pointer;" title="remove document" onclick="deletedoc(this.id)"/>' +
                           '<input type="hidden" class="IsTemp" value="0" /></div>');
    }

    CloseOpenPopup("previewdocinner", "previewdocouter");
}

//Set Uploaded File Name By check in particular directory
function SettheEmpDocName(title, ext, ROOT, fileurl, type) {

    var title = title;
    //alert(title + "   " + type);
    $.ajax({
        url: fileurl,
        data: { 'title': title.replace(/\s+/g, ''), 'type': parseInt(type) },
        cache: false,
        type: "POST",
        datatype: 'json',
        success: function (result) {
            $('#docfile').val('');
            //if ((title + ":contains($)")) {
            if (title.indexOf('$') === -1) {
                var namefile = '';
                if (result != null && result.length > 2) {
                    namefile = result.substring(1, (result.length));
                }
                $('#docfile').val(namefile + "." + ext);
            }
            else {
                var namefile = '';
                if (result != null && result.length > 2) {
                    namefile = result.split('$')[0] +
                        "!" + result.split('$')[1].substring(1, (result.split('$')[1].Length));

                    //namefile = result.substring(1, (result.length));
                }
                $('#docfile').val(namefile + "." + ext);

            }
            // alert(result);
            // alert($('#docfile').val());
            createcookie(result.replace(/\s/g, '') + "." + ext);
        }
    });
}



//Delete Cookies whci create forfile upload title
function erasecookie(ROOT, type) {
    if (type == 0) {
        $.ajax({
            url: ROOT + "CompanyDashBoard/CreateEraseTitleNameCookie/",
            data: { 'name': '', 'type': 1 },
            cache: false,
            type: "POST",
            datatype: 'json',
            success: function (result) {

            }
        });
    }
    else if (type == 2) {

        $.ajax({
            url: ROOT + "JobComplianceRequirement/CreateEraseTitleNameCookie/",
            data: { 'name': '', 'type': 1 },
            cache: false,
            type: "POST",
            datatype: 'json',
            success: function (result) {

            }
        });
    }
    else {
        $.ajax({
            url: ROOT + "UserDashBoard/CreateEraseTitleNameCookie/",
            data: { 'name': '', 'type': 1 },
            cache: false,
            type: "POST",
            datatype: 'json',
            success: function (result) {

            }
        });
    }
}
//Used for validate the uploaded document 
function setdocfilename(input) {
    //var fileName = input.files[0].name;
    var fileName = $("input[id='" + input.id + "']").val().split('/').pop().split('\\').pop();
    var ext = fileName.substr(fileName.lastIndexOf('.') + 1);
    
    switch (ext) {
        case 'ppt':
            $('#docfileName_0').html('');
            return true;
            break;
        case 'pptx':
            $('#docfileName_0').html('');
            return true;
            break;
        case 'pdf':
            $('#docfileName_0').html('');
            return true;
            break;
        case 'jpg':
            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    $('#docfileName_0').html('');
                    return true;
                }
                else { return false; }
            }
            else { return false; }
            break;
        case 'jpeg':
            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    $('#docfileName_0').html('');
                    return true;
                }
                else { return false; }
            }
            else { return false; }
            break;
        case 'bmp':
            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    $('#docfileName_0').html('');
                    return true;
                }
                else { return false; }
            }
            else { return false; }
            break;
        case 'png':
            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    $('#docfileName_0').html('');
                    return true;
                }
                else { return false; }
            }
            else { return false; }
            break;
        case 'tif':
            if ($('#IsDriverLic').length) {
                if ($('#IsDriverLic').val() == 1) {
                    $('#docfileName_0').html('');
                    return true;
                }
                else { return false; }
            }
            else { return false; }
            break;
        default:
            $('#docfileName_0').html("Not Allowed");

            return false;
    }


}

//Read Th euploaded file details 
function ReadUploadedFIle(input, id, ROOT, titleval, titleid, fileurl, type) {
    // alert('come');
    var title = $('#' + titleid).html().trim();
    if (type == 0 || type == 2) {
        title = $('#' + titleid).val().trim();
    }
    if (title != "") {
        erasecookie(ROOT, type);
        $('body').css("overflow-y", "hidden");
        //set id for fileupload and title
        if (setdocfilename(input)) {
            ResetDoc();
            var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
            var poistion = parseInt(scrollTop) + 10
            $('#previewdocinner').css("top", poistion);
            $('#previewdocinner').fadeIn('slow');
            $('#previewdocouter').fadeIn('slow');
            $('#docUpload').html("we are uploading your document for compliance");
            var id = $(input).attr("class");
            //var fileName = input.files[0].name;
            var fileName = $("input[id='" + input.id + "']").val().split('/').pop().split('\\').pop();
            var ext = fileName.substr(fileName.lastIndexOf('.') + 1);
            //alert('come');
            SettheEmpDocName(titleval, ext, ROOT, fileurl, type);
        }
    }
    else {

        $('#' + titleid).addClass("errorClass");
    }

}

//Used for reset the validate fileds
function onchangetext(id) {
    if ($('#' + id).val() != '') {
        $('#' + id).removeClass('errorClass');
        $('#mandatoryerrormsg').hide();
    } else {
        $('#' + id).addClass('errorClass');
        result1 = false;
    }
}

//Used for validate the textbox from special charcters
function ValidateTextBox(id, event) {
    $("#" + id).on("keypress", function (event) {

        var englishAlphabetAndWhiteSpace = /[A-Za-z ]/g;
        var key = String.fromCharCode(event.which);

        if (event.which == 39 || event.which == 37) {
            return false;
        }
        else if (event.which == 0 || event.which == 8 || event.which == 37 || event.which == 39 || event.which == 46 || event.which == 38 || event.which == 95 || event.which == 44 || englishAlphabetAndWhiteSpace.test(key) || (event.which >= 48 && event.which <= 57)) {
            return true;
        }

        return false;
    });
    if ($('#' + id).val().trim() != "") {
        $('#' + id).attr("title", $('#' + id).val());
        $('#' + id).removeClass("errorClass");
    }
}
//used for Reset the Popup Which is used for upload the documents
function ResetDoc() {
    $('#btnpublish').hide();
    $('#loaddiv').show();
    $('#pdfdiv').hide();
    $('#progressfile').css('width', '0%');
    $('#pdfdiv').attr('src', '');
}

//Used for close the popup
function CloseOpenPopup(innerId, outerId) {
    $('#' + innerId).fadeOut('slow');
    $('#' + outerId).fadeOut('slow');
    $('body').css("overflow-y", "auto");
}

//Used for check uploaded image
function check_file() {
    str = document.getElementById('file').value.toUpperCase();
    suffix1 = ".JPG";
    suffix2 = ".JPEG";
    suffix3 = ".BMP";
    suffix4 = ".PNG";
    suffix5 = ".TIF";
    if (!(str.indexOf(suffix1, str.length - suffix1.length) !== -1 ||
                   str.indexOf(suffix2, str.length - suffix2.length) !== -1 || str.indexOf(suffix3, str.length - suffix3.length) !== -1 ||
                   str.indexOf(suffix4, str.length - suffix5.length) !== -1 || str.indexOf(suffix5, str.length - suffix5.length) !== -1)) {

        $('#divManualInviteOuter').show();
        $('#divManualInviteInner').show();

        $('#dvSaveMsg').text('sorry you can upload only *.jpg,*.jpeg,*.bmp,*.png,*.tif file');

        var control = $("#file");
        control.replaceWith(control = control.clone(true));
        return false;
    }
    else {
        return true;
    }
}

//Used for set the tool tip
function tooltip(e) {
    $('#file').attr('title', e.value);

}
//Used for read the uploded image and dispaly it as image src
function readURL(input) {
    if (check_file()) {
        if (input.files && input.files[0]) {

            var reader = new FileReader();

            reader.onload = function (e) {


                $('#personpic1').attr('src', e.target.result);
            }

            return reader.readAsDataURL(input.files[0]);

        }
    }
}


//Used for showing the communication sender details from Grid
function showCompanyprofileDetails(UserId, CompanyId, ROOT) {
    //alert('company Id:' + CompanyId);
    $('#CompProfileouter').fadeIn('slow');
    $('#CompProfilepopup').fadeIn('slow');
    var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
    var poistion = scrollTop + 100;
    $('#CompProfilepopup').css("top", poistion);
    $('#companyprofileUserId').val(UserId);
    var $tbl = $('#tblcomprofile1');
    //alert(CompanyId);
    $.ajax({
        type: 'GET',
        datatype: 'json',
        url: ROOT + "CompanyDashBoard/ViewCompanyprofile",
        cache: false,
        data: { 'UserId': UserId },
        success: function (data) {
            data = $.parseJSON(data);
            //code to bind table
            var rows = ' ';

            //  alert(data.OfficePhone);

            rows += '<div style="line-height: 32px;">  <b>Name :</b> <span style="padding-left:75px;">'
                   + data.FName + ',' + data.MName + data.LName + '</span> </div> <div style="line-height: 32px;">  <b>Company Name:</b> ' +
                   '<span style="padding-left:13px;">' + data.CompanyName +
                   '</span></div>  <div style="line-height: 32px;">  <b>Office Phone :</b> <span style="padding-left:30px;">';

            if (data.OfficePhone != '') {
                rows = rows + '(' + data.OfficePhone.substring(0, 3) + ') ' + data.OfficePhone.substring(3, 6) + '-' + data.OfficePhone.substring(6, 10);
            }

            rows = rows + '</span></div> <div style="line-height: 32px;"> <b>Cell Phone :</b> <span style="padding-left:44px;"> '
            if (data.DirectPhone != '') {
                rows = rows + '(' + data.DirectPhone.substring(0, 3) + ') ' + data.DirectPhone.substring(3, 6) + '-' + data.DirectPhone.substring(6, 10)
            }
            rows = rows + '</span></div>' +
            '<div style="line-height: 32px;"> <b>Email :</b><span style="padding-left:84px;">' + data.EmailId + '</span></div> ';


            rows = rows + ' ';
            $tbl.empty();//make the table empty if already filled
            $tbl.append(rows);//append the new/updated rows in the table      
            getuserroles(CompanyId, ROOT);

        }, async: true,
        error: function (data) {
        }
    });

}

//Get User services for showing the communication sender details
function getuserroles(CompanyId, ROOT) {
    // alert(CompanyId);
    var $tbl = $('#tblrole');

    $.ajax({
        url: ROOT + "UserDashBoard/ViewUserServices",
        data: { 'CompanyId': CompanyId },
        cache: false,
        type: "GET",
        datatype: 'json',
        success: function (result) {
            data = $.parseJSON(result);
            //code to bind table
            var rows = ' ';
            $.each(data, function (i, item) {
                rows += '<div><ul style="list-style:none;"><li>' + item + '</li></ul></div>';

            });

            rows = rows + ' ';
            $tbl.empty();//make the table empty if already filled
            $tbl.append(rows);//append the new/updated rows in the table 

        }
    });

}


//Background Check Funtionality

function getUploadfilesFOrBGCheck(id, ROOT) {
    $.ajax({
        url: ROOT + "UserDashBoard/GetBackgroundUploadDocById/",
        data: { 'reqDocId': parseInt(id) },
        cache: false,
        type: "GET",
        success: function (result) {

            $('#userdocfiles1_0').html("");
            result = $.parseJSON(result);

            var count = "";
            var fileurl = "";
            var url1 = "";
            var title = $('#lblBGtitle').val();
            $.each(result, function (i, item) {

                count = $('#userdocfiles1_0 div').length;
                url1 = item.DocLoc;

                $("#userdocfiles1_0").append('<div id="div_' + count + '" style="border-radius: 5px 5px 5px 5px;  width:auto;height:auto;float:left;"><a id="docfile_' + count + '" href="' + url1 + '" target="_blank" title=' + title + '_' + item.DocSeq + '<style="cursor:pointer;margin-left:6px;" >' + title.replace(/\s/g, '') + '_' + item.DocSeq + '</a><img id="' + count + '" src="' + ROOT + 'Content/images/cross-button.png" style="cursor:pointer;" title="remove document" onclick="deletedoc(this.id)"/>' +
                               '<input type="hidden" id="doccontentid_' + count + '" value=' + item.DocTblId + ' /></div>');

            })
        }
    });
}

//set audit trail for Staff and IC
function SetAuditTrail(result) {
    var $tbl = $('#tblaudittrail');
    //st audit trail
    var rows = '<table class="table table-hover" id="back-tbl"><thead><th>Audit Date</th><th>Audit By</th><th>Audit Status</th><th>Source</th><th>Notes</th><th>Expiration Date</th></thead><tbody>';
    for (var i = 0; i < result.length; i++) {
        var ExpirationDate = '';
        var date = '';
        if (result[i].DateTime != null) {
            date = new Date(parseInt(result[i].DateTime.substr(6)));
            date = dateFormat(new Date(date), 'mm/dd/yyyy')
        }

        if (result[i].IsDNA) {
            ExpirationDate = 'DNA';
        }
        else {
            if (result[i].ExpirationDate != null) {
                ExpirationDate = dateFormat(new Date(new Date(parseInt(result[i].ExpirationDate.substr(6)))), 'mm/dd/yyyy');
            }
        }

        rows += '<tr><td>' + date + '</td><td>' + result[i].CreatorName + '</td><td style="TEXT-ALIGN:center;">' + result[i].BackgroundCheckStatus + '</td><td style="padding-left: 23px;">' + result[i].Source + '</td><td>' + result[i].Notes + '</td><td>' +
            ExpirationDate + '</td></tr>';
    }

    rows = rows + ' ';
    $tbl.empty();
    $tbl.append(rows);
    $('#back-tbl tbody td').addClass('back-tbl-td-width');
    $('#back-tbl thead th').addClass('back-tbl-th-width');

}


function openEditBGPopUp(id, title, ROOT) {
    getBackgroundDetails(id);
    $('#hid_BackgroundMasterId').val(id);
    $('#userdocfiles1_0').html('');
    $('#txtBackNotes').val('');
    $('#ddlBackStatus').val('1');
    $('#ddlBackSource').val('1');
    $('#hid_BackgroundId').val(0);
    $('#lblTopBGTitle').html(title);
    $('#lblBGtitle').val(title);
    $('#backtrans').slideDown('fast');
    $('#backpopup').slideDown('fast');
    var scrollTop = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
    var poistion = scrollTop;
    $('#backpopup').css("top", poistion);
    $('body').css("overflow-y", "hidden");

}

function Allowonlynumericvalue(id) {

    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    $("#" + id).on("keydown", function (e) {

        var keyCode = e.which ? e.which : e.keyCode

        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || specialKeys.indexOf(keyCode) != -1 || keyCode == 40 || keyCode == 41 || keyCode == 45 || keyCode == 37
            || keyCode == 32 || keyCode == 37 || keyCode == 8 || keyCode == 46 || keyCode == 39 || keyCode == 36 || keyCode == 35 || keyCode == 173 || keyCode == 189) {
            return true
        }
        else {
            return false;
        }


    });
}

//on radio button unchanged
function BgCheckRadioChanged() {

    if ($("#rddna").is(':checked')) {
        $('#bgCheckExpDate').prop("disabled", true);
        $('#bgCheckExpDate').val('');
    }
    else {
        $('#bgCheckExpDate').prop("disabled", false);
    }
}

function ValidateExpDateOnBackGroundCheck() {
    if ($("#rdexpdate").is(':checked')) {
        var currentDate = new Date();
        var expirationdate = new Date($('#bgCheckExpDate').val());
        if (expirationdate < currentDate) {
            $('#backgroundchkerrormsg').html('Expiration Date should be greater than current date.')
            return false;
        }
        else {
            $('#backgroundchkerrormsg').html('');
            return true;
        }
    }
    else {
        return true;
    }
}



