
//------------------------Aim 页面处理 开始------------------------//

var AimState = {};
var AimSearchCrit = {}; // 数据查询规则
var AimDefQryTarget = null;    // 默认查询目标
var AimCtrl = {};
var AimFrm = null;
var AimDlg = null;
var pgInitEvent = new Aim.EventHelper();    // 页面初始化事件
var pgLoadEvent = new Aim.EventHelper();    // 页面加载事件
var pgCssRules = null;    // 页面样式规则集
var IsAimFullSearch = false; //默认不是全文检索

var PAGE_PATH = "";

var pgOperation, pgOperationType, pgAction;

//快捷设置
var quickSetting;

// 页面初始化
$().ready(function() {
    PAGE_PATH = /[\s\S]*[/]/.exec(window.document.location.pathname);

    //pgCssRules = $.getCssRule();
    AimState = getPageState() || AimState;

    if (AimState) {
        AimSearchCrit = AimState["SearchCriterion"] || {}
    }

    if (typeof (onPgInit) == "function") {
        pgInitEvent.subscribe(onPgInit);
    }

    if (typeof (onPgLoad) == "function") {
        pgLoadEvent.subscribe(onPgLoad);
    }

    pgInitEvent.notify();

    pgInit();

    pgLoadEvent.notify();
});

function getPageState() {
    var pgState = null;
    var psstr = $("#__PAGESTATE").val();

    if (psstr != null) {
        // psstr = unescape(psstr);
        pgState = $.getJsonObj(psstr);
    }

    return pgState;
}

// 清理iframe防止内存泄漏
function unloadPage() {
    var innerFrames = document.frames;

    for (var i = 0; i < innerFrames.length; i++) {
        try {
            if (innerFrames[i].UnloadPage) {
                innerFrames[i].UnloadPage();
            }

            innerFrames[i].document.write("");
            innerFrames[i].document.clear();
        } catch (e) { }
    }

    try {
        CollectGarbage();
    } catch (e) { }
}

// 初始化Aim页面
function pgInit() {
    // 设置必填项属性
    $("[class*=validate]").filter("[class*=required]").attr("Required", true);

    pgOperation = $.getQueryString({ ID: "op" });   // 操作
    pgOperationType = $.getQueryString({ ID: "optype", DefaultValue: "s" });   // 操作执行位置(s, c)服务器端客户端

    initAimEvt();   // 初始化事件
    initAimCtrl();  // 初始化Aim控件
    initAimUI();    // 初始化Aim样式
    //initAimQry();   // 初始化Aim查询控件
    initAimSubQry();  //初始化Aim订阅查询
}

// 初始化事件
function initAimEvt() {
    $(window).bind("beforeunload", function() {
        // 清理iframe防止内存泄漏
        unloadPage();

        for (var i = 0; i < AimCtrl.length; i++) {
            AimCtrl[i] = null;
        }
    });

    $(window).bind("beforeprint", function() {
        // 隐藏按钮（并保存当前按钮状态以便于还原）
        // $("aim-ui-button-panel")
    });

    $(window).bind("afterprint", function() {
        // 还原按钮
    });

    $(document).bind("mousedown", function() {
        //隐藏首页的个人信息窗口,下拉菜单窗口
        try {
            window.parent.doHideWindow();
            //隐藏本页面的快捷设置
            quickSetting && quickSetting.hide();
        } catch (ex) {
        }
    });

    //点击父页面是关闭本页面可能的弹出框
    $(window.parent.document.body).bind("mousedown", function() {
        //隐藏首页的个人信息窗口,下拉菜单窗口
        try {

            //隐藏本页面的快捷设置
            quickSetting && quickSetting.hide();
        } catch (ex) {
        }
    });
}

