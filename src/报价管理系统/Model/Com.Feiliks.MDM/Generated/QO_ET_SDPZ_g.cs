// Business class QO_ET_SDPZ generated from QO_ET_SDPZ
// Creator: rw
// Created Date: [2017-11-14]

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
	[ActiveRecord("QO_ET_SDPZ")]
	public partial class QO_ET_SDPZ : EntityBase<QO_ET_SDPZ>
	{
		#region Property_Names

		public static string Prop_ZTYPE_YW = "ZTYPE_YW";
		public static string Prop_NODE_NAME = "NODE_NAME";
		public static string Prop_FIELD = "FIELD";
		public static string Prop_ZLOCK_INTERNAL = "ZLOCK_INTERNAL";
		public static string Prop_ZLOCK_EXTERNAL = "ZLOCK_EXTERNAL";
		public static string Prop_FIELD_DES = "FIELD_DES";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _zTYPE_YW;
		private string _nODE_NAME;
		private string _fIELD;
		private string _zLOCK_INTERNAL;
		private string _zLOCK_EXTERNAL;
		private string _fIELD_DES;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public QO_ET_SDPZ()
		{
		}

		public QO_ET_SDPZ(
			string p_zTYPE_YW,
			string p_nODE_NAME,
			string p_fIELD,
			string p_zLOCK_INTERNAL,
			string p_zLOCK_EXTERNAL,
			string p_fIELD_DES,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_zTYPE_YW = p_zTYPE_YW;
			_nODE_NAME = p_nODE_NAME;
			_fIELD = p_fIELD;
			_zLOCK_INTERNAL = p_zLOCK_INTERNAL;
			_zLOCK_EXTERNAL = p_zLOCK_EXTERNAL;
			_fIELD_DES = p_fIELD_DES;
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

		[Property("ZTYPE_YW", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ZTYPE_YW
		{
			get { return _zTYPE_YW; }
			set
			{
				if ((_zTYPE_YW == null) || (value == null) || (!value.Equals(_zTYPE_YW)))
				{
                    object oldValue = _zTYPE_YW;
					_zTYPE_YW = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_ZTYPE_YW, oldValue, value);
				}
			}
		}

		[Property("NODE_NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string NODE_NAME
		{
			get { return _nODE_NAME; }
			set
			{
				if ((_nODE_NAME == null) || (value == null) || (!value.Equals(_nODE_NAME)))
				{
                    object oldValue = _nODE_NAME;
					_nODE_NAME = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_NODE_NAME, oldValue, value);
				}
			}
		}

		[Property("FIELD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string FIELD
		{
			get { return _fIELD; }
			set
			{
				if ((_fIELD == null) || (value == null) || (!value.Equals(_fIELD)))
				{
                    object oldValue = _fIELD;
					_fIELD = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_FIELD, oldValue, value);
				}
			}
		}

		[Property("ZLOCK_INTERNAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ZLOCK_INTERNAL
		{
			get { return _zLOCK_INTERNAL; }
			set
			{
				if ((_zLOCK_INTERNAL == null) || (value == null) || (!value.Equals(_zLOCK_INTERNAL)))
				{
                    object oldValue = _zLOCK_INTERNAL;
					_zLOCK_INTERNAL = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_ZLOCK_INTERNAL, oldValue, value);
				}
			}
		}

		[Property("ZLOCK_EXTERNAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ZLOCK_EXTERNAL
		{
			get { return _zLOCK_EXTERNAL; }
			set
			{
				if ((_zLOCK_EXTERNAL == null) || (value == null) || (!value.Equals(_zLOCK_EXTERNAL)))
				{
                    object oldValue = _zLOCK_EXTERNAL;
					_zLOCK_EXTERNAL = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_ZLOCK_EXTERNAL, oldValue, value);
				}
			}
		}

		[Property("FIELD_DES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FIELD_DES
		{
			get { return _fIELD_DES; }
			set
			{
				if ((_fIELD_DES == null) || (value == null) || (!value.Equals(_fIELD_DES)))
				{
                    object oldValue = _fIELD_DES;
					_fIELD_DES = value;
					RaisePropertyChanged(QO_ET_SDPZ.Prop_FIELD_DES, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // QO_ET_SDPZ
}

