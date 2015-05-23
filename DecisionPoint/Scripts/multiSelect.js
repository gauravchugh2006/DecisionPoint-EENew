
(function ($) {
    $.fn.multiselectable = function (options) {
        var that = $(this);
        options = $.extend({
            template: '<div class="multiselectable">' +
				'<div class="m-selectable-from"><input id="txtCountySearch" placeholder="Filter County" style="width:191px;height:20px;" type="text">' +
				'<select multiple="multiple" id="m-selectable"></select>' +
				'</div>' +
				'<div class="m-selectable-controls">' +
					'<button class="multis-AllRight" id="AddAll" title="Add All">>></button>' +
					'<button class="multis-right" id="AddSelectedRight" title="Add Selected">></button>' +
                    '<button class="multis-left" id="AddSelectedleft" title="Remove Selected"><</button>' +
					'<button class="multis-AllLeft" id="RemoveAll" title="Remove All"><<</button>' +
				'</div>' +
				'<div class="m-selectable-to"><label for="m-selected" class="selectheading"></label>' +
				'<select multiple="multiple" id="m-selected"></select>' +
				'</div>' +
			'</div>',
            selectableLabel: 'Selectable:',
            selectedLabel: 'Selected:',
            moveRightText: '→',
            moveLeftText: '←',
            moveAllRightText: '→',
            moveAllLeftText: '←',
            sort: true,
            sortAscending: true
        }, options || {});

        //disable sorting if sortOptions plugin is not found
        if (!$.fn.sortOptions) {
            options.sort = false;
        }

        return that.each(function () {
            var master = $(this);

            //make sure there are no duplicate id:s inserted into the document
            var template = options.template;
            var num = 1;
            var no = 1;
            var nos = 1;
            if ($('#m-selectable').length > 0) {
                while ($('#m-selectable_' + num).length > 0) {
                    num++;
                }
                template = template.replace(/\"m-selectable\"/g, 'm-selectable_' + num);
                template = template.replace(/\"m-selected\"/g, 'm-selected_' + num);
            }
            if ($('#RemoveAll').length > 0) {
                while ($('#RemoveAll_' + no).length > 0) {
                    no++;
                }
                template = template.replace(/\"RemoveAll\"/g, 'RemoveAll_' + no);
                //template = template.replace(/\"m-selected\"/g, 'm-selected_' + num);
            }
            if ($('#AddAll').length > 0) {
                while ($('#AddAll_' + nos).length > 0) {
                    nos++;
                }
                template = template.replace(/\"AddAll\"/g, 'AddAll_' + nos);               
            }
            if ($('#AddSelectedRight').length > 0) {
                while ($('#AddSelectedRight_' + nos).length > 0) {
                    nos++;
                }
                template = template.replace(/\"AddSelectedRight\"/g, 'AddSelectedRight_' + nos);
            }
            if ($('#AddSelectedleft').length > 0) {
                while ($('#AddSelectedleft_' + nos).length > 0) {
                    nos++;
                }
                template = template.replace(/\"AddSelectedleft\"/g, 'AddSelectedleft_' + nos);
            }
            if ($('#txtCountySearch').length > 0) {                
                template = template.replace(/\"txtCountySearch\"/g, 'txtCitySearch');
                template = template.replace(/Filter County/ig, "Filter City");
            }
            if ($('#txtCitySearch').length > 0) {
               // template = template.replace(/\"txtCitySearch\"/g, 'txtZipSearch');
                template = template.replace(/txtCitySearch/ig, "txtZipSearch");
                template = template.replace(/Filter City/ig, "Filter Zip");
            }
            if ($('#txtZipSearch').length > 0) {
                template = template.replace(/txtZipSearch/ig, "txtCitySearch2");
                template = template.replace(/Filter Zip/ig, "Filter City");
            }
            //set the template
            master.hide().after(template);

            var size = master.attr('size');
            var m = master.next();
            var m1 = m.find('.m-selectable-from select');
            var m2 = m.find('.m-selectable-to select');

            //match the size of the reference element
            m1.attr('size', size);
            m2.attr('size', size);

            //set texts according to options 
            m.find('.m-selectable-from label').text(options.selectableLabel);
            m.find('.m-selectable-to label').text(options.selectedLabel);
            m.find('.multis-right').text(options.moveRightText);
            m.find('.multis-left').text(options.moveLeftText);
            m.find('.multis-AllRight').text(options.moveAllRightText);
            m.find('.multis-AllLeft').text(options.moveAllLeftText);
            //move selected options to m2, unselected to m1
            $(this).find('option:selected').clone().appendTo(m2);
            $(this).find('option:not(:selected)').clone().appendTo(m1);

            //do an initial sort to both selects
            if (options.sort) {
                m1.sortOptions(options.sortAscending);
                m2.sortOptions(options.sortAscending);
            }

            function move(from, to, master, select) {               
                var MoveValue=''; var MoveHtml='';
                from.find('option:selected').removeAttr('selected').remove().appendTo(to).each(function () {
                    var matchedElem = master.find('option[value="' + $(this).val() + '"]');                   
                    MoveValue = MoveValue + $(this).val() + ';';
                    MoveHtml = MoveHtml + $(this).html() + ';';                   
                    if (select) {
                        matchedElem.attr('selected', 'selected');
                    } else {
                        matchedElem.removeAttr('selected');
                    }
                });
                $('#selectedForMove').val(MoveValue);
                $('#selectedForMoveName').val(MoveHtml);
                removeDuplicateFromList(to.attr('id'));
                if (options.sort) {
                    to.sortOptions(options.sortAscending);
                }

                return false;
            }

            function moveLeft() {
                return move(m2, m1, master, false);
            }
            function moveRight() {
                return move(m1, m2, master, true);
            }

            function moveAll(from, to, master, select) {
                var MoveValue = ''; var MoveHtml = '';
                from.find('option').removeAttr('option').remove().appendTo(to).each(function () {
                    var matchedElem = master.find('option[value="' + $(this).val() + '"]');
                    MoveValue = MoveValue + $(this).val() + ';';
                    MoveHtml = MoveHtml + $(this).html() + ';';
                    if (select) {
                        matchedElem.attr('selected', 'selected');
                    } else {
                        matchedElem.removeAttr('selected');
                    }
                });
                $('#selectedForMove').val(MoveValue);
                $('#selectedForMoveName').val(MoveHtml);
                if (options.sort) {
                    to.sortOptions(options.sortAscending);
                }
                return false;
            }
            function moveAllLeft() {
                return moveAll(m2, m1, master, false);
            }
            function moveAllRight() {
                return moveAll(m1, m2, master, true);
            }
            //set all the events that trigger a move
            m.find('.multis-right').click(moveRight);
            m.find('.multis-left').click(moveLeft);
            m.find('.multis-AllRight').click(moveAllRight);
            m.find('.multis-AllLeft').click(moveAllLeft);
            m1.dblclick(moveRight);
            m2.dblclick(moveLeft);
            m1.keydown(function (event) {
                if (event.keyCode === 13) {
                    moveRight();
                }
            });
            m2.keydown(function (event) {
                if (event.keyCode === 13) {
                    moveLeft();
                }
            });
        });
    };
})(jQuery);