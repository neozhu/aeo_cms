// Business class P_KITTING generated from P_KITTING
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
	[ActiveRecord("P_KITTING")]
	public partial class P_KITTING : EntityBase<P_KITTING>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_SEQ = "SEQ";
		public static string Prop_FACTORYTYPE = "FACTORYTYPE";
		public static string Prop_PDLINE = "PDLINE";
		public static string Prop_TRANSTYPE = "TRANSTYPE";
		public static string Prop_WORKNO = "WORKNO";
		public static string Prop_STATIONTYPE = "STATIONTYPE";
		public static string Prop_ITEMLINE = "ITEMLINE";
		public static string Prop_ITEMCODE = "ITEMCODE";
		public static string Prop_ITEMNAME = "ITEMNAME";
		public static string Prop_KEEPERCODE = "KEEPERCODE";
		public static string Prop_FROMTYPE = "FROMTYPE";
		public static string Prop_TOTYPE = "TOTYPE";
		public static string Prop_NEEDQTY = "NEEDQTY";
		public static string Prop_GIVEBUILDING = "GIVEBUILDING";
		public static string Prop_SENDPORT = "SENDPORT";
		public static string Prop_REQSENDTIME = "REQSENDTIME";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_LOTNO = "LOTNO";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_COMPANYID = "COMPANYID";
		public static string Prop_COMPANYNAME = "COMPANYNAME";
		public static string Prop_SYNCSTATE = "SYNCSTATE";
		public static string Prop_SYNCLOG = "SYNCLOG";
        public static string Prop_SYNCTIME = "SYNCTIME";
        public static string Prop_ITEMSEQ = "ITEMSEQ";

		#endregion

		#region Private_Variables

		private string _id;
		private System.Decimal? _sEQ;
		private string _fACTORYTYPE;
		private string _pDLINE;
		private string _tRANSTYPE;
		private string _wORKNO;
		private string _sTATIONTYPE;
		private string _iTEMLINE;
		private string _iTEMCODE;
		private string _iTEMNAME;
		private string _kEEPERCODE;
		private string _fROMTYPE;
		private string _tOTYPE;
		private System.Decimal? _nEEDQTY;
		private string _gIVEBUILDING;
		private string _sENDPORT;
		private DateTime? _rEQSENDTIME;
		private string _rEMARK;
		private string _lOTNO;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;
		private string _cOMPANYID;
		private string _cOMPANYNAME;
		private string _sYNCSTATE;
		private string _sYNCLOG;
        private DateTime? _sYNCTIME;
        private string _iTEMSEQ;


		#endregion

		#region Constructors

		public P_KITTING()
		{
		}

		public P_KITTING(
			string p_id,
			System.Decimal? p_sEQ,
			string p_fACTORYTYPE,
			string p_pDLINE,
			string p_tRANSTYPE,
			string p_wORKNO,
			string p_sTATIONTYPE,
			string p_iTEMLINE,
			string p_iTEMCODE,
			string p_iTEMNAME,
			string p_kEEPERCODE,
			string p_fROMTYPE,
			string p_tOTYPE,
			System.Decimal? p_nEEDQTY,
			string p_gIVEBUILDING,
			string p_sENDPORT,
			DateTime? p_rEQSENDTIME,
			string p_rEMARK,
			string p_lOTNO,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			string p_cOMPANYID,
			string p_cOMPANYNAME,
			string p_sYNCSTATE,
			string p_sYNCLOG,
			DateTime? p_sYNCTIME)
		{
			_id = p_id;
			_sEQ = p_sEQ;
			_fACTORYTYPE = p_fACTORYTYPE;
			_pDLINE = p_pDLINE;
			_tRANSTYPE = p_tRANSTYPE;
			_wORKNO = p_wORKNO;
			_sTATIONTYPE = p_sTATIONTYPE;
			_iTEMLINE = p_iTEMLINE;
			_iTEMCODE = p_iTEMCODE;
			_iTEMNAME = p_iTEMNAME;
			_kEEPERCODE = p_kEEPERCODE;
			_fROMTYPE = p_fROMTYPE;
			_tOTYPE = p_tOTYPE;
			_nEEDQTY = p_nEEDQTY;
			_gIVEBUILDING = p_gIVEBUILDING;
			_sENDPORT = p_sENDPORT;
			_rEQSENDTIME = p_rEQSENDTIME;
			_rEMARK = p_rEMARK;
			_lOTNO = p_lOTNO;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
			_cOMPANYID = p_cOMPANYID;
			_cOMPANYNAME = p_cOMPANYNAME;
			_sYNCSTATE = p_sYNCSTATE;
			_sYNCLOG = p_sYNCLOG;
			_sYNCTIME = p_sYNCTIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
		}

		[Property("SEQ", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SEQ
		{
			get { return _sEQ; }
			set
			{
				if (value != _sEQ)
				{
                    object oldValue = _sEQ;
					_sEQ = value;
					RaisePropertyChanged(P_KITTING.Prop_SEQ, oldValue, value);
				}
			}
		}
        [Property("ITEMSEQ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string ITEMSEQ
        {
            get { return _iTEMSEQ; }
            set
            {
                if (value != _iTEMSEQ)
                {
                    object oldValue = _iTEMSEQ;
                    _iTEMSEQ = value;
                    RaisePropertyChanged(P_KITTING.Prop_ITEMSEQ, oldValue, value);
                }
            }
        }

		[Property("FACTORYTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FACTORYTYPE
		{
			get { return _fACTORYTYPE; }
			set
			{
				if ((_fACTORYTYPE == null) || (value == null) || (!value.Equals(_fACTORYTYPE)))
				{
                    object oldValue = _fACTORYTYPE;
					_fACTORYTYPE = value;
					RaisePropertyChanged(P_KITTING.Prop_FACTORYTYPE, oldValue, value);
				}
			}
		}

		[Property("PDLINE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string PDLINE
		{
			get { return _pDLINE; }
			set
			{
				if ((_pDLINE == null) || (value == null) || (!value.Equals(_pDLINE)))
				{
                    object oldValue = _pDLINE;
					_pDLINE = value;
					RaisePropertyChanged(P_KITTING.Prop_PDLINE, oldValue, value);
				}
			}
		}

		[Property("TRANSTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TRANSTYPE
		{
			get { return _tRANSTYPE; }
			set
			{
				if ((_tRANSTYPE == null) || (value == null) || (!value.Equals(_tRANSTYPE)))
				{
                    object oldValue = _tRANSTYPE;
					_tRANSTYPE = value;
					RaisePropertyChanged(P_KITTING.Prop_TRANSTYPE, oldValue, value);
				}
			}
		}

		[Property("WORKNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WORKNO
		{
			get { return _wORKNO; }
			set
			{
				if ((_wORKNO == null) || (value == null) || (!value.Equals(_wORKNO)))
				{
                    object oldValue = _wORKNO;
					_wORKNO = value;
					RaisePropertyChanged(P_KITTING.Prop_WORKNO, oldValue, value);
				}
			}
		}

		[Property("STATIONTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string STATIONTYPE
		{
			get { return _sTATIONTYPE; }
			set
			{
				if ((_sTATIONTYPE == null) || (value == null) || (!value.Equals(_sTATIONTYPE)))
				{
                    object oldValue = _sTATIONTYPE;
					_sTATIONTYPE = value;
					RaisePropertyChanged(P_KITTING.Prop_STATIONTYPE, oldValue, value);
				}
			}
		}

		[Property("ITEMLINE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string ITEMLINE
		{
			get { return _iTEMLINE; }
			set
			{
				if ((_iTEMLINE == null) || (value == null) || (!value.Equals(_iTEMLINE)))
				{
                    object oldValue = _iTEMLINE;
					_iTEMLINE = value;
					RaisePropertyChanged(P_KITTING.Prop_ITEMLINE, oldValue, value);
				}
			}
		}

		[Property("ITEMCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string ITEMCODE
		{
			get { return _iTEMCODE; }
			set
			{
				if ((_iTEMCODE == null) || (value == null) || (!value.Equals(_iTEMCODE)))
				{
                    object oldValue = _iTEMCODE;
					_iTEMCODE = value;
					RaisePropertyChanged(P_KITTING.Prop_ITEMCODE, oldValue, value);
				}
			}
		}

		[Property("ITEMNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string ITEMNAME
		{
			get { return _iTEMNAME; }
			set
			{
				if ((_iTEMNAME == null) || (value == null) || (!value.Equals(_iTEMNAME)))
				{
                    object oldValue = _iTEMNAME;
					_iTEMNAME = value;
					RaisePropertyChanged(P_KITTING.Prop_ITEMNAME, oldValue, value);
				}
			}
		}

		[Property("KEEPERCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string KEEPERCODE
		{
			get { return _kEEPERCODE; }
			set
			{
				if ((_kEEPERCODE == null) || (value == null) || (!value.Equals(_kEEPERCODE)))
				{
                    object oldValue = _kEEPERCODE;
					_kEEPERCODE = value;
					RaisePropertyChanged(P_KITTING.Prop_KEEPERCODE, oldValue, value);
				}
			}
		}

		[Property("FROMTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string FROMTYPE
		{
			get { return _fROMTYPE; }
			set
			{
				if ((_fROMTYPE == null) || (value == null) || (!value.Equals(_fROMTYPE)))
				{
                    object oldValue = _fROMTYPE;
					_fROMTYPE = value;
					RaisePropertyChanged(P_KITTING.Prop_FROMTYPE, oldValue, value);
				}
			}
		}

		[Property("TOTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TOTYPE
		{
			get { return _tOTYPE; }
			set
			{
				if ((_tOTYPE == null) || (value == null) || (!value.Equals(_tOTYPE)))
				{
                    object oldValue = _tOTYPE;
					_tOTYPE = value;
					RaisePropertyChanged(P_KITTING.Prop_TOTYPE, oldValue, value);
				}
			}
		}

		[Property("NEEDQTY", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? NEEDQTY
		{
			get { return _nEEDQTY; }
			set
			{
				if (value != _nEEDQTY)
				{
                    object oldValue = _nEEDQTY;
					_nEEDQTY = value;
					RaisePropertyChanged(P_KITTING.Prop_NEEDQTY, oldValue, value);
				}
			}
		}

		[Property("GIVEBUILDING", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string GIVEBUILDING
		{
			get { return _gIVEBUILDING; }
			set
			{
				if ((_gIVEBUILDING == null) || (value == null) || (!value.Equals(_gIVEBUILDING)))
				{
                    object oldValue = _gIVEBUILDING;
					_gIVEBUILDING = value;
					RaisePropertyChanged(P_KITTING.Prop_GIVEBUILDING, oldValue, value);
				}
			}
		}

		[Property("SENDPORT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SENDPORT
		{
			get { return _sENDPORT; }
			set
			{
				if ((_sENDPORT == null) || (value == null) || (!value.Equals(_sENDPORT)))
				{
                    object oldValue = _sENDPORT;
					_sENDPORT = value;
					RaisePropertyChanged(P_KITTING.Prop_SENDPORT, oldValue, value);
				}
			}
		}

		[Property("REQSENDTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? REQSENDTIME
		{
			get { return _rEQSENDTIME; }
			set
			{
				if (value != _rEQSENDTIME)
				{
                    object oldValue = _rEQSENDTIME;
					_rEQSENDTIME = value;
					RaisePropertyChanged(P_KITTING.Prop_REQSENDTIME, oldValue, value);
				}
			}
		}

		[Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(P_KITTING.Prop_REMARK, oldValue, value);
				}
			}
		}

		[Property("LOTNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string LOTNO
		{
			get { return _lOTNO; }
			set
			{
				if ((_lOTNO == null) || (value == null) || (!value.Equals(_lOTNO)))
				{
                    object oldValue = _lOTNO;
					_lOTNO = value;
					RaisePropertyChanged(P_KITTING.Prop_LOTNO, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(P_KITTING.Prop_CREATEID, oldValue, value);
				}
			}
		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(P_KITTING.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(P_KITTING.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("COMPANYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COMPANYID
		{
			get { return _cOMPANYID; }
			set
			{
				if ((_cOMPANYID == null) || (value == null) || (!value.Equals(_cOMPANYID)))
				{
                    object oldValue = _cOMPANYID;
					_cOMPANYID = value;
					RaisePropertyChanged(P_KITTING.Prop_COMPANYID, oldValue, value);
				}
			}
		}

		[Property("COMPANYNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string COMPANYNAME
		{
			get { return _cOMPANYNAME; }
			set
			{
				if ((_cOMPANYNAME == null) || (value == null) || (!value.Equals(_cOMPANYNAME)))
				{
                    object oldValue = _cOMPANYNAME;
					_cOMPANYNAME = value;
					RaisePropertyChanged(P_KITTING.Prop_COMPANYNAME, oldValue, value);
				}
			}
		}

		[Property("SYNCSTATE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
		public string SYNCSTATE
		{
			get { return _sYNCSTATE; }
			set
			{
				if ((_sYNCSTATE == null) || (value == null) || (!value.Equals(_sYNCSTATE)))
				{
                    object oldValue = _sYNCSTATE;
					_sYNCSTATE = value;
					RaisePropertyChanged(P_KITTING.Prop_SYNCSTATE, oldValue, value);
				}
			}
		}

		[Property("SYNCLOG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string SYNCLOG
		{
			get { return _sYNCLOG; }
			set
			{
				if ((_sYNCLOG == null) || (value == null) || (!value.Equals(_sYNCLOG)))
				{
                    object oldValue = _sYNCLOG;
					_sYNCLOG = value;
					RaisePropertyChanged(P_KITTING.Prop_SYNCLOG, oldValue, value);
				}
			}
		}

		[Property("SYNCTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? SYNCTIME
		{
			get { return _sYNCTIME; }
			set
			{
				if (value != _sYNCTIME)
				{
                    object oldValue = _sYNCTIME;
					_sYNCTIME = value;
					RaisePropertyChanged(P_KITTING.Prop_SYNCTIME, oldValue, value);
				}
			}
		}

		#endregion
	} // P_KITTING
}

