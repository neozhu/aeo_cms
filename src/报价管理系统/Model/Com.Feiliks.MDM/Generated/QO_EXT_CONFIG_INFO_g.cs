// Business class QO_EXT_CONFIG_INFO generated from QO_EXT_CONFIG_INFO
// Creator: rw
// Created Date: [2017-09-28]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("QO_EXT_CONFIG_INFO")]
	public partial class QO_EXT_CONFIG_INFO : EntityBase<QO_EXT_CONFIG_INFO>
	{
		#region Property_Names

		public static string Prop_INFONAME = "INFONAME";
		public static string Prop_INFOCODE = "INFOCODE";
		public static string Prop_INFOTYPE = "INFOTYPE";
		public static string Prop_MDMKEY = "MDMKEY";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
        public static string Prop_FIELDNAME = "FIELDNAME";

        public static string Prop_SORD = "SORD";
        

		#endregion

		#region Private_Variables

		private string _iNFONAME;
		private string _iNFOCODE;
		private string _iNFOTYPE;
		private string _mDMKEY;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
        private string _fIELDNAME;

        private string _sORD;

		#endregion

		#region Constructors

		public QO_EXT_CONFIG_INFO()
		{
		}

		public QO_EXT_CONFIG_INFO(
			string p_iNFONAME,
			string p_iNFOCODE,
			string p_iNFOTYPE,
			string p_mDMKEY,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
            string p_fIELDNAME,
            string p_sORD)
		{
			_iNFONAME = p_iNFONAME;
			_iNFOCODE = p_iNFOCODE;
			_iNFOTYPE = p_iNFOTYPE;
			_mDMKEY = p_mDMKEY;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
            _fIELDNAME = p_fIELDNAME;
            _sORD = p_sORD;
		}

		#endregion

		#region Properties

		[Property("INFONAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string INFONAME
		{
			get { return _iNFONAME; }
			set
			{
				if ((_iNFONAME == null) || (value == null) || (!value.Equals(_iNFONAME)))
				{
                    object oldValue = _iNFONAME;
					_iNFONAME = value;
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_INFONAME, oldValue, value);
				}
			}
		}

		[Property("INFOCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string INFOCODE
		{
			get { return _iNFOCODE; }
			set
			{
				if ((_iNFOCODE == null) || (value == null) || (!value.Equals(_iNFOCODE)))
				{
                    object oldValue = _iNFOCODE;
					_iNFOCODE = value;
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_INFOCODE, oldValue, value);
				}
			}
		}

		[Property("INFOTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string INFOTYPE
		{
			get { return _iNFOTYPE; }
			set
			{
				if ((_iNFOTYPE == null) || (value == null) || (!value.Equals(_iNFOTYPE)))
				{
                    object oldValue = _iNFOTYPE;
					_iNFOTYPE = value;
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_INFOTYPE, oldValue, value);
				}
			}
		}

		[Property("MDMKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MDMKEY
		{
			get { return _mDMKEY; }
			set
			{
				if ((_mDMKEY == null) || (value == null) || (!value.Equals(_mDMKEY)))
				{
                    object oldValue = _mDMKEY;
					_mDMKEY = value;
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_MDMKEY, oldValue, value);
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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_CREATETIME, oldValue, value);
				}
			}
		}

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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_MEMO, oldValue, value);
				}
			}
		}

        [Property("FIELDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string FIELDNAME
        {
            get { return _fIELDNAME; }
            set
            {
                if ((_fIELDNAME == null) || (value == null) || (!value.Equals(_fIELDNAME)))
                {
                    object oldValue = _fIELDNAME;
                    _fIELDNAME = value;
                    RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_FIELDNAME, _fIELDNAME, value);
                }
            }
        }

        [Property("SORD", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public string SORD
        {
            get { return _sORD; }
            set
            {
                if ((_sORD == null) || (value == null) || (!value.Equals(_sORD)))
                {
                    object oldValue = _sORD;
                    _sORD = value;
                    RaisePropertyChanged(QO_EXT_CONFIG_INFO.Prop_SORD, _sORD, value);
                }
            }
        }

		#endregion
	} // QO_EXT_CONFIG_INFO
}

