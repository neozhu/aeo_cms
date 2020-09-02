// Business class SQM_SRV_ATCOSTCONFIG generated from SQM_SRV_ATCOSTCONFIG
// Creator: rw
// Created Date: [2020-02-27]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_SRV_ATCOSTCONFIG")]
	public partial class SQM_SRV_ATCOSTCONFIG : EntityBase<SQM_SRV_ATCOSTCONFIG>
	{
		#region Property_Names

		public static string Prop_PRODCODE = "PRODCODE";
		public static string Prop_PRODNAME = "PRODNAME";
		public static string Prop_SRVCODE = "SRVCODE";
		public static string Prop_SRVNAME = "SRVNAME";
		public static string Prop_SRVDISP = "SRVDISP";
		public static string Prop_FEECODE = "FEECODE";
		public static string Prop_FEENAME = "FEENAME";
		public static string Prop_FEECATG = "FEECATG";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_BUSINESSORG = "BUSINESSORG";
		public static string Prop_ISALONE = "ISALONE";

		#endregion

		#region Private_Variables

		private string _pRODCODE;
		private string _pRODNAME;
		private string _sRVCODE;
		private string _sRVNAME;
		private string _sRVDISP;
		private string _fEECODE;
		private string _fEENAME;
		private string _fEECATG;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private string _bUSINESSORG;
		private string _iSALONE;


		#endregion

		#region Constructors

		public SQM_SRV_ATCOSTCONFIG()
		{
		}

		public SQM_SRV_ATCOSTCONFIG(
			string p_pRODCODE,
			string p_pRODNAME,
			string p_sRVCODE,
			string p_sRVNAME,
			string p_sRVDISP,
			string p_fEECODE,
			string p_fEENAME,
			string p_fEECATG,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			string p_bUSINESSORG,
			string p_iSALONE)
		{
			_pRODCODE = p_pRODCODE;
			_pRODNAME = p_pRODNAME;
			_sRVCODE = p_sRVCODE;
			_sRVNAME = p_sRVNAME;
			_sRVDISP = p_sRVDISP;
			_fEECODE = p_fEECODE;
			_fEENAME = p_fEENAME;
			_fEECATG = p_fEECATG;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_bUSINESSORG = p_bUSINESSORG;
			_iSALONE = p_iSALONE;
		}

		#endregion

		#region Properties

		[Property("PRODCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODCODE
		{
			get { return _pRODCODE; }
			set
			{
				if ((_pRODCODE == null) || (value == null) || (!value.Equals(_pRODCODE)))
				{
                    object oldValue = _pRODCODE;
					_pRODCODE = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_PRODCODE, oldValue, value);
				}
			}
		}

		[Property("PRODNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string PRODNAME
		{
			get { return _pRODNAME; }
			set
			{
				if ((_pRODNAME == null) || (value == null) || (!value.Equals(_pRODNAME)))
				{
                    object oldValue = _pRODNAME;
					_pRODNAME = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_PRODNAME, oldValue, value);
				}
			}
		}

		[Property("SRVCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string SRVCODE
		{
			get { return _sRVCODE; }
			set
			{
				if ((_sRVCODE == null) || (value == null) || (!value.Equals(_sRVCODE)))
				{
                    object oldValue = _sRVCODE;
					_sRVCODE = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_SRVCODE, oldValue, value);
				}
			}
		}

		[Property("SRVNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SRVNAME
		{
			get { return _sRVNAME; }
			set
			{
				if ((_sRVNAME == null) || (value == null) || (!value.Equals(_sRVNAME)))
				{
                    object oldValue = _sRVNAME;
					_sRVNAME = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_SRVNAME, oldValue, value);
				}
			}
		}

		[Property("SRVDISP", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string SRVDISP
		{
			get { return _sRVDISP; }
			set
			{
				if ((_sRVDISP == null) || (value == null) || (!value.Equals(_sRVDISP)))
				{
                    object oldValue = _sRVDISP;
					_sRVDISP = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_SRVDISP, oldValue, value);
				}
			}
		}

		[Property("FEECODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEECODE
		{
			get { return _fEECODE; }
			set
			{
				if ((_fEECODE == null) || (value == null) || (!value.Equals(_fEECODE)))
				{
                    object oldValue = _fEECODE;
					_fEECODE = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_FEECODE, oldValue, value);
				}
			}
		}

		[Property("FEENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string FEENAME
		{
			get { return _fEENAME; }
			set
			{
				if ((_fEENAME == null) || (value == null) || (!value.Equals(_fEENAME)))
				{
                    object oldValue = _fEENAME;
					_fEENAME = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_FEENAME, oldValue, value);
				}
			}
		}

		[Property("FEECATG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FEECATG
		{
			get { return _fEECATG; }
			set
			{
				if ((_fEECATG == null) || (value == null) || (!value.Equals(_fEECATG)))
				{
                    object oldValue = _fEECATG;
					_fEECATG = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_FEECATG, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("BUSINESSORG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BUSINESSORG
		{
			get { return _bUSINESSORG; }
			set
			{
				if ((_bUSINESSORG == null) || (value == null) || (!value.Equals(_bUSINESSORG)))
				{
                    object oldValue = _bUSINESSORG;
					_bUSINESSORG = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_BUSINESSORG, oldValue, value);
				}
			}
		}

		[Property("ISALONE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ISALONE
		{
			get { return _iSALONE; }
			set
			{
				if ((_iSALONE == null) || (value == null) || (!value.Equals(_iSALONE)))
				{
                    object oldValue = _iSALONE;
					_iSALONE = value;
					RaisePropertyChanged(SQM_SRV_ATCOSTCONFIG.Prop_ISALONE, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_SRV_ATCOSTCONFIG
}

