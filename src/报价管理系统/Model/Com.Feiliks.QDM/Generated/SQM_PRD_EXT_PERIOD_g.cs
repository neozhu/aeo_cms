// Business class SQM_PRD_EXT_PERIOD generated from SQM_PRD_EXT_PERIOD
// Creator: rw
// Created Date: [2018-04-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_PRD_EXT_PERIOD")]
	public partial class SQM_PRD_EXT_PERIOD : EntityBase<SQM_PRD_EXT_PERIOD>
	{
		#region Property_Names

		public static string Prop_PRODUCTKEY = "PRODUCTKEY";
		public static string Prop_DTFROM = "DTFROM";
		public static string Prop_DTTO = "DTTO";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _pRODUCTKEY;
		private DateTime? _dTFROM;
		private DateTime? _dTTO;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _mEMO;


		#endregion

		#region Constructors

		public SQM_PRD_EXT_PERIOD()
		{
		}

		public SQM_PRD_EXT_PERIOD(
			string p_pRODUCTKEY,
			DateTime? p_dTFROM,
			DateTime? p_dTTO,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO)
		{
			_pRODUCTKEY = p_pRODUCTKEY;
			_dTFROM = p_dTFROM;
			_dTTO = p_dTTO;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("PRODUCTKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRODUCTKEY
		{
			get { return _pRODUCTKEY; }
			set
			{
				if ((_pRODUCTKEY == null) || (value == null) || (!value.Equals(_pRODUCTKEY)))
				{
                    object oldValue = _pRODUCTKEY;
					_pRODUCTKEY = value;
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_PRODUCTKEY, oldValue, value);
				}
			}
		}

		[Property("DTFROM", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DTFROM
		{
			get { return _dTFROM; }
			set
			{
				if (value != _dTFROM)
				{
                    object oldValue = _dTFROM;
					_dTFROM = value;
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_DTFROM, oldValue, value);
				}
			}
		}

		[Property("DTTO", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DTTO
		{
			get { return _dTTO; }
			set
			{
				if (value != _dTTO)
				{
                    object oldValue = _dTTO;
					_dTTO = value;
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_DTTO, oldValue, value);
				}
			}
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(SQM_PRD_EXT_PERIOD.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_PRD_EXT_PERIOD
}

