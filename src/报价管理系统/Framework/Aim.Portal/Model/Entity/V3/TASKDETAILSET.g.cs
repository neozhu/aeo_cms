// Business class TASKDETAILSET generated from TASKDETAILSET
// Creator: Ray
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
	[ActiveRecord("TASKDETAILSET")]
	public partial class TASKDETAILSET : ModelBase<TASKDETAILSET>
	{
		#region Property_Names

		public static string Prop_MODE1 = "MODE1";
		public static string Prop_MATCHCONDITIONTYPE = "MATCHCONDITIONTYPE";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_EXT5 = "EXT5";
		public static string Prop_ID = "ID";
		public static string Prop_WORKFLOWTEMPLETEID = "WORKFLOWTEMPLETEID";
		public static string Prop_NODEID = "NODEID";
		public static string Prop_NODENAME = "NODENAME";
		public static string Prop_USERTYPE = "USERTYPE";
		public static string Prop_USERIDS = "USERIDS";
		public static string Prop_USERNAMES = "USERNAMES";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _mODE1;
		private string _mATCHCONDITIONTYPE;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private string _eXT5;
		private string _id;
		private string _wORKFLOWTEMPLETEID;
		private string _nODEID;
		private string _nODENAME;
		private string _uSERTYPE;
		private string _uSERIDS;
		private string _uSERNAMES;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public TASKDETAILSET()
		{
		}

		public TASKDETAILSET(
			string p_mODE1,
			string p_mATCHCONDITIONTYPE,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			string p_eXT5,
			string p_id,
			string p_wORKFLOWTEMPLETEID,
			string p_nODEID,
			string p_nODENAME,
			string p_uSERTYPE,
			string p_uSERIDS,
			string p_uSERNAMES,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_mODE1 = p_mODE1;
			_mATCHCONDITIONTYPE = p_mATCHCONDITIONTYPE;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
			_eXT5 = p_eXT5;
			_id = p_id;
			_wORKFLOWTEMPLETEID = p_wORKFLOWTEMPLETEID;
			_nODEID = p_nODEID;
			_nODENAME = p_nODENAME;
			_uSERTYPE = p_uSERTYPE;
			_uSERIDS = p_uSERIDS;
			_uSERNAMES = p_uSERNAMES;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[Property("MODE1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string MODE1
		{
			get { return _mODE1; }
			set
			{
				if ((_mODE1 == null) || (value == null) || (!value.Equals(_mODE1)))
				{
                    object oldValue = _mODE1;
					_mODE1 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_MODE1, oldValue, value);
				}
			}

		}

		[Property("MATCHCONDITIONTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MATCHCONDITIONTYPE
		{
			get { return _mATCHCONDITIONTYPE; }
			set
			{
				if ((_mATCHCONDITIONTYPE == null) || (value == null) || (!value.Equals(_mATCHCONDITIONTYPE)))
				{
                    object oldValue = _mATCHCONDITIONTYPE;
					_mATCHCONDITIONTYPE = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_MATCHCONDITIONTYPE, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_EXT2, oldValue, value);
				}
			}

		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_EXT3, oldValue, value);
				}
			}

		}

		[Property("EXT4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string EXT4
		{
			get { return _eXT4; }
			set
			{
				if ((_eXT4 == null) || (value == null) || (!value.Equals(_eXT4)))
				{
                    object oldValue = _eXT4;
					_eXT4 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_EXT4, oldValue, value);
				}
			}

		}

		[Property("EXT5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string EXT5
		{
			get { return _eXT5; }
			set
			{
				if ((_eXT5 == null) || (value == null) || (!value.Equals(_eXT5)))
				{
                    object oldValue = _eXT5;
					_eXT5 = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_EXT5, oldValue, value);
				}
			}

		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("WORKFLOWTEMPLETEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string WORKFLOWTEMPLETEID
		{
			get { return _wORKFLOWTEMPLETEID; }
			set
			{
				if ((_wORKFLOWTEMPLETEID == null) || (value == null) || (!value.Equals(_wORKFLOWTEMPLETEID)))
				{
                    object oldValue = _wORKFLOWTEMPLETEID;
					_wORKFLOWTEMPLETEID = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_WORKFLOWTEMPLETEID, oldValue, value);
				}
			}

		}

		[Property("NODEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string NODEID
		{
			get { return _nODEID; }
			set
			{
				if ((_nODEID == null) || (value == null) || (!value.Equals(_nODEID)))
				{
                    object oldValue = _nODEID;
					_nODEID = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_NODEID, oldValue, value);
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
					RaisePropertyChanged(TASKDETAILSET.Prop_NODENAME, oldValue, value);
				}
			}

		}

		[Property("USERTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string USERTYPE
		{
			get { return _uSERTYPE; }
			set
			{
				if ((_uSERTYPE == null) || (value == null) || (!value.Equals(_uSERTYPE)))
				{
                    object oldValue = _uSERTYPE;
					_uSERTYPE = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_USERTYPE, oldValue, value);
				}
			}

		}

		[Property("USERIDS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string USERIDS
		{
			get { return _uSERIDS; }
			set
			{
				if ((_uSERIDS == null) || (value == null) || (!value.Equals(_uSERIDS)))
				{
                    object oldValue = _uSERIDS;
					_uSERIDS = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_USERIDS, oldValue, value);
				}
			}

		}

		[Property("USERNAMES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string USERNAMES
		{
			get { return _uSERNAMES; }
			set
			{
				if ((_uSERNAMES == null) || (value == null) || (!value.Equals(_uSERNAMES)))
				{
                    object oldValue = _uSERNAMES;
					_uSERNAMES = value;
					RaisePropertyChanged(TASKDETAILSET.Prop_USERNAMES, oldValue, value);
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
					RaisePropertyChanged(TASKDETAILSET.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(TASKDETAILSET.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(TASKDETAILSET.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // TASKDETAILSET
}