// 初始化Aim控件
function initAimCtrl() {
    // 首先创建Aim弹出框（有可能其他控件会在初始化时与用户交互）
    AimDlg = ((typeof (Aim.Dialog) == "undefined") ? null : (new Aim.Dialog()));
    var firstGrid = null;
    var actrl;
    $("*[aimctrl][id]").each(function(i) {
        var atype = $.attr(this, "aimctrl");
        if (atype && this.id) {
            switch (atype.toLowerCase()) {
                case "grid":
                    actrl = ((typeof (Aim.Grid) == "undefined") ? null : (new Aim.Grid(this)));
                    firstGrid = firstGrid || actrl;
                    break;
                case "window":
                    actrl = ((typeof (Aim.Window) == "undefined") ? null : (new Aim.Window(this)));
                    break;
                case "select":
                    actrl = ((typeof (Aim.Select) == "undefined") ? null : (new Aim.Select(this)));
                    break;
                case "popup":
                    actrl = ((typeof (Aim.PopUp) == "undefined") ? null : (new Aim.PopUp(this)));
                    break;
                case "user":
                    actrl = ((typeof (Aim.User) == "undefined") ? null : (new Aim.User(this)));
                    break;
                case "file":
                    actrl = ((typeof (Aim.File) == "undefined") ? null : (new Aim.File(this)));
                    break;
                case "file2":
                    actrl = ((typeof (Aim.File2) == "undefined") ? null : (new Aim.File2(this)));
                    break;
                case "date":
                    actrl = ((typeof (Aim.Date) == "undefined") ? null : (new Aim.Date(this)));
                    break;
                case "editor":
                    actrl = ((typeof (Aim.Editor) == "undefined") ? null : (new Aim.Editor(this)));
                    break;
                case "dept":
                    actrl = ((typeof (Aim.Dept) == "undefined") ? null : (new Aim.Dept(this)));
                    break;
                case "custom":
                    actrl = ((typeof (Aim.Custom) == "undefined") ? null : (new Aim.Custom(this)));
                    break;
                case "mcombox":
                    actrl = ((typeof (Aim.MCombox) == "undefined") ? null : (new Aim.MCombox(this)));
                    break;
                case "tcombox":
                    actrl = ((typeof (Aim.TCombox) == "undefined") ? null : (new Aim.TCombox(this)));
                    break;
            }

            this.Aim = actrl;

            if ($(this).attr("defQryTarget") != undefined) {
                AimDefQryTarget = actrl;
            }
        }
        AimCtrl[this.id] = actrl;
    });

    $("form[aimctrl][id]").each(function(i) {
        // form最后初始化
        var actrl = ((typeof (Aim.Form) == "undefined") ? null : (new Aim.Form(this)));
        AimFrm = actrl;
        AimCtrl[this.id] = actrl;
    });

    AimDefQryTarget = AimDefQryTarget || firstGrid; // 默认第一个grid控件为默认查询目标

    // 若没有属性为aimctrl的表单，则将页面中第一个表单生成为AimForm
    AimFrm = AimFrm || (new Aim.Form($("form")[0]));
}

