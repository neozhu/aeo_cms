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
    [ActiveRecord("MDM_PRD_SRV_REF")]
    public partial class MDM_PRD_SRV_REF : EntityBase<MDM_PRD_SRV_REF>
    {
        #region Property_Names

        public static string Prop_ProductCode = "ProductCode";
        public static string Prop_ServiceTypeCode = "ServiceTypeCode";
        public static string Prop_RID = "RID";
        public static string Prop_STATUS = "STATUS";
		#endregion

		#region Private_Variables

        private string _productCode;
        private string _serviceTypeCode;
        private string _rid;
        private string _sTATUS;
		#endregion

		#region Constructors

		public MDM_PRD_SRV_REF()
		{
		}

        public MDM_PRD_SRV_REF(
            string p_productCode,
            string p_serviceTypeCode,
            string p_rid,
            string p_sTATUS)
		{
            _productCode = p_productCode;
            _serviceTypeCode = p_serviceTypeCode;
            _rid = p_rid;
            _sTATUS = p_sTATUS;
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
                    RaisePropertyChanged(MDM_PRD_SRV_REF.Prop_ProductCode, oldValue, value);
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
                    RaisePropertyChanged(MDM_PRD_SRV_REF.Prop_ServiceTypeCode, oldValue, value);
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
                    RaisePropertyChanged(MDM_PRD_SRV_REF.Prop_STATUS, oldValue, value);
                }
            }
        }
		#endregion
    }
}
