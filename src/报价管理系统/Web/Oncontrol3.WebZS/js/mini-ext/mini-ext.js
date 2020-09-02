
//----------Aim DataGrid Begin------------
mini.ux.AimDataGrid = function(config) {
    mini.ux.AimDataGrid.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.AimDataGrid, mini.DataGrid, {
    uiCls: 'aim-datagrid',
    initComponents: function(config) {
        config = config || {};
        config.idField = config.idProperty;
        config.multiSelect = !(config.multiSelect == false);
        config.allowAlternating = !(config.allowAlternating == false);
        config.height = config.height || "100%";
        config.showPager = false;           //不使用grid自带分页控件
        //config.showHGridLines = !(config.showHGridLines == false);
        //config.showVGridLines = (config.showVGridLines == true);


        this.set(config);

        if (config.aimpager) {//初始化分页控件区
            var pager = new mini.ux.AimPager({});
            var owner = this;
            pager.on("pagechanged", function(e) {
                var pinfo = {};
                pinfo.start = e.pageIndex * e.pageSize;
                pinfo.limit = e.pageSize;

                var url = window.location.search;
                if (url.indexOf("?") != -1) {
                    var str = url.substr(1)
                    strs = str.split("&");
                    for (i = 0; i < strs.length; i++) {
                        pinfo[strs[i].split("=")[0]] = strs[i].split("=")[1];
                    }
                }

                owner.reload(pinfo);
            });
            pager.render(owner.aimpager);
            pager.setId(owner.aimpager); //设置Id
        }

        if (config.renderTo) {
            this.render(config.renderTo);
        }

        //if (config.schpanel) {//初始化查询区
        //    initAimQry(this);
        //}
        initAimQry(this);
    },
    initEvent: function() {
        //绑定表头点击事件以实现排序,
        this.on("headercellclick", function(sender) {
            var column = sender.column;
            var dir, sort;
            if (column.sortable) {
                var oldField = grid.sortField;
                if (oldField != column.field) {
                    grid.setSortField(column.field);
                    grid.setSortOrder("asc");
                } else {
                    grid.setSortOrder(grid.sortOrder == "asc" ? "desc" : "asc");
                }
                dir = grid.sortOrder == "asc" ? "ASC" : "DESC";
                sort = grid.sortField;

                var parms = { dir: dir, sort: sort };
                var url = window.location.search;
                if (url.indexOf("?") != -1) {
                    var str = url.substr(1)
                    strs = str.split("&");
                    for (i = 0; i < strs.length; i++) {
                        parms[strs[i].split("=")[0]] = strs[i].split("=")[1];
                    }
                }
                grid.reload(parms);
                //grid.reload({ dir: dir, sort: sort });
            }
        });
    },
    reload: function(params) {
        AimReloadGrid(this, params);
    },
    GetModifiedDataStringArr: function(recs) {
        var dt = [];
        var rows = recs || this.getChanges();

        $.each(rows, function() {
            dt.push($.getJsonString(this));
        });

        return dt;
    }
});

function AimReloadGrid(grid, params) {
    mini.mask({
        el: document.body,
        cls: 'mini-mask-loading',
        html: '加载中...'
    });
    params = params || {};

    params.data = params.data || {};

    params["data"] = params["data"] || {};

    for (var key in params) {
        if (typeof (params[key]) == "string" || typeof (params[key]) == "number") {
            params["data"][key] = params[key];
        }
    }

    params["qrycrit"] = params["qrycrit"] || AimSearchCrit || {};

    if (params.schtype == "field" && params.schdom) {
        params.schcrit = getSchCriterion(params.schdom);

        if (params.start == undefined) {
            params.start = 1;
            AimSearchCrit["CurrentPageIndex"] = 1;
        }
    }

    //全文搜索
    if (params.schtype == "fulltext") {
        var tcrit = { ccrit: [], ftcrit: [], jcrit: [] };
        tcrit["ftcrit"][tcrit.ftcrit.length] = { Value: params.schval, ColumnList: params.schcols }
        params.schcrit = tcrit;
        if (params.start == undefined) {
            params.start = 1;
            AimSearchCrit["CurrentPageIndex"] = 1;
        }
        IsAimFullSearch = true;
    }
    else {
        IsAimFullSearch = false;
    }

    if (params.schcrit !== undefined) {
        params["qrycrit"]["Searches"] = params["qrycrit"]["Searches"] || {};

        params["qrycrit"]["Searches"]["Searches"] = params.schcrit["ccrit"] || [];
        params["qrycrit"]["Searches"]["FTSearches"] = params.schcrit["ftcrit"] || [];
        params["qrycrit"]["Searches"]["JuncSearches"] = params.schcrit["jcrit"] || [];
    }

    if (params.start !== undefined && params.limit !== undefined) {
        params["qrycrit"]["CurrentPageIndex"] = parseInt(params.start / params.limit) + 1;
        params["qrycrit"]["PageSize"] = params.limit;
        this.start = params.start;
        this.limit = params.limit;
    }

    if (params.dir !== undefined && params.sort !== undefined) {
        var orders = params["qrycrit"]["Orders"] || [];
        params["qrycrit"]["Orders"] = [{ "PropertyName": params.sort, "Ascending": (params.dir == "ASC")}];
    }

    var loader = new Aim.Data.RemoteModel(params);

    loader.onDataLoaded.subscribe(function(response) {
        grid.setData(response.data[grid.dsname]); //重新装载grid的数据

        if (grid.aimload) {
            grid.aimload(response.data);
        }

        //获取分页栏
        var pager = mini.get(grid.aimpager);

        if (pager && response.data.SearchCriterion) {
            pager.setTotalCount(response.data.SearchCriterion["RecordCount"]); //设置分页控件的值
        }
        mini.unmask(document.body);
    });

    loader.ensureData();
}

