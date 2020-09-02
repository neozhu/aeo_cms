// Business class MDM_LOC generated from MDM_LOC
// Creator: rw
// Created Date: [2017-10-25]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
using System.Xml.Serialization;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_LOC")]
	public partial class MDM_LOC : EntityBase<MDM_LOC>
	{
		#region Property_Names

		public static string Prop_LOCID = "LOCID";
		public static string Prop_LOCNO = "LOCNO";
		public static string Prop_LOCTYPE = "LOCTYPE";
		public static string Prop_DESCR40 = "DESCR40";
		public static string Prop_PARTNER = "PARTNER";
		public static string Prop_RID = "RID";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _lOCID;
		private string _lOCNO;
		private string _lOCTYPE;
		private string _dESCR40;
		private string _pARTNER;
		private string _rid;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_LOC()
		{
		}

		public MDM_LOC(
			string p_lOCID,
			string p_lOCNO,
			string p_lOCTYPE,
			string p_dESCR40,
			string p_pARTNER,
			string p_rid,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_sTATUS,
			string p_mEMO)
		{
			_lOCID = p_lOCID;
			_lOCNO = p_lOCNO;
			_lOCTYPE = p_lOCTYPE;
			_dESCR40 = p_dESCR40;
			_pARTNER = p_pARTNER;
			_rid = p_rid;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("LOCID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string LOCID
		{
			get { return _lOCID; }
			set
			{
				if ((_lOCID == null) || (value == null) || (!value.Equals(_lOCID)))
				{
                    object oldValue = _lOCID;
					_lOCID = value;
					RaisePropertyChanged(MDM_LOC.Prop_LOCID, oldValue, value);
				}
			}
		}

		[Property("LOCNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string LOCNO
		{
			get { return _lOCNO; }
			set
			{
				if ((_lOCNO == null) || (value == null) || (!value.Equals(_lOCNO)))
				{
                    object oldValue = _lOCNO;
					_lOCNO = value;
					RaisePropertyChanged(MDM_LOC.Prop_LOCNO, oldValue, value);
				}
			}
		}

		[Property("LOCTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string LOCTYPE
		{
			get { return _lOCTYPE; }
			set
			{
				if ((_lOCTYPE == null) || (value == null) || (!value.Equals(_lOCTYPE)))
				{
                    object oldValue = _lOCTYPE;
					_lOCTYPE = value;
					RaisePropertyChanged(MDM_LOC.Prop_LOCTYPE, oldValue, value);
				}
			}
		}

		[Property("DESCR40", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string DESCR40
		{
			get { return _dESCR40; }
			set
			{
				if ((_dESCR40 == null) || (value == null) || (!value.Equals(_dESCR40)))
				{
                    object oldValue = _dESCR40;
					_dESCR40 = value;
					RaisePropertyChanged(MDM_LOC.Prop_DESCR40, oldValue, value);
				}
			}
		}

		[Property("PARTNER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string PARTNER
		{
			get { return _pARTNER; }
			set
			{
				if ((_pARTNER == null) || (value == null) || (!value.Equals(_pARTNER)))
				{
                    object oldValue = _pARTNER;
					_pARTNER = value;
					RaisePropertyChanged(MDM_LOC.Prop_PARTNER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_LOC.Prop_CREATETIME, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(MDM_LOC.Prop_CREATEUSER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_LOC.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(MDM_LOC.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_LOC.Prop_STATUS, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_LOC.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_LOC
}

