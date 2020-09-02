// Business class NAVNODESET generated from NAVNODESET
// Created Date: [2014-04-09]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("NAVNODESET")]
    public partial class NAVNODESET : ModelBase<NAVNODESET>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_TEMPLATEID = "TEMPLATEID";
		public static string Prop_NODEID = "NODEID";
		public static string Prop_NODENAME = "NODENAME";
		public static string Prop_MODULENAME = "MODULENAME";
		public static string Prop_URL = "URL";
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
		public static string Prop_COMPETENCEID = "COMPETENCEID";
		public static string Prop_COMPETENCENAME = "COMPETENCENAME";

		#endregion

		#region Private_Variables

		private string _id;
		private string _tEMPLATEID;
		private string _nODEID;
		private string _nODENAME;
		private string _mODULENAME;
		private string _uRL;
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
		private string _cOMPETENCEID;
		private string _cOMPETENCENAME;


		#endregion

		#region Constructors

		public NAVNODESET()
		{
		}

		public NAVNODESET(
			string p_id,
			string p_tEMPLATEID,
			string p_nODEID,
			string p_nODENAME,
			string p_mODULENAME,
			string p_uRL,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			string p_eXT5,
			string p_sTATE,
			string p_rEMARK,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			string p_cOMPETENCEID,
			string p_cOMPETENCENAME)
		{
			_id = p_id;
			_tEMPLATEID = p_tEMPLATEID;
			_nODEID = p_nODEID;
			_nODENAME = p_nODENAME;
			_mODULENAME = p_mODULENAME;
			_uRL = p_uRL;
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
			_cOMPETENCEID = p_cOMPETENCEID;
			_cOMPETENCENAME = p_cOMPETENCENAME;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TEMPLATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string TEMPLATEID
		{
			get { return _tEMPLATEID; }
			set
			{
				if ((_tEMPLATEID == null) || (value == null) || (!value.Equals(_tEMPLATEID)))
				{
                    object oldValue = _tEMPLATEID;
					_tEMPLATEID = value;
					RaisePropertyChanged(NAVNODESET.Prop_TEMPLATEID, oldValue, value);
				}
			}

		}

		[Property("NODEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 30)]
		public string NODEID
		{
			get { return _nODEID; }
			set
			{
				if ((_nODEID == null) || (value == null) || (!value.Equals(_nODEID)))
				{
                    object oldValue = _nODEID;
					_nODEID = value;
					RaisePropertyChanged(NAVNODESET.Prop_NODEID, oldValue, value);
				}
			}

		}

		[Property("NODENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string NODENAME
		{
			get { return _nODENAME; }
			set
			{
				if ((_nODENAME == null) || (value == null) || (!value.Equals(_nODENAME)))
				{
                    object oldValue = _nODENAME;
					_nODENAME = value;
					RaisePropertyChanged(NAVNODESET.Prop_NODENAME, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_MODULENAME, oldValue, value);
				}
			}

		}

		[Property("URL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string URL
		{
			get { return _uRL; }
			set
			{
				if ((_uRL == null) || (value == null) || (!value.Equals(_uRL)))
				{
                    object oldValue = _uRL;
					_uRL = value;
					RaisePropertyChanged(NAVNODESET.Prop_URL, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_EXT1, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_EXT3, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_EXT4, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_EXT5, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_STATE, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_REMARK, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(NAVNODESET.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		[Property("COMPETENCEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string COMPETENCEID
		{
			get { return _cOMPETENCEID; }
			set
			{
				if ((_cOMPETENCEID == null) || (value == null) || (!value.Equals(_cOMPETENCEID)))
				{
                    object oldValue = _cOMPETENCEID;
					_cOMPETENCEID = value;
					RaisePropertyChanged(NAVNODESET.Prop_COMPETENCEID, oldValue, value);
				}
			}

		}

		[Property("COMPETENCENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COMPETENCENAME
		{
			get { return _cOMPETENCENAME; }
			set
			{
				if ((_cOMPETENCENAME == null) || (value == null) || (!value.Equals(_cOMPETENCENAME)))
				{
                    object oldValue = _cOMPETENCENAME;
					_cOMPETENCENAME = value;
					RaisePropertyChanged(NAVNODESET.Prop_COMPETENCENAME, oldValue, value);
				}
			}

		}

		#endregion
	} // NAVNODESET
}

