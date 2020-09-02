function RootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    //return (prePath + postPath);如果发布IIS，有虚假目录用用这句
    return (prePath);
}
function AjaxJson(url, postData, callBack) {
    $.ajax({
        url: RootPath() + url,
        type: "post",
        data: postData,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.Code == "-1") {
                //alertDialog(data.Message, -1);
            } else {
                callBack(data);
            }
        },
        error: function (data) {
            if (!!data.responseText) {
                //top.layer.msg(data.responseText);
            } else {
                //top.layer.msg("无法与服务器建立通信，请求无效或失败！");
            }
        }
    });
}
//自动补全表格
var IndetableRow_autocomplete = 0;
var scrollTopheight = 0;
function autocomplete(Objkey, width, height, data, callBack) {
    if ($('#' + Objkey).attr('readonly') == 'readonly') {
        return false;
    }
    if ($('#' + Objkey).attr('disabled') == 'disabled') {
        return false;
    }
    IndetableRow_autocomplete = 0;
    scrollTopheight = 0;
    var X = $("#" + Objkey).offset().top;
    var Y = $("#" + Objkey).offset().left;
    $("#div_gridshow").html("");
    if ($("#div_gridshow").attr("id") == undefined) {
        $('body').append('<div id="div_gridshow" style="overflow: auto;z-index: 9999;border: 1px solid #A8A8A8;width:' + width + ';height:' + height + ';margin-top:10px;position: absolute; background-color: #fff; display: none;"></div>');
    } else {
        $("#div_gridshow").height(height);
        $("#div_gridshow").width(width);
    }
    var sbhtml = '<table class="grid" style="width: 100%;">';
    if (data != "") {
        sbhtml += '<tbody>' + data + '</tbody>';
    } else {
        sbhtml += '<tbody><tr><td style="color:red;text-align:center;width:' + width + ';">未检索到匹配的数据！</td></tr></tbody>';
    }
    sbhtml += '</table>';
    $("#div_gridshow").html(sbhtml);
    $("#div_gridshow").css("left", Y).css("top", X + $('#' + Objkey).height()).show();
    $("#div_gridshow .grid td").css("border-left", "none").css("padding-left", "2px");
    if (data != "") {
        $("#div_gridshow").find('tbody tr').each(function (r) {
            if (r == 0) {
                $(this).addClass('selected');
            }
        });
    }
    $("#div_gridshow").find('tbody tr').click(function () {
        var parameter = "";
        $(this).find('td').each(function (i) {
            parameter += '"' + $(this).attr('id') + '"' + ':' + '"' + $.trim($(this).text()) + '",'
        });
        if ($('#' + Objkey).attr('readonly') == 'readonly') {
            return false;
        }
        if ($('#' + Objkey).attr('disabled') == 'disabled') {
            return false;
        }
        callBack(JSON.parse('{' + parameter.substr(0, parameter.length - 1) + '}'));
        $("#div_gridshow").hide();
    });
    $("#div_gridshow").find('tbody tr').hover(function () {
        $(this).addClass("selected");
    }, function () {
        $(this).removeClass("selected");
    });
    //任意键关闭
    document.onclick = function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'div_gridshow') {
            if ($(tar).attr("id") == 'div_gridshow' || $(tar).attr("id") == Objkey) {
                $("#div_gridshow").show();
            } else {
                $("#div_gridshow").hide();
            }
        }

        if ($('#' + Objkey).attr('mdmkey') == 'WTFS')   //委托方式显示中文
        {
            var itmval = $('#' + Objkey).val();
            $.ajax({
                url: "/MDM/GetMDMDescription",
                data: { 'mdmkey': $('#' + Objkey).attr('mdmkey'), 'mdmfieldname': $('#' + Objkey).attr('mdmfieldname'), 'srch': itmval },
                type: "GET",
                async: false,
                success: function (rtn) {
                    $("#" + Objkey + "MS").val(rtn);
                }
            });
        }

        if ($('#' + Objkey).attr('name') == 'zbgjydw')   //报关经营单位显示中文
        {
            var itmval = $('#' + Objkey).val();
            $.ajax({
                url: "/MDM/GetBPDescription",
                data: { 'qstr': itmval },
                type: "GET",
                async: false,
                success: function (rtn) {
                    $("#" + Objkey + "MS").val(rtn);
                }
            });
        }
    }
}
//方向键上,方向键下,回车键
function autocompletekeydown(Objkey, callBack) {
    $("#" + Objkey).keydown(function (e) {
        switch (e.keyCode) {
            case 38: // 方向键上
                if (IndetableRow_autocomplete > 0) {
                    IndetableRow_autocomplete--
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight -= 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 40: // 方向键下
                var tindex = $("#div_gridshow").find('tbody tr').length - 1;
                if (IndetableRow_autocomplete < tindex) {
                    IndetableRow_autocomplete++;
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight += 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 13:  //回车键
                return false;
                //var parameter = "";
                //$("#div_gridshow").find('tbody tr').each(function (r) {
                //    if (r == IndetableRow_autocomplete) {
                //        $(this).find('td').each(function (i) {
                //            parameter += '"' + $(this).attr('id') + '"' + ':' + '"' + $.trim($(this).text()) + '",'
                //        });
                //    }
                //});
                //if ($('#' + Objkey).attr('readonly') == 'readonly') {
                //    return false;
                //}
                //if ($('#' + Objkey).attr('disabled') == 'disabled') {
                //    return false;
                //}
                //callBack(JSON.parse('{' + parameter.substr(0, parameter.length - 1) + '}'));
                //$("#div_gridshow").hide();
                break;
            default:
                break;
        }
    })
}
$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: true,
        url: "/MDM/GetDatas?mdm=",
        param: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    options.url = options.url + $element.attr('mdm');
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                });
                $element.select2({
                    placeholder: '请选择',
                    allowClear: true,
                    minimumResultsForSearch: options.search == true ? 0 : -1
                });
                $element.on("change", function (e) {
                    if (options.change != null) {
                        options.change(data[$(this).find("option:selected").index()]);
                    }
                    $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                });
            }
        });
    } else {
        $element.select2({
            minimumResultsForSearch: -1
        });
    }
}