// 初始化Aim样式
function initAimUI() {
    /*
    *编辑页面,区块是否可以隐藏
    */


    //$(".aim-form-block").each(function () {
    //    var preDiv = '<div class="aim-form-block-collapsiblewarp">';
    //    if ($(this).attr("collapsible")) {
    //        preDiv += '<input type="checkbox" class="aim-form-block-collapsible" aimchecked checked="checked" />';
    //    }
    //    preDiv += '</div>';
    //    $(this).find(".aim-form-blockhead").prepend(preDiv);
    //});

    /*$(".aim-form-block").each(function () {
    var preDiv = $("<div></div>").addClass("aim-form-block-collapsiblewarp");
    var checkBox = $('<input type="checkbox"/>').addClass("aim-form-block-collapsible").attr("checked", $(this).attr("collapsible") == "true" ? true : false);
    (typeof ($(this).attr("collapsible")) != "undefined") && preDiv.append(checkBox);
    $(this).find(".aim-form-blockhead").prepend(preDiv);
    });

    $(".aim-form-block[collapsible=false]>.aim-form-table:gt(0)").hide();

    $(".aim-form-block-collapsible").bind("click", function () {
    $(this).closest(".aim-form-table").next().toggle(!!$(this).attr("checked"));
    });*/

    //图标方式//

    $(".aim-form-block").each(function() {
        var preDiv = $("<div></div>").addClass("aim-form-block-collapsiblewarp");
        var cSpan = $("<div></div>").addClass("aim-form-block-collapsible").addClass($(this).attr("collapsibled") == "true" ? "collapse" : "expand");
        (typeof ($(this).attr("collapsible")) != "undefined") && preDiv.append(cSpan);
        $(this).find(".aim-form-blockhead").prepend(preDiv);
    });
    $(".aim-form-block-collapsible").bind("click", function() {
        $(this).closest(".aim-form-table").next().toggle($(this).attr("collapsible"));
        $(this).attr("collapsibled", $(this).attr("collapsibled") == "true" ? "false" : "true");
        $(this).removeClass("expand").removeClass("collapse").addClass($(this).attr("collapsibled") == "true" ? "collapse" : "expand");

    });
    /*--------------------------------*/


    // 必填项颜色
    /*
    不在需要输入框颜色提示必填项,文本颜色提示,文本前加米字号 
    */
    $(".aim-valid-required").each(function() {
        var text = $(this).text();
        if (!/^\*[\s\S]*$/.test(text)) {
            $(this).text("*" + text);
        }
    });

    /*快捷设置   默认一个页面只有一个*/

    if ($(".aim-ui-setting .aim-ui-setting-btn").length > 0) {
        quickSetting = (quickSetting || new Neter.DropDownMenu({
            trigger: $(".aim-ui-setting .aim-ui-setting-btn"),
            mode: 'click',
            alignment: 'right',
            showStatus: false,
            items: [
                //{ key: 'help', name: '帮助', url: '/Help/QuestionQuery.aspx' },
                { name: '加载时间为：' + (AimState["PAGEEXECUTETIME"] ? Math.round(AimState["PAGEEXECUTETIME"] / 10) / 100 : 0) + '秒' }//,
                //{ name: "提交页面错误信息" }
            ],
            menuEvent: function(dropDownMenu, options) {
                //在此切换
                switch (options.key) {
                    case "help":
                        helper.openWin();
                        break;
                }
                dropDownMenu.hide();
            }
        }).render());

    }

    //$("[Required]").css({ background: FIELD_REQUIRED_BGCOLOR });

    $.each($("[dateonly]"), function() {
        1
        var val = $(this).val();
        $(this).val($.dateOnly(val));
    });

    if ($(".aim-ui-button-panel").length > 0) {
        $(".aim-ui-button-panel").css('padding', '10px');
    }

    //initAimExt();   // 初始化ExtJs

    switch (pgOperation) {
        case "c":
            $("input[type=checkbox]").not("[aimchecked]").attr('checked', false);
            pgAction = "create";
            break;
        case "cs":
            pgAction = "createsub";
            break;
        case "cp":
            pgAction = "copy";
        case "exec":
            pgAction = "execute";
            break;
        case "u":
            pgAction = "update";
            break;
        case "d":
            pgAction = "delete";
            if (AimFrm) {
                AimFrm.setStatus("readOnly", true);
            }
            break;
        case "r":
            AimFrm.setStatus("readOnly", true);
            $(":input").attr("disabled", true);

            /*$("[class*='aim-ui-table-edit']").find('td').css('border', 'solid 1px');
            // 所有输入框转换为文本框
            $("[class*=aim-ui-td-data]").each(function (i) {
            var ttd = this;
            //$(ttd).css("vertical-align", "top");
            $(ttd).css("height", ttd.offsetHeight);

                $(ttd).find("[class*=aim-ui-button]").css("display", "none");
            $(ttd).find("[class*=aim-ctrl]").css("display", "none");

                $(ttd).find("iframe").each(function () {
            // 设置FCKeditorAPI显示值
            var iframesrc = $(this).attr("src");
            if (iframesrc && iframesrc.indexOf(FCK_EDITOR_BASEPATH) >= 0) {
            $(this).css("display", "none");

                        var instanceName = $.getQueryString({ "URL": iframesrc, "ID": "InstanceName" });
            $(this).bind('load', function () {
            if (instanceName && AimCtrl[instanceName]) {
            $(this).before("<span>" + $("#" + instanceName).val() + "</span>");
            }
            });
            }
            });

                // 处理文件字段
            $(ttd).find("[class*=aim-ctrl-file-link]").each(function (k) {
            var ttdf = this;
            $(ttdf).find("input[type=checkbox]").css("display", "none");

                    $(ttdf).each(function (u) {
            $(this).css("display", "");
            $(this).appendTo(ttd);
            });
            });

                $(ttd).find(":input:visible").each(function (j) {
            var tinput = this;
            $(tinput).css("display", "none");

                    var ttext = "";
            if (tinput.type == "checkbox" || tinput.type == "radio") {
            if (tinput.checked) {
            ttext = "是";
            } else {
            ttext = "否";
            }
            } else if (tinput.tagName.equals('select')) {
            var val = $(tinput).val();

                        if (tinput.selectedIndex < 0 || val === '') {
            ttext = '';
            } else if (val != null && typeof (val) != 'undefined') {
            ttext = tinput.options[tinput.selectedIndex].text;
            }
            } else {
            ttext = $.htmlEncode($(tinput).val());
            }

                    $(tinput).before("<span>" + ttext + "</span>");
            });

            });*/
        default:
            if (pgOperation) {
                pgAction = pgOperation;
            }
            $("[class*='aim-ui-button submit']").hide();
            if (!window.opener && typeof (dialogArguments) == "undefined") {
                $("[class*='aim-ui-button cancel']").hide();
            }
            break;
    }
}

