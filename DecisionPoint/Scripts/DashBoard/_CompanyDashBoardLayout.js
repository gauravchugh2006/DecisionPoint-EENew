var ROOT = '@Url.Content("~/")';
var CountText = 1;
var InstResult;

$(window).load(function () {

    window.onbeforeunload = null

    var heights = $(window).height();
    heights = heights / 2 - 50;
    $('#content').css('min-height', heights + 'px');

    var status = '@TempData["GuideStatus"]';

    if (status == "True") {

        var top = $("#SuperAdminInstruction").position();
        $('#ConfirmSuperAdmindiv').animate({ 'top': top.top + 55 }).css("display", "block");
        $("#licompanyDashboard").removeClass("active");
        $("#SuperAdminInstruction").addClass("active");

        var SuperadminInst = tinymce.get('txtInstruction').getContent();
        var bodyTextInst = new RegExp(/<body[^>]*>([\S\s]*?)<\/body>/).exec(SuperadminInst)[1];
        SetTempData(bodyTextInst);
    }
    //function DeleteDoc() {
    $('#btncommalert').click(function () {

        var type = $('#type').val();
        var docId = parseInt($('#msgdocid').val());
        if ($('#OPTypeId').val() == 1) {
            $.ajax({
                url: '@Url.Content("~/CompanyDashBoard/RemoveDocument/")',
                cache: false,
                data: { 'docId': docId, type: parseInt(type) },
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (data) {

                    CancelDoc();
                    if (type == '0') {
                        getdocumentsdetails(getpageid("documentgrid"));
                    }
                    else if (type == '1' || type == '4') {
                        getglobaldetails(getpageid("historygrid"));
                    }
                    else if (type == '2') {
                        getincomingdetails(getpageid("documentgrid"));
                    }


                },
                error: function () {
                },
                async: false,

            });
        }
        else if ($('#OPTypeId').val() == 2) {
            Reactivedoc();
        }
    })

})

$('#myTabs').click(function () {
    $('.globallibraryli').hide();
    $('.reportingli').hide();
    $('.incommli').hide();
    $('.commli').hide();
    $('.administrationli').hide();

    $('.mydashboardli').show();
    $('.partnersli').show();
    $('.messagedocli').show();
    $('.historyli').show();
    $('.accountprofile').show();
    $('#myAdmin').show();
    $('#myTabs').hide();
    $('.contractli').hide();
    $('.inviteli').hide();
    $('.mycontractli').show();
});
$('#myAdmin').click(function () {
    $('.globallibraryli').show();
    $('.reportingli').show();
    $('.incommli').show();
    $('.commli').show();
    $('.administrationli').show();

    $('.mydashboardli').hide();
    $('.partnersli').hide();
    $('.messagedocli').hide();
    $('.historyli').hide();
    $('.accountprofile').hide();
    $('#myTabs').show();
    $('#myAdmin').hide();
    $('.contractli').show();
    $('.inviteli').show();
    $('.mycontractli').hide();


});
$("#lnkemailid").click(function () {
    var getemail = $("#lnkemailid").text();
    window.location.href = "mailto:" + getemail;
    //window.open('mailto:' + getemail + '', 'email');

})
function SetTempData(instruction) {

    $.ajax({
        type: "POST",
        url: '@Url.Content("~/CompanyDashBoard/SetTempData/")',
        cache: false,
        data: JSON.stringify({
            GuidInstruction: instruction,
            ID: 2
        }),
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

        },
        error: function (error) {

        }

    })
}

$(function () { $("#ConfirmdivAdmin").draggable({ containment: "window" }); });

$("#ConfirmdivAdmin").resizable({

    minWidth: 150,
    minHeight: 150,
    maxWidth: ($(window).width() - 100),
    maxHeight: ($(window).height() - 100),
    resize: function (event, ui) {

        var ed = tinymce.get('txtInstruction');

        var width = parseFloat($(this).width()) - 10;
        var height = parseFloat($(this).height()) - 170;
        ed.theme.resizeTo(width, height);

    },
    stop: function (event, ui) {
    }

});



$("#ConfirmSuperAdmindiv").draggable({ containment: "window" });
$("#ConfirmSuperAdmindiv").resizable({

    minWidth: 150,
    minHeight: 150,
    maxWidth: ($(window).width() - 100),
    maxHeight: ($(window).height() - 100),
    resize: function (event, ui) {

        var ed = tinymce.get('txtInstruction');

        var width = parseFloat($(this).width()) - 10;
        var height = parseFloat($(this).height()) - 170;
        ed.theme.resizeTo(width, height);

    },
    stop: function (event, ui) {
    }

});