//MDM自动补全
function AutocompleteMDM(mdmid, selshow) {
    var MDMCTRL = $("#" + mdmid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(mdmid, function (data) {
        $("#" + mdmid).val(data.CODE);
        if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
            $("#" + selshow).html(data.NAME);
        }
        var desc = $("#" + mdmid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }
        if (MDMCTRL.attr('mdmkey') == 'WTFS')   //委托方式显示中文
        {
            $("#" + mdmid + "MS").val(data.NAME);
        }
    });
    //获取数据源
    function DataSource(flag) {
        var url = "/MDM/GetDatas";
        var html = "";
        if (selshow == "kygj" || selshow == "kygn") {
            url += "?language=zh";
        }
        AjaxJson(url, { 'mdmkey': MDMCTRL.attr('mdmkey'), 'mdmfieldname': MDMCTRL.attr('mdmfieldname'), 'q': MDMCTRL.val() }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            if (DataJson.length > 0 && mdmid != "jsfjsZS") {
                html = "<tr><td id='CODE' style='width: 60px;'>*</td><td id='NAME' style='width: 100%;'>*</td></tr>";
            }
            $.each(DataJson, function (i) {
                //html += "<tr>";
                //for (var key in DataJson[0]) {
                //    console.log(key);
                //    var trid = key;
                //    html += '<td id="' + key + '" style="width: 100%;">' + DataJson[i][key] + '</td>';
                //}
                //html += "</tr>";

                html += "<tr>";
                //html += '<td id="RID" style="display: none;">' + DataJson[i].RID + '</td>';
                html += '<td id="CODE" style="width: 60px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            //autocomplete(mdmid, MDMCTRL.width() + "px", "200px", html, function (data) {
            //    $("#" + mdmid).val(data.text)
            //});
            var width = $("#" + mdmid).parent().width() + "px";
            var mdmname = MDMCTRL.attr('name');
            var mdmjc = mdmid.substring(0, mdmid.length - 2);
            //输入*的时候补全CODE和NAME
            if (MDMCTRL.val() == "*") {
                $("#" + mdmjc).val("*");
                $("#" + mdmjc + "C").val("*");
            }
            if (MDMCTRL.val() != "*" && flag == 2 && DataJson.length == 0) {
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.css("border-color", "#a90329");
                MDMCTRL.parent().removeClass('state-success').addClass("state-error");
                MDMCTRL.closest("label").after("<em for=" + mdmname + " class='cdinvalid'>未检索到匹配的数据！</em>");
            }
            if (flag == 2 && (MDMCTRL.val() == "*" || DataJson.length > 0)) {
                MDMCTRL.css("border-color", "#7dc27d");
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.parent().parent().find(".invalid").remove();
            }
            if (flag != 2) {
                autocomplete(mdmid, width, "305px", html, function (data) {
                    if (mdmid == "BZ" || mdmid == "CURRENCY") {
                        $("#" + mdmid).val(data.CODE);
                        // 隐藏域赋值
                        $("#" + mdmid + "CODE").val(data.CODE);
                        $("#" + mdmid + "C").val(data.CODE);
                    } else if (mdmid == "jsfjsZS") {
                        $("#" + mdmid).val(data.NAME);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                        $("#jsf").val("");
                        $("#jsfC").val("");
                        $("#jsfZS").val("");
                    } else {
                        $("#" + mdmid).val(data.NAME);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    }
                    var lockey = $("#" + mdmid).attr("mdmcode");
                    if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
                        $("#" + lockey).val(data.CODE);
                    }
                    if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
                        $("#" + selshow).html(data.NAME);
                    }
                    var desc = $("#" + mdmid).attr("mdmdesc");
                    if (desc != undefined && desc != null && desc.toString().length >= 1) {
                        $("#" + desc).html(data.NAME);
                        $("#" + desc).attr('title', data.NAME);
                    }
                    if (MDMCTRL.attr('mdmkey') == 'WTFS') {
                        $("#" + mdmid + "MS").val(data.NAME);
                    }
                });
            }
        });
    }
}

