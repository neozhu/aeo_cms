// Business class P_EMNODE generated from P_EMNODE
// Creator: rw
// Created Date: [2016-11-03]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("P_EMNODE")]
	public partial class P_EMNODE : EntityBase<P_EMNODE>
	{
		#region Property_Names

		public static string Prop_OPCONTENT = "OPCONTENT";
		public static string Prop_RETURNCONTENT = "RETURNCONTENT";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_SORTINDEX = "SORTINDEX";
		public static string Prop_ID = "ID";
		public static string Prop_TYPE = "TYPE";
		public static string Prop_NAME = "NAME";
		public static string Prop_EMSTATE = "EMSTATE";
		public static string Prop_EMTIME = "EMTIME";
		public static string Prop_STATE = "STATE";
		public static string Prop_OPNAME = "OPNAME";

		#endregion

		#region Private_Variables

		private string _oPCONTENT;
		private string _rETURNCONTENT;
		private string _rEMARK;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private System.Decimal? _sORTINDEX;
		private string _id;
		private string _tYPE;
		private string _nAME;
		private string _eMSTATE;
		private DateTime? _eMTIME;
		private string _sTATE;
		private string _oPNAME;


		#endregion

		#region Constructors

		public P_EMNODE()
		{
		}

		public P_EMNODE(
			string p_oPCONTENT,
			string p_rETURNCONTENT,
			string p_rEMARK,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			System.Decimal? p_sORTINDEX,
			string p_id,
			string p_tYPE,
			string p_nAME,
			string p_eMSTATE,
			DateTime? p_eMTIME,
			string p_sTATE,
			string p_oPNAME)
		{
			_oPCONTENT = p_oPCONTENT;
			_rETURNCONTENT = p_rETURNCONTENT;
			_rEMARK = p_rEMARK;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
			_sORTINDEX = p_sORTINDEX;
			_id = p_id;
			_tYPE = p_tYPE;
			_nAME = p_nAME;
			_eMSTATE = p_eMSTATE;
			_eMTIME = p_eMTIME;
			_sTATE = p_sTATE;
			_oPNAME = p_oPNAME;
		}

		#endregion

		#region Properties

		[Property("OPCONTENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string OPCONTENT
		{
			get { return _oPCONTENT; }
			set
			{
				if ((_oPCONTENT == null) || (value == null) || (!value.Equals(_oPCONTENT)))
				{
                    object oldValue = _oPCONTENT;
					_oPCONTENT = value;
					RaisePropertyChanged(P_EMNODE.Prop_OPCONTENT, oldValue, value);
				}
			}
		}

		[Property("RETURNCONTENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string RETURNCONTENT
		{
			get { return _rETURNCONTENT; }
			set
			{
				if ((_rETURNCONTENT == null) || (value == null) || (!value.Equals(_rETURNCONTENT)))
				{
                    object oldValue = _rETURNCONTENT;
					_rETURNCONTENT = value;
					RaisePropertyChanged(P_EMNODE.Prop_RETURNCONTENT, oldValue, value);
				}
			}
		}

		[Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(P_EMNODE.Prop_REMARK, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_EXT1, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_EXT3, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_EXT4, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_SORTINDEX, oldValue, value);
				}
			}
		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
		}

		[Property("TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string TYPE
		{
			get { return _tYPE; }
			set
			{
				if ((_tYPE == null) || (value == null) || (!value.Equals(_tYPE)))
				{
                    object oldValue = _tYPE;
					_tYPE = value;
					RaisePropertyChanged(P_EMNODE.Prop_TYPE, oldValue, value);
				}
			}
		}

		[Property("NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string NAME
		{
			get { return _nAME; }
			set
			{
				if ((_nAME == null) || (value == null) || (!value.Equals(_nAME)))
				{
                    object oldValue = _nAME;
					_nAME = value;
					RaisePropertyChanged(P_EMNODE.Prop_NAME, oldValue, value);
				}
			}
		}

		[Property("EMSTATE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string EMSTATE
		{
			get { return _eMSTATE; }
			set
			{
				if ((_eMSTATE == null) || (value == null) || (!value.Equals(_eMSTATE)))
				{
                    object oldValue = _eMSTATE;
					_eMSTATE = value;
					RaisePropertyChanged(P_EMNODE.Prop_EMSTATE, oldValue, value);
				}
			}
		}

		[Property("EMTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? EMTIME
		{
			get { return _eMTIME; }
			set
			{
				if (value != _eMTIME)
				{
                    object oldValue = _eMTIME;
					_eMTIME = value;
					RaisePropertyChanged(P_EMNODE.Prop_EMTIME, oldValue, value);
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
					RaisePropertyChanged(P_EMNODE.Prop_STATE, oldValue, value);
				}
			}
		}

		[Property("OPNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string OPNAME
		{
			get { return _oPNAME; }
			set
			{
				if ((_oPNAME == null) || (value == null) || (!value.Equals(_oPNAME)))
				{
                    object oldValue = _oPNAME;
					_oPNAME = value;
					RaisePropertyChanged(P_EMNODE.Prop_OPNAME, oldValue, value);
				}
			}
		}

		#endregion
	} // P_EMNODE
}

