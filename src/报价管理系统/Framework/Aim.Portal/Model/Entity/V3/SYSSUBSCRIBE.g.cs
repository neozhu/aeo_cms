// Business class SYSSUBSCRIBE generated from SYSSUBSCRIBE
// Creator: Ray
// Created Date: [2014-05-19]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("SYSSUBSCRIBE")]
	public partial class SYSSUBSCRIBE : ModelBase<SYSSUBSCRIBE>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_TITLE = "TITLE";
		public static string Prop_EMAIL = "EMAIL";
		public static string Prop_LISTURL = "LISTURL";
		public static string Prop_CONDITION = "CONDITION";
		public static string Prop_USERID = "USERID";
		public static string Prop_USERNAME = "USERNAME";
		public static string Prop_USERLOGINNAME = "USERLOGINNAME";
		public static string Prop_TASKMODE = "TASKMODE";
		public static string Prop_TASKTIME = "TASKTIME";
		public static string Prop_TASKRULE = "TASKRULE";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_SIMPJG = "SIMPJG";
		public static string Prop_SIMPZXCS = "SIMPZXCS";
		public static string Prop_CRONFSPL = "CRONFSPL";
		public static string Prop_CRONWEEK1 = "CRONWEEK1";
		public static string Prop_CRONWEEK2 = "CRONWEEK2";
		public static string Prop_CRONWEEK3 = "CRONWEEK3";
		public static string Prop_CRONWEEK4 = "CRONWEEK4";
		public static string Prop_CRONWEEK5 = "CRONWEEK5";
		public static string Prop_CRONWEEK6 = "CRONWEEK6";
		public static string Prop_CRONWEEK7 = "CRONWEEK7";
		public static string Prop_MYZXTS = "MYZXTS";
		public static string Prop_CRONMTTYPE = "CRONMTTYPE";
		public static string Prop_MYMTZXYC = "MYMTZXYC";
		public static string Prop_MYMTZXDCM = "MYMTZXDCM";
		public static string Prop_XZMTZXSJ = "XZMTZXSJ";
		public static string Prop_XZMTKSSJ = "XZMTKSSJ";
		public static string Prop_XZMTJSSJ = "XZMTJSSJ";
		public static string Prop_YXQKS = "YXQKS";
		public static string Prop_XZYXQJS = "XZYXQJS";
		public static string Prop_YXQJS = "YXQJS";

		#endregion

		#region Private_Variables

		private string _id;
		private string _tITLE;
		private string _eMAIL;
		private string _lISTURL;
		private string _cONDITION;
		private string _uSERID;
		private string _uSERNAME;
		private string _uSERLOGINNAME;
		private string _tASKMODE;
		private string _tASKTIME;
		private string _tASKRULE;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private System.Decimal? _sIMPJG;
		private System.Decimal? _sIMPZXCS;
		private string _cRONFSPL;
		private string _cRONWEEK1;
		private string _cRONWEEK2;
		private string _cRONWEEK3;
		private string _cRONWEEK4;
		private string _cRONWEEK5;
		private string _cRONWEEK6;
		private string _cRONWEEK7;
		private string _mYZXTS;
		private string _cRONMTTYPE;
		private string _mYMTZXYC;
		private System.Decimal? _mYMTZXDCM;
		private string _xZMTZXSJ;
		private string _xZMTKSSJ;
		private string _xZMTJSSJ;
		private DateTime? _yXQKS;
		private string _xZYXQJS;
		private DateTime? _yXQJS;


		#endregion

		#region Constructors

		public SYSSUBSCRIBE()
		{
		}

		public SYSSUBSCRIBE(
			string p_id,
			string p_tITLE,
			string p_eMAIL,
			string p_lISTURL,
			string p_cONDITION,
			string p_uSERID,
			string p_uSERNAME,
			string p_uSERLOGINNAME,
			string p_tASKMODE,
			string p_tASKTIME,
			string p_tASKRULE,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			System.Decimal? p_sIMPJG,
			System.Decimal? p_sIMPZXCS,
			string p_cRONFSPL,
			string p_cRONWEEK1,
			string p_cRONWEEK2,
			string p_cRONWEEK3,
			string p_cRONWEEK4,
			string p_cRONWEEK5,
			string p_cRONWEEK6,
			string p_cRONWEEK7,
			string p_mYZXTS,
			string p_cRONMTTYPE,
			string p_mYMTZXYC,
			System.Decimal? p_mYMTZXDCM,
			string p_xZMTZXSJ,
			string p_xZMTKSSJ,
			string p_xZMTJSSJ,
			DateTime? p_yXQKS,
			string p_xZYXQJS,
			DateTime? p_yXQJS)
		{
			_id = p_id;
			_tITLE = p_tITLE;
			_eMAIL = p_eMAIL;
			_lISTURL = p_lISTURL;
			_cONDITION = p_cONDITION;
			_uSERID = p_uSERID;
			_uSERNAME = p_uSERNAME;
			_uSERLOGINNAME = p_uSERLOGINNAME;
			_tASKMODE = p_tASKMODE;
			_tASKTIME = p_tASKTIME;
			_tASKRULE = p_tASKRULE;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
			_sIMPJG = p_sIMPJG;
			_sIMPZXCS = p_sIMPZXCS;
			_cRONFSPL = p_cRONFSPL;
			_cRONWEEK1 = p_cRONWEEK1;
			_cRONWEEK2 = p_cRONWEEK2;
			_cRONWEEK3 = p_cRONWEEK3;
			_cRONWEEK4 = p_cRONWEEK4;
			_cRONWEEK5 = p_cRONWEEK5;
			_cRONWEEK6 = p_cRONWEEK6;
			_cRONWEEK7 = p_cRONWEEK7;
			_mYZXTS = p_mYZXTS;
			_cRONMTTYPE = p_cRONMTTYPE;
			_mYMTZXYC = p_mYMTZXYC;
			_mYMTZXDCM = p_mYMTZXDCM;
			_xZMTZXSJ = p_xZMTZXSJ;
			_xZMTKSSJ = p_xZMTKSSJ;
			_xZMTJSSJ = p_xZMTJSSJ;
			_yXQKS = p_yXQKS;
			_xZYXQJS = p_xZYXQJS;
			_yXQJS = p_yXQJS;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TITLE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string TITLE
		{
			get { return _tITLE; }
			set
			{
				if ((_tITLE == null) || (value == null) || (!value.Equals(_tITLE)))
				{
                    object oldValue = _tITLE;
					_tITLE = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_TITLE, oldValue, value);
				}
			}

		}

		[Property("EMAIL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string EMAIL
		{
			get { return _eMAIL; }
			set
			{
				if ((_eMAIL == null) || (value == null) || (!value.Equals(_eMAIL)))
				{
                    object oldValue = _eMAIL;
					_eMAIL = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_EMAIL, oldValue, value);
				}
			}

		}

		[Property("LISTURL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string LISTURL
		{
			get { return _lISTURL; }
			set
			{
				if ((_lISTURL == null) || (value == null) || (!value.Equals(_lISTURL)))
				{
                    object oldValue = _lISTURL;
					_lISTURL = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_LISTURL, oldValue, value);
				}
			}

		}

		[Property("CONDITION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string CONDITION
		{
			get { return _cONDITION; }
			set
			{
				if ((_cONDITION == null) || (value == null) || (!value.Equals(_cONDITION)))
				{
                    object oldValue = _cONDITION;
					_cONDITION = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CONDITION, oldValue, value);
				}
			}

		}

		[Property("USERID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string USERID
		{
			get { return _uSERID; }
			set
			{
				if ((_uSERID == null) || (value == null) || (!value.Equals(_uSERID)))
				{
                    object oldValue = _uSERID;
					_uSERID = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_USERID, oldValue, value);
				}
			}

		}

		[Property("USERNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string USERNAME
		{
			get { return _uSERNAME; }
			set
			{
				if ((_uSERNAME == null) || (value == null) || (!value.Equals(_uSERNAME)))
				{
                    object oldValue = _uSERNAME;
					_uSERNAME = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_USERNAME, oldValue, value);
				}
			}

		}

		[Property("USERLOGINNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string USERLOGINNAME
		{
			get { return _uSERLOGINNAME; }
			set
			{
				if ((_uSERLOGINNAME == null) || (value == null) || (!value.Equals(_uSERLOGINNAME)))
				{
                    object oldValue = _uSERLOGINNAME;
					_uSERLOGINNAME = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_USERLOGINNAME, oldValue, value);
				}
			}

		}

		[Property("TASKMODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TASKMODE
		{
			get { return _tASKMODE; }
			set
			{
				if ((_tASKMODE == null) || (value == null) || (!value.Equals(_tASKMODE)))
				{
                    object oldValue = _tASKMODE;
					_tASKMODE = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_TASKMODE, oldValue, value);
				}
			}

		}

		[Property("TASKTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TASKTIME
		{
			get { return _tASKTIME; }
			set
			{
				if ((_tASKTIME == null) || (value == null) || (!value.Equals(_tASKTIME)))
				{
                    object oldValue = _tASKTIME;
					_tASKTIME = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_TASKTIME, oldValue, value);
				}
			}

		}

		[Property("TASKRULE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string TASKRULE
		{
			get { return _tASKRULE; }
			set
			{
				if ((_tASKRULE == null) || (value == null) || (!value.Equals(_tASKRULE)))
				{
                    object oldValue = _tASKRULE;
					_tASKRULE = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_TASKRULE, oldValue, value);
				}
			}

		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CREATEID, oldValue, value);
				}
			}

		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_EXT2, oldValue, value);
				}
			}

		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_EXT3, oldValue, value);
				}
			}

		}

		[Property("EXT4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT4
		{
			get { return _eXT4; }
			set
			{
				if ((_eXT4 == null) || (value == null) || (!value.Equals(_eXT4)))
				{
                    object oldValue = _eXT4;
					_eXT4 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_EXT4, oldValue, value);
				}
			}

		}

		[Property("SIMPJG", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SIMPJG
		{
			get { return _sIMPJG; }
			set
			{
				if (value != _sIMPJG)
				{
                    object oldValue = _sIMPJG;
					_sIMPJG = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_SIMPJG, oldValue, value);
				}
			}

		}

		[Property("SIMPZXCS", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SIMPZXCS
		{
			get { return _sIMPZXCS; }
			set
			{
				if (value != _sIMPZXCS)
				{
                    object oldValue = _sIMPZXCS;
					_sIMPZXCS = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_SIMPZXCS, oldValue, value);
				}
			}

		}

		[Property("CRONFSPL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string CRONFSPL
		{
			get { return _cRONFSPL; }
			set
			{
				if ((_cRONFSPL == null) || (value == null) || (!value.Equals(_cRONFSPL)))
				{
                    object oldValue = _cRONFSPL;
					_cRONFSPL = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONFSPL, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK1
		{
			get { return _cRONWEEK1; }
			set
			{
				if ((_cRONWEEK1 == null) || (value == null) || (!value.Equals(_cRONWEEK1)))
				{
                    object oldValue = _cRONWEEK1;
					_cRONWEEK1 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK1, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK2
		{
			get { return _cRONWEEK2; }
			set
			{
				if ((_cRONWEEK2 == null) || (value == null) || (!value.Equals(_cRONWEEK2)))
				{
                    object oldValue = _cRONWEEK2;
					_cRONWEEK2 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK2, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK3
		{
			get { return _cRONWEEK3; }
			set
			{
				if ((_cRONWEEK3 == null) || (value == null) || (!value.Equals(_cRONWEEK3)))
				{
                    object oldValue = _cRONWEEK3;
					_cRONWEEK3 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK3, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK4
		{
			get { return _cRONWEEK4; }
			set
			{
				if ((_cRONWEEK4 == null) || (value == null) || (!value.Equals(_cRONWEEK4)))
				{
                    object oldValue = _cRONWEEK4;
					_cRONWEEK4 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK4, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK5
		{
			get { return _cRONWEEK5; }
			set
			{
				if ((_cRONWEEK5 == null) || (value == null) || (!value.Equals(_cRONWEEK5)))
				{
                    object oldValue = _cRONWEEK5;
					_cRONWEEK5 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK5, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK6", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK6
		{
			get { return _cRONWEEK6; }
			set
			{
				if ((_cRONWEEK6 == null) || (value == null) || (!value.Equals(_cRONWEEK6)))
				{
                    object oldValue = _cRONWEEK6;
					_cRONWEEK6 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK6, oldValue, value);
				}
			}

		}

		[Property("CRONWEEK7", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONWEEK7
		{
			get { return _cRONWEEK7; }
			set
			{
				if ((_cRONWEEK7 == null) || (value == null) || (!value.Equals(_cRONWEEK7)))
				{
                    object oldValue = _cRONWEEK7;
					_cRONWEEK7 = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONWEEK7, oldValue, value);
				}
			}

		}

		[Property("MYZXTS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MYZXTS
		{
			get { return _mYZXTS; }
			set
			{
				if ((_mYZXTS == null) || (value == null) || (!value.Equals(_mYZXTS)))
				{
                    object oldValue = _mYZXTS;
					_mYZXTS = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_MYZXTS, oldValue, value);
				}
			}

		}

		[Property("CRONMTTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CRONMTTYPE
		{
			get { return _cRONMTTYPE; }
			set
			{
				if ((_cRONMTTYPE == null) || (value == null) || (!value.Equals(_cRONMTTYPE)))
				{
                    object oldValue = _cRONMTTYPE;
					_cRONMTTYPE = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_CRONMTTYPE, oldValue, value);
				}
			}

		}

		[Property("MYMTZXYC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string MYMTZXYC
		{
			get { return _mYMTZXYC; }
			set
			{
				if ((_mYMTZXYC == null) || (value == null) || (!value.Equals(_mYMTZXYC)))
				{
                    object oldValue = _mYMTZXYC;
					_mYMTZXYC = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_MYMTZXYC, oldValue, value);
				}
			}

		}

		[Property("MYMTZXDCM", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MYMTZXDCM
		{
			get { return _mYMTZXDCM; }
			set
			{
				if (value != _mYMTZXDCM)
				{
                    object oldValue = _mYMTZXDCM;
					_mYMTZXDCM = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_MYMTZXDCM, oldValue, value);
				}
			}

		}

		[Property("XZMTZXSJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string XZMTZXSJ
		{
			get { return _xZMTZXSJ; }
			set
			{
				if ((_xZMTZXSJ == null) || (value == null) || (!value.Equals(_xZMTZXSJ)))
				{
                    object oldValue = _xZMTZXSJ;
					_xZMTZXSJ = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_XZMTZXSJ, oldValue, value);
				}
			}

		}

		[Property("XZMTKSSJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string XZMTKSSJ
		{
			get { return _xZMTKSSJ; }
			set
			{
				if ((_xZMTKSSJ == null) || (value == null) || (!value.Equals(_xZMTKSSJ)))
				{
                    object oldValue = _xZMTKSSJ;
					_xZMTKSSJ = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_XZMTKSSJ, oldValue, value);
				}
			}

		}

		[Property("XZMTJSSJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string XZMTJSSJ
		{
			get { return _xZMTJSSJ; }
			set
			{
				if ((_xZMTJSSJ == null) || (value == null) || (!value.Equals(_xZMTJSSJ)))
				{
                    object oldValue = _xZMTJSSJ;
					_xZMTJSSJ = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_XZMTJSSJ, oldValue, value);
				}
			}

		}

		[Property("YXQKS", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? YXQKS
		{
			get { return _yXQKS; }
			set
			{
				if (value != _yXQKS)
				{
                    object oldValue = _yXQKS;
					_yXQKS = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_YXQKS, oldValue, value);
				}
			}

		}

		[Property("XZYXQJS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string XZYXQJS
		{
			get { return _xZYXQJS; }
			set
			{
				if ((_xZYXQJS == null) || (value == null) || (!value.Equals(_xZYXQJS)))
				{
                    object oldValue = _xZYXQJS;
					_xZYXQJS = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_XZYXQJS, oldValue, value);
				}
			}

		}

		[Property("YXQJS", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? YXQJS
		{
			get { return _yXQJS; }
			set
			{
				if (value != _yXQJS)
				{
                    object oldValue = _yXQJS;
					_yXQJS = value;
					RaisePropertyChanged(SYSSUBSCRIBE.Prop_YXQJS, oldValue, value);
				}
			}

		}

		#endregion
	} // SYSSUBSCRIBE
}

