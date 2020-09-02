<%@ Page Title="表详细信息" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="SYSTBLMMEdit.aspx.cs" Inherits="Aim.OnControl.Web.SYSTBLMMEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .aim-ui-td-data
        {
            width: 35%;
        }
        .aim-ui-td-caption
        {
            width: 15%;
        }
        fieldset
        {
            margin: 5px;
            width: 100%;
            padding: 1px;
            text-align: left;
        }
        fieldset legend
        {
            color: #000;
            font-size: 12px;
            font-weight: bold;
        }
        .body
        {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">

        function onPgLoad() {
            setPgUI();
            gridInit();
        }

        function setPgUI() {
            if (pgOperation == "c" || pgOperation == "cs") {
                $("#CreateName").val(AimState.UserInfo.Name);
                $("#CreateTime").val(jQuery.dateOnly(AimState.SystemInfo.Date));
            }

            //绑定按钮验证
            FormValidationBind('btnSubmit', SuccessSubmit);

            $("#btnCancel").click(function () {
                window.close();
            });
        }

        function gridInit() {

            tlBar_dept = new Ext.Toolbar({
                // renderTo: 'Access_bar',
                items: [
                { text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function () {
                        openOrgWin("gridAccess");
                    }
                },
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function () {
                       var recs = gridAccess.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               gridAccess.getStore().remove(recs[i]);
                           }
                       }

                   }
               }
]
            });

            myData1 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || ""
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData1,
                fields: [
            { name: 'ID' },
			{ name: 'REFTBLKEY' },
			{ name: 'REFTBLCODE' },
			{ name: 'CLNCODE' },
			{ name: 'CLNNAME' },
			{ name: 'CLNCOMMENT' },
			{ name: 'CLNDATATYPE' },
			{ name: 'CLNCREATETIME' },
			{ name: 'CLNCREATEUSR' },
			{ name: 'CLNMODIFYTIME' },
			{ name: 'CLNMODIFYUSR' },
			{ name: 'EXT1' },
			{ name: 'EXT2' },
			{ name: 'CREATEID' },
			{ name: 'CREATENAME' },
			{ name: 'CREATETIME' }
                  ]
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                store: store,
                //clicksToEdit: 2,
                height: 230,
                renderTo: 'gridDiv',
                autoExpandColumn: 'CLNNAME',
                columns: [
                     new Ext.ux.grid.AimRowNumberer(),
                     new Ext.grid.MultiSelectionModel(),
                     { id: 'ID', header: "ID", dataIndex: 'ID', hidden: true },
                     { id: 'CLNCODE', header: "字段", dataIndex: 'CLNCODE', width: 100 },
                     { id: 'CLNDATATYPE', header: "数据类型", dataIndex: 'CLNDATATYPE', width: 100 },
                     { id: 'CLNNAME', header: "字段名称", dataIndex: 'CLNNAME', width: 100 },
                     { id: 'CLNCREATEUSR', header: "创建用户", dataIndex: 'CLNCREATEUSR', width: 100 },
                     { id: 'CLNCREATETIME', header: "创建时间", dataIndex: 'CLNCREATETIME', width: 100 },
                     { id: 'CLNCOMMENT', header: "说明", dataIndex: 'CLNCOMMENT', width: 100 }
                  ]
            });
        }

        //验证成功执行保存方法
        function SuccessSubmit() {
            AimFrm.submit(pgAction, {}, null, SubFinish);
        }

        function SubFinish(args) {
            RefreshClose();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            数据表</h1>
    </div>
    <div id="editDiv" align="center">
        <fieldset>
            <legend>基本信息</legend>
            <table class="aim-ui-table-edit">
                <tbody>
                    <tr style="display: none">
                        <td>
                            <input id="Id" name="Id" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">
                            表
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="TBLCODE" name="TBLCODE" class="validate[required]" />
                        </td>
                        <td class="aim-ui-td-caption">
                            表名称
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="TBLNAME" name="TBLNAME" class="validate[required]" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">
                            类型
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="TBLTYPE" name="TBLTYPE" readonly="readonly" />
                        </td>
                        <td class="aim-ui-td-caption">
                            容量
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="CAPATICY" name="CAPATICY" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">
                            创建账户
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="CREATENAME" name="CREATENAME" readonly="readonly" />
                        </td>
                        <td class="aim-ui-td-caption">
                            创建时间
                        </td>
                        <td class="aim-ui-td-data">
                            <input id="CREATETIME" name="CREATETIME" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">
                            说明
                        </td>
                        <td class="aim-ui-td-data" colspan="3">
                            <textarea id="TBLCOMMENT" name="TBLCOMMENT" rows="3" style="width: 93%"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </fieldset>
        <fieldset>
            <legend>字段信息</legend>
            <table style="width: 100%; table-layout: fixed">
                <tr>
                    <td style="width: 100%">
                        <div id="gridDiv">
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table class="aim-ui-table-edit">
            <tbody>
                <tr>
                    <td class="aim-ui-button-panel" colspan="4">
                        <a id="A1" class="aim-ui-button submit">保存</a> <a id="btnCancel" class="aim-ui-button cancel">
                            取消</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
