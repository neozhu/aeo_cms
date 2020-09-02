var DEFAULT_EDIT_WIN_STYLE = CenterWin("width=650,height=600,scrollbars=yes");

var FIELD_REQUIRED_CLSNAME = "aim-ui-required";
var FIELD_REQUIRED_BGCOLOR = "#FEFFBB";

var UPLOAD_PAGE_URL = "/CommonPages/File/Upload2.aspx";

var DOWNLOAD_PAGE_URL = "/CommonPages/File/DownLoad.aspx";
var DOWNLOAD_Sign_URL = "/CommonPages/File/DownLoadSign.aspx";


(function () {
    $.fn.file = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.file.methods[options](this, param);
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'file');
            if (state) {
                $.extend(state.options, options);
            } else {
                $.data(this, 'file', {
                    options: $.extend({}, $.fn.file.defaults, $.fn.file.parseOptions(this), options)
                });
                $(this).removeAttr('disabled');
                $(this).bind('_resize', function (e, force) {
                    if ($(this).hasClass('easyui-fluid') || force) {
                        setSize(this);
                    }
                    return false;
                });
            }
            RenderFile(this);
        });
    };


    $.fn.file.methods = {
        options: function (jq) {
            return $.data(jq[0], 'file').options;
        },
        setValue: function (jq, param) {
            return jq.each(function () {
                setValue(this, param);
            });
        },
        getValue: function (jq, param) {
            return jq.each(function () {
                getValue(this, param);
            });
        },
        enable: function (jq) {
            return jq.each(function () {
                setDisabled(false, this);
            });
        },
        disable: function (jq) {
            return jq.each(function () {
                setDisabled(true, this);
            });
        }
    };

    $.fn.file.parseOptions = function (target) {
        var t = $(target);
        return $.extend({}, $.parser.parseOptions(target,
			['id', 'iconCls', 'iconAlign', 'group', 'size', 'text', { plain: 'boolean', toggle: 'boolean', selected: 'boolean', outline: 'boolean' }]
		), {
		    disabled: (t.attr('disabled') ? true : undefined),
		    text: ($.trim(t.html()) || undefined),
		    iconCls: (t.attr('icon') || t.attr('iconCls'))
		});
    };

    $.fn.file.defaults = {
        id: null,
        disabled: false,
        toggle: false,
        selected: false,
        outline: false,
        group: null,
        plain: false,
        text: '',
        iconCls: null,
        iconAlign: 'left',
        size: 'small',	// small,large
        onClick: function () { }
    };
    function RenderFile(htmele) {
        var htmlele = typeof (htmele) == "object" ? $("#" + htmele.id) : $("#" + htmele);
        htmlele.css("float", "left");
        var eleSpan, fileLinkSpan, fileBtnSpan, fileInputField, btnFileAdd, btnFileDel, btnFileClr, btnFileOpn;


        var readOnly = htmlele.attr("readonly");
        var disabled = htmlele.attr("disabled");
        var mode = htmlele.attr("mode") || "multi";

        var IsLog = htmlele.attr("IsLog") || ""; // 上传是否写日志
        var Filter = htmlele.attr("Filter") || ""; // 上传文件过滤字符
        var MaximumUpload = ""; // 最大单个上传文件大小(MB)
        var MaxNumberToUpload = ""; // 最大上传文件数
        var AllowThumbnail = false; // 是否允许显示缩略图
        var DoCheck = htmlele.attr("DoCheck") || null; // 上传前检查方法
        var UploadPage = htmlele.attr("UploadPage") || UPLOAD_PAGE_URL;
        var DownloadPage = htmlele.attr("DownloadPage") || DOWNLOAD_PAGE_URL;

        var BtnFileAddID = "btnFileAdd_" + htmele.id;
        var BtnFileClrID = "btnFileClr_" + htmele.id;
        var BtnFileDelID = "btnFileDel_" + htmele.id;
        var BtnFileOpnID = "btnFileOpn_" + htmele.id;
        var FileLinkSpanID = "spanFileLink_" + htmele.id;
        var FileInputFieldID = "spanFileInput_" + htmele.id;
        var FileBtnSpanID = "spanFileBtn_" + htmele.id;

        var SingleFileBlock = "<div class='aim-ctrl-file-link' linkfile='{filefullname}' style='float:left; width:100%; border:0px;'>"
            + "<a href='" + DownloadPage + "?Id={fileid}' style='margin:5px;' title='{filename}' "
            + " >{filename}</a></div>";

        var FileBlock = "<div class='aim-ctrl-file-link' linkfile='{filefullname}' style='float:left; width:120; height: 20; margin:2px; border:0px;'>"
            + "<input type='checkbox'style='border:0px' />"
            + "<a href='" + DownloadPage + "?Id={fileid}' style='margin:5px;' title='{filename}' "
            + " >{filename}</a></div>";


        var UploadStyle = "<div id='showDialog' style=\"background-color:rgb(233, 240, 252); overflow-y:hidden;margin:0px;display:none;text-align: center;vertical-align: center\" >"
           + "<iframe  id='fileframe' width=\"410px\" style=\"background-color:rgb(233, 240, 252);\" height=\"100%\" style=\"margin:0px\" frameborder=\"0\"  marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\"></iframe></div>"

        var SingleStructure = UploadStyle + " <table style='border:0px; width:100%; font-size:12px;'><tr>"
            + "<td style='width:*; vertical-align:top; border-color:#8FAACF; padding:2px;' class='aim-ctrl-file'><span id='" + FileInputFieldID + "' style='width:100%' /></td>"
            + "<td style='width:100px; border:0px; padding:0px;' align='center'><span id='" + FileBtnSpanID + "' class='aim-ctrl-file-button-span'>"
            + "<a id='" + BtnFileAddID + "' class='aim-ctrl-file-button l-btn'>上传</a>"
            + "<a id='" + BtnFileClrID + "' class='aim-ctrl-file-button l-btn'>清空</a>"
            + "</span></td></tr></table>";

        var MultiStructure = UploadStyle + "<table style='border:0px; width:100%; font-size:12px;'><tr>"
            + "<td style='width:*; vertical-align:top; border-color:#8FAACF' class='aim-ctrl-file'><span id='" + FileLinkSpanID + "' style='width:100%;'></span></td>"
            + "<td style='width:50px; border:0px;' align='center'><span id='" + FileBtnSpanID + "' class='aim-ctrl-file-button-span'>"
            + "<a id='" + BtnFileAddID + "' class='aim-ctrl-file-button l-btn'>上传</a><br><br>"
            + "<a id='" + BtnFileDelID + "' class='aim-ctrl-file-button l-btn'>删除</a><br><br>"
            + "<a id='" + BtnFileClrID + "' class='aim-ctrl-file-button l-btn'>清空</a>"
            + "</span></td></tr></table>";



        init();
        $('#showDialog').dialog({
            width: 425,
            height: 340,
            modal: true,
            closed: true,
            title: '选择要上传的附件后点击开始上传',
            buttons: [{
                text: '完成',
                handler: function () {
                    var doc = window.frames["fileframe"].document || window.frames["fileframe"].contentDocument;
                    var rtn = doc.getElementById("files").value;
                    if (rtn == "0") {
                        AimDlg.show("文件还在上传中请稍后...");
                        return;
                    }
                    if (rtn == "") {
                        if (confirm("您没有上传文件！是否关闭窗口")) {
                            doc.getElementById("files").value = "";
                            $("#showDialog").dialog("close");
                            return;
                        } else {
                            return;
                        }
                    }
                    if (rtn) {
                        if (mode == "single") {
                            htmlele.val(rtn);
                        } else
                            htmlele.val(htmlele.val() + rtn);
                    }
                    refreshFileView();
                    $("#showDialog").dialog("close");
                }
            }, {
                text: '取消',
                handler: function () {
                    var doc = window.frames["fileframe"].document || window.frames["fileframe"].contentDocument;
                    var rtn = doc.getElementById("files").value;
                    if (rtn == "0") {
                        if (!confirm("文件正在传输！是否确认关闭窗口?"))
                            return;
                    }
                    $("#showDialog").dialog("close");
                }
            }]
        });

        function init() {
            // 如果父元素不是span，则创建一个span
            htmlele.wrap("<span></span>");
            eleSpan = htmlele.parent();

            eleSpan.attr("height", htmlele.attr("height"));
            eleSpan.css("height", htmlele.css("height"));

            eleSpan.attr("width", htmlele.attr("width"));
            eleSpan.css("width", htmlele.css("width"));

            if (!eleSpan.css("height") || eleSpan.css("height") == "auto") {
                if (mode != "single") {
                    eleSpan.css("height", 60);
                }
            }

            eleSpan.attr("className", "aim-ctrl");

            var structure;

            if (mode == "single") {
                structure = $(SingleStructure);
                htmlele.css("display", "none");
            } else {
                structure = $(MultiStructure);
                htmlele.css("visibility", "hidden");
                htmlele.css("width", "15");
            }
            eleSpan.append(structure);

            fileLinkSpan = $("#" + FileLinkSpanID);
            fileLinkSpan.css("height", parseInt(eleSpan.css("height").replace("px", "")) + 20);
            fileLinkSpan.css("overflow-y", "auto");
            fileInputField = $("#" + FileInputFieldID);
            fileBtnSpan = $("#" + FileBtnSpanID);
            btnFileAdd = $("#" + BtnFileAddID);
            btnFileDel = $("#" + BtnFileDelID);
            btnFileClr = $("#" + BtnFileClrID);
            btnFileOpn = $("#" + BtnFileOpnID);

            fileLinkSpan.append(htmlele);

            if (htmlele.attr("Required")) {
                structure.find(".aim-ctrl-file").css("background", FIELD_REQUIRED_BGCOLOR)

                if (mode == "single") {
                    fileInputField.css("background", FIELD_REQUIRED_BGCOLOR);
                    htmlele.removeClass("validate[required]");
                    fileInputField.addClass("validate[required]");
                }
            }

            btnFileAdd.click(function () {
                $('#fileframe').attr("src", getUploadUrl());
                $('#showDialog').dialog('open');
            });

            btnFileDel.click(function () {
                $.each(fileLinkSpan.find("input[type='checkbox']"), function () {
                    if (this.checked) {
                        var ffname = $(this.parentNode).attr("linkfile");
                        removeFile(ffname);
                    }
                });
                if (htmlele.attr("DeleteAfter")) {
                    try {
                        eval(htmlele.attr("DeleteAfter") + "('');");
                    } catch (e) { }
                }
            });

            btnFileClr.click(function () {
                htmlele.val('');
                clearFileView();
                if (htmlele.attr("ClearAfter")) {
                    try {
                        eval(htmlele.attr("ClearAfter") + "('');");
                    } catch (e) { }
                }
            });

            btnFileOpn.click(function () {
                if (htmlele.val()) {
                    var tflid = htmlele.val().substring(0, htmlele.val().indexOf("_"));
                    OpenWin(DownloadPage + '?Id=' + tflid, '_blank', 'width=1,height=1');
                }
            });

            htmlele.change(function () {
                refreshFileView();
            });

            refreshFileView();

            if (readOnly) {
                setReadOnly(readOnly);
            } else if (disabled) {
                setDisabled(disabled);
            }
        }

        // 刷新文件按视图
        function refreshFileView() {
            clearFileView();

            var fileval = htmlele.val();
            if (!fileval) return;

            if (mode == "single") {
                fileval = fileval.trimEnd(',');
                var tflname = fileval.substring(fileval.indexOf("_") + 1);
                var tflid = fileval.substring(0, fileval.indexOf("_"));

                var linkFile = $(SingleFileBlock.replace(/{filefullname}/g, fileval).replace(/{filename}/g, tflname).replace(/{fileid}/g, tflid));
                fileInputField.html(linkFile);
            } else {
                var ctrl = this;
                var fileVals = fileval.split(",");

                $.each(fileVals, function () {
                    if (this != "") {
                        var tflname = this.substring(this.indexOf("_") + 1);
                        var tflid = this.substring(0, this.indexOf("_"));

                        var linkFile = $(FileBlock.replace(/{filefullname}/g, this).replace(/{filename}/g, tflname).replace(/{fileid}/g, tflid));

                        if (readOnly || disabled) {
                            linkFile.find("input").css("display", "none");
                        }

                        if (mode == "single") {
                            linkFile.css("display", "none");
                        }

                        linkFile.insertBefore(htmlele);
                    }
                }
                );
            }
        }

        // 移除文件
        function removeFile(filefullname) {
            var fstr = filefullname + ","
            var val = htmlele.val().replace(fstr, "");

            fileLinkSpan.find("[linkfile='" + filefullname + "']").remove();
            htmlele.val(val);
        }

        // 清空文件视图
        function clearFileView() {
            if (mode == "single") {
                fileInputField.html("");
            } else {
                fileLinkSpan.find(".aim-ctrl-file-link").remove();
            }
        }

        function getValue() {
            return this.htmlele.val();
        }

        function setValue(val) {
            this.htmlele.val(val);
        }

        function setReadOnly(bool) {
            this.readOnly = bool;
            if (bool) {
                eleSpan.find("input").attr("readonly", true);
                fileBtnSpan.css("visibility", "hidden");
            } else {
                eleSpan.find("input").attr("readonly", false);
                fileBtnSpan.css("visibility", "visible");
            }
        }

        function setDisabled(bool) {
            if (bool) {
                eleSpan.find("input").attr("disabled", true);
                fileBtnSpan.css("visibility", "hidden");
            } else {
                eleSpan.find("input").attr("disabled", false);
                fileBtnSpan.css("visibility", "visible");
            }
        }

        // 获取上传文件路径
        function getUploadUrl() {
            var qrystr = "&IsLog=" + IsLog + "&Filter=" + escape(Filter)
                + "&MaximumUpload=" + MaximumUpload + "&MaxNumberToUpload=" + MaxNumberToUpload
                + "&AllowThumbnail=" + AllowThumbnail;

            if (mode == "single") {
                qrystr += "&IsSingle=true";
            }

            if (this.DoCheck) {
                qrystr += "&DoCheck=" + DoCheck;
            }

            var uploadurl = UploadPage + "?" + qrystr;
            return uploadurl;
        }
    }
    $.parser.plugins.push('file');
})(jQuery);

