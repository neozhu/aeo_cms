// Business class SQM_HKYDIC generated from SQM_HKYDIC
// Creator: rw
// Created Date: [2019-02-20]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("SQM_HKYDIC")]
	public partial class SQM_HKYDIC : EntityBase<SQM_HKYDIC>
	{
		#region Property_Names

		public static string Prop_CODE = "CODE";
		public static string Prop_NAME = "NAME";
		public static string Prop_TYPE = "TYPE";
		public static string Prop_MRID = "MRID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
        public static string Prop_EXT4 = "EXT4";
        public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
        public static string Prop_CREATETIME = "CREATETIME";
        
        #endregion

        #region Private_Variables

        private string _cODE;
		private string _nAME;
		private string _tYPE;
		private string _mRID;
		private string _mEMO;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
        private string _eXT4;
        private string _rid;
		private string _sTATUS;
        private DateTime? _cREATETIME;


        #endregion

        #region Constructors

        public SQM_HKYDIC()
		{
		}

		public SQM_HKYDIC(
			string p_cODE,
			string p_nAME,
			string p_tYPE,
			string p_mRID,
			string p_mEMO,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
            string p_eXT4,
            string p_rID,
			string p_sTATUS,
            DateTime? p_cREATETIME)
		{
			_cODE = p_cODE;
			_nAME = p_nAME;
			_tYPE = p_tYPE;
			_mRID = p_mRID;
			_mEMO = p_mEMO;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
            _eXT4 = p_eXT4;
            _rid = p_rID;
			_sTATUS = p_sTATUS;
            _cREATETIME = p_cREATETIME;
        }

		#endregion

		#region Properties

		[Property("CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CODE
		{
			get { return _cODE; }
			set
			{
				if ((_cODE == null) || (value == null) || (!value.Equals(_cODE)))
				{
                    object oldValue = _cODE;
					_cODE = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_CODE, oldValue, value);
				}
			}
		}

		[Property("NAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string NAME
		{
			get { return _nAME; }
			set
			{
				if ((_nAME == null) || (value == null) || (!value.Equals(_nAME)))
				{
                    object oldValue = _nAME;
					_nAME = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_NAME, oldValue, value);
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
					RaisePropertyChanged(SQM_HKYDIC.Prop_TYPE, oldValue, value);
				}
			}
		}

		[Property("MRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string MRID
		{
			get { return _mRID; }
			set
			{
				if ((_mRID == null) || (value == null) || (!value.Equals(_mRID)))
				{
                    object oldValue = _mRID;
					_mRID = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_MRID, oldValue, value);
				}
			}
		}

		[Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MEMO
		{
			get { return _mEMO; }
			set
			{
				if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
				{
                    object oldValue = _mEMO;
					_mEMO = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_EXT1, oldValue, value);
				}
			}
		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_EXT2, oldValue, value);
				}
			}
		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_EXT3, oldValue, value);
				}
			}
		}

        [Property("EXT4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string EXT4
        {
            get { return _eXT4; }
            set
            {
                if ((_eXT4 == null) || (value == null) || (!value.Equals(_eXT4)))
                {
                    object oldValue = _eXT4;
                    _eXT4 = value;
                    RaisePropertyChanged(SQM_HKYDIC.Prop_EXT4, oldValue, value);
                }
            }
        }

        [PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            get { return _rid; }
            set { _rid = value; }
        }

        [Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(SQM_HKYDIC.Prop_STATUS, oldValue, value);
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
                    RaisePropertyChanged(SQM_WEBCONFIG.Prop_CREATETIME, oldValue, value);
                }
            }
        }
        #endregion
    } // SQM_HKYDIC
}

