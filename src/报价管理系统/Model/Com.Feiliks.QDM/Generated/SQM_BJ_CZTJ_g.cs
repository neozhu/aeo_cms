// Business class SQM_BJ_CZTJ generated from SQM_BJ_CZTJ
// Creator: rw
// Created Date: [2018-08-28]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_BJ_CZTJ")]
	public partial class SQM_BJ_CZTJ : EntityBase<SQM_BJ_CZTJ>
	{
		#region Property_Names

		public static string Prop_TJMCKEY = "TJMCKEY";
		public static string Prop_TJTYPEKEY = "TJTYPEKEY";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_TJMC = "TJMC";
		public static string Prop_TJTYPE = "TJTYPE";
		public static string Prop_WDZ = "WDZ";
		public static string Prop_FEECODE = "FEECODE";
		public static string Prop_DJFSRID = "DJFSRID";
		public static string Prop_GDZRID = "GDZRID";
		public static string Prop_BJRID = "BJRID";

		#endregion

		#region Private_Variables

		private string _tJMCKEY;
		private string _tJTYPEKEY;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _tJMC;
		private string _tJTYPE;
		private string _wDZ;
		private string _fEECODE;
		private string _dJFSRID;
		private string _gDZRID;
		private string _bJRID;


		#endregion

		#region Constructors

		public SQM_BJ_CZTJ()
		{
		}

		public SQM_BJ_CZTJ(
			string p_tJMCKEY,
			string p_tJTYPEKEY,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_tJMC,
			string p_tJTYPE,
			string p_wDZ,
			string p_fEECODE,
			string p_dJFSRID,
			string p_gDZRID,
			string p_bJRID)
		{
			_tJMCKEY = p_tJMCKEY;
			_tJTYPEKEY = p_tJTYPEKEY;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_tJMC = p_tJMC;
			_tJTYPE = p_tJTYPE;
			_wDZ = p_wDZ;
			_fEECODE = p_fEECODE;
			_dJFSRID = p_dJFSRID;
			_gDZRID = p_gDZRID;
			_bJRID = p_bJRID;
		}

		#endregion

		#region Properties

		[Property("TJMCKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TJMCKEY
		{
			get { return _tJMCKEY; }
			set
			{
				if ((_tJMCKEY == null) || (value == null) || (!value.Equals(_tJMCKEY)))
				{
                    object oldValue = _tJMCKEY;
					_tJMCKEY = value;
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_TJMCKEY, oldValue, value);
				}
			}
		}

		[Property("TJTYPEKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TJTYPEKEY
		{
			get { return _tJTYPEKEY; }
			set
			{
				if ((_tJTYPEKEY == null) || (value == null) || (!value.Equals(_tJTYPEKEY)))
				{
                    object oldValue = _tJTYPEKEY;
					_tJTYPEKEY = value;
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_TJTYPEKEY, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("TJMC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TJMC
		{
			get { return _tJMC; }
			set
			{
				if ((_tJMC == null) || (value == null) || (!value.Equals(_tJMC)))
				{
                    object oldValue = _tJMC;
					_tJMC = value;
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_TJMC, oldValue, value);
				}
			}
		}

		[Property("TJTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TJTYPE
		{
			get { return _tJTYPE; }
			set
			{
				if ((_tJTYPE == null) || (value == null) || (!value.Equals(_tJTYPE)))
				{
                    object oldValue = _tJTYPE;
					_tJTYPE = value;
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_TJTYPE, oldValue, value);
				}
			}
		}

		[Property("WDZ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string WDZ
		{
			get { return _wDZ; }
			set
			{
				if ((_wDZ == null) || (value == null) || (!value.Equals(_wDZ)))
				{
                    object oldValue = _wDZ;
					_wDZ = value;
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_WDZ, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_FEECODE, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_DJFSRID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_GDZRID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_CZTJ.Prop_BJRID, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_BJ_CZTJ
}