//------------------------Aim File 结束------------------------//


//------------------------popcombobox 弹出选择+自动完成开始------------------------//
(function ($) {
    function getRowIndex(target, value) {
        var state = $.data(target, 'popcombobox');
        return $.easyui.indexOfArray(state.data, state.options.valueField, value);
    }

    /**
	 * scroll panel to display the specified item
	 */
    function scrollTo(target, value) {
        var opts = $.data(target, 'popcombobox').options;
        var panel = $(target).combo('panel');
        var item = opts.finder.getEl(target, value);
        if (item.length) {
            if (item.position().top <= 0) {
                var h = panel.scrollTop() + item.position().top;
                panel.scrollTop(h);
            } else if (item.position().top + item.outerHeight() > panel.height()) {
                var h = panel.scrollTop() + item.position().top + item.outerHeight() - panel.height();
                panel.scrollTop(h);
            }
        }
        panel.triggerHandler('scroll');	// trigger the group sticking
    }

    function nav(target, dir) {
        var opts = $.data(target, 'popcombobox').options;
        var panel = $(target).popcombobox('panel');
        var item = panel.children('div.popcombobox-item-hover');
        if (!item.length) {
            item = panel.children('div.popcombobox-item-selected');
        }
        item.removeClass('popcombobox-item-hover');
        var firstSelector = 'div.popcombobox-item:visible:not(.popcombobox-item-disabled):first';
        var lastSelector = 'div.popcombobox-item:visible:not(.popcombobox-item-disabled):last';
        if (!item.length) {
            item = panel.children(dir == 'next' ? firstSelector : lastSelector);
        } else {
            if (dir == 'next') {
                item = item.nextAll(firstSelector);
                if (!item.length) {
                    item = panel.children(firstSelector);
                }
            } else {
                item = item.prevAll(firstSelector);
                if (!item.length) {
                    item = panel.children(lastSelector);
                }
            }
        }
        if (item.length) {
            item.addClass('popcombobox-item-hover');
            var row = opts.finder.getRow(target, item);
            if (row) {
                $(target).popcombobox('scrollTo', row[opts.valueField]);
                if (opts.selectOnNavigation) {
                    select(target, row[opts.valueField]);
                }
            }
        }
    }

    /**
	 * select the specified value
	 */
    function select(target, value, remainText) {
        var opts = $.data(target, 'popcombobox').options;
        var values = $(target).combo('getValues');
        if ($.inArray(value + '', values) == -1) {
            if (opts.multiple) {
                values.push(value);
            } else {
                values = [value];
            }
            setValues(target, values, remainText);
        }
    }

    /**
	 * unselect the specified value
	 */
    function unselect(target, value) {
        var opts = $.data(target, 'popcombobox').options;
        var values = $(target).combo('getValues');
        var index = $.inArray(value + '', values);
        if (index >= 0) {
            values.splice(index, 1);
            setValues(target, values);
        }
    }

    /**
	 * set values
	 */
    function setValues(target, values, remainText) {
        var opts = $.data(target, 'popcombobox').options;
        var panel = $(target).combo('panel');

        if (!$.isArray(values)) {
            values = values.split(opts.separator);
        }
        if (!opts.multiple) {
            values = values.length ? [values[0]] : [''];
        }

        // unselect the old rows
        $.map($(target).combo('getValues'), function (v) {
            if ($.easyui.indexOfArray(values, v) == -1) {
                var el = opts.finder.getEl(target, v);
                if (el.hasClass('popcombobox-item-selected')) {
                    el.removeClass('popcombobox-item-selected');
                    opts.onUnselect.call(target, opts.finder.getRow(target, v));
                }
            }
        });

        var theRow = null;
        var vv = [], ss = [];
        for (var i = 0; i < values.length; i++) {
            var v = values[i];
            var s = v;
            var row = opts.finder.getRow(target, v);
            if (row) {
                s = row[opts.textField];
                theRow = row;
                var el = opts.finder.getEl(target, v);
                if (!el.hasClass('popcombobox-item-selected')) {
                    el.addClass('popcombobox-item-selected');
                    opts.onSelect.call(target, row);
                }
            }
            vv.push(v);
            ss.push(s);
        }

        if (!remainText) {
            $(target).combo('setText', ss.join(opts.separator));
            if ($("#" + opts.nameField).length > 0)
                $("#" + opts.nameField).val(ss.join(opts.separator));
        }
        if (opts.showItemIcon) {
            var tb = $(target).popcombobox('textbox');
            tb.removeClass('textbox-bgicon ' + opts.textboxIconCls);
            if (theRow && theRow.iconCls) {
                tb.addClass('textbox-bgicon ' + theRow.iconCls);
                opts.textboxIconCls = theRow.iconCls;
            }
        }
        $(target).combo('setValues', vv);
        panel.triggerHandler('scroll');	// trigger the group sticking
    }

    /**
	 * load data, the old list items will be removed.
	 */
    function loadData(target, data, remainText) {
        var state = $.data(target, 'popcombobox');
        var opts = state.options;
        state.data = opts.loadFilter.call(target, data);

        opts.view.render.call(opts.view, target, $(target).combo('panel'), state.data);

        var vv = $(target).popcombobox('getValues');
        $.easyui.forEach(state.data, false, function (row) {
            if (row['selected']) {
                $.easyui.addArrayItem(vv, row[opts.valueField] + '');
            }
        });
        if (opts.multiple) {
            setValues(target, vv, remainText);
        } else {
            setValues(target, vv.length ? [vv[vv.length - 1]] : [], remainText);
        }

        opts.onLoadSuccess.call(target, data);
    }

    /**
	 * request remote data if the url property is setted.
	 */
    function request(target, url, param, remainText) {
        var opts = $.data(target, 'popcombobox').options;
        if (url) {
            opts.url = url;
        }
        param = $.extend({}, opts.queryParams, param || {});
        //		param = param || {};

        if (opts.onBeforeLoad.call(target, param) == false) return;

        opts.loader.call(target, param, function (data) {
            loadData(target, data, remainText);
        }, function () {
            opts.onLoadError.apply(this, arguments);
        });
    }

    /**
	 * do the query action
	 */
    function doQuery(target, q) {
        var state = $.data(target, 'popcombobox');
        var opts = state.options;

        var qq = opts.multiple ? q.split(opts.separator) : [q];
        if (opts.mode == 'remote') {
            _setValues(qq);
            request(target, null, { q: q }, true);
        } else {
            var panel = $(target).combo('panel');
            panel.find('.popcombobox-item-hover').removeClass('popcombobox-item-hover');
            panel.find('.popcombobox-item,.popcombobox-group').hide();
            var data = state.data;
            var vv = [];
            $.map(qq, function (q) {
                q = $.trim(q);
                var value = q;
                var group = undefined;
                for (var i = 0; i < data.length; i++) {
                    var row = data[i];
                    if (opts.filter.call(target, q, row)) {
                        var v = row[opts.valueField];
                        var s = row[opts.textField];
                        var g = row[opts.groupField];
                        var item = opts.finder.getEl(target, v).show();
                        if (s.toLowerCase() == q.toLowerCase()) {
                            value = v;
                            select(target, v, true);
                        }
                        if (opts.groupField && group != g) {
                            opts.finder.getGroupEl(target, g).show();
                            group = g;
                        }
                    }
                }
                vv.push(value);
            });
            _setValues(vv);
        }
        function _setValues(vv) {
            setValues(target, opts.multiple ? (q ? vv : []) : vv, true);
        }
    }

    function doEnter(target) {
        var t = $(target);
        var opts = t.popcombobox('options');
        var panel = t.popcombobox('panel');
        var item = panel.children('div.popcombobox-item-hover');
        if (item.length) {
            var row = opts.finder.getRow(target, item);
            var value = row[opts.valueField];
            if (opts.multiple) {
                if (item.hasClass('popcombobox-item-selected')) {
                    t.popcombobox('unselect', value);
                } else {
                    t.popcombobox('select', value);
                }
            } else {
                t.popcombobox('select', value);
            }
        }
        var vv = [];
        $.map(t.popcombobox('getValues'), function (v) {
            if (getRowIndex(target, v) >= 0) {
                vv.push(v);
            }
        });
        t.popcombobox('setValues', vv);
        if (!opts.multiple) {
            t.popcombobox('hidePanel');
        }
    }

    /**
	 * create the component
	 */
    function create(target) {
        var state = $.data(target, 'popcombobox');
        var opts = state.options;

        $(target).addClass('popcombobox-f');
        $(target).combo($.extend({}, opts, {
            onShowPanel: function () {
                $(this).combo('panel').find('div.popcombobox-item:hidden,div.popcombobox-group:hidden').show();
                setValues(this, $(this).popcombobox('getValues'), true);
                $(this).popcombobox('scrollTo', $(this).popcombobox('getValue'));
                opts.onShowPanel.call(this);
            }
        }));

        var p = $(target).combo('panel');
        p.unbind('.popcombobox');
        for (var event in opts.panelEvents) {
            p.bind(event + '.popcombobox', { target: target }, opts.panelEvents[event]);
        }
        //修改下拉图标样式,并扩展popup jnp
        var combo = $.data(target, "combo").combo;
        if (opts.nameField && opts.nameField != "")
            combo.append("<input id='" + opts.nameField + "' name='" + opts.nameField + "' type=hidden>")
        combo.find(".combo-arrow").addClass("popcombobox-search");
        combo.find(".combo-arrow").on("click", function (e) {
            var url = "/Common/Index";
            if (opts.popurl) {
                url = opts.popurl;
            }
            if (!$("#popcomboxwindow")[0]) {
                $(document.body).append("<div id='popcomboxwindow'></div>")
            }

            var dialog = $('#popcomboxwindow').dialog({
                title: '请选择', width: opts.dialogWidth, height: opts.dialogHeight,
                closed: false, cache: false,
                href: url, modal: true, resizable: true, maximizable: true,
                queryParams: { controlId: target.id }
            });
            e.stopPropagation();
            return false;

        })
    }

    function mouseoverHandler(e) {
        $(this).children('div.popcombobox-item-hover').removeClass('popcombobox-item-hover');
        var item = $(e.target).closest('div.popcombobox-item');
        if (!item.hasClass('popcombobox-item-disabled')) {
            item.addClass('popcombobox-item-hover');
        }
        e.stopPropagation();
    }
    function mouseoutHandler(e) {
        $(e.target).closest('div.popcombobox-item').removeClass('popcombobox-item-hover');
        e.stopPropagation();
    }
    function clickHandler(e) {
        var target = $(this).panel('options').comboTarget;
        if (!target) { return; }
        var opts = $(target).popcombobox('options');
        var item = $(e.target).closest('div.popcombobox-item');
        if (!item.length || item.hasClass('popcombobox-item-disabled')) { return }
        var row = opts.finder.getRow(target, item);
        if (!row) { return }
        var value = row[opts.valueField];
        if (opts.multiple) {
            if (item.hasClass('popcombobox-item-selected')) {
                unselect(target, value);
            } else {
                select(target, value);
            }
        } else {
            $(target).popcombobox('setValue', value).popcombobox('hidePanel');
        }
        e.stopPropagation();
    }
    function scrollHandler(e) {
        var target = $(this).panel('options').comboTarget;
        if (!target) { return; }
        var opts = $(target).popcombobox('options');
        if (opts.groupPosition == 'sticky') {
            var stick = $(this).children('.popcombobox-stick');
            if (!stick.length) {
                stick = $('<div class="popcombobox-stick"></div>').appendTo(this);
            }
            stick.hide();
            var state = $(target).data('popcombobox');
            $(this).children('.popcombobox-group:visible').each(function () {
                var g = $(this);
                var groupData = opts.finder.getGroup(target, g);
                var rowData = state.data[groupData.startIndex + groupData.count - 1];
                var last = opts.finder.getEl(target, rowData[opts.valueField]);
                if (g.position().top < 0 && last.position().top > 0) {
                    stick.show().html(g.html());
                    return false;
                }
            });
        }
    }

    $.fn.popcombobox = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.popcombobox.methods[options];
            if (method) {
                return method(this, param);
            } else {
                return this.combo(options, param);
            }
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'popcombobox');
            if (state) {
                $.extend(state.options, options);
            } else {
                state = $.data(this, 'popcombobox', {
                    options: $.extend({}, $.fn.popcombobox.defaults, $.fn.popcombobox.parseOptions(this), options),
                    data: []
                });
            }
            create(this);
            if (state.options.data) {
                loadData(this, state.options.data);
            } else {
                var data = $.fn.popcombobox.parseData(this);
                if (data.length) {
                    loadData(this, data);
                }
            }
            request(this);
        });
    };

    $.fn.popcombobox.methods = {
        options: function (jq) {
            var copts = jq.combo('options');
            return $.extend($.data(jq[0], 'popcombobox').options, {
                width: copts.width,
                height: copts.height,
                originalValue: copts.originalValue,
                disabled: copts.disabled,
                readonly: copts.readonly
            });
        },
        cloneFrom: function (jq, from) {
            return jq.each(function () {
                $(this).combo('cloneFrom', from);
                $.data(this, 'popcombobox', $(from).data('popcombobox'));
                $(this).addClass('popcombobox-f').attr('comboboxName', $(this).attr('textboxName'));
            });
        },
        getData: function (jq) {
            return $.data(jq[0], 'popcombobox').data;
        },
        setValues: function (jq, values) {
            return jq.each(function () {
                setValues(this, values);
            });
        },
        setValue: function (jq, value) {
            return jq.each(function () {
                setValues(this, $.isArray(value) ? value : [value]);
            });
        },
        clear: function (jq) {
            return jq.each(function () {
                setValues(this, []);
            });
        },
        reset: function (jq) {
            return jq.each(function () {
                var opts = $(this).popcombobox('options');
                if (opts.multiple) {
                    $(this).popcombobox('setValues', opts.originalValue);
                } else {
                    $(this).popcombobox('setValue', opts.originalValue);
                }
            });
        },
        loadData: function (jq, data) {
            return jq.each(function () {
                loadData(this, data);
            });
        },
        reload: function (jq, url) {
            return jq.each(function () {
                if (typeof url == 'string') {
                    request(this, url);
                } else {
                    if (url) {
                        var opts = $(this).popcombobox('options');
                        opts.queryParams = url;
                    }
                    request(this);
                }
            });
        },
        select: function (jq, value) {
            return jq.each(function () {
                select(this, value);
            });
        },
        unselect: function (jq, value) {
            return jq.each(function () {
                unselect(this, value);
            });
        },
        scrollTo: function (jq, value) {
            return jq.each(function () {
                scrollTo(this, value);
            });
        }
    };

    $.fn.popcombobox.parseOptions = function (target) {
        var t = $(target);
        return $.extend({}, $.fn.combo.parseOptions(target), $.parser.parseOptions(target, [
			'valueField', 'textField', 'groupField', 'groupPosition', 'mode', 'method', 'url',
            'popurl', 'nameField', 'datasql', 'displaynames', 'searchnames', 'sort', 'order', 'dialogWidth', 'dialogHeight',
			{ showItemIcon: 'boolean', limitToList: 'boolean' }
        ]));
    };

    $.fn.popcombobox.parseData = function (target) {
        var data = [];
        var opts = $(target).popcombobox('options');
        $(target).children().each(function () {
            if (this.tagName.toLowerCase() == 'optgroup') {
                var group = $(this).attr('label');
                $(this).children().each(function () {
                    _parseItem(this, group);
                });
            } else {
                _parseItem(this);
            }
        });
        return data;

        function _parseItem(el, group) {
            var t = $(el);
            var row = {};
            row[opts.valueField] = t.attr('value') != undefined ? t.attr('value') : t.text();
            row[opts.textField] = t.text();
            row['selected'] = t.is(':selected');
            row['disabled'] = t.is(':disabled');
            if (group) {
                opts.groupField = opts.groupField || 'group';
                row[opts.groupField] = group;
            }
            data.push(row);
        }
    };

    var COMBOBOX_SERNO = 0;
    var defaultView = {
        render: function (target, container, data) {
            var state = $.data(target, 'popcombobox');
            var opts = state.options;

            COMBOBOX_SERNO++;
            state.itemIdPrefix = '_easyui_combobox_i' + COMBOBOX_SERNO;
            state.groupIdPrefix = '_easyui_combobox_g' + COMBOBOX_SERNO;
            state.groups = [];

            var dd = [];
            var group = undefined;
            for (var i = 0; i < data.length; i++) {
                var row = data[i];
                var v = row[opts.valueField] + '';
                var s = row[opts.textField];
                var g = row[opts.groupField];

                if (g) {
                    if (group != g) {
                        group = g;
                        state.groups.push({
                            value: g,
                            startIndex: i,
                            count: 1
                        });
                        dd.push('<div id="' + (state.groupIdPrefix + '_' + (state.groups.length - 1)) + '" class="popcombobox-group">');
                        dd.push(opts.groupFormatter ? opts.groupFormatter.call(target, g) : g);
                        dd.push('</div>');
                    } else {
                        state.groups[state.groups.length - 1].count++;
                    }
                } else {
                    group = undefined;
                }

                var cls = 'popcombobox-item' + (row.disabled ? ' popcombobox-item-disabled' : '') + (g ? ' popcombobox-gitem' : '');
                dd.push('<div id="' + (state.itemIdPrefix + '_' + i) + '" class="' + cls + '">');
                if (opts.showItemIcon && row.iconCls) {
                    dd.push('<span class="popcombobox-icon ' + row.iconCls + '"></span>');
                }
                dd.push(opts.formatter ? opts.formatter.call(target, row) : s);
                dd.push('</div>');
            }
            $(container).html(dd.join(''));
        }
    };

    $.fn.popcombobox.defaults = $.extend({}, $.fn.combo.defaults, {
        valueField: 'value',
        textField: 'text',
        groupPosition: 'static',	// or 'sticky'
        groupField: null,
        groupFormatter: function (group) { return group; },
        mode: 'local',	// or 'remote'
        method: 'post',
        url: null,
        data: null,
        queryParams: {},
        showItemIcon: false,
        limitToList: false,	// limit the inputed values to the listed items
        view: defaultView,
        dialogWidth: 700,
        dialogHeight: 400,

        keyHandler: {
            up: function (e) { nav(this, 'prev'); e.preventDefault() },
            down: function (e) { nav(this, 'next'); e.preventDefault() },
            left: function (e) { },
            right: function (e) { },
            enter: function (e) { doEnter(this) },
            query: function (q, e) { doQuery(this, q) }
        },
        inputEvents: $.extend({}, $.fn.combo.defaults.inputEvents, {
            blur: function (e) {
                var target = e.data.target;
                var opts = $(target).popcombobox('options');
                if (opts.limitToList) {
                    doEnter(target);
                }
            }
        }),
        panelEvents: {
            mouseover: mouseoverHandler,
            mouseout: mouseoutHandler,
            click: clickHandler,
            scroll: scrollHandler
        },
        filter: function (q, row) {
            var opts = $(this).popcombobox('options');
            return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0;
        },
        formatter: function (row) {
            var opts = $(this).popcombobox('options');
            return row[opts.textField];
        },
        loader: function (param, success, error) {
            var opts = $(this).popcombobox('options');
            if (!opts.url) opts.url = "/Common/GetDatas";
            var url = opts.url;
            //矫正初始值获取绑定
            if (url.indexOf("q=") > 0 && param && param.q) {
                url = url.split("?")[0] + "?" + (opts.url.split("?")[1], "q");
            }
            if ($("#" + opts.valueField).val() != "" && (param.q == null || param.q == "")) {
                param.q = $("#" + this.id).val();
            }
            param.valueField = opts.valueField;
            param.textField = opts.textField;
            if (opts.datasql)
                param.datasql = opts.datasql;
            $.ajax({
                type: opts.method,
                url: url,
                data: param,
                dataType: 'json',
                success: function (data) {
                    success(data);
                },
                error: function () {
                    error.apply(this, arguments);
                }
            });
        },
        loadFilter: function (data) {
            return data;
        },
        finder: {
            getEl: function (target, value) {
                var index = getRowIndex(target, value);
                var id = $.data(target, 'popcombobox').itemIdPrefix + '_' + index;
                return $('#' + id);
            },
            getGroupEl: function (target, gvalue) {
                var state = $.data(target, 'popcombobox');
                var index = $.easyui.indexOfArray(state.groups, 'value', gvalue);
                var id = state.groupIdPrefix + '_' + index;
                return $('#' + id);
            },
            getGroup: function (target, p) {
                var state = $.data(target, 'popcombobox');
                var index = p.attr('id').substr(state.groupIdPrefix.length + 1);
                return state.groups[parseInt(index)];
            },
            getRow: function (target, p) {
                var state = $.data(target, 'popcombobox');
                var index = (p instanceof $) ? p.attr('id').substr(state.itemIdPrefix.length + 1) : getRowIndex(target, p);
                return state.data[parseInt(index)];
            }
        },

        onBeforeLoad: function (param) { },
        onBeforePop: function (param) { },
        onLoadSuccess: function () { },
        onLoadError: function () { },
        onSelect: function (record) { },
        onUnselect: function (record) { }
    });
    $.parser.plugins.push("popcombobox");
})(jQuery);
/*------------------弹出框结束--------------------------*/

