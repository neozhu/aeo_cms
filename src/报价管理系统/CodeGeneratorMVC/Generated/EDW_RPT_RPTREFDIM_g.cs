// Business class EDW_RPT_RPTREFDIM generated from EDW_RPT_RPTREFDIM
// Creator: rw
// Created Date: [2017-09-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("EDW_RPT_RPTREFDIM")]
	public partial class EDW_RPT_RPTREFDIM : EntityBase<EDW_RPT_RPTREFDIM>
	{
		#region Property_Names

		public static string Prop_REPORTKEY = "REPORTKEY";
		public static string Prop_DIMENSIONKEY = "DIMENSIONKEY";
		public static string Prop_DIMENSIONTYPE = "DIMENSIONTYPE";
		public static string Prop_SN = "SN";
		public static string Prop_DEFAULTVAL = "DEFAULTVAL";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";

		#endregion

		#region Private_Variables

		private string _rEPORTKEY;
		private string _dIMENSIONKEY;
		private string _dIMENSIONTYPE;
		private System.Decimal? _sN;
		private string _dEFAULTVAL;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;


		#endregion

		#region Constructors

		public EDW_RPT_RPTREFDIM()
		{
		}

		public EDW_RPT_RPTREFDIM(
			string p_rEPORTKEY,
			string p_dIMENSIONKEY,
			string p_dIMENSIONTYPE,
			System.Decimal? p_sN,
			string p_dEFAULTVAL,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER)
		{
			_rEPORTKEY = p_rEPORTKEY;
			_dIMENSIONKEY = p_dIMENSIONKEY;
			_dIMENSIONTYPE = p_dIMENSIONTYPE;
			_sN = p_sN;
			_dEFAULTVAL = p_dEFAULTVAL;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
		}

		#endregion

		#region Properties

		[Property("REPORTKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 40)]
		public string REPORTKEY
		{
			get { return _rEPORTKEY; }
			set
			{
				if ((_rEPORTKEY == null) || (value == null) || (!value.Equals(_rEPORTKEY)))
				{
                    object oldValue = _rEPORTKEY;
					_rEPORTKEY = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_REPORTKEY, oldValue, value);
				}
			}
		}

		[Property("DIMENSIONKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 40)]
		public string DIMENSIONKEY
		{
			get { return _dIMENSIONKEY; }
			set
			{
				if ((_dIMENSIONKEY == null) || (value == null) || (!value.Equals(_dIMENSIONKEY)))
				{
                    object oldValue = _dIMENSIONKEY;
					_dIMENSIONKEY = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_DIMENSIONKEY, oldValue, value);
				}
			}
		}

		[Property("DIMENSIONTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 400)]
		public string DIMENSIONTYPE
		{
			get { return _dIMENSIONTYPE; }
			set
			{
				if ((_dIMENSIONTYPE == null) || (value == null) || (!value.Equals(_dIMENSIONTYPE)))
				{
                    object oldValue = _dIMENSIONTYPE;
					_dIMENSIONTYPE = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_DIMENSIONTYPE, oldValue, value);
				}
			}
		}

		[Property("SN", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SN
		{
			get { return _sN; }
			set
			{
				if (value != _sN)
				{
                    object oldValue = _sN;
					_sN = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_SN, oldValue, value);
				}
			}
		}

		[Property("DEFAULTVAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string DEFAULTVAL
		{
			get { return _dEFAULTVAL; }
			set
			{
				if ((_dEFAULTVAL == null) || (value == null) || (!value.Equals(_dEFAULTVAL)))
				{
                    object oldValue = _dEFAULTVAL;
					_dEFAULTVAL = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_DEFAULTVAL, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string MEMO
		{
			get { return _mEMO; }
			set
			{
				if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
				{
                    object oldValue = _mEMO;
					_mEMO = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(EDW_RPT_RPTREFDIM.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		#endregion
	} // EDW_RPT_RPTREFDIM
}

