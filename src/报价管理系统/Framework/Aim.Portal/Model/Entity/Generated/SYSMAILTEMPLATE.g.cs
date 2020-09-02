// Business class SYSMAILTEMPLATE generated from SYSMAILTEMPLATE
// Creator: Ray
// Created Date: [2016-10-27]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SYSMAILTEMPLATE")]
    public partial class SYSMAILTEMPLATE : EntityBase<SYSMAILTEMPLATE>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_CODE = "CODE";
		public static string Prop_NAME = "NAME";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_TITLE = "TITLE";
		public static string Prop_BODY = "BODY";
		public static string Prop_TYPE = "TYPE";
		public static string Prop_COMID = "COMID";
		public static string Prop_COMNAME = "COMNAME";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _id;
		private string _cODE;
		private string _nAME;
		private string _sTATUS;
		private string _tITLE;
		private string _bODY;
		private string _tYPE;
		private string _cOMID;
		private string _cOMNAME;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public SYSMAILTEMPLATE()
		{
		}

		public SYSMAILTEMPLATE(
			string p_id,
			string p_cODE,
			string p_nAME,
			string p_sTATUS,
			string p_tITLE,
			string p_bODY,
			string p_tYPE,
			string p_cOMID,
			string p_cOMNAME,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_id = p_id;
			_cODE = p_cODE;
			_nAME = p_nAME;
			_sTATUS = p_sTATUS;
			_tITLE = p_tITLE;
			_bODY = p_bODY;
			_tYPE = p_tYPE;
			_cOMID = p_cOMID;
			_cOMNAME = p_cOMNAME;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
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
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_CODE, oldValue, value);
				}
			}

		}

		[Property("NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string NAME
		{
			get { return _nAME; }
			set
			{
				if ((_nAME == null) || (value == null) || (!value.Equals(_nAME)))
				{
                    object oldValue = _nAME;
					_nAME = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_NAME, oldValue, value);
				}
			}

		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_STATUS, oldValue, value);
				}
			}

		}

		[Property("TITLE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TITLE
		{
			get { return _tITLE; }
			set
			{
				if ((_tITLE == null) || (value == null) || (!value.Equals(_tITLE)))
				{
                    object oldValue = _tITLE;
					_tITLE = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_TITLE, oldValue, value);
				}
			}

		}

        [Property("BODY", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "Aim.Portal.Model.OracleClobField, Aim.Portal")]
		public string BODY
		{
			get { return _bODY; }
			set
			{
				if ((_bODY == null) || (value == null) || (!value.Equals(_bODY)))
				{
                    object oldValue = _bODY;
					_bODY = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_BODY, oldValue, value);
				}
			}

		}

		[Property("TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TYPE
		{
			get { return _tYPE; }
			set
			{
				if ((_tYPE == null) || (value == null) || (!value.Equals(_tYPE)))
				{
                    object oldValue = _tYPE;
					_tYPE = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_TYPE, oldValue, value);
				}
			}

		}

		[Property("COMID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COMID
		{
			get { return _cOMID; }
			set
			{
				if ((_cOMID == null) || (value == null) || (!value.Equals(_cOMID)))
				{
                    object oldValue = _cOMID;
					_cOMID = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_COMID, oldValue, value);
				}
			}

		}

		[Property("COMNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string COMNAME
		{
			get { return _cOMNAME; }
			set
			{
				if ((_cOMNAME == null) || (value == null) || (!value.Equals(_cOMNAME)))
				{
                    object oldValue = _cOMNAME;
					_cOMNAME = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_COMNAME, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_EXT2, oldValue, value);
				}
			}

		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_EXT3, oldValue, value);
				}
			}

		}

		[Property("EXT4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT4
		{
			get { return _eXT4; }
			set
			{
				if ((_eXT4 == null) || (value == null) || (!value.Equals(_eXT4)))
				{
                    object oldValue = _eXT4;
					_eXT4 = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_EXT4, oldValue, value);
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
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_CREATEID, oldValue, value);
				}
			}

		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(SYSMAILTEMPLATE.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // SYSMAILTEMPLATE
}

