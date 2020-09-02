<%@ Page Title="标题" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="SYSLOGList.aspx.cs" Inherits="Aim.OnControl.Web.SYSLOGList" %>

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
        
        .montor
        {
            padding-left: 20xp;
        }
    </style>
    <script src="/js/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "SYSLOGEdit.aspx";

        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {

            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["SYSLOGList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'SYSLOGList',
                idProperty: 'ID',
                data: myData,
                fields: [
			{ name: 'ACTION' },
			{ name: 'REMARK' },
			{ name: 'ID' },
			{ name: 'TABLEEN' },
			{ name: 'TABLECN' },
			{ name: 'COLUMNEN' },
			{ name: 'COLUMNCN' },
			{ name: 'OLDVALUE' },
			{ name: 'NEWVALUE' },
			{ name: 'CONTENT' },
			{ name: 'CREATETIME' },
			{ name: 'CREATEID' },
			{ name: 'CREATENAME' },
			{ name: 'DEPTID' },
			{ name: 'DEPTNAME' },
			{ name: 'COMPANYID' },
			{ name: 'COMPANYNAME' }
			]
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });

            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                collapsed: false,
                columns: 4,
                store: store,
                items: [
                { fieldLabel: '表名', id: 'table', schopts: { qryopts: "{ mode: 'Like', field: 'TABLEEN' }"} },
             {
                 fieldLabel: '操作类型',
                 id: 'action',
                 xtype: 'aimcombo',
                 required: true,
                 name: "ACTION",
                 allowBlank: true,
                 blankText: "请选择...",
                 enumdata: { "%%": "请选择...", "Update": "更新", "Create": "创建", "Delete": "删除", "F": "字段更新" },
                 schopts: {
                     qryopts: "{ mode: 'Like', field: 'ACTION' }"
                 },
                 listeners: {
                     "collapse": function (e) {
                         if (e.getValue()) {
                             Ext.ux.AimDoSearch(Ext.getCmp("action"));
                         }
                     }
                 }
             },
                { fieldLabel: '公司', id: 'COMPANYNAME', schopts: { qryopts: "{ mode: 'Like', field: 'COMPANYNAME' }"} },
                { fieldLabel: '部门', id: 'DEPTNAME', schopts: { qryopts: "{ mode: 'Like', field: 'DEPTNAME' }"} },
                { fieldLabel: '创建人', id: 'CREATENAME', schopts: { qryopts: "{ mode: 'Like', field: 'CREATENAME' }"} },
                {
                    fieldLabel: '起始时间',
                    id: 'StartTime',
                    format: 'Y-m-d',
                    xtype: 'datefield',
                    vtype: 'daterange',
                    endDateField: 'EndTime',
                    schopts: {
                        qryopts: "{ mode: 'GreaterThanEqual', datatype:'Date', field: 'StartTime' }"
                    },
                    listeners: {
                        focus: function (obj) {
                            WdatePicker({
                                maxDate: new Date(),
                                dateFmt: "yyyy-MM-dd"
                            });
                        },
                        blur: function (obj) {
                            return false;
                        }
                    }
                }, {
                    fieldLabel: '截至时间',
                    id: 'EndTime',
                    format: 'Y-m-d',
                    xtype: 'datefield',
                    vtype: 'daterange',
                    startDateField: 'StartTime',
                    schopts: {
                        qryopts: "{ mode: 'LessThanEqual', datatype:'Date', field: 'EndTime' }"
                    },
                    listeners: {
                        focus: function (obj) {
                            var date = $('#StartTime').val() ? $('#StartTime').val() : "";
                            WdatePicker({
                                minDate: date,
                                dateFmt: "yyyy-MM-dd"
                            });
                        },
                        blur: function (obj) {
                            return false;
                        }
                    }
                }
                ]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: []
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //  tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '系统日志',
                store: store,
                region: 'center',
                autoExpandColumn: 'TABLEEN',
                columns: [
                    { id: 'ID', dataIndex: 'ID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'TABLEEN', dataIndex: 'TABLEEN', header: '表名', width: 150, sortable: true, renderer: RowRender },
					{ id: 'TABLECN', dataIndex: 'TABLECN', header: '中文名', width: 100, sortable: true },
					{ id: 'COLUMNEN', dataIndex: 'COLUMNEN', header: '字段', width: 100, sortable: true },
					{ id: 'COMPANYNAME', dataIndex: 'COMPANYNAME', header: '所属公司', width: 200, sortable: true },
					{ id: 'DEPTNAME', dataIndex: 'DEPTNAME', header: '部门', width: 150, sortable: true },
                    { id: 'ACTION', dataIndex: 'ACTION', header: '操作类型', width: 100, sortable: true, renderer: RowRender },
					{ id: 'CREATENAME', dataIndex: 'CREATENAME', header: '创建人', width: 80, sortable: true },
					{ id: 'CREATETIME', dataIndex: 'CREATETIME', header: '创建日期', width: 120, sortable: true },
					{ id: 'Edit', dataIndex: 'Edit', header: '', width: 80, sortable: true, renderer: RowRender }
                    ],
                bbar: pgBar,
                tbar: titPanel
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        //Ext.Window 
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Edit":
                    var str = "<span style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='windowOpen(\"" + record.get("ID") + "\")'>" + "查看详细" + "</span>";
                    rtn = str;
                    break;
                case "TABLEEN":
                    if (value) {
                        value = value.substring((value || "").lastIndexOf(".") + 1, (value || "").length);
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "ACTION":
                    if (value) {
                        var str = "";
                        value = (value || "").toLocaleUpperCase();
                        value = value.indexOf("UPDATE") > -1 ? "更新" : (value.indexOf("CREATE") > -1 ? "创建" : (value.indexOf("DELETE") > -1 ? "删除" : value))
                        rtn = value;
                    } else {
                        rtn = "字段更新";
                    }
                    break;
            }
            return rtn;
        }

        function windowOpen(ID) {

            var win = new Ext.Window({
                title: '详细信息',
                width: 800,
                height: 500,
                padding: '1 5 5 5',
                autoScroll: true,
                maximizable: true,
                bbar: ['->', {
                    text: '取消',
                    iconCls: 'aim-icon-delete',
                    handler: function () {
                        win.close();
                    }
                }],
                html: "<iframe width = \"100%\" height = \"100%\" id = \"frameContent\" src='RecordDetail.aspx?id=" + ID + "&op=r' name = \"frameContent\" frameborder = \"0\" scrolling = \"yes\"></iframe >"
            }).show();
        }

        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display: none;">
        <h1>
            标题</h1>
    </div>
</asp:Content>
