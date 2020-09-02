// Business class SQM_BJ_MAIN_BASIC generated from SQM_BJ_MAIN_BASIC
// Creator: rw
// Created Date: [2018-05-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM.Model
{
    [ActiveRecord("SQM_BJ_MAIN_BASIC")]
    public partial class SQM_BJ_MAIN_BASIC : EntityBase<SQM_BJ_MAIN_BASIC>
    {
        #region Property_Names

        public static string Prop_BJNAME = "BJNAME";
        public static string Prop_BJNAME_EN = "BJNAME_EN";//英文报价名称
        public static string Prop_RID = "RID";
        public static string Prop_MODIFYTIME = "MODIFYTIME";
        public static string Prop_MODIFYUSER = "MODIFYUSER";
        public static string Prop_CREATETIME = "CREATETIME";
        public static string Prop_CREATEUSER = "CREATEUSER";
        public static string Prop_REMARK = "REMARK";
        public static string Prop_DB_KEY = "DB_KEY";
        public static string Prop_CREATEID = "CREATEID";
        public static string Prop_MODIFYID = "MODIFYID";
        public static string Pro_DTFROM = "DTFROM";
        public static string Pro_DTTO = "DTTO";
        public static string Pro_MEMO = "MEMO";
        public static string Pro_FBPRICE = "FBPRICE";
        public static string Pro_ORIGINAL = "ORIGINAL";
        public static string Pro_MVFILECODE = "MVFILECODE";
        public static string Pro_MVFILE = "MVFILE";
        public static string Pro_XSYBJID = "XSYBJID";
        public static string Pro_BJTCURR = "BJTCURR";
        public static string Prop_AFFILIATION = "AFFILIATION";

        #endregion

        #region Private_Variables

        private string _bJNAME;
        private string _bJNAME_EN;//英文报价名称
        private string _rid;
        private DateTime? _mODIFYTIME;
        private string _mODIFYUSER;
        private DateTime? _cREATETIME;
        private string _cREATEUSER;
        private string _rEMARK;
        private string _dB_KEY;
        private string _cREATEID;
        private string _mODIFYID;
        private DateTime? _dTFROM;
        private DateTime? _dTTO;
        private string _mEMO;
        private string _fBPRICE;
        private string _oRIGINAL;
        private string _mVFILECODE;
        private string _mVFILE;
        private string _xSYBJID;
        private string _bJTCURR;
        private string _aFFILIATION;

        #endregion

        #region Constructors

        public SQM_BJ_MAIN_BASIC()
        {
        }

        public SQM_BJ_MAIN_BASIC(
            string p_bJNAME,
            string p_bJNAME_EN,//英文报价名称
            string p_rid,
            DateTime? p_mODIFYTIME,
            string p_mODIFYUSER,
            DateTime? p_cREATETIME,
            string p_cREATEUSER,
            string p_rEMARK,
            string p_dB_KEY,
            string p_cREATEID,
            string p_mODIFYID,
            DateTime? p_dTFROM,
            DateTime? p_dTTO,
            string p_mEMO,
            string p_fBPRICE,
            string p_oRIGINAL,
            string p_mVFILECODE,
            string p_mVFILE,
            string p_xSYBJID,
            string p_bJTCURR,
            string p_aFFILIATION
            )
        {
            _bJNAME = p_bJNAME;
            _bJNAME_EN = p_bJNAME_EN;//英文报价名称
            _rid = p_rid;
            _mODIFYTIME = p_mODIFYTIME;
            _mODIFYUSER = p_mODIFYUSER;
            _cREATETIME = p_cREATETIME;
            _cREATEUSER = p_cREATEUSER;
            _rEMARK = p_rEMARK;
            _dB_KEY = p_dB_KEY;
            _cREATEID = p_cREATEID;
            _mODIFYID = p_mODIFYID;
            _dTFROM = p_dTFROM;
            _dTTO = p_dTTO;
            _mEMO = p_mEMO;
            _fBPRICE = p_fBPRICE;
            _oRIGINAL = p_oRIGINAL;
            _mVFILECODE = p_mVFILECODE;
            _mVFILE = p_mVFILE;
            _xSYBJID = p_xSYBJID;
            _bJTCURR = p_bJTCURR;
            _aFFILIATION = p_aFFILIATION;
        }

        #endregion

        #region Properties

        [Property("BJNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string BJNAME
        {
            get { return _bJNAME; }
            set
            {
                if ((_bJNAME == null) || (value == null) || (!value.Equals(_bJNAME)))
                {
                    object oldValue = _bJNAME;
                    _bJNAME = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_BJNAME, oldValue, value);
                }
            }
        }
        //新增属性：英文报价名称
        [Property("BJNAME_EN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string BJNAME_EN
        {
            get { return _bJNAME_EN; }
            set
            {
                if ((_bJNAME_EN == null) || (value == null) || (!value.Equals(_bJNAME_EN)))
                {
                    object oldValue = _bJNAME_EN;
                    _bJNAME_EN = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_BJNAME_EN, oldValue, value);
                }
            }
        }

        [PrimaryKey(PrimaryKeyType.Assigned, "RID", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            set { _rid = value; }
            get { return _rid; }
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_MODIFYTIME, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_MODIFYUSER, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_CREATETIME, oldValue, value);
                }
            }
        }

        [Property("DTFROM", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? DTFROM
        {
            get { return _dTFROM; }
            set
            {
                if (value != _dTFROM)
                {
                    object oldValue = _dTFROM;
                    _dTFROM = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_DTFROM, oldValue, value);
                }
            }
        }

        [Property("DTTO", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? DTTO
        {
            get { return _dTTO; }
            set
            {
                if (value != _dTTO)
                {
                    object oldValue = _dTTO;
                    _dTTO = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_DTTO, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_CREATEUSER, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_REMARK, oldValue, value);
                }
            }
        }

        [Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
        public string MEMO
        {
            get { return _mEMO; }
            set
            {
                if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
                {
                    object oldValue = _mEMO;
                    _mEMO = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_MEMO, oldValue, value);
                }
            }
        }

        [Property("DB_KEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
        public string DB_KEY
        {
            get { return _dB_KEY; }
            set
            {
                if ((_dB_KEY == null) || (value == null) || (!value.Equals(_dB_KEY)))
                {
                    object oldValue = _dB_KEY;
                    _dB_KEY = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_DB_KEY, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_CREATEID, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_MODIFYID, oldValue, value);
                }
            }
        }
        [Property("FBPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string FBPRICE
        {
            get { return _fBPRICE; }
            set
            {
                if ((_fBPRICE == null) || (value == null) || (!value.Equals(_fBPRICE)))
                {
                    object oldValue = _fBPRICE;
                    _fBPRICE = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_FBPRICE, oldValue, value);
                }
            }
        }
        [Property("ORIGINAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string ORIGINAL
        {
            get { return _fBPRICE; }
            set
            {
                if ((_oRIGINAL == null) || (value == null) || (!value.Equals(_oRIGINAL)))
                {
                    object oldValue = _oRIGINAL;
                    _oRIGINAL = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_ORIGINAL, oldValue, value);
                }
            }
        }
        [Property("MVFILECODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string MVFILECODE
        {
            get { return _mVFILECODE; }
            set
            {
                if ((_mVFILECODE == null) || (value == null) || (!value.Equals(_mVFILECODE)))
                {
                    object oldValue = _mVFILECODE;
                    _mVFILECODE = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_MVFILECODE, oldValue, value);
                }
            }
        }
        [Property("MVFILE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string MVFILE
        {
            get { return _mVFILE; }
            set
            {
                if ((_mVFILE == null) || (value == null) || (!value.Equals(_mVFILE)))
                {
                    object oldValue = _mVFILE;
                    _mVFILE = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_MVFILE, oldValue, value);
                }
            }
        }
        [Property("XSYBJID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
        public string XSYBJID
        {
            get { return _xSYBJID; }
            set
            {
                if ((_xSYBJID == null) || (value == null) || (!value.Equals(_xSYBJID)))
                {
                    object oldValue = _xSYBJID;
                    _xSYBJID = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_XSYBJID, oldValue, value);
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
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Pro_BJTCURR, oldValue, value);
                }
            }
        }
        #endregion

        [Property("AFFILIATION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string AFFILIATION
        {
            get { return _aFFILIATION; }
            set
            {
                if ((_aFFILIATION == null) || (value == null) || (!value.Equals(_aFFILIATION)))
                {
                    object oldValue = _aFFILIATION;
                    _aFFILIATION = value;
                    RaisePropertyChanged(SQM_BJ_MAIN_BASIC.Prop_AFFILIATION, oldValue, value);
                }
            }
        }
    } // SQM_BJ_MAIN_BASIC
}

