// Business class MDM_MAIN_BASIC generated from MDM_MAIN_BASIC
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
	[ActiveRecord("MDM_MAIN_BASIC")]
	public partial class MDM_MAIN_BASIC : EntityBase<MDM_MAIN_BASIC>
	{
		#region Property_Names

		public static string Prop_MDKEY = "MDKEY";
		public static string Prop_MDNAME = "MDNAME";
		public static string Prop_MDTYPE = "MDTYPE";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _mdkey;
		private string _mDNAME;
		private string _mDTYPE;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_MAIN_BASIC()
		{
		}

		public MDM_MAIN_BASIC(
			string p_mdkey,
			string p_mDNAME,
			string p_mDTYPE,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID,
			string p_sTATUS,
			string p_mEMO)
		{
			_mdkey = p_mdkey;
			_mDNAME = p_mDNAME;
			_mDTYPE = p_mDTYPE;
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

        //[PrimaryKey("MDKEY", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        [PrimaryKey(PrimaryKeyType.Assigned, "MDKEY", Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string MDKEY
		{
            set { _mdkey = value; }
			get { return _mdkey; }
		}

		[Property("MDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string MDNAME
		{
			get { return _mDNAME; }
			set
			{
				if ((_mDNAME == null) || (value == null) || (!value.Equals(_mDNAME)))
				{
                    object oldValue = _mDNAME;
					_mDNAME = value;
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_MDNAME, oldValue, value);
				}
			}
		}

		[Property("MDTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string MDTYPE
		{
			get { return _mDTYPE; }
			set
			{
				if ((_mDTYPE == null) || (value == null) || (!value.Equals(_mDTYPE)))
				{
                    object oldValue = _mDTYPE;
					_mDTYPE = value;
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_MDTYPE, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_RID, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_MAIN_BASIC.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_MAIN_BASIC
}

