<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="AuthTree.aspx.cs" Inherits="CRM.Web.AuthTree" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script src="/js/ext/ext-base.js" type="text/javascript"></script>
    <script type='text/javascript'>
        if (Ext.isIE6 && /msie 9/.test(navigator.userAgent.toLowerCase())) {
            Ext.isIE6 = Ext.isIE = false;
            Ext.isChrome = Ext.isIE9 = true;
        }
    </script>
    <script src="/js/ext/ext-all2.js" type="text/javascript"></script>
    <script src="/js/ext/ux/TreeCheckNodeUI.js"></script>
    <script type="text/javascript">
        var viewport;
        var rootNode;
        var authData, authList;
        var optype;

        function onPgLoad() {
            optype = $.getQueryString({ ID: "type" });
            setPgUI();
        }

        function setPgUI() {

            Array.prototype.contains = function (obj) {
                var i = this.length;
                while (i--) {
                    if (this[i] == obj) {
                        return true;
                    }
                }
                return false;
            }

            authData = AimState["DtList"];
            authList = AimState["AtList"] || [];

            // 工具栏
            var tlBar = new Ext.Toolbar({
                items: [{
                    text: '保存',
                    iconCls: 'aim-icon-save',
                    handler: function () {
                        saveChanges();
                    }
                }]
            });

            // 工具标题栏
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [{ hidden: true }]
            });

            var tree = new Ext.tree.TreePanel({
                id: 'tree',
                region: 'center',
                expanded: true,
                border: false,
                tbar: titPanel,
                width: 230,
                height: 250,
                autoScroll: true,
                animate: true,
                checkModel: 'cascade',
                containerScroll: true,
                lines: true, //节点之间连接的横竖线
                rootVisible: false, //是否显示根节点
                loader: new Ext.tree.TreeLoader({ baseAttrs: { uiProvider: Ext.ux.TreeCheckNodeUI } })
            });

            tree.on('beforeload', function (node) {
                tree.loader.dataUrl = 'AuthTree.aspx?asyncreq=true&reqaction=querydescendant&id=' + node.attributes.id;
            });

            tree.on('load', function (node) {
                $.each(node.childNodes, function (i) {
                    var attrs = this.attributes;
                    if (authList.contains(attrs.id)) {
                        attrs.checked = true;
                    }
                });
            });

            rootNode = new Ext.tree.AsyncTreeNode({
                draggable: false,
                id: 'root',
                expanded: true,
                children: authData
            });

            tree.setRootNode(rootNode);

            // 页面视图
            viewport = new Ext.Viewport({
                layout: 'border',
                items: [tree]
            });

            rootNode.expand();
        }

        // 获取树下所有节点
        function getAllNodes(rnode, cnodes) {
            cnodes = cnodes || [];
            var nodes = rnode.childNodes || [];
            $.merge(cnodes, nodes);

            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];

                if (node.childNodes.length > 0) {
                    getAllNodes(node, cnodes);
                }
            }

            return cnodes;
        }

        function saveChanges() {
            var allNodes = getAllNodes(rootNode);
            var authAdded = []; // 所有新赋的权限
            var authAddedName = []; // 所有新赋的权限name
            var authRemoved = [];  // 所有移除的权限
            $.each(allNodes, function () {
                var node = this;
                var cAuthID = node.attributes.id;

                if (cAuthID && cAuthID != "") {
                    if (node.attributes.checked) {
                        if (!authList.contains(cAuthID)) {
                            authAdded.push(cAuthID);
                            if (node.leaf == 1) {
                                authAddedName.push(node.parentNode.text + '-' + node.attributes.text);
                            }
                            else {
                                authAddedName.push(node.attributes.text);
                            }
                        }
                    } else {
                        if (authList.contains(cAuthID)) {
                            authRemoved.push(cAuthID);
                        }
                    }
                }
            });

            if (authAdded.length == 0 && authRemoved.length == 0)
                return;

            jQuery.ajaxExec("savechanges", {
                added: authAdded.join(','),
                removed: authRemoved.join(','),
                addedName: authAddedName.join(','),
                roleId: $.getQueryString({ ID: "roleId" })
            }, function () {
                alert("保存成功！")
                window.location.reload();
            });
        }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