//MDMJC自动补全
function AutocompleteMDMJC(mdmid, selshow) {
    var MDMCTRL = $("#" + mdmid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(mdmid, function (data) {
        $("#" + mdmid).val(data.CODE);
        if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
            $("#" + selshow).html(data.NAME);
        }
        var desc = $("#" + mdmid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }
        if (MDMCTRL.attr('mdmkey') == 'WTFS')   //委托方式显示中文
        {
            $("#" + mdmid + "MS").val(data.NAME);
        }
    });
    //获取数据源
    function DataSource(flag) {
        var url = "/MDM/GetDataJC";
        var html = "";
        if (true) {
            url += "?language=zh";
        }
        AjaxJson(url, { 'mdmkey': MDMCTRL.attr('mdmkey'), 'mdmfieldname': MDMCTRL.attr('mdmfieldname'), 'q': MDMCTRL.val() }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            if (DataJson.length > 0) {
                html = "<tr><td id='CODE' style='width: 60px;'>*</td><td id='NAME' style='width: 100%;'>*</td></tr>";
            }
            $.each(DataJson, function (i) {
                //html += "<tr>";
                //for (var key in DataJson[0]) {
                //    console.log(key);
                //    var trid = key;
                //    html += '<td id="' + key + '" style="width: 100%;">' + DataJson[i][key] + '</td>';
                //}
                //html += "</tr>";

                html += "<tr>";
                //html += '<td id="RID" style="display: none;">' + DataJson[i].RID + '</td>';
                html += '<td id="CODE" style="width: 60px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            //autocomplete(mdmid, MDMCTRL.width() + "px", "200px", html, function (data) {
            //    $("#" + mdmid).val(data.text)
            //});
            var width = $("#" + mdmid).parent().width() + "px";
            var mdmname = MDMCTRL.attr('name');
            var mdmjc = mdmid.substring(0, mdmid.length - 2);
            //输入*的时候补全CODE和NAME
            if (MDMCTRL.val() == "*") {
                $("#" + mdmjc).val("*");
                $("#" + mdmjc + "C").val("*");
            }
            if (MDMCTRL.val() != "*" && flag == 2 && DataJson.length == 0) {
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.css("border-color", "#a90329");
                MDMCTRL.parent().removeClass('state-success').addClass("state-error");
                MDMCTRL.closest("label").after("<em for=" + mdmname + " class='cdinvalid'>未检索到匹配的数据！</em>");
            }
            if (flag == 2 && (MDMCTRL.val() == "*" || DataJson.length > 0)) {
                MDMCTRL.css("border-color", "#7dc27d");
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.parent().parent().find(".invalid").remove();
            }
            if (flag != 2) {
                autocomplete(mdmid, width, "305px", html, function (data) {
                    if (mdmid == "BZ" || mdmid == "CURRENCY" || mdmname == "EQUIP_TYPE") {
                        $("#" + mdmid).val(data.CODE);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    } else {
                        $("#" + mdmid).val(data.NAME);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    }
                    var lockey = $("#" + mdmid).attr("mdmcode");
                    if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
                        $("#" + lockey).val(data.CODE);
                    }
                    if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
                        $("#" + selshow).html(data.NAME);
                    }
                    var desc = $("#" + mdmid).attr("mdmdesc");
                    if (desc != undefined && desc != null && desc.toString().length >= 1) {
                        $("#" + desc).html(data.NAME);
                        $("#" + desc).attr('title', data.NAME);
                    }
                    if (MDMCTRL.attr('mdmkey') == 'WTFS') {
                        $("#" + mdmid + "MS").val(data.NAME);
                    }
                });
            }
        });
    }
}
//MDMJC自动补全（detailadd专用）
function AutocompleteMDMJCDa(mdmid, selshow) {
    var MDMCTRL = $("#" + mdmid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(mdmid, function (data) {
        $("#" + mdmid).val(data.CODE);
        if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
            $("#" + selshow).html(data.NAME);
        }
        var desc = $("#" + mdmid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }
        if (MDMCTRL.attr('mdmkey') == 'WTFS')   //委托方式显示中文
        {
            $("#" + mdmid + "MS").val(data.NAME);
        }
    });
    //获取数据源
    function DataSource(flag) {
        var url = "/MDM/GetDataJC";
        var html = "";
        if (true) {
            url += "?language=zh";
        }
        AjaxJson(url, { 'mdmkey': MDMCTRL.attr('mdmkey'), 'mdmfieldname': MDMCTRL.attr('mdmfieldname'), 'q': MDMCTRL.val() }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            if (DataJson.length > 0) {
                html = "<tr><td id='CODE' style='width: 60px;'>*</td><td id='NAME' style='width: 100%;'>*</td></tr>";
            }
            $.each(DataJson, function (i) {
                //html += "<tr>";
                //for (var key in DataJson[0]) {
                //    console.log(key);
                //    var trid = key;
                //    html += '<td id="' + key + '" style="width: 100%;">' + DataJson[i][key] + '</td>';
                //}
                //html += "</tr>";

                html += "<tr>";
                //html += '<td id="RID" style="display: none;">' + DataJson[i].RID + '</td>';
                html += '<td id="CODE" style="width: 60px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            //autocomplete(mdmid, MDMCTRL.width() + "px", "200px", html, function (data) {
            //    $("#" + mdmid).val(data.text)
            //});
            var width = $("#" + mdmid).parent().width() + "px";
            var mdmname = MDMCTRL.attr('name');
            var mdmjc = mdmid.substring(0, mdmid.length - 2);
            //输入*的时候补全CODE和NAME
            if (MDMCTRL.val() == "*") {
                $("#" + mdmjc).val("*");
                $("#" + mdmjc + "C").val("*");
            }
            if (MDMCTRL.val() != "*" && flag == 2 && DataJson.length == 0) {
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.css("border-color", "#a90329");
                MDMCTRL.parent().removeClass('state-success').addClass("state-error");
                MDMCTRL.closest("label").after("<em for=" + mdmname + " class='cdinvalid'>未检索到匹配的数据！</em>");
            }
            if (flag == 2 && (MDMCTRL.val() == "*" || DataJson.length > 0)) {
                MDMCTRL.css("border-color", "#7dc27d");
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.parent().parent().find(".invalid").remove();
            }
            if (flag != 2) {
                autocomplete(mdmid, width, "305px", html, function (data) {
                    if (mdmname == "EQUIP_TYPE") {
                        $("#" + mdmid).val(data.NAME);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    } else {
                        $("#" + mdmid).val(data.NAME);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    }
                    var lockey = $("#" + mdmid).attr("mdmcode");
                    if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
                        $("#" + lockey).val(data.CODE);
                    }
                    if (selshow != undefined && selshow != null && selshow.toString().length >= 1) {
                        $("#" + selshow).html(data.NAME);
                    }
                    var desc = $("#" + mdmid).attr("mdmdesc");
                    if (desc != undefined && desc != null && desc.toString().length >= 1) {
                        $("#" + desc).html(data.NAME);
                        $("#" + desc).attr('title', data.NAME);
                    }
                    if (MDMCTRL.attr('mdmkey') == 'WTFS') {
                        $("#" + mdmid + "MS").val(data.NAME);
                    }
                });
            }
        });
    }
}
//BP自动补全
function AutocompleteMDMBP(bpid) {
    var MDMCTRL = $("#" + bpid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(bpid, function (data) {
        $("#" + bpid).val(data.CODE);
        var bpkey = $("#" + bpid).attr("mdmkey");
        if (bpkey != undefined && bpkey != null && bpkey.toString().length >= 1) {
            $("#" + bpkey).val(data.RID);
        }
        var desc = $("#" + bpid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }

        if ($('#' + bpid).attr('name') == 'zbgjydw')   //报关经营单位显示中文
        {
            $("#" + bpid + "MS").val(data.NAME);
        }

        //发货方自动带出源位置；收货方自动带出目标位置；
        //修改收发货方时，同步赋值至原、目标位置
        //单独修改原、目标位置时，无其它逻辑
        if (bpid == "shipper_id") {
            $('#src_loc_id').val(data.CODE);
            $('#src_loc_key').val(data.RID);
        }
        else if (bpid == "consignee_id") {
            $('#des_loc_id').val(data.CODE);
            $('#des_loc_key').val(data.RID);
        }
    });
    //获取数据源
    function DataSource(flag) {
        var html = "";
        AjaxJson("/MDM/GetDatasBP", { 'q': MDMCTRL.val() }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            if (DataJson.length > 0) {
                html = "<tr><td id='CODE' style='width: 60px;'>*</td><td id='NAME' style='width: 100%;'>*</td></tr>";
            }
            $.each(DataJson, function (i) {
                html += "<tr>";
                html += '<td id="RID" style="display: none;">' + DataJson[i].RID + '</td>';
                html += '<td id="CODE" style="width: 90px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            //autocomplete(bpid, MDMCTRL.width() + "px", "200px", html, function (data) {
            //    $("#" + bpid).val(data.text)
            //});
            var width = $("#" + bpid).parent().width() + "px";
            var mdmname = MDMCTRL.attr('name');
            var mdmjc = bpid.substring(0, bpid.length - 2);
            //输入*的时候补全CODE和NAME
            if (MDMCTRL.val() == "*") {
                $("#" + mdmjc).val("*");
                $("#" + mdmjc + "C").val("*");
            }
            if (MDMCTRL.val() != "*" && flag == 2 && DataJson.length == 0) {
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.css("border-color", "#a90329");
                MDMCTRL.parent().removeClass('state-success').addClass("state-error");
                MDMCTRL.closest("label").after("<em for=" + mdmname + " class='cdinvalid'>未检索到匹配的数据！</em>");
            }
            if (flag == 2 && (MDMCTRL.val() == "*" || DataJson.length > 0)) {
                MDMCTRL.css("border-color", "#7dc27d");
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.parent().parent().find(".invalid").remove();
            }
            if (flag != 2) {
                autocomplete(bpid, width, "200px", html, function (data) {
                    $("#" + bpid).val(data.CODE);
                    $("#" + mdmjc).val(data.NAME);
                    $("#" + mdmjc + "C").val(data.CODE);
                    console.log($("#" + bpid + "CODE").val());
                    var bpkey = $("#" + bpid).attr("mdmkey");
                    if (bpkey != undefined && bpkey != null && bpkey.toString().length >= 1) {
                        // 隐藏域赋值
                        //$("#" + bpkey).val(data.RID);
                        $("#" + bpkey).val(data.CODE);
                    }
                    var desc = $("#" + bpid).attr("mdmdesc");
                    if (desc != undefined && desc != null && desc.toString().length >= 1) {
                        $("#" + desc).html(data.NAME);
                        $("#" + desc).attr('title', data.NAME);
                    }

                    if ($('#' + bpid).attr('name') == 'zbgjydw')   //报关经营单位显示中文
                    {
                        $("#" + bpid + "MS").val(data.NAME);
                    }

                    //发货方自动带出源位置；收货方自动带出目标位置；
                    //修改收发货方时，同步赋值至原、目标位置
                    //单独修改原、目标位置时，无其它逻辑
                    if (bpid == "shipper_id") {
                        $('#src_loc_id').val(data.CODE);
                        $('#src_loc_key').val(data.RID);
                    }
                    else if (bpid == "consignee_id") {
                        $('#des_loc_id').val(data.CODE);
                        $('#des_loc_key').val(data.RID);
                    }
                });
            }
        });
    }
}
//报价BP自动补全
function AutocompleteMDMBPBJ(bpid) {
    var MDMCTRL = $("#" + bpid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(bpid, function (data) {
        $("#" + bpid).val(data.NAME);
    });
    //获取数据源
    function DataSource(flag) {
        var html = "";
        AjaxJson("/MDM/GetDatasBPBJ", { 'q': MDMCTRL.val() }, function (DataJson) {
            $.each(DataJson, function (i) {
                html += "<tr>";
                html += '<td id="CODE" style="width: 90px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            var width = $("#" + bpid).parent().width() + "px";
            var mdmname = MDMCTRL.attr('name');
            var mdmjc = bpid.substring(0, bpid.length - 2);
            if (flag != 2) {
                autocomplete(bpid, width, "200px", html, function (data) {
                    $("#" + bpid).val(data.NAME);
                    $("#" + mdmjc).val(data.NAME);
                    $("#" + mdmjc + "C").val(data.CODE);
                    $("#jsfjs").val("");
                    $("#jsfjsC").val("");
                    $("#jsfjsZS").val("");
                });
            }
        });
    }
}

//位置LOC自动补全
function AutocompleteMDMLOC(locid, lockey, selshow) {
    var MDMCTRL = $("#" + locid);
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource();
        }
    }).focus(function () {
        $(this).select();
        DataSource();
    });
    //上，下键盘回调
    autocompletekeydown(locid, function (data) {
        $("#" + locid).val(data.CODE);
        var lockey = $("#" + locid).attr("mdmkey");
        if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
            $("#" + lockey).val(data.RID);
        }
        var locidconnect = $("#" + locid).attr("locidconnect");
        if (locidconnect != undefined && locidconnect != null && locidconnect.toString().length >= 1) {
            $("#" + locidconnect).val(data.CODE);
        }
        var lockeyconnect = $("#" + locid).attr("lockeyconnect");
        if (lockeyconnect != undefined && lockeyconnect != null && lockeyconnect.toString().length >= 1) {
            $("#" + lockeyconnect).val(data.RID);
        }
        var desc = $("#" + locid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }
    });
    //获取数据源
    function DataSource() {
        AjaxJson("/MDM/GetDatasLOC", { 'q': MDMCTRL.val() }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            var html = "";
            $.each(DataJson, function (i) {
                html += "<tr>";
                html += '<td id="RID" style="display: none;">' + DataJson[i].RID + '</td>';
                html += '<td id="CODE" style="width: 60px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            //autocomplete(locid, MDMCTRL.width() + "px", "200px", html, function (data) {
            //    $("#" + locid).val(data.text)
            //});
            var width = $("#" + locid).parent().width() + "px";
            autocomplete(locid, width, "200px", html, function (data) {
                $("#" + locid).val(data.CODE);
                var lockey = $("#" + locid).attr("mdmkey");
                if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
                    $("#" + lockey).val(data.RID);
                }
                var locidconnect = $("#" + locid).attr("locidconnect");
                if (locidconnect != undefined && locidconnect != null && locidconnect.toString().length >= 1) {
                    $("#" + locidconnect).val(data.CODE);
                }
                var lockeyconnect = $("#" + locid).attr("lockeyconnect");
                if (lockeyconnect != undefined && lockeyconnect != null && lockeyconnect.toString().length >= 1) {
                    $("#" + lockeyconnect).val(data.RID);
                }
                var desc = $("#" + locid).attr("mdmdesc");
                if (desc != undefined && desc != null && desc.toString().length >= 1) {
                    $("#" + desc).html(data.NAME);
                    $("#" + desc).attr('title', data.NAME);
                }
            });
        });
    }
}

//组织自动补全
function AutocompleteMDMORG(orgid) {
    var MDMCTRL = $("#" + orgid);
    var orgspo = $("#" + orgid).attr("mdmfieldname");
    if (orgspo == undefined || orgspo == null || orgspo == 'null' || orgspo.toString().length < 1) {
        orgspo = "";
    }
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource();
        }
    }).focus(function () {
        $(this).select();
        DataSource();
    });
    //上，下键盘回调
    autocompletekeydown(orgid, function (data) {
        $("#" + orgid).val(data.CODE);
        var desc = $("#" + orgid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }

        var orgidconnect = $("#" + orgid).attr("orgidconnect");
        if (orgidconnect != undefined && orgidconnect != null && orgidconnect.toString().length >= 1) {
            $("#" + orgidconnect).val(data.CODE);
        }
    });
    //获取数据源
    function DataSource() {
        AjaxJson("/MDM/GetDatasORG", { 'q': MDMCTRL.val(), 'spo': orgspo }, function (DataJson) {
            var html = "";
            $.each(DataJson, function (i) {
                html += "<tr>";
                html += '<td id="CODE" style="width: 90px;">' + DataJson[i].CODE + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                html += "</tr>";
            });
            var width = $("#" + orgid).parent().width() + "px";
            autocomplete(orgid, width, "200px", html, function (data) {
                $("#" + orgid).val(data.CODE);
                var desc = $("#" + orgid).attr("mdmdesc");
                if (desc != undefined && desc != null && desc.toString().length >= 1) {
                    $("#" + desc).html(data.NAME);
                    $("#" + desc).attr('title', data.NAME);
                }
                var orgidconnect = $("#" + orgid).attr("orgidconnect");
                if (orgidconnect != undefined && orgidconnect != null && orgidconnect.toString().length >= 1) {
                    $("#" + orgidconnect).val(data.CODE);
                }
            });
        });
    }
}

