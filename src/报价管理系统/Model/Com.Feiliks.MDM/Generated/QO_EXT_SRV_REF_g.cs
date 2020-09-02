using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;


namespace Com.Feiliks.MDM
{
    [ActiveRecord("QO_EXT_SRV_REF")]
    public partial class QO_EXT_SRV_REF : EntityBase<QO_EXT_SRV_REF>
    {
        #region Property_Names

        public static string Prop_InfoCode = "InfoCode";
        public static string Prop_ServiceTypeCode = "ServiceTypeCode";
        public static string Prop_RID = "RID";
		#endregion

		#region Private_Variables

        private string _infoCode;
        private string _serviceTypeCode;
        private string _rid;

		#endregion

		#region Constructors

		public QO_EXT_SRV_REF()
		{
		}

        public QO_EXT_SRV_REF(
            string p_infoCode,
            string p_serviceTypeCode,
            string p_rid)
		{
            _infoCode = p_infoCode;
            _serviceTypeCode = p_serviceTypeCode;
            _rid = p_rid;

		}
		#endregion

		#region Properties
        [Property("InfoCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string InfoCode
		{
            get { return _infoCode; }
			set
			{
                if ((_infoCode == null) || (value == null) || (!value.Equals(_infoCode)))
				{
                    object oldValue = _infoCode;
                    _infoCode = value;
                    RaisePropertyChanged(QO_EXT_SRV_REF.Prop_InfoCode, oldValue, value);
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
                    RaisePropertyChanged(QO_EXT_SRV_REF.Prop_ServiceTypeCode, oldValue, value);
				}
			}
		}

        [PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            get { return _rid; }
        }
		#endregion
    }
}
