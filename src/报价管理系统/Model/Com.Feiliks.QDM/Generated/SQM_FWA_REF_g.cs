// Business class SQM_FWA_REF generated from SQM_FWA_REF
// Creator: rw
// Created Date: [2018-08-16]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FWA_REF")]
	public partial class SQM_FWA_REF : EntityBase<SQM_FWA_REF>
	{
		#region Property_Names

		public static string Prop_MRID = "MRID";
		public static string Prop_ZVER = "ZVER";
		public static string Prop_FWA = "FWA";
		public static string Prop_PRODUCTS = "PRODUCTS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
        public static string Prop_ITEMNO = "ITEMNO";
        #endregion

        #region Private_Variables

        private string _mRID;
		private string _zVER;
		private string _fWA;
		private string _pRODUCTS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
        private string _iTEMNO;

        #endregion

        #region Constructors

        public SQM_FWA_REF()
		{
		}

		public SQM_FWA_REF(
			string p_mRID,
			string p_zVER,
			string p_fWA,
			string p_pRODUCTS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
            string p_iTEMNO)
		{
			_mRID = p_mRID;
			_zVER = p_zVER;
			_fWA = p_fWA;
			_pRODUCTS = p_pRODUCTS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
            _iTEMNO = p_iTEMNO;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_FWA_REF.Prop_MRID, oldValue, value);
				}
			}
		}

		[Property("ZVER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ZVER
		{
			get { return _zVER; }
			set
			{
				if ((_zVER == null) || (value == null) || (!value.Equals(_zVER)))
				{
                    object oldValue = _zVER;
					_zVER = value;
					RaisePropertyChanged(SQM_FWA_REF.Prop_ZVER, oldValue, value);
				}
			}
		}

		[Property("FWA", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FWA
		{
			get { return _fWA; }
			set
			{
				if ((_fWA == null) || (value == null) || (!value.Equals(_fWA)))
				{
                    object oldValue = _fWA;
					_fWA = value;
					RaisePropertyChanged(SQM_FWA_REF.Prop_FWA, oldValue, value);
				}
			}
		}

		[Property("PRODUCTS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string PRODUCTS
		{
			get { return _pRODUCTS; }
			set
			{
				if ((_pRODUCTS == null) || (value == null) || (!value.Equals(_pRODUCTS)))
				{
                    object oldValue = _pRODUCTS;
					_pRODUCTS = value;
					RaisePropertyChanged(SQM_FWA_REF.Prop_PRODUCTS, oldValue, value);
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_FWA_REF.Prop_MEMO, oldValue, value);
				}
			}
		}
        [Property("ITEMNO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
        public string ITEMNO
        {
            get { return _iTEMNO; }
            set
            {
                if ((_iTEMNO == null) || (value == null) || (!value.Equals(_iTEMNO)))
                {
                    object oldValue = _iTEMNO;
                    _iTEMNO = value;
                    RaisePropertyChanged(SQM_FWA_REF.Prop_ITEMNO, oldValue, value);
                }
            }
        }
        #endregion
    } // SQM_FWA_REF
}