// 初始化Aim查询控件
function initAimQry(grid) {
    //$("[aimqry], [qryopts], [qrygrp]")
    $("[qryopts]").each(function(i) {
        var qryopts = $.getJsonObj($(this).attr("qryopts")) || {};
        var qryevent = qryopts["event"] || "keyup";

        if (qryevent && !$(this).attr(qryevent)) {
            $(this).bind(qryevent, function(event) {
                var tevent = event || window.event;
                if (qryevent != "keyup" || (qryevent == "keyup" && tevent.keyCode == 13)) {
                    //参数
                    var parms = { schtype: "field", schdom: this };
                    var url = window.location.search;
                    if (url.indexOf("?") != -1) {
                        var str = url.substr(1)
                        strs = str.split("&");
                        for (i = 0; i < strs.length; i++) {
                            parms[strs[i].split("=")[0]] = strs[i].split("=")[1];
                        }
                    }
                    grid.reload(parms);
                    //grid.reload({ schtype: "field", schdom: this });
                }
            });
        }
    });
}


mini.regClass(mini.ux.AimDataGrid, "aimgrid");
///------------Aim DataGrid End------------

///------------Aim EditGrid Start----------

///------------Aim EditGrid End----------

///------------Aim Pager Begin---------
mini.ux.AimPager = function(config) {
    mini.ux.AimPager.superclass.constructor.call(this);
    this.initComponents(config);
}

mini.extend(mini.ux.AimPager, mini.Pager, {
    initComponents: function(config) {
        config = config || {};
        config.sizeList = [10, 20, 50, 100, 200, 300];
        config.pageSize = config.pageSize || AimSearchCrit["PageSize"];
        config.totalCount = AimSearchCrit["RecordCount"];
        this.set(config);
    }
});

mini.regClass(mini.ux.AimPager, "aimpager");
///-------------Aim Pager end------------




///-------------Aim AimSelect begin------
mini.ux.AimSelect = function(config) {

    mini.ux.AimSelect.superclass.constructor.call(this);
    this.initComponents(this);
    this.initEvent();
}

