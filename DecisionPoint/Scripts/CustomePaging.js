function getpageid(id) {
    var pageid = 1;
    if ($('#' + id + ' tfoot tr td').length) {
        pageid = $('#' + id + ' tfoot tr td').clone();
        $(pageid).find('a').each(function () {

            $(this).remove();
        })
        pageid = pageid.html().trim();

    }
    return pageid;
}