// Business class MDM_MIAN_VALUE generated from MDM_MIAN_VALUE
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
	[ActiveRecord("MDM_MIAN_VALUE")]
	public partial class MDM_MIAN_VALUE : EntityBase<MDM_MIAN_VALUE>
	{
		#region Property_Names

		public static string Prop_MDKEY = "MDKEY";
		public static string Prop_COLUMN1 = "COLUMN1";
		public static string Prop_COLUMN2 = "COLUMN2";
		public static string Prop_COLUMN3 = "COLUMN3";
		public static string Prop_COLUMN4 = "COLUMN4";
		public static string Prop_COLUMN5 = "COLUMN5";
		public static string Prop_COLUMN6 = "COLUMN6";
		public static string Prop_COLUMN7 = "COLUMN7";
		public static string Prop_COLUMN8 = "COLUMN8";
		public static string Prop_COLUMN9 = "COLUMN9";
		public static string Prop_COLUMN10 = "COLUMN10";
		public static string Prop_COLUMN11 = "COLUMN11";
		public static string Prop_COLUMN12 = "COLUMN12";
		public static string Prop_COLUMN13 = "COLUMN13";
		public static string Prop_COLUMN14 = "COLUMN14";
		public static string Prop_COLUMN15 = "COLUMN15";
		public static string Prop_COLUMN16 = "COLUMN16";
		public static string Prop_COLUMN17 = "COLUMN17";
		public static string Prop_COLUMN18 = "COLUMN18";
		public static string Prop_COLUMN19 = "COLUMN19";
		public static string Prop_COLUMN20 = "COLUMN20";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _mDKEY;
		private string _cOLUMN1;
		private string _cOLUMN2;
		private string _cOLUMN3;
		private string _cOLUMN4;
		private string _cOLUMN5;
		private string _cOLUMN6;
		private string _cOLUMN7;
		private string _cOLUMN8;
		private string _cOLUMN9;
		private string _cOLUMN10;
		private string _cOLUMN11;
		private string _cOLUMN12;
		private string _cOLUMN13;
		private string _cOLUMN14;
		private string _cOLUMN15;
		private string _cOLUMN16;
		private string _cOLUMN17;
		private string _cOLUMN18;
		private string _cOLUMN19;
		private string _cOLUMN20;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_MIAN_VALUE()
		{
		}

		public MDM_MIAN_VALUE(
			string p_mDKEY,
			string p_cOLUMN1,
			string p_cOLUMN2,
			string p_cOLUMN3,
			string p_cOLUMN4,
			string p_cOLUMN5,
			string p_cOLUMN6,
			string p_cOLUMN7,
			string p_cOLUMN8,
			string p_cOLUMN9,
			string p_cOLUMN10,
			string p_cOLUMN11,
			string p_cOLUMN12,
			string p_cOLUMN13,
			string p_cOLUMN14,
			string p_cOLUMN15,
			string p_cOLUMN16,
			string p_cOLUMN17,
			string p_cOLUMN18,
			string p_cOLUMN19,
			string p_cOLUMN20,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_mDKEY = p_mDKEY;
			_cOLUMN1 = p_cOLUMN1;
			_cOLUMN2 = p_cOLUMN2;
			_cOLUMN3 = p_cOLUMN3;
			_cOLUMN4 = p_cOLUMN4;
			_cOLUMN5 = p_cOLUMN5;
			_cOLUMN6 = p_cOLUMN6;
			_cOLUMN7 = p_cOLUMN7;
			_cOLUMN8 = p_cOLUMN8;
			_cOLUMN9 = p_cOLUMN9;
			_cOLUMN10 = p_cOLUMN10;
			_cOLUMN11 = p_cOLUMN11;
			_cOLUMN12 = p_cOLUMN12;
			_cOLUMN13 = p_cOLUMN13;
			_cOLUMN14 = p_cOLUMN14;
			_cOLUMN15 = p_cOLUMN15;
			_cOLUMN16 = p_cOLUMN16;
			_cOLUMN17 = p_cOLUMN17;
			_cOLUMN18 = p_cOLUMN18;
			_cOLUMN19 = p_cOLUMN19;
			_cOLUMN20 = p_cOLUMN20;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("MDKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MDKEY
		{
			get { return _mDKEY; }
			set
			{
				if ((_mDKEY == null) || (value == null) || (!value.Equals(_mDKEY)))
				{
                    object oldValue = _mDKEY;
					_mDKEY = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_MDKEY, oldValue, value);
				}
			}
		}

		[Property("COLUMN1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN1
		{
			get { return _cOLUMN1; }
			set
			{
				if ((_cOLUMN1 == null) || (value == null) || (!value.Equals(_cOLUMN1)))
				{
                    object oldValue = _cOLUMN1;
					_cOLUMN1 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN1, oldValue, value);
				}
			}
		}

		[Property("COLUMN2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN2
		{
			get { return _cOLUMN2; }
			set
			{
				if ((_cOLUMN2 == null) || (value == null) || (!value.Equals(_cOLUMN2)))
				{
                    object oldValue = _cOLUMN2;
					_cOLUMN2 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN2, oldValue, value);
				}
			}
		}

		[Property("COLUMN3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN3
		{
			get { return _cOLUMN3; }
			set
			{
				if ((_cOLUMN3 == null) || (value == null) || (!value.Equals(_cOLUMN3)))
				{
                    object oldValue = _cOLUMN3;
					_cOLUMN3 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN3, oldValue, value);
				}
			}
		}

		[Property("COLUMN4", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN4
		{
			get { return _cOLUMN4; }
			set
			{
				if ((_cOLUMN4 == null) || (value == null) || (!value.Equals(_cOLUMN4)))
				{
                    object oldValue = _cOLUMN4;
					_cOLUMN4 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN4, oldValue, value);
				}
			}
		}

		[Property("COLUMN5", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN5
		{
			get { return _cOLUMN5; }
			set
			{
				if ((_cOLUMN5 == null) || (value == null) || (!value.Equals(_cOLUMN5)))
				{
                    object oldValue = _cOLUMN5;
					_cOLUMN5 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN5, oldValue, value);
				}
			}
		}

		[Property("COLUMN6", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN6
		{
			get { return _cOLUMN6; }
			set
			{
				if ((_cOLUMN6 == null) || (value == null) || (!value.Equals(_cOLUMN6)))
				{
                    object oldValue = _cOLUMN6;
					_cOLUMN6 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN6, oldValue, value);
				}
			}
		}

		[Property("COLUMN7", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN7
		{
			get { return _cOLUMN7; }
			set
			{
				if ((_cOLUMN7 == null) || (value == null) || (!value.Equals(_cOLUMN7)))
				{
                    object oldValue = _cOLUMN7;
					_cOLUMN7 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN7, oldValue, value);
				}
			}
		}

		[Property("COLUMN8", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN8
		{
			get { return _cOLUMN8; }
			set
			{
				if ((_cOLUMN8 == null) || (value == null) || (!value.Equals(_cOLUMN8)))
				{
                    object oldValue = _cOLUMN8;
					_cOLUMN8 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN8, oldValue, value);
				}
			}
		}

		[Property("COLUMN9", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN9
		{
			get { return _cOLUMN9; }
			set
			{
				if ((_cOLUMN9 == null) || (value == null) || (!value.Equals(_cOLUMN9)))
				{
                    object oldValue = _cOLUMN9;
					_cOLUMN9 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN9, oldValue, value);
				}
			}
		}

		[Property("COLUMN10", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN10
		{
			get { return _cOLUMN10; }
			set
			{
				if ((_cOLUMN10 == null) || (value == null) || (!value.Equals(_cOLUMN10)))
				{
                    object oldValue = _cOLUMN10;
					_cOLUMN10 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN10, oldValue, value);
				}
			}
		}

		[Property("COLUMN11", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN11
		{
			get { return _cOLUMN11; }
			set
			{
				if ((_cOLUMN11 == null) || (value == null) || (!value.Equals(_cOLUMN11)))
				{
                    object oldValue = _cOLUMN11;
					_cOLUMN11 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN11, oldValue, value);
				}
			}
		}

		[Property("COLUMN12", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN12
		{
			get { return _cOLUMN12; }
			set
			{
				if ((_cOLUMN12 == null) || (value == null) || (!value.Equals(_cOLUMN12)))
				{
                    object oldValue = _cOLUMN12;
					_cOLUMN12 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN12, oldValue, value);
				}
			}
		}

		[Property("COLUMN13", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN13
		{
			get { return _cOLUMN13; }
			set
			{
				if ((_cOLUMN13 == null) || (value == null) || (!value.Equals(_cOLUMN13)))
				{
                    object oldValue = _cOLUMN13;
					_cOLUMN13 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN13, oldValue, value);
				}
			}
		}

		[Property("COLUMN14", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN14
		{
			get { return _cOLUMN14; }
			set
			{
				if ((_cOLUMN14 == null) || (value == null) || (!value.Equals(_cOLUMN14)))
				{
                    object oldValue = _cOLUMN14;
					_cOLUMN14 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN14, oldValue, value);
				}
			}
		}

		[Property("COLUMN15", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN15
		{
			get { return _cOLUMN15; }
			set
			{
				if ((_cOLUMN15 == null) || (value == null) || (!value.Equals(_cOLUMN15)))
				{
                    object oldValue = _cOLUMN15;
					_cOLUMN15 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN15, oldValue, value);
				}
			}
		}

		[Property("COLUMN16", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN16
		{
			get { return _cOLUMN16; }
			set
			{
				if ((_cOLUMN16 == null) || (value == null) || (!value.Equals(_cOLUMN16)))
				{
                    object oldValue = _cOLUMN16;
					_cOLUMN16 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN16, oldValue, value);
				}
			}
		}

		[Property("COLUMN17", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN17
		{
			get { return _cOLUMN17; }
			set
			{
				if ((_cOLUMN17 == null) || (value == null) || (!value.Equals(_cOLUMN17)))
				{
                    object oldValue = _cOLUMN17;
					_cOLUMN17 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN17, oldValue, value);
				}
			}
		}

		[Property("COLUMN18", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN18
		{
			get { return _cOLUMN18; }
			set
			{
				if ((_cOLUMN18 == null) || (value == null) || (!value.Equals(_cOLUMN18)))
				{
                    object oldValue = _cOLUMN18;
					_cOLUMN18 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN18, oldValue, value);
				}
			}
		}

		[Property("COLUMN19", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN19
		{
			get { return _cOLUMN19; }
			set
			{
				if ((_cOLUMN19 == null) || (value == null) || (!value.Equals(_cOLUMN19)))
				{
                    object oldValue = _cOLUMN19;
					_cOLUMN19 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN19, oldValue, value);
				}
			}
		}

		[Property("COLUMN20", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 510)]
		public string COLUMN20
		{
			get { return _cOLUMN20; }
			set
			{
				if ((_cOLUMN20 == null) || (value == null) || (!value.Equals(_cOLUMN20)))
				{
                    object oldValue = _cOLUMN20;
					_cOLUMN20 = value;
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_COLUMN20, oldValue, value);
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}
        [XmlIgnore]
		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_MIAN_VALUE.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_MIAN_VALUE
}