mini.extend(mini.ux.AimSelect, mini.TextBoxList, {
    uiCls: 'aim-select',
    initComponents: function(config) {
        config = config || {};
        //拼音查询,提交路径
        config.url = config.url;

        config.valueField = config.valueField;
        config.textField = config.textField;
        //扩展单选多选属性,目前用valuechanged实现
        config.multiSelect = config.multiSelect == true;

        /*
        *弹出窗口部分配置
        */
        //默认显示小人按钮
        config.showSelBtn = !(config.showSelBtn == false);
        //弹出窗口的按钮的样式
        config.selBtnClass = config.selBtnClass;
        //弹出页面路径
        config.popUrl = config.popUrl;
        //弹出窗口标题
        config.popTitle = config.popTitle || "请选择";
        //弹出窗口宽度
        config.popWidth = config.popWidth || 950;
        //弹出窗口高度
        config.popHeight = config.popHeight || 550;


        //选择及返回类型,预留字段
        config.rtnType = config.rtntype || "array";
        config.selType = config.multiSelect == true ? "multi" : "single";

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段,
        config.nameField = config.nameField;
        //保留字段
        config.fieldParam = config.fieldParam;

        if (config.showSelBtn) {
            var boxList = this;
            var ul = this.ulEl;
            var jUserBtn = $('<div></div>').addClass(config.selBtnClass);
            jUserBtn.bind("click", function() {
                var url = config.popUrl;
                url = $.combineQueryUrl(url, { rtntype: config.rtnType, seltype: config.selType });
                mini.open({
                    url: url,
                    title: config.popTitle,
                    width: config.popWidth,
                    height: config.popHeight,
                    ondestroy: function(action) {
                        if (action == "ok") {
                            var iframe = this.getIFrameEl();
                            //获取选中、编辑的结果

                            //弹出选择页面必须包含getContentData()供调用
                            var data = iframe.contentWindow.getUsrData();
                            data = mini.clone(data); //必须。克隆数据。
                            if (config.selType == "multi") {
                                var vals = boxList.getValue();
                                var texts = boxList.getText(); //
                                $.each(data, function() {
                                    if (vals.indexOf(this[boxList.valueField]) < 0) {
                                        //筛选已选择的记录
                                        if (vals) {
                                            vals += ",";
                                            texts += ",";
                                        }
                                        vals += this[boxList.valueField];
                                        texts += this[boxList.textField];
                                    }
                                });
                                boxList.setValue(vals);
                                boxList.setText(texts);
                            } else { //单选
                                if (data && data.length > 0) {
                                    boxList.setValue(data[0][boxList.valueField]);
                                    boxList.setText(data[0][boxList.textField]);
                                }
                            }
                        } else if (action == "clear") {
                            boxList.setValue("");
                            boxList.setText("");
                        }
                    }
                });
            });

            $(ul).css("marginRight", "16px").parent().append(jUserBtn);
        }
        this.set(config);

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var uselect = e.sender;
            var vals = uselect.getValue();
            var texts = uselect.getText(); //
            if (!this.multiSelect && vals.indexOf(",") > 0) {
                //控件单选且有多个值
                vals = vals.substr(vals.lastIndexOf(",") + 1);
                texts = texts.substr(texts.lastIndexOf(",") + 1);
                uselect.setValue(vals);
                uselect.setText(texts);
            }
            this.idField && typeof this.idField == "string" && $("#" + this.idField).val(vals);
            this.nameField && typeof this.nameField == "string" && $("#" + this.nameField).val(texts);


            if (this.fieldParam && typeof this.nameField == "string") {
                //
            }
            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = uselect.data;
                this.afterSelect(rtns);
            }

        });
    }
});
mini.regClass(mini.ux.AimSelect, "aimselect");
///-------------Aim AimSelect end------
///-------------Aim UserSelect begin-----

//mini.ux.AimUserSelect = function (config) {
//    mini.ux.AimUserSelect.superclass.constructor.call(this);
//    this.initComponents(config);
//}

//mini.extend(mini.ux.AimUserSelect, mini.ux.AimSelect, {
//    uiCls: 'aim-user',
//    initComponents: function (config) {
//        var config = config || {};
//        拼音查询,提交路径
//        config.url = config.url || "/commonpages/miniui/data/userdataora.aspx";

//        config.valueField = config.valueField || "UserID";
//        config.textField = config.textField || "Name";

//        config.selBtnClass = config.selBtnClass || "mini-aimuser-btns aimuser-btn-expand";
//        弹出页面路径
//        config.popUrl = config.popUrl || "/CommonPages/MiniUI/Select/UsrSelect/UsrSelect.aspx";
//        config.popTitle = config.popTitle || "人员选择";
//        this.set(config);
//    }
//});
//mini.regClass(mini.ux.AimUserSelect, "aimuserselect");




