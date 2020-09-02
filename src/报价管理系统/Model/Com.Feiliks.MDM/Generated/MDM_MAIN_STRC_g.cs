// Business class MDM_MAIN_STRC generated from MDM_MAIN_STRC
// Creator: rw
// Created Date: [2017-09-28]

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
	[ActiveRecord("MDM_MAIN_STRC")]
	public partial class MDM_MAIN_STRC : EntityBase<MDM_MAIN_STRC>
	{
		#region Property_Names

		public static string Prop_MDKEY = "MDKEY";
		public static string Prop_TABNAME = "TABNAME";
		public static string Prop_FIELDNAME = "FIELDNAME";
		public static string Prop_KEYFLAG = "KEYFLAG";
		public static string Prop_POSITION = "POSITION";
		public static string Prop_INTTYPE = "INTTYPE";
		public static string Prop_INTLEN = "INTLEN";
		public static string Prop_DDLANGUAGE = "DDLANGUAGE";
		public static string Prop_DDTEXT = "DDTEXT";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _mDKEY;
		private string _tABNAME;
		private string _fIELDNAME;
		private string _kEYFLAG;
		private System.Decimal? _pOSITION;
		private string _iNTTYPE;
		private System.Decimal? _iNTLEN;
		private string _dDLANGUAGE;
		private string _dDTEXT;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_MAIN_STRC()
		{
		}

		public MDM_MAIN_STRC(
			string p_mDKEY,
			string p_tABNAME,
			string p_fIELDNAME,
			string p_kEYFLAG,
			System.Decimal? p_pOSITION,
			string p_iNTTYPE,
			System.Decimal? p_iNTLEN,
			string p_dDLANGUAGE,
			string p_dDTEXT,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_mDKEY = p_mDKEY;
			_tABNAME = p_tABNAME;
			_fIELDNAME = p_fIELDNAME;
			_kEYFLAG = p_kEYFLAG;
			_pOSITION = p_pOSITION;
			_iNTTYPE = p_iNTTYPE;
			_iNTLEN = p_iNTLEN;
			_dDLANGUAGE = p_dDLANGUAGE;
			_dDTEXT = p_dDTEXT;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("MDKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MDKEY
		{
			get { return _mDKEY; }
			set
			{
				if ((_mDKEY == null) || (value == null) || (!value.Equals(_mDKEY)))
				{
                    object oldValue = _mDKEY;
					_mDKEY = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_MDKEY, oldValue, value);
				}
			}
		}

		[Property("TABNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 60)]
		public string TABNAME
		{
			get { return _tABNAME; }
			set
			{
				if ((_tABNAME == null) || (value == null) || (!value.Equals(_tABNAME)))
				{
                    object oldValue = _tABNAME;
					_tABNAME = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_TABNAME, oldValue, value);
				}
			}
		}

		[Property("FIELDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 60)]
		public string FIELDNAME
		{
			get { return _fIELDNAME; }
			set
			{
				if ((_fIELDNAME == null) || (value == null) || (!value.Equals(_fIELDNAME)))
				{
                    object oldValue = _fIELDNAME;
					_fIELDNAME = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_FIELDNAME, oldValue, value);
				}
			}
		}

		[Property("KEYFLAG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string KEYFLAG
		{
			get { return _kEYFLAG; }
			set
			{
				if ((_kEYFLAG == null) || (value == null) || (!value.Equals(_kEYFLAG)))
				{
                    object oldValue = _kEYFLAG;
					_kEYFLAG = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_KEYFLAG, oldValue, value);
				}
			}
		}

		[Property("POSITION", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? POSITION
		{
			get { return _pOSITION; }
			set
			{
				if (value != _pOSITION)
				{
                    object oldValue = _pOSITION;
					_pOSITION = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_POSITION, oldValue, value);
				}
			}
		}

		[Property("INTTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string INTTYPE
		{
			get { return _iNTTYPE; }
			set
			{
				if ((_iNTTYPE == null) || (value == null) || (!value.Equals(_iNTTYPE)))
				{
                    object oldValue = _iNTTYPE;
					_iNTTYPE = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_INTTYPE, oldValue, value);
				}
			}
		}

		[Property("INTLEN", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? INTLEN
		{
			get { return _iNTLEN; }
			set
			{
				if (value != _iNTLEN)
				{
                    object oldValue = _iNTLEN;
					_iNTLEN = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_INTLEN, oldValue, value);
				}
			}
		}

		[Property("DDLANGUAGE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string DDLANGUAGE
		{
			get { return _dDLANGUAGE; }
			set
			{
				if ((_dDLANGUAGE == null) || (value == null) || (!value.Equals(_dDLANGUAGE)))
				{
                    object oldValue = _dDLANGUAGE;
					_dDLANGUAGE = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_DDLANGUAGE, oldValue, value);
				}
			}
		}

		[Property("DDTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 110)]
		public string DDTEXT
		{
			get { return _dDTEXT; }
			set
			{
				if ((_dDTEXT == null) || (value == null) || (!value.Equals(_dDTEXT)))
				{
                    object oldValue = _dDTEXT;
					_dDTEXT = value;
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_DDTEXT, oldValue, value);
				}
			}
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_STRC.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_MAIN_STRC
}

