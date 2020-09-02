// Business class MDM_PRODUCT generated from MDM_PRODUCT
// Creator: rw
// Created Date: [2017-09-28]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
using System.Xml.Serialization;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_PRODUCT")]
	public partial class MDM_PRODUCT : EntityBase<MDM_PRODUCT>
	{
		#region Property_Names

		public static string Prop_PRODUCTKEY = "PRODUCTKEY";
		public static string Prop_PRODUCTNAME = "PRODUCTNAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

        public static string Prop_ZFFWZH = "ZFFWZH";

		#endregion

		#region Private_Variables

		private string _productkey;
		private string _pRODUCTNAME;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;
		private string _sTATUS;
		private string _mEMO;

        private string _zFFWZH;

		#endregion

		#region Constructors

		public MDM_PRODUCT()
		{
		}

		public MDM_PRODUCT(
			string p_productkey,
			string p_pRODUCTNAME,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID,
			string p_sTATUS,
			string p_mEMO,
            string p_zFFWZH)
		{
			_productkey = p_productkey;
			_pRODUCTNAME = p_pRODUCTNAME;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
            _zFFWZH = p_zFFWZH;
		}

		#endregion

		#region Properties

        //[PrimaryKey("PRODUCTKEY", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        [PrimaryKey(PrimaryKeyType.Assigned, "PRODUCTKEY", Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string PRODUCTKEY
		{
            set { _productkey = value; }
			get { return _productkey; }
		}

		[Property("PRODUCTNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODUCTNAME
		{
			get { return _pRODUCTNAME; }
			set
			{
				if ((_pRODUCTNAME == null) || (value == null) || (!value.Equals(_pRODUCTNAME)))
				{
                    object oldValue = _pRODUCTNAME;
					_pRODUCTNAME = value;
					RaisePropertyChanged(MDM_PRODUCT.Prop_PRODUCTNAME, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_CREATETIME, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_CREATEUSER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[Property("RID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string RID
		{
			get { return _rID; }
			set
			{
				if ((_rID == null) || (value == null) || (!value.Equals(_rID)))
				{
                    object oldValue = _rID;
					_rID = value;
					RaisePropertyChanged(MDM_PRODUCT.Prop_RID, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_STATUS, oldValue, value);
				}
			}
		}
        [XmlIgnore]
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
					RaisePropertyChanged(MDM_PRODUCT.Prop_MEMO, oldValue, value);
				}
			}
		}

        [Property("ZFFWZH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
        public string ZFFWZH
        {
            get { return _zFFWZH; }
            set
            {
                if ((_zFFWZH == null) || (value == null) || (!value.Equals(_zFFWZH)))
                {
                    object oldValue = _zFFWZH;
                    _zFFWZH = value;
                    RaisePropertyChanged(MDM_PRODUCT.Prop_ZFFWZH, oldValue, value);
                }
            }
        }
		#endregion
	} // MDM_PRODUCT
}

