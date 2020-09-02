<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="UserListByGroup.aspx.cs" Inherits="CRM.Web.UserListByGroup" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background: url(/theme/default/images/public/paperbg.jpg);
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
        .x-grid3
        {
            background-color: #eef2f6;
        }
    </style>

    <script type="text/javascript">
        var viewport;
        var store;
        var grid, pgBar;
        var GroupID, GroupName;
        function onPgLoad() {
            setPgUI();
            qtype = $.getQueryString({ "ID": "type" });
            op = $.getQueryString({ "ID": "op" });
            id = $.getQueryString({ "ID": "id" });
            GroupID = $.getQueryString({ "ID": "GroupID" });
            GroupName = unescape($.getQueryString({ "ID": "GroupName" }));
        }

        function setPgUI() {
            var myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'UsrList',
                idProperty: 'UserID',
                data: myData,
                fields: [
                { name: 'UserID' },
                { name: 'Name' },
                { name: 'LoginName' },
                { name: 'WorkNo' },
                { name: 'Status' },
                { name: 'Email' },
                { name: 'Remark' },
                { name: 'CreateDate', type: 'date'}],
                proxy: new Ext.ux.data.AimRemotingProxy({
                    aimbeforeload: function(proxy, options) {
                        options.data = { GroupID: GroupID };
                    }
                })
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            var schBar = new Ext.Panel({
                collapsed: true,
                unstyled: true,
                padding: 5,
                layout: 'column',
                items: []
            });
            var tlBar = new Ext.ux.AimToolbar({
                items: [
                //                { text: '添加人员', iconCls: 'aim-icon-user-add', aimexecutable: true,
                //                    handler: function() {
                //                        openMdlWin("/CommonPages/Select/UsrSelect/MUsrSelect.aspx?rtntype=array", "addgrpuser");
                //                    }
                //                }, { text: '移除人员', iconCls: 'aim-icon-user-delete', aimexecutable: true,
                //                    handler: function() {
                //                        UpdateGroupUser('delete');
                //                    }
                //                }, '->', { text: '查询:' }, new Ext.app.AimSearchField({ store: store, pgbar: pgBar, schbutton: true, qryopts: "{ type: 'fulltext' }" })
                ]
            });
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [schBar]
            });
            grid = new Ext.ux.grid.AimGridPanel({
                title: '<img src="/images/titleicons.gif" style="margin-top:3px;float:left"/><span style="padding-left:10px;">人员列表</span>',
                store: store,
                region: 'center',
                columns: [
                { id: 'UserID', header: 'UserID', dataIndex: 'UserID', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'Name', header: '姓名', width: 100, sortable: true, dataIndex: 'Name' },
                { id: 'WorkNo', header: '工号', width: 100, sortable: true, dataIndex: 'WorkNo' },
                { id: 'Email', header: '邮箱', width: 200, sortable: true, dataIndex: 'Email' },
                { id: 'Status', header: '状态', width: 100, renderer: linkRender, sortable: true, dataIndex: 'Status' }
            ],
                // bbar: pgBar,
                // tbar: titPanel,
                autoExpandColumn: 'Email'
            });
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid]
            });
        }
        function linkRender(val, p, rec) {
            var rtn = val;
            switch (this.dataIndex) {
                case "Name":
                    rtn = "<a class='aim-ui-link' onclick='openMdlWin(\"UsrEdit.aspx?id=" + rec.id + "\", null, usrEditStyle)'>" + val + "</a>";
                    break;
                case "Status":
                    rtn = StatusEnum[val];
                    break;
            }

            return rtn;
        }
        // 打开模态窗口
        function openMdlWin(url, op, style) {
            op = op || "r";
            style = style || "dialogWidth:750px; dialogHeight:550px; scroll:yes; center:yes; status:no; resizable:yes;";

            var params = [];
            params[params.length] = "op=" + op;

            url = $.combineQueryUrl(url, params)
            rtn = window.showModalDialog(url, window, style);
            if (rtn && rtn.result) {
                if (rtn.result === 'success') {
                    if (op == 'addgrpuser') {
                        var uids = [];
                        var usrs = rtn.data;

                        $.each(usrs, function() {
                            if (this.UserID) {
                                uids.push(this.UserID);
                            }
                        });

                        $.ajaxExec("adduserbycc", { id: id, UserIDs: uids }, onExecuted);
                        //UpdateGroupUser('add', uids);
                    }
                }
            }
        }
        function onExecuted() {
            store.reload();
        }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
