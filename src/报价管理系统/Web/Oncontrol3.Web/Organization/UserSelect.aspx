<%@ Page Title="人员选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="UserSelect.aspx.cs" Inherits="CRM.Web.UserSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background: url(../theme/default/images/public/paperbg.jpg);
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
            text-align: center;
        }
    </style>

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">
        var myData, store, viewport, pgBar, schBar, tlBar, titPanel;
        function onSelPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'UserID',
                data: myData,
                fields: [
			    { name: 'UserID' }, { name: 'WorkNo' }, { name: 'Name' }
			]
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            var buttonPanel = new Ext.form.FormPanel({
                region: 'south',
                frame: true,
                buttonAlign: 'center',
                buttons: [{ text: '确定', handler: function() { AimGridSelect(); } }, { text: '取消', handler: function() {
                    window.close();
                } }]
                });
                schBar = new Ext.ux.AimSchPanel({
                    store: store,
                    columns: 2,
                    collapsed: false,
                    items: [{ fieldLabel: '工号/姓名', id: 'SName', schopts: { qryopts: "{ mode: 'Like', field: 'SName' }"} }
                       ]
                });
                tlBar = new Ext.ux.AimToolbar({
                    items: [
           ]
                });
                titPanel = new Ext.ux.AimPanel({
                    items: [schBar]
                });
                // 表格面板
                Ext.override(Ext.grid.CheckboxSelectionModel, {
                    handleMouseDown: function(g, rowIndex, e) {
                        if (e.button !== 0 || this.isLocked()) {
                            return;
                        }
                        var view = this.grid.getView();
                        if (e.shiftKey && !this.singleSelect && this.last !== false) {
                            var last = this.last;
                            this.selectRange(last, rowIndex, e.ctrlKey);
                            this.last = last; // reset the last     
                            view.focusRow(rowIndex);
                        } else {
                            var isSelected = this.isSelected(rowIndex);
                            if (isSelected) {
                                this.deselectRow(rowIndex);
                            } else if (!isSelected || this.getCount() > 1) {
                                this.selectRow(rowIndex, true);
                                view.focusRow(rowIndex);
                            }
                        }
                    }
                });
                AimSelGrid = new Ext.ux.grid.AimGridPanel({
                    title: '<img src="/images/titleicons.gif" style="margin-top:3px;float:left"/><span style="padding-left:10px;">人员选择</span>',
                    store: store,
                    region: 'center',
                    autoExpandColumn: 'Name',
                    columns: [
                    { id: 'UserID', header: '标识', dataIndex: 'UserID', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    AimSelCheckModel,
					{ id: 'WorkNo', header: '员工工号', width: 130, sortable: true, dataIndex: 'WorkNo' },
					{ id: 'Name', header: '员工姓名', width: 180, sortable: true, dataIndex: 'Name' }
					 ],
                    bbar: pgBar,
                    tbar: titPanel
                });
                viewport = new Ext.ux.AimViewport({
                    items: [AimSelGrid, buttonPanel]
                });
            }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
