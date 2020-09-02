// Business class SQM_BJ_PSF generated from SQM_BJ_PSF
// Creator: rw
// Created Date: [2020-03-04]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_BJ_PSF")]
	public partial class SQM_BJ_PSF : EntityBase<SQM_BJ_PSF>
	{
		#region Property_Names

		public static string Prop_BJSTARTDATE = "BJSTARTDATE";
		public static string Prop_BJENDDATE = "BJENDDATE";
		public static string Prop_ALOENFEE = "ALOENFEE";
		public static string Prop_ISCOPY = "ISCOPY";
		public static string Prop_BGFZRID = "BGFZRID";
		public static string Prop_JSFCODE = "JSFCODE";
		public static string Prop_JSF = "JSF";
		public static string Prop_JSFJSCODE = "JSFJSCODE";
		public static string Prop_JSFJS = "JSFJS";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_PRODUCT_NAME = "PRODUCT_NAME";
		public static string Prop_PRODUCT_CODE = "PRODUCT_CODE";
		public static string Prop_SERVICE_NAME = "SERVICE_NAME";
		public static string Prop_SERVICE_CODE = "SERVICE_CODE";
		public static string Prop_FEE_NAME = "FEE_NAME";
		public static string Prop_FEE_CODE = "FEE_CODE";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_ORGNAME = "ORGNAME";
		public static string Prop_ORGCODE = "ORGCODE";
		public static string Prop_BUSINESSORG = "BUSINESSORG";
		public static string Prop_BJFS = "BJFS";
		public static string Prop_ISLSC = "ISLSC";
		public static string Prop_DISCOUNT = "DISCOUNT";
		public static string Prop_RATERULE = "RATERULE";
		public static string Prop_STAGETYPE = "STAGETYPE";
		public static string Prop_MRID = "MRID";
		public static string Prop_OTHER_NAME = "OTHER_NAME";
		public static string Prop_BJSTATAUS = "BJSTATAUS";
		public static string Prop_CONDITION = "CONDITION";
		public static string Prop_JXJC = "JXJC";
		public static string Prop_CHOOSESTATUS = "CHOOSESTATUS";
		public static string Prop_VRID = "VRID";
		public static string Prop_IFBJCX = "IFBJCX";
		public static string Prop_MINSTATUS = "MINSTATUS";
		public static string Prop_FEECATG = "FEECATG";
		public static string Prop_OTHER_NAME_EN = "OTHER_NAME_EN";
		public static string Prop_BJTCURR = "BJTCURR";

		#endregion

		#region Private_Variables

		private DateTime? _bJSTARTDATE;
		private DateTime? _bJENDDATE;
		private string _aLOENFEE;
		private string _iSCOPY;
		private string _bGFZRID;
		private string _jSFCODE;
		private string _jSF;
		private string _jSFJSCODE;
		private string _jSFJS;
		private string _rid;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rEMARK;
		private string _dB_KEY;
		private string _pRODUCT_NAME;
		private string _pRODUCT_CODE;
		private string _sERVICE_NAME;
		private string _sERVICE_CODE;
		private string _fEE_NAME;
		private string _fEE_CODE;
		private string _cREATEID;
		private string _mODIFYID;
		private string _oRGNAME;
		private string _oRGCODE;
		private string _bUSINESSORG;
		private string _bJFS;
		private string _iSLSC;
		private System.Decimal? _dISCOUNT;
		private string _rATERULE;
		private string _sTAGETYPE;
		private string _mRID;
		private string _oTHER_NAME;
		private string _bJSTATAUS;
		private string _cONDITION;
		private string _jXJC;
		private string _cHOOSESTATUS;
		private string _vRID;
		private string _iFBJCX;
		private string _mINSTATUS;
		private string _fEECATG;
		private string _oTHER_NAME_EN;
		private string _bJTCURR;


		#endregion

		#region Constructors

		public SQM_BJ_PSF()
		{
		}

		public SQM_BJ_PSF(
			DateTime? p_bJSTARTDATE,
			DateTime? p_bJENDDATE,
			string p_aLOENFEE,
			string p_iSCOPY,
			string p_bGFZRID,
			string p_jSFCODE,
			string p_jSF,
			string p_jSFJSCODE,
			string p_jSFJS,
			string p_rid,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rEMARK,
			string p_dB_KEY,
			string p_pRODUCT_NAME,
			string p_pRODUCT_CODE,
			string p_sERVICE_NAME,
			string p_sERVICE_CODE,
			string p_fEE_NAME,
			string p_fEE_CODE,
			string p_cREATEID,
			string p_mODIFYID,
			string p_oRGNAME,
			string p_oRGCODE,
			string p_bUSINESSORG,
			string p_bJFS,
			string p_iSLSC,
			System.Decimal? p_dISCOUNT,
			string p_rATERULE,
			string p_sTAGETYPE,
			string p_mRID,
			string p_oTHER_NAME,
			string p_bJSTATAUS,
			string p_cONDITION,
			string p_jXJC,
			string p_cHOOSESTATUS,
			string p_vRID,
			string p_iFBJCX,
			string p_mINSTATUS,
			string p_fEECATG,
			string p_oTHER_NAME_EN,
			string p_bJTCURR)
		{
			_bJSTARTDATE = p_bJSTARTDATE;
			_bJENDDATE = p_bJENDDATE;
			_aLOENFEE = p_aLOENFEE;
			_iSCOPY = p_iSCOPY;
			_bGFZRID = p_bGFZRID;
			_jSFCODE = p_jSFCODE;
			_jSF = p_jSF;
			_jSFJSCODE = p_jSFJSCODE;
			_jSFJS = p_jSFJS;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rEMARK = p_rEMARK;
			_dB_KEY = p_dB_KEY;
			_pRODUCT_NAME = p_pRODUCT_NAME;
			_pRODUCT_CODE = p_pRODUCT_CODE;
			_sERVICE_NAME = p_sERVICE_NAME;
			_sERVICE_CODE = p_sERVICE_CODE;
			_fEE_NAME = p_fEE_NAME;
			_fEE_CODE = p_fEE_CODE;
			_cREATEID = p_cREATEID;
			_mODIFYID = p_mODIFYID;
			_oRGNAME = p_oRGNAME;
			_oRGCODE = p_oRGCODE;
			_bUSINESSORG = p_bUSINESSORG;
			_bJFS = p_bJFS;
			_iSLSC = p_iSLSC;
			_dISCOUNT = p_dISCOUNT;
			_rATERULE = p_rATERULE;
			_sTAGETYPE = p_sTAGETYPE;
			_mRID = p_mRID;
			_oTHER_NAME = p_oTHER_NAME;
			_bJSTATAUS = p_bJSTATAUS;
			_cONDITION = p_cONDITION;
			_jXJC = p_jXJC;
			_cHOOSESTATUS = p_cHOOSESTATUS;
			_vRID = p_vRID;
			_iFBJCX = p_iFBJCX;
			_mINSTATUS = p_mINSTATUS;
			_fEECATG = p_fEECATG;
			_oTHER_NAME_EN = p_oTHER_NAME_EN;
			_bJTCURR = p_bJTCURR;
		}

		#endregion

		#region Properties

		[Property("BJSTARTDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? BJSTARTDATE
		{
			get { return _bJSTARTDATE; }
			set
			{
				if (value != _bJSTARTDATE)
				{
                    object oldValue = _bJSTARTDATE;
					_bJSTARTDATE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BJSTARTDATE, oldValue, value);
				}
			}
		}

		[Property("BJENDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? BJENDDATE
		{
			get { return _bJENDDATE; }
			set
			{
				if (value != _bJENDDATE)
				{
                    object oldValue = _bJENDDATE;
					_bJENDDATE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BJENDDATE, oldValue, value);
				}
			}
		}

		[Property("ALOENFEE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ALOENFEE
		{
			get { return _aLOENFEE; }
			set
			{
				if ((_aLOENFEE == null) || (value == null) || (!value.Equals(_aLOENFEE)))
				{
                    object oldValue = _aLOENFEE;
					_aLOENFEE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_ALOENFEE, oldValue, value);
				}
			}
		}

		[Property("ISCOPY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string ISCOPY
		{
			get { return _iSCOPY; }
			set
			{
				if ((_iSCOPY == null) || (value == null) || (!value.Equals(_iSCOPY)))
				{
                    object oldValue = _iSCOPY;
					_iSCOPY = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_ISCOPY, oldValue, value);
				}
			}
		}

		[Property("BGFZRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BGFZRID
		{
			get { return _bGFZRID; }
			set
			{
				if ((_bGFZRID == null) || (value == null) || (!value.Equals(_bGFZRID)))
				{
                    object oldValue = _bGFZRID;
					_bGFZRID = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BGFZRID, oldValue, value);
				}
			}
		}

		[Property("JSFCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string JSFCODE
		{
			get { return _jSFCODE; }
			set
			{
				if ((_jSFCODE == null) || (value == null) || (!value.Equals(_jSFCODE)))
				{
                    object oldValue = _jSFCODE;
					_jSFCODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_JSFCODE, oldValue, value);
				}
			}
		}

		[Property("JSF", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string JSF
		{
			get { return _jSF; }
			set
			{
				if ((_jSF == null) || (value == null) || (!value.Equals(_jSF)))
				{
                    object oldValue = _jSF;
					_jSF = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_JSF, oldValue, value);
				}
			}
		}

		[Property("JSFJSCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string JSFJSCODE
		{
			get { return _jSFJSCODE; }
			set
			{
				if ((_jSFJSCODE == null) || (value == null) || (!value.Equals(_jSFJSCODE)))
				{
                    object oldValue = _jSFJSCODE;
					_jSFJSCODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_JSFJSCODE, oldValue, value);
				}
			}
		}

		[Property("JSFJS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string JSFJS
		{
			get { return _jSFJS; }
			set
			{
				if ((_jSFJS == null) || (value == null) || (!value.Equals(_jSFJS)))
				{
                    object oldValue = _jSFJS;
					_jSFJS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_JSFJS, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
            set
            {
                if ((_rid == null) || (value == null) || (!value.Equals(_rid)))
                {
                    object oldValue = _rid;
                    _rid = value;
                    RaisePropertyChanged(SQM_BJ_PSF.Prop_RID, oldValue, value);
                }
            }
        }

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_PSF.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_CREATEUSER, oldValue, value);
				}
			}
		}

		[Property("MODIFYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? MODIFYTIME
		{
			get { return _mODIFYTIME; }
			set
			{
				if (value != _mODIFYTIME)
				{
                    object oldValue = _mODIFYTIME;
					_mODIFYTIME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_REMARK, oldValue, value);
				}
			}
		}

		[Property("DB_KEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DB_KEY
		{
			get { return _dB_KEY; }
			set
			{
				if ((_dB_KEY == null) || (value == null) || (!value.Equals(_dB_KEY)))
				{
                    object oldValue = _dB_KEY;
					_dB_KEY = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("PRODUCT_NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string PRODUCT_NAME
		{
			get { return _pRODUCT_NAME; }
			set
			{
				if ((_pRODUCT_NAME == null) || (value == null) || (!value.Equals(_pRODUCT_NAME)))
				{
                    object oldValue = _pRODUCT_NAME;
					_pRODUCT_NAME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_PRODUCT_NAME, oldValue, value);
				}
			}
		}

		[Property("PRODUCT_CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODUCT_CODE
		{
			get { return _pRODUCT_CODE; }
			set
			{
				if ((_pRODUCT_CODE == null) || (value == null) || (!value.Equals(_pRODUCT_CODE)))
				{
                    object oldValue = _pRODUCT_CODE;
					_pRODUCT_CODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_PRODUCT_CODE, oldValue, value);
				}
			}
		}

		[Property("SERVICE_NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SERVICE_NAME
		{
			get { return _sERVICE_NAME; }
			set
			{
				if ((_sERVICE_NAME == null) || (value == null) || (!value.Equals(_sERVICE_NAME)))
				{
                    object oldValue = _sERVICE_NAME;
					_sERVICE_NAME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_SERVICE_NAME, oldValue, value);
				}
			}
		}

		[Property("SERVICE_CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string SERVICE_CODE
		{
			get { return _sERVICE_CODE; }
			set
			{
				if ((_sERVICE_CODE == null) || (value == null) || (!value.Equals(_sERVICE_CODE)))
				{
                    object oldValue = _sERVICE_CODE;
					_sERVICE_CODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_SERVICE_CODE, oldValue, value);
				}
			}
		}

		[Property("FEE_NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string FEE_NAME
		{
			get { return _fEE_NAME; }
			set
			{
				if ((_fEE_NAME == null) || (value == null) || (!value.Equals(_fEE_NAME)))
				{
                    object oldValue = _fEE_NAME;
					_fEE_NAME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_FEE_NAME, oldValue, value);
				}
			}
		}

		[Property("FEE_CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEE_CODE
		{
			get { return _fEE_CODE; }
			set
			{
				if ((_fEE_CODE == null) || (value == null) || (!value.Equals(_fEE_CODE)))
				{
                    object oldValue = _fEE_CODE;
					_fEE_CODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_FEE_CODE, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_CREATEID, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_MODIFYID, oldValue, value);
				}
			}
		}

		[Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string ORGNAME
		{
			get { return _oRGNAME; }
			set
			{
				if ((_oRGNAME == null) || (value == null) || (!value.Equals(_oRGNAME)))
				{
                    object oldValue = _oRGNAME;
					_oRGNAME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_ORGNAME, oldValue, value);
				}
			}
		}

		[Property("ORGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string ORGCODE
		{
			get { return _oRGCODE; }
			set
			{
				if ((_oRGCODE == null) || (value == null) || (!value.Equals(_oRGCODE)))
				{
                    object oldValue = _oRGCODE;
					_oRGCODE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_ORGCODE, oldValue, value);
				}
			}
		}

		[Property("BUSINESSORG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BUSINESSORG
		{
			get { return _bUSINESSORG; }
			set
			{
				if ((_bUSINESSORG == null) || (value == null) || (!value.Equals(_bUSINESSORG)))
				{
                    object oldValue = _bUSINESSORG;
					_bUSINESSORG = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BUSINESSORG, oldValue, value);
				}
			}
		}

		[Property("BJFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string BJFS
		{
			get { return _bJFS; }
			set
			{
				if ((_bJFS == null) || (value == null) || (!value.Equals(_bJFS)))
				{
                    object oldValue = _bJFS;
					_bJFS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BJFS, oldValue, value);
				}
			}
		}

		[Property("ISLSC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ISLSC
		{
			get { return _iSLSC; }
			set
			{
				if ((_iSLSC == null) || (value == null) || (!value.Equals(_iSLSC)))
				{
                    object oldValue = _iSLSC;
					_iSLSC = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_ISLSC, oldValue, value);
				}
			}
		}

		[Property("DISCOUNT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DISCOUNT
		{
			get { return _dISCOUNT; }
			set
			{
				if (value != _dISCOUNT)
				{
                    object oldValue = _dISCOUNT;
					_dISCOUNT = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_DISCOUNT, oldValue, value);
				}
			}
		}

		[Property("RATERULE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string RATERULE
		{
			get { return _rATERULE; }
			set
			{
				if ((_rATERULE == null) || (value == null) || (!value.Equals(_rATERULE)))
				{
                    object oldValue = _rATERULE;
					_rATERULE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_RATERULE, oldValue, value);
				}
			}
		}

		[Property("STAGETYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string STAGETYPE
		{
			get { return _sTAGETYPE; }
			set
			{
				if ((_sTAGETYPE == null) || (value == null) || (!value.Equals(_sTAGETYPE)))
				{
                    object oldValue = _sTAGETYPE;
					_sTAGETYPE = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_STAGETYPE, oldValue, value);
				}
			}
		}

		[Property("MRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MRID
		{
			get { return _mRID; }
			set
			{
				if ((_mRID == null) || (value == null) || (!value.Equals(_mRID)))
				{
                    object oldValue = _mRID;
					_mRID = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_MRID, oldValue, value);
				}
			}
		}

		[Property("OTHER_NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string OTHER_NAME
		{
			get { return _oTHER_NAME; }
			set
			{
				if ((_oTHER_NAME == null) || (value == null) || (!value.Equals(_oTHER_NAME)))
				{
                    object oldValue = _oTHER_NAME;
					_oTHER_NAME = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_OTHER_NAME, oldValue, value);
				}
			}
		}

		[Property("BJSTATAUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BJSTATAUS
		{
			get { return _bJSTATAUS; }
			set
			{
				if ((_bJSTATAUS == null) || (value == null) || (!value.Equals(_bJSTATAUS)))
				{
                    object oldValue = _bJSTATAUS;
					_bJSTATAUS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BJSTATAUS, oldValue, value);
				}
			}
		}

		[Property("CONDITION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CONDITION
		{
			get { return _cONDITION; }
			set
			{
				if ((_cONDITION == null) || (value == null) || (!value.Equals(_cONDITION)))
				{
                    object oldValue = _cONDITION;
					_cONDITION = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_CONDITION, oldValue, value);
				}
			}
		}

		[Property("JXJC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string JXJC
		{
			get { return _jXJC; }
			set
			{
				if ((_jXJC == null) || (value == null) || (!value.Equals(_jXJC)))
				{
                    object oldValue = _jXJC;
					_jXJC = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_JXJC, oldValue, value);
				}
			}
		}

		[Property("CHOOSESTATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CHOOSESTATUS
		{
			get { return _cHOOSESTATUS; }
			set
			{
				if ((_cHOOSESTATUS == null) || (value == null) || (!value.Equals(_cHOOSESTATUS)))
				{
                    object oldValue = _cHOOSESTATUS;
					_cHOOSESTATUS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_CHOOSESTATUS, oldValue, value);
				}
			}
		}

		[Property("VRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string VRID
		{
			get { return _vRID; }
			set
			{
				if ((_vRID == null) || (value == null) || (!value.Equals(_vRID)))
				{
                    object oldValue = _vRID;
					_vRID = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_VRID, oldValue, value);
				}
			}
		}

		[Property("IFBJCX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string IFBJCX
		{
			get { return _iFBJCX; }
			set
			{
				if ((_iFBJCX == null) || (value == null) || (!value.Equals(_iFBJCX)))
				{
                    object oldValue = _iFBJCX;
					_iFBJCX = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_IFBJCX, oldValue, value);
				}
			}
		}

		[Property("MINSTATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string MINSTATUS
		{
			get { return _mINSTATUS; }
			set
			{
				if ((_mINSTATUS == null) || (value == null) || (!value.Equals(_mINSTATUS)))
				{
                    object oldValue = _mINSTATUS;
					_mINSTATUS = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_MINSTATUS, oldValue, value);
				}
			}
		}

		[Property("FEECATG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FEECATG
		{
			get { return _fEECATG; }
			set
			{
				if ((_fEECATG == null) || (value == null) || (!value.Equals(_fEECATG)))
				{
                    object oldValue = _fEECATG;
					_fEECATG = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_FEECATG, oldValue, value);
				}
			}
		}

		[Property("OTHER_NAME_EN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string OTHER_NAME_EN
		{
			get { return _oTHER_NAME_EN; }
			set
			{
				if ((_oTHER_NAME_EN == null) || (value == null) || (!value.Equals(_oTHER_NAME_EN)))
				{
                    object oldValue = _oTHER_NAME_EN;
					_oTHER_NAME_EN = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_OTHER_NAME_EN, oldValue, value);
				}
			}
		}

		[Property("BJTCURR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string BJTCURR
		{
			get { return _bJTCURR; }
			set
			{
				if ((_bJTCURR == null) || (value == null) || (!value.Equals(_bJTCURR)))
				{
                    object oldValue = _bJTCURR;
					_bJTCURR = value;
					RaisePropertyChanged(SQM_BJ_PSF.Prop_BJTCURR, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_BJ_PSF
}

