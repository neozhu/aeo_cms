<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="UsrView.aspx.cs" Inherits="CRM.Web.Virtual.UsrView" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background: url(../theme/default/images/public/paperbg.jpg);
        }
        #header
        {
            background: url(/theme/default/images/public/block_1/titlebg2.png) left top no-repeat;
            display: block;
            font: 14px "Microsoft YaHei" , Tahoma, Geneva, sans-serif;
        }
        #header h1
        {
            font-size: 14px;
            color: #fff;
            font-weight: normal;
            padding: 5px 10px;
            height: 32px !important;
        }
    </style>

    <script type="text/javascript">
        var StatusEnum = { '1': '有效', '0': '无效' };
        var usrEditStyle = "dialogWidth:450px; dialogHeight:300px; scroll:yes; center:yes; status:no; resizable:yes;";
        var viewport;
        var store;
        var grid, pgBar;
        var qtype, op, id;
        var GroupID, GroupName;
        function onPgLoad() {
            setPgUI();
            qtype = $.getQueryString({ "ID": "type" });
            op = $.getQueryString({ "ID": "op" });
            GroupID = $.getQueryString({ "ID": "GroupID" });
            GroupName = unescape($.getQueryString({ "ID": "GroupName" }));
        }

        function setPgUI() {
            var myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["UsrList"] || []
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
                { name: 'STARTTIME' },
                { name: 'STOPTIME' },
                { name: 'Email' },
                { name: 'Remark' },
                { name: 'CreateDate', type: 'date'}],
                proxy: new Ext.ux.data.AimRemotingProxy({
                    aimbeforeload: function(proxy, options) {
                        options.data = { type: qtype, id: id, op: op, "GroupID": GroupID };
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
                items: [{
                    text: '添加人员', iconCls: 'aim-icon-user-add', aimexecutable: true,
                    handler: function() {
                        openMdlWin("/CommonPages/Select/UsrSelect/MUsrSelect.aspx?rtntype=array", "addgrpuser");
                    }
                }, {
                    text: '启用', iconCls: 'aim-icon-run',
                    handler: function() {
                        UpdateGroupUser('enabled');
                    }
                }, {
                    text: '停用', iconCls: 'aim-icon-stop',
                    handler: function() {
                        UpdateGroupUser('disabled');
                    }
                }, {
                    text: '移除人员', iconCls: 'aim-icon-user-delete',
                    hidden: true,
                    handler: function() {
                        UpdateGroupUser('delete');
                    }
                }, '->', { text: '查询:' }, new Ext.app.AimSearchField({ store: store, pgbar: pgBar, schbutton: true, qryopts: "{ type: 'fulltext' }" })]
            });
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [schBar]
            });
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                region: 'center',
                margins: '32 0 0 0',
                columns: [
                { id: 'UserID', header: 'UserID', dataIndex: 'UserID', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'Name', header: '姓名', width: 50, sortable: true, dataIndex: 'Name' },
                { id: 'WorkNo', header: '工号', width: 80, sortable: true, dataIndex: 'WorkNo' },
                { id: 'Email', header: '邮箱', width: 130, sortable: true, dataIndex: 'Email' },
                { id: 'Status', header: '状态', width: 40, align: 'center', sortable: true, dataIndex: 'Status' },
                { id: 'STARTTIME', header: '启用时间', width: 80, align: 'center', sortable: true, dataIndex: 'STARTTIME', renderer: ExtGridDateOnlyRender },
                { id: 'STOPTIME', header: '停用时间', width: 80, align: 'center', sortable: true, dataIndex: 'STOPTIME', renderer: ExtGridDateOnlyRender }
                ],
                bbar: pgBar,
                tbar: titPanel,
                autoExpandColumn: 'Email'
            });
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid]
            });
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
                            if (this.UserID && store.findExact('UserID', this.UserID) == -1) {
                                uids.push(this.UserID);
                            }
                        });
                        if (uids.length > 0) {
                            $.ajaxExec("adduserbycc", { GroupID: GroupID, UserIDs: uids }, onExecuted);
                        }
                    }
                }
            }
        }

        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }

        // 添加用户到组
        function UpdateGroupUser(op, uids) {
            if (op == 'add' && uids) {
                $.ajaxExec("addgrpuser", { id: id, UserIDs: uids }, onExecuted);
            }
            else if (op == "enabled") {
                var uids = [];
                var recs = grid.getSelectionModel().getSelections();
                if (!recs || recs.length <= 0) {
                    AimDlg.show("请先选择要启用的人员！");
                    return;
                }

                if (!confirm("确定启用所选人员？")) {
                    return;
                }
                var stop = false;
                if (recs != null) {
                    $.each(recs, function() {
                        if (this.json.Status == "启用") {
                            alert("已启用的记录不需要再次启用！");
                            stop = true;
                            return false;
                        }
                        uids.push(this.json.UserID);
                    })
                }

                if (stop)
                    return;

                $.ajaxExec("enabled", { GroupID: GroupID, UserIDs: uids }, onExecuted);
            }
            else if (op == "disabled") {
                var uids = [];
                var recs = grid.getSelectionModel().getSelections();
                if (!recs || recs.length <= 0) {
                    AimDlg.show("请先选择要停用的人员！");
                    return;
                }

                if (!confirm("确定停用所选人员？")) {
                    return;
                }
                var stop = false;
                if (recs != null) {
                    $.each(recs, function() {
                        if (this.json.Status == "停用") {
                            alert("已停用的记录不需要再次停用！");
                            stop = true;
                            return false;
                        }
                        uids.push(this.json.UserID);
                    });
                }

                if (stop)
                    return;

                $.ajaxExec("disabled", { GroupID: GroupID, UserIDs: uids }, onExecuted);
            }
            else if (op == "delete" || op == "remove") {
                var uids = [];
                var recs = grid.getSelectionModel().getSelections();
                if (!recs || recs.length <= 0) {
                    AimDlg.show("请先选择要删除的人员！");
                    return;
                }

                if (!confirm("确定删除所选人员？")) {
                    return;
                }

                if (recs != null) {
                    $.each(recs, function() {
                        uids.push(this.json.UserID);
                    });
                }

                $.ajaxExec("delgrpuser", { GroupID: GroupID, UserIDs: uids }, onExecuted);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            <img src="/images/titleicons.gif" style="margin-top: 3px; margin-right: 6px; float: left" />人员列表</h1>
    </div>
</asp:Content>