$("#SuperAdminInstruction").click(function (event) {
    // alert('call');
    var top = $("#SuperAdminInstruction").position();

    if (CountText == 1) {
        CountText = 2;

    }
    var tinymce_editor_id1 = 'txtInstruction';

    tinymce.get(tinymce_editor_id1).setContent(" ");
    //  alert('call1');
    $('#Insterror').text("");
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/GetGuideInstruction/")',
        type: 'GET',
        cache: false,
        data: { IsActive: true },

        success: function (result) {
            InstResult = result;

            tinymce.get(tinymce_editor_id1).setContent(result);

        },
        error: function (result) {

        }
    })



    $('#ConfirmSuperAdmindiv').animate({ 'top': top.top + 55 }).css("display", "block");
    $("#licompanyDashboard").removeClass("active");
    $("#SuperAdminInstruction").addClass("active");

});

$("#AdminInstruction").click(function (event) {


    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/GetGuideInstruction/")',
        type: 'GET',
        cache: false,
        data: { IsActive: false },
        success: function (result) {

            $('#lblGuideInst').append(result);
            var top = $('#AdminInstruction').position();

            $('#ConfirmdivAdmin').animate({ 'top': top.top + 60 }).css("display", "block");
            $("#licompanyDashboard").removeClass("active");
            $("#SuperAdminInstruction").addClass("active");

        },
        error: function (result) {

        }
    })

})

$('#btnCloseInst').click(function () {
    var SuperadminInst = tinymce.get('txtInstruction').getContent();
    var bodyTextInst = new RegExp(/<body[^>]*>([\S\s]*?)<\/body>/).exec(SuperadminInst)[1];

    if (bodyTextInst.trim() != "") {
        $.ajax({
            type: "POST",
            url: '@Url.Content("~/CompanyDashBoard/SaveGuidInstrictions/")',
            data: JSON.stringify({
                GuidInstruction: bodyTextInst,
                ID: 2
            }),
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {

                if (response > 0) {

                    $('#ConfirmSuperAdmindiv').css("display", "none");
                    $("#licompanyDashboard").addClass("active");
                    $("#SuperAdminInstruction").removeClass("active");

                }
                else {

                    $('#ConfirmSuperAdmindiv').css("display", "none");
                    $("#licompanyDashboard").addClass("active");
                    $("#SuperAdminInstruction").removeClass("active");
                }

                SetTempData(bodyTextInst);
            },
            error: function (data) {

                $('#ConfirmSuperAdmindiv').css("display", "none");
                $("#licompanyDashboard").addClass("active");
                $("#SuperAdminInstruction").removeClass("active");
                SetTempData("");
            }
        });
    }
    else {
        $('#Insterror').css("color", "red");
        $('#Insterror').text("Please Enter Instruction.");

    }


})

$('#btnSuperGudielose').click(function () {
    //   debugger;
    var status = '@Session["UserDashboarInst"]';
    var tinymce_editor_id1 = 'txtInstruction';

    if (status == 'True') {

        CountText = 1;

    }


    $('#ConfirmSuperAdmindiv').css("display", "none");
    $("#licompanyDashboard").addClass("active");
    $("#SuperAdminInstruction").removeClass("active");

    SetTempData("");

})

$('#btnGudielose').click(function () {

    $('#ConfirmdivAdmin').css("display", "none");
    $("#licompanyDashboard").addClass("active");
    $("#SuperAdminInstruction").removeClass("active");
    $('#lblGuideInst').text("");
})

$('#btnSubmitInst').click(function () {

    var SuperadminInst = tinymce.get('txtInstruction').getContent();
    var bodyTextInst = new RegExp(/<body[^>]*>([\S\s]*?)<\/body>/).exec(SuperadminInst)[1];

    if (bodyTextInst.trim() != "") {
        $.ajax({
            type: "POST",
            url: '@Url.Content("~/CompanyDashBoard/SaveGuidInstrictions/")',
            data: JSON.stringify({
                GuidInstruction: bodyTextInst,
                ID: 1
            }),
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {

                if (response > 0) {
                    $('#Insterror').css("color", "green");
                    $('#Insterror').text("Successfully Saved.");
                }
                else {
                    $('#Insterror').css("color", "red");
                    $('#Insterror').text("Not Saved.");
                }
            },
            error: function (data) {

            }
        });


    }
    else {
        $('#Insterror').css("color", "red");
        $('#Insterror').text("Please Enter Instruction.");

    }
})



