<%@ Page Title="标题" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="SYSTBLCLNSMMListEdit.aspx.cs" Inherits="Aim.OnControl.Web.SYSTBLCLNSMMListEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "SYSTBLCLNSMMEdit.aspx";

        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {

            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["SYSTBLCLNSMMList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'SYSTBLCLNSMMList',
                idProperty: 'ID',
                data: myData,
                fields: [
			{ name: 'ID' },
			{ name: 'REFTBLKEY' },
			{ name: 'REFTBLCODE' },
			{ name: 'CLNCODE' },
			{ name: 'CLNNAME' },
			{ name: 'CLNCOMMENT' },
			{ name: 'CLNDATATYPE' },
			{ name: 'CLNCREATETIME' },
			{ name: 'CLNCREATEUSR' },
			{ name: 'CLNMODIFYTIME' },
			{ name: 'CLNMODIFYUSR' },
			{ name: 'EXT1' },
			{ name: 'EXT2' },
			{ name: 'CREATEID' },
			{ name: 'CREATENAME' },
			{ name: 'CREATETIME' }
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
                items: [
                { fieldLabel: '名称', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                { fieldLabel: '编码', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'Code' }"} },
                { fieldLabel: '创建人', id: 'CreateName', schopts: { qryopts: "{ mode: 'Like', field: 'CreateName' }"}}]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '保存',
                    iconCls: 'aim-icon-save',
                    handler: function () {
                        // 保存修改的数据
                        var recs = store.getModifiedRecords();
                        if (recs && recs.length > 0) {
                            var dt = store.getModifiedDataStringArr(recs) || [];

                            jQuery.ajaxExec('batchsave', { "data": dt }, function () {
                                store.commitChanges();

                                AimDlg.show("保存成功！");
                            });
                        }
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
                store: store,
                region: 'center',
                autoExpandColumn: 'CLNNAME',
                columns: [
                    { id: 'ID', dataIndex: 'ID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'CLNCODE', dataIndex: 'CLNCODE', header: '字段', width: 100, sortable: true },
					{ id: 'CLNNAME', dataIndex: 'CLNNAME', header: '中文名', width: 100, editor: { xtype: 'textfield'} },
                     { id: 'CLNDATATYPE', dataIndex: 'CLNDATATYPE', header: '数据类型', width: 120, sortable: true }
                    ]
                // tbar: titPanel
            });
            grid.on("afteredit", function (e) {
                var rec = e.record;
                if (!!rec.get("CLNNAME")) {
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
                items: [grid]
            });
        }

        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
