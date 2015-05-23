function SterlingMapping(ROOT, PackageId)
{
    $.ajax({
        url: ROOT + "Company/UpgradePackage/",
        data: JSON.stringify({
            'PackageIds': PackageId
        }),
        contentType: 'application/json; charset=utf-8',
        cache: false,
        type: "POST",
        success: function (result) {
           
            if (result == "OK") {
                window.opener.document.location.reload(true);
                window.open("", "_top", "", "true ");
                window.close();
            }
            else {
                window.opener.document.location.reload(true);
                window.open("", "_top", "", "true ");
                window.close();
            }
            //window.location.reload();
        },
        beforeSend: function (jqXHR, obj) {


            $('#divAlertPopupOuter').show()
            $('#divAlertPopupInner').show()
            $('#dvAlertPopUpMsg').text('Please wait...');
        }
        
    });
}

function UpgradePackage(tblId, packageId, id) {
   
    $('#' + id).attr("target", "_blank");
    $('#' + id).attr("href", "/Company/UpgradePackage?packageId=" + packageId + "&TblId=" + tblId);
    var target = e.target;
    e.preventDefault();
    target.click();
}

function ReviewSterlingResult(reviewResultUrl, ROOT) {
   
}