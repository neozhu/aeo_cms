// Business class QDM_PRODUCT generated from QDM_PRODUCT
// Creator: rw
// Created Date: [2018-02-26]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM
{
	[ActiveRecord("QDM_PRODUCT")]
	public partial class QDM_PRODUCT : EntityBase<QDM_PRODUCT>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_CODE = "CODE";
		public static string Prop_NAME = "NAME";
		public static string Prop_SYB = "SYB";
		public static string Prop_YWLX = "YWLX";
		public static string Prop_CPJL = "CPJL";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_DTFROM = "DTFROM";
		public static string Prop_DTTO = "DTTO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
        public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _id;
		private string _cODE;
		private string _nAME;
		private string _sYB;
		private string _yWLX;
		private string _cPJL;
		private string _sTATUS;
		private DateTime? _dTFROM;
		private DateTime? _dTTO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
        private string _mEMO;

		#endregion

		#region Constructors

		public QDM_PRODUCT()
		{
		}

		public QDM_PRODUCT(
			string p_id,
			string p_cODE,
			string p_nAME,
			string p_sYB,
			string p_yWLX,
			string p_cPJL,
			string p_sTATUS,
			DateTime? p_dTFROM,
			DateTime? p_dTTO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
            string p_mODIFYUSER,
            string p_mEMO)
		{
			_id = p_id;
			_cODE = p_cODE;
			_nAME = p_nAME;
			_sYB = p_sYB;
			_yWLX = p_yWLX;
			_cPJL = p_cPJL;
			_sTATUS = p_sTATUS;
			_dTFROM = p_dTFROM;
			_dTTO = p_dTTO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
            _mEMO = p_mEMO;
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_CODE, oldValue, value);
				}
			}
		}

		[Property("NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string NAME
		{
			get { return _nAME; }
			set
			{
				if ((_nAME == null) || (value == null) || (!value.Equals(_nAME)))
				{
                    object oldValue = _nAME;
					_nAME = value;
					RaisePropertyChanged(QDM_PRODUCT.Prop_NAME, oldValue, value);
				}
			}
		}

		[Property("SYB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SYB
		{
			get { return _sYB; }
			set
			{
				if ((_sYB == null) || (value == null) || (!value.Equals(_sYB)))
				{
                    object oldValue = _sYB;
					_sYB = value;
					RaisePropertyChanged(QDM_PRODUCT.Prop_SYB, oldValue, value);
				}
			}
		}

		[Property("YWLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string YWLX
		{
			get { return _yWLX; }
			set
			{
				if ((_yWLX == null) || (value == null) || (!value.Equals(_yWLX)))
				{
                    object oldValue = _yWLX;
					_yWLX = value;
					RaisePropertyChanged(QDM_PRODUCT.Prop_YWLX, oldValue, value);
				}
			}
		}

		[Property("CPJL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CPJL
		{
			get { return _cPJL; }
			set
			{
				if ((_cPJL == null) || (value == null) || (!value.Equals(_cPJL)))
				{
                    object oldValue = _cPJL;
					_cPJL = value;
					RaisePropertyChanged(QDM_PRODUCT.Prop_CPJL, oldValue, value);
				}
			}
		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(QDM_PRODUCT.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_DTFROM, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_DTTO, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(QDM_PRODUCT.Prop_MODIFYUSER, oldValue, value);
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
                    RaisePropertyChanged(QDM_PRODUCT.Prop_MEMO, oldValue, value);
                }
            }
        }
		#endregion
	} // QDM_PRODUCT
}

