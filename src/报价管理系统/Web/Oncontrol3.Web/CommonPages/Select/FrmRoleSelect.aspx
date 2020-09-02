<%@ Page Title="角色选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="FrmRoleSelect.aspx.cs" Inherits="CRM.Web.FrmRoleSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onSelPgLoad() {
            setPgUI();
        }

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
                { name: 'NAME' }
			], listeners: { "aimbeforeload": function(proxy, options) {
			    options.data.UserId = $.getQueryString({ ID: "UserId", DefaultValue: "" });
			}
			}
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
                columns: 3,
                items: [
                { fieldLabel: '角色', id: 'NAME', schopts: { qryopts: "{ mode: 'Like', field: 'NAME' }"} },
                { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '查 询', handler: function() {
                    Ext.ux.AimDoSearch(Ext.getCmp("NAME"));
                }
}]
                });

                // 工具栏
                tlBar = new Ext.ux.AimToolbar({
                    items: ['<font color=red style="font-size:12px;">请点击复选框选择/取消选择记录</font>']
                });

                // 工具标题栏
                titPanel = new Ext.ux.AimPanel({
                    tbar: tlBar,
                    items: [schBar]
                });

                var buttonPanel = new Ext.form.FormPanel({
                    region: 'south',
                    frame: true,
                    buttonAlign: 'center',
                    buttons: [{ text: '确定', handler: function() {
                        AimGridSelect();
                    }
                    }, { text: '取消', handler: function() {
                        window.close();
                    } }]
                    });

                    // 表格面板
                    grid = new Ext.ux.grid.AimEditorGridPanel({
                        store: store,
                        region: 'center',
                        autoExpandColumn: 'NAME',
                        columns: [
                        new Ext.ux.grid.AimRowNumberer(),
                        new Ext.ux.grid.AimCheckboxSelectionModel(),
                        { id: 'NAME', dataIndex: 'NAME', header: '角色', width: 130, sortable: true}],
                        bbar: pgBar,
                        tbar: titPanel
                    });

                    AimSelGrid = grid;
                    // 页面视图
                    viewport = new Ext.ux.AimViewport({
                        items: [grid, buttonPanel]
                    });
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
            人力需求申请单选择</h1>
    </div>
</asp:Content>
