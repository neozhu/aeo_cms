// Business class SYSTBLMM generated from SYSTBLMM
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
	[ActiveRecord("SYSTBLMM")]
	public partial class SYSTBLMM : ModelBase<SYSTBLMM>
	{
		#region Property_Names

		public static string Prop_TBLSPACE = "TBLSPACE";
		public static string Prop_TBLOWNER = "TBLOWNER";
		public static string Prop_ID = "ID";
		public static string Prop_TBLCODE = "TBLCODE";
		public static string Prop_TBLNAME = "TBLNAME";
		public static string Prop_TBLTYPE = "TBLTYPE";
		public static string Prop_TBLCOMMENT = "TBLCOMMENT";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_TBLCREATETIME = "TBLCREATETIME";
		public static string Prop_TBLCREATEUSR = "TBLCREATEUSR";
		public static string Prop_TBLMODIFYTIME = "TBLMODIFYTIME";
		public static string Prop_TBLMODIFYUSR = "TBLMODIFYUSR";
		public static string Prop_CAPATICY = "CAPATICY";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";

		#endregion

		#region Private_Variables

		private string _tBLSPACE;
		private string _tBLOWNER;
		private string _id;
		private string _tBLCODE;
		private string _tBLNAME;
		private string _tBLTYPE;
		private string _tBLCOMMENT;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;
		private DateTime? _tBLCREATETIME;
		private string _tBLCREATEUSR;
		private DateTime? _tBLMODIFYTIME;
		private string _tBLMODIFYUSR;
		private System.Decimal? _cAPATICY;
		private string _eXT1;
		private string _eXT2;


		#endregion

		#region Constructors

		public SYSTBLMM()
		{
		}

		public SYSTBLMM(
			string p_tBLSPACE,
			string p_tBLOWNER,
			string p_id,
			string p_tBLCODE,
			string p_tBLNAME,
			string p_tBLTYPE,
			string p_tBLCOMMENT,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			DateTime? p_tBLCREATETIME,
			string p_tBLCREATEUSR,
			DateTime? p_tBLMODIFYTIME,
			string p_tBLMODIFYUSR,
			System.Decimal? p_cAPATICY,
			string p_eXT1,
			string p_eXT2)
		{
			_tBLSPACE = p_tBLSPACE;
			_tBLOWNER = p_tBLOWNER;
			_id = p_id;
			_tBLCODE = p_tBLCODE;
			_tBLNAME = p_tBLNAME;
			_tBLTYPE = p_tBLTYPE;
			_tBLCOMMENT = p_tBLCOMMENT;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
			_tBLCREATETIME = p_tBLCREATETIME;
			_tBLCREATEUSR = p_tBLCREATEUSR;
			_tBLMODIFYTIME = p_tBLMODIFYTIME;
			_tBLMODIFYUSR = p_tBLMODIFYUSR;
			_cAPATICY = p_cAPATICY;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
		}

		#endregion

		#region Properties

		[Property("TBLSPACE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string TBLSPACE
		{
			get { return _tBLSPACE; }
			set
			{
				if ((_tBLSPACE == null) || (value == null) || (!value.Equals(_tBLSPACE)))
				{
                    object oldValue = _tBLSPACE;
					_tBLSPACE = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLSPACE, oldValue, value);
				}
			}

		}

		[Property("TBLOWNER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TBLOWNER
		{
			get { return _tBLOWNER; }
			set
			{
				if ((_tBLOWNER == null) || (value == null) || (!value.Equals(_tBLOWNER)))
				{
                    object oldValue = _tBLOWNER;
					_tBLOWNER = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLOWNER, oldValue, value);
				}
			}

		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TBLCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 600)]
		public string TBLCODE
		{
			get { return _tBLCODE; }
			set
			{
				if ((_tBLCODE == null) || (value == null) || (!value.Equals(_tBLCODE)))
				{
                    object oldValue = _tBLCODE;
					_tBLCODE = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLCODE, oldValue, value);
				}
			}

		}

		[Property("TBLNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string TBLNAME
		{
			get { return _tBLNAME; }
			set
			{
				if ((_tBLNAME == null) || (value == null) || (!value.Equals(_tBLNAME)))
				{
                    object oldValue = _tBLNAME;
					_tBLNAME = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLNAME, oldValue, value);
				}
			}

		}

		[Property("TBLTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string TBLTYPE
		{
			get { return _tBLTYPE; }
			set
			{
				if ((_tBLTYPE == null) || (value == null) || (!value.Equals(_tBLTYPE)))
				{
                    object oldValue = _tBLTYPE;
					_tBLTYPE = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLTYPE, oldValue, value);
				}
			}

		}

		[Property("TBLCOMMENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
		public string TBLCOMMENT
		{
			get { return _tBLCOMMENT; }
			set
			{
				if ((_tBLCOMMENT == null) || (value == null) || (!value.Equals(_tBLCOMMENT)))
				{
                    object oldValue = _tBLCOMMENT;
					_tBLCOMMENT = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLCOMMENT, oldValue, value);
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
					RaisePropertyChanged(SYSTBLMM.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SYSTBLMM.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(SYSTBLMM.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		[Property("TBLCREATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? TBLCREATETIME
		{
			get { return _tBLCREATETIME; }
			set
			{
				if (value != _tBLCREATETIME)
				{
                    object oldValue = _tBLCREATETIME;
					_tBLCREATETIME = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLCREATETIME, oldValue, value);
				}
			}

		}

		[Property("TBLCREATEUSR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string TBLCREATEUSR
		{
			get { return _tBLCREATEUSR; }
			set
			{
				if ((_tBLCREATEUSR == null) || (value == null) || (!value.Equals(_tBLCREATEUSR)))
				{
                    object oldValue = _tBLCREATEUSR;
					_tBLCREATEUSR = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLCREATEUSR, oldValue, value);
				}
			}

		}

		[Property("TBLMODIFYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? TBLMODIFYTIME
		{
			get { return _tBLMODIFYTIME; }
			set
			{
				if (value != _tBLMODIFYTIME)
				{
                    object oldValue = _tBLMODIFYTIME;
					_tBLMODIFYTIME = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLMODIFYTIME, oldValue, value);
				}
			}

		}

		[Property("TBLMODIFYUSR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string TBLMODIFYUSR
		{
			get { return _tBLMODIFYUSR; }
			set
			{
				if ((_tBLMODIFYUSR == null) || (value == null) || (!value.Equals(_tBLMODIFYUSR)))
				{
                    object oldValue = _tBLMODIFYUSR;
					_tBLMODIFYUSR = value;
					RaisePropertyChanged(SYSTBLMM.Prop_TBLMODIFYUSR, oldValue, value);
				}
			}

		}

		[Property("CAPATICY", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? CAPATICY
		{
			get { return _cAPATICY; }
			set
			{
				if (value != _cAPATICY)
				{
                    object oldValue = _cAPATICY;
					_cAPATICY = value;
					RaisePropertyChanged(SYSTBLMM.Prop_CAPATICY, oldValue, value);
				}
			}

		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(SYSTBLMM.Prop_EXT1, oldValue, value);
				}
			}

		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(SYSTBLMM.Prop_EXT2, oldValue, value);
				}
			}

		}

		#endregion
	} // SYSTBLMM
}