$.modalOpen = function (options) {
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };
    var options = $.extend(defaults, options);
    var _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
    var _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        yes: function () {
            options.callBack(options.id)
        }, cancel: function () {
            return true;
        }
    });
}

$.fn.SetWebControls = function (data) {
    var $id = $(this);
    for (var key in data) {
        var id = $id.find('#' + key);
        var idzs = $id.find('#' + key + 'ZS');
        if (id.attr('id')) {
            var type = id.attr('type');
            if (id.hasClass("select2")) {
                type = "select2";
            }
            else if (id.hasClass("datepicker") || id.hasClass("form-date")) {
                type = "datepicker";
            }
            else if (id.hasClass("form-datetime")) {
                type = "datetimepicker";
            }
            else if (id.hasClass("form-datetime2")) {
                type = "datetimepicker2";
            }
            //var value = $.trim(data[key]).replace(/&nbsp;/g, '');
            var value = data[key];
            switch (type) {
                case "checkbox":
                    if (value == 1) {
                        id.attr("checked", 'checked');
                    } else {
                        id.removeAttr("checked");
                    }
                    break;
                case "select2":
                    id.select2('val', value);
                    break;
                case "datepicker":
                    id.val(formatDate2(value, 'yyyy/MM/dd'));
                    break;
                case "datetimepicker":
                    id.val(formatDate2(value, 'yyyy/MM/dd hh:mm:ss'));
                    break;
                case "datetimepicker2":
                    id.val(formatDate2(value, 'yyyy/MM/dd hh:mm'));
                    break;
                default:
                    id.val(value);
                    idzs.val(value);
                    break;
            }
        }
    }
}

