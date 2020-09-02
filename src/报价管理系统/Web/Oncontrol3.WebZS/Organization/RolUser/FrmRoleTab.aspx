<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="FrmRoleTab.aspx.cs" Inherits="Aim.AM.Web.FrmRoleTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">

        var mdls, tabPanel;

        function onPgLoad() {
            mdls = [{ id: 'anqx', Name: "按钮权限", Url: "AuthTree.aspx?roleId=" + $.getQueryString({ ID: "roleId" }) }
            //, { id: 'cxqx', Name: "查询权限", Url: "SearchAuthTree.aspx?roleId=" + $.getQueryString({ ID: "roleId" }) }
            ];

            setPgUI();
        }

        function setPgUI() {
            var tabArr = new Array();
            var i = 0;
            var FrameHtml = "";
            // 构建tab标签
            $.each(mdls, function() {
                var tab = {
                    title: this["Name"],
                    margins: '0 0 0 0',
                    html: '<iframe width="100%" height="100%" id="frameContent" src="' + this["Url"] + '" name="frameContent" frameborder="0"></iframe>'
                }
                tabArr.push(tab);
            });

            tabPanel = new Ext.ux.AimTabPanel({
                enableTabScroll: true,
                border: true,
                region: 'center',
                margins: '-1 0 0 0',
                activeTab: 0,
                width: document.body.offsetWidth - 5,
                height: 10,
                items: tabArr
            });

            var viewport = new Ext.ux.AimViewport({
                items: [tabPanel]
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
