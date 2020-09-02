<%@ Page Title="标题" Language="C#" MasterPageFile="~/Masters/Mini/formpage.Master" AutoEventWireup="true" CodeBehind="SysSubscribeEdit.aspx.cs" Inherits="CRM.Web.CommonPages.SysSubscribeEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            overflow-y: auto;
        }

        .aim-ui-td-caption
        {
            width: 15%;
        }

        .aim-ui-td-data
        {
            width: 35%;
        }
    </style>
    <script type="text/javascript">
        var subs = $.getQueryString({ ID: "subs" });
        var title = $.getQueryString({ ID: "title" });
        var url = $.getQueryString({ ID: "qururl" });

        var store, grid;
        var subdata;
        function onPgLoad() {
            subdata = $.getJsonObj(subs);
            //if (AimState["frmdata"] || AimState["frmdata"].ID) {
            //    subdata = $.getJsonObj(AimState["frmdata"].CONDITION);
            //}
            initGrid();
            setPgUI();
            initData();
        }

        function initData() {
            $("#TITLE").val($("#TITLE").val() || title);
            $("#LISTURL").val($("#LISTURL").val() || url);

            if ($("#ID").val()) {
                $("#btnCancelSub").show();
            }
        }

        function initGrid() {
            if (AimState["frmdata"]) {
                //绑定radio
                $("input[type=radio]").each(function () {
                    if (this.value == AimState["frmdata"][this.name]) {
                        this.checked = true;
                    }
                });

                $("input[type=checkbox]").each(function () {
                    if (AimState["frmdata"][this.name] == "on") {
                        this.checked = true;
                    }
                });

                dochangetype(AimState["frmdata"]["CRONFSPL"]);
                //changetype(AimState["frmdata"]["TRIGGERTYPE"])
            } else {
                $("#CRONFSPLd").attr("checked", true);
                dochangetype("天");

            }

            var columns = [
                        { type: "indexcolumn", width: 30, allowResize: false },
                        { header: "字段", field: "id", width: 150 },
                        { header: "文本名称", field: "label", width: "100%" },
                        { header: "值", field: "value", width: 150, editor: { type: "textbox", minValue: 0, maxValue: 200 } }
            ];
            grid = new mini.ux.AimDataGrid({
                idProperty: "id",
                dsname: "field",

                allowCellEdit: true,
                allowCellSelect: true,
                allowAlternating: true,

                columns: columns,
                data: subdata,
                renderTo: "grid1"
            });
        }

        function setPgUI() {
            //绑定按钮验证

            FormValidationBind('btnSubmit', SuccessSubmit);

            //if (AimState["frmdata"]) {
            //    //绑定radio
            //    $("input[type=radio]").each(function () {
            //        if (this.value == AimState["frmdata"][this.name]) {
            //            this.checked = true;
            //        }
            //    });

            //    $("input[type=checkbox]").each(function () {
            //        if (AimState["frmdata"][this.name] == "on") {
            //            this.checked = true;
            //        }
            //    });

            //    dochangetype(AimState["frmdata"]["CRONFSPL"]);
            //    changetype(AimState["frmdata"]["TASKMODE"]);
            //} else {
            //    $("#CRONFSPLd").attr("checked", true);
            //    dochangetype("天");

            //    $("#fldsimple").hide();
            //    //$("#fldcron").hide();
            //}

            $("#btnCancel").click(function () {
                window.CloseOwnerWindow();
            });

            $("#btnCancelSub").click(function () {
                $.ajaxExec("undo", { id: $("#ID").val() }, function () {
                    window.CloseOwnerWindow();
                });
            });
        }

        //验证成功执行保存方法
        function SuccessSubmit() {
            //if (!$("input[name='TASKMODE']:checked").val()) {
            //    alert("请选择定时器类型");
            //    return;
            //}
            if (!$("input[name='CRONMTTYPE']:checked").val()) {
                alert("请选择一天内发生频率");
                return;
            }
            if ($("input[name='CRONMTTYPE']:checked").val() == "一次" && !$("#MYMTZXYC").val()) {
                alert("请填写一天内发生时间");
                return;
            }
            if ($("input[name='CRONMTTYPE']:checked").val() == "周期" && !$("#MYMTZXDCM").val()) {
                alert("请填写一天内周期发生频率");
                return;
            }

            if ($("input[name='CRONFSPL']:checked").val() == "月" && !$("#MYZXTS").val()) {
                alert("请填写每月发生的天数");
                return;
            }

            if ($("input[name='CRONFSPL']:checked").val() == "周") {

                if ($("#flsWeek :checked").length == 0) {
                    alert("请选择每周发生的天数");
                    return;
                }
                //var count = 0;
                //for (var i = 1; i < 8; i++) {
                //    if ($("#CRONWEEK" + i).attr("checked") == true) {
                //        count++;
                //    }
                //}
                //if (count == 0) {
                //    alert("请选择每周发生的天数");
                //    return;
                //}
            }


            var fieldData = grid.getData();
            var ddt = grid.GetModifiedDataStringArr(fieldData);
            //pgAction = $("#ID").val() ? "update" : "create";
            AimFrm.submit(pgAction, { ddt: ddt }, null, SubFinish);
        }

        function SubFinish(args) {
            if (window.CloseOwnerWindow) {
                return window.CloseOwnerWindow("ok");
            }
            else {
                window.close();
            }
        }

        //出发类型变化(天、周、月)
        function dochangetype(val) {
            if (val == "天") {
                $("#flsWeek").hide();
                $("#flsMonth").hide();
            }
            else if (val == "周") {
                $("#flsWeek").show();
                $("#flsMonth").hide();
            }
            else if (val == "月") {
                $("#flsWeek").hide();
                $("#flsMonth").show();
            }
        }

        //发生频率变化
        function dochangefspl(val) {
            if (val == "一次") {
                $("#MYMTZXYC").attr("disabled", false);

                $("#MYMTZXDCM").attr("disabled", true).val("");
                $("#XZMTZXSJ").attr("disabled", true).attr("checked", false);
                $("#XZMTKSSJ").attr("disabled", true).val("");
                $("#XZMTJSSJ").attr("disabled", true).val("");
            }
            else {
                $("#MYMTZXYC").attr("disabled", true).val("");

                $("#MYMTZXDCM").attr("disabled", false);
                $("#XZMTZXSJ").attr("disabled", false);
                $("#XZMTKSSJ").attr("disabled", false);
                $("#XZMTJSSJ").attr("disabled", false);
            }
        }

        function xzyxq(obj) {
            if (obj.checked == true) {
                $("#YXQJS").attr("disabled", false);
            }
            else {
                $("#YXQJS").attr("disabled", true).val("");
            }
        }

        function changetype(val) {
            if (val == "SIMPLE") {
                $("#fldsimple").show();
                $("#fldcron").hide();
            }
            else if (val == "CRON") {
                $("#fldsimple").hide();
                $("#fldcron").show();
            } else {
                $("#fldsimple").hide();
                $("#fldcron").hide();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="editDiv">
        <fieldset>
            <legend>基本信息</legend>
            <table class="aim-ui-table-edit" id="condition">
                <tbody>
                    <tr style="display: none">
                        <td>
                            <input id="ID" name="ID" />
                            <input id="TASKMODE" name="TASKMODE" value="CRON" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">标题(邮件标题)
                        </td>
                        <td class="aim-ui-td-data" colspan="3">
                            <input id="TITLE" name="TITLE" class="validate[required]" style="width: 80%;" />
                        </td>

                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">订阅url
                        </td>
                        <td class="aim-ui-td-data" colspan="3">
                            <input id="LISTURL" name="LISTURL" class="validate[required]" readonly style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="aim-ui-td-caption">接收邮箱
                        </td>
                        <td class="aim-ui-td-data" colspan="3">
                            <input id="EMAIL" name="EMAIL" class="validate[required]" style="width: 80%;" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </fieldset>
        <fieldset>
            <legend>订阅条件</legend>
            <div id="grid1"></div>
        </fieldset>
        <%-- <fieldset>
            <legend>任务模式</legend>
            <div style="font-size: 12px;">
                <label>
                    <input type="radio" id="TRIGGERTYPE1" name="TASKMODE" value="SIMPLE" onclick="changetype(this.value)" />简单任务</label>
                <label>
                    <input type="radio" id="TRIGGERTYPE2" name="TASKMODE" value="CRON" onclick="changetype(this.value)" />复杂任务</label>
            </div>
        </fieldset>--%>

        <%-- <fieldset id="fldsimple">
            <legend>简单定时器</legend>
            <table class="aim-ui-table-edit" style="border-width: 0px;">
                <tr>
                    <td>间隔
                    <input type="text" id="SIMPJG" name="SIMPJG" style="width: 60px;" />
                        秒执行一次，执行
                    <input type="text" id="SIMPZXCS" name="SIMPZXCS" style="width: 50px;" />
                        次后停止
                    </td>
                </tr>
            </table>
        </fieldset>--%>
        <fieldset id="fldcron">
            <legend>订阅规则</legend>
            <fieldset>
                <legend>发生频率</legend>
                <label>
                    <input type="radio" id="CRONFSPLd" name="CRONFSPL" value="天" onclick="dochangetype(this.value)" />天</label>
                <label>
                    <input type="radio" id="CRONFSPlz" name="CRONFSPL" value="周" onclick="dochangetype(this.value)" />周</label>
                <label>
                    <input type="radio" id="CRONFSPly" name="CRONFSPL" value="月" onclick="dochangetype(this.value)" />月</label>
            </fieldset>
            <fieldset id="flsWeek">
                <legend>每周</legend>
                <label>
                    <input type="checkbox" id="CRONWEEK1" name="CRONWEEK1" chktype="single" />星期一</label><label>
                        <input type="checkbox" id="CRONWEEK2" name="CRONWEEK2" chktype="single" />星期二</label><label>
                            <input type="checkbox" id="CRONWEEK3" name="CRONWEEK3" chktype="single" />星期三</label><label>
                                <input type="checkbox" id="CRONWEEK4" name="CRONWEEK4" chktype="single" />星期四</label><label>
                                    <input type="checkbox" id="CRONWEEK5" name="CRONWEEK5" chktype="single" />星期五</label><label>
                                        <input type="checkbox" id="CRONWEEK6" name="CRONWEEK6" chktype="single" />星期六</label><label>
                                            <input type="checkbox" id="CRONWEEK7" name="CRONWEEK7" chktype="single" />星期日</label>
            </fieldset>
            <fieldset id="flsMonth">
                <legend>每月</legend>每月
            <input type="text" id="MYZXTS" name="MYZXTS" style="width: 300px;" />
                执行
            <label style="color: Red;">
                （文本框可以填 ',' '-'）</label>
            </fieldset>
            <fieldset>
                <legend>一天内</legend>
                <label>
                    <input type="radio" id="raomtfs1" name="CRONMTTYPE" value="一次" onclick="dochangefspl(this.value)" />
                    发生一次 时间
                <input type="text" id="MYMTZXYC" name="MYMTZXYC" style="width: 100px;" /></label><br />
                <label>
                    <input type="radio" id="raomtfs2" name="CRONMTTYPE" value="周期" onclick="dochangefspl(this.value)" />
                    周期发生</label>
                每
            <input type="text" id="MYMTZXDCM" name="MYMTZXDCM" style="width: 100px;" />
                秒执行一次
            <input type="checkbox" id="XZMTZXSJ" name="XZMTZXSJ" />执行时间 从
            <input type="text" id="XZMTKSSJ" name="XZMTKSSJ" style="width: 50px;" />
                时 至
            <input type="text" id="XZMTJSSJ" name="XZMTJSSJ" style="width: 50px;" />
                时
            </fieldset>
            <%--            <fieldset>
                <legend>有效期</legend>开始日期<input aimctrl="date" id="YXQKS" name="YXQKS" />
                <input type="checkbox" id="XZYXQJS" name="XZYXQJS" onclick="xzyxq(this)" />
                结束日期<input aimctrl="date" id="YXQJS" name="YXQJS" />
            </fieldset>--%>
        </fieldset>

        <table class="aim-ui-table-edit" style="border-width: 0px;">
            <tr>
                <td class="aim-ui-button-panel" style="border-width: 0px;">
                    <a id="btnCancelSub" class="aim-ui-button submit" style="display: none;">取消订阅</a>
                    <a id="btnSubmit" class="aim-ui-button submit">提交</a>
                    <a id="btnCancel" class="aim-ui-button cancel">取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


