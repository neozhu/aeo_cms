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
    [ActiveRecord("QDM_FEE_SRV_REF")]
    public partial class QDM_FEE_SRV_REF : EntityBase<QDM_FEE_SRV_REF>
    {
        #region Property_Names

        public static string Prop_ProductCode = "ProductCode";
        public static string Prop_FeeCode = "FeeCode";
        public static string Prop_ServiceTypeCode = "ServiceTypeCode";
        public static string Prop_RID = "RID";
        public static string Prop_STATUS = "STATUS";
        public static string Prop_SORID = "SORID";
        public static string Prop_BXBJ = "BXBJ";
        #endregion

        #region Private_Variables

        private string _productCode;
        private string _feeCode;
        private string _serviceTypeCode;
        private string _rid;
        private string _sTATUS;
        private string _sORID;
        private string _bXBJ;
        #endregion

        #region Constructors

        public QDM_FEE_SRV_REF()
		{
		}

        public QDM_FEE_SRV_REF(
            string p_feeCode,
            string p_serviceTypeCode,
            string p_rid,
            string p_sTATUS,
            string p_sORID,
            string p_bXBJ,
            string p_productCode
            )
		{
            _feeCode = p_feeCode;
            _serviceTypeCode = p_serviceTypeCode;
            _rid = p_rid;
            _sTATUS = p_sTATUS;
            _sORID = p_sORID;
            _bXBJ = p_bXBJ;
            _productCode = p_productCode;
		}
		#endregion

		#region Properties
        [Property("ProductCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string ProductCode
        {
            get { return _productCode; }
            set
            {
                if ((_productCode == null) || (value == null) || (!value.Equals(_productCode)))
                {
                    object oldValue = _productCode;
                    _productCode = value;
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_ProductCode, oldValue, value);
                }
            }
        }
        [Property("FeeCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string FeeCode
        {
            get { return _feeCode; }
			set
			{
                if ((_feeCode == null) || (value == null) || (!value.Equals(_feeCode)))
				{
                    object oldValue = _feeCode;
                    _feeCode = value;
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_FeeCode, oldValue, value);
				}
			}
		}

        [Property("ServiceTypeCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string ServiceTypeCode
		{
            get { return _serviceTypeCode; }
			set
			{
                if ((_serviceTypeCode == null) || (value == null) || (!value.Equals(_serviceTypeCode)))
				{
                    object oldValue = _serviceTypeCode;
                    _serviceTypeCode = value;
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_ServiceTypeCode, oldValue, value);
				}
			}
		}
       
        [PrimaryKey(PrimaryKeyType.Assigned, "RID", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            set { _rid = value; }
            get { return _rid; }
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
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_STATUS, oldValue, value);
                }
            }
        }
        [Property("BXBJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string BXBJ
        {
            get { return _bXBJ; }
            set
            {
                if ((_bXBJ == null) || (value == null) || (!value.Equals(_bXBJ)))
                {
                    object oldValue = _bXBJ;
                    _bXBJ = value;
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_BXBJ, oldValue, value);
                }
            }
        }
        [Property("SORID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string SORID
        {
            get { return _sORID; }
            set
            {
                if ((_sORID == null) || (value == null) || (!value.Equals(_sORID)))
                {
                    object oldValue = _sORID;
                    _sORID = value;
                    RaisePropertyChanged(QDM_FEE_SRV_REF.Prop_SORID, oldValue, value);
                }
            }
        }
        #endregion
    }
}