//初始化aim订阅查询
function initAimSubQry() {
    if ($.getQueryString({ ID: "mode" }) == "subs") {
        //确定是订阅查询

        var subs = unescape(decodeURI($.getQueryString({ ID: "subs" })));
        //先取出汉子,再进行url解码
        var sQrys = $.getJsonObj(subs);
        $.each(sQrys, function() {
            //var sQry = $.getJsonObj(this);
            $("#" + this.id).val(this.value);
        });
    }
}

// 操作类型
var OP_TYPE = {
    c: { name: "c", code: "create", text: "创建", action: "create" },
    r: { name: "r", code: "read", text: "读取", action: "read" },
    u: { name: "u", code: "update", text: "更新", action: "update" },
    d: { name: "d", code: "delete", text: "删除", action: "delete" },
    o: { name: "o", code: "other", text: "其他", action: "other" }
}

function getCssValue(selector, attribute) {
    ///	<summary>
    ///	获取样式值
    ///	</summary>
    if (!pgCssRules) {
        pgCssRules = $.getCssRule();
    }
    return (pgCssRules[selector]) ? (pgCssRules[selector][attribute]) ? pgCssRules[selector][attribute] : false : false;
}

//------------------------Aim 页面处理 结束------------------------//


//------------------------Aim 查询规则处理 开始------------------------//

// 查询方式
var SEARCH_MODE = {
    Equal: ["C", 0],
    NotEqual: ["C", 1],
    In: ["C", 2],
    NotIn: ["C", 3],
    Like: ["C", 4],
    NotLike: ["C", 5],
    GreaterThan: ["C", 6],
    GreaterThanEqual: ["C", 7],
    LessThan: ["C", 8],
    LessThanEqual: ["C", 9],
    StartWith: ["C", 10],
    EndWith: ["C", 11],
    NotStartWith: ["C", 12],
    NotEndWith: ["C", 13],
    UnSettled: ["C", 14],
    IsEmpty: ["S", 0],    // 查询集合时使用
    IsNotEmpty: ["S", 1],    // 查询集合时使用
    IsNull: ["S", 2],
    IsNotNull: ["S", 3],
    UnSettled: ["S", 4]   // 未设定
}

// 数据类型
var DATA_TYPE = {
    Integer: "Int32",
    Int: "Int32",
    Date: "DateTime"
}

// 获取查询模式值
function getSearchModeValue(str) {
    return SEARCH_MODE[str] || SEARCH_MODE["Equal"];    // 默认等于查询
}

// 初始化Aim查询控件
function initAimQry() {
    $("[aimqry], [qryopts], [qrygrp]").each(function(i) {
        var qryopts = $.getJsonObj($(this).attr("qryopts")) || {};
        var qryevent = qryopts["event"] || "keyup";

        if (qryevent && !$(this).attr(qryevent)) {
            $(this).bind(qryevent, function(event) {
                var tevent = event || window.event;
                if (qryevent != "keyup" || (qryevent == "keyup" && tevent.keyCode == 13)) {
                    qryEventHandler(this, tevent);
                }
            });
        }
    });
}
// 处理查询
function qryEventHandler(obj, event) {
    var qryopts = $.getJsonObj($(obj).attr("qryopts")) || {};
    var qryTarget = ($(qryopts["target"])[0] || {}).Aim || AimDefQryTarget;
    if (qryTarget) {
        var schCrit = getSchCriterion(obj);
        if (qryTarget.query) {
            qryTarget.query(schCrit["ccrit"], schCrit["ftcrit"]);
        }
    }
}

//// 处理查询
//function qryEventHandler(obj, event) {
//    var qryopts = $.getJsonObj($(obj).attr("qryopts")) || {};
//    var qryTarget = ($(qryopts["target"])[0] || {}).Aim || AimDefQryTarget;
//    if (qryTarget) {
//        var schCrit = getSchCriterion(obj);
//        if (qryTarget.query) {
//            qryTarget.query(schCrit["ccrit"], schCrit["ftcrit"]);
//        }
//    }
//}

