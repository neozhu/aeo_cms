// Business class NAVTEMPLATE generated from NAVTEMPLATE
// Created Date: [2014-05-26]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("NAVTEMPLATE")]
    public partial class NAVTEMPLATE : ModelBase<NAVTEMPLATE>
	{
		#region Property_Names

		public static string Prop_VERSION = "VERSION";
		public static string Prop_UPDATETIME = "UPDATETIME";
		public static string Prop_ID = "ID";
		public static string Prop_TITLE = "TITLE";
		public static string Prop_CODE = "CODE";
		public static string Prop_MODULENAME = "MODULENAME";
		public static string Prop_XML = "XML";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_EXT5 = "EXT5";
		public static string Prop_STATE = "STATE";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _vERSION;
		private DateTime? _uPDATETIME;
		private string _id;
		private string _tITLE;
		private string _cODE;
		private string _mODULENAME;
		private string _xML;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private string _eXT5;
		private string _sTATE;
		private string _rEMARK;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public NAVTEMPLATE()
		{
		}

		public NAVTEMPLATE(
			string p_vERSION,
			DateTime? p_uPDATETIME,
			string p_id,
			string p_tITLE,
			string p_cODE,
			string p_mODULENAME,
			string p_xML,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			string p_eXT5,
			string p_sTATE,
			string p_rEMARK,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_vERSION = p_vERSION;
			_uPDATETIME = p_uPDATETIME;
			_id = p_id;
			_tITLE = p_tITLE;
			_cODE = p_cODE;
			_mODULENAME = p_mODULENAME;
			_xML = p_xML;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
			_eXT5 = p_eXT5;
			_sTATE = p_sTATE;
			_rEMARK = p_rEMARK;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[Property("VERSION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string VERSION
		{
			get { return _vERSION; }
			set
			{
				if ((_vERSION == null) || (value == null) || (!value.Equals(_vERSION)))
				{
                    object oldValue = _vERSION;
					_vERSION = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_VERSION, oldValue, value);
				}
			}

		}

		[Property("UPDATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? UPDATETIME
		{
			get { return _uPDATETIME; }
			set
			{
				if (value != _uPDATETIME)
				{
                    object oldValue = _uPDATETIME;
					_uPDATETIME = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_UPDATETIME, oldValue, value);
				}
			}

		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TITLE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TITLE
		{
			get { return _tITLE; }
			set
			{
				if ((_tITLE == null) || (value == null) || (!value.Equals(_tITLE)))
				{
                    object oldValue = _tITLE;
					_tITLE = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_TITLE, oldValue, value);
				}
			}

		}

		[Property("CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CODE
		{
			get { return _cODE; }
			set
			{
				if ((_cODE == null) || (value == null) || (!value.Equals(_cODE)))
				{
                    object oldValue = _cODE;
					_cODE = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_CODE, oldValue, value);
				}
			}

		}

		[Property("MODULENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODULENAME
		{
			get { return _mODULENAME; }
			set
			{
				if ((_mODULENAME == null) || (value == null) || (!value.Equals(_mODULENAME)))
				{
                    object oldValue = _mODULENAME;
					_mODULENAME = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_MODULENAME, oldValue, value);
				}
			}

		}

		[Property("XML", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string XML
		{
			get { return _xML; }
			set
			{
				if ((_xML == null) || (value == null) || (!value.Equals(_xML)))
				{
                    object oldValue = _xML;
					_xML = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_XML, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_EXT1, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_EXT3, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_EXT4, oldValue, value);
				}
			}

		}

		[Property("EXT5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT5
		{
			get { return _eXT5; }
			set
			{
				if ((_eXT5 == null) || (value == null) || (!value.Equals(_eXT5)))
				{
                    object oldValue = _eXT5;
					_eXT5 = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_EXT5, oldValue, value);
				}
			}

		}

		[Property("STATE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
		public string STATE
		{
			get { return _sTATE; }
			set
			{
				if ((_sTATE == null) || (value == null) || (!value.Equals(_sTATE)))
				{
                    object oldValue = _sTATE;
					_sTATE = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_STATE, oldValue, value);
				}
			}

		}

		[Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_REMARK, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_CREATEID, oldValue, value);
				}
			}

		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(NAVTEMPLATE.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(NAVTEMPLATE.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // NAVTEMPLATE
}

