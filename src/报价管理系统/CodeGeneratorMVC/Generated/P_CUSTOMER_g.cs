// Business class P_CUSTOMER generated from P_CUSTOMER
// Creator: rw
// Created Date: [2016-11-01]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("P_CUSTOMER")]
	public partial class P_CUSTOMER : EntityBase<P_CUSTOMER>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_STORERKEY = "STORERKEY";
		public static string Prop_COMPANY = "COMPANY";
		public static string Prop_ADDRESS1 = "ADDRESS1";
		public static string Prop_ZIP = "ZIP";
		public static string Prop_CITYCODE = "CITYCODE";
		public static string Prop_CITY = "CITY";
		public static string Prop_COUNTRYCODE = "COUNTRYCODE";
		public static string Prop_COUNTRY = "COUNTRY";
		public static string Prop_STATECODE = "STATECODE";
		public static string Prop_STATE = "STATE";
		public static string Prop_PHONE1 = "PHONE1";
		public static string Prop_FAX1 = "FAX1";
		public static string Prop_EMAIL1 = "EMAIL1";
		public static string Prop_NOTES1 = "NOTES1";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_ADDRESS2 = "ADDRESS2";
		public static string Prop_SUSR3 = "SUSR3";
		public static string Prop_TYPE = "TYPE";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_EXT4 = "EXT4";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_LASTMODIFIEDDATE = "LASTMODIFIEDDATE";

		#endregion

		#region Private_Variables

		private string _id;
		private string _sTORERKEY;
		private string _cOMPANY;
		private string _aDDRESS1;
		private string _zIP;
		private string _cITYCODE;
		private string _cITY;
		private string _cOUNTRYCODE;
		private string _cOUNTRY;
		private string _sTATECODE;
		private string _sTATE;
		private string _pHONE1;
		private string _fAX1;
		private string _eMAIL1;
		private string _nOTES1;
		private string _dESCRIPTION;
		private string _aDDRESS2;
		private string _sUSR3;
		private string _tYPE;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _eXT4;
		private string _cREATEID;
		private string _cREATENAME;
		private DateTime? _cREATETIME;
		private DateTime? _lASTMODIFIEDDATE;


		#endregion

		#region Constructors

		public P_CUSTOMER()
		{
		}

		public P_CUSTOMER(
			string p_id,
			string p_sTORERKEY,
			string p_cOMPANY,
			string p_aDDRESS1,
			string p_zIP,
			string p_cITYCODE,
			string p_cITY,
			string p_cOUNTRYCODE,
			string p_cOUNTRY,
			string p_sTATECODE,
			string p_sTATE,
			string p_pHONE1,
			string p_fAX1,
			string p_eMAIL1,
			string p_nOTES1,
			string p_dESCRIPTION,
			string p_aDDRESS2,
			string p_sUSR3,
			string p_tYPE,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_eXT4,
			string p_cREATEID,
			string p_cREATENAME,
			DateTime? p_cREATETIME,
			DateTime? p_lASTMODIFIEDDATE)
		{
			_id = p_id;
			_sTORERKEY = p_sTORERKEY;
			_cOMPANY = p_cOMPANY;
			_aDDRESS1 = p_aDDRESS1;
			_zIP = p_zIP;
			_cITYCODE = p_cITYCODE;
			_cITY = p_cITY;
			_cOUNTRYCODE = p_cOUNTRYCODE;
			_cOUNTRY = p_cOUNTRY;
			_sTATECODE = p_sTATECODE;
			_sTATE = p_sTATE;
			_pHONE1 = p_pHONE1;
			_fAX1 = p_fAX1;
			_eMAIL1 = p_eMAIL1;
			_nOTES1 = p_nOTES1;
			_dESCRIPTION = p_dESCRIPTION;
			_aDDRESS2 = p_aDDRESS2;
			_sUSR3 = p_sUSR3;
			_tYPE = p_tYPE;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_eXT4 = p_eXT4;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_cREATETIME = p_cREATETIME;
			_lASTMODIFIEDDATE = p_lASTMODIFIEDDATE;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
		}

		[Property("STORERKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string STORERKEY
		{
			get { return _sTORERKEY; }
			set
			{
				if ((_sTORERKEY == null) || (value == null) || (!value.Equals(_sTORERKEY)))
				{
                    object oldValue = _sTORERKEY;
					_sTORERKEY = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_STORERKEY, oldValue, value);
				}
			}
		}

		[Property("COMPANY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string COMPANY
		{
			get { return _cOMPANY; }
			set
			{
				if ((_cOMPANY == null) || (value == null) || (!value.Equals(_cOMPANY)))
				{
                    object oldValue = _cOMPANY;
					_cOMPANY = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_COMPANY, oldValue, value);
				}
			}
		}

		[Property("ADDRESS1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string ADDRESS1
		{
			get { return _aDDRESS1; }
			set
			{
				if ((_aDDRESS1 == null) || (value == null) || (!value.Equals(_aDDRESS1)))
				{
                    object oldValue = _aDDRESS1;
					_aDDRESS1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_ADDRESS1, oldValue, value);
				}
			}
		}

		[Property("ZIP", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 18)]
		public string ZIP
		{
			get { return _zIP; }
			set
			{
				if ((_zIP == null) || (value == null) || (!value.Equals(_zIP)))
				{
                    object oldValue = _zIP;
					_zIP = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_ZIP, oldValue, value);
				}
			}
		}

		[Property("CITYCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CITYCODE
		{
			get { return _cITYCODE; }
			set
			{
				if ((_cITYCODE == null) || (value == null) || (!value.Equals(_cITYCODE)))
				{
                    object oldValue = _cITYCODE;
					_cITYCODE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_CITYCODE, oldValue, value);
				}
			}
		}

		[Property("CITY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string CITY
		{
			get { return _cITY; }
			set
			{
				if ((_cITY == null) || (value == null) || (!value.Equals(_cITY)))
				{
                    object oldValue = _cITY;
					_cITY = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_CITY, oldValue, value);
				}
			}
		}

		[Property("COUNTRYCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string COUNTRYCODE
		{
			get { return _cOUNTRYCODE; }
			set
			{
				if ((_cOUNTRYCODE == null) || (value == null) || (!value.Equals(_cOUNTRYCODE)))
				{
                    object oldValue = _cOUNTRYCODE;
					_cOUNTRYCODE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_COUNTRYCODE, oldValue, value);
				}
			}
		}

		[Property("COUNTRY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string COUNTRY
		{
			get { return _cOUNTRY; }
			set
			{
				if ((_cOUNTRY == null) || (value == null) || (!value.Equals(_cOUNTRY)))
				{
                    object oldValue = _cOUNTRY;
					_cOUNTRY = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_COUNTRY, oldValue, value);
				}
			}
		}

		[Property("STATECODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string STATECODE
		{
			get { return _sTATECODE; }
			set
			{
				if ((_sTATECODE == null) || (value == null) || (!value.Equals(_sTATECODE)))
				{
                    object oldValue = _sTATECODE;
					_sTATECODE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_STATECODE, oldValue, value);
				}
			}
		}

		[Property("STATE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string STATE
		{
			get { return _sTATE; }
			set
			{
				if ((_sTATE == null) || (value == null) || (!value.Equals(_sTATE)))
				{
                    object oldValue = _sTATE;
					_sTATE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_STATE, oldValue, value);
				}
			}
		}

		[Property("PHONE1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string PHONE1
		{
			get { return _pHONE1; }
			set
			{
				if ((_pHONE1 == null) || (value == null) || (!value.Equals(_pHONE1)))
				{
                    object oldValue = _pHONE1;
					_pHONE1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_PHONE1, oldValue, value);
				}
			}
		}

		[Property("FAX1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string FAX1
		{
			get { return _fAX1; }
			set
			{
				if ((_fAX1 == null) || (value == null) || (!value.Equals(_fAX1)))
				{
                    object oldValue = _fAX1;
					_fAX1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_FAX1, oldValue, value);
				}
			}
		}

		[Property("EMAIL1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string EMAIL1
		{
			get { return _eMAIL1; }
			set
			{
				if ((_eMAIL1 == null) || (value == null) || (!value.Equals(_eMAIL1)))
				{
                    object oldValue = _eMAIL1;
					_eMAIL1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_EMAIL1, oldValue, value);
				}
			}
		}

		[Property("NOTES1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string NOTES1
		{
			get { return _nOTES1; }
			set
			{
				if ((_nOTES1 == null) || (value == null) || (!value.Equals(_nOTES1)))
				{
                    object oldValue = _nOTES1;
					_nOTES1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_NOTES1, oldValue, value);
				}
			}
		}

		[Property("DESCRIPTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string DESCRIPTION
		{
			get { return _dESCRIPTION; }
			set
			{
				if ((_dESCRIPTION == null) || (value == null) || (!value.Equals(_dESCRIPTION)))
				{
                    object oldValue = _dESCRIPTION;
					_dESCRIPTION = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_DESCRIPTION, oldValue, value);
				}
			}
		}

		[Property("ADDRESS2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string ADDRESS2
		{
			get { return _aDDRESS2; }
			set
			{
				if ((_aDDRESS2 == null) || (value == null) || (!value.Equals(_aDDRESS2)))
				{
                    object oldValue = _aDDRESS2;
					_aDDRESS2 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_ADDRESS2, oldValue, value);
				}
			}
		}

		[Property("SUSR3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SUSR3
		{
			get { return _sUSR3; }
			set
			{
				if ((_sUSR3 == null) || (value == null) || (!value.Equals(_sUSR3)))
				{
                    object oldValue = _sUSR3;
					_sUSR3 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_SUSR3, oldValue, value);
				}
			}
		}

		[Property("TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string TYPE
		{
			get { return _tYPE; }
			set
			{
				if ((_tYPE == null) || (value == null) || (!value.Equals(_tYPE)))
				{
                    object oldValue = _tYPE;
					_tYPE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_TYPE, oldValue, value);
				}
			}
		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_EXT1, oldValue, value);
				}
			}
		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_EXT2, oldValue, value);
				}
			}
		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_EXT3, oldValue, value);
				}
			}
		}

		[Property("EXT4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string EXT4
		{
			get { return _eXT4; }
			set
			{
				if ((_eXT4 == null) || (value == null) || (!value.Equals(_eXT4)))
				{
                    object oldValue = _eXT4;
					_eXT4 = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_EXT4, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_CREATEID, oldValue, value);
				}
			}
		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_CREATENAME, oldValue, value);
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
					RaisePropertyChanged(P_CUSTOMER.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("LASTMODIFIEDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? LASTMODIFIEDDATE
		{
			get { return _lASTMODIFIEDDATE; }
			set
			{
				if (value != _lASTMODIFIEDDATE)
				{
                    object oldValue = _lASTMODIFIEDDATE;
					_lASTMODIFIEDDATE = value;
					RaisePropertyChanged(P_CUSTOMER.Prop_LASTMODIFIEDDATE, oldValue, value);
				}
			}
		}

		#endregion
	} // P_CUSTOMER
}