// 由Html元素和查询标准模版获取查询标准对象
function getSchCriterion(htmlele, crit) {
    var tcrit = { ccrit: [], ftcrit: [], jcrit: [] };

    if (!htmlele) return crit || tcrit;
    crit = crit || tcrit;

    var aimgrp = $(htmlele).attr("aimgrp"); // 查询组

    var qryinputs = [];
    if (!aimgrp) {  // 没有组，则为单字段查询
        qryinputs[qryinputs.length] = htmlele;
    } else {
        qryinputs = $("[aimgrp=" + aimgrp + "]");
    }

    // 普通检索
    $.each(qryinputs, function(i) {
        var val;


        val = $(this).val();


        var _crit = getSingleSchCriterion($(this).attr("qryopts"), val);

        if (_crit["type"] == "fulltext") {
            crit["ftcrit"][crit["ftcrit"].length] = _crit["crit"];
        } else if (_crit["type"] == "junc") {
            crit["jcrit"][crit["jcrit"].length] = _crit["crit"];
        } else if (_crit["type"] == "default") {
            crit["ccrit"][crit["ccrit"].length] = _crit["crit"];
        }
    });

    return crit;
}

function getSingleSchCriterion(obj, val) {
    var opts = {};

    if (typeof (obj) == "string") {
        opts = $.getJsonObj(obj) || {};
    } else if (typeof (obj) == "object") {
        opts = obj;
    }

    var tcrit = {}
    var isft = false;

    // 查询类型 fulltext或default或junc(全文查询或默认查询)
    var type = opts["type"] || "default";

    // junc查询
    if (isJuncSchOptions(opts)) {
        type = "junc";
    }

    if (type.toLowerCase() == "fulltext") {
        tcrit["Value"] = val;
        tcrit["ColumnList"] = (opts["field"] ? opts["field"].split(",") : null);
    } else if (type.toLowerCase() == "junc") {
        tcrit = getJuncSchCriterion(opts, val);
    } else {
        tcrit["PropertyName"] = opts["field"] || obj.id || $(obj).attr("name");
        tcrit["Value"] = val;
        if (opts["datatype"]) {
            tcrit["Type"] = DATA_TYPE[opts["datatype"]] || opts["datatype"];
        }
        var schMode = getSearchModeValue(opts["mode"]);
        if (schMode[0] == "C") {
            tcrit["SearchMode"] = schMode[1]
        } else {
            tcrit["SingleSearchMode"] = schMode[1];
        }
    }

    return { crit: tcrit, type: type };
}

function isJuncSchOptions(opts) {
    return !!(opts["juncmode"] || opts["items"] && $.isArray(typeof (opts["items"])) || $.isArray(opts));
}

// 获取连接查询
function getJuncSchCriterion(opts, val) {
    var tcrit = { JunctionMode: 'Or', Searches: [] };
    var items = opts["items"] || [];
    tcrit["JunctionMode"] = opts["juncmode"] || "Or";

    if ($.isArray(opts)) {
        items = opts;
    }

    $.each(items, function() {
        var _tcrit = getSingleSchCriterion(this, val);
        if (_tcrit["crit"]) {
            tcrit["Searches"].push(_tcrit["crit"]);
        }
    });

    return tcrit;
}
//------------------------Aim 查询规则处理 结束------------------------//

//------------------------tab窗口------------------------//
function OpenTab(url, title, formid) {
    var win = window;
    var parent = 0;
    var navgat = window.navigation;
    var tabCon = window.tabContent;
    while (!navgat && window.parent.window) {
        parent++;
        if (parent > 5)
            return;
        navgat = window.parent.window.navigation;
        tabCon = window.parent.window.tabContent;
    }

    window.parent.beforeTab = navgat.getActiveNav();

    formid = formid ? formid : url;
    var nav = navgat.getNav("appid", formid);
    if (!nav) {
        //给tabcontent增加一个
        var tab = {};
        tab._nodeid = formid;
        tab.name = formid;
        tab.title = title;
        tab.type = "mdl";
        tab.showCloseButton = true;
        tab.url = url;
        tab.opener = window;
        tabCon.addTab(tab);
        nav = {};
        nav.name = title;
        nav.title = title;
        nav.appid = formid;
        nav.type = "mdl";
        nav.win = win;
        nav.closeButton = true;
        nav.active = false;
        //增加一个导航块,当导航块的条目超过20个的时候,新增的导航快插入到固定标签的后面
        var itemCount = navgat.handler.items.length;
        if (itemCount < 21) {
            navgat.insert(-1, nav);
            navgat.active(navgat.getIndexByProp("appid", formid));
        } else {
            navgat.insert(mdls.length, nav);
            window.setTimeout(function() {
                navgat.active(navgat.getIndexByProp("appid", formid));
            }, 0);
        }
    } else {
        navgat.active(navgat.getIndexByProp("appid", formid));
    }
}

