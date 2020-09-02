// Business class SQM_FBREASON generated from SQM_FBREASON
// Creator: rw
// Created Date: [2019-08-26]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FBREASON")]
	public partial class SQM_FBREASON : EntityBase<SQM_FBREASON>
	{
		#region Property_Names

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
		public static string Prop_REASONCODE = "REASONCODE";
		public static string Prop_REASONNAME = "REASONNAME";

		#endregion

		#region Private_Variables

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
		private string _rEASONCODE;
		private string _rEASONNAME;


		#endregion

		#region Constructors

		public SQM_FBREASON()
		{
		}

		public SQM_FBREASON(
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
			string p_rEASONCODE,
			string p_rEASONNAME)
		{
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
			_rEASONCODE = p_rEASONCODE;
			_rEASONNAME = p_rEASONNAME;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_FBREASON.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_FBREASON.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_FBREASON.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FBREASON.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_FBREASON.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_FBREASON.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            get { return _rid; }
            set { _rid = value; }
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
					RaisePropertyChanged(SQM_FBREASON.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_FBREASON.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_FBREASON.Prop_SORD, oldValue, value);
				}
			}
		}

		[Property("REASONCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string REASONCODE
		{
			get { return _rEASONCODE; }
			set
			{
				if ((_rEASONCODE == null) || (value == null) || (!value.Equals(_rEASONCODE)))
				{
                    object oldValue = _rEASONCODE;
					_rEASONCODE = value;
					RaisePropertyChanged(SQM_FBREASON.Prop_REASONCODE, oldValue, value);
				}
			}
		}

		[Property("REASONNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string REASONNAME
		{
			get { return _rEASONNAME; }
			set
			{
				if ((_rEASONNAME == null) || (value == null) || (!value.Equals(_rEASONNAME)))
				{
                    object oldValue = _rEASONNAME;
					_rEASONNAME = value;
					RaisePropertyChanged(SQM_FBREASON.Prop_REASONNAME, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_FBREASON
}

