// Business class QO_ET_SDPZ_S generated from QO_ET_SDPZ_S
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
	[ActiveRecord("QO_ET_SDPZ_S")]
	public partial class QO_ET_SDPZ_S : EntityBase<QO_ET_SDPZ_S>
	{
		#region Property_Names

		public static string Prop_ZTYPE = "ZTYPE";
		public static string Prop_ZTYPE_YW = "ZTYPE_YW";
		public static string Prop_ZTYPE_YWMS = "ZTYPE_YWMS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _zTYPE;
		private string _zTYPE_YW;
		private string _zTYPE_YWMS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public QO_ET_SDPZ_S()
		{
		}

		public QO_ET_SDPZ_S(
			string p_zTYPE,
			string p_zTYPE_YW,
			string p_zTYPE_YWMS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_zTYPE = p_zTYPE;
			_zTYPE_YW = p_zTYPE_YW;
			_zTYPE_YWMS = p_zTYPE_YWMS;
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

		[Property("ZTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ZTYPE
		{
			get { return _zTYPE; }
			set
			{
				if ((_zTYPE == null) || (value == null) || (!value.Equals(_zTYPE)))
				{
                    object oldValue = _zTYPE;
					_zTYPE = value;
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_ZTYPE, oldValue, value);
				}
			}
		}

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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_ZTYPE_YW, oldValue, value);
				}
			}
		}

		[Property("ZTYPE_YWMS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ZTYPE_YWMS
		{
			get { return _zTYPE_YWMS; }
			set
			{
				if ((_zTYPE_YWMS == null) || (value == null) || (!value.Equals(_zTYPE_YWMS)))
				{
                    object oldValue = _zTYPE_YWMS;
					_zTYPE_YWMS = value;
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_ZTYPE_YWMS, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(QO_ET_SDPZ_S.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // QO_ET_SDPZ_S
}

