// Business class LANGUAGESTYPE generated from LANGUAGESTYPE
// Created Date: [2014-04-15]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
    [ActiveRecord("LANGUAGESTYPE")]
    public partial class LANGUAGESTYPE : ModelBase<LANGUAGESTYPE>
    {
        #region Property_Names

        public static string Prop_ID = "ID";
        public static string Prop_LANGCODE = "LANGCODE";
        public static string Prop_LANGNAME = "LANGNAME";
        public static string Prop_PREFIX = "PREFIX";
        public static string Prop_SORTINDEX = "SORTINDEX";
        public static string Prop_CREATEID = "CREATEID";
        public static string Prop_CREATENAME = "CREATENAME";
        public static string Prop_CREATETIME = "CREATETIME";

        #endregion

        #region Private_Variables

        private string _id;
        private string _lANGCODE;
        private string _lANGNAME;
        private string _pREFIX;
        private System.Decimal? _sORTINDEX;
        private string _cREATEID;
        private string _cREATENAME;
        private DateTime? _cREATETIME;


        #endregion

        #region Constructors

        public LANGUAGESTYPE()
        {
        }

        public LANGUAGESTYPE(
            string p_id,
            string p_lANGCODE,
            string p_lANGNAME,
            string p_pREFIX,
            System.Decimal? p_sORTINDEX,
            string p_cREATEID,
            string p_cREATENAME,
            DateTime? p_cREATETIME)
        {
            _id = p_id;
            _lANGCODE = p_lANGCODE;
            _lANGNAME = p_lANGNAME;
            _pREFIX = p_pREFIX;
            _sORTINDEX = p_sORTINDEX;
            _cREATEID = p_cREATEID;
            _cREATENAME = p_cREATENAME;
            _cREATETIME = p_cREATETIME;
        }

        #endregion

        #region Properties

        [PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string ID
        {
            get { return _id; }
            set { _id = value; } // 处理列表编辑时去掉注释

        }

        [Property("LANGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
        public string LANGCODE
        {
            get { return _lANGCODE; }
            set
            {
                if ((_lANGCODE == null) || (value == null) || (!value.Equals(_lANGCODE)))
                {
                    object oldValue = _lANGCODE;
                    _lANGCODE = value;
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_LANGCODE, oldValue, value);
                }
            }

        }

        [Property("LANGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
        public string LANGNAME
        {
            get { return _lANGNAME; }
            set
            {
                if ((_lANGNAME == null) || (value == null) || (!value.Equals(_lANGNAME)))
                {
                    object oldValue = _lANGNAME;
                    _lANGNAME = value;
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_LANGNAME, oldValue, value);
                }
            }

        }

        [Property("PREFIX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 3000)]
        public string PREFIX
        {
            get { return _pREFIX; }
            set
            {
                if ((_pREFIX == null) || (value == null) || (!value.Equals(_pREFIX)))
                {
                    object oldValue = _pREFIX;
                    _pREFIX = value;
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_PREFIX, oldValue, value);
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
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_SORTINDEX, oldValue, value);
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
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_CREATEID, oldValue, value);
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
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_CREATENAME, oldValue, value);
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
                    RaisePropertyChanged(LANGUAGESTYPE.Prop_CREATETIME, oldValue, value);
                }
            }

        }

        #endregion
    } // LANGUAGESTYPE
}