/*------------------超文本编辑器扩展开始--------------------------*/
(function ($, K) {
    if (!K)
        throw "KindEditor未定义!";

    function create(target) {
        var opts = $.data(target, 'kindeditor').options;
        var editor = K.create(target, opts);
        $.data(target, 'kindeditor').options.editor = editor;
    }

    $.fn.kindeditor = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.kindeditor.methods[options];
            if (method) {
                return method(this, param);
            }
        }
        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'kindeditor');
            if (state) {
                $.extend(state.options, options);
            } else {
                state = $.data(this, 'kindeditor', {
                    options: $.extend({}, $.fn.kindeditor.defaults, $.fn.kindeditor.parseOptions(this), options)
                });
            }
            create(this);
        });
    }

    $.fn.kindeditor.parseOptions = function (target) {
        return $.extend({}, $.parser.parseOptions(target, []));
    };

    $.fn.kindeditor.methods = {
        editor: function (jq) {
            return $.data(jq[0], 'kindeditor').options.editor;
        }
    };

    $.fn.kindeditor.defaults = {
        resizeType: 1,
        allowPreviewEmoticons: false,
        allowImageUpload: false,
        items: [
            'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
            'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
            'insertunorderedlist', '|', 'emoticons', 'image', 'link'],
        afterChange: function () {
            this.sync();
        },
        afterBlur: function () { this.sync(); }
    };
    $.parser.plugins.push("kindeditor");
})(jQuery, KindEditor);

/*------------------超文本编辑器结束--------------------------*/