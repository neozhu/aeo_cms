// Business class QUESTIONHELP generated from QUESTIONHELP
// Created Date: [2014-04-16]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("QUESTIONHELP")]
	public partial class QUESTIONHELP : ModelBase<QUESTIONHELP>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_CODE = "CODE";
		public static string Prop_NODENAME = "NODENAME";
		public static string Prop_PARENTID = "PARENTID";
		public static string Prop_PATH = "PATH";
		public static string Prop_ISLEAF = "ISLEAF";
		public static string Prop_DEEPTH = "DEEPTH";
		public static string Prop_URLKEY = "URLKEY";
		public static string Prop_KEYWORD = "KEYWORD";
		public static string Prop_IMGFULLPATH = "IMGFULLPATH";
		public static string Prop_FILEITEMS = "FILEITEMS";
		public static string Prop_FILEFULLPATH = "FILEFULLPATH";
		public static string Prop_CONTEXT = "CONTEXT";
		public static string Prop_SORTINDEX = "SORTINDEX";
		public static string Prop_EXTJSON = "EXTJSON";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _id;
		private string _cODE;
		private string _nODENAME;
		private string _pARENTID;
		private string _pATH;
		private string _iSLEAF;
		private System.Decimal? _dEEPTH;
		private string _uRLKEY;
		private string _kEYWORD;
		private string _iMGFULLPATH;
		private string _fILEITEMS;
		private string _fILEFULLPATH;
		private string _cONTEXT;
		private System.Decimal? _sORTINDEX;
		private string _eXTJSON;
		private string _eXT1;
		private string _eXT2;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public QUESTIONHELP()
		{
		}

		public QUESTIONHELP(
			string p_id,
			string p_cODE,
			string p_nODENAME,
			string p_pARENTID,
			string p_pATH,
			string p_iSLEAF,
			System.Decimal? p_dEEPTH,
			string p_uRLKEY,
			string p_kEYWORD,
			string p_iMGFULLPATH,
			string p_fILEITEMS,
			string p_fILEFULLPATH,
			string p_cONTEXT,
			System.Decimal? p_sORTINDEX,
			string p_eXTJSON,
			string p_eXT1,
			string p_eXT2,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_id = p_id;
			_cODE = p_cODE;
			_nODENAME = p_nODENAME;
			_pARENTID = p_pARENTID;
			_pATH = p_pATH;
			_iSLEAF = p_iSLEAF;
			_dEEPTH = p_dEEPTH;
			_uRLKEY = p_uRLKEY;
			_kEYWORD = p_kEYWORD;
			_iMGFULLPATH = p_iMGFULLPATH;
			_fILEITEMS = p_fILEITEMS;
			_fILEFULLPATH = p_fILEFULLPATH;
			_cONTEXT = p_cONTEXT;
			_sORTINDEX = p_sORTINDEX;
			_eXTJSON = p_eXTJSON;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CODE
		{
			get { return _cODE; }
			set
			{
				if ((_cODE == null) || (value == null) || (!value.Equals(_cODE)))
				{
                    object oldValue = _cODE;
					_cODE = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_CODE, oldValue, value);
				}
			}

		}

		[Property("NODENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string NODENAME
		{
			get { return _nODENAME; }
			set
			{
				if ((_nODENAME == null) || (value == null) || (!value.Equals(_nODENAME)))
				{
                    object oldValue = _nODENAME;
					_nODENAME = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_NODENAME, oldValue, value);
				}
			}

		}

		[Property("PARENTID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string PARENTID
		{
			get { return _pARENTID; }
			set
			{
				if ((_pARENTID == null) || (value == null) || (!value.Equals(_pARENTID)))
				{
                    object oldValue = _pARENTID;
					_pARENTID = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_PARENTID, oldValue, value);
				}
			}

		}

		[Property("PATH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1500)]
		public string PATH
		{
			get { return _pATH; }
			set
			{
				if ((_pATH == null) || (value == null) || (!value.Equals(_pATH)))
				{
                    object oldValue = _pATH;
					_pATH = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_PATH, oldValue, value);
				}
			}

		}

		[Property("ISLEAF", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string ISLEAF
		{
			get { return _iSLEAF; }
			set
			{
				if ((_iSLEAF == null) || (value == null) || (!value.Equals(_iSLEAF)))
				{
                    object oldValue = _iSLEAF;
					_iSLEAF = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_ISLEAF, oldValue, value);
				}
			}

		}

		[Property("DEEPTH", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DEEPTH
		{
			get { return _dEEPTH; }
			set
			{
				if (value != _dEEPTH)
				{
                    object oldValue = _dEEPTH;
					_dEEPTH = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_DEEPTH, oldValue, value);
				}
			}

		}

		[Property("URLKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string URLKEY
		{
			get { return _uRLKEY; }
			set
			{
				if ((_uRLKEY == null) || (value == null) || (!value.Equals(_uRLKEY)))
				{
                    object oldValue = _uRLKEY;
					_uRLKEY = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_URLKEY, oldValue, value);
				}
			}

		}

		[Property("KEYWORD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string KEYWORD
		{
			get { return _kEYWORD; }
			set
			{
				if ((_kEYWORD == null) || (value == null) || (!value.Equals(_kEYWORD)))
				{
                    object oldValue = _kEYWORD;
					_kEYWORD = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_KEYWORD, oldValue, value);
				}
			}

		}

		[Property("IMGFULLPATH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string IMGFULLPATH
		{
			get { return _iMGFULLPATH; }
			set
			{
				if ((_iMGFULLPATH == null) || (value == null) || (!value.Equals(_iMGFULLPATH)))
				{
                    object oldValue = _iMGFULLPATH;
					_iMGFULLPATH = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_IMGFULLPATH, oldValue, value);
				}
			}

		}

		[Property("FILEITEMS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string FILEITEMS
		{
			get { return _fILEITEMS; }
			set
			{
				if ((_fILEITEMS == null) || (value == null) || (!value.Equals(_fILEITEMS)))
				{
                    object oldValue = _fILEITEMS;
					_fILEITEMS = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_FILEITEMS, oldValue, value);
				}
			}

		}

		[Property("FILEFULLPATH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string FILEFULLPATH
		{
			get { return _fILEFULLPATH; }
			set
			{
				if ((_fILEFULLPATH == null) || (value == null) || (!value.Equals(_fILEFULLPATH)))
				{
                    object oldValue = _fILEFULLPATH;
					_fILEFULLPATH = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_FILEFULLPATH, oldValue, value);
				}
			}

		}

		[Property("CONTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string CONTEXT
		{
			get { return _cONTEXT; }
			set
			{
				if ((_cONTEXT == null) || (value == null) || (!value.Equals(_cONTEXT)))
				{
                    object oldValue = _cONTEXT;
					_cONTEXT = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_CONTEXT, oldValue, value);
				}
			}

		}

		[Property("SORTINDEX", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SORTINDEX
		{
			get { return _sORTINDEX; }
			set
			{
				if (value != _sORTINDEX)
				{
                    object oldValue = _sORTINDEX;
					_sORTINDEX = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_SORTINDEX, oldValue, value);
				}
			}

		}

		[Property("EXTJSON", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string EXTJSON
		{
			get { return _eXTJSON; }
			set
			{
				if ((_eXTJSON == null) || (value == null) || (!value.Equals(_eXTJSON)))
				{
                    object oldValue = _eXTJSON;
					_eXTJSON = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_EXTJSON, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_EXT2, oldValue, value);
				}
			}

		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_CREATEID, oldValue, value);
				}
			}

		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(QUESTIONHELP.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(QUESTIONHELP.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // QUESTIONHELP
}

