<%@ Page Title="采购流程" Language="C#" MasterPageFile="~/Masters/Ext/formpage.master"
    AutoEventWireup="true" CodeBehind="ProcTrack.aspx.cs" Inherits="Aim.Portal.Web.EPC.Procurement.ProcTrack" %>

<%@ OutputCache Duration="1" VaryByParam="None" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/plug-ins/jquery.corner.js" type="text/javascript"></script>

    <style type="text/css">
        .thumb
        {
            background-color: #dddddd;
            padding: 4px;
            text-align: center;
        }
        .thumb-activated
        {
            background-color: #33dd33;
            padding: 4px;
            text-align: center;
        }
        .thumb-separater
        {
            float: left;
            padding: 2px;
            margin-left: 5px;
            margin-right: 5px;
            vertical-align: middle;
        }
        .thumb-wrap-out
        {
            float: left;
            width: 80px;
            margin-right: 0;
            padding: 0px; /*background-color:#8DB2E3;*/
        }
        .thumb-wrap
        {
            font-size: 13px;
            font-weight: bold;
        }
        .span_track_link
        {
            cursor: hand;
        }
    </style>

    <script type="text/javascript">
        var phase, tracktype, PhaseData = {};
        var store, trackPanel, dataView, SubPortalWin;
        var PhaseData;

        function onPgLoad() {
            PhaseData = AimState["FlowEnum"];
            phase = $.getQueryString({ ID: 'phase', DefaultValue: AimState["FlowEnum"][0].Value });

            SubPortalWin = window
            /*while (SubPortalWin.parent) {
            SubPortalWin = SubPortalWin.parent;
            if (SubPortalWin.parent.FireItemMenuClk) {
            SubPortalWin = SubPortalWin.parent;
            break;
            }
            }*/

            setPgUI();

            $('.span_track_link').mouseover(function() {
                $(this.parentNode).attr('orgbgcolor', $(this.parentNode).css('background-color'));
                $(this.parentNode).css('background-color', 'green');
            });

            $('.span_track_link').mouseout(function() {
                $(this.parentNode).css('background-color', $(this.parentNode).attr('orgbgcolor'));
            });

            $('.span_track_link').click(function() {
                var code = $(this).attr('code');

                if (SubPortalWin && SubPortalWin.FireItemMenuClk) {
                    SubPortalWin.FireItemMenuClk(code);
                }
            });
        }

        function OnTrackLinkClick() {

        }

        function setPgUI() {
            myData = {
                total: AimState["FlowEnum"].length,
                records: AimState["FlowEnum"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'FlowEnum',
                idProperty: 'EnumerationID',
                data: myData,
                fields: [
			{ name: 'EnumerationID' },
			{ name: 'Code' },
			{ name: 'Name' },
			{ name: 'Value' },
			{ name: 'CreatedDate' }
			]
            });

            var dtCount = store.getRange().length;

            var tpl = new Ext.XTemplate(
		'<tpl for=".">',
		    '<div class="thumb-wrap-out">',
            '<div class="thumb-wrap">',
            '<tpl if="this.isActivated(Value)">',
		        '<div class="thumb-activated">',
		    '</tpl>',
            '<tpl if="!this.isActivated(Value)">',
		        '<div class="thumb">',
		    '</tpl>',
		    '<span class="span_track_link" code="{Value}">{#}.{Name}</span></div>',
		    '</div>',
		    '</div>',
            '<tpl if="!this.isLast([xindex][0])">',
		        '<div class="thumb-separater"><img src="/images/shared/arrow_right1.png" /></div>',
		    '</tpl>',
        '</tpl>',
        '<div class="x-clear"></div>', {
            isActivated: function(val) {
                return phase.equals(val);
            },
            isLast: function(idx) {
                return idx >= dtCount;
            }
        }
	);
            dataView = new Ext.DataView({
                store: store,
                tpl: tpl,
                autoHeight: true,
                overClass: 'x-view-over',
                itemSelector: 'div.thumb-wrap'
            });

            trackPanel = new Ext.ux.AimPanel({
                region: 'center',
                border: false,
                bodyStyle: 'background-color: #DFE8F6; margin:5px;',
                items: dataView
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [{ xtype: 'box', region: 'north', applyTo: 'header', height: 30 }, trackPanel]
            });

            //$('.thumb-wrap').corner("round 5px");
            $('.thumb-wrap-out').corner("round 5px")
        }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display: none;">
        <h1>
            流程跟踪</h1>
    </div>
</asp:Content>
