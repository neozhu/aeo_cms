<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="MontorConfig.aspx.cs" Inherits="Aim.OnControl.Web.MontorSet.MontorConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background: url(/theme/default/images/public/paperbg.jpg);
        }
        .renderEdit
        {
        }
        .renderEdit label
        {
            margin-right: 12px;
            color: Blue;
            text-decoration: "underline";
        }
        .renderEdit label:hover
        {
            margin-right: 12px;
            font-weight: bolder;
            color: Blue;
            text-decoration: "underline";
        }
        .x-panel-header-text
        {
            font: 14px "Microsoft YaHei" , Tahoma, Geneva, sans-serif;
            text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.5);
            float: left;
            color: #fff; /*font-size: 14px;*/
            vertical-align: middle;
        }
        .x-panel-header
        {
            background: url(/images/titlebg.png) norepeat left top;
            height: 32px;
            border: none;
        }
        .x-grid3-hd-inner
        {
            font-weight: bold;
            font-family: "Microsoft YaHei" , Tahoma, Geneva, sans-serif;
            font-size: 12px;
        }
        
        .montor
        {
            padding-left: 20px;
        }
    </style>
    <script src="/js/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "SYSTBLMMEdit.aspx";

        var store, fieldStore, selectID = "", selectIndex, tblname = tblencode = "";
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onPgLoad() {
            setPgUI();
        }
        //        window.onerror = function () {
        //            return true;
        //        }
        function setPgUI() {

            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'ID',
                data: myData,
                fields: [
			{ name: 'ID' },
			{ name: 'TBLENCODE' },
			{ name: 'TBLNAME' },
			{ name: 'ORGANIZATIONIDS' },
			{ name: 'ORGANIZATIONNAMES' },
			{ name: 'TBLCLNS' },
			{ name: 'CREATEID' },
			{ name: 'CREATENAME' },
			{ name: 'CREATETIME' },
			{ name: 'ISMONTOR' },
			{ name: 'PERSONIDS' },
			{ name: 'PERSONNAMES' },
			{ name: 'WEEK' },
			{ name: 'DATATIMEPOINT' },
			{ name: 'STARTTIME' },
			{ name: 'ENDTIME' }
			]
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });

            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 2,
                items: [
                { fieldLabel: '表名', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'TBLENCODE' }"} },
                { fieldLabel: '中文名', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'TBLNAME' }"} }
                 ]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '刷新数据源',
                    iconCls: 'aim-icon-search',
                    handler: function () {
                        Ext.getBody().mask("数据重新加载中，请稍等");
                        $.ajaxExec("reflash", {}, function (rtn) {
                            if (rtn.data.statue == "1") {
                                AimDlg.show("数据刷新成功!");
                                store.reload();
                            }
                            Ext.getBody().unmask();
                        });
                    }
                }, '-', {
                    text: '快速配置',
                    iconCls: 'aim-icon-edit',
                    handler: function () {
                        if (grid.getSelectionModel().getSelections().length <= 0) {
                            AimDlg.show("请选择要设置的记录!");
                            return;
                        }
                        selectAll(null, true); //全选
                        saveConfig();
                    }
                },
                {
                    text: '保存配置',
                    iconCls: 'aim-icon-save',
                    handler: function () {
                        saveConfig();
                    }
                }]
            });

            //-------------保存配置具体方法--------------
            function saveConfig() {
                var recs = grid.getSelectionModel().getSelections();
                if (recs.length <= 0) {
                    AimDlg.show("请选择要操作的记录!");
                    return;
                }

                //                var gogate = false;
                //                $.each(fieldStore.getRange(), function () {
                //                    if (this.get("ISCHECKED") == "Y") {
                //                        gogate = true;
                //                        return false;
                //                    }
                //                });

                //  if (!gogate) {
                //      AimDlg.show("请配置要监视的字段!");
                //      return;
                //  }

                if (recs[0].get("ISMONTOR") != "Y") {
                    AimDlg.show("请配置要监视的表或表的字段!");
                    return;
                }

                var week = [];
                $(".week").each(function () {
                    if ($(this).attr("checked")) {
                        week.push($(this).val())
                    }
                })

                var arr = [];
                $.each(fieldStore.getRange(), function () {
                    arr.push(this.data)
                })
                var objData = {
                    ID: selectID,
                    ORGANIZATIONNAMES: $("#ORGANIZATIONNAMES").val(),
                    ORGANIZATIONIDS: $("#ORGANIZATIONIDS").val(),
                    PERSONIDS: $("#PERSONIDS").val(),
                    PERSONNAMES: $("#PERSONNAMES").val(),
                    TBLCLNS: $.getJsonString(arr),
                    DATATIMEPOINT: $("#DATATIMEPOINT").val(), //日期
                    TIMEPOINT: $("#TIMEPOINT").val(),      //时间点
                    STARTTIME: $("#STARTTIME").val(),     //STARTTIME
                    ENDTIME: $("#ENDTIME").val(),         //ENDTIME
                    ISMONTOR: "Y",
                    TBLENCODE: tblencode,
                    TBLNAME: tblname,
                    WEEK: week.join()
                };
                var objJson = $.getJsonString(objData);
                $.ajaxExec("update", { objJson: objJson }, function (rtn) {
                    store.reload();
                    AimDlg.show("保存成功!");
                });
            };
            //-----------------------------------------------------

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '数据库表监控配置',
                store: store,
                width: 520,
                region: 'west',
                autoExpandColumn: 'TBLNAME',
                columns: [
                    { id: 'ID', dataIndex: 'ID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'TBLENCODE', dataIndex: 'TBLENCODE', header: '表名', width: 180, sortable: true },
                    { id: 'TBLNAME', dataIndex: 'TBLNAME', header: '中文名', width: 100 },
                    { id: 'ISMONTOR', dataIndex: 'ISMONTOR', header: '是否监视', width: 80, renderer: RowRender }
                    ]
                 ,
                bbar: pgBar,
                tbar: titPanel
            });
            grid.on("rowclick", function (grid, rowIndex, e) {
                rowSelected(rowIndex);
            })

            //--------------------配置区域---------------------------

            // 表格数据源
            fieldStore = new Ext.ux.data.AimJsonStore({
                dsname: 'SYSTBLCLNSMMList',
                idProperty: 'ID',
                data: {
                    total: AimSearchCrit["RecordCount"],
                    records: AimState["SYSTBLCLNSMMList"] || []
                },
                fields: [
			{ name: 'ID' },
			{ name: 'CLNCODE' },
			{ name: 'CLNNAME' },
			{ name: 'CLNDATATYPE' },
			{ name: 'CREATETIME' },
            { name: 'ISCHECKED' }
			]
            });

            // 表格面板
            filedGrid = new Ext.ux.grid.AimGridPanel({
                store: fieldStore,
                region: 'center',
                autoExpandColumn: 'CLNNAME',
                columns: [
                    { id: 'ID', dataIndex: 'ID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'CLNCODE', dataIndex: 'CLNCODE', header: '字段', width: 150 },
					{ id: 'CLNNAME', dataIndex: 'CLNNAME', header: '中文名', width: 100 },
                    { id: 'CLNDATATYPE', dataIndex: 'CLNDATATYPE', header: '数据类型', width: 120 },
                    { id: 'ISCHECKED', dataIndex: 'ISCHECKED', header: '<input type="checkbox" onclick="selectAll(this)" >&nbsp;监视', width: 120, renderer: filedRender }
                    ]
            });


            setpanel = new Ext.Panel({
                title: ' ',
                region: 'center',
                border: false,
                height: 200,
                autoScroll: true,
                layout: 'border',
                items: [filedGrid, {
                    region: "north",
                    layout: 'column',
                    frame: true,
                    height: 160,
                    items: [{
                        xtype: "form",
                        labelWidth: 60,
                        columnWidth: .8,
                        defaultType: "textfield",
                        items: [{
                            id: 'ORGANIZATIONNAMES',
                            fieldLabel: "组织机构",
                            name: "ORGANIZATIONNAMES",
                            seltype: "multi&para=ORGANIZATIONIDS:ORGANIZATIONNAMES",
                            xtype: 'aimdeptselector',
                            width: 370,
                            listeners: { 'change': function (obj) {
                            }
                            },
                            popAfter: function (rtn) {
                                if (!!!rtn || !rtn.data) {
                                    return;
                                }
                                var data = rtn.data;
                                var names = ids = "";
                                if (rtn.data && rtn.data.length > 0) {
                                    $.each(data, function (i) {
                                        if (i > 0) {
                                            names += ",";
                                            ids += ",";
                                        }
                                        names += this.Name;
                                        ids += this.GroupID;
                                    })
                                    Ext.getCmp("ORGANIZATIONNAMES").setValue(names);
                                    $("#ORGANIZATIONIDS").val(ids)
                                }
                            },
                            allowBlank: false
                        },
                        //   {
                        //       fieldLabel: "部门",
                        //       width: 370,
                        //       xtype: 'aimdeptselector',
                        //       name: "nickname"
                        //   },
                         {
                         id: "PERSONNAMES",
                         fieldLabel: "人员",
                         xtype: "aimuserselector",
                         seltype: "multi&para=PERSONIDS:PERSONNAMES",
                         popStyle: "dialogWidth:780px;dialogHeight:450px",
                         //popParam: "PERSONIDS:UserID;PERSONNAMES:Name",
                         width: 370,
                         name: "PERSONNAMES",
                         listeners: { 'blur': function (obj) {
                             alert();
                         }
                         },
                         popAfter: function (rtn) {
                             if (!!!rtn || !rtn.data) return;
                             var data = rtn.data;
                             var names = ids = "";
                             if (rtn.data && rtn.data.length > 0) {
                                 $.each(data, function (i) {
                                     if (i > 0) {
                                         names += ",";
                                         ids += ",";
                                     }
                                     names += this.Name;
                                     ids += this.UserID;
                                 })
                                 Ext.getCmp("PERSONNAMES").setValue(names);
                                 $("#PERSONIDS").val(ids)
                             }
                         }
                     }, {
                         fieldLabel: "日期",
                         xtype: 'panel',
                         width: 370,
                         html: "<input class='Wdate' id='DATATIMEPOINT' name='DATATIMEPOINT' onfocus = 'WdatePicker({minDate:new Date()})' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;时间点:&nbsp;&nbsp;&nbsp;&nbsp;"
                                   + "<input class='Wdate' id='TIMEPOINT' name='TIMEPOINT' onfocus='WdatePicker({dateFmt:\"H:mm:ss\"})' style=\"width:128px;\" />"
                     }, {
                         fieldLabel: "时间段",
                         xtype: 'panel',
                         width: 370,
                         html: "<input id='STARTTIME' name='STARTTIME' class='Wdate' onfocus = 'WdatePicker({minDate:new Date()})' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;至&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                    + "<input class ='Wdate' name='ENDTIME' id='ENDTIME' onfocus = 'var date=$(\"#STARTTIME\").val()?$(\"#STARTTIME\").val():new Date();WdatePicker({minDate:date})'/>"
                     }, {
                         fieldLabel: "星期",
                         xtype: 'panel',
                         width: 370,
                         html: "<input type='checkbox' class='week' value='1' />&nbsp;周一&nbsp;<input  type='checkbox' class='week' value='2' />&nbsp;周二&nbsp;<input  type='checkbox' class='week' value='3' />&nbsp;周三&nbsp;<input  type='checkbox' class='week' value='4' />&nbsp;周四&nbsp;"
                                + "<input type='checkbox' class='week' value='5' />&nbsp;周五&nbsp;<input type='checkbox' class='week' value='6' />&nbsp;周六&nbsp;<input type='checkbox' class='week' value='7' />&nbsp;周日&nbsp;"
                     }]
                    }, {
                        columnWidth: .1,
                        layout: 'form',
                        margin: '0 0 -20 0',
                        items: [
                        //  {
                        //      boxLabel: '监控',
                        //      xtype: 'checkbox',
                        //      name: "birthday"
                        //  }
                        ]
                    }
                    ]
                }]
            });


            //------------------------------------------------------
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [grid, setpanel]
            });

            window.setTimeout(function () {
                var ele = document.getElementById("frameContent");
                if (ele) {
                    if (store.getRange().length > 0) {
                        rec = store.getAt(0);
                        grid.getSelectionModel().selectRow(0);
                        Ext.getCmp("clnGrid").setTitle("【" + rec.get("TBLCODE") + "】字段详细");
                        frameContent.location.href = "SYSTBLCLNSMMListEdit.aspx?reftblkey=" + rec.get("ID") + "&title=" + escape(rec.get("TBLCODE") || "");
                    }
                }
            }, 100);
        }

        //组件值初始化
        function initCtrl(record) {
            if ($.isEmptyObject(record)) return;

            $("#ORGANIZATIONNAMES").val(record.get("ORGANIZATIONNAMES") || "");
            $("#ORGANIZATIONIDS").val(record.get("ORGANIZATIONIDS") || "");
            $("#PERSONNAMES").val(record.get("PERSONNAMES") || "");
            $("#PERSONIDS").val(record.get("PERSONIDS") || "")

            $("#DATATIMEPOINT").val(record.get("DATATIMEPOINT") || ""); //日期
            $("#TIMEPOINT").val(rec.get("TIMEPOINT") || "");     //时间点
            $("#STARTTIME").val(record.get("STARTTIME") || "");    //STARTTIME
            $("#ENDTIME").val(record.get("ENDTIME") || "");        //ENDTIME

            var weeks = record.get("WEEK") || "";
            $(".week").each(function () {
                if ((weeks + "").indexOf($(this).val()) > -1) {
                    $(this).attr("checked", true)
                }
            })
        }
        function rowSelected(rowIndex) {
            rec = store.getAt(rowIndex);
            selectID = rec.get("ID") || "";
            tblencode = rec.get("TBLENCODE") || "";
            tblname = rec.get("TBLNAME") || "";
            selectIndex = rowIndex;

            initCtrl(rec)//控件值初始化
            var objData = eval("(" + rec.get("TBLCLNS") + ")");
            fieldStore.removeAll();
            $.each(objData, function () {
                var EntRecord = fieldStore.recordType;
                var rec = new EntRecord({
                    ID: this.ID,
                    CLNCODE: this.CLNCODE,
                    CLNNAME: this.CLNNAME,
                    ISCHECKED: !!this.ISCHECKED ? this.ISCHECKED : "N",
                    CLNDATATYPE: this.CLNDATATYPE
                });
                fieldStore.insert(fieldStore.data.length, rec);
            });
            //  fieldStore.loadData(rec.get("TBLCLNS"));
        }

        function selectAll(obj, ischecked) {
            var argArray = Array.prototype.slice.call(arguments);
            if (argArray.length == 2) {
                $(".montorfield").each(function () {
                    $(this).attr("checked", ischecked)
                })
                var reds = fieldStore.getRange();
                $.each(reds, function () {
                    this.set("ISCHECKED", ischecked ? "Y" : "N");
                });
            } else {
                $(".montorfield").each(function () {
                    $(this).attr("checked", $(obj).attr("checked"))
                })
                var reds = fieldStore.getRange();
                $.each(reds, function () {
                    this.set("ISCHECKED", $(obj).attr("checked") ? "Y" : "N");
                });
            }
        }

        function filedRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "ISCHECKED":
                    if (value == "Y") {
                        value = "<input type='checkbox' class='montorfield' checked='checked' onclick='filedCK(\"" + rowIndex + "\",this)' />";
                    } else {
                        value = "<input type='checkbox' class='montorfield'  onclick='filedCK(\"" + rowIndex + "\",this)' />";
                    }
                    rtn = value;
                    break;
            }
            return rtn;
        }

        function filedCK(rowindex, sender) {
            var rec = fieldStore.getAt(rowindex);
            rec.set("ISCHECKED", $(sender).attr("checked") ? "Y" : "N");
        }

        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "ISMONTOR":
                    if (value == "Y") {
                        value = "<input type='checkbox' class='tableset' checked='checked' onclick='tblCK(\"" + rowIndex + "\",this)' />";
                    } else {
                        value = "<input type='checkbox' class='tableset' onclick='tblCK(\"" + rowIndex + "\",this)' />";
                    }
                    rtn = value;
                    break;
            }
            return rtn;
        }
        function tblCK(rowindex, sender) {
            window.setTimeout(function () {
                var rec = store.getAt(rowindex);
                rec.set("ISMONTOR", $(sender).attr("checked") ? "Y" : "N");
                $.ajaxExec("UpdataState", { ID: rec.get("ID") || "", ISMONTOR: rec.get("ISMONTOR") || "" }, function (rtn) {
                    rec.commit();
                });
            }, 100);
        }

        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
    <div style="display: none">
        <input id="DEPTIDS" name="DEPTIDS" />
        <input id="PERSONIDS" name="PERSONIDS" />
        <input id="ORGANIZATIONIDS" name="ORGANIZATIONIDS" />
    </div>
</asp:Content>
