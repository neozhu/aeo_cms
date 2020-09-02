<%@ Page Title="组织策划" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="OrgStructure.aspx.cs" Inherits="CRM.Web.Virtual.OrgStructure" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background: url(/theme/default/images/public/paperbg.jpg);
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
            height: 22px;
        }
        .x-panel-header-text
        {
            font: 14px "Microsoft YaHei" , Tahoma, Geneva, sans-serif;
            text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.5);
            float: left;
            color: #fff;
            vertical-align: middle;
        }
        .x-panel-header
        {
            border: none;
            color: #333;
            font-weight: bold;
            font-size: 11px;
            height: 24px;
            font-family: tahoma,arial,verdana,sans-serif;
            border-color: #d0d0d0;
            background-image: url(/images/titlebg.png);
        }
    </style>
    <link href="/App_Themes/Ext/ux/TreeGrid/TreeGrid.css" rel="stylesheet" type="text/css" />

    <script src="/js/ext/ux/TreeGrid.js" type="text/javascript"></script>

    <script src="/js/ext/ux/FieldLabeler.js" type="text/javascript"></script>

    <script src="/js/pgfunc-ext-adv.js" type="text/javascript"></script>

    <script type="text/javascript">
        var EditWinStyle = "dialogWidth:550px; dialogHeight:250px; scroll:yes; center:yes; status:no; resizable:yes;";
        var EditPageUrl = "OrgStructureEdit.aspx";

        var DataRecord, store, gridRole, rolID, groupId;
        var viewport, grid, contextMenu, store2;

        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {
            var data = AimState["DtList"];
            DataRecord = Ext.data.Record.create([
            { name: 'GROUPID' }, { name: 'ParentID' }, { name: 'IsLeaf', type: 'bool' }, { name: 'NAME' }, { name: 'CODE' },
            { name: 'TYPE' }, { name: 'STATUS' }, { name: 'SORTINDEX' }, { name: 'REMARK' }, { name: 'CREATETIME' }
            ]);
            store = new Ext.ux.data.AimAdjacencyListStore({
                data: data,
                aimbeforeload: function(proxy, options) {
                    var rec = store.getById(options.anode);
                    options.reqaction = "querychildren";
                    if (rec) {
                        options.data.id = rec.id;
                    }
                },
                reader: new Ext.ux.data.AimJsonReader({ id: 'GROUPID', dsname: 'DtList' }, DataRecord)
            });

            // 搜索栏
            var schBar = new Ext.ux.AimSchPanel({
                items: []
            });

            // 工具栏
            var tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '展开', handler: function() { store.expandAll(); }
                }, '-', { html: '<span color="yellow">(右键编辑节点)</span>', xtype: 'tbtext'}]
            });
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [schBar]
            });
            var sm = new Ext.grid.RowSelectionModel({
                singleSelect: false,
                listeners: {
                    rowselect: function(g, ridx, e) {
                        if (e.data.TYPE == 3) {
                            rolID = e.data.GROUPID;
                            groupId = e.data.ParentID;
                            frameContent.location.href = "UsrView.aspx?GroupID=" + e.data.GROUPID + "&GroupName=" + escape(e.data.NAME);
                            store2.reload();
                        }
                        else {
                            rolID = "";
                            groupId = "";
                        }
                    }
                }
            });
            grid = new Ext.ux.grid.AimEditorTreeGridPanel({
                store: store,
                master_column_id: 'NAME',
                region: 'west',
                split: true,
                margins: '32 0 0 0',
                width: 330,
                minSize: 250,
                maxSize: 500,
                columns: [
				{ id: 'NAME', header: "组织结构", renderer: colRender, width: 110, sortable: true, dataIndex: 'NAME' },
				{ header: "编号", width: 70, sortable: true, dataIndex: 'CODE', hidden: true },
                { header: "排序号", width: 60, sortable: true, dataIndex: 'SORTINDEX', hidden: true }
                ],
                sm: sm,
                autoExpandColumn: 'NAME',
                tbar: titPanel
            });

            grid.on("rowcontextmenu", function(grid, rowIdx, e) {
                e.preventDefault(); //这行是必须的
                showContextMenu(rowIdx, e.getXY());
            });

            ///----rolegrid begin---

            var myData2 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["ActualRoleList"] || []
            };

            store2 = new Ext.ux.data.AimJsonStore({
                dsname: 'ActualRoleList',
                idProperty: 'ID',
                data: myData2,
                fields: [{ name: 'ID' }, { name: 'ROLENAME' }, { name: 'ROLEID'}],
                listeners: {
                    "aimbeforeload": function(proxy, options) {
                        options.data.Rolid = rolID;
                    }
                }
            });

            // 分页栏
            var pgBar2 = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store2,
                displayInfo: true,
                displayMsg: '当前条目 {0} - {1}, 总条目 {2}',
                emptyMsg: "无条目显示",
                items: ['-']
            });

            // 搜索栏
            var schBar2 = new Ext.ux.AimSchPanel({
                store: store2,
                collapsed: false,
                columns: 3,
                height: 38,
                items: [
                    { fieldLabel: '角色', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                    { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '查 询', handler: function() {
                        Ext.ux.AimDoSearch(Ext.getCmp("Name"));
                    } }]
            });

            // 工具栏
            var tlBar2 = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        if (rolID) {//groupId
                            var style = "dialogWidth:550px; dialogHeight:550px; scroll:yes; center:yes; status:no; resizable:yes;";
                            var url = "/CommonPages/Select/GrpSelect/MGrpSelect.aspx?seltype=multi&cid=2&showrole=true&CompanyId=" + groupId;
                            OpenModelWin(url, {}, style, function() {
                                if (this.data == null) return;
                                var Names = "";
                                var Ids = "";
                                for (var i = 0; i < this.data.length; i++) {
                                    if (this.data[0].Type == 3) {
                                        Names += this.data[i].Name + ",";
                                        Ids += this.data[i].GroupID + ",";
                                    }
                                }

                                if (Names.length > 0) {
                                    Names = Names.substring(0, Names.length - 1);
                                    Ids = Ids.substring(0, Ids.length - 1);

                                    jQuery.ajaxExec('setrolenames', { "id": rolID, ids: Ids, names: Names }, function(rtn) {
                                        store2.reload();
                                    });
                                }
                            });
                        }
                        else {
                            alert("请先选择角色");
                        }
                    }
                }, {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var roleids = "";
                        var sels = gridRole.getSelectionModel().getSelections();
                        if (sels.length == 0) {
                            alert("请选择要删除的岗位!"); return;
                        }
                        if (!window.confirm("确定删除？")) return;
                        for (var i = 0; i < sels.length; i++) {
                            roleids += sels[i].json.ROLEID + ',';
                        }

                        if (roleids.length > 0) {
                            roleids = roleids.substring(0, roleids.length - 1);
                            jQuery.ajaxExec('deleterolenames', { "id": rolID, roleids: roleids }, function(rtn) {
                                if (rtn) {
                                    alert("删除成功!");
                                    store2.reload();
                                }
                            });
                        }
                    }
}]
                });

                // 工具标题栏
                var titPanel2 = new Ext.ux.AimPanel({
                    tbar: tlBar2,
                    items: [schBar2]
                });

                // 表格面板
                gridRole = new Ext.ux.grid.AimGridPanel({
                    title: '<img src="/images/titleicons.gif" style="margin-top:3px;float:left"/><span style="padding-left:10px;">实际岗位</span>',
                    store: store2,
                    region: 'center',
                    monitorResize: true,
                    columns: [
                            { id: 'ROLEID', header: 'ROLEID', dataIndex: 'ROLEID', hidden: true },
                            new Ext.ux.grid.AimRowNumberer(),
                            new Ext.ux.grid.AimCheckboxSelectionModel(),
                            { id: 'ROLENAME', header: '岗位', width: 180, sortable: true, dataIndex: 'ROLENAME'}],
                    bbar: pgBar2,
                    tbar: tlBar2
                });

                //-----roleGrid end---------------


                viewport = new Ext.ux.AimViewport({
                    layout: 'border',
                    items: [grid, gridRole, {
                        region: 'east',
                        margins: '0 1 1 0',
                        border: false,
                        split: true,
                        width: 550,
                        html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0" ></iframe>'
}]
                    });
                    // 展开所有加载的节点
                    var roots = store.getRootNodes();
                    if (roots) {
                        try {
                            $.each(roots, function() {
                                store.expandNode(this);
                            });
                        }
                        catch (ex)
                { }
                    }

                    if (store.data.length > 0) {
                        return;
                        frameContent.location.href = "UsrView.aspx?GroupID=" + store.getAt(0).get("GROUPID") + "&GroupName=" + escape(store.getAt(0).get("NAME"));
                    }
                }

                function colRender(val, p, rec) {
                    var rtn = val;
                    var type = rec.get('TYPE');

                    switch (type) {
                        case 3:
                            rtn = '<span valign="bottom"><img src="/images/shared/user_red.png">' + val + '</span>';
                            break;
                        case 2:
                            rtn = '<span valign="bottom"><img src="/images/shared/preview2.png">' + val + '</span>';
                            break;
                    }

                    return rtn;
                }

                //添加右键
                function showContextMenu(rowIdx, xy) {
                    if (pgOperation == 'r') {
                        return false;
                    }

                    var rec = store.getAt(rowIdx);

                    if (!contextMenu) {
                        contextMenu = new Ext.menu.Menu({ id: 'contextMenu' });

                        menuItemAddCls = new Ext.menu.Item({
                            id: 'menuItemAddSid',
                            text: '新增同级部门'
                        });

                        menuItemAdd = new Ext.menu.Item({
                            id: 'menuItemAddSub',
                            text: '新增下级部门'
                        });

                        menuItemAddSub = new Ext.menu.Item({
                            id: 'menuItemAddSubRole',
                            text: '新增组织角色'
                        });


                        menuItemUpdate = new Ext.menu.Item({
                            id: 'menuItemUpdate',
                            text: '修改'
                        });

                        menuItemDelete = new Ext.menu.Item({
                            id: 'menuItemDelete',
                            text: '删除'
                        });

                        contextMenu.addItem(menuItemAdd);
                        contextMenu.addItem(menuItemAddSub);

                        contextMenu.addItem(menuItemUpdate);
                        contextMenu.addItem(menuItemDelete);
                    }

                    if (rec.get("TYPE") == 2) {
                        menuItemAddSub.setVisible(true);
                        menuItemAdd.setVisible(true);
                    }

                    if (!rec.get('ParentID')) {
                        menuItemAddCls.setVisible(false);
                        menuItemDelete.setVisible(false);
                        menuItemUpdate.setVisible(false);
                    } else if (rec.get('IsLeaf') == false) {
                        menuItemDelete.setVisible(false);
                    }
                    else {
                        menuItemAddCls.setVisible(true);
                        menuItemAdd.setVisible(true);
                        menuItemAddSub.setVisible(true);
                        menuItemDelete.setVisible(true);
                        menuItemUpdate.setVisible(true);
                    }

                    //角色不能新增下级
                    if (rec.get("TYPE") == 3) {
                        menuItemAddSub.setVisible(false);
                        menuItemAdd.setVisible(false);
                    }

                    menuItemAdd.setHandler(function() { showEditWin('cs', rec, "2"); });   // 创建子节点
                    menuItemAddSub.setHandler(function() { showEditWin('cs', rec, "3"); });      // 创建子节点角色
                    menuItemUpdate.setHandler(function() { showEditWin('u', rec); });  // 更新节点
                    menuItemDelete.setHandler(function() { batchOperate('batchdelete', [rec]); }); // 删除节点

                    contextMenu.showAt(xy);
                }

                function batchOperate(action, recs, params, url) {
                    if (!recs || recs.length <= 0) {
                        AimDlg.show("请先选择要操作的结点！");
                        return;
                    } else if (!confirm("确定要删除该结点吗？")) {
                        return;
                    }

                    ExtBatchOperate(action, recs, params, url, function(args) {
                        if (args.status == "success") {
                            var pids = $.map(recs, function(n, i) {
                                var tpid = n.data["ParentID"];
                                store.remove(n);
                                var pnode = store.getNodeParent(n);
                                store.expandNode(pnode);
                                var childs = store.getNodeChildren(pnode);
                                if (childs.length == 0) {
                                    //把父节点设置成叶子节点
                                    pnode.data.IsLeaf = true;
                                }
                                return tpid;
                            });
                        }
                    });
                }

                function showEditWin(op, rec, type) {
                    rec = rec || {};
                    OpenModelWin(EditPageUrl, { op: op, id: rec.id, type: type }, EditWinStyle, function() {
                        window.location.reload();
                    });
                }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            <img src="/images/titleicons.gif" style="margin-top: 3px; margin-right: 6px; float: left" />组织机构</h1>
    </div>
</asp:Content>