function CloseTab(isRefresh) {
    var parent = 0;
    var navgat = window.navigation;
    var tabCon = window.tabContent;
    while (!navgat && window.parent.window) {
        parent++;
        if (parent > 5)
            return;
        navgat = window.parent.window.navigation;
        tabCon = window.parent.window.tabContent;
    }
    var formid = tabCon.getActiveTab().name;
    if (false) {
        if (navgat.getNav("appid", tabCon.getActiveTab().name).win && navgat.getNav("appid", formid).win.doSearch) {
            navgat.getNav("appid", formid).win.doSearch();
        }
        else if (navgat.getNav("appid", formid).win.IsAimFullSearch) {
            if (navgat.getNav("appid", tabCon.getActiveTab().name).win && navgat.getNav("appid", formid).win.doFullSearch) {
                navgat.getNav("appid", formid).win.doFullSearch();
            }
        }
        else if (navgat.getNav("appid", formid).win) {
            navgat.getNav("appid", formid).win.location.reload();
        }
    }

    if (window.parent.beforeTab) {
        //navgat.active(navgat.getIndexByProp("appid", window.parent.beforeTab.appid));
    }

    navgat.remove(navgat.getIndexByProp("appid", formid));
    //tabCon.removeTab(tabCon.getTab(formid));
}
//------------------------tab窗口结束------------------------//

//-----------------------帮助开始---------------------------//
var helper = {
    openWin: function(urlKey, keyWord, width, height, openWay) {
        var argArray = Array.prototype.slice.call(arguments);
        var param = {
            openWay: openWay || "open", //open|modal
            url: urlKey || "/Help/QuestionQuery.aspx",  //
            handlerUrl: "/Help/QuryHandler.aspx", //得到urlkey
            keyword: keyWord || "",
            width: width || window.screen.availWidth,
            height: height || window.screen.availHeight
        }
        var urlParrArr = $.getQueryUrl(window.location.href).split("/");
        var sourUrl = "";
        if (!!!urlKey) {
            sourUrl = urlParrArr[parseInt(urlParrArr.length) - 1];
        }
        if (argArray.length == 0) {

        }
        else if (argArray.length == 1) {

        }
        else if (argArray.length == 2) {

        }
        else if (argArray.length == 3) {

        }

        $.ajaxExec("GetQryInfo", { urlKey: sourUrl }, function(rtn) {
            var qParm = {
                Id: "",
                Code: "",
                KeyWord: "",
                UrlKey: "",
                NodeName: ""
            }

            $.each(rtn.data.Ents, function(i) {
                if (i > 0) {
                    qParm.Id += ",";
                    qParm.NodeName += ",";
                    qParm.Code += ",";
                    qParm.KeyWord += "|";
                    qParm.UrlKey += ",";
                }
                qParm.Id += this.Id || this.ID;
                qParm.Code += this.Code || this.CODE;
                qParm.KeyWord += this.KeyWord || this.KEYWORD;
                qParm.UrlKey += this.UrlKey || this.URLKEY;
                qParm.NodeName += this.NodeName || this.NODENAME;
            })

            if (!jQuery.isEmptyObject(qParm)) {
                var entJsonStr = $.getJsonString(qParm);
                param.url += param.url.indexOf("?") > -1 ? "&qParm=" : "?qParm=";
                param.url += escape(entJsonStr);
            }

            if (param.openWay.toLowerCase() == "open") {
                var iTop = (window.screen.availHeight - 30 - param.width) / 2;
                var iLeft = (window.screen.availWidth - 10 - param.height) / 2;
                window.open(param.url, "", 'height=' + param.height + ',innerHeight=' + param.height + ',width=' + param.width + ',innerWidth=' + param.width + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
            } else if ((param.openWay + "").toLowerCase() == "modal") {
                var mStyle = "dialogWidth:" + param.width + "px; dialogHeight:" + param.height + "px; scroll:yes; center:yes; status:no; resizable:yes;";
                rtn = window.showModalDialog(param.url, window, mStyle);
            }
        }, null, param.handlerUrl)
    }
};
//-----------------------------帮助结束----------------------------------//