mini.ux.AimUserSelect = function(config) {
    mini.ux.AimUserSelect.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.AimUserSelect, mini.TextBoxList, {
    uiCls: 'aim-user',
    initComponents: function(config) {
        config = config || {};
        //拼音查询,提交路径

        //弹出页面路径
        config.popUrl = config.popUrl || "/CommonPages/MiniUI/Select/UsrSelect/UsrSelect.aspx";
        config.url = config.url || "/commonpages/miniui/data/userdataora.aspx";
        //查询参数
        if (config.popParam) {
            var pramsjson = eval('({' + config.popParam + '})');
            config.url = $.combineQueryUrl(config.url, pramsjson);
            config.popUrl = $.combineQueryUrl(config.popUrl, pramsjson);
        }

        config.valueField = config.valueField || "UserID";
        config.textField = config.textField || "Name";
        //扩展单选多选属性,目前用valuechanged实现
        config.multiSelect = config.multiSelect == true;

        //默认显示小人按钮
        config.showSelBtn = !(config.showSelBtn == false);
        config.selBtnClass = config.selBtnClass || "mini-aimuser-btns aimuser-btn-expand";

        //选择及返回类型,预留字段
        config.rtnType = config.rtntype || "array";
        config.selType = config.multiSelect == true ? "multi" : "single";

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;
        //保留字段
        config.fieldParam = config.fieldParam;

        var idField = config.idField;
        var nameField = config.nameField;

        if (config.showSelBtn) {
            var boxList = this;
            var ul = this.ulEl;
            var jUserBtn = $('<div></div>').addClass(config.selBtnClass);
            jUserBtn.bind("click", function() {

                if (!boxList.enabled)
                    return;

                var url = config.popUrl;
                url = $.combineQueryUrl(url, { rtntype: config.rtnType, seltype: config.selType });
                mini.open({
                    url: url,
                    title: "人员选择",
                    width: 950,
                    height: 550,
                    ondestroy: function(action) {
                        if (action == "ok") {
                            var iframe = this.getIFrameEl();
                            //获取选中、编辑的结果
                            var data = iframe.contentWindow.getUsrData();
                            data = mini.clone(data); //必须。克隆数据。
                            if (config.selType == "multi") {
                                var vals = boxList.getValue();
                                var texts = boxList.getText();
                                $.each(data, function() {
                                    if (vals.indexOf(this.UserID) < 0) { //筛选已选择人员
                                        if (vals) {
                                            vals += ",";
                                            texts += ",";
                                        }
                                        vals += this.UserID;
                                        texts += this.Name;
                                    }
                                });
                                boxList.setValue(vals);
                                boxList.setText(texts);

                                idField && $("#" + idField).val(vals);
                                nameField && $("#" + nameField).val(texts);
                            } else { //单选
                                if (data && data.length > 0) {
                                    boxList.setValue(data[0].UserID);
                                    boxList.setText(data[0].Name);

                                    idField && $("#" + idField).val(data[0].UserID);
                                    nameField && $("#" + nameField).val(data[0].Name);
                                }
                            }
                        } else if (action == "clear") {
                            boxList.setValue("");
                            boxList.setText("");
                        }

                        if (boxList.afterSelect && typeof boxList.afterSelect == "function") {
                            //扩展选中后的回调函数
                            var rtns = new Object();
                            rtns.value = boxList.getValue();
                            rtns.text = boxList.getText();
                            rtns.data = boxList.data;
                            boxList.afterSelect(rtns);
                        }
                    }
                });
            });

            $(ul).css("marginRight", "16px").parent().append(jUserBtn);
        }
        this.set(config);
        if (config.applyTo) {
            this.render(config.applyTo);
        }

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var uselect = e.sender;
            var vals = uselect.getValue();
            var texts = uselect.getText(); //
            if (!this.multiSelect && vals.indexOf(",") > 0) {
                //控件单选且有多个值
                vals = vals.substr(vals.lastIndexOf(",") + 1);
                texts = texts.substr(texts.lastIndexOf(",") + 1);
                uselect.setValue(vals);
                uselect.setText(texts);
            }
            this.idField && typeof this.idField == "string" && $("#" + this.idField).val(vals);
            this.nameField && typeof this.nameField == "string" && $("#" + this.nameField).val(texts);
            if (this.fieldParam && typeof this.fieldParam == "string") {
                //
            }
            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = uselect.data;
                this.afterSelect(rtns);
            }

        });
    }
});
mini.regClass(mini.ux.AimUserSelect, "aimuserselect");

///-------------Aim UserSelect end-------

///-------------Aim DeptSelect Start 部门选择控件-----
mini.ux.AimDeptSelect = function(config) {
    mini.ux.AimDeptSelect.superclass.constructor.call(this);
    this.initComponents(config);
    //this.initEvent();
}


