// Business class BACKUPHEADER generated from BACKUPHEADER
// Creator: rw
// Created Date: [2018-01-16]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.LEAR
{
    [ActiveRecord("BACKUPHEADER")]
    public partial class BACKUPHEADER : EntityBase<BACKUPHEADER>
    {
        #region Property_Names

        public static string Prop_ADDUSER = "ADDUSER";
        public static string Prop_ADDDATE = "ADDDATE";
        public static string Prop_MESSAGETYPE = "MESSAGETYPE";
        public static string Prop_MESSAGENAME = "MESSAGENAME";
        public static string Prop_REFPO = "REFPO";
        public static string Prop_REF1 = "REF1";
        public static string Prop_REF2 = "REF2";
        public static string Prop_REF3 = "REF3";
        public static string Prop_REF4 = "REF4";
        public static string Prop_REF5 = "REF5";
        public static string Prop_REF6 = "REF6";
        public static string Prop_REF7 = "REF7";
        public static string Prop_REF8 = "REF8";
        public static string Prop_REF9 = "REF9";
        public static string Prop_REF10 = "REF10";
        public static string Prop_REF11 = "REF11";
        public static string Prop_REF12 = "REF12";
        public static string Prop_REF13 = "REF13";
        public static string Prop_REF14 = "REF14";
        public static string Prop_REF15 = "REF15";
        public static string Prop_REF16 = "REF16";
        public static string Prop_REF17 = "REF17";
        public static string Prop_REF18 = "REF18";
        public static string Prop_REF19 = "REF19";
        public static string Prop_REF20 = "REF20";
        public static string Prop_NOTES1 = "NOTES1";
        public static string Prop_NOTES2 = "NOTES2";
        public static string Prop_REF21 = "REF21";
        public static string Prop_REF22 = "REF22";
        public static string Prop_REF23 = "REF23";
        public static string Prop_REF24 = "REF24";
        public static string Prop_REF25 = "REF25";
        public static string Prop_REF26 = "REF26";
        public static string Prop_REF27 = "REF27";
        public static string Prop_REF28 = "REF28";
        public static string Prop_REF29 = "REF29";
        public static string Prop_REF30 = "REF30";
        public static string Prop_NOTES3 = "NOTES3";
        public static string Prop_QTY1 = "QTY1";
        public static string Prop_QTY2 = "QTY2";
        public static string Prop_QTY3 = "QTY3";
        public static string Prop_QTY4 = "QTY4";
        public static string Prop_QTY5 = "QTY5";
        public static string Prop_CUSTOMER = "CUSTOMER";
        public static string Prop_EDITDATE = "EDITDATE";
        public static string Prop_MAILTIPE = "MAILTIPE";

        #endregion

        #region Private_Variables

        private string _aDDUSER;
        private DateTime? _aDDDATE;
        private string _messagetype;
        private string _mESSAGENAME;
        private string _refpo;
        private string _rEF1;
        private string _rEF2;
        private string _rEF3;
        private string _rEF4;
        private string _rEF5;
        private string _rEF6;
        private string _rEF7;
        private string _rEF8;
        private string _rEF9;
        private string _rEF10;
        private string _rEF11;
        private string _rEF12;
        private string _rEF13;
        private string _rEF14;
        private string _rEF15;
        private string _rEF16;
        private string _rEF17;
        private string _rEF18;
        private string _rEF19;
        private string _rEF20;
        private string _nOTES1;
        private string _nOTES2;
        private string _rEF21;
        private string _rEF22;
        private string _rEF23;
        private string _rEF24;
        private string _rEF25;
        private string _rEF26;
        private string _rEF27;
        private string _rEF28;
        private string _rEF29;
        private string _rEF30;
        private string _nOTES3;
        private System.Decimal? _qTY1;
        private System.Decimal? _qTY2;
        private System.Decimal? _qTY3;
        private System.Decimal? _qTY4;
        private System.Decimal? _qTY5;
        private string _cUSTOMER;
        private DateTime? _eDITDATE;
        private string _mAILTIPE;


        #endregion

        #region Constructors

        public BACKUPHEADER()
        {
        }

        public BACKUPHEADER(
            string p_aDDUSER,
            DateTime? p_aDDDATE,
            string p_messagetype,
            string p_mESSAGENAME,
            string p_refpo,
            string p_rEF1,
            string p_rEF2,
            string p_rEF3,
            string p_rEF4,
            string p_rEF5,
            string p_rEF6,
            string p_rEF7,
            string p_rEF8,
            string p_rEF9,
            string p_rEF10,
            string p_rEF11,
            string p_rEF12,
            string p_rEF13,
            string p_rEF14,
            string p_rEF15,
            string p_rEF16,
            string p_rEF17,
            string p_rEF18,
            string p_rEF19,
            string p_rEF20,
            string p_nOTES1,
            string p_nOTES2,
            string p_rEF21,
            string p_rEF22,
            string p_rEF23,
            string p_rEF24,
            string p_rEF25,
            string p_rEF26,
            string p_rEF27,
            string p_rEF28,
            string p_rEF29,
            string p_rEF30,
            string p_nOTES3,
            System.Decimal? p_qTY1,
            System.Decimal? p_qTY2,
            System.Decimal? p_qTY3,
            System.Decimal? p_qTY4,
            System.Decimal? p_qTY5,
            string p_cUSTOMER,
            DateTime? p_eDITDATE,
            string p_mAILTIPE)
        {
            _aDDUSER = p_aDDUSER;
            _aDDDATE = p_aDDDATE;
            _messagetype = p_messagetype;
            _mESSAGENAME = p_mESSAGENAME;
            _refpo = p_refpo;
            _rEF1 = p_rEF1;
            _rEF2 = p_rEF2;
            _rEF3 = p_rEF3;
            _rEF4 = p_rEF4;
            _rEF5 = p_rEF5;
            _rEF6 = p_rEF6;
            _rEF7 = p_rEF7;
            _rEF8 = p_rEF8;
            _rEF9 = p_rEF9;
            _rEF10 = p_rEF10;
            _rEF11 = p_rEF11;
            _rEF12 = p_rEF12;
            _rEF13 = p_rEF13;
            _rEF14 = p_rEF14;
            _rEF15 = p_rEF15;
            _rEF16 = p_rEF16;
            _rEF17 = p_rEF17;
            _rEF18 = p_rEF18;
            _rEF19 = p_rEF19;
            _rEF20 = p_rEF20;
            _nOTES1 = p_nOTES1;
            _nOTES2 = p_nOTES2;
            _rEF21 = p_rEF21;
            _rEF22 = p_rEF22;
            _rEF23 = p_rEF23;
            _rEF24 = p_rEF24;
            _rEF25 = p_rEF25;
            _rEF26 = p_rEF26;
            _rEF27 = p_rEF27;
            _rEF28 = p_rEF28;
            _rEF29 = p_rEF29;
            _rEF30 = p_rEF30;
            _nOTES3 = p_nOTES3;
            _qTY1 = p_qTY1;
            _qTY2 = p_qTY2;
            _qTY3 = p_qTY3;
            _qTY4 = p_qTY4;
            _qTY5 = p_qTY5;
            _cUSTOMER = p_cUSTOMER;
            _eDITDATE = p_eDITDATE;
            _mAILTIPE = p_mAILTIPE;
        }

        #endregion

        #region Properties

        [Property("ADDUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string ADDUSER
        {
            get { return _aDDUSER; }
            set
            {
                if ((_aDDUSER == null) || (value == null) || (!value.Equals(_aDDUSER)))
                {
                    object oldValue = _aDDUSER;
                    _aDDUSER = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_ADDUSER, oldValue, value);
                }
            }
        }

        [Property("ADDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? ADDDATE
        {
            get { return _aDDDATE; }
            set
            {
                if (value != _aDDDATE)
                {
                    object oldValue = _aDDDATE;
                    _aDDDATE = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_ADDDATE, oldValue, value);
                }
            }
        }

        [Property("MESSAGENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string MESSAGENAME
        {
            get { return _mESSAGENAME; }
            set
            {
                if ((_mESSAGENAME == null) || (value == null) || (!value.Equals(_mESSAGENAME)))
                {
                    object oldValue = _mESSAGENAME;
                    _mESSAGENAME = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_MESSAGENAME, oldValue, value);
                }
            }
        }
        [PrimaryKey("MESSAGETYPE", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string MESSAGETYPE
        {
            get { return _messagetype; }
        }
        //[PrimaryKey("REFPO", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        //[CompositeKey]
        [Property("REFPO")]
        public string REFPO
        {
            get { return _refpo; }
        }

        //private PKModel _pk;
        //[CompositeKey]
        //public PKModel PK
        //{
        //    get { return _pk; }
        //    set { _pk = value; }
        //}


        [Property("REF1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF1
        {
            get { return _rEF1; }
            set
            {
                if ((_rEF1 == null) || (value == null) || (!value.Equals(_rEF1)))
                {
                    object oldValue = _rEF1;
                    _rEF1 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF1, oldValue, value);
                }
            }
        }

        [Property("REF2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF2
        {
            get { return _rEF2; }
            set
            {
                if ((_rEF2 == null) || (value == null) || (!value.Equals(_rEF2)))
                {
                    object oldValue = _rEF2;
                    _rEF2 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF2, oldValue, value);
                }
            }
        }

        [Property("REF3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF3
        {
            get { return _rEF3; }
            set
            {
                if ((_rEF3 == null) || (value == null) || (!value.Equals(_rEF3)))
                {
                    object oldValue = _rEF3;
                    _rEF3 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF3, oldValue, value);
                }
            }
        }

        [Property("REF4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF4
        {
            get { return _rEF4; }
            set
            {
                if ((_rEF4 == null) || (value == null) || (!value.Equals(_rEF4)))
                {
                    object oldValue = _rEF4;
                    _rEF4 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF4, oldValue, value);
                }
            }
        }

        [Property("REF5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 240)]
        public string REF5
        {
            get { return _rEF5; }
            set
            {
                if ((_rEF5 == null) || (value == null) || (!value.Equals(_rEF5)))
                {
                    object oldValue = _rEF5;
                    _rEF5 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF5, oldValue, value);
                }
            }
        }

        [Property("REF6", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 240)]
        public string REF6
        {
            get { return _rEF6; }
            set
            {
                if ((_rEF6 == null) || (value == null) || (!value.Equals(_rEF6)))
                {
                    object oldValue = _rEF6;
                    _rEF6 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF6, oldValue, value);
                }
            }
        }

        [Property("REF7", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF7
        {
            get { return _rEF7; }
            set
            {
                if ((_rEF7 == null) || (value == null) || (!value.Equals(_rEF7)))
                {
                    object oldValue = _rEF7;
                    _rEF7 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF7, oldValue, value);
                }
            }
        }

        [Property("REF8", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF8
        {
            get { return _rEF8; }
            set
            {
                if ((_rEF8 == null) || (value == null) || (!value.Equals(_rEF8)))
                {
                    object oldValue = _rEF8;
                    _rEF8 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF8, oldValue, value);
                }
            }
        }

        [Property("REF9", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF9
        {
            get { return _rEF9; }
            set
            {
                if ((_rEF9 == null) || (value == null) || (!value.Equals(_rEF9)))
                {
                    object oldValue = _rEF9;
                    _rEF9 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF9, oldValue, value);
                }
            }
        }

        [Property("REF10", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF10
        {
            get { return _rEF10; }
            set
            {
                if ((_rEF10 == null) || (value == null) || (!value.Equals(_rEF10)))
                {
                    object oldValue = _rEF10;
                    _rEF10 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF10, oldValue, value);
                }
            }
        }

        [Property("REF11", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF11
        {
            get { return _rEF11; }
            set
            {
                if ((_rEF11 == null) || (value == null) || (!value.Equals(_rEF11)))
                {
                    object oldValue = _rEF11;
                    _rEF11 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF11, oldValue, value);
                }
            }
        }

        [Property("REF12", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF12
        {
            get { return _rEF12; }
            set
            {
                if ((_rEF12 == null) || (value == null) || (!value.Equals(_rEF12)))
                {
                    object oldValue = _rEF12;
                    _rEF12 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF12, oldValue, value);
                }
            }
        }

        [Property("REF13", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF13
        {
            get { return _rEF13; }
            set
            {
                if ((_rEF13 == null) || (value == null) || (!value.Equals(_rEF13)))
                {
                    object oldValue = _rEF13;
                    _rEF13 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF13, oldValue, value);
                }
            }
        }

        [Property("REF14", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF14
        {
            get { return _rEF14; }
            set
            {
                if ((_rEF14 == null) || (value == null) || (!value.Equals(_rEF14)))
                {
                    object oldValue = _rEF14;
                    _rEF14 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF14, oldValue, value);
                }
            }
        }

        [Property("REF15", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF15
        {
            get { return _rEF15; }
            set
            {
                if ((_rEF15 == null) || (value == null) || (!value.Equals(_rEF15)))
                {
                    object oldValue = _rEF15;
                    _rEF15 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF15, oldValue, value);
                }
            }
        }

        [Property("REF16", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF16
        {
            get { return _rEF16; }
            set
            {
                if ((_rEF16 == null) || (value == null) || (!value.Equals(_rEF16)))
                {
                    object oldValue = _rEF16;
                    _rEF16 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF16, oldValue, value);
                }
            }
        }

        [Property("REF17", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF17
        {
            get { return _rEF17; }
            set
            {
                if ((_rEF17 == null) || (value == null) || (!value.Equals(_rEF17)))
                {
                    object oldValue = _rEF17;
                    _rEF17 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF17, oldValue, value);
                }
            }
        }

        [Property("REF18", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF18
        {
            get { return _rEF18; }
            set
            {
                if ((_rEF18 == null) || (value == null) || (!value.Equals(_rEF18)))
                {
                    object oldValue = _rEF18;
                    _rEF18 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF18, oldValue, value);
                }
            }
        }

        [Property("REF19", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF19
        {
            get { return _rEF19; }
            set
            {
                if ((_rEF19 == null) || (value == null) || (!value.Equals(_rEF19)))
                {
                    object oldValue = _rEF19;
                    _rEF19 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF19, oldValue, value);
                }
            }
        }

        [Property("REF20", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string REF20
        {
            get { return _rEF20; }
            set
            {
                if ((_rEF20 == null) || (value == null) || (!value.Equals(_rEF20)))
                {
                    object oldValue = _rEF20;
                    _rEF20 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF20, oldValue, value);
                }
            }
        }

        [Property("NOTES1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string NOTES1
        {
            get { return _nOTES1; }
            set
            {
                if ((_nOTES1 == null) || (value == null) || (!value.Equals(_nOTES1)))
                {
                    object oldValue = _nOTES1;
                    _nOTES1 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_NOTES1, oldValue, value);
                }
            }
        }

        [Property("NOTES2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string NOTES2
        {
            get { return _nOTES2; }
            set
            {
                if ((_nOTES2 == null) || (value == null) || (!value.Equals(_nOTES2)))
                {
                    object oldValue = _nOTES2;
                    _nOTES2 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_NOTES2, oldValue, value);
                }
            }
        }

        [Property("REF21", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF21
        {
            get { return _rEF21; }
            set
            {
                if ((_rEF21 == null) || (value == null) || (!value.Equals(_rEF21)))
                {
                    object oldValue = _rEF21;
                    _rEF21 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF21, oldValue, value);
                }
            }
        }

        [Property("REF22", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF22
        {
            get { return _rEF22; }
            set
            {
                if ((_rEF22 == null) || (value == null) || (!value.Equals(_rEF22)))
                {
                    object oldValue = _rEF22;
                    _rEF22 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF22, oldValue, value);
                }
            }
        }

        [Property("REF23", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF23
        {
            get { return _rEF23; }
            set
            {
                if ((_rEF23 == null) || (value == null) || (!value.Equals(_rEF23)))
                {
                    object oldValue = _rEF23;
                    _rEF23 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF23, oldValue, value);
                }
            }
        }

        [Property("REF24", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF24
        {
            get { return _rEF24; }
            set
            {
                if ((_rEF24 == null) || (value == null) || (!value.Equals(_rEF24)))
                {
                    object oldValue = _rEF24;
                    _rEF24 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF24, oldValue, value);
                }
            }
        }

        [Property("REF25", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF25
        {
            get { return _rEF25; }
            set
            {
                if ((_rEF25 == null) || (value == null) || (!value.Equals(_rEF25)))
                {
                    object oldValue = _rEF25;
                    _rEF25 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF25, oldValue, value);
                }
            }
        }

        [Property("REF26", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF26
        {
            get { return _rEF26; }
            set
            {
                if ((_rEF26 == null) || (value == null) || (!value.Equals(_rEF26)))
                {
                    object oldValue = _rEF26;
                    _rEF26 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF26, oldValue, value);
                }
            }
        }

        [Property("REF27", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF27
        {
            get { return _rEF27; }
            set
            {
                if ((_rEF27 == null) || (value == null) || (!value.Equals(_rEF27)))
                {
                    object oldValue = _rEF27;
                    _rEF27 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF27, oldValue, value);
                }
            }
        }

        [Property("REF28", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF28
        {
            get { return _rEF28; }
            set
            {
                if ((_rEF28 == null) || (value == null) || (!value.Equals(_rEF28)))
                {
                    object oldValue = _rEF28;
                    _rEF28 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF28, oldValue, value);
                }
            }
        }

        [Property("REF29", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF29
        {
            get { return _rEF29; }
            set
            {
                if ((_rEF29 == null) || (value == null) || (!value.Equals(_rEF29)))
                {
                    object oldValue = _rEF29;
                    _rEF29 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF29, oldValue, value);
                }
            }
        }

        [Property("REF30", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
        public string REF30
        {
            get { return _rEF30; }
            set
            {
                if ((_rEF30 == null) || (value == null) || (!value.Equals(_rEF30)))
                {
                    object oldValue = _rEF30;
                    _rEF30 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_REF30, oldValue, value);
                }
            }
        }

        [Property("NOTES3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string NOTES3
        {
            get { return _nOTES3; }
            set
            {
                if ((_nOTES3 == null) || (value == null) || (!value.Equals(_nOTES3)))
                {
                    object oldValue = _nOTES3;
                    _nOTES3 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_NOTES3, oldValue, value);
                }
            }
        }

        [Property("QTY1", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? QTY1
        {
            get { return _qTY1; }
            set
            {
                if (value != _qTY1)
                {
                    object oldValue = _qTY1;
                    _qTY1 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_QTY1, oldValue, value);
                }
            }
        }

        [Property("QTY2", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? QTY2
        {
            get { return _qTY2; }
            set
            {
                if (value != _qTY2)
                {
                    object oldValue = _qTY2;
                    _qTY2 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_QTY2, oldValue, value);
                }
            }
        }

        [Property("QTY3", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? QTY3
        {
            get { return _qTY3; }
            set
            {
                if (value != _qTY3)
                {
                    object oldValue = _qTY3;
                    _qTY3 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_QTY3, oldValue, value);
                }
            }
        }

        [Property("QTY4", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? QTY4
        {
            get { return _qTY4; }
            set
            {
                if (value != _qTY4)
                {
                    object oldValue = _qTY4;
                    _qTY4 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_QTY4, oldValue, value);
                }
            }
        }

        [Property("QTY5", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? QTY5
        {
            get { return _qTY5; }
            set
            {
                if (value != _qTY5)
                {
                    object oldValue = _qTY5;
                    _qTY5 = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_QTY5, oldValue, value);
                }
            }
        }

        [Property("CUSTOMER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 120)]
        public string CUSTOMER
        {
            get { return _cUSTOMER; }
            set
            {
                if ((_cUSTOMER == null) || (value == null) || (!value.Equals(_cUSTOMER)))
                {
                    object oldValue = _cUSTOMER;
                    _cUSTOMER = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_CUSTOMER, oldValue, value);
                }
            }
        }

        [Property("EDITDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? EDITDATE
        {
            get { return _eDITDATE; }
            set
            {
                if (value != _eDITDATE)
                {
                    object oldValue = _eDITDATE;
                    _eDITDATE = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_EDITDATE, oldValue, value);
                }
            }
        }

        [Property("MAILTIPE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public string MAILTIPE
        {
            get { return _mAILTIPE; }
            set
            {
                if ((_mAILTIPE == null) || (value == null) || (!value.Equals(_mAILTIPE)))
                {
                    object oldValue = _mAILTIPE;
                    _mAILTIPE = value;
                    RaisePropertyChanged(BACKUPHEADER.Prop_MAILTIPE, oldValue, value);
                }
            }
        }

        #endregion
    } // BACKUPHEADER


    [Serializable]
    public class PKModel
    {
        public string _messagetype;
        public string _refpo;

        [KeyProperty]
        public string MESSAGETYPE
        {
            get { return _messagetype; }
            set { _messagetype = value; }
        }

        [KeyProperty]
        public string REFPO
        {
            get { return _refpo; }
            set { _refpo = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            PKModel key = obj as PKModel;
            if (key == null)
            {
                return false;
            }
            if (this.MESSAGETYPE == key._messagetype && this.REFPO == key._refpo)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.MESSAGETYPE.GetHashCode() ^ this.REFPO.GetHashCode();//base.GetHashCode();
        }
    }
}