function GetWebControls(element) {
    //var reVal = "";

    //$(element).find('input,select,textarea').each(function (r) {
    //    var id = $(this).attr('id');
    //    var value = $(this).val();
    //    var type = $(this).attr('type');

    //    switch (type) {
    //        case "checkbox":
    //            if ($(this).is(':checked')) {
    //                reVal += '"' + id + '"' + ':' + '"1",'
    //            } else {
    //                reVal += '"' + id + '"' + ':' + '"0",'
    //            }
    //            break;
    //        default:
    //            if (value == "") {
    //                value = "";
    //            }
    //            //value = value.replace(/\\/g, '\\\\');
    //            //value = value.replace(/\n/g, '\\n');
    //            reVal += '"' + id + '"' + ':' + '"' + value + '",'
    //            break;
    //    }
    //});
    //reVal = reVal.substr(0, reVal.length - 1);
    //reVal = reVal.replace(/\\/g, '\\\\');
    //reVal = reVal.replace(/\n/g, '\\n');
    //reVal = reVal.replace(/\t/g, '\\t');

    var postdata = {};
    $(element).find('input,select,textarea').each(function (r) {
        var id = $(this).attr('id');
        var value = $(this).val();
        var type = $(this).attr('type');

        switch (type) {
            case "checkbox":
                if ($(this).is(':checked')) {
                    postdata[id] = 1;
                } else {
                    postdata[id] = 0;
                }
                break;
            default:
                postdata[id] = value;
                break;
        }
    });

    return postdata;
}

