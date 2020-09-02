// Business class SYSMONTORSET generated from SYSMONTORSET
// Creator: Ray
// Created Date: [2014-05-04]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
    [ActiveRecord("SYSMONTORSET")]
    public partial class SYSMONTORSET : EntityBase<SYSMONTORSET>
    {
        #region Property_Names

        public static string Prop_ID = "ID";
        public static string Prop_TBLENCODE = "TBLENCODE";
        public static string Prop_TBLNAME = "TBLNAME";
        public static string Prop_ORGANIZATIONIDS = "ORGANIZATIONIDS";
        public static string Prop_TBLCLNS = "TBLCLNS";
        public static string Prop_CREATEID = "CREATEID";
        public static string Prop_CREATENAME = "CREATENAME";
        public static string Prop_CREATETIME = "CREATETIME";
        public static string Prop_ISMONTOR = "ISMONTOR";
        public static string Prop_PERSONIDS = "PERSONIDS";
        public static string Prop_PERSONNAMES = "PERSONNAMES";
        public static string Prop_ORGANIZATIONNAMES = "ORGANIZATIONNAMES";
        public static string Prop_WEEK = "WEEK";
        public static string Prop_DATATIMEPOINT = "DATATIMEPOINT";
        public static string Prop_STARTTIME = "STARTTIME";
        public static string Prop_ENDTIME = "ENDTIME";
        public static string Prop_TIMEPOINT = "TIMEPOINT";
        public static string Prop_EXT1 = "EXT1";
        public static string Prop_DEPTIDS = "DEPTIDS";
        public static string Prop_DEPTNAMES = "DEPTNAMES";

        #endregion

        #region Private_Variables

        private string _id;
        private string _tBLENCODE;
        private string _tBLNAME;
        private string _oRGANIZATIONIDS;
        private string _tBLCLNS;
        private string _cREATEID;
        private string _cREATENAME;
        private DateTime? _cREATETIME;
        private string _iSMONTOR;
        private string _pERSONIDS;
        private string _pERSONNAMES;
        private string _oRGANIZATIONNAMES;
        private string _wEEK;
        private DateTime? _dATATIMEPOINT;
        private DateTime? _sTARTTIME;
        private DateTime? _eNDTIME;
        private string _tIMEPOINT;
        private string _eXT1;
        private string _dEPTIDS;
        private string _dEPTNAMES;


        #endregion

        #region Constructors

        public SYSMONTORSET()
        {
        }

        public SYSMONTORSET(
            string p_id,
            string p_tBLENCODE,
            string p_tBLNAME,
            string p_oRGANIZATIONIDS,
            string p_tBLCLNS,
            string p_cREATEID,
            string p_cREATENAME,
            DateTime? p_cREATETIME,
            string p_iSMONTOR,
            string p_pERSONIDS,
            string p_pERSONNAMES,
            string p_oRGANIZATIONNAMES,
            string p_wEEK,
            DateTime? p_dATATIMEPOINT,
            DateTime? p_sTARTTIME,
            DateTime? p_eNDTIME,
            string p_tIMEPOINT,
            string p_eXT1,
            string p_dEPTIDS,
            string p_dEPTNAMES)
        {
            _id = p_id;
            _tBLENCODE = p_tBLENCODE;
            _tBLNAME = p_tBLNAME;
            _oRGANIZATIONIDS = p_oRGANIZATIONIDS;
            _tBLCLNS = p_tBLCLNS;
            _cREATEID = p_cREATEID;
            _cREATENAME = p_cREATENAME;
            _cREATETIME = p_cREATETIME;
            _iSMONTOR = p_iSMONTOR;
            _pERSONIDS = p_pERSONIDS;
            _pERSONNAMES = p_pERSONNAMES;
            _oRGANIZATIONNAMES = p_oRGANIZATIONNAMES;
            _wEEK = p_wEEK;
            _dATATIMEPOINT = p_dATATIMEPOINT;
            _sTARTTIME = p_sTARTTIME;
            _eNDTIME = p_eNDTIME;
            _tIMEPOINT = p_tIMEPOINT;
            _eXT1 = p_eXT1;
            _dEPTIDS = p_dEPTIDS;
            _dEPTNAMES = p_dEPTNAMES;
        }

        #endregion

        #region Properties

        [PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string ID
        {
            get { return _id; }
            set { _id = value; } // 处理列表编辑时去掉注释

        }

        [Property("TBLENCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string TBLENCODE
        {
            get { return _tBLENCODE; }
            set
            {
                if ((_tBLENCODE == null) || (value == null) || (!value.Equals(_tBLENCODE)))
                {
                    object oldValue = _tBLENCODE;
                    _tBLENCODE = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_TBLENCODE, oldValue, value);
                }
            }

        }

        [Property("TBLNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 600)]
        public string TBLNAME
        {
            get { return _tBLNAME; }
            set
            {
                if ((_tBLNAME == null) || (value == null) || (!value.Equals(_tBLNAME)))
                {
                    object oldValue = _tBLNAME;
                    _tBLNAME = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_TBLNAME, oldValue, value);
                }
            }

        }

        [Property("ORGANIZATIONIDS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string ORGANIZATIONIDS
        {
            get { return _oRGANIZATIONIDS; }
            set
            {
                if ((_oRGANIZATIONIDS == null) || (value == null) || (!value.Equals(_oRGANIZATIONIDS)))
                {
                    object oldValue = _oRGANIZATIONIDS;
                    _oRGANIZATIONIDS = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_ORGANIZATIONIDS, oldValue, value);
                }
            }

        }

        [Property("TBLCLNS", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "Aim.Portal.Model.OracleClobField, Aim.Portal")]
        public string TBLCLNS
        {
            get { return _tBLCLNS; }
            set
            {
                if ((_tBLCLNS == null) || (value == null) || (!value.Equals(_tBLCLNS)))
                {
                    object oldValue = _tBLCLNS;
                    _tBLCLNS = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_TBLCLNS, oldValue, value);
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
                    RaisePropertyChanged(SYSMONTORSET.Prop_CREATEID, oldValue, value);
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
                    RaisePropertyChanged(SYSMONTORSET.Prop_CREATENAME, oldValue, value);
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
                    RaisePropertyChanged(SYSMONTORSET.Prop_CREATETIME, oldValue, value);
                }
            }

        }

        [Property("ISMONTOR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
        public string ISMONTOR
        {
            get { return _iSMONTOR; }
            set
            {
                if ((_iSMONTOR == null) || (value == null) || (!value.Equals(_iSMONTOR)))
                {
                    object oldValue = _iSMONTOR;
                    _iSMONTOR = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_ISMONTOR, oldValue, value);
                }
            }

        }

        [Property("PERSONIDS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string PERSONIDS
        {
            get { return _pERSONIDS; }
            set
            {
                if ((_pERSONIDS == null) || (value == null) || (!value.Equals(_pERSONIDS)))
                {
                    object oldValue = _pERSONIDS;
                    _pERSONIDS = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_PERSONIDS, oldValue, value);
                }
            }

        }

        [Property("PERSONNAMES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string PERSONNAMES
        {
            get { return _pERSONNAMES; }
            set
            {
                if ((_pERSONNAMES == null) || (value == null) || (!value.Equals(_pERSONNAMES)))
                {
                    object oldValue = _pERSONNAMES;
                    _pERSONNAMES = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_PERSONNAMES, oldValue, value);
                }
            }

        }

        [Property("ORGANIZATIONNAMES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string ORGANIZATIONNAMES
        {
            get { return _oRGANIZATIONNAMES; }
            set
            {
                if ((_oRGANIZATIONNAMES == null) || (value == null) || (!value.Equals(_oRGANIZATIONNAMES)))
                {
                    object oldValue = _oRGANIZATIONNAMES;
                    _oRGANIZATIONNAMES = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_ORGANIZATIONNAMES, oldValue, value);
                }
            }

        }

        [Property("WEEK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
        public string WEEK
        {
            get { return _wEEK; }
            set
            {
                if ((_wEEK == null) || (value == null) || (!value.Equals(_wEEK)))
                {
                    object oldValue = _wEEK;
                    _wEEK = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_WEEK, oldValue, value);
                }
            }

        }

        [Property("DATATIMEPOINT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? DATATIMEPOINT
        {
            get { return _dATATIMEPOINT; }
            set
            {
                if (value != _dATATIMEPOINT)
                {
                    object oldValue = _dATATIMEPOINT;
                    _dATATIMEPOINT = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_DATATIMEPOINT, oldValue, value);
                }
            }

        }

        [Property("STARTTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? STARTTIME
        {
            get { return _sTARTTIME; }
            set
            {
                if (value != _sTARTTIME)
                {
                    object oldValue = _sTARTTIME;
                    _sTARTTIME = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_STARTTIME, oldValue, value);
                }
            }

        }

        [Property("ENDTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? ENDTIME
        {
            get { return _eNDTIME; }
            set
            {
                if (value != _eNDTIME)
                {
                    object oldValue = _eNDTIME;
                    _eNDTIME = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_ENDTIME, oldValue, value);
                }
            }

        }

        [Property("TIMEPOINT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string TIMEPOINT
        {
            get { return _tIMEPOINT; }
            set
            {
                if ((_tIMEPOINT == null) || (value == null) || (!value.Equals(_tIMEPOINT)))
                {
                    object oldValue = _tIMEPOINT;
                    _tIMEPOINT = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_TIMEPOINT, oldValue, value);
                }
            }

        }

        [Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string EXT1
        {
            get { return _eXT1; }
            set
            {
                if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
                {
                    object oldValue = _eXT1;
                    _eXT1 = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_EXT1, oldValue, value);
                }
            }

        }

        [Property("DEPTIDS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
        public string DEPTIDS
        {
            get { return _dEPTIDS; }
            set
            {
                if ((_dEPTIDS == null) || (value == null) || (!value.Equals(_dEPTIDS)))
                {
                    object oldValue = _dEPTIDS;
                    _dEPTIDS = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_DEPTIDS, oldValue, value);
                }
            }

        }

        [Property("DEPTNAMES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
        public string DEPTNAMES
        {
            get { return _dEPTNAMES; }
            set
            {
                if ((_dEPTNAMES == null) || (value == null) || (!value.Equals(_dEPTNAMES)))
                {
                    object oldValue = _dEPTNAMES;
                    _dEPTNAMES = value;
                    RaisePropertyChanged(SYSMONTORSET.Prop_DEPTNAMES, oldValue, value);
                }
            }

        }

        #endregion
    } // SYSMONTORSET
}

