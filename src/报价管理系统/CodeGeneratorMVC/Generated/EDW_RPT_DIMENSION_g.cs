// Business class EDW_RPT_DIMENSION generated from EDW_RPT_DIMENSION
// Creator: rw
// Created Date: [2017-10-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("EDW_RPT_DIMENSION")]
	public partial class EDW_RPT_DIMENSION : EntityBase<EDW_RPT_DIMENSION>
	{
		#region Property_Names

		public static string Prop_DIMENSIONKEY = "DIMENSIONKEY";
		public static string Prop_DIMENSIONNAME = "DIMENSIONNAME";
		public static string Prop_DIMENSIONTYPE = "DIMENSIONTYPE";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_DATATYPE = "DATATYPE";
		public static string Prop_DATEPICKER = "DATEPICKER";

		#endregion

		#region Private_Variables

		private string _dIMENSIONKEY;
		private string _dIMENSIONNAME;
		private string _dIMENSIONTYPE;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _dATATYPE;
		private string _dATEPICKER;


		#endregion

		#region Constructors

		public EDW_RPT_DIMENSION()
		{
		}

		public EDW_RPT_DIMENSION(
			string p_dIMENSIONKEY,
			string p_dIMENSIONNAME,
			string p_dIMENSIONTYPE,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_dATATYPE,
			string p_dATEPICKER)
		{
			_dIMENSIONKEY = p_dIMENSIONKEY;
			_dIMENSIONNAME = p_dIMENSIONNAME;
			_dIMENSIONTYPE = p_dIMENSIONTYPE;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_dATATYPE = p_dATATYPE;
			_dATEPICKER = p_dATEPICKER;
		}

		#endregion

		#region Properties

		[Property("DIMENSIONKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 100)]
		public string DIMENSIONKEY
		{
			get { return _dIMENSIONKEY; }
			set
			{
				if ((_dIMENSIONKEY == null) || (value == null) || (!value.Equals(_dIMENSIONKEY)))
				{
                    object oldValue = _dIMENSIONKEY;
					_dIMENSIONKEY = value;
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_DIMENSIONKEY, oldValue, value);
				}
			}
		}

		[Property("DIMENSIONNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 400)]
		public string DIMENSIONNAME
		{
			get { return _dIMENSIONNAME; }
			set
			{
				if ((_dIMENSIONNAME == null) || (value == null) || (!value.Equals(_dIMENSIONNAME)))
				{
                    object oldValue = _dIMENSIONNAME;
					_dIMENSIONNAME = value;
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_DIMENSIONNAME, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_DIMENSIONTYPE, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[Property("DATATYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DATATYPE
		{
			get { return _dATATYPE; }
			set
			{
				if ((_dATATYPE == null) || (value == null) || (!value.Equals(_dATATYPE)))
				{
                    object oldValue = _dATATYPE;
					_dATATYPE = value;
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_DATATYPE, oldValue, value);
				}
			}
		}

		[Property("DATEPICKER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DATEPICKER
		{
			get { return _dATEPICKER; }
			set
			{
				if ((_dATEPICKER == null) || (value == null) || (!value.Equals(_dATEPICKER)))
				{
                    object oldValue = _dATEPICKER;
					_dATEPICKER = value;
					RaisePropertyChanged(EDW_RPT_DIMENSION.Prop_DATEPICKER, oldValue, value);
				}
			}
		}

		#endregion
	} // EDW_RPT_DIMENSION
}

