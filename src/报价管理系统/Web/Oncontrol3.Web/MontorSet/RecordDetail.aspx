<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/formpage.master" AutoEventWireup="true"
    CodeBehind="RecordDetail.aspx.cs" Inherits="Aim.OnControl.Web.MontorSet.RecordDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="editDiv" align="center">
        <table class="aim-ui-table-edit">
            <tbody>
                <tr style="display: none">
                    <td>
                        <input id="ID" name="ID" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        操作人
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="CREATENAME" name="CREATENAME" />
                    </td>
                    <td class="aim-ui-td-caption">
                        操作类型
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="ACTION" name="ACTION" style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        公司
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="COMPANYNAME" name="COMPANYNAME" />
                    </td>
                    <td class="aim-ui-td-caption">
                        部门
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DEPTNAME" name="DEPTNAME" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        表名
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="TABLEEN" name="TABLEEN" />
                    </td>
                    <td class="aim-ui-td-caption">
                        中文名
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="TABLECN" name="TABLECN" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        字段
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="COLUMNEN" name="COLUMNEN" />
                    </td>
                    <td class="aim-ui-td-caption">
                        字段名
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="COLUMNCN" name="COLUMNCN" style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        原字段值
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="OLDVALUE" name="OLDVALUE" style="width: 100%" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 10%">
                        新字段值
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="NEWVALUE" name="NEWVALUE" style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        详细内容
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="CONTENT" name="CONTENT" style="width: 100%; height: 80px"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        备注
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="REMARK" name="REMARK" rows="3" style="width: 100%; height: 80px"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