mini.extend(mini.ux.AimDeptSelect, mini.ButtonEdit, {
    uiCls: 'aim-dept',
    initComponents: function(config) {
        var intWidth = '500';
        config = config || {};
        config.url = config.url || '/commonpages/miniui/select/deptselect/deptselect.aspx';
        config.title = config.title || '部门选择',
        config.valueField = "UserID";
        config.textField = "Name";
        config.multiSelect = config.multiSelect == true; //扩展单选多选属性,默认为单选
        config.popParam = config.popParam;

        if (config.popParam) {
            var pramsjson = eval('({' + config.popParam + '})');
            if (pramsjson.width) {
                intWidth = pramsjson.width;
            }
        }

        config.allowInput = config.allowInput; //是否可以输入

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;


        var selType = config.multiSelect == true ? "multi" : "single";
        if (config.url.indexOf("?") > -1) {
            config.url += "&seltype=" + selType;
        }
        else {
            config.url += "?seltype=" + selType;
        }
        this.set(config);
        if (config.applyTo) {
            this.render(config.applyTo);
        }


        var me = this;
        this.on('buttonclick', function(e) {
            mini.open({
                url: me.url,
                title: me.title,
                width: intWidth,
                height: 550,
                ondestroy: function(action) {
                    var vals = me.getValue();
                    var texts = me.getText();

                    var iframe = this.getIFrameEl();
                    //获取选中、编辑的结果
                    var data = iframe.contentWindow.getUsrData();
                    data = mini.clone(data);    //必须。克隆数据。

                    if (action == "ok") {
                        if (selType == "multi") {
                            $.each(data, function() {
                                if (vals.indexOf(this.GroupID) < 0) {//筛选已选择人员
                                    if (vals) {
                                        vals += ",";
                                        texts += ",";
                                    }
                                    vals += this.GroupID;
                                    texts += this.Name;
                                }
                            });

                            me.setText(texts);
                        }
                        else {//单选
                            if (data && data.length > 0) {
                                vals = data[0].GroupID;
                                texts = data[0].Name;
                            }
                        }
                    } else if (action == "clear") {
                        vals = "";
                        texts = "";
                    }
                    else { return; }
                    me.setValue(vals);
                    me.setText(texts);
                    me.idField && typeof me.idField == "string" && $("#" + me.idField).val(vals);
                    me.nameField && typeof me.nameField == "string" && $("#" + me.nameField).val(texts);

                    if (me.afterSelect && typeof me.afterSelect == "function") {
                        var rtns = new Object();
                        rtns.value = vals;
                        rtns.text = texts;
                        rtns.data = data;
                        me.afterSelect(rtns);
                    }
                }
            });
        });

    }
});

mini.regClass(mini.ux.AimDeptSelect, "aimdeptselect");
///-------------Aim DeptSelect End-------

///-------------Aim CompanyQuickSel Start----------------
mini.ux.CompanyQuickSelect = function(config) {
    mini.ux.CompanyQuickSelect.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.CompanyQuickSelect, mini.TextBoxList, {
    uiCls: 'aim-company',

    initComponents: function(config) {
        //debugger

        config = config || {};
        config.selSql = config.selSql || 'select GroupId as "Id", Code||Name as "Name",Code from SysGroup where corpCode is not null';
        config.selColName = config.selColName || "Code";
        config.selData = config.selData || "sysgroup";

        config.url = config.url || "/commonpages/miniui/data/CustomerData.aspx";
        config.valueField = config.valueField || "Id";
        config.textField = config.textField || "Name";
        config.multiSelect = config.multiSelect == true; //扩展单选多选属性,目前用valuechanged实现
        config.showSelBtn = !(config.showSelBtn == false); //默认显示小人按钮
        config.rtnType = config.rtntype || "array";
        config.selType = config.multiSelect == true ? "multi" : "single";
        config.url = $.combineQueryUrl(config.url, { selsql: config.selSql, selColName: config.selColName, selData: config.selData });
        config.nameField = config.nameField;
        config.fieldParam = config.fieldParam;

        if (config.showSelBtn) {
            var boxList = this;
            var ul = this.ulEl;
            var jUserBtn = $('<div class="mini-aimuser-btns aimuser-btn-expand"></div>');
            jUserBtn.bind("click", function() {
                var url = "/CommonPages/MiniUI/Select/UsrSelect/UsrSelect.aspx";
                url += "?rtntype=" + config.rtnType;
                url += "&seltype=" + config.selType;
                mini.open({
                    url: url,
                    title: "公司选择",
                    width: 950,
                    height: 550,
                    ondestroy: function(action) {
                        if (action == "ok") {
                            var iframe = this.getIFrameEl();
                            //获取选中、编辑的结果
                            var data = iframe.contentWindow.getUsrData();
                            data = mini.clone(data); //必须。克隆数据。
                            if (config.selType == "multi") {
                                var vals = boxList.getValue();
                                var texts = boxList.getText(); //
                                $.each(data, function() {
                                    if (vals.indexOf(this.UserID) < 0) { //筛选已选择人员
                                        if (vals) {
                                            vals += ",";
                                            texts += ",";
                                        }
                                        vals += this.UserID;
                                        texts += this.Name;
                                    }
                                });
                                boxList.setValue(vals);
                                boxList.setText(texts);
                            } else { //单选
                                if (data && data.length > 0) {
                                    boxList.setValue(data[0].UserID);
                                    boxList.setText(data[0].Name);
                                }
                            }
                        } else if (action == "clear") {
                            boxList.setValue("");
                            boxList.setText("");
                        }
                    }
                });
            });

            $(ul).css("marginRight", "16px").parent().append(jUserBtn);
        }
        this.set(config);

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var cselect = e.sender;
            var vals = cselect.getValue();
            var texts = cselect.getText(); //
            if (!this.multiSelect && vals.indexOf(",") > 0) {
                //控件单选且有多个值
                vals = vals.substr(vals.lastIndexOf(",") + 1);
                texts = texts.substr(texts.lastIndexOf(",") + 1);
                cselect.setValue(vals);
                cselect.setText(texts);
            }
            if (this.nameField && typeof this.nameField == "string") {
                $("#" + this.nameField).val(texts);
            }
            if (this.fieldParam && typeof this.nameField == "string") {
                //
            }
            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = cselect.data;
                this.afterSelect(rtns);
            }
        });
    }
});
mini.regClass(mini.ux.CompanyQuickSelect, "aimcompanyquickselect");

