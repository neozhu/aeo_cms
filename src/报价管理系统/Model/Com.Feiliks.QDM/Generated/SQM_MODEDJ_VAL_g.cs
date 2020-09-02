// Business class SQM_MODEDJ_VAL generated from SQM_MODEDJ_VAL
// Creator: rw
// Created Date: [2018-07-14]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_MODEDJ_VAL")]
	public partial class SQM_MODEDJ_VAL : EntityBase<SQM_MODEDJ_VAL>
	{
        #region Property_Names
        public static string Prop_COSTRID = "COSTRID";
        public static string Prop_GDZRID = "GDZRID";
		public static string Prop_CALCTYPE = "CALCTYPE";
		public static string Prop_COLUMN46 = "COLUMN46";
		public static string Prop_COLUMN46C = "COLUMN46C";
		public static string Prop_COLUMN47 = "COLUMN47";
		public static string Prop_COLUMN47C = "COLUMN47C";
		public static string Prop_COLUMN48 = "COLUMN48";
		public static string Prop_COLUMN48C = "COLUMN48C";
		public static string Prop_COLUMN49 = "COLUMN49";
		public static string Prop_COLUMN49C = "COLUMN49C";
		public static string Prop_COLUMN50 = "COLUMN50";
		public static string Prop_COLUMN50C = "COLUMN50C";
		public static string Prop_DJSTATUS = "DJSTATUS";
		public static string Prop_CALCCODE = "CALCCODE";
		public static string Prop_CALCNAME = "CALCNAME";
		public static string Prop_BJRID = "BJRID";
		public static string Prop_IFBJITEM = "IFBJITEM";
		public static string Prop_DJFSRID = "DJFSRID";
		public static string Prop_MIN = "MIN";
		public static string Prop_BJPRICE = "BJPRICE";
		public static string Prop_SPRICE = "SPRICE";
		public static string Prop_WDJBJRID = "WDJBJRID";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_SORD = "SORD";
		public static string Prop_CALCUNIT = "CALCUNIT";
		public static string Prop_CURRENCY = "CURRENCY";
		public static string Prop_PURPRICE = "PURPRICE";
		public static string Prop_COSTPRICE = "COSTPRICE";
		public static string Prop_MAXPRICE = "MAXPRICE";
		public static string Prop_MINPRICE = "MINPRICE";
		public static string Prop_GUIDEPRICE = "GUIDEPRICE";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_FEECALCID = "FEECALCID";
		public static string Prop_COLUMN1 = "COLUMN1";
		public static string Prop_COLUMN1C = "COLUMN1C";
		public static string Prop_COLUMN2 = "COLUMN2";
		public static string Prop_COLUMN2C = "COLUMN2C";
		public static string Prop_COLUMN3 = "COLUMN3";
		public static string Prop_COLUMN3C = "COLUMN3C";
		public static string Prop_COLUMN4 = "COLUMN4";
		public static string Prop_COLUMN4C = "COLUMN4C";
		public static string Prop_COLUMN5 = "COLUMN5";
		public static string Prop_COLUMN5C = "COLUMN5C";
		public static string Prop_COLUMN6 = "COLUMN6";
		public static string Prop_COLUMN6C = "COLUMN6C";
		public static string Prop_COLUMN7 = "COLUMN7";
		public static string Prop_COLUMN7C = "COLUMN7C";
		public static string Prop_COLUMN8 = "COLUMN8";
		public static string Prop_COLUMN8C = "COLUMN8C";
		public static string Prop_COLUMN9 = "COLUMN9";
		public static string Prop_COLUMN9C = "COLUMN9C";
		public static string Prop_COLUMN10 = "COLUMN10";
		public static string Prop_COLUMN10C = "COLUMN10C";
		public static string Prop_COLUMN11 = "COLUMN11";
		public static string Prop_COLUMN11C = "COLUMN11C";
		public static string Prop_COLUMN12 = "COLUMN12";
		public static string Prop_COLUMN12C = "COLUMN12C";
		public static string Prop_COLUMN13 = "COLUMN13";
		public static string Prop_COLUMN13C = "COLUMN13C";
		public static string Prop_COLUMN14 = "COLUMN14";
		public static string Prop_COLUMN14C = "COLUMN14C";
		public static string Prop_COLUMN15 = "COLUMN15";
		public static string Prop_COLUMN15C = "COLUMN15C";
		public static string Prop_COLUMN16 = "COLUMN16";
		public static string Prop_COLUMN16C = "COLUMN16C";
		public static string Prop_COLUMN17 = "COLUMN17";
		public static string Prop_COLUMN17C = "COLUMN17C";
		public static string Prop_COLUMN18 = "COLUMN18";
		public static string Prop_COLUMN18C = "COLUMN18C";
		public static string Prop_COLUMN19 = "COLUMN19";
		public static string Prop_COLUMN19C = "COLUMN19C";
		public static string Prop_COLUMN20 = "COLUMN20";
		public static string Prop_COLUMN20C = "COLUMN20C";
		public static string Prop_COLUMN21 = "COLUMN21";
		public static string Prop_COLUMN21C = "COLUMN21C";
		public static string Prop_COLUMN22 = "COLUMN22";
		public static string Prop_COLUMN22C = "COLUMN22C";
		public static string Prop_COLUMN23 = "COLUMN23";
		public static string Prop_COLUMN23C = "COLUMN23C";
		public static string Prop_COLUMN24 = "COLUMN24";
		public static string Prop_COLUMN24C = "COLUMN24C";
		public static string Prop_COLUMN25 = "COLUMN25";
		public static string Prop_COLUMN25C = "COLUMN25C";
		public static string Prop_COLUMN26 = "COLUMN26";
		public static string Prop_COLUMN26C = "COLUMN26C";
		public static string Prop_COLUMN27 = "COLUMN27";
		public static string Prop_COLUMN27C = "COLUMN27C";
		public static string Prop_COLUMN28 = "COLUMN28";
		public static string Prop_COLUMN28C = "COLUMN28C";
		public static string Prop_COLUMN29 = "COLUMN29";
		public static string Prop_COLUMN29C = "COLUMN29C";
		public static string Prop_COLUMN30 = "COLUMN30";
		public static string Prop_COLUMN30C = "COLUMN30C";
		public static string Prop_COLUMN31 = "COLUMN31";
		public static string Prop_COLUMN31C = "COLUMN31C";
		public static string Prop_COLUMN32 = "COLUMN32";
		public static string Prop_COLUMN32C = "COLUMN32C";
		public static string Prop_COLUMN33 = "COLUMN33";
		public static string Prop_COLUMN33C = "COLUMN33C";
		public static string Prop_COLUMN34 = "COLUMN34";
		public static string Prop_COLUMN34C = "COLUMN34C";
		public static string Prop_COLUMN35 = "COLUMN35";
		public static string Prop_COLUMN35C = "COLUMN35C";
		public static string Prop_COLUMN36 = "COLUMN36";
		public static string Prop_COLUMN36C = "COLUMN36C";
		public static string Prop_COLUMN37 = "COLUMN37";
		public static string Prop_COLUMN37C = "COLUMN37C";
		public static string Prop_COLUMN38 = "COLUMN38";
		public static string Prop_COLUMN38C = "COLUMN38C";
		public static string Prop_COLUMN39 = "COLUMN39";
		public static string Prop_COLUMN39C = "COLUMN39C";
		public static string Prop_COLUMN40 = "COLUMN40";
		public static string Prop_COLUMN40C = "COLUMN40C";
		public static string Prop_COLUMN41 = "COLUMN41";
		public static string Prop_COLUMN41C = "COLUMN41C";
		public static string Prop_COLUMN42 = "COLUMN42";
		public static string Prop_COLUMN42C = "COLUMN42C";
		public static string Prop_COLUMN43 = "COLUMN43";
		public static string Prop_COLUMN43C = "COLUMN43C";
		public static string Prop_COLUMN44 = "COLUMN44";
		public static string Prop_COLUMN44C = "COLUMN44C";
		public static string Prop_COLUMN45 = "COLUMN45";
		public static string Prop_COLUMN45C = "COLUMN45C";
        public static string Prop_CUSTOMERSNO = "CUSTOMERSNO";//指定客户

        #endregion

        #region Private_Variables
        private string _cOSTRID;
        private string _gDZRID;
		private string _cALCTYPE;
		private string _cOLUMN46;
		private string _cOLUMN46C;
		private string _cOLUMN47;
		private string _cOLUMN47C;
		private string _cOLUMN48;
		private string _cOLUMN48C;
		private string _cOLUMN49;
		private string _cOLUMN49C;
		private string _cOLUMN50;
		private string _cOLUMN50C;
		private string _dJSTATUS;
		private string _cALCCODE;
		private string _cALCNAME;
		private string _bJRID;
		private string _iFBJITEM;
		private string _dJFSRID;
		private System.Decimal? _mIN;
		private System.Decimal? _bJPRICE;
		private System.Decimal? _sPRICE;
		private string _wDJBJRID;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private System.Decimal? _sORD;
		private string _cALCUNIT;
		private string _cURRENCY;
		private System.Decimal? _pURPRICE;
		private System.Decimal? _cOSTPRICE;
		private System.Decimal? _mAXPRICE;
		private System.Decimal? _mINPRICE;
		private System.Decimal? _gUIDEPRICE;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _fEECALCID;
		private string _cOLUMN1;
		private string _cOLUMN1C;
		private string _cOLUMN2;
		private string _cOLUMN2C;
		private string _cOLUMN3;
		private string _cOLUMN3C;
		private string _cOLUMN4;
		private string _cOLUMN4C;
		private string _cOLUMN5;
		private string _cOLUMN5C;
		private string _cOLUMN6;
		private string _cOLUMN6C;
		private string _cOLUMN7;
		private string _cOLUMN7C;
		private string _cOLUMN8;
		private string _cOLUMN8C;
		private string _cOLUMN9;
		private string _cOLUMN9C;
		private string _cOLUMN10;
		private string _cOLUMN10C;
		private string _cOLUMN11;
		private string _cOLUMN11C;
		private string _cOLUMN12;
		private string _cOLUMN12C;
		private string _cOLUMN13;
		private string _cOLUMN13C;
		private string _cOLUMN14;
		private string _cOLUMN14C;
		private string _cOLUMN15;
		private string _cOLUMN15C;
		private string _cOLUMN16;
		private string _cOLUMN16C;
		private string _cOLUMN17;
		private string _cOLUMN17C;
		private string _cOLUMN18;
		private string _cOLUMN18C;
		private string _cOLUMN19;
		private string _cOLUMN19C;
		private string _cOLUMN20;
		private string _cOLUMN20C;
		private string _cOLUMN21;
		private string _cOLUMN21C;
		private string _cOLUMN22;
		private string _cOLUMN22C;
		private string _cOLUMN23;
		private string _cOLUMN23C;
		private string _cOLUMN24;
		private string _cOLUMN24C;
		private string _cOLUMN25;
		private string _cOLUMN25C;
		private string _cOLUMN26;
		private string _cOLUMN26C;
		private string _cOLUMN27;
		private string _cOLUMN27C;
		private string _cOLUMN28;
		private string _cOLUMN28C;
		private string _cOLUMN29;
		private string _cOLUMN29C;
		private string _cOLUMN30;
		private string _cOLUMN30C;
		private string _cOLUMN31;
		private string _cOLUMN31C;
		private string _cOLUMN32;
		private string _cOLUMN32C;
		private string _cOLUMN33;
		private string _cOLUMN33C;
		private string _cOLUMN34;
		private string _cOLUMN34C;
		private string _cOLUMN35;
		private string _cOLUMN35C;
		private string _cOLUMN36;
		private string _cOLUMN36C;
		private string _cOLUMN37;
		private string _cOLUMN37C;
		private string _cOLUMN38;
		private string _cOLUMN38C;
		private string _cOLUMN39;
		private string _cOLUMN39C;
		private string _cOLUMN40;
		private string _cOLUMN40C;
		private string _cOLUMN41;
		private string _cOLUMN41C;
		private string _cOLUMN42;
		private string _cOLUMN42C;
		private string _cOLUMN43;
		private string _cOLUMN43C;
		private string _cOLUMN44;
		private string _cOLUMN44C;
		private string _cOLUMN45;
		private string _cOLUMN45C;
        private string _cUSTOMERSNO;

		#endregion

		#region Constructors

		public SQM_MODEDJ_VAL()
		{
		}

		public SQM_MODEDJ_VAL(
            string p_cOSTRID,
            string p_gDZRID,
			string p_cALCTYPE,
			string p_cOLUMN46,
			string p_cOLUMN46C,
			string p_cOLUMN47,
			string p_cOLUMN47C,
			string p_cOLUMN48,
			string p_cOLUMN48C,
			string p_cOLUMN49,
			string p_cOLUMN49C,
			string p_cOLUMN50,
			string p_cOLUMN50C,
			string p_dJSTATUS,
			string p_cALCCODE,
			string p_cALCNAME,
			string p_bJRID,
			string p_iFBJITEM,
			string p_dJFSRID,
			System.Decimal? p_mIN,
			System.Decimal? p_bJPRICE,
			System.Decimal? p_sPRICE,
			string p_wDJBJRID,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			System.Decimal? p_sORD,
			string p_cALCUNIT,
			string p_cURRENCY,
			System.Decimal? p_pURPRICE,
			System.Decimal? p_cOSTPRICE,
			System.Decimal? p_mAXPRICE,
			System.Decimal? p_mINPRICE,
			System.Decimal? p_gUIDEPRICE,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_fEECALCID,
			string p_cOLUMN1,
			string p_cOLUMN1C,
			string p_cOLUMN2,
			string p_cOLUMN2C,
			string p_cOLUMN3,
			string p_cOLUMN3C,
			string p_cOLUMN4,
			string p_cOLUMN4C,
			string p_cOLUMN5,
			string p_cOLUMN5C,
			string p_cOLUMN6,
			string p_cOLUMN6C,
			string p_cOLUMN7,
			string p_cOLUMN7C,
			string p_cOLUMN8,
			string p_cOLUMN8C,
			string p_cOLUMN9,
			string p_cOLUMN9C,
			string p_cOLUMN10,
			string p_cOLUMN10C,
			string p_cOLUMN11,
			string p_cOLUMN11C,
			string p_cOLUMN12,
			string p_cOLUMN12C,
			string p_cOLUMN13,
			string p_cOLUMN13C,
			string p_cOLUMN14,
			string p_cOLUMN14C,
			string p_cOLUMN15,
			string p_cOLUMN15C,
			string p_cOLUMN16,
			string p_cOLUMN16C,
			string p_cOLUMN17,
			string p_cOLUMN17C,
			string p_cOLUMN18,
			string p_cOLUMN18C,
			string p_cOLUMN19,
			string p_cOLUMN19C,
			string p_cOLUMN20,
			string p_cOLUMN20C,
			string p_cOLUMN21,
			string p_cOLUMN21C,
			string p_cOLUMN22,
			string p_cOLUMN22C,
			string p_cOLUMN23,
			string p_cOLUMN23C,
			string p_cOLUMN24,
			string p_cOLUMN24C,
			string p_cOLUMN25,
			string p_cOLUMN25C,
			string p_cOLUMN26,
			string p_cOLUMN26C,
			string p_cOLUMN27,
			string p_cOLUMN27C,
			string p_cOLUMN28,
			string p_cOLUMN28C,
			string p_cOLUMN29,
			string p_cOLUMN29C,
			string p_cOLUMN30,
			string p_cOLUMN30C,
			string p_cOLUMN31,
			string p_cOLUMN31C,
			string p_cOLUMN32,
			string p_cOLUMN32C,
			string p_cOLUMN33,
			string p_cOLUMN33C,
			string p_cOLUMN34,
			string p_cOLUMN34C,
			string p_cOLUMN35,
			string p_cOLUMN35C,
			string p_cOLUMN36,
			string p_cOLUMN36C,
			string p_cOLUMN37,
			string p_cOLUMN37C,
			string p_cOLUMN38,
			string p_cOLUMN38C,
			string p_cOLUMN39,
			string p_cOLUMN39C,
			string p_cOLUMN40,
			string p_cOLUMN40C,
			string p_cOLUMN41,
			string p_cOLUMN41C,
			string p_cOLUMN42,
			string p_cOLUMN42C,
			string p_cOLUMN43,
			string p_cOLUMN43C,
			string p_cOLUMN44,
			string p_cOLUMN44C,
			string p_cOLUMN45,
            string p_cUSTOMERSNO,
			string p_cOLUMN45C)
		{
            _cOSTRID = p_cOSTRID;
			_gDZRID = p_gDZRID;
			_cALCTYPE = p_cALCTYPE;
			_cOLUMN46 = p_cOLUMN46;
			_cOLUMN46C = p_cOLUMN46C;
			_cOLUMN47 = p_cOLUMN47;
			_cOLUMN47C = p_cOLUMN47C;
			_cOLUMN48 = p_cOLUMN48;
			_cOLUMN48C = p_cOLUMN48C;
			_cOLUMN49 = p_cOLUMN49;
			_cOLUMN49C = p_cOLUMN49C;
			_cOLUMN50 = p_cOLUMN50;
			_cOLUMN50C = p_cOLUMN50C;
			_dJSTATUS = p_dJSTATUS;
			_cALCCODE = p_cALCCODE;
			_cALCNAME = p_cALCNAME;
			_bJRID = p_bJRID;
			_iFBJITEM = p_iFBJITEM;
			_dJFSRID = p_dJFSRID;
			_mIN = p_mIN;
			_bJPRICE = p_bJPRICE;
			_sPRICE = p_sPRICE;
			_wDJBJRID = p_wDJBJRID;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_sORD = p_sORD;
			_cALCUNIT = p_cALCUNIT;
			_cURRENCY = p_cURRENCY;
			_pURPRICE = p_pURPRICE;
			_cOSTPRICE = p_cOSTPRICE;
			_mAXPRICE = p_mAXPRICE;
			_mINPRICE = p_mINPRICE;
			_gUIDEPRICE = p_gUIDEPRICE;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_fEECALCID = p_fEECALCID;
			_cOLUMN1 = p_cOLUMN1;
			_cOLUMN1C = p_cOLUMN1C;
			_cOLUMN2 = p_cOLUMN2;
			_cOLUMN2C = p_cOLUMN2C;
			_cOLUMN3 = p_cOLUMN3;
			_cOLUMN3C = p_cOLUMN3C;
			_cOLUMN4 = p_cOLUMN4;
			_cOLUMN4C = p_cOLUMN4C;
			_cOLUMN5 = p_cOLUMN5;
			_cOLUMN5C = p_cOLUMN5C;
			_cOLUMN6 = p_cOLUMN6;
			_cOLUMN6C = p_cOLUMN6C;
			_cOLUMN7 = p_cOLUMN7;
			_cOLUMN7C = p_cOLUMN7C;
			_cOLUMN8 = p_cOLUMN8;
			_cOLUMN8C = p_cOLUMN8C;
			_cOLUMN9 = p_cOLUMN9;
			_cOLUMN9C = p_cOLUMN9C;
			_cOLUMN10 = p_cOLUMN10;
			_cOLUMN10C = p_cOLUMN10C;
			_cOLUMN11 = p_cOLUMN11;
			_cOLUMN11C = p_cOLUMN11C;
			_cOLUMN12 = p_cOLUMN12;
			_cOLUMN12C = p_cOLUMN12C;
			_cOLUMN13 = p_cOLUMN13;
			_cOLUMN13C = p_cOLUMN13C;
			_cOLUMN14 = p_cOLUMN14;
			_cOLUMN14C = p_cOLUMN14C;
			_cOLUMN15 = p_cOLUMN15;
			_cOLUMN15C = p_cOLUMN15C;
			_cOLUMN16 = p_cOLUMN16;
			_cOLUMN16C = p_cOLUMN16C;
			_cOLUMN17 = p_cOLUMN17;
			_cOLUMN17C = p_cOLUMN17C;
			_cOLUMN18 = p_cOLUMN18;
			_cOLUMN18C = p_cOLUMN18C;
			_cOLUMN19 = p_cOLUMN19;
			_cOLUMN19C = p_cOLUMN19C;
			_cOLUMN20 = p_cOLUMN20;
			_cOLUMN20C = p_cOLUMN20C;
			_cOLUMN21 = p_cOLUMN21;
			_cOLUMN21C = p_cOLUMN21C;
			_cOLUMN22 = p_cOLUMN22;
			_cOLUMN22C = p_cOLUMN22C;
			_cOLUMN23 = p_cOLUMN23;
			_cOLUMN23C = p_cOLUMN23C;
			_cOLUMN24 = p_cOLUMN24;
			_cOLUMN24C = p_cOLUMN24C;
			_cOLUMN25 = p_cOLUMN25;
			_cOLUMN25C = p_cOLUMN25C;
			_cOLUMN26 = p_cOLUMN26;
			_cOLUMN26C = p_cOLUMN26C;
			_cOLUMN27 = p_cOLUMN27;
			_cOLUMN27C = p_cOLUMN27C;
			_cOLUMN28 = p_cOLUMN28;
			_cOLUMN28C = p_cOLUMN28C;
			_cOLUMN29 = p_cOLUMN29;
			_cOLUMN29C = p_cOLUMN29C;
			_cOLUMN30 = p_cOLUMN30;
			_cOLUMN30C = p_cOLUMN30C;
			_cOLUMN31 = p_cOLUMN31;
			_cOLUMN31C = p_cOLUMN31C;
			_cOLUMN32 = p_cOLUMN32;
			_cOLUMN32C = p_cOLUMN32C;
			_cOLUMN33 = p_cOLUMN33;
			_cOLUMN33C = p_cOLUMN33C;
			_cOLUMN34 = p_cOLUMN34;
			_cOLUMN34C = p_cOLUMN34C;
			_cOLUMN35 = p_cOLUMN35;
			_cOLUMN35C = p_cOLUMN35C;
			_cOLUMN36 = p_cOLUMN36;
			_cOLUMN36C = p_cOLUMN36C;
			_cOLUMN37 = p_cOLUMN37;
			_cOLUMN37C = p_cOLUMN37C;
			_cOLUMN38 = p_cOLUMN38;
			_cOLUMN38C = p_cOLUMN38C;
			_cOLUMN39 = p_cOLUMN39;
			_cOLUMN39C = p_cOLUMN39C;
			_cOLUMN40 = p_cOLUMN40;
			_cOLUMN40C = p_cOLUMN40C;
			_cOLUMN41 = p_cOLUMN41;
			_cOLUMN41C = p_cOLUMN41C;
			_cOLUMN42 = p_cOLUMN42;
			_cOLUMN42C = p_cOLUMN42C;
			_cOLUMN43 = p_cOLUMN43;
			_cOLUMN43C = p_cOLUMN43C;
			_cOLUMN44 = p_cOLUMN44;
			_cOLUMN44C = p_cOLUMN44C;
			_cOLUMN45 = p_cOLUMN45;
			_cOLUMN45C = p_cOLUMN45C;
            _cUSTOMERSNO = p_cUSTOMERSNO;
		}

        #endregion

        #region Properties
        [Property("COSTRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string COSTRID
        {
            get { return _cOSTRID; }
            set
            {
                if ((_cOSTRID == null) || (value == null) || (!value.Equals(_cOSTRID)))
                {
                    object oldValue = _cOSTRID;
                    _cOSTRID = value;
                    RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COSTRID, oldValue, value);
                }
            }
        }
        [Property("GDZRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string GDZRID
		{
			get { return _gDZRID; }
			set
			{
				if ((_gDZRID == null) || (value == null) || (!value.Equals(_gDZRID)))
				{
                    object oldValue = _gDZRID;
					_gDZRID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_GDZRID, oldValue, value);
				}
			}
		}

		[Property("CALCTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string CALCTYPE
		{
			get { return _cALCTYPE; }
			set
			{
				if ((_cALCTYPE == null) || (value == null) || (!value.Equals(_cALCTYPE)))
				{
                    object oldValue = _cALCTYPE;
					_cALCTYPE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CALCTYPE, oldValue, value);
				}
			}
		}

		[Property("COLUMN46", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN46
		{
			get { return _cOLUMN46; }
			set
			{
				if ((_cOLUMN46 == null) || (value == null) || (!value.Equals(_cOLUMN46)))
				{
                    object oldValue = _cOLUMN46;
					_cOLUMN46 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN46, oldValue, value);
				}
			}
		}

		[Property("COLUMN46C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN46C
		{
			get { return _cOLUMN46C; }
			set
			{
				if ((_cOLUMN46C == null) || (value == null) || (!value.Equals(_cOLUMN46C)))
				{
                    object oldValue = _cOLUMN46C;
					_cOLUMN46C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN46C, oldValue, value);
				}
			}
		}

		[Property("COLUMN47", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN47
		{
			get { return _cOLUMN47; }
			set
			{
				if ((_cOLUMN47 == null) || (value == null) || (!value.Equals(_cOLUMN47)))
				{
                    object oldValue = _cOLUMN47;
					_cOLUMN47 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN47, oldValue, value);
				}
			}
		}

		[Property("COLUMN47C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN47C
		{
			get { return _cOLUMN47C; }
			set
			{
				if ((_cOLUMN47C == null) || (value == null) || (!value.Equals(_cOLUMN47C)))
				{
                    object oldValue = _cOLUMN47C;
					_cOLUMN47C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN47C, oldValue, value);
				}
			}
		}

		[Property("COLUMN48", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN48
		{
			get { return _cOLUMN48; }
			set
			{
				if ((_cOLUMN48 == null) || (value == null) || (!value.Equals(_cOLUMN48)))
				{
                    object oldValue = _cOLUMN48;
					_cOLUMN48 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN48, oldValue, value);
				}
			}
		}

		[Property("COLUMN48C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN48C
		{
			get { return _cOLUMN48C; }
			set
			{
				if ((_cOLUMN48C == null) || (value == null) || (!value.Equals(_cOLUMN48C)))
				{
                    object oldValue = _cOLUMN48C;
					_cOLUMN48C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN48C, oldValue, value);
				}
			}
		}

		[Property("COLUMN49", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN49
		{
			get { return _cOLUMN49; }
			set
			{
				if ((_cOLUMN49 == null) || (value == null) || (!value.Equals(_cOLUMN49)))
				{
                    object oldValue = _cOLUMN49;
					_cOLUMN49 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN49, oldValue, value);
				}
			}
		}

		[Property("COLUMN49C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN49C
		{
			get { return _cOLUMN49C; }
			set
			{
				if ((_cOLUMN49C == null) || (value == null) || (!value.Equals(_cOLUMN49C)))
				{
                    object oldValue = _cOLUMN49C;
					_cOLUMN49C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN49C, oldValue, value);
				}
			}
		}

		[Property("COLUMN50", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN50
		{
			get { return _cOLUMN50; }
			set
			{
				if ((_cOLUMN50 == null) || (value == null) || (!value.Equals(_cOLUMN50)))
				{
                    object oldValue = _cOLUMN50;
					_cOLUMN50 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN50, oldValue, value);
				}
			}
		}

		[Property("COLUMN50C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN50C
		{
			get { return _cOLUMN50C; }
			set
			{
				if ((_cOLUMN50C == null) || (value == null) || (!value.Equals(_cOLUMN50C)))
				{
                    object oldValue = _cOLUMN50C;
					_cOLUMN50C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN50C, oldValue, value);
				}
			}
		}

		[Property("DJSTATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string DJSTATUS
		{
			get { return _dJSTATUS; }
			set
			{
				if ((_dJSTATUS == null) || (value == null) || (!value.Equals(_dJSTATUS)))
				{
                    object oldValue = _dJSTATUS;
					_dJSTATUS = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_DJSTATUS, oldValue, value);
				}
			}
		}
        

        [Property("CALCCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CALCCODE
		{
			get { return _cALCCODE; }
			set
			{
				if ((_cALCCODE == null) || (value == null) || (!value.Equals(_cALCCODE)))
				{
                    object oldValue = _cALCCODE;
					_cALCCODE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CALCCODE, oldValue, value);
				}
			}
		}

		[Property("CALCNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CALCNAME
		{
			get { return _cALCNAME; }
			set
			{
				if ((_cALCNAME == null) || (value == null) || (!value.Equals(_cALCNAME)))
				{
                    object oldValue = _cALCNAME;
					_cALCNAME = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CALCNAME, oldValue, value);
				}
			}
		}

		[Property("BJRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BJRID
		{
			get { return _bJRID; }
			set
			{
				if ((_bJRID == null) || (value == null) || (!value.Equals(_bJRID)))
				{
                    object oldValue = _bJRID;
					_bJRID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_BJRID, oldValue, value);
				}
			}
		}

		[Property("IFBJITEM", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string IFBJITEM
		{
			get { return _iFBJITEM; }
			set
			{
				if ((_iFBJITEM == null) || (value == null) || (!value.Equals(_iFBJITEM)))
				{
                    object oldValue = _iFBJITEM;
					_iFBJITEM = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_IFBJITEM, oldValue, value);
				}
			}
		}

		[Property("DJFSRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DJFSRID
		{
			get { return _dJFSRID; }
			set
			{
				if ((_dJFSRID == null) || (value == null) || (!value.Equals(_dJFSRID)))
				{
                    object oldValue = _dJFSRID;
					_dJFSRID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_DJFSRID, oldValue, value);
				}
			}
		}

		[Property("MIN", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MIN
		{
			get { return _mIN; }
			set
			{
				if (value != _mIN)
				{
                    object oldValue = _mIN;
					_mIN = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MIN, oldValue, value);
				}
			}
		}

		[Property("BJPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? BJPRICE
		{
			get { return _bJPRICE; }
			set
			{
				if (value != _bJPRICE)
				{
                    object oldValue = _bJPRICE;
					_bJPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_BJPRICE, oldValue, value);
				}
			}
		}

		[Property("SPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SPRICE
		{
			get { return _sPRICE; }
			set
			{
				if (value != _sPRICE)
				{
                    object oldValue = _sPRICE;
					_sPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_SPRICE, oldValue, value);
				}
			}
		}

		[Property("WDJBJRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string WDJBJRID
		{
			get { return _wDJBJRID; }
			set
			{
				if ((_wDJBJRID == null) || (value == null) || (!value.Equals(_wDJBJRID)))
				{
                    object oldValue = _wDJBJRID;
					_wDJBJRID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_WDJBJRID, oldValue, value);
				}
			}
		}

		[Property("CREATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CREATETIME
		{
			get { return _cREATETIME; }
			set
			{
				if (value != _cREATETIME)
				{
                    object oldValue = _cREATETIME;
					_cREATETIME = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CREATEID, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CREATEUSER, oldValue, value);
				}
			}
		}

		[Property("MODIFYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? MODIFYTIME
		{
			get { return _mODIFYTIME; }
			set
			{
				if (value != _mODIFYTIME)
				{
                    object oldValue = _mODIFYTIME;
					_mODIFYTIME = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MODIFYID, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string MEMO
		{
			get { return _mEMO; }
			set
			{
				if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
				{
                    object oldValue = _mEMO;
					_mEMO = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("SORD", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SORD
		{
			get { return _sORD; }
			set
			{
				if (value != _sORD)
				{
                    object oldValue = _sORD;
					_sORD = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_SORD, oldValue, value);
				}
			}
		}

		[Property("CALCUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CALCUNIT
		{
			get { return _cALCUNIT; }
			set
			{
				if ((_cALCUNIT == null) || (value == null) || (!value.Equals(_cALCUNIT)))
				{
                    object oldValue = _cALCUNIT;
					_cALCUNIT = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CALCUNIT, oldValue, value);
				}
			}
		}

		[Property("CURRENCY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CURRENCY
		{
			get { return _cURRENCY; }
			set
			{
				if ((_cURRENCY == null) || (value == null) || (!value.Equals(_cURRENCY)))
				{
                    object oldValue = _cURRENCY;
					_cURRENCY = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CURRENCY, oldValue, value);
				}
			}
		}

		[Property("PURPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? PURPRICE
		{
			get { return _pURPRICE; }
			set
			{
				if (value != _pURPRICE)
				{
                    object oldValue = _pURPRICE;
					_pURPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_PURPRICE, oldValue, value);
				}
			}
		}

		[Property("COSTPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? COSTPRICE
		{
			get { return _cOSTPRICE; }
			set
			{
				if (value != _cOSTPRICE)
				{
                    object oldValue = _cOSTPRICE;
					_cOSTPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COSTPRICE, oldValue, value);
				}
			}
		}

		[Property("MAXPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MAXPRICE
		{
			get { return _mAXPRICE; }
			set
			{
				if (value != _mAXPRICE)
				{
                    object oldValue = _mAXPRICE;
					_mAXPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MAXPRICE, oldValue, value);
				}
			}
		}

		[Property("MINPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MINPRICE
		{
			get { return _mINPRICE; }
			set
			{
				if (value != _mINPRICE)
				{
                    object oldValue = _mINPRICE;
					_mINPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_MINPRICE, oldValue, value);
				}
			}
		}

		[Property("GUIDEPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? GUIDEPRICE
		{
			get { return _gUIDEPRICE; }
			set
			{
				if (value != _gUIDEPRICE)
				{
                    object oldValue = _gUIDEPRICE;
					_gUIDEPRICE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_GUIDEPRICE, oldValue, value);
				}
			}
		}

		[Property("STARTDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? STARTDATE
		{
			get { return _sTARTDATE; }
			set
			{
				if (value != _sTARTDATE)
				{
                    object oldValue = _sTARTDATE;
					_sTARTDATE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_STARTDATE, oldValue, value);
				}
			}
		}

		[Property("ENDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ENDDATE
		{
			get { return _eNDDATE; }
			set
			{
				if (value != _eNDDATE)
				{
                    object oldValue = _eNDDATE;
					_eNDDATE = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_ENDDATE, oldValue, value);
				}
			}
		}

		[Property("FEECALCID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEECALCID
		{
			get { return _fEECALCID; }
			set
			{
				if ((_fEECALCID == null) || (value == null) || (!value.Equals(_fEECALCID)))
				{
                    object oldValue = _fEECALCID;
					_fEECALCID = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_FEECALCID, oldValue, value);
				}
			}
		}

		[Property("COLUMN1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN1
		{
			get { return _cOLUMN1; }
			set
			{
				if ((_cOLUMN1 == null) || (value == null) || (!value.Equals(_cOLUMN1)))
				{
                    object oldValue = _cOLUMN1;
					_cOLUMN1 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN1, oldValue, value);
				}
			}
		}

		[Property("COLUMN1C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN1C
		{
			get { return _cOLUMN1C; }
			set
			{
				if ((_cOLUMN1C == null) || (value == null) || (!value.Equals(_cOLUMN1C)))
				{
                    object oldValue = _cOLUMN1C;
					_cOLUMN1C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN1C, oldValue, value);
				}
			}
		}

		[Property("COLUMN2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN2
		{
			get { return _cOLUMN2; }
			set
			{
				if ((_cOLUMN2 == null) || (value == null) || (!value.Equals(_cOLUMN2)))
				{
                    object oldValue = _cOLUMN2;
					_cOLUMN2 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN2, oldValue, value);
				}
			}
		}

		[Property("COLUMN2C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN2C
		{
			get { return _cOLUMN2C; }
			set
			{
				if ((_cOLUMN2C == null) || (value == null) || (!value.Equals(_cOLUMN2C)))
				{
                    object oldValue = _cOLUMN2C;
					_cOLUMN2C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN2C, oldValue, value);
				}
			}
		}

		[Property("COLUMN3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN3
		{
			get { return _cOLUMN3; }
			set
			{
				if ((_cOLUMN3 == null) || (value == null) || (!value.Equals(_cOLUMN3)))
				{
                    object oldValue = _cOLUMN3;
					_cOLUMN3 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN3, oldValue, value);
				}
			}
		}

		[Property("COLUMN3C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN3C
		{
			get { return _cOLUMN3C; }
			set
			{
				if ((_cOLUMN3C == null) || (value == null) || (!value.Equals(_cOLUMN3C)))
				{
                    object oldValue = _cOLUMN3C;
					_cOLUMN3C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN3C, oldValue, value);
				}
			}
		}

		[Property("COLUMN4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN4
		{
			get { return _cOLUMN4; }
			set
			{
				if ((_cOLUMN4 == null) || (value == null) || (!value.Equals(_cOLUMN4)))
				{
                    object oldValue = _cOLUMN4;
					_cOLUMN4 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN4, oldValue, value);
				}
			}
		}

		[Property("COLUMN4C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN4C
		{
			get { return _cOLUMN4C; }
			set
			{
				if ((_cOLUMN4C == null) || (value == null) || (!value.Equals(_cOLUMN4C)))
				{
                    object oldValue = _cOLUMN4C;
					_cOLUMN4C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN4C, oldValue, value);
				}
			}
		}

		[Property("COLUMN5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN5
		{
			get { return _cOLUMN5; }
			set
			{
				if ((_cOLUMN5 == null) || (value == null) || (!value.Equals(_cOLUMN5)))
				{
                    object oldValue = _cOLUMN5;
					_cOLUMN5 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN5, oldValue, value);
				}
			}
		}

		[Property("COLUMN5C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN5C
		{
			get { return _cOLUMN5C; }
			set
			{
				if ((_cOLUMN5C == null) || (value == null) || (!value.Equals(_cOLUMN5C)))
				{
                    object oldValue = _cOLUMN5C;
					_cOLUMN5C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN5C, oldValue, value);
				}
			}
		}

		[Property("COLUMN6", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN6
		{
			get { return _cOLUMN6; }
			set
			{
				if ((_cOLUMN6 == null) || (value == null) || (!value.Equals(_cOLUMN6)))
				{
                    object oldValue = _cOLUMN6;
					_cOLUMN6 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN6, oldValue, value);
				}
			}
		}

		[Property("COLUMN6C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN6C
		{
			get { return _cOLUMN6C; }
			set
			{
				if ((_cOLUMN6C == null) || (value == null) || (!value.Equals(_cOLUMN6C)))
				{
                    object oldValue = _cOLUMN6C;
					_cOLUMN6C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN6C, oldValue, value);
				}
			}
		}

		[Property("COLUMN7", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN7
		{
			get { return _cOLUMN7; }
			set
			{
				if ((_cOLUMN7 == null) || (value == null) || (!value.Equals(_cOLUMN7)))
				{
                    object oldValue = _cOLUMN7;
					_cOLUMN7 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN7, oldValue, value);
				}
			}
		}

		[Property("COLUMN7C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN7C
		{
			get { return _cOLUMN7C; }
			set
			{
				if ((_cOLUMN7C == null) || (value == null) || (!value.Equals(_cOLUMN7C)))
				{
                    object oldValue = _cOLUMN7C;
					_cOLUMN7C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN7C, oldValue, value);
				}
			}
		}

		[Property("COLUMN8", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN8
		{
			get { return _cOLUMN8; }
			set
			{
				if ((_cOLUMN8 == null) || (value == null) || (!value.Equals(_cOLUMN8)))
				{
                    object oldValue = _cOLUMN8;
					_cOLUMN8 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN8, oldValue, value);
				}
			}
		}

		[Property("COLUMN8C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN8C
		{
			get { return _cOLUMN8C; }
			set
			{
				if ((_cOLUMN8C == null) || (value == null) || (!value.Equals(_cOLUMN8C)))
				{
                    object oldValue = _cOLUMN8C;
					_cOLUMN8C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN8C, oldValue, value);
				}
			}
		}

		[Property("COLUMN9", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN9
		{
			get { return _cOLUMN9; }
			set
			{
				if ((_cOLUMN9 == null) || (value == null) || (!value.Equals(_cOLUMN9)))
				{
                    object oldValue = _cOLUMN9;
					_cOLUMN9 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN9, oldValue, value);
				}
			}
		}

		[Property("COLUMN9C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN9C
		{
			get { return _cOLUMN9C; }
			set
			{
				if ((_cOLUMN9C == null) || (value == null) || (!value.Equals(_cOLUMN9C)))
				{
                    object oldValue = _cOLUMN9C;
					_cOLUMN9C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN9C, oldValue, value);
				}
			}
		}

		[Property("COLUMN10", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN10
		{
			get { return _cOLUMN10; }
			set
			{
				if ((_cOLUMN10 == null) || (value == null) || (!value.Equals(_cOLUMN10)))
				{
                    object oldValue = _cOLUMN10;
					_cOLUMN10 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN10, oldValue, value);
				}
			}
		}

		[Property("COLUMN10C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN10C
		{
			get { return _cOLUMN10C; }
			set
			{
				if ((_cOLUMN10C == null) || (value == null) || (!value.Equals(_cOLUMN10C)))
				{
                    object oldValue = _cOLUMN10C;
					_cOLUMN10C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN10C, oldValue, value);
				}
			}
		}

		[Property("COLUMN11", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN11
		{
			get { return _cOLUMN11; }
			set
			{
				if ((_cOLUMN11 == null) || (value == null) || (!value.Equals(_cOLUMN11)))
				{
                    object oldValue = _cOLUMN11;
					_cOLUMN11 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN11, oldValue, value);
				}
			}
		}

		[Property("COLUMN11C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN11C
		{
			get { return _cOLUMN11C; }
			set
			{
				if ((_cOLUMN11C == null) || (value == null) || (!value.Equals(_cOLUMN11C)))
				{
                    object oldValue = _cOLUMN11C;
					_cOLUMN11C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN11C, oldValue, value);
				}
			}
		}

		[Property("COLUMN12", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN12
		{
			get { return _cOLUMN12; }
			set
			{
				if ((_cOLUMN12 == null) || (value == null) || (!value.Equals(_cOLUMN12)))
				{
                    object oldValue = _cOLUMN12;
					_cOLUMN12 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN12, oldValue, value);
				}
			}
		}

		[Property("COLUMN12C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN12C
		{
			get { return _cOLUMN12C; }
			set
			{
				if ((_cOLUMN12C == null) || (value == null) || (!value.Equals(_cOLUMN12C)))
				{
                    object oldValue = _cOLUMN12C;
					_cOLUMN12C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN12C, oldValue, value);
				}
			}
		}

		[Property("COLUMN13", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN13
		{
			get { return _cOLUMN13; }
			set
			{
				if ((_cOLUMN13 == null) || (value == null) || (!value.Equals(_cOLUMN13)))
				{
                    object oldValue = _cOLUMN13;
					_cOLUMN13 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN13, oldValue, value);
				}
			}
		}

		[Property("COLUMN13C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN13C
		{
			get { return _cOLUMN13C; }
			set
			{
				if ((_cOLUMN13C == null) || (value == null) || (!value.Equals(_cOLUMN13C)))
				{
                    object oldValue = _cOLUMN13C;
					_cOLUMN13C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN13C, oldValue, value);
				}
			}
		}

		[Property("COLUMN14", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN14
		{
			get { return _cOLUMN14; }
			set
			{
				if ((_cOLUMN14 == null) || (value == null) || (!value.Equals(_cOLUMN14)))
				{
                    object oldValue = _cOLUMN14;
					_cOLUMN14 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN14, oldValue, value);
				}
			}
		}

		[Property("COLUMN14C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN14C
		{
			get { return _cOLUMN14C; }
			set
			{
				if ((_cOLUMN14C == null) || (value == null) || (!value.Equals(_cOLUMN14C)))
				{
                    object oldValue = _cOLUMN14C;
					_cOLUMN14C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN14C, oldValue, value);
				}
			}
		}

		[Property("COLUMN15", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN15
		{
			get { return _cOLUMN15; }
			set
			{
				if ((_cOLUMN15 == null) || (value == null) || (!value.Equals(_cOLUMN15)))
				{
                    object oldValue = _cOLUMN15;
					_cOLUMN15 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN15, oldValue, value);
				}
			}
		}

		[Property("COLUMN15C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN15C
		{
			get { return _cOLUMN15C; }
			set
			{
				if ((_cOLUMN15C == null) || (value == null) || (!value.Equals(_cOLUMN15C)))
				{
                    object oldValue = _cOLUMN15C;
					_cOLUMN15C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN15C, oldValue, value);
				}
			}
		}

		[Property("COLUMN16", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN16
		{
			get { return _cOLUMN16; }
			set
			{
				if ((_cOLUMN16 == null) || (value == null) || (!value.Equals(_cOLUMN16)))
				{
                    object oldValue = _cOLUMN16;
					_cOLUMN16 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN16, oldValue, value);
				}
			}
		}

		[Property("COLUMN16C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN16C
		{
			get { return _cOLUMN16C; }
			set
			{
				if ((_cOLUMN16C == null) || (value == null) || (!value.Equals(_cOLUMN16C)))
				{
                    object oldValue = _cOLUMN16C;
					_cOLUMN16C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN16C, oldValue, value);
				}
			}
		}

		[Property("COLUMN17", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN17
		{
			get { return _cOLUMN17; }
			set
			{
				if ((_cOLUMN17 == null) || (value == null) || (!value.Equals(_cOLUMN17)))
				{
                    object oldValue = _cOLUMN17;
					_cOLUMN17 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN17, oldValue, value);
				}
			}
		}

		[Property("COLUMN17C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN17C
		{
			get { return _cOLUMN17C; }
			set
			{
				if ((_cOLUMN17C == null) || (value == null) || (!value.Equals(_cOLUMN17C)))
				{
                    object oldValue = _cOLUMN17C;
					_cOLUMN17C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN17C, oldValue, value);
				}
			}
		}

		[Property("COLUMN18", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN18
		{
			get { return _cOLUMN18; }
			set
			{
				if ((_cOLUMN18 == null) || (value == null) || (!value.Equals(_cOLUMN18)))
				{
                    object oldValue = _cOLUMN18;
					_cOLUMN18 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN18, oldValue, value);
				}
			}
		}

		[Property("COLUMN18C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN18C
		{
			get { return _cOLUMN18C; }
			set
			{
				if ((_cOLUMN18C == null) || (value == null) || (!value.Equals(_cOLUMN18C)))
				{
                    object oldValue = _cOLUMN18C;
					_cOLUMN18C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN18C, oldValue, value);
				}
			}
		}

		[Property("COLUMN19", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN19
		{
			get { return _cOLUMN19; }
			set
			{
				if ((_cOLUMN19 == null) || (value == null) || (!value.Equals(_cOLUMN19)))
				{
                    object oldValue = _cOLUMN19;
					_cOLUMN19 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN19, oldValue, value);
				}
			}
		}

		[Property("COLUMN19C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN19C
		{
			get { return _cOLUMN19C; }
			set
			{
				if ((_cOLUMN19C == null) || (value == null) || (!value.Equals(_cOLUMN19C)))
				{
                    object oldValue = _cOLUMN19C;
					_cOLUMN19C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN19C, oldValue, value);
				}
			}
		}

		[Property("COLUMN20", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN20
		{
			get { return _cOLUMN20; }
			set
			{
				if ((_cOLUMN20 == null) || (value == null) || (!value.Equals(_cOLUMN20)))
				{
                    object oldValue = _cOLUMN20;
					_cOLUMN20 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN20, oldValue, value);
				}
			}
		}

		[Property("COLUMN20C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN20C
		{
			get { return _cOLUMN20C; }
			set
			{
				if ((_cOLUMN20C == null) || (value == null) || (!value.Equals(_cOLUMN20C)))
				{
                    object oldValue = _cOLUMN20C;
					_cOLUMN20C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN20C, oldValue, value);
				}
			}
		}

		[Property("COLUMN21", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN21
		{
			get { return _cOLUMN21; }
			set
			{
				if ((_cOLUMN21 == null) || (value == null) || (!value.Equals(_cOLUMN21)))
				{
                    object oldValue = _cOLUMN21;
					_cOLUMN21 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN21, oldValue, value);
				}
			}
		}

		[Property("COLUMN21C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN21C
		{
			get { return _cOLUMN21C; }
			set
			{
				if ((_cOLUMN21C == null) || (value == null) || (!value.Equals(_cOLUMN21C)))
				{
                    object oldValue = _cOLUMN21C;
					_cOLUMN21C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN21C, oldValue, value);
				}
			}
		}

		[Property("COLUMN22", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN22
		{
			get { return _cOLUMN22; }
			set
			{
				if ((_cOLUMN22 == null) || (value == null) || (!value.Equals(_cOLUMN22)))
				{
                    object oldValue = _cOLUMN22;
					_cOLUMN22 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN22, oldValue, value);
				}
			}
		}

		[Property("COLUMN22C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN22C
		{
			get { return _cOLUMN22C; }
			set
			{
				if ((_cOLUMN22C == null) || (value == null) || (!value.Equals(_cOLUMN22C)))
				{
                    object oldValue = _cOLUMN22C;
					_cOLUMN22C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN22C, oldValue, value);
				}
			}
		}

		[Property("COLUMN23", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN23
		{
			get { return _cOLUMN23; }
			set
			{
				if ((_cOLUMN23 == null) || (value == null) || (!value.Equals(_cOLUMN23)))
				{
                    object oldValue = _cOLUMN23;
					_cOLUMN23 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN23, oldValue, value);
				}
			}
		}

		[Property("COLUMN23C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN23C
		{
			get { return _cOLUMN23C; }
			set
			{
				if ((_cOLUMN23C == null) || (value == null) || (!value.Equals(_cOLUMN23C)))
				{
                    object oldValue = _cOLUMN23C;
					_cOLUMN23C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN23C, oldValue, value);
				}
			}
		}

		[Property("COLUMN24", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN24
		{
			get { return _cOLUMN24; }
			set
			{
				if ((_cOLUMN24 == null) || (value == null) || (!value.Equals(_cOLUMN24)))
				{
                    object oldValue = _cOLUMN24;
					_cOLUMN24 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN24, oldValue, value);
				}
			}
		}

		[Property("COLUMN24C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN24C
		{
			get { return _cOLUMN24C; }
			set
			{
				if ((_cOLUMN24C == null) || (value == null) || (!value.Equals(_cOLUMN24C)))
				{
                    object oldValue = _cOLUMN24C;
					_cOLUMN24C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN24C, oldValue, value);
				}
			}
		}

		[Property("COLUMN25", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN25
		{
			get { return _cOLUMN25; }
			set
			{
				if ((_cOLUMN25 == null) || (value == null) || (!value.Equals(_cOLUMN25)))
				{
                    object oldValue = _cOLUMN25;
					_cOLUMN25 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN25, oldValue, value);
				}
			}
		}

		[Property("COLUMN25C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN25C
		{
			get { return _cOLUMN25C; }
			set
			{
				if ((_cOLUMN25C == null) || (value == null) || (!value.Equals(_cOLUMN25C)))
				{
                    object oldValue = _cOLUMN25C;
					_cOLUMN25C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN25C, oldValue, value);
				}
			}
		}

		[Property("COLUMN26", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN26
		{
			get { return _cOLUMN26; }
			set
			{
				if ((_cOLUMN26 == null) || (value == null) || (!value.Equals(_cOLUMN26)))
				{
                    object oldValue = _cOLUMN26;
					_cOLUMN26 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN26, oldValue, value);
				}
			}
		}

		[Property("COLUMN26C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN26C
		{
			get { return _cOLUMN26C; }
			set
			{
				if ((_cOLUMN26C == null) || (value == null) || (!value.Equals(_cOLUMN26C)))
				{
                    object oldValue = _cOLUMN26C;
					_cOLUMN26C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN26C, oldValue, value);
				}
			}
		}

		[Property("COLUMN27", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN27
		{
			get { return _cOLUMN27; }
			set
			{
				if ((_cOLUMN27 == null) || (value == null) || (!value.Equals(_cOLUMN27)))
				{
                    object oldValue = _cOLUMN27;
					_cOLUMN27 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN27, oldValue, value);
				}
			}
		}

		[Property("COLUMN27C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN27C
		{
			get { return _cOLUMN27C; }
			set
			{
				if ((_cOLUMN27C == null) || (value == null) || (!value.Equals(_cOLUMN27C)))
				{
                    object oldValue = _cOLUMN27C;
					_cOLUMN27C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN27C, oldValue, value);
				}
			}
		}

		[Property("COLUMN28", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN28
		{
			get { return _cOLUMN28; }
			set
			{
				if ((_cOLUMN28 == null) || (value == null) || (!value.Equals(_cOLUMN28)))
				{
                    object oldValue = _cOLUMN28;
					_cOLUMN28 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN28, oldValue, value);
				}
			}
		}

		[Property("COLUMN28C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN28C
		{
			get { return _cOLUMN28C; }
			set
			{
				if ((_cOLUMN28C == null) || (value == null) || (!value.Equals(_cOLUMN28C)))
				{
                    object oldValue = _cOLUMN28C;
					_cOLUMN28C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN28C, oldValue, value);
				}
			}
		}

		[Property("COLUMN29", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN29
		{
			get { return _cOLUMN29; }
			set
			{
				if ((_cOLUMN29 == null) || (value == null) || (!value.Equals(_cOLUMN29)))
				{
                    object oldValue = _cOLUMN29;
					_cOLUMN29 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN29, oldValue, value);
				}
			}
		}

		[Property("COLUMN29C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN29C
		{
			get { return _cOLUMN29C; }
			set
			{
				if ((_cOLUMN29C == null) || (value == null) || (!value.Equals(_cOLUMN29C)))
				{
                    object oldValue = _cOLUMN29C;
					_cOLUMN29C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN29C, oldValue, value);
				}
			}
		}

		[Property("COLUMN30", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN30
		{
			get { return _cOLUMN30; }
			set
			{
				if ((_cOLUMN30 == null) || (value == null) || (!value.Equals(_cOLUMN30)))
				{
                    object oldValue = _cOLUMN30;
					_cOLUMN30 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN30, oldValue, value);
				}
			}
		}

		[Property("COLUMN30C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN30C
		{
			get { return _cOLUMN30C; }
			set
			{
				if ((_cOLUMN30C == null) || (value == null) || (!value.Equals(_cOLUMN30C)))
				{
                    object oldValue = _cOLUMN30C;
					_cOLUMN30C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN30C, oldValue, value);
				}
			}
		}

		[Property("COLUMN31", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN31
		{
			get { return _cOLUMN31; }
			set
			{
				if ((_cOLUMN31 == null) || (value == null) || (!value.Equals(_cOLUMN31)))
				{
                    object oldValue = _cOLUMN31;
					_cOLUMN31 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN31, oldValue, value);
				}
			}
		}

		[Property("COLUMN31C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN31C
		{
			get { return _cOLUMN31C; }
			set
			{
				if ((_cOLUMN31C == null) || (value == null) || (!value.Equals(_cOLUMN31C)))
				{
                    object oldValue = _cOLUMN31C;
					_cOLUMN31C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN31C, oldValue, value);
				}
			}
		}

		[Property("COLUMN32", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN32
		{
			get { return _cOLUMN32; }
			set
			{
				if ((_cOLUMN32 == null) || (value == null) || (!value.Equals(_cOLUMN32)))
				{
                    object oldValue = _cOLUMN32;
					_cOLUMN32 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN32, oldValue, value);
				}
			}
		}

		[Property("COLUMN32C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN32C
		{
			get { return _cOLUMN32C; }
			set
			{
				if ((_cOLUMN32C == null) || (value == null) || (!value.Equals(_cOLUMN32C)))
				{
                    object oldValue = _cOLUMN32C;
					_cOLUMN32C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN32C, oldValue, value);
				}
			}
		}

		[Property("COLUMN33", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN33
		{
			get { return _cOLUMN33; }
			set
			{
				if ((_cOLUMN33 == null) || (value == null) || (!value.Equals(_cOLUMN33)))
				{
                    object oldValue = _cOLUMN33;
					_cOLUMN33 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN33, oldValue, value);
				}
			}
		}

		[Property("COLUMN33C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN33C
		{
			get { return _cOLUMN33C; }
			set
			{
				if ((_cOLUMN33C == null) || (value == null) || (!value.Equals(_cOLUMN33C)))
				{
                    object oldValue = _cOLUMN33C;
					_cOLUMN33C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN33C, oldValue, value);
				}
			}
		}

		[Property("COLUMN34", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN34
		{
			get { return _cOLUMN34; }
			set
			{
				if ((_cOLUMN34 == null) || (value == null) || (!value.Equals(_cOLUMN34)))
				{
                    object oldValue = _cOLUMN34;
					_cOLUMN34 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN34, oldValue, value);
				}
			}
		}

		[Property("COLUMN34C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN34C
		{
			get { return _cOLUMN34C; }
			set
			{
				if ((_cOLUMN34C == null) || (value == null) || (!value.Equals(_cOLUMN34C)))
				{
                    object oldValue = _cOLUMN34C;
					_cOLUMN34C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN34C, oldValue, value);
				}
			}
		}

		[Property("COLUMN35", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN35
		{
			get { return _cOLUMN35; }
			set
			{
				if ((_cOLUMN35 == null) || (value == null) || (!value.Equals(_cOLUMN35)))
				{
                    object oldValue = _cOLUMN35;
					_cOLUMN35 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN35, oldValue, value);
				}
			}
		}

		[Property("COLUMN35C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN35C
		{
			get { return _cOLUMN35C; }
			set
			{
				if ((_cOLUMN35C == null) || (value == null) || (!value.Equals(_cOLUMN35C)))
				{
                    object oldValue = _cOLUMN35C;
					_cOLUMN35C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN35C, oldValue, value);
				}
			}
		}

		[Property("COLUMN36", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN36
		{
			get { return _cOLUMN36; }
			set
			{
				if ((_cOLUMN36 == null) || (value == null) || (!value.Equals(_cOLUMN36)))
				{
                    object oldValue = _cOLUMN36;
					_cOLUMN36 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN36, oldValue, value);
				}
			}
		}

		[Property("COLUMN36C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN36C
		{
			get { return _cOLUMN36C; }
			set
			{
				if ((_cOLUMN36C == null) || (value == null) || (!value.Equals(_cOLUMN36C)))
				{
                    object oldValue = _cOLUMN36C;
					_cOLUMN36C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN36C, oldValue, value);
				}
			}
		}

		[Property("COLUMN37", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN37
		{
			get { return _cOLUMN37; }
			set
			{
				if ((_cOLUMN37 == null) || (value == null) || (!value.Equals(_cOLUMN37)))
				{
                    object oldValue = _cOLUMN37;
					_cOLUMN37 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN37, oldValue, value);
				}
			}
		}

		[Property("COLUMN37C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN37C
		{
			get { return _cOLUMN37C; }
			set
			{
				if ((_cOLUMN37C == null) || (value == null) || (!value.Equals(_cOLUMN37C)))
				{
                    object oldValue = _cOLUMN37C;
					_cOLUMN37C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN37C, oldValue, value);
				}
			}
		}

		[Property("COLUMN38", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN38
		{
			get { return _cOLUMN38; }
			set
			{
				if ((_cOLUMN38 == null) || (value == null) || (!value.Equals(_cOLUMN38)))
				{
                    object oldValue = _cOLUMN38;
					_cOLUMN38 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN38, oldValue, value);
				}
			}
		}

		[Property("COLUMN38C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN38C
		{
			get { return _cOLUMN38C; }
			set
			{
				if ((_cOLUMN38C == null) || (value == null) || (!value.Equals(_cOLUMN38C)))
				{
                    object oldValue = _cOLUMN38C;
					_cOLUMN38C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN38C, oldValue, value);
				}
			}
		}

		[Property("COLUMN39", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN39
		{
			get { return _cOLUMN39; }
			set
			{
				if ((_cOLUMN39 == null) || (value == null) || (!value.Equals(_cOLUMN39)))
				{
                    object oldValue = _cOLUMN39;
					_cOLUMN39 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN39, oldValue, value);
				}
			}
		}

		[Property("COLUMN39C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN39C
		{
			get { return _cOLUMN39C; }
			set
			{
				if ((_cOLUMN39C == null) || (value == null) || (!value.Equals(_cOLUMN39C)))
				{
                    object oldValue = _cOLUMN39C;
					_cOLUMN39C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN39C, oldValue, value);
				}
			}
		}

		[Property("COLUMN40", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN40
		{
			get { return _cOLUMN40; }
			set
			{
				if ((_cOLUMN40 == null) || (value == null) || (!value.Equals(_cOLUMN40)))
				{
                    object oldValue = _cOLUMN40;
					_cOLUMN40 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN40, oldValue, value);
				}
			}
		}

		[Property("COLUMN40C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN40C
		{
			get { return _cOLUMN40C; }
			set
			{
				if ((_cOLUMN40C == null) || (value == null) || (!value.Equals(_cOLUMN40C)))
				{
                    object oldValue = _cOLUMN40C;
					_cOLUMN40C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN40C, oldValue, value);
				}
			}
		}

		[Property("COLUMN41", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN41
		{
			get { return _cOLUMN41; }
			set
			{
				if ((_cOLUMN41 == null) || (value == null) || (!value.Equals(_cOLUMN41)))
				{
                    object oldValue = _cOLUMN41;
					_cOLUMN41 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN41, oldValue, value);
				}
			}
		}

		[Property("COLUMN41C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN41C
		{
			get { return _cOLUMN41C; }
			set
			{
				if ((_cOLUMN41C == null) || (value == null) || (!value.Equals(_cOLUMN41C)))
				{
                    object oldValue = _cOLUMN41C;
					_cOLUMN41C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN41C, oldValue, value);
				}
			}
		}

		[Property("COLUMN42", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN42
		{
			get { return _cOLUMN42; }
			set
			{
				if ((_cOLUMN42 == null) || (value == null) || (!value.Equals(_cOLUMN42)))
				{
                    object oldValue = _cOLUMN42;
					_cOLUMN42 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN42, oldValue, value);
				}
			}
		}

		[Property("COLUMN42C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN42C
		{
			get { return _cOLUMN42C; }
			set
			{
				if ((_cOLUMN42C == null) || (value == null) || (!value.Equals(_cOLUMN42C)))
				{
                    object oldValue = _cOLUMN42C;
					_cOLUMN42C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN42C, oldValue, value);
				}
			}
		}

		[Property("COLUMN43", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN43
		{
			get { return _cOLUMN43; }
			set
			{
				if ((_cOLUMN43 == null) || (value == null) || (!value.Equals(_cOLUMN43)))
				{
                    object oldValue = _cOLUMN43;
					_cOLUMN43 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN43, oldValue, value);
				}
			}
		}

		[Property("COLUMN43C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN43C
		{
			get { return _cOLUMN43C; }
			set
			{
				if ((_cOLUMN43C == null) || (value == null) || (!value.Equals(_cOLUMN43C)))
				{
                    object oldValue = _cOLUMN43C;
					_cOLUMN43C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN43C, oldValue, value);
				}
			}
		}

		[Property("COLUMN44", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN44
		{
			get { return _cOLUMN44; }
			set
			{
				if ((_cOLUMN44 == null) || (value == null) || (!value.Equals(_cOLUMN44)))
				{
                    object oldValue = _cOLUMN44;
					_cOLUMN44 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN44, oldValue, value);
				}
			}
		}

		[Property("COLUMN44C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN44C
		{
			get { return _cOLUMN44C; }
			set
			{
				if ((_cOLUMN44C == null) || (value == null) || (!value.Equals(_cOLUMN44C)))
				{
                    object oldValue = _cOLUMN44C;
					_cOLUMN44C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN44C, oldValue, value);
				}
			}
		}

		[Property("COLUMN45", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN45
		{
			get { return _cOLUMN45; }
			set
			{
				if ((_cOLUMN45 == null) || (value == null) || (!value.Equals(_cOLUMN45)))
				{
                    object oldValue = _cOLUMN45;
					_cOLUMN45 = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN45, oldValue, value);
				}
			}
		}

		[Property("COLUMN45C", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COLUMN45C
		{
			get { return _cOLUMN45C; }
			set
			{
				if ((_cOLUMN45C == null) || (value == null) || (!value.Equals(_cOLUMN45C)))
				{
                    object oldValue = _cOLUMN45C;
					_cOLUMN45C = value;
					RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_COLUMN45C, oldValue, value);
				}
			}
		}
        //指定客户对应的客户编码
        [Property("CUSTOMERSNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string CUSTOMERSNO
        {
            get { return _cUSTOMERSNO; }
            set
            {
                if ((_cUSTOMERSNO == null) || (value == null) || (!value.Equals(_cUSTOMERSNO)))
                {
                    object oldValue = _cUSTOMERSNO;
                    _cUSTOMERSNO = value;
                    RaisePropertyChanged(SQM_MODEDJ_VAL.Prop_CUSTOMERSNO, oldValue, value);
                }
            }
        }

        #endregion
    } // SQM_MODEDJ_VAL
}