tinymce.init({

    mode: 'specific_textareas',
    force_p_newlines: false,
    force_br_newlines: true,
    forced_root_block: '',
    paste_data_images: true,
    editor_deselector: "mceNoEditor",

    plugins: [
            "pagebreak advlist autolink autosave link image lists charmap print preview hr anchor spellchecker",
            "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons template textcolor paste fullpage textcolor"
    ],

    toolbar1: "newdocument | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect | forecolor backcolor | cut copy paste | searchreplace | bullist numlist",
    toolbar2: "search,replace,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,|,insertdate,inserttime,preview,|,charmap,emotions,iespell,media,advhr|,fullscreen,pagebreak",


    menubar: false,
    toolbar_items_size: 'small',
    resize: false,

    style_formats: [
            { title: 'Bold text', inline: 'b' },
            { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
            { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
            { title: 'Example 1', inline: 'span', classes: 'example1' },
            { title: 'Example 2', inline: 'span', classes: 'example2' },
            { title: 'Table styles' },
            { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
    ],

    templates: [
            { title: 'Test template 1', content: 'Test 1' },
            { title: 'Test template 2', content: 'Test 2' }
    ]

});


//show dlete confirmation div
function showconfirmationdiv(msgId, type) {
    $('#OPTypeId').val(1);
    $('#type').val(type);
    $('#msgdocid').val(msgId);
    if (type == '0') {
        $('#divmsg').html("Are you sure you want to move this record in library?");
    }
    else if (type == '2' || type == '1') {
        $('#divmsg').html("Are you sure you want to move this record in withdrawn items? ");
    }

    $('#btnok').css("display", "block");
    $('#btncancel').css("display", "block");
    $('.confirmationdivinner').fadeIn('slow');
    $('.confirmationdivmain').fadeIn('slow');
}

//Delete messges



function getdocumentsdetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/DocumentAction/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#documents').html($(result).find('#documents').html().trim());
    }
});
}
function getglobaldetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/GlobalLibrary/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#historydiv').html($(result).find('#historydiv').html().trim());
    }
});
}
function gethistorydetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/HistoryAction/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#historydiv').html($(result).find('#historydiv').html().trim());
    }
});
}
function getincomingdetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/Incomming/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#incodocuments').html($(result).find('#incodocuments').html().trim());
    }
});
}
function CancelDoc() {
    $('.confirmationdivinner').fadeOut('slow');
    $('.confirmationdivmain').fadeOut('slow');
}

//reactive documrnt
function showreactivediv(msgId, type) {
    $('#OPTypeId').val(2);
    $('#type').val(type);
    $('#msgdocid').val(msgId);
    if (type == '1' || type == '2' || type == '3' || type == '4') {
        $('#divmsg').html("Are you sure you want to reactive this communication ?");
    }

    $('#btnok').show();
    $('#btncancel').show();
    $('#btncommalert').show();
    $('.confirmationdivinner').fadeIn('slow');
    $('.confirmationdivmain').fadeIn('slow');
}
function Reactivedoc() {
    var type = $('#type').val();
    var docId = parseInt($('#msgdocid').val());
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/ReactiveDocument/")',
        cache: false,
        data: { 'docId': docId, 'type': parseInt(type) },
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html'

    })