///-------------Aim CompanyQuickSel End----------------

///-------------Aim GroupSelect Start 组织机构选择控件-----


///-------------Aim GroupSelect End-------


// 对recs进行批量处理
function ExtBatchOperate(action, recs, params, url, onOperated) {
    if (!url) url = null;

    params = params || {};

    if (!params["IdList"]) {
        var idList = [];

        if (recs != null) {
            jQuery.each(recs, function() {
                idList.push(this.Id || this.ID);
            });
        }

        params["IdList"] = idList;
    }

    jQuery.ajaxExec(action, params, onOperated);
}

/*
*订阅查询模式标志:mode=subs
*
*/
function subscribe() {
    var qurUrl = $.getQueryUrl();
    //var params = $.getAllQueryStrings();
    var title = $("#header h1:first").text(); //可能的标题
    var qobjs = [];
    //获取列表页的查询字段和值
    $("[aimqry], [qryopts], [qrygrp]").each(function() {
        var obj = { id: $(this).attr("id"), label: $(this).prev().text(), value: $(this).val() };
        qobjs.push(obj);
    });
    var subsUrl = SUBSCRIBE_URL;
    var params = new Object({
        op: 'c',
        qururl: qurUrl,
        subs: $.getJsonString(qobjs)
    });
    if (title) {
        params["title"] = title;
    }
    subsUrl = $.combineQueryUrl(subsUrl, params);
    mini.open({
        url: subsUrl,
        title: "查询订阅",
        width: 600,
        height: 550
    });
}


///-------------Aim popup begin-----
mini.ux.AimComSelect = function(config) {
    mini.ux.AimComSelect.superclass.constructor.call(this);
    this.initComponents(config);
}

mini.extend(mini.ux.AimComSelect, mini.ButtonEdit, {
    uiCls: 'aim-dept',
    initComponents: function(config) {
        config = config || {};
        config.url = config.url;
        config.title = config.title || '选择',
        config.valueField = config.valueField;
        config.textField = config.textField;
        config.multiSelect = config.multiSelect == true; //扩展单选多选属性,默认为单选
        config.popParam = config.popParam;

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;

        var selType = config.multiSelect == true ? "multi" : "single";
        if (config.url.indexOf("?") > -1) {
            config.url += "&seltype=" + selType;
        }
        else {
            config.url += "?seltype=" + selType;
        }
        this.set(config);
        if (config.applyTo) {
            this.render(config.applyTo);
        }

        var me = this;
        this.on('buttonclick', function(e) {
            mini.open({
                url: me.url,
                title: me.title,
                width: 950,
                height: 550,
                ondestroy: function(action) {
                    var vals = me.getValue();
                    var texts = me.getText();

                    var iframe = this.getIFrameEl();
                    //获取选中、编辑的结果
                    var data = iframe.contentWindow.getUsrData();
                    data = mini.clone(data);    //必须。克隆数据。

                    if (action == "ok") {
                        if (selType == "multi") {
                            $.each(data, function() {
                                if (vals.indexOf(this.ID) < 0) {//筛选已选择人员
                                    if (vals) {
                                        vals += ",";
                                        texts += ",";
                                    }
                                    vals += this[me.valueField];
                                    texts += this[me.valueField];
                                }
                            });

                            me.setText(texts);
                        }
                        else {//单选
                            if (data && data.length > 0) {
                                vals = data[0][me.valueField];
                                texts = data[0][me.textField];
                            }
                        }
                    } else if (action == "clear") {
                        vals = "";
                        texts = "";
                    }
                    me.setValue(vals);
                    me.setText(texts);
                    me.idField && typeof me.idField == "string" && $("#" + me.idField).val(vals);
                    me.nameField && typeof me.nameField == "string" && $("#" + me.nameField).val(texts);

                    if (me.afterSelect && typeof me.afterSelect == "function") {
                        var rtns = new Object();
                        rtns.value = vals;
                        rtns.text = texts;
                        rtns.data = data;
                        me.afterSelect(rtns);
                    }
                }
            });
        });
    }
});

