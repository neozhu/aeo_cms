// Business class SYSTBLCLNSMM generated from SYSTBLCLNSMM
// Creator: Ray
// Created Date: [2014-04-24]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("SYSTBLCLNSMM")]
	public partial class SYSTBLCLNSMM : ModelBase<SYSTBLCLNSMM>
	{
		#region Property_Names

		public static string Prop_CLNLEN = "CLNLEN";
		public static string Prop_ID = "ID";
		public static string Prop_REFTBLKEY = "REFTBLKEY";
		public static string Prop_REFTBLCODE = "REFTBLCODE";
		public static string Prop_CLNCODE = "CLNCODE";
		public static string Prop_CLNNAME = "CLNNAME";
		public static string Prop_CLNCOMMENT = "CLNCOMMENT";
		public static string Prop_CLNDATATYPE = "CLNDATATYPE";
		public static string Prop_CLNCREATETIME = "CLNCREATETIME";
		public static string Prop_CLNCREATEUSR = "CLNCREATEUSR";
		public static string Prop_CLNMODIFYTIME = "CLNMODIFYTIME";
		public static string Prop_CLNMODIFYUSR = "CLNMODIFYUSR";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _cLNLEN;
		private string _id;
		private string _rEFTBLKEY;
		private string _rEFTBLCODE;
		private string _cLNCODE;
		private string _cLNNAME;
		private string _cLNCOMMENT;
		private string _cLNDATATYPE;
		private DateTime? _cLNCREATETIME;
		private string _cLNCREATEUSR;
		private DateTime? _cLNMODIFYTIME;
		private string _cLNMODIFYUSR;
		private string _eXT1;
		private string _eXT2;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public SYSTBLCLNSMM()
		{
		}

		public SYSTBLCLNSMM(
			string p_cLNLEN,
			string p_id,
			string p_rEFTBLKEY,
			string p_rEFTBLCODE,
			string p_cLNCODE,
			string p_cLNNAME,
			string p_cLNCOMMENT,
			string p_cLNDATATYPE,
			DateTime? p_cLNCREATETIME,
			string p_cLNCREATEUSR,
			DateTime? p_cLNMODIFYTIME,
			string p_cLNMODIFYUSR,
			string p_eXT1,
			string p_eXT2,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME)
		{
			_cLNLEN = p_cLNLEN;
			_id = p_id;
			_rEFTBLKEY = p_rEFTBLKEY;
			_rEFTBLCODE = p_rEFTBLCODE;
			_cLNCODE = p_cLNCODE;
			_cLNNAME = p_cLNNAME;
			_cLNCOMMENT = p_cLNCOMMENT;
			_cLNDATATYPE = p_cLNDATATYPE;
			_cLNCREATETIME = p_cLNCREATETIME;
			_cLNCREATEUSR = p_cLNCREATEUSR;
			_cLNMODIFYTIME = p_cLNMODIFYTIME;
			_cLNMODIFYUSR = p_cLNMODIFYUSR;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[Property("CLNLEN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CLNLEN
		{
			get { return _cLNLEN; }
			set
			{
				if ((_cLNLEN == null) || (value == null) || (!value.Equals(_cLNLEN)))
				{
                    object oldValue = _cLNLEN;
					_cLNLEN = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNLEN, oldValue, value);
				}
			}

		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("REFTBLKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string REFTBLKEY
		{
			get { return _rEFTBLKEY; }
			set
			{
				if ((_rEFTBLKEY == null) || (value == null) || (!value.Equals(_rEFTBLKEY)))
				{
                    object oldValue = _rEFTBLKEY;
					_rEFTBLKEY = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_REFTBLKEY, oldValue, value);
				}
			}

		}

		[Property("REFTBLCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string REFTBLCODE
		{
			get { return _rEFTBLCODE; }
			set
			{
				if ((_rEFTBLCODE == null) || (value == null) || (!value.Equals(_rEFTBLCODE)))
				{
                    object oldValue = _rEFTBLCODE;
					_rEFTBLCODE = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_REFTBLCODE, oldValue, value);
				}
			}

		}

		[Property("CLNCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CLNCODE
		{
			get { return _cLNCODE; }
			set
			{
				if ((_cLNCODE == null) || (value == null) || (!value.Equals(_cLNCODE)))
				{
                    object oldValue = _cLNCODE;
					_cLNCODE = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNCODE, oldValue, value);
				}
			}

		}

		[Property("CLNNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string CLNNAME
		{
			get { return _cLNNAME; }
			set
			{
				if ((_cLNNAME == null) || (value == null) || (!value.Equals(_cLNNAME)))
				{
                    object oldValue = _cLNNAME;
					_cLNNAME = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNNAME, oldValue, value);
				}
			}

		}

		[Property("CLNCOMMENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 600)]
		public string CLNCOMMENT
		{
			get { return _cLNCOMMENT; }
			set
			{
				if ((_cLNCOMMENT == null) || (value == null) || (!value.Equals(_cLNCOMMENT)))
				{
                    object oldValue = _cLNCOMMENT;
					_cLNCOMMENT = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNCOMMENT, oldValue, value);
				}
			}

		}

		[Property("CLNDATATYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CLNDATATYPE
		{
			get { return _cLNDATATYPE; }
			set
			{
				if ((_cLNDATATYPE == null) || (value == null) || (!value.Equals(_cLNDATATYPE)))
				{
                    object oldValue = _cLNDATATYPE;
					_cLNDATATYPE = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNDATATYPE, oldValue, value);
				}
			}

		}

		[Property("CLNCREATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CLNCREATETIME
		{
			get { return _cLNCREATETIME; }
			set
			{
				if (value != _cLNCREATETIME)
				{
                    object oldValue = _cLNCREATETIME;
					_cLNCREATETIME = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNCREATETIME, oldValue, value);
				}
			}

		}

		[Property("CLNCREATEUSR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CLNCREATEUSR
		{
			get { return _cLNCREATEUSR; }
			set
			{
				if ((_cLNCREATEUSR == null) || (value == null) || (!value.Equals(_cLNCREATEUSR)))
				{
                    object oldValue = _cLNCREATEUSR;
					_cLNCREATEUSR = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNCREATEUSR, oldValue, value);
				}
			}

		}

		[Property("CLNMODIFYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CLNMODIFYTIME
		{
			get { return _cLNMODIFYTIME; }
			set
			{
				if (value != _cLNMODIFYTIME)
				{
                    object oldValue = _cLNMODIFYTIME;
					_cLNMODIFYTIME = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNMODIFYTIME, oldValue, value);
				}
			}

		}

		[Property("CLNMODIFYUSR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CLNMODIFYUSR
		{
			get { return _cLNMODIFYUSR; }
			set
			{
				if ((_cLNMODIFYUSR == null) || (value == null) || (!value.Equals(_cLNMODIFYUSR)))
				{
                    object oldValue = _cLNMODIFYUSR;
					_cLNMODIFYUSR = value;
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CLNMODIFYUSR, oldValue, value);
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
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_EXT1, oldValue, value);
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
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(SYSTBLCLNSMM.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		#endregion
	} // SYSTBLCLNSMM
}