.success(function (data) {
    CancelDoc();
    if (type == 2) {

        getwithdrawninboxdetails(getpageid("historygrid"));
    }
    else if (type == 1 || type == 3) {
        //if (type == 3) {
        //    if (data == -1) {

        //        $('#divmsg').html("you can't reactive this communication.");
        //        $('#btnok').hide();
        //        $('#btncommalert').hide();

        //        $('#btncancel').hide();
        //        //$('.confirmationdivinner').fadeIn('slow');
        //        //$('.confirmationdivmain').fadeIn('slow');
        //        setTimeout(function () {
        //            $('.confirmationdivinner').fadeOut('slow');
        //            $('.confirmationdivmain').fadeOut('slow');
        //        }, 2000);
        //    }
        //}
        //if (type == 1) {
        //    CancelDoc();
        //}
        getwithdrawnhistorydetails(getpageid("historygrid"));
    }
    else if (type == 4) {
        gethistorydetails(getpageid("historygrid"));
    }
})
.error(function (xhr, status) {
    CancelDoc()
})
}
function getwithdrawninboxdetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/Communication/WithdrawnInboxHistory/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#withdrawninboxdiv').html($(result).find('#withdrawninboxdiv').html().trim());
    }
});
}
function getwithdrawnhistorydetails(pageid) {
    $.ajax({
        url: '@Url.Content("~/Communication/WithdrawnHistory/?page=")' + pageid,
        data: { id: 'All' },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#withdrawnhistorydiv').html($(result).find('#withdrawnhistorydiv').html().trim());
    }
});
}
function CloseProfile() {
    $('#CompProfileouter').fadeOut('slow');
    $('#CompProfilepopup').fadeOut('slow');
}
var currentgridl = $('#historygrid tr').length;
//serach in history section
function Go() {
    var filtervalue = setgroupfiltervalue() + "," + $('#reftypedd').html() + "," + $('#doctypedd').html();
    alert(filtervalue);
    var currentlength = $('#historygrid tr').length;
    if (currentgridl == currentlength) {
        $('#btnviewall').css("display", "none");
    }
    var filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";

    filtervalue = filtervalue + "," + $('#txtserach').val();
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/HistoryAction/")',
        data: { id: filtervalue + ":serach" },
        cache: false,
        type: "GET",
        datatype: 'json',
        success: function (result) {
            $('#historydiv').html($(result).find('#historydiv').html().trim());
            $("#btnviewall").css("display", "block")
            $('#messagedoccourseli').removeClass("active");
            $('#historyli').addClass("active");
            $('#Accountprofileli').removeClass("active");
            currentlength = $('#historygrid tbody tr').length;
            if (currentlength == 0) {
                $('#divmsg').html("No record found !");
                $('.confirmationdivinner').slideDown('fast');
                $('.confirmationdivmain').slideDown('fast');
                $('#btnok').hide();
                $('#btncancel').hide();
                $('#btncommalert').hide();
                $('.confirmationdivinner').css("width", "175px");
                setTimeout(function () {
                    $('.confirmationdivinner').slideUp('fast');
                    $('.confirmationdivmain').slideUp('fast');
                    $('#btnok').show();
                }, 2000)
            }
            if (currentgridl == currentlength) {
                $('#btnviewall').css("display", "none");
            }
        }
    });


}

function viewall() {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/HistoryAction/")',
        data: { id: 'All' },
        cache: false,
        type: "GET",
        datatype: 'json',
        success: function (result) {
            $('#historydiv').html($(result).find('#historydiv').html().trim());
            $("#btnviewall").css("display", "none")
            $('#messagedoccourseli').removeClass("active");
            $('#historyli').addClass("active");
            $('#Accountprofileli').removeClass("active");
            $('#txtserach').val('');
        }
    });



}