mini.regClass(mini.ux.AimComSelect, "aimpopup");

///-------------Aim popup end-----


///-------------Aim CostomSelect begin-------

mini.ux.AimCustomSelect = function(config) {
    mini.ux.AimCustomSelect.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.AimCustomSelect, mini.TextBoxList, {
    uiCls: 'aim-popup',
    initComponents: function(config) {
        config = config || {};

        config.selSql = config.selSql || '';
        config.selColName = config.selColName || "";
        config.selTable = config.selTable || "";
        config.disColName = config.disColName || "";
        config.popWidth = config.popWidth || 950;

        //拼音查询,提交路径
        config.url = "/commonpages/miniui/data/CustomData.aspx?selSql=" + config.selSql + "&selColName=" + config.selColName + "&selTable=" + config.selTable + "&disColName=" + config.disColName;
        config.valueField = config.valueField || "";
        config.textField = config.textField || "";
        //扩展单选多选属性,目前用valuechanged实现
        config.multiSelect = config.multiSelect == true;
        config.emptyTitle = config.emptyTitle;

        //默认显示小人按钮
        config.showSelBtn = !(config.showSelBtn == false || config.showSelBtn == "false");
        config.selBtnClass = config.selBtnClass || "mini-aimuser-btns aimcustomer-btn-expand";
        //弹出页面路径
        config.popUrl = config.popUrl || "/CommonPages/MiniUI/Select/FrmCustomSelect.aspx";

        //选择及返回类型,预留字段
        config.rtnType = config.rtntype || "array";
        config.selType = config.multiSelect == true ? "multi" : "single";

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;
        //保留字段
        config.fieldParam = config.fieldParam;

        config.required = config.required;

        var idField = config.idField;
        var nameField = config.nameField;

        if (config.showSelBtn) {
            var boxList = this;
            var ul = this.ulEl;
            var jUserBtn = $('<div></div>').addClass(config.selBtnClass);
            jUserBtn.bind("click", function() {

                if (!boxList.enabled)
                    return;

                var url = config.popUrl;
                url = $.combineQueryUrl(url, { rtntype: config.rtnType, seltype: config.selType });
                mini.open({
                    url: url,
                    title: config.emptyTitle || "",
                    width: config.popWidth,
                    height: 550,
                    ondestroy: function(action) {
                        if (action == "ok") {
                            var iframe = this.getIFrameEl();
                            //获取选中、编辑的结果
                            var data = iframe.contentWindow.getUsrData();
                            data = mini.clone(data); //必须。克隆数据。
                            if (config.selType == "multi") {
                                var vals = boxList.getValue();
                                var texts = boxList.getText(); //
                                $.each(data, function() {
                                    if (vals.indexOf(this.UserID) < 0) { //筛选已选择人员
                                        if (vals) {
                                            vals += ",";
                                            texts += ",";
                                        }
                                        vals += this[config.valueField];
                                        texts += this[config.textField];
                                    }
                                });
                                boxList.setValue(vals);
                                boxList.setText(texts);

                                idField && $("#" + idField).val(vals);
                                nameField && $("#" + nameField).val(texts);
                            } else { //单选
                                if (data && data.length > 0) {
                                    boxList.setValue(data[0][config.valueField]);
                                    boxList.setText(data[0][config.textField]);

                                    idField && $("#" + idField).val(data[0][config.valueField]);
                                    nameField && $("#" + nameField).val(data[0][config.textField]);
                                }
                            }
                        } else if (action == "clear") {
                            boxList.setValue("");
                            boxList.setText("");

                            idField && $("#" + idField).val("");
                            nameField && $("#" + nameField).val("");
                        }

                        if (config.afterSelect && typeof config.afterSelect == "function") {
                            //扩展选中后的回调函数
                            var rtns = new Object();
                            rtns.action = action;
                            rtns.value = boxList.getValue();
                            rtns.text = boxList.getText();
                            rtns.data = boxList.data;
                            config.afterSelect(rtns);
                        }
                    }
                });
            });

            $(ul).css("marginRight", "16px").parent().append(jUserBtn);
        }
        this.set(config);
        if (config.applyTo) {
            this.render(config.applyTo);
        }

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var uselect = e.sender;
            var vals = uselect.getValue();
            var texts = uselect.getText(); //
            if (!this.multiSelect && vals.indexOf(",") > 0) {
                //控件单选且有多个值
                vals = vals.substr(vals.lastIndexOf(",") + 1);
                texts = texts.substr(texts.lastIndexOf(",") + 1);
                uselect.setValue(vals);
                uselect.setText(texts);
            }
            this.idField && typeof this.idField == "string" && $("#" + this.idField).val(vals);
            this.nameField && typeof this.nameField == "string" && $("#" + this.nameField).val(texts);
            if (this.fieldParam && typeof this.fieldParam == "string") {
                //
            }
            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = uselect.data;
                this.afterSelect(rtns);
            }

        });
    }
});
mini.regClass(mini.ux.AimCustomSelect, "aimcustomselect");

