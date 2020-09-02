// Business class SQM_BJ_CZXG generated from SQM_BJ_CZXG
// Creator: rw
// Created Date: [2018-08-29]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_BJ_CZXG")]
	public partial class SQM_BJ_CZXG : EntityBase<SQM_BJ_CZXG>
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
		public static string Prop_CZBY = "CZBY";
		public static string Prop_YZD = "YZD";
		public static string Prop_BYFY = "BYFY";
		public static string Prop_DTCK = "DTCK";
		public static string Prop_MZTS = "MZTS";
		public static string Prop_BJRID = "BJRID";
		public static string Prop_FEECODE = "FEECODE";
		public static string Prop_DJFSRID = "DJFSRID";
		public static string Prop_GDZRID = "GDZRID";

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
		private string _cZBY;
		private string _yZD;
		private string _bYFY;
		private string _dTCK;
		private string _mZTS;
		private string _bJRID;
		private string _fEECODE;
		private string _dJFSRID;
		private string _gDZRID;


		#endregion

		#region Constructors

		public SQM_BJ_CZXG()
		{
		}

		public SQM_BJ_CZXG(
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_cZBY,
			string p_yZD,
			string p_bYFY,
			string p_dTCK,
			string p_mZTS,
			string p_bJRID,
			string p_fEECODE,
			string p_dJFSRID,
			string p_gDZRID)
		{
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_cZBY = p_cZBY;
			_yZD = p_yZD;
			_bYFY = p_bYFY;
			_dTCK = p_dTCK;
			_mZTS = p_mZTS;
			_bJRID = p_bJRID;
			_fEECODE = p_fEECODE;
			_dJFSRID = p_dJFSRID;
			_gDZRID = p_gDZRID;
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("CZBY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CZBY
		{
			get { return _cZBY; }
			set
			{
				if ((_cZBY == null) || (value == null) || (!value.Equals(_cZBY)))
				{
                    object oldValue = _cZBY;
					_cZBY = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_CZBY, oldValue, value);
				}
			}
		}

		[Property("YZD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string YZD
		{
			get { return _yZD; }
			set
			{
				if ((_yZD == null) || (value == null) || (!value.Equals(_yZD)))
				{
                    object oldValue = _yZD;
					_yZD = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_YZD, oldValue, value);
				}
			}
		}

		[Property("BYFY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BYFY
		{
			get { return _bYFY; }
			set
			{
				if ((_bYFY == null) || (value == null) || (!value.Equals(_bYFY)))
				{
                    object oldValue = _bYFY;
					_bYFY = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_BYFY, oldValue, value);
				}
			}
		}

		[Property("DTCK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DTCK
		{
			get { return _dTCK; }
			set
			{
				if ((_dTCK == null) || (value == null) || (!value.Equals(_dTCK)))
				{
                    object oldValue = _dTCK;
					_dTCK = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_DTCK, oldValue, value);
				}
			}
		}

		[Property("MZTS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MZTS
		{
			get { return _mZTS; }
			set
			{
				if ((_mZTS == null) || (value == null) || (!value.Equals(_mZTS)))
				{
                    object oldValue = _mZTS;
					_mZTS = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_MZTS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_BJRID, oldValue, value);
				}
			}
		}

		[Property("FEECODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEECODE
		{
			get { return _fEECODE; }
			set
			{
				if ((_fEECODE == null) || (value == null) || (!value.Equals(_fEECODE)))
				{
                    object oldValue = _fEECODE;
					_fEECODE = value;
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_FEECODE, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_DJFSRID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZXG.Prop_GDZRID, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_BJ_CZXG
}

