// Business class SYSMONTBLCLNSSET generated from SYSMONTBLCLNSSET
// Creator: Ray
// Created Date: [2014-05-05]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
    [ActiveRecord("SYSMONTBLCLNSSET")]
    public partial class SYSMONTBLCLNSSET : EntityBase<SYSMONTBLCLNSSET>
    {
        #region Property_Names

        public static string Prop_ID = "ID";
        public static string Prop_CLNCODE = "CLNCODE";
        public static string Prop_CLNNAME = "CLNNAME";
        public static string Prop_CLNDATATYPE = "CLNDATATYPE";
        public static string Prop_ISCHECKED = "ISCHECKED";

        #endregion

        #region Private_Variables

        private string _id;
        private string _cLNCODE;
        private string _cLNNAME;
        private string _cLNDATATYPE;
        private string _iSCHECKED;


        #endregion

        #region Constructors

        public SYSMONTBLCLNSSET()
        {
        }

        public SYSMONTBLCLNSSET(
            string p_id,
            string p_cLNCODE,
            string p_cLNNAME,
            string p_cLNDATATYPE,
            string p_iSCHECKED)
        {
            _id = p_id;
            _cLNCODE = p_cLNCODE;
            _cLNNAME = p_cLNNAME;
            _cLNDATATYPE = p_cLNDATATYPE;
            _iSCHECKED = p_iSCHECKED;
        }

        #endregion

        #region Properties

        [PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string ID
        {
            get { return _id; }
            set { _id = value; } // 处理列表编辑时去掉注释

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
                    RaisePropertyChanged(SYSMONTBLCLNSSET.Prop_CLNCODE, oldValue, value);
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
                    RaisePropertyChanged(SYSMONTBLCLNSSET.Prop_CLNNAME, oldValue, value);
                }
            }

        }

        [Property("CLNDATATYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string CLNDATATYPE
        {
            get { return _cLNDATATYPE; }
            set
            {
                if ((_cLNDATATYPE == null) || (value == null) || (!value.Equals(_cLNDATATYPE)))
                {
                    object oldValue = _cLNDATATYPE;
                    _cLNDATATYPE = value;
                    RaisePropertyChanged(SYSMONTBLCLNSSET.Prop_CLNDATATYPE, oldValue, value);
                }
            }

        }

        [Property("ISCHECKED", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
        public string ISCHECKED
        {
            get { return _iSCHECKED; }
            set
            {
                if ((_iSCHECKED == null) || (value == null) || (!value.Equals(_iSCHECKED)))
                {
                    object oldValue = _iSCHECKED;
                    _iSCHECKED = value;
                    RaisePropertyChanged(SYSMONTBLCLNSSET.Prop_ISCHECKED, oldValue, value);
                }
            }

        }

        #endregion
    } // SYSMONTBLCLNSSET
}