///-------------Aim CustomSelect end-------



///-------------Aim MultiCombox begin-------

mini.ux.AimMultiCombox = function(config) {
    mini.ux.AimMultiCombox.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.AimMultiCombox, mini.ComboBox, {
    initComponents: function(config) {
        config = config || {};

        config.valueField = config.valueField || "id";
        config.textField = config.textField || "text";
        config.multiSelect = config.multiSelect;
        var datatmp;
        if (config.data) {
            if (AimState[config.data]) {
                datatmp = AimState[config.data];
            } else {
                datatmp = eval(config.data);
            }
        }
        config.data = [];
        for (var tmp in datatmp) {
            config.data.push({ "id": tmp, "text": datatmp[tmp] });
        }

        config.data = config.data;

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;

        this.set(config);
        if (config.applyTo) {
            this.render(config.applyTo);
        }

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var uselect = e.sender;
            var vals = uselect.getValue();
            var texts = uselect.getText();

            this.idField && typeof this.idField == "string" && $("#" + this.idField).val(vals);
            this.nameField && typeof this.nameField == "string" && $("#" + this.nameField).val(texts);
            if (this.fieldParam && typeof this.fieldParam == "string") {
                //
            }
            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = uselect.data;
                this.afterSelect(rtns);
            }

        });
    }
});
mini.regClass(mini.ux.AimMultiCombox, "aimmulticombox");

///-------------Aim CustomSelect end-------



///-------------Aim TreeCombox begin-------

mini.ux.AimTreeCombox = function(config) {
    mini.ux.AimTreeCombox.superclass.constructor.call(this);
    this.initComponents(config);
    this.initEvent();
}

mini.extend(mini.ux.AimTreeCombox, mini.TreeSelect, {
    initComponents: function(config) {
        config = config || {};
        config.valueField = config.valueField || "id";
        config.textField = config.textField || "text";
        config.parentField = config.parentField || "pid";
        config.multiSelect = config.multiSelect;
        config.checkRecursive = config.checkRecursive;
        config.showFolderCheckBox = config.showFolderCheckBox;
        config.expandOnLoad = config.expandOnLoad;
        config.showClose = config.showClose;
        config.autoCheckParent = config.autoCheckParent;

        //关联Id字段
        config.idField = config.idField;
        //关联名称字段
        config.nameField = config.nameField;
        var datatmp = config.data;
        this.set(config);

        //加载数据
        this.loadList(AimState[datatmp], "id", "pid");

        if (config.applyTo) {
            this.render(config.applyTo);
        }

    },
    initEvent: function() {
        this.on("valuechanged", function(e) {
            var uselect = e.sender;
            var vals = uselect.getValue();
            var texts = uselect.getText();

            this.idField && typeof this.idField == "string" && $("#" + this.idField).val(vals);
            this.nameField && typeof this.nameField == "string" && $("#" + this.nameField).val(texts);

            if (this.afterSelect && typeof this.afterSelect == "function") {
                //扩展选中后的回调函数
                var rtns = new Object();
                rtns.value = vals;
                rtns.text = texts;
                rtns.data = uselect.data;
                this.afterSelect(rtns);
            }

        });
    }
});
mini.regClass(mini.ux.AimTreeCombox, "aimtreecombox");

///-------------Aim TreeCombox end-------
