// Business class LANGUAGESSET generated from LANGUAGESSET
// Created Date: [2014-04-15]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("LANGUAGESSET")]
    public partial class LANGUAGESSET : ModelBase<LANGUAGESSET>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_LANGCODE = "LANGCODE";
		public static string Prop_PREFIXURL = "PREFIXURL";
		public static string Prop_PREFIXCODE = "PREFIXCODE";
		public static string Prop_DATAKEY = "DATAKEY";
		public static string Prop_MIXDATAKEY = "MIXDATAKEY";
		public static string Prop_DATAVAL = "DATAVAL";
		public static string Prop_COMMENTS = "COMMENTS";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _id;
		private string _lANGCODE;
		private string _pREFIXURL;
		private string _pREFIXCODE;
		private string _dATAKEY;
		private string _mIXDATAKEY;
		private string _dATAVAL;
		private string _cOMMENTS;
		private string _eXT1;
		private string _eXT2;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public LANGUAGESSET()
		{
		}

		public LANGUAGESSET(
			string p_id,
			string p_lANGCODE,
			string p_pREFIXURL,
			string p_pREFIXCODE,
			string p_dATAKEY,
			string p_mIXDATAKEY,
			string p_dATAVAL,
			string p_cOMMENTS,
			string p_eXT1,
			string p_eXT2,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_id = p_id;
			_lANGCODE = p_lANGCODE;
			_pREFIXURL = p_pREFIXURL;
			_pREFIXCODE = p_pREFIXCODE;
			_dATAKEY = p_dATAKEY;
			_mIXDATAKEY = p_mIXDATAKEY;
			_dATAVAL = p_dATAVAL;
			_cOMMENTS = p_cOMMENTS;
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

		[Property("LANGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string LANGCODE
		{
			get { return _lANGCODE; }
			set
			{
				if ((_lANGCODE == null) || (value == null) || (!value.Equals(_lANGCODE)))
				{
                    object oldValue = _lANGCODE;
					_lANGCODE = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_LANGCODE, oldValue, value);
				}
			}

		}

		[Property("PREFIXURL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string PREFIXURL
		{
			get { return _pREFIXURL; }
			set
			{
				if ((_pREFIXURL == null) || (value == null) || (!value.Equals(_pREFIXURL)))
				{
                    object oldValue = _pREFIXURL;
					_pREFIXURL = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_PREFIXURL, oldValue, value);
				}
			}

		}

		[Property("PREFIXCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PREFIXCODE
		{
			get { return _pREFIXCODE; }
			set
			{
				if ((_pREFIXCODE == null) || (value == null) || (!value.Equals(_pREFIXCODE)))
				{
                    object oldValue = _pREFIXCODE;
					_pREFIXCODE = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_PREFIXCODE, oldValue, value);
				}
			}

		}

		[Property("DATAKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string DATAKEY
		{
			get { return _dATAKEY; }
			set
			{
				if ((_dATAKEY == null) || (value == null) || (!value.Equals(_dATAKEY)))
				{
                    object oldValue = _dATAKEY;
					_dATAKEY = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_DATAKEY, oldValue, value);
				}
			}

		}

		[Property("MIXDATAKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string MIXDATAKEY
		{
			get { return _mIXDATAKEY; }
			set
			{
				if ((_mIXDATAKEY == null) || (value == null) || (!value.Equals(_mIXDATAKEY)))
				{
                    object oldValue = _mIXDATAKEY;
					_mIXDATAKEY = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_MIXDATAKEY, oldValue, value);
				}
			}

		}

		[Property("DATAVAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string DATAVAL
		{
			get { return _dATAVAL; }
			set
			{
				if ((_dATAVAL == null) || (value == null) || (!value.Equals(_dATAVAL)))
				{
                    object oldValue = _dATAVAL;
					_dATAVAL = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_DATAVAL, oldValue, value);
				}
			}

		}

		[Property("COMMENTS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string COMMENTS
		{
			get { return _cOMMENTS; }
			set
			{
				if ((_cOMMENTS == null) || (value == null) || (!value.Equals(_cOMMENTS)))
				{
                    object oldValue = _cOMMENTS;
					_cOMMENTS = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_COMMENTS, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(LANGUAGESSET.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(LANGUAGESSET.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(LANGUAGESSET.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(LANGUAGESSET.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // LANGUAGESSET
}

