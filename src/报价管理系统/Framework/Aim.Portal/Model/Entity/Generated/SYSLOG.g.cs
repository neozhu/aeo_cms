// Business class SYSLOG generated from SYSLOG
// Creator: Ray
// Created Date: [2014-04-28]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SYSLOG")]
	public partial class SYSLOG : EntityBase<SYSLOG>
	{
		#region Property_Names

		public static string Prop_ACTION = "ACTION";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_ID = "ID";
		public static string Prop_TABLEEN = "TABLEEN";
		public static string Prop_TABLECN = "TABLECN";
		public static string Prop_COLUMNEN = "COLUMNEN";
		public static string Prop_COLUMNCN = "COLUMNCN";
		public static string Prop_OLDVALUE = "OLDVALUE";
		public static string Prop_NEWVALUE = "NEWVALUE";
		public static string Prop_CONTENT = "CONTENT";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_DEPTID = "DEPTID";
		public static string Prop_DEPTNAME = "DEPTNAME";
		public static string Prop_COMPANYID = "COMPANYID";
		public static string Prop_COMPANYNAME = "COMPANYNAME";

		#endregion

		#region Private_Variables

		private string _aCTION;
		private string _rEMARK;
		private string _id;
		private string _tABLEEN;
		private string _tABLECN;
		private string _cOLUMNEN;
		private string _cOLUMNCN;
		private string _oLDVALUE;
		private string _nEWVALUE;
		private string _cONTENT;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATENAME;
		private string _dEPTID;
		private string _dEPTNAME;
		private string _cOMPANYID;
		private string _cOMPANYNAME;


		#endregion

		#region Constructors

		public SYSLOG()
		{
		}

		public SYSLOG(
			string p_aCTION,
			string p_rEMARK,
			string p_id,
			string p_tABLEEN,
			string p_tABLECN,
			string p_cOLUMNEN,
			string p_cOLUMNCN,
			string p_oLDVALUE,
			string p_nEWVALUE,
			string p_cONTENT,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATENAME,
			string p_dEPTID,
			string p_dEPTNAME,
			string p_cOMPANYID,
			string p_cOMPANYNAME)
		{
			_aCTION = p_aCTION;
			_rEMARK = p_rEMARK;
			_id = p_id;
			_tABLEEN = p_tABLEEN;
			_tABLECN = p_tABLECN;
			_cOLUMNEN = p_cOLUMNEN;
			_cOLUMNCN = p_cOLUMNCN;
			_oLDVALUE = p_oLDVALUE;
			_nEWVALUE = p_nEWVALUE;
			_cONTENT = p_cONTENT;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_dEPTID = p_dEPTID;
			_dEPTNAME = p_dEPTNAME;
			_cOMPANYID = p_cOMPANYID;
			_cOMPANYNAME = p_cOMPANYNAME;
		}

		#endregion

		#region Properties

		[Property("ACTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ACTION
		{
			get { return _aCTION; }
			set
			{
				if ((_aCTION == null) || (value == null) || (!value.Equals(_aCTION)))
				{
                    object oldValue = _aCTION;
					_aCTION = value;
					RaisePropertyChanged(SYSLOG.Prop_ACTION, oldValue, value);
				}
			}

		}

		[Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(SYSLOG.Prop_REMARK, oldValue, value);
				}
			}

		}

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("TABLEEN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 80)]
		public string TABLEEN
		{
			get { return _tABLEEN; }
			set
			{
				if ((_tABLEEN == null) || (value == null) || (!value.Equals(_tABLEEN)))
				{
                    object oldValue = _tABLEEN;
					_tABLEEN = value;
					RaisePropertyChanged(SYSLOG.Prop_TABLEEN, oldValue, value);
				}
			}

		}

		[Property("TABLECN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 240)]
		public string TABLECN
		{
			get { return _tABLECN; }
			set
			{
				if ((_tABLECN == null) || (value == null) || (!value.Equals(_tABLECN)))
				{
                    object oldValue = _tABLECN;
					_tABLECN = value;
					RaisePropertyChanged(SYSLOG.Prop_TABLECN, oldValue, value);
				}
			}

		}

		[Property("COLUMNEN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 80)]
		public string COLUMNEN
		{
			get { return _cOLUMNEN; }
			set
			{
				if ((_cOLUMNEN == null) || (value == null) || (!value.Equals(_cOLUMNEN)))
				{
                    object oldValue = _cOLUMNEN;
					_cOLUMNEN = value;
					RaisePropertyChanged(SYSLOG.Prop_COLUMNEN, oldValue, value);
				}
			}

		}

		[Property("COLUMNCN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 240)]
		public string COLUMNCN
		{
			get { return _cOLUMNCN; }
			set
			{
				if ((_cOLUMNCN == null) || (value == null) || (!value.Equals(_cOLUMNCN)))
				{
                    object oldValue = _cOLUMNCN;
					_cOLUMNCN = value;
					RaisePropertyChanged(SYSLOG.Prop_COLUMNCN, oldValue, value);
				}
			}

		}

		[Property("OLDVALUE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string OLDVALUE
		{
			get { return _oLDVALUE; }
			set
			{
				if ((_oLDVALUE == null) || (value == null) || (!value.Equals(_oLDVALUE)))
				{
                    object oldValue = _oLDVALUE;
					_oLDVALUE = value;
					RaisePropertyChanged(SYSLOG.Prop_OLDVALUE, oldValue, value);
				}
			}

		}

		[Property("NEWVALUE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string NEWVALUE
		{
			get { return _nEWVALUE; }
			set
			{
				if ((_nEWVALUE == null) || (value == null) || (!value.Equals(_nEWVALUE)))
				{
                    object oldValue = _nEWVALUE;
					_nEWVALUE = value;
					RaisePropertyChanged(SYSLOG.Prop_NEWVALUE, oldValue, value);
				}
			}

		}

        [Property("CONTENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "Aim.Portal.Model.OracleClobField, Aim.Portal")]
		public string CONTENT
		{
			get { return _cONTENT; }
			set
			{
				if ((_cONTENT == null) || (value == null) || (!value.Equals(_cONTENT)))
				{
                    object oldValue = _cONTENT;
					_cONTENT = value;
					RaisePropertyChanged(SYSLOG.Prop_CONTENT, oldValue, value);
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
					RaisePropertyChanged(SYSLOG.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SYSLOG.Prop_CREATEID, oldValue, value);
				}
			}

		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(SYSLOG.Prop_CREATENAME, oldValue, value);
				}
			}

		}

		[Property("DEPTID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string DEPTID
		{
			get { return _dEPTID; }
			set
			{
				if ((_dEPTID == null) || (value == null) || (!value.Equals(_dEPTID)))
				{
                    object oldValue = _dEPTID;
					_dEPTID = value;
					RaisePropertyChanged(SYSLOG.Prop_DEPTID, oldValue, value);
				}
			}

		}

		[Property("DEPTNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string DEPTNAME
		{
			get { return _dEPTNAME; }
			set
			{
				if ((_dEPTNAME == null) || (value == null) || (!value.Equals(_dEPTNAME)))
				{
                    object oldValue = _dEPTNAME;
					_dEPTNAME = value;
					RaisePropertyChanged(SYSLOG.Prop_DEPTNAME, oldValue, value);
				}
			}

		}

		[Property("COMPANYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string COMPANYID
		{
			get { return _cOMPANYID; }
			set
			{
				if ((_cOMPANYID == null) || (value == null) || (!value.Equals(_cOMPANYID)))
				{
                    object oldValue = _cOMPANYID;
					_cOMPANYID = value;
					RaisePropertyChanged(SYSLOG.Prop_COMPANYID, oldValue, value);
				}
			}

		}

		[Property("COMPANYNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 240)]
		public string COMPANYNAME
		{
			get { return _cOMPANYNAME; }
			set
			{
				if ((_cOMPANYNAME == null) || (value == null) || (!value.Equals(_cOMPANYNAME)))
				{
                    object oldValue = _cOMPANYNAME;
					_cOMPANYNAME = value;
					RaisePropertyChanged(SYSLOG.Prop_COMPANYNAME, oldValue, value);
				}
			}

		}

		#endregion
	} // SYSLOG
}

