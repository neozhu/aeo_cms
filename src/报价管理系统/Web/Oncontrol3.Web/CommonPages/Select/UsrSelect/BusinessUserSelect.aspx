<%@ Page Title="跟进者选择" Language="C#" MasterPageFile="~/Masters/Mini/Base.Master" AutoEventWireup="true"
    CodeBehind="BusinessUserSelect.aspx.cs" Inherits="CRM.Web.BusinessUserSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            border: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        #header
        {
            background: url(/theme/default/images/public/block_1/titlebg2.png) left top no-repeat;
            display: block;
            font: 14px "Microsoft YaHei", Tahoma, Geneva, sans-serif;
        }

            #header h1
            {
                font-size: 14px;
                color: #fff;
                font-weight: normal;
                padding: 5px 10px;
                height: 22px !important;
            }

        .neter-box
        {
            height: 180px !important;
            width: 320px !important;
        }

        .neter-box-view
        {
            height: 155px !important;
            width: 310px !important;
        }

        #boxContent label
        {
            width: 100px !important;
        }
    </style>

    <script type="text/javascript">
        var grid;
        var seltype = 'multi';  // multi(多选), single(单选)
        var rtntype = 'string'; // string, json(json字符串), record(Ext DataRecord), array(数组)
        function onPgLoad() {

            rtntype = $.getQueryString({ ID: "rtntype", DefaultValue: "array" }).toLowerCase();
            seltype = $.getQueryString({ ID: "seltype", DefaultValue: "multi" }).toLowerCase();

            setPgUI();
        }

        function setPgUI() {
            var columns = [
              { type: "indexcolumn", width: 20, allowResize: false },
              { type: "checkcolumn" },
              { header: "姓名", field: "NAME", width: 100, sortable: true },
              { header: "工号", field: "WORKNO", width: 150, sortable: true },
               { header: "用户ID", field: "USERID", width: 150, sortable: true, visible:false }
            ];

            grid = new mini.ux.AimDataGrid({
                idProperty: "ID",
                dsname: "dt",
                columns: columns,
                schpanel: "schPanel",
                data: AimState["dt"],
                aimpager: 'aimpager',
                renderTo: "grid1"
            });

            grid.on("rowdblclick", function (e) {
                CloseWindow("ok");
            });
        }

        function doSearch(domid) {
            grid.reload({ schtype: "field", schdom: document.getElementById(domid) });
        }

        function getUsrData() {
            return GetUsers(rtntype);
        }

        function GetUsers(type) {
            switch (type) {
                case "record":
                    rtns = grid.getSelecteds();
                    break;
                case "array":
                    rtns = grid.getSelecteds();
                    break;
                case "json":
                case "string":
                default:
                    rtns = GetUserString();
                    break;
            }

            return rtns;
        }
        function GetUserString() {
            var strjson = {};
            var data = grid.getData();
            for (var key in data) {
                if (!strjson[key]) {
                    strjson[key] = tdata[key]
                } else {
                    if (tdata[key]) {
                        strjson[key] += "," + tdata[key].toString();
                    }
                }
            }
            return strjson;
        }

        function onConfirm() {
            CloseWindow("ok");
        }
        function onClear() {
            CloseWindow("clear");
        }
        function onCancel() {
            CloseWindow("cancel");
        }


        function CloseWindow(action) {
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="schPanel" style="padding: 5px 10px;">
        <span>姓名：</span><input type="text" id="USERNAME" qryopts="{ mode: 'Like', field: 'USERNAME' }"
            aimgrp="defgrp" />
        <span>工号：</span><input type="text" id="WORKNO" qryopts="{ mode: 'Like', field: 'WORKNO' }"
            aimgrp="defgrp" />
        <input type="button" value="查找" onclick="doSearch('WORKNO')" />
    </div>
    <div id="grid1" class="mini-fit">
    </div>
    <div id="aimpager" class="aim-pager">
    </div>
    <div class="aim-from-toolbar">
        <table width="100%">
            <tbody>
                <tr>
                    <td colspan="6" style="text-align: center">
                        <a class="mini-button" id="btnConfirm" onclick="onConfirm">确认</a> <a class="mini-button"
                            id="btnClear" onclick="onClear">清空</a> <a class="mini-button" id="btnCancel" onclick="onCancel">取消</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
