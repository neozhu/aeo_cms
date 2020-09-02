// Business class Test generated from Test
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
	[ActiveRecord("Test")]
	public partial class Test : EntityBase<Test>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_Name = "Name";

		#endregion

		#region Private_Variables

		private System.Decimal? _iD;
		private string _name;


		#endregion

		#region Constructors

		public Test()
		{
		}

		public Test(
			System.Decimal? p_iD,
			string p_name)
		{
			_iD = p_iD;
			_name = p_name;
		}

		#endregion

		#region Properties

		[Property("ID", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? ID
		{
			get { return _iD; }
			set
			{
				if (value != _iD)
				{
                    object oldValue = _iD;
					_iD = value;
					RaisePropertyChanged(Test.Prop_ID, oldValue, value);
				}
			}
		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
                    object oldValue = _name;
					_name = value;
					RaisePropertyChanged(Test.Prop_Name, oldValue, value);
				}
			}
		}

		#endregion
	} // Test
}

