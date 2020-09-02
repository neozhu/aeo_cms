// Business class SQM_WEBCONFIG generated from SQM_WEBCONFIG
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
	[ActiveRecord("SQM_WEBCONFIG")]
	public partial class SQM_WEBCONFIG : EntityBase<SQM_WEBCONFIG>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_DICNAME = "DICNAME";
		public static string Prop_DICCODE = "DICCODE";
		public static string Prop_SIGN = "SIGN";

		#endregion

		#region Private_Variables

		private string _rid;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _dESCRIPTION;
		private string _dICNAME;
		private string _dICCODE;
		private string _sIGN;


		#endregion

		#region Constructors

		public SQM_WEBCONFIG()
		{
		}

		public SQM_WEBCONFIG(
			string p_rID,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_dESCRIPTION,
			string p_dICNAME,
			string p_dICCODE,
			string p_sIGN)
		{
			_rid = p_rID;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_dESCRIPTION = p_dESCRIPTION;
			_dICNAME = p_dICNAME;
			_dICCODE = p_dICCODE;
			_sIGN = p_sIGN;
		}

        #endregion

        #region Properties

        [PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            get { return _rid; }
            set { _rid = value; }
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

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[Property("DESCRIPTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string DESCRIPTION
		{
			get { return _dESCRIPTION; }
			set
			{
				if ((_dESCRIPTION == null) || (value == null) || (!value.Equals(_dESCRIPTION)))
				{
                    object oldValue = _dESCRIPTION;
					_dESCRIPTION = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_DESCRIPTION, oldValue, value);
				}
			}
		}

		[Property("DICNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string DICNAME
		{
			get { return _dICNAME; }
			set
			{
				if ((_dICNAME == null) || (value == null) || (!value.Equals(_dICNAME)))
				{
                    object oldValue = _dICNAME;
					_dICNAME = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_DICNAME, oldValue, value);
				}
			}
		}

		[Property("DICCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DICCODE
		{
			get { return _dICCODE; }
			set
			{
				if ((_dICCODE == null) || (value == null) || (!value.Equals(_dICCODE)))
				{
                    object oldValue = _dICCODE;
					_dICCODE = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_DICCODE, oldValue, value);
				}
			}
		}

		[Property("SIGN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string SIGN
		{
			get { return _sIGN; }
			set
			{
				if ((_sIGN == null) || (value == null) || (!value.Equals(_sIGN)))
				{
                    object oldValue = _sIGN;
					_sIGN = value;
					RaisePropertyChanged(SQM_WEBCONFIG.Prop_SIGN, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_WEBCONFIG
}