//Filteration in webgrid using in drop down
function filter(filterOn, filtervalue, filteronDD) {
    var filterselectedvalue = "";
    if (filterOn == "document") {
        if (filteronDD == "0") if (filteronDD == "0") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;
            }
            else {
                filterselectedvalue = "All";

            }
        }

        if (filteronDD == "1") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;

            }
            else {
                filterselectedvalue = "All";

            }

        }
        if (filteronDD == "2") {

            if (filtervalue != "All") {

                filterselectedvalue = setgroupfiltervalue();

            }
            else {
                filterselectedvalue = setgroupfiltervalue();

            }

        }
        setbackground(filteronDD, filterselectedvalue, filterOn);
        filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '@Url.Content("~/CompanyDashBoard/DocumentAction")',
            cache: false,
            data: { 'id': filtervalue },
            success: function (result) {
                $('#documents').html($(result).find('#documents').html().trim());
                // setbackground(filteronDD, filterselectedvalue, filterOn);

            }, async: true,
            error: function (result) {
            }

        });
    }


    if (filterOn == "history") {
        if (filteronDD == "0") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;
            }
            else {
                filterselectedvalue = "All";

            }
        }

        if (filteronDD == "1") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;

            }
            else {
                filterselectedvalue = "All";

            }

        }
        if (filteronDD == "2") {

            if (filtervalue != "All") {

                filterselectedvalue = setgroupfiltervalue();

            }
            else {
                filterselectedvalue = setgroupfiltervalue();

            }

        }
        setbackground(filteronDD, filterselectedvalue, filterOn);
        filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";
        setTimeout(function () {
            $.ajax({
                type: 'GET',
                datatype: 'json',
                url: '@Url.Content("~/CompanyDashBoard/HistoryAction")',
                cache: false,
                data: { 'id': filtervalue },
                success: function (result) {
                    $('#historydiv').html($(result).find('#historydiv').html().trim());

                }, async: true,
                error: function (result) {
                }

            });
        }, 200);

    }

    if (filterOn == "globallibrary") {
        if (filteronDD == "0") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;
            }
            else {
                filterselectedvalue = "All";

            }
        }

        if (filteronDD == "1") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;

            }
            else {
                filterselectedvalue = "All";

            }

        }
        if (filteronDD == "2") {

            if (filtervalue != "All") {

                filterselectedvalue = setgroupfiltervalue();

            }
            else {
                filterselectedvalue = setgroupfiltervalue();

            }

        }

        setbackground(filteronDD, filterselectedvalue, filterOn);
        filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";
            
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '@Url.Content("~/CompanyDashBoard/GlobalLibrary")',
            cache: false,
            data: { 'id': filtervalue },
            success: function (result) {
                $('#historydiv').html($(result).find('#historydiv').html().trim());


            }, async: true,
            error: function (result) {
            }

        });

    }
    //WithdrawnHistory/Library
    if (filterOn == "withdrawnhistory") {

        if (filteronDD == "0") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;
            }
            else {
                filterselectedvalue = "All";

            }
        }

        if (filteronDD == "1") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;

            }
            else {
                filterselectedvalue = "All";

            }

        }
        if (filteronDD == "2") {

            if (filtervalue != "All") {

                filterselectedvalue = setgroupfiltervalue();

            }
            else {
                filterselectedvalue = setgroupfiltervalue();

            }

        }
        setbackground(filteronDD, filterselectedvalue, filterOn);
        filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '@Url.Content("~/Communication/WithdrawnHistory")',
            cache: false,
            data: { 'id': filtervalue },
            success: function (result) {
                $('#withdrawnhistorydiv').html($(result).find('#withdrawnhistorydiv').html().trim());
                setbackground(filteronDD, filterselectedvalue, filterOn);

            }, async: true,
            error: function (result) {
            }

        });

    }

    //WithdrawnInboxHistory/Inbox
    if (filterOn == "WithdrawnInboxHistory") {
        if (filteronDD == "0") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;
            }
            else {
                filterselectedvalue = "All";

            }
        }

        if (filteronDD == "1") {
            if (filtervalue != "All") {
                filterselectedvalue = filtervalue;

            }
            else {
                filterselectedvalue = "All";

            }

        }
        if (filteronDD == "2") {

            if (filtervalue != "All") {

                filterselectedvalue = setgroupfiltervalue();

            }
            else {
                filterselectedvalue = setgroupfiltervalue();

            }

        }

        setbackground(filteronDD, filterselectedvalue, filterOn);
        filtervalue = setgroupfiltervalue() + ":group" + "," + $('#reftypedd').html() + ":reference" + "," + $('#doctypedd').html() + ":doctype";
        // setTimeout(function () {
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '@Url.Content("~/Communication/WithdrawnInboxHistory")',
            cache: false,
            data: { 'id': filtervalue },
            success: function (result) {
                $('#withdrawninboxdiv').html($(result).find('#withdrawninboxdiv').html().trim());
                setbackground(filteronDD, filterselectedvalue, filterOn);

            }, async: true,
            error: function (result) {
            }

        });
    }


}
function setbackground(filteronDD, filtervalue, filterOn) {
    if (filteronDD == "2") {
        if (filtervalue != "All") {
            if (filtervalue == "") {
                $('#groupdd').attr("title", "Group");
                $("#groupdd").css({ "background-color": "#0044CC" });
                $("#groupdd").css({ "background-image": "linear-gradient(to bottom, #0088CC, #0044CC)" });
            }
            else {
                $('#groupdd').attr("title", filtervalue);
                $("#groupdd").css({ "background-color": "#0044CC !important" });
                $("#groupdd").css({ "background-image": "linear-gradient(to bottom, #0044CC, #0044CC)" });
            }
            fillcategory(filtervalue, filterOn);

        }
        else {
            fillcategory(filtervalue, filterOn);
            filtervalue = "Group";
            $('#groupdd').attr("title", filtervalue);
            $("#groupdd").css({ "background-color": "#0044CC" });
            $("#groupdd").css({ "background-image": "linear-gradient(to bottom, #0088CC, #0044CC)" });

        }


    }
    if (filteronDD == "0") {
        if (filtervalue == "All") {
            $("#docdd").css({ "background-color": "#0044CC" });
            $("#docdd").css({ "background-image": "linear-gradient(to bottom, #0088CC, #0044CC)" });
            filtervalue = "Sub Category";
        }
        else {
            $("#docdd").css({ "background-color": "#0044CC !important" });
            $("#docdd").css({ "background-image": "linear-gradient(to bottom, #0044CC, #0044CC)" });
        }
        if (filtervalue != "All") {
            $('#doctypedd').html(filtervalue);
        }
        else { $('#doctypedd').html(filtervalue); }


    }
    if (filteronDD == "1") {
        if (filtervalue != "All") {

            $('#reftypedd').html(filtervalue);
            filtervalue = filtervalue + "$" + setgroupfiltervalue();
            fillsubcategory(filtervalue, filterOn);
            $("#refdd").css({ "background-color": "#0044CC !important" });
            $("#refdd").css({ "background-image": "linear-gradient(to bottom, #0044CC, #0044CC)" });
        }
        else {
            fillsubcategory(filtervalue, filterOn);
            filtervalue = "Category";
            $('#reftypedd').html(filtervalue);
            $("#refdd").css({ "background-color": "#0044CC" });
            $("#refdd").css({ "background-image": "linear-gradient(to bottom, #0088CC, #0044CC)" });
        }

    }
}

