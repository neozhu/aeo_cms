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
    [ActiveRecord("QDM_PRD_SRV_REF")]
    public partial class QDM_PRD_SRV_REF : EntityBase<QDM_PRD_SRV_REF>
    {
        #region Property_Names

        public static string Prop_ProductCode = "ProductCode";
        public static string Prop_ServiceTypeCode = "ServiceTypeCode";
        public static string Prop_RID = "RID";
        public static string Prop_STATUS = "STATUS";
        public static string Prop_SORID = "SORID";
        #endregion

        #region Private_Variables

        private string _productCode;
        private string _serviceTypeCode;
        private string _rid;
        private string _sTATUS;
        private string _sORID;

        #endregion

        #region Constructors

        public QDM_PRD_SRV_REF()
		{
		}

        public QDM_PRD_SRV_REF(
            string p_productCode,
            string p_serviceTypeCode,
            string p_rid,
            string p_sTATUS,
            string p_sORID)
		{
            _productCode = p_productCode;
            _serviceTypeCode = p_serviceTypeCode;
            _rid = p_rid;
            _sTATUS = p_sTATUS;
            _sORID = p_sORID;
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
                    RaisePropertyChanged(QDM_PRD_SRV_REF.Prop_ProductCode, oldValue, value);
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
                    RaisePropertyChanged(QDM_PRD_SRV_REF.Prop_ServiceTypeCode, oldValue, value);
				}
			}
		}
        [XmlIgnore]
        [PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
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
                    RaisePropertyChanged(QDM_PRD_SRV_REF.Prop_STATUS, oldValue, value);
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
                    RaisePropertyChanged(QDM_PRD_SRV_REF.Prop_SORID, oldValue, value);
                }
            }
        }
        #endregion
    }
}