$.fn.serializeObject = function () {
    var o = {};
    this.find("[rwfld='rwfld']").each(function () {
        o[$(this).attr("name")] = $(this).val();
    });
    return o;
};

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function fqgenguid() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

String.prototype.replaceAll = function (FindText, RepText) {
    regExp = new RegExp(FindText, "g");
    return this.replace(regExp, RepText);
}
function fqLTrim(s) {
    return s.replace(/\b(0+)/gi, "");
}
request = function (keyValue) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == keyValue) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}

///**格式化yyyyMMddhhmmss日期时间显示方式**/
formatDate = function (dateString) {
    if (dateString == '0')
        return '';
    var pattern = /(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})/;
    var formatedDate = dateString.replace(pattern, '$1/$2/$3 $4:$5:$6');
    return formatedDate;
};

/**格式化时间显示方式、用法:format="yyyy-MM-dd hh:mm:ss";**/
formatDate2 = function (v, format) {
    if (!v) return "";
    var d = v;
    if (typeof v === 'string') {
        if (v.indexOf("/Date(") > -1)
            d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
        else
            d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
    }
    var o = {
        "M+": d.getMonth() + 1,  //month
        "d+": d.getDate(),       //day
        "h+": d.getHours(),      //hour
        "m+": d.getMinutes(),    //minute
        "s+": d.getSeconds(),    //second
        "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
        "S": d.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function scrollToCtrl(toid, tipmsg) {
    $("html,body").animate({ scrollTop: $("#" + toid).offset().top - 50 }, 1000);
    layer.tips(tipmsg, '#' + toid, {
        tips: [1, '#3595CC'],
        time: 5000
    });
}

/**
 * title string 对话框标题
 * msg string 消息内容
 * callback function 返回函数。
 **/
window.alert = function (title, msg, callback) {
    if (!title) {
        title = '对话框';
    }
    var dialogHTML = '<div id="selfAlert" class="modal fade">';
    dialogHTML += '<div class="modal-dialog">';
    dialogHTML += '<div class="modal-content">';
    dialogHTML += '<div class="modal-header">';
    dialogHTML += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">';
    dialogHTML += '<span aria-hidden="true">&times;</span>';
    dialogHTML += '</button>';
    dialogHTML += '<h4 class="modal-title">' + title + '</h4>';
    dialogHTML += '</div>';
    dialogHTML += '<div class="modal-body">';
    dialogHTML += msg;
    dialogHTML += '</div>';
    dialogHTML += '<div class="modal-footer">';
    dialogHTML += '<button type="button" class="btn btn-primary" data-dismiss="modal">确定</button>';
    dialogHTML += '</div>';
    dialogHTML += '</div>';
    dialogHTML += '</div>';
    dialogHTML += '</div>';

    if ($('#selfAlert').length <= 0) {
        $('body').append(dialogHTML);
    }

    $('#selfAlert').on('hidden.bs.modal', function () {
        $('#selfAlert').remove();
        if (typeof callback == 'function') {
            callback();
        }
    }).modal('show');
}

//设备检测  
function detectmob() {
    if (navigator.userAgent.match(/Android/i)
    || navigator.userAgent.match(/webOS/i)
    || navigator.userAgent.match(/iPhone/i)
    || navigator.userAgent.match(/iPad/i)
    || navigator.userAgent.match(/iPod/i)
    || navigator.userAgent.match(/BlackBerry/i)
    || navigator.userAgent.match(/Windows Phone/i)
    ) {
        $("#amenufld").trigger("click");
        $("#aminifymefld").trigger("click");
        //$("#smart-fixed-header").trigger("click");
        //$("#smart-fixed-navigation").trigger("click");
        return true;
    }
    else {
        return false;
    }
}

// 2018.4.20 chenbo 
// 位置LOC自动补全（select控件）
function AutocompleteMDMLOCselect(locid, lockey, selshow) {
    console.log(locid);
    var MDMCTRL = $("#" + locid);
    DataSource();
    MDMCTRL.bind("keyup", function (e) {
        console.log("keyup");
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            console.log("beginDataSource");
            DataSource();
            console.log(2);
        }
    }).focus(function () {
        $(this).select();
        DataSource();
        console.log(3);
    }).change(function () {
        console.log("change");
    });
    //上，下键盘回调
    autocompletekeydown(locid, function (data) {
        $("#" + locid).val(data.CODE);
        var lockey = $("#" + locid).attr("mdmkey");
        if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
            $("#" + lockey).val(data.RID);
        }
        var locidconnect = $("#" + locid).attr("locidconnect");
        if (locidconnect != undefined && locidconnect != null && locidconnect.toString().length >= 1) {
            $("#" + locidconnect).val(data.CODE);
        }
        var lockeyconnect = $("#" + locid).attr("lockeyconnect");
        if (lockeyconnect != undefined && lockeyconnect != null && lockeyconnect.toString().length >= 1) {
            $("#" + lockeyconnect).val(data.RID);
        }
        var desc = $("#" + locid).attr("mdmdesc");
        if (desc != undefined && desc != null && desc.toString().length >= 1) {
            $("#" + desc).html(data.NAME);
            $("#" + desc).attr('title', data.NAME);
        }
    });
    //获取数据源
    function DataSource() {
        console.log("DataSource");
        AjaxJson("/MDM/GetDatasLOC", { 'q': MDMCTRL.val() }, function (DataJson) {
            $.each(DataJson, function (i) {
                MDMCTRL.append($("<option></option>").attr("value", DataJson[i].CODE).html(DataJson[i].NAME));
            });
        });
    }
}
//位置LOC自动补全 (海运成本：起运港、目的港)
function AutocompleteMDMGK(locid, lockey, selshow) {
    var MDMCTRL = $("#" + locid);
    var MDMNAME = MDMCTRL.attr("name");
    var mdmloctype = MDMCTRL.attr("mdmloctype");
    MDMCTRL.bind("keyup", function (e) {
        if (e.which != 13 && e.which != 40 && e.which != 38) {
            DataSource(0);
        }
    }).focus(function () {
        $(this).select();
        DataSource(1);
    }).blur(function () {
        DataSource(2);
    });
    //上，下键盘回调
    autocompletekeydown(locid, function (data) {
        $("#" + locid).val(data.NAME);
        var lockey = $("#" + locid).attr("mdmkey");
        if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
            $("#" + lockey).val(data.CODE);
        }
    });
    //获取数据源
    function DataSource(flag) {
        var html = "";
        AjaxJson("/MDM/GetDatasLOC", { 'q': MDMCTRL.val(), 'mdmloctype': mdmloctype }, function (DataJson) {
            //for (var key in DataJson[0]) {
            //    console.log(key);
            //}
            if (DataJson.length > 0) {
                html = "<tr><td id='CODE' style='width: 60px;'>*</td><td id='NAME' style='width: 100%;'>*</td></tr>";
            }
            $.each(DataJson, function (i) {
                html += "<tr>";
                html += '<td id="CODE" style="width: 100px;">' + DataJson[i].RID + '</td>';
                html += '<td id="NAME" style="width: 100%;">' + DataJson[i].CODE + (DataJson[i].NAME == null ? "" : "(" + DataJson[i].NAME.replace(DataJson[i].CODE, "") + ")") + '</td>';
                //html += '<td id="CODE" style="width: 100px;">' + DataJson[i].CODE + '</td>';
                //html += '<td id="NAME" style="width: 100%;">' + DataJson[i].NAME + '</td>';
                //html += '<td id="NAME" style="width: 100%;">' + (DataJson[i].NAME == null ? "" : DataJson[i].NAME) + '</td>';
                html += "</tr>";
            });
            //点击事件回调
            var width = $("#" + locid).parent().width() + "px";
            var mdmjc = locid.substring(0, locid.length - 2);
            //输入*的时候补全CODE和NAME
            if (MDMCTRL.val() == "*") {
                $("#" + mdmjc).val("*");
                $("#" + mdmjc + "C").val("*");
            }
            if (MDMCTRL.val() != "*" && flag == 2 && DataJson.length == 0) {
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.css("border-color", "#a90329");
                MDMCTRL.parent().removeClass('state-success').addClass("state-error");
                MDMCTRL.closest("label").after("<em for=" + MDMNAME + " class='cdinvalid'>未检索到匹配的数据！</em>");
            }
            if (flag == 2 && (MDMCTRL.val() == "*" || DataJson.length > 0)) {
                MDMCTRL.css("border-color", "#7dc27d");
                MDMCTRL.parent().parent().find(".cdinvalid").remove();
                MDMCTRL.parent().parent().find(".invalid").remove();
            }
            if (flag != 2) {
                autocomplete(locid, width, "305px", html, function (data) {
                    if (locid.indexOf("ZZG") >= "0" || MDMNAME == "ZZZGHC") {
                        $("#" + locid).val(data.CODE);
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    } else {
                        $("#" + locid).val(data.CODE + "(" + data.NAME + ")");
                        $("#" + mdmjc).val(data.NAME);
                        $("#" + mdmjc + "C").val(data.CODE);
                    }
                    var lockey = $("#" + locid).attr("mdmkey");
                    if (lockey != undefined && lockey != null && lockey.toString().length >= 1) {
                        $("#" + lockey).val(data.CODE);
                    }
                });
            }
        });
    }
}
//function heartbeat() {
//    $.get(
//        "/Service/SessionHeartbeatHttpHandler.ashx",
//        null,
//        function (data) {
//            console.info("heartbeat_");
//        },
//        "json"
//    );
//}

//function setHeartbeat() {
//    setInterval("heartbeat()", 300000); // every 5 min
//}

//$(document).ready(function () {
//    setHeartbeat();
//});


