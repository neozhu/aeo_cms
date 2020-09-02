<%@ Page Title="组织机构" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="GroupTree.aspx.cs" Inherits="CRM.Web.GroupTree" %>

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
        }
        .x-grid3
        {
            background-color: #eef2f6;
        }
    </style>
    <%--<script src="/js/ext/ux/TreeCheckNodeUI.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        var EditWinStyle = "dialogWidth:550px; dialogHeight:250px; scroll:yes; center:yes; status:no; resizable:yes;";
        var IsReceiver = $.getQueryString({ ID: 'IsReceiver' });
        var DataRecord, store, treeLoader;
        var viewport, grid, contextMenu;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            // var treeData = adjustData(AimState["DataTree"]);
            treeLoader = new Ext.tree.TreeLoader({
            // baseAttrs: { uiProvider: Ext.ux.TreeCheckNodeUI }
        });
        tlBar = new Ext.ux.AimToolbar({
            items: [{}]
        });


        var tree = new Ext.tree.TreePanel({
            title: '<img src="/images/titleicons.gif" style="margin-top:3px;float:left"/><span style="padding-left:10px;">组织机构</span>',
            id: 'tree',
            region: 'west',
            expanded: true,
            border: false,
            //    tbar: titPanel,
            width: 430,
            // tbar: tlBar,
            autoScroll: true,
            animate: true,
            checkModel: 'cascade',
            containerScroll: true,
            lines: true, //节点之间连接的横竖线
            //rootVisible: false, //是否显示根节点
            loader: treeLoader,
            root: new Ext.tree.AsyncTreeNode({
                id: '1001',
                text: '飞力集团',
                leaf: false
                // children: treejson
            }),
            listeners: { 'beforeload': function(node) {
                tree.loader.dataUrl = 'GroupTree.aspx?reqaction=querydescendant&id=' + node.attributes.id;
            },
                "click": function(node, e) {
                    if (IsReceiver == "T") {
                        frameContent.location.href = "GroupReceiverList.aspx?GroupID=" + node.attributes.id + "&GroupName=" + escape(node.attributes.text);
                    }
                    else {
                        frameContent.location.href = "UserListByGroup.aspx?GroupID=" + node.attributes.id + "&GroupName=" + escape(node.attributes.text);
                    }
                }
            }
        });
        viewport = new Ext.ux.AimViewport({
            layout: 'border',
            items: [tree, {
                border: false,
                region: 'center',
                html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0"></iframe>'}]
            });
            var rootNode = tree.getRootNode();
            rootNode.expand(); //展开根节点
            if (rootNode) {
                if (IsReceiver == "T") {
                    frameContent.location.href = "GroupReceiverList.aspx?GroupID=" + rootNode.attributes.id + "&GroupName=" + escape(rootNode.attributes.text);
                }
                else {
                    frameContent.location.href = "UserListByGroup.aspx?GroupID=" + rootNode.attributes.id + "&GroupName=" + escape(rootNode.attributes.text);
                }
            }
        }       
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
