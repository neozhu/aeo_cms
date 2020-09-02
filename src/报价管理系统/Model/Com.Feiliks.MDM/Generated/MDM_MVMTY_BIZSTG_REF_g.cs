// Business class MDM_MVMTY_BIZSTG_REF generated from MDM_MVMTY_BIZSTG_REF
// Creator: rw
// Created Date: [2017-10-24]

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
	[ActiveRecord("MDM_MVMTY_BIZSTG_REF")]
	public partial class MDM_MVMTY_BIZSTG_REF : EntityBase<MDM_MVMTY_BIZSTG_REF>
	{
		#region Property_Names

		public static string Prop_MVMTYCODE = "MVMTYCODE";
		public static string Prop_BIZSTGCODE = "BIZSTGCODE";
		public static string Prop_RID = "RID";

		#endregion

		#region Private_Variables

		private string _mVMTYCODE;
		private string _bIZSTGCODE;
		private string _rid;


		#endregion

		#region Constructors

		public MDM_MVMTY_BIZSTG_REF()
		{
		}

		public MDM_MVMTY_BIZSTG_REF(
			string p_mVMTYCODE,
			string p_bIZSTGCODE,
			string p_rid)
		{
			_mVMTYCODE = p_mVMTYCODE;
			_bIZSTGCODE = p_bIZSTGCODE;
			_rid = p_rid;
		}

		#endregion

		#region Properties

		[Property("MVMTYCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MVMTYCODE
		{
			get { return _mVMTYCODE; }
			set
			{
				if ((_mVMTYCODE == null) || (value == null) || (!value.Equals(_mVMTYCODE)))
				{
                    object oldValue = _mVMTYCODE;
					_mVMTYCODE = value;
					RaisePropertyChanged(MDM_MVMTY_BIZSTG_REF.Prop_MVMTYCODE, oldValue, value);
				}
			}
		}

		[Property("BIZSTGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string BIZSTGCODE
		{
			get { return _bIZSTGCODE; }
			set
			{
				if ((_bIZSTGCODE == null) || (value == null) || (!value.Equals(_bIZSTGCODE)))
				{
                    object oldValue = _bIZSTGCODE;
					_bIZSTGCODE = value;
					RaisePropertyChanged(MDM_MVMTY_BIZSTG_REF.Prop_BIZSTGCODE, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		#endregion
	} // MDM_MVMTY_BIZSTG_REF
}

