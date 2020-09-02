// Business class MDM_BP generated from MDM_BP
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
	[ActiveRecord("MDM_BP")]
	public partial class MDM_BP : EntityBase<MDM_BP>
	{
		#region Property_Names

		public static string Prop_BPKEY = "BPKEY";
		public static string Prop_BPNAME = "BPNAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _bpkey;
		private string _bPNAME;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_BP()
		{
		}

		public MDM_BP(
			string p_bpkey,
			string p_bPNAME,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID,
			string p_sTATUS,
			string p_mEMO)
		{
			_bpkey = p_bpkey;
			_bPNAME = p_bPNAME;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

        //[PrimaryKey("BPKEY", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        [PrimaryKey(PrimaryKeyType.Assigned, "BPKEY", Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string BPKEY
		{
            set { _bpkey = value; }
			get { return _bpkey; }
		}

		[Property("BPNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string BPNAME
		{
			get { return _bPNAME; }
			set
			{
				if ((_bPNAME == null) || (value == null) || (!value.Equals(_bPNAME)))
				{
                    object oldValue = _bPNAME;
					_bPNAME = value;
					RaisePropertyChanged(MDM_BP.Prop_BPNAME, oldValue, value);
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
					RaisePropertyChanged(MDM_BP.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_BP.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_BP.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_BP.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

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
					RaisePropertyChanged(MDM_BP.Prop_RID, oldValue, value);
				}
			}
		}
        //[XmlIgnore]
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
					RaisePropertyChanged(MDM_BP.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_BP.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_BP
}

