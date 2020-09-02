// Business class TEST123 generated from TEST123
// Creator: rw
// Created Date: [2018-01-05]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("TEST123")]
	public partial class TEST123 : EntityBase<TEST123>
	{
		#region Property_Names

		public static string Prop_IDS = "IDS";
		public static string Prop_NAMES = "NAMES";

		#endregion

		#region Private_Variables

		private System.Decimal _ids;
		private string _nAMES;


		#endregion

		#region Constructors

		public TEST123()
		{
		}

		public TEST123(
			System.Decimal p_ids,
			string p_nAMES)
		{
			_ids = p_ids;
			_nAMES = p_nAMES;
		}

		#endregion

		#region Properties

		[PrimaryKey("IDS", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public System.Decimal IDS
		{
			get { return _ids; }
		}

		[Property("NAMES", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string NAMES
		{
			get { return _nAMES; }
			set
			{
				if ((_nAMES == null) || (value == null) || (!value.Equals(_nAMES)))
				{
                    object oldValue = _nAMES;
					_nAMES = value;
					RaisePropertyChanged(TEST123.Prop_NAMES, oldValue, value);
				}
			}
		}

		#endregion
	} // TEST123
}

