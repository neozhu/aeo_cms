// Business class BJ_SSO_LOGIN generated from BJ_SSO_LOGIN
// Creator: rw
// Created Date: [2019-10-24]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM
{
    [ActiveRecord("BJ_SSO_LOGIN")]
    public partial class BJ_SSO_LOGIN : EntityBase<BJ_SSO_LOGIN>
    {
        #region Property_Names

        public static string Prop_HASH = "HASH";
        public static string Prop_STAFFKEY = "STAFFKEY";
        //public static string Prop_CUSTOMERNO = "CUSTOMERNO";
        //public static string Prop_BUSSNIESSNO = "BUSSNIESSNO";
        public static string Prop_SYSTEMKEY = "SYSTEMKEY";
        public static string Prop_MEMO = "MEMO";
        public static string Prop_CREATETIME = "CREATETIME";
        public static string Prop_CREATEUSER = "CREATEUSER";
        public static string Prop_MODIFYTIME = "MODIFYTIME";
        public static string Prop_MODIFYUSER = "MODIFYUSER";
        public static string Prop_RID = "RID";
        public static string Prop_ACTIVITYTIME = "ACTIVITYTIME";
        public static string Prop_DateSpan = "DateSpan";

        #endregion

        #region Private_Variables

        private string _hASH;
        private string _sTAFFKEY;
        private string _cUSTOMERNO;
        private string _bUSSNIESSNO;
        private string _sYSTEMKEY;
        private string _mEMO;
        private DateTime? _cREATETIME;
        private string _cREATEUSER;
        private DateTime? _mODIFYTIME;
        private string _mODIFYUSER;
        private string _rid;
        private System.Decimal? _aCTIVITYTIME;
        private string _dateSpan;


        #endregion

        #region Constructors

        public BJ_SSO_LOGIN()
        {
        }

        public BJ_SSO_LOGIN(
            string p_hASH,
            string p_sTAFFKEY,
            //string p_cUSTOMERNO,
            //string p_bUSSNIESSNO,
            string p_sYSTEMKEY,
            string p_mEMO,
            DateTime? p_cREATETIME,
            string p_cREATEUSER,
            DateTime? p_mODIFYTIME,
            string p_mODIFYUSER,
            string p_rid,
            System.Decimal? p_aCTIVITYTIME,
            string p_dateSpan)
        {
            _hASH = p_hASH;
            _sTAFFKEY = p_sTAFFKEY;
            //_cUSTOMERNO = p_cUSTOMERNO;
            //_bUSSNIESSNO = p_bUSSNIESSNO;
            _sYSTEMKEY = p_sYSTEMKEY;
            _mEMO = p_mEMO;
            _cREATETIME = p_cREATETIME;
            _cREATEUSER = p_cREATEUSER;
            _mODIFYTIME = p_mODIFYTIME;
            _mODIFYUSER = p_mODIFYUSER;
            _rid = p_rid;
            _aCTIVITYTIME = p_aCTIVITYTIME;
            _dateSpan = p_dateSpan;
        }

        #endregion

        #region Properties

        [Property("HASH", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 1000)]
        public string HASH
        {
            get { return _hASH; }
            set
            {
                if ((_hASH == null) || (value == null) || (!value.Equals(_hASH)))
                {
                    object oldValue = _hASH;
                    _hASH = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_HASH, oldValue, value);
                }
            }
        }

        [Property("STAFFKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string STAFFKEY
        {
            get { return _sTAFFKEY; }
            set
            {
                if ((_sTAFFKEY == null) || (value == null) || (!value.Equals(_sTAFFKEY)))
                {
                    object oldValue = _sTAFFKEY;
                    _sTAFFKEY = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_STAFFKEY, oldValue, value);
                }
            }
        }

        //[Property("CUSTOMERNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        //public string CUSTOMERNO
        //{
        //    get { return _cUSTOMERNO; }
        //    set
        //    {
        //        if ((_cUSTOMERNO == null) || (value == null) || (!value.Equals(_cUSTOMERNO)))
        //        {
        //            object oldValue = _cUSTOMERNO;
        //            _cUSTOMERNO = value;
        //            RaisePropertyChanged(BJ_SSO_LOGIN.Prop_CUSTOMERNO, oldValue, value);
        //        }
        //    }
        //}

        //[Property("BUSSNIESSNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        //public string BUSSNIESSNO
        //{
        //    get { return _bUSSNIESSNO; }
        //    set
        //    {
        //        if ((_bUSSNIESSNO == null) || (value == null) || (!value.Equals(_bUSSNIESSNO)))
        //        {
        //            object oldValue = _bUSSNIESSNO;
        //            _bUSSNIESSNO = value;
        //            RaisePropertyChanged(BJ_SSO_LOGIN.Prop_BUSSNIESSNO, oldValue, value);
        //        }
        //    }
        //}

        [Property("SYSTEMKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string SYSTEMKEY
        {
            get { return _sYSTEMKEY; }
            set
            {
                if ((_sYSTEMKEY == null) || (value == null) || (!value.Equals(_sYSTEMKEY)))
                {
                    object oldValue = _sYSTEMKEY;
                    _sYSTEMKEY = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_SYSTEMKEY, oldValue, value);
                }
            }
        }

        [Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
        public string MEMO
        {
            get { return _mEMO; }
            set
            {
                if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
                {
                    object oldValue = _mEMO;
                    _mEMO = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_MEMO, oldValue, value);
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
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_CREATETIME, oldValue, value);
                }
            }
        }

        [Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string CREATEUSER
        {
            get { return _cREATEUSER; }
            set
            {
                if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
                {
                    object oldValue = _cREATEUSER;
                    _cREATEUSER = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_CREATEUSER, oldValue, value);
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
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_MODIFYTIME, oldValue, value);
                }
            }
        }

        [Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string MODIFYUSER
        {
            get { return _mODIFYUSER; }
            set
            {
                if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
                {
                    object oldValue = _mODIFYUSER;
                    _mODIFYUSER = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_MODIFYUSER, oldValue, value);
                }
            }
        }

        [PrimaryKey("RID", Generator = PrimaryKeyType.Assigned, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            get { return _rid; }
            set { _rid = value; }
        }

        [Property("ACTIVITYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? ACTIVITYTIME
        {
            get { return _aCTIVITYTIME; }
            set
            {
                if (value != _aCTIVITYTIME)
                {
                    object oldValue = _aCTIVITYTIME;
                    _aCTIVITYTIME = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_ACTIVITYTIME, oldValue, value);
                }
            }
        }

        [Property("DateSpan", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string DateSpan
        {
            get { return _dateSpan; }
            set
            {
                if ((_dateSpan == null) || (value == null) || (!value.Equals(_dateSpan)))
                {
                    object oldValue = _dateSpan;
                    _dateSpan = value;
                    RaisePropertyChanged(BJ_SSO_LOGIN.Prop_DateSpan, oldValue, value);
                }
            }
        }

        #endregion
    } // BJ_SSO_LOGIN
}

