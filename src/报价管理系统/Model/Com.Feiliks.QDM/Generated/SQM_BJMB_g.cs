// Business class SQM_BJMB generated from SQM_BJMB
// Creator: rw
// Created Date: [2018-05-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_BJMB")]
	public partial class SQM_BJMB : EntityBase<SQM_BJMB>
	{
		#region Property_Names

		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATENAME = "CREATENAME";
		public static string Prop_RID = "RID";
		public static string Prop_REMARK = "REMARK";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_VERID = "VERID";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_TEMPLATENAME = "TEMPLATENAME";
		public static string Prop_TEMPLATETYPE = "TEMPLATETYPE";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYNAME = "MODIFYNAME";
		public static string Prop_TEMPLATEJJ = "TEMPLATEJJ";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
        public static string Prop_SORD = "SORD";
        public static string Prop_ORGNAME = "ORGNAME";

        #endregion

        #region Private_Variables

        private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATENAME;
		private string _rid;
		private string _rEMARK;
		private string _dB_KEY;
		private string _vERID;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _tEMPLATENAME;
		private string _tEMPLATETYPE;
		private string _mODIFYID;
		private string _mODIFYNAME;
		private string _tEMPLATEJJ;
		private DateTime? _mODIFYTIME;
        private string _sORD;
        private string _oRGNAME;

		#endregion

		#region Constructors

		public SQM_BJMB()
		{
		}

		public SQM_BJMB(
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATENAME,
			string p_rid,
			string p_rEMARK,
			string p_dB_KEY,
			string p_vERID,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_tEMPLATENAME,
			string p_tEMPLATETYPE,
			string p_mODIFYID,
			string p_mODIFYNAME,
			string p_tEMPLATEJJ,
			DateTime? p_mODIFYTIME,
            string p_sORD,
            string p_oRGNAME)
        

        {
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATENAME = p_cREATENAME;
			_rid = p_rid;
			_rEMARK = p_rEMARK;
			_dB_KEY = p_dB_KEY;
			_vERID = p_vERID;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_tEMPLATENAME = p_tEMPLATENAME;
			_tEMPLATETYPE = p_tEMPLATETYPE;
			_mODIFYID = p_mODIFYID;
			_mODIFYNAME = p_mODIFYNAME;
			_tEMPLATEJJ = p_tEMPLATEJJ;
			_mODIFYTIME = p_mODIFYTIME;
            _sORD = p_sORD;
            _oRGNAME = p_oRGNAME;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_BJMB.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJMB.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_BJMB.Prop_CREATEID, oldValue, value);
				}
			}
		}

		[Property("CREATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATENAME
		{
			get { return _cREATENAME; }
			set
			{
				if ((_cREATENAME == null) || (value == null) || (!value.Equals(_cREATENAME)))
				{
                    object oldValue = _cREATENAME;
					_cREATENAME = value;
					RaisePropertyChanged(SQM_BJMB.Prop_CREATENAME, oldValue, value);
				}
			}
		}

        [PrimaryKey(PrimaryKeyType.Assigned, "RID", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            set { _rid = value; }
            get { return _rid; }
        }

        [Property("REMARK", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string REMARK
		{
			get { return _rEMARK; }
			set
			{
				if ((_rEMARK == null) || (value == null) || (!value.Equals(_rEMARK)))
				{
                    object oldValue = _rEMARK;
					_rEMARK = value;
					RaisePropertyChanged(SQM_BJMB.Prop_REMARK, oldValue, value);
				}
			}
		}

		[Property("DB_KEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string DB_KEY
		{
			get { return _dB_KEY; }
			set
			{
				if ((_dB_KEY == null) || (value == null) || (!value.Equals(_dB_KEY)))
				{
                    object oldValue = _dB_KEY;
					_dB_KEY = value;
					RaisePropertyChanged(SQM_BJMB.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("VERID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string VERID
        {
			get { return _vERID; }
			set
			{
				if ((_vERID == null) || (value == null) || (!value.Equals(_vERID)))
				{
                    object oldValue = _vERID;
                    _vERID = value;
					RaisePropertyChanged(SQM_BJMB.Prop_VERID, oldValue, value);
				}
			}
		}

        [Property("SORD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string SORD
        {
            get { return _sORD; }
            set
            {
                if ((_sORD == null) || (value == null) || (!value.Equals(_sORD)))
                {
                    object oldValue = _sORD;
                    _sORD = value;
                    RaisePropertyChanged(SQM_BJMB.Prop_SORD, oldValue, value);
                }
            }
        }

        [Property("STARTDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? STARTDATE
		{
			get { return _sTARTDATE; }
			set
			{
				if (value != _sTARTDATE)
				{
                    object oldValue = _sTARTDATE;
					_sTARTDATE = value;
					RaisePropertyChanged(SQM_BJMB.Prop_STARTDATE, oldValue, value);
				}
			}
		}

		[Property("ENDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ENDDATE
		{
			get { return _eNDDATE; }
			set
			{
				if (value != _eNDDATE)
				{
                    object oldValue = _eNDDATE;
					_eNDDATE = value;
					RaisePropertyChanged(SQM_BJMB.Prop_ENDDATE, oldValue, value);
				}
			}
		}

		[Property("TEMPLATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string TEMPLATENAME
		{
			get { return _tEMPLATENAME; }
			set
			{
				if ((_tEMPLATENAME == null) || (value == null) || (!value.Equals(_tEMPLATENAME)))
				{
                    object oldValue = _tEMPLATENAME;
					_tEMPLATENAME = value;
					RaisePropertyChanged(SQM_BJMB.Prop_TEMPLATENAME, oldValue, value);
				}
			}
		}

		[Property("TEMPLATETYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TEMPLATETYPE
		{
			get { return _tEMPLATETYPE; }
			set
			{
				if ((_tEMPLATETYPE == null) || (value == null) || (!value.Equals(_tEMPLATETYPE)))
				{
                    object oldValue = _tEMPLATETYPE;
					_tEMPLATETYPE = value;
					RaisePropertyChanged(SQM_BJMB.Prop_TEMPLATETYPE, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_BJMB.Prop_MODIFYID, oldValue, value);
				}
			}
		}

		[Property("MODIFYNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MODIFYNAME
		{
			get { return _mODIFYNAME; }
			set
			{
				if ((_mODIFYNAME == null) || (value == null) || (!value.Equals(_mODIFYNAME)))
				{
                    object oldValue = _mODIFYNAME;
					_mODIFYNAME = value;
					RaisePropertyChanged(SQM_BJMB.Prop_MODIFYNAME, oldValue, value);
				}
			}
		}

		[Property("TEMPLATEJJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TEMPLATEJJ
		{
			get { return _tEMPLATEJJ; }
			set
			{
				if ((_tEMPLATEJJ == null) || (value == null) || (!value.Equals(_tEMPLATEJJ)))
				{
                    object oldValue = _tEMPLATEJJ;
					_tEMPLATEJJ = value;
					RaisePropertyChanged(SQM_BJMB.Prop_TEMPLATEJJ, oldValue, value);
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
					RaisePropertyChanged(SQM_BJMB.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

        [Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 60)]
        public string ORGNAME
        {
            get { return _oRGNAME; }
            set
            {
                if (value != _oRGNAME)
                {
                    object oldValue = _oRGNAME;
                    _oRGNAME = value;
                    RaisePropertyChanged(SQM_BJMB.Prop_ORGNAME, oldValue, value);
                }
            }
        }

        #endregion
    } // SQM_BJMB
}

