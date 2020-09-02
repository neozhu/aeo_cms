// Business class QDM_FEE generated from QDM_FEE
// Creator: rw
// Created Date: [2018-03-15]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
using System.Xml.Serialization;

namespace Com.Feiliks.QDM
{
    [ActiveRecord("QDM_FEE")]
    public partial class QDM_FEE : EntityBase<QDM_FEE>
    {
        #region Property_Names

        public static string Prop_ID = "ID";
        public static string Prop_CODE = "CODE";
        public static string Prop_NAME = "NAME";
        public static string Prop_ENAME = "ENAME";
        public static string Prop_PRICE = "PRICE";
        public static string Prop_UNIT = "UNIT";
        public static string Prop_STATUS = "STATUS";
        public static string Prop_DTFROM = "DTFROM";
        public static string Prop_DTTO = "DTTO";
        public static string Prop_CREATETIME = "CREATETIME";
        public static string Prop_CREATEUSER = "CREATEUSER";
        public static string Prop_CREATEUSERNAME = "CREATEUSERNAME";
        public static string Prop_MODIFYTIME = "MODIFYTIME";
        public static string Prop_MODIFYUSER = "MODIFYUSER";
        public static string Prop_MODIFYUSERNAME = "MODIFYUSERNAME";
        public static string Prop_MEMO = "MEMO";
        public static string Prop_SORID = "SORID";
        #endregion

        #region Private_Variables

        private string _id;
        private string _cODE;
        private string _nAME;
        private string _eNAME;
        private string _pRICE;
        private string _uNIT;
        private string _sTATUS;
        private DateTime? _dTFROM;
        private DateTime? _dTTO;
        private DateTime? _cREATETIME;
        private string _cREATEUSER;
        private string _cREATEUSERNAME;
        private DateTime? _mODIFYTIME;
        private string _mODIFYUSER;
        private string _mODIFYUSERNAME;
        private string _mEMO;
        private string _sORID;


        #endregion

        #region Constructors

        public QDM_FEE()
        {

        }

        public QDM_FEE(
        string p_id,
        string p_cODE,
        string p_nAME,
        string p_eNAME,
        string p_pRICE,
        string p_uNIT,
        string p_sTATUS,
        DateTime? p_dTFROM,
        DateTime? p_dTTO,
        DateTime? p_cREATETIME,
        string  p_cREATEUSER,
        string p_cREATEUSERNAME,
        DateTime? p_mODIFYTIME,
        string p_mODIFYUSER,
        string p_mODIFYUSERNAME,
        string p_mEMO,
        string p_sORID
    )
		{
            _id = p_id;
            _cODE = p_cODE;
            _nAME = p_nAME;
            _eNAME = p_eNAME;
            _pRICE = p_pRICE;
            _uNIT = p_uNIT;
            _sTATUS = p_sTATUS;
            _dTFROM = p_dTFROM;
            _dTTO = p_dTTO;
            _cREATETIME = p_cREATETIME;
            _cREATEUSER = p_cREATEUSER;
            _cREATEUSERNAME = p_cREATEUSERNAME;
            _mODIFYTIME = p_mODIFYTIME;
            _mODIFYUSER = p_mODIFYUSER;
            _mODIFYUSERNAME = p_mODIFYUSERNAME;
            _mEMO = p_mEMO;
            _sORID = p_sORID;

		}

        #endregion

        #region Properties

        [PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string ID
        {
            get { return _id; }
        }

        [Property("CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CODE
        {
			get { return _cODE; }
			set
			{
				if ((_cODE == null) || (value == null) || (!value.Equals(_cODE)))
				{
                    object oldValue = _cODE;
                    _cODE = value;
					RaisePropertyChanged(QDM_FEE.Prop_CODE, oldValue, value);
				}
			}
		}

        [Property("NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string NAME
        {
			get { return _nAME; }
			set
			{
				if ((_nAME == null) || (value == null) || (!value.Equals(_nAME)))
				{
                    object oldValue = _nAME;
                    _nAME = value;
                    RaisePropertyChanged(QDM_FEE.Prop_NAME, oldValue, value);
				}
			}
		}
		[Property("ENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ENAME
        {
			get { return _eNAME; }
			set
			{
				if (value != _eNAME)
				{
                    object oldValue = _eNAME;
                    _eNAME = value;
					RaisePropertyChanged(QDM_FEE.Prop_ENAME, oldValue, value);
				}
			}
		}
       
		[Property("PRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRICE
        {
			get { return _pRICE; }
			set
			{
				if ((_pRICE == null) || (value == null) || (!value.Equals(_pRICE)))
				{
                    object oldValue = _pRICE;
                    _pRICE = value;
					RaisePropertyChanged(QDM_FEE.Prop_PRICE, oldValue, value);
				}
			}
		}

		[Property("UNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string UNIT 
		{
			get { return _uNIT; }
			set
			{
				if (value != _uNIT)
				{
                    object oldValue = _uNIT;
                    _uNIT = value;
					RaisePropertyChanged(QDM_FEE.Prop_UNIT, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_STATUS, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_DTFROM, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_DTTO, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_CREATETIME, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_CREATEUSER, oldValue, value);
                }
            }
        }

        [Property("CREATEUSERNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string CREATEUSERNAME
        {
            get { return _cREATEUSERNAME; }
            set
            {
                if ((_cREATEUSERNAME == null) || (value == null) || (!value.Equals(_cREATEUSERNAME)))
                {
                    object oldValue = _cREATEUSERNAME;
                    _cREATEUSERNAME = value;
                    RaisePropertyChanged(QDM_FEE.Prop_CREATEUSERNAME, oldValue, value);
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
                    RaisePropertyChanged(QDM_FEE.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(QDM_FEE.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

        [Property("MODIFYUSERNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string MODIFYUSERNAME
        {
            get { return _mODIFYUSERNAME; }
            set
            {
                if ((_mODIFYUSERNAME == null) || (value == null) || (!value.Equals(_mODIFYUSERNAME)))
                {
                    object oldValue = _mODIFYUSERNAME;
                    _mODIFYUSERNAME = value;
                    RaisePropertyChanged(QDM_FEE.Prop_MODIFYUSERNAME, oldValue, value);
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
					RaisePropertyChanged(QDM_FEE.Prop_MEMO, oldValue, value);
				}
			}
		}
        public string SORID
        {
            get { return _sORID; }
            set
            {
                if ((_sORID == null) || (value == null) || (!value.Equals(_sORID)))
                {
                    object oldValue = _sORID;
                    _sORID = value;
                    RaisePropertyChanged(QDM_FEE.Prop_SORID, oldValue, value);
                }
            }
        }

        #endregion
    } // QDM_FEE
}

