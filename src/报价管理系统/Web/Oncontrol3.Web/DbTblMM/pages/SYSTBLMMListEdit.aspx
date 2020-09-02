<%@ Page Title="标题" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="SYSTBLMMListEdit.aspx.cs" Inherits="Aim.OnControl.Web.SYSTBLMMListEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
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
    </style>
    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "SYSTBLMMEdit.aspx";

        var store, myData, rec;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {

            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["SYSTBLMMList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'SYSTBLMMList',
                idProperty: 'ID',
                data: myData,
                fields: [
			{ name: 'ID' },
			{ name: 'TBLCODE' },
			{ name: 'TBLNAME' },
			{ name: 'TBLTYPE' },
			{ name: 'TBLCOMMENT' },
			{ name: 'CREATEID' },
			{ name: 'CREATENAME' },
			{ name: 'CREATETIME' },
			{ name: 'TBLCREATETIME' },
			{ name: 'TBLCREATEUSR' },
			{ name: 'TBLMODIFYTIME' },
			{ name: 'TBLMODIFYUSR' },
			{ name: 'CAPATICY' },
			{ name: 'EXT1' },
			{ name: 'EXT2' }
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
                columns: 4,
                items: [
                { fieldLabel: '表名', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'TBLCODE' }"} },
                { fieldLabel: '中文名', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'TBLNAME' }"} }
                 ]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [


                //                {
                //                    text: '添加',
                //                    iconCls: 'aim-icon-add',
                //                    handler: function () {
                //                        ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
                //                    }
                //                }, {
                //                    text: '修改',
                //                    iconCls: 'aim-icon-edit',
                //                    handler: function () {
                //                        // 保存修改的数据
                //                        //                        var recs = store.getModifiedRecords();
                //                        //                        if (recs && recs.length > 0) {
                //                        //                            var dt = store.getModifiedDataStringArr(recs) || [];
                //                        //                            jQuery.ajaxExec('batchsave', { "data": dt }, function () {
                //                        //                                store.commitChanges();
                //                        //                                AimDlg.show("保存成功！");
                //                        //                            });
                //                        //                        }

                //                        ExtOpenGridEditWin(grid, EditPageUrl, "u", EditWinStyle);
                //                    }
                //                }, 

                {
                text: '导出Excel',
                iconCls: 'aim-icon-xls',
                handler: function () {
                    ExtGridExportExcel(grid, { store: null, title: '标题' });
                }
            }, '->', {
                text: '刷新数据源',
                iconCls: 'aim-icon-search',
                handler: function () {
                    Ext.getBody().mask("数据重新加载中，请稍等");
                    $.ajaxExec("reflash", {}, function (rtn) {
                        if (rtn.data.statue == "1") {
                            AimDlg.show("数据刷新成功!");
                            store.reload({ data: {} });
                        }
                        Ext.getBody().unmask();
                    });
                }
            }]
        });

        // 工具标题栏
        titPanel = new Ext.ux.AimPanel({
            tbar: tlBar,
            items: [schBar]
        });

        // 表格面板
        grid = new Ext.ux.grid.AimEditorGridPanel({
            title: '数据库表',
            store: store,
            region: 'center',
            autoExpandColumn: 'TBLNAME',
            columns: [
                    { id: 'ID', dataIndex: 'ID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'TBLCODE', dataIndex: 'TBLCODE', header: '表名', width: 180, sortable: true },
            //{ id: 'TBLTYPE', dataIndex: 'TBLTYPE', header: '类型', width: 100, sortable: true, renderer: RowRender },
            //{ id: 'CAPATICY', dataIndex: 'CAPATICY', header: '容量', width: 100, sortable: true },
            //{ id: 'CreateTime', dataIndex: 'CreateTime', header: '创建日期', width: 100, sortable: true },
                    {id: 'TBLNAME', dataIndex: 'TBLNAME', header: '中文名', width: 100, editor: { xtype: 'textfield'} }
                    ],
            bbar: pgBar,
            tbar: titPanel
        });
        grid.on("rowclick", function (grid, rowIndex, e) {
            rec = store.getAt(rowIndex);
            var title = escape(rec.get("TBLCODE") || "");
            Ext.getCmp("clnGrid").setTitle("【" + rec.get("TBLCODE") + "】字段详细");
            frameContent.location.href = "SYSTBLCLNSMMListEdit.aspx?reftblkey=" + rec.get("ID") + "&title=" + title;
        })

        grid.on("afteredit", function (e) {
            var rec = e.record;
            if (!!rec.get("TBLNAME")) {
                var recJson = $.getJsonString(rec.data);
                $.ajaxExec("afterEdit", { recJson: recJson }, function () {
                    e.record.commit();
                });
            } else {
                e.record.commit();
            }
        })

        // 页面视图
        viewport = new Ext.ux.AimViewport({
            items: [grid, {
                id: "clnGrid",
                title: '字段详细',
                // height: parseInt($("body").innerHeight() / 2) - 20,
                width: '50%',
                //collapsible: true,
                // collapsed: false,
                region: 'east',
                //  split: true,
                margins: '0 0 0 0',
                cls: 'empty',
                bodyStyle: 'background:#f1f1f1',
                html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0"></iframe>'
            }]
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

    function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
        var rtn = "";
        switch (this.id) {
            case "TBLTYPE":
                if (value) {
                    value = (value + "") == "NO" ? "无分区" : value;
                    rtn = value;
                }
                break;
        }
        return rtn;
    }

    // 提交数据成功后
    function onExecuted() {
        store.reload();
    }
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
