<%@ Page Title="组织信息" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="OrgStructureEdit.aspx.cs" Inherits=" CRM.Web.Virtual.OrgStructureEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">

        var DataTypeEnum = { '1': '启用', '2': '停用' };

        var id = null;

        function onPgLoad() {
            id = $.getQueryString({ ID: 'id' });

            setPgUI();

            if ($.getQueryString({ ID: "op" }) == 'cs' || $.getQueryString({ ID: "op" }) == 'c') {
                $("#TYPE").val($.getQueryString({ ID: "type" }));
            }
        }

        function setPgUI() {
            //绑定按钮验证
            FormValidationBind('btnSubmit', SuccessSubmit);

            $("#btnCancel").click(function() {
                window.close();
            });
            if ($.getQueryString({ ID: "op" }) == 'cs') {
                $("[class*=aim-ui-button submit]").show();
            }
        }

        //验证成功执行保存方法
        function SuccessSubmit() {
            AimFrm.submit(pgAction, {}, null, SubFinish);
        }

        function SubFinish(args) {
            if (args.data.error) {
                alert(args.data.error);
            }
            else {
                Aim.PopUp.ReturnValue({ id: id, op: pgOperation });
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            虚拟组织维护</h1>
    </div>
    <div id="editDiv" align="center">
        <table class="aim-ui-table-edit">
            <tbody>
                <tr style="display: none">
                    <td>
                        <input id="PARENTID" name="PARENTID" />
                        <input id="TYPE" name="TYPE" />
                        <input id="GROUPID" name="GROUPID" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        名称
                    </td>
                    <td class="aim-ui-td-data">
                        <asp:Literal runat="server" ID="litname" />
                        <!--<input id="NAME" name="NAME" class="validate[required]" />
                        <select id='NAME' aimctrl='select' name='NAME' enum='VirtualRole' style='width: 100%;'
                            class='aim-input-select validate[required]'>
                        </select>-->
                    </td>
                    <td class="aim-ui-td-caption">
                        排序号
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="SORTINDEX" name="SORTINDEX" class="validate[custom[onlyInteger]]" value="10" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        描述
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="REMARK" name="REMARK" rows="5" style="width: 98%"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-button-panel" colspan="4">
                        <a id="btnSubmit" class="aim-ui-button submit">保存</a> <a id="btnCancel" class="aim-ui-button cancel">
                            取消</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
