var i18n_msg = {};
i18n_msg.zh = {
    PageTemplate: "共<#totalRows#>条记录,分<#totalPages#>页显示"
    , Loading: "数据加载中..."
    , Waiting: "正在等待上一指令执行..."
    , Error: "执行失败，请联系管理员或稍后再试 ..."
    , Executing: "执行中 ..."
}
i18n_msg.en = {
    PageTemplate: "total <#totalRows#> record(s),<#totalPages#> page(s)"
    , Loading: "Loading data ..."
    , Waiting: "Running please wait ..."
    , Error: "There is an error please contact admin ..."
    , Executing: "Executing ..."

}
var base = {};
base.Get = function (param, func, url) {
    if (page.isLoading == true) {
        page.showMsg("error", '正在等待上次提交...', '请稍后重试...', 0);
        return;
    } else {
        var parm = param;
        var unique_id = page.showMsg("info", '数据加载中...', '请耐心等待...', 0);
        var fun_success = function (d) {
            page.isLoading = false;
            page.hideMsg(unique_id);
            func(d, param);
        };
        var fun_error = function () {
            page.isLoading = false;
            page.hideMsg(unique_id);
            page.showMsg("error", '数据加载失败', '请稍候重试，或联系管理员', 0);
        };
        page.isLoading = true;
        $.ajax({
            type: "POST",
            dataType: "Json",
            url: url,
            data: $.param(parm),
            success: fun_success,
            error: fun_error
        });
    }
};
base.Save = function (param, func, url) {
    if (page.isLoading == true) {
        page.showMsg("error", '正在等待上次提交...', '请稍后重试...', 2000);
        return;
    }

    var parm = param;
    var unique_id = page.showMsg("info", '数据保存中...', '请耐心等待...', 0);
    var fun_success = function (d) {
        page.isLoading = false;
        page.hideMsg(unique_id);
        func(d, param);
        if (d.IsSuccess) {
            page.showMsg("success", '数据保存成功', '', 3000);
        }
    };
    var fun_error = function () {
        page.isLoading = false;
        page.hideMsg(unique_id);
        page.showMsg("error", '数据保存失败', '请稍候重试，或联系管理员', 0);
    };
    page.isLoading = true;
    $.ajax({
        type: "POST",
        dataType: "Json",
        url: url,
        data: $.param(parm),
        success: fun_success,
        error: fun_error
    });
};

base._lang = function () {
    return window.location.pathname.substr(1, 5);
}
base.Lang = base._lang();

base.PageVisible = 12; //分页显示页数
base.PageTemplate = "共<#totalRows#>条记录,分<#totalPages#>页显示";
base.pageSize = 10;
base.pageSizeReport = 5000;
base.msg = i18n_msg.zh;

base.SetLang = function (lang) {
    if (lang == "en-US") {
        base.PageTemplate = i18n_msg.en["PageTemplate"];
        base.msg = i18n_msg.en;
    } else {
        base.PageTemplate = i18n_msg.zh["PageTemplate"];
        base.msg = i18n_msg.zh;
    }
}


var page = {};
page.isLoading = false;

page.showMsg = function (type, title, text, timeOut) {
    switch (type) {
        case "info":
            return toastr.info(text, title, { "timeOut": timeOut, "hideDuration": "0" });
            break;
        case "success":
            return toastr.success(text, title, { "timeOut": timeOut, "hideDuration": "0" });
            break;

        case "warning":
            return toastr.warning(text, title, { "timeOut": timeOut, "hideDuration": "0" });
            break;

        default:
            return toastr.error(text, title, { "timeOut": timeOut, "hideDuration": "0" });
            break;
    }
};

page.hideMsg = function (unique_id) {
    toastr.clear(unique_id);
};