function setgroupfiltervalue() {
    var groupv = "";
    var count = 0;
    var maxcount = $('.groupchk').length;
    $('.groupchk').each(function () {

        if ($(this).is(':checked')) {
            if (groupv == "") {
                groupv = $(this).val();
            }
            else {
                groupv = groupv + "*" + $(this).val();
            }
            count = count + 1;
        }

    })
    if ($('.groupchkall').is(":checked")) {
        groupv = "";
        $('.groupchk').each(function () {


            if (groupv == "") {
                groupv = $(this).val();
            }
            else {
                groupv = groupv + "*" + $(this).val();
            }
            count = count + 1;


        })
    }

    return groupv;
}
function fillsubcategory(source, filterOn) {
    $.ajax({
        url: '@Url.Content("~/CompanyDashBoard/getSubCategoryOnbasisOfCategory/")',
        data: { 'id': source },
    cache: false,
    type: "GET",
    datatype: 'json',
    success: function (result) {
        $('#refelis').empty();
        $('#refelis').css("display", "");
        if (result.length > 0) {
            $('#refelis').prepend('<li onclick=filter("' + filterOn + '","All","0")><a><label style="font-weight:normal" class="checkbox" id="allref">All</label></a></li>');
        }
        else {
            $('#refelis').prepend('<li><a><label style="font-weight:normal" class="checkbox" id="allref">No Sub Category</label></a></li>');
        }

        for (var i = 0; i < result.length; i++) {
            var catname = result[i].categoryName;
            $('#refelis').append('<li id="' + catname + '" onclick=filter("' + filterOn + '",this.id,"0")><a><label style="font-weight:normal" class="checkbox" id=' + result[i].categoryName + '>' + result[i].categoryName + '</label></a></li>');
        }
    }
})
}
function fillcategory(group, filterOn) {
    if (group != "") {
        $.ajax({
            url: '@Url.Content("~/CompanyDashBoard/getCategoryOnbasisOfGroup/")',
            data: { 'group': group },
        cache: false,
        type: "GET",
        datatype: 'json',
        success: function (result) {
            $('#catlis').empty();
            $('#catlis').css("display", "");
            if (result.length > 0) {
                $('#catlis').prepend('<li onclick=filter("' + filterOn + '","All","1")><a><label style="font-weight:normal" class="checkbox" id="allref">All</label></a></li>');
            }
            else {
                $('#catlis').prepend('<li><a><label style="font-weight:normal" class="checkbox" id="allref">No Category</label></a></li>');
            }
            for (var i = 0; i < result.length; i++) {
                var catname = result[i].referenceName;
                $('#catlis').append('<li id="' + catname + '" onclick=filter("' + filterOn + '",this.id,"1")><a><label style="font-weight:normal" class="checkbox" id=' + result[i].referenceName + '>' + result[i].referenceName + '</label></a></li>');
            }

        }
    })
        }
else {
            $('#catlis').empty();
}
}
function showCompanyprofile(UserId, CompanyId) {
    showCompanyprofileDetails(UserId, CompanyId, ROOT);
}
