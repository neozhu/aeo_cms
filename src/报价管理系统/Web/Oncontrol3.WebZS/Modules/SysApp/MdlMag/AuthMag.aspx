<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="AuthMag.aspx.cs" Inherits="Aim.Portal.Web.Modules.SysApp.MdlMag.AuthMag" %>

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
        var tabArr = [{ title: "权限列表", href: "AuthList.aspx" },
        { title: "人员权限", href: "AuthUser.aspx" },
        { title: "角色权限", href: "AuthRole.aspx" },
        { title: "组权限", href: "AuthGroup.aspx"}];
        var viewport = null;
        var tab = null;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            $.each(tabArr, function(i) {
                this.listeners = { activate: handleActivate };
                this.html = "<div style='display:none;'></div>";
            });

            tab = new Ext.TabPanel({
                //  title: '<img src="/images/titleicons.gif" style="margin-top:3px;float:left"/><span style="padding-left:10px;">系统权限管理</span>',
                region: 'north',
                margins: '32 0 0 0',
                activeTab: 0,
                // width: document.body.offsetWidth - 5,
                //                height: 29,
                items: tabArr
            });

            viewport = new Ext.Viewport({
                layout: 'border',
                items: [
                    tab, {
                        region: 'center',
                        cls: 'empty',
                        bodyStyle: 'background:#f1f1f1',
                        html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0" src="AuthList.aspx"></iframe>'
}]
            });
        }

        function handleActivate(tab) {
            if (document.getElementById("frameContent")) {
                document.getElementById("frameContent").src = tab.href;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            <img src="/images/titleicons.gif" style="margin-top: 3px; margin-right: 6px; float: left" />权限管理</h1>
    </div>
</asp:Content>
