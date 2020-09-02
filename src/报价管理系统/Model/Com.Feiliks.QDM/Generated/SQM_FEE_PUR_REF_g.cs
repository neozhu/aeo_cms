// Business class SQM_FEE_PUR_REF generated from SQM_FEE_PUR_REF
// Creator: rw
// Created Date: [2018-11-22]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FEE_PUR_REF")]
	public partial class SQM_FEE_PUR_REF : EntityBase<SQM_FEE_PUR_REF>
	{
		#region Property_Names

		public static string Prop_FSSORT = "FSSORT";
        public static string Prop_FSSETCUTOMER = "FSSETCUSTOMER";//是否指定客户的标记
		public static string Prop_FSDISP = "FSDISP";
		public static string Prop_FEECODE = "FEECODE";
		public static string Prop_FSMIN = "FSMIN";
		public static string Prop_FSPRECOND = "FSPRECOND";
		public static string Prop_FSRSLBASE = "FSRSLBASE";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_JSFFLX = "JSFFLX";
		public static string Prop_JSFF = "JSFF";
		public static string Prop_JTLJ = "JTLJ";
		public static string Prop_FEEUNIT = "FEEUNIT";
		public static string Prop_FSJDLB = "FSJDLB";
		public static string Prop_FSFYSM = "FSFYSM";
		public static string Prop_GDZRID = "GDZRID";
		public static string Prop_GDZKEY = "GDZKEY";
		public static string Prop_GDZNAME = "GDZNAME";
		public static string Prop_RID = "RID";
		public static string Prop_FEERID = "FEERID";
		public static string Prop_DJFSRID = "DJFSRID";
		public static string Prop_DJFSNAME = "DJFSNAME";
		public static string Prop_STATUS = "STATUS";
        public static string Prop_FSFYSM_EN = "FSFYSM_EN";//英文费用说明
        public static string Prop_FEEUNIT_EN = "FEEUNIT_EN";//英文费目单位
        public static string Prop_SIGHT_FSFYSM = "SIGHT_FSFYSM";//场景费用说明

        #endregion

        #region Private_Variables

        private string _fSSORT;
        private string _fSSETCUSTOMER;
		private string _fSDISP;
		private string _fEECODE;
		private string _fSMIN;
		private string _fSPRECOND;
		private string _fSRSLBASE;
		private DateTime? _cREATETIME;
		private string _jSFFLX;
		private string _jSFF;
		private string _jTLJ;
		private string _fEEUNIT;
		private string _fSJDLB;
		private string _fSFYSM;
		private string _gDZRID;
		private string _gDZKEY;
		private string _gDZNAME;
		private string _rid;
		private string _fEERID;
		private string _dJFSRID;
		private string _dJFSNAME;
		private string _sTATUS;
        private string _fSFYSM_EN;
        private string _fEEUNIT_EN;
        private string _sIGHT_FSFYSM;//场景费用说明


        #endregion

        #region Constructors

        public SQM_FEE_PUR_REF()
		{
		}

        public SQM_FEE_PUR_REF(
            string p_fSSORT,
            string p_fSSETCUSTOMER,
            string p_fSDISP,
            string p_fEECODE,
            string p_fSMIN,
            string p_fSPRECOND,
            string p_fSRSLBASE,
            DateTime? p_cREATETIME,
            string p_jSFFLX,
            string p_jSFF,
            string p_jTLJ,
            string p_fEEUNIT,
            string p_fSJDLB,
            string p_fSFYSM,
            string p_gDZRID,
            string p_gDZKEY,
            string p_gDZNAME,
            string p_rid,
            string p_fEERID,
            string p_dJFSRID,
            string p_dJFSNAME,
            string p__fSFYSM_EN,
            string p__fEEUNIT_EN,
            string p__sIGHT_FSFYSM,
            string p_sTATUS)
		{
			_fSSORT = p_fSSORT;
            _fSSETCUSTOMER = p_fSSETCUSTOMER;
			_fSDISP = p_fSDISP;
			_fEECODE = p_fEECODE;
			_fSMIN = p_fSMIN;
			_fSPRECOND = p_fSPRECOND;
			_fSRSLBASE = p_fSRSLBASE;
			_cREATETIME = p_cREATETIME;
			_jSFFLX = p_jSFFLX;
			_jSFF = p_jSFF;
			_jTLJ = p_jTLJ;
			_fEEUNIT = p_fEEUNIT;
			_fSJDLB = p_fSJDLB;
			_fSFYSM = p_fSFYSM;
			_gDZRID = p_gDZRID;
			_gDZKEY = p_gDZKEY;
			_gDZNAME = p_gDZNAME;
			_rid = p_rid;
			_fEERID = p_fEERID;
			_dJFSRID = p_dJFSRID;
			_dJFSNAME = p_dJFSNAME;
			_sTATUS = p_sTATUS;
            _fEEUNIT_EN = p__fEEUNIT_EN;
            _fSFYSM_EN = p__fSFYSM_EN;
            _sIGHT_FSFYSM = p__sIGHT_FSFYSM;//场景费用说明

        }

		#endregion

		#region Properties

		[Property("FSSORT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FSSORT
		{
			get { return _fSSORT; }
			set
			{
				if ((_fSSORT == null) || (value == null) || (!value.Equals(_fSSORT)))
				{
                    object oldValue = _fSSORT;
					_fSSORT = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSSORT, oldValue, value);
				}
			}
		}

        //是否指定客户的标记
        [Property("FSSETCUSTOMER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string FSSETCUSTOMER
        {
            get { return _fSSETCUSTOMER; }
            set
            {
                if ((_fSSETCUSTOMER == null) || (value == null) || (!value.Equals(_fSSETCUSTOMER)))
                {
                    object oldValue = _fSSETCUSTOMER;
                    _fSSETCUSTOMER = value;
                    RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSSETCUTOMER, oldValue, value);
                }
            }
        }
        //英文费用说明
        [Property("FSFYSM_EN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string FSFYSM_EN
        {
            get { return _fSFYSM_EN; }
            set
            {
                if ((_fSFYSM_EN == null) || (value == null) || (!value.Equals(_fSFYSM_EN)))
                {
                    object oldValue = _fSFYSM_EN;
                    _fSFYSM_EN = value;
                    RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSFYSM_EN, oldValue, value);
                }
            }
        }

        //场景费用说明
        [Property("SIGHT_FSFYSM", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string SIGHT_FSFYSM
        {
            get { return _sIGHT_FSFYSM; }
            set
            {
                if ((_sIGHT_FSFYSM == null) || (value == null) || (!value.Equals(_sIGHT_FSFYSM)))
                {
                    object oldValue = _sIGHT_FSFYSM;
                    _sIGHT_FSFYSM = value;
                    RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_SIGHT_FSFYSM, oldValue, value);
                }
            }
        }
        //英文费目单位
        [Property("FEEUNIT_EN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string FEEUNIT_EN
        {
            get { return _fEEUNIT_EN; }
            set
            {
                if ((_fEEUNIT_EN == null) || (value == null) || (!value.Equals(_fEEUNIT_EN)))
                {
                    object oldValue = _fEEUNIT_EN;
                    _fEEUNIT_EN = value;
                    RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FEEUNIT_EN, oldValue, value);
                }
            }
        }

        [Property("FSDISP", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FSDISP
		{
			get { return _fSDISP; }
			set
			{
				if ((_fSDISP == null) || (value == null) || (!value.Equals(_fSDISP)))
				{
                    object oldValue = _fSDISP;
					_fSDISP = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSDISP, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FEECODE, oldValue, value);
				}
			}
		}

		[Property("FSMIN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FSMIN
		{
			get { return _fSMIN; }
			set
			{
				if ((_fSMIN == null) || (value == null) || (!value.Equals(_fSMIN)))
				{
                    object oldValue = _fSMIN;
					_fSMIN = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSMIN, oldValue, value);
				}
			}
		}

		[Property("FSPRECOND", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FSPRECOND
		{
			get { return _fSPRECOND; }
			set
			{
				if ((_fSPRECOND == null) || (value == null) || (!value.Equals(_fSPRECOND)))
				{
                    object oldValue = _fSPRECOND;
					_fSPRECOND = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSPRECOND, oldValue, value);
				}
			}
		}

		[Property("FSRSLBASE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FSRSLBASE
		{
			get { return _fSRSLBASE; }
			set
			{
				if ((_fSRSLBASE == null) || (value == null) || (!value.Equals(_fSRSLBASE)))
				{
                    object oldValue = _fSRSLBASE;
					_fSRSLBASE = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSRSLBASE, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("JSFFLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string JSFFLX
		{
			get { return _jSFFLX; }
			set
			{
				if ((_jSFFLX == null) || (value == null) || (!value.Equals(_jSFFLX)))
				{
                    object oldValue = _jSFFLX;
					_jSFFLX = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_JSFFLX, oldValue, value);
				}
			}
		}

		[Property("JSFF", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string JSFF
		{
			get { return _jSFF; }
			set
			{
				if ((_jSFF == null) || (value == null) || (!value.Equals(_jSFF)))
				{
                    object oldValue = _jSFF;
					_jSFF = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_JSFF, oldValue, value);
				}
			}
		}

		[Property("JTLJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string JTLJ
		{
			get { return _jTLJ; }
			set
			{
				if ((_jTLJ == null) || (value == null) || (!value.Equals(_jTLJ)))
				{
                    object oldValue = _jTLJ;
					_jTLJ = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_JTLJ, oldValue, value);
				}
			}
		}

		[Property("FEEUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEEUNIT
		{
			get { return _fEEUNIT; }
			set
			{
				if ((_fEEUNIT == null) || (value == null) || (!value.Equals(_fEEUNIT)))
				{
                    object oldValue = _fEEUNIT;
					_fEEUNIT = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FEEUNIT, oldValue, value);
				}
			}
		}

		[Property("FSJDLB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FSJDLB
		{
			get { return _fSJDLB; }
			set
			{
				if ((_fSJDLB == null) || (value == null) || (!value.Equals(_fSJDLB)))
				{
                    object oldValue = _fSJDLB;
					_fSJDLB = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSJDLB, oldValue, value);
				}
			}
		}

		[Property("FSFYSM", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string FSFYSM
		{
			get { return _fSFYSM; }
			set
			{
				if ((_fSFYSM == null) || (value == null) || (!value.Equals(_fSFYSM)))
				{
                    object oldValue = _fSFYSM;
					_fSFYSM = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FSFYSM, oldValue, value);
				}
			}
		}

		[Property("GDZRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string GDZRID
		{
			get { return _gDZRID; }
			set
			{
				if ((_gDZRID == null) || (value == null) || (!value.Equals(_gDZRID)))
				{
                    object oldValue = _gDZRID;
					_gDZRID = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_GDZRID, oldValue, value);
				}
			}
		}

		[Property("GDZKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string GDZKEY
		{
			get { return _gDZKEY; }
			set
			{
				if ((_gDZKEY == null) || (value == null) || (!value.Equals(_gDZKEY)))
				{
                    object oldValue = _gDZKEY;
					_gDZKEY = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_GDZKEY, oldValue, value);
				}
			}
		}

		[Property("GDZNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string GDZNAME
		{
			get { return _gDZNAME; }
			set
			{
				if ((_gDZNAME == null) || (value == null) || (!value.Equals(_gDZNAME)))
				{
                    object oldValue = _gDZNAME;
					_gDZNAME = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_GDZNAME, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("FEERID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEERID
		{
			get { return _fEERID; }
			set
			{
				if ((_fEERID == null) || (value == null) || (!value.Equals(_fEERID)))
				{
                    object oldValue = _fEERID;
					_fEERID = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_FEERID, oldValue, value);
				}
			}
		}

		[Property("DJFSRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DJFSRID
		{
			get { return _dJFSRID; }
			set
			{
				if ((_dJFSRID == null) || (value == null) || (!value.Equals(_dJFSRID)))
				{
                    object oldValue = _dJFSRID;
					_dJFSRID = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_DJFSRID, oldValue, value);
				}
			}
		}

		[Property("DJFSNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DJFSNAME
		{
			get { return _dJFSNAME; }
			set
			{
				if ((_dJFSNAME == null) || (value == null) || (!value.Equals(_dJFSNAME)))
				{
                    object oldValue = _dJFSNAME;
					_dJFSNAME = value;
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_DJFSNAME, oldValue, value);
				}
			}
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
					RaisePropertyChanged(SQM_FEE_PUR_REF.Prop_STATUS, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_FEE_PUR_REF
}

