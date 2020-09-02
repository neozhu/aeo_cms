// Business class SysUser generated from SysUser
// Created Date: [2010-04-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
    [ActiveRecord("SysUser")]
    public partial class SysUser : EntityBase<SysUser>, INotifyPropertyChanged
    {

        #region Property_Names

        public static string Prop_UserID = "UserID";
        public static string Prop_LoginName = "LoginName";
        public static string Prop_WorkNo = "WorkNo";
        public static string Prop_Password = "Password";
        public static string Prop_Name = "Name";
        public static string Prop_Email = "Email";
        public static string Prop_Phone = "Phone";
        public static string Prop_HomePhone = "HomePhone";
        public static string Prop_Fax = "Fax";
        public static string Prop_Sex = "Sex";
        public static string Prop_Server_IAGUID = "Server_IAGUID";
        public static string Prop_Server_Seed = "Server_Seed";
        public static string Prop_ThreeDESKEY = "ThreeDESKEY";
        public static string Prop_Ext1 = "Ext1";
        public static string Prop_Ext2 = "Ext2";
        public static string Prop_Remark = "Remark";
        public static string Prop_Status = "Status";
        public static string Prop_LastLogIP = "LastLogIP";
        public static string Prop_LastLogDate = "LastLogDate";
        public static string Prop_SortIndex = "SortIndex";
        public static string Prop_LastModifiedDate = "LastModifiedDate";
        public static string Prop_CreateDate = "CreateDate";

        public static string Prop_Htbegindate = "Htbegindate";
        public static string Prop_Htenddate = "Htenddate";
        public static string Prop_Pk_Jndj = "Pk_Jndj";
        public static string Prop_PostLavel = "PostLavel";
        public static string Prop_Indutydate = "Indutydate";
        public static string Prop_Pk_zw = "Pk_zw";
        public static string Prop_Pk_gw = "Pk_gw";
        public static string Prop_Pk_corp = "Pk_corp";
        public static string Prop_Pk_deptdoc = "Pk_deptdoc";
        public static string Prop_Pk_rylb = "Pk_rylb";

        public static string Prop_Wage = "Wage";
        public static string Prop_Enname = "Enname";
        public static string Prop_Category = "Category";
        public static string Prop_Groups = "Groups";
        public static string Prop_Companys = "Companys";
        public static string Prop_Branchs = "Branchs";
        public static string Prop_Positions = "Positions";
        public static string Prop_Leadership = "Leadership";

        public static string Prop_ACCOUNTTYPE = "ACCOUNTTYPE";
        public static string Prop_PARENTID = "PARENTID";
        public static string Prop_OPNID = "OPNID";
        public static string Prop_OPNNAME = "OPNNAME";
        public static string Prop_CUSTOMERID = "CUSTOMERID";
        public static string Prop_CUSTOMERCODE = "CUSTOMERCODE";
        public static string Prop_CUSTOMERNAME = "CUSTOMERNAME";
        #endregion

        #region Private_Variables

        private string _userid;
        private string _loginName;
        private string _workNo;
        private string _password;
        private string _name;
        private string _email;
        private string _phone;
        private string _homePhone;
        private string _fax;
        private string _sex;
        private string _server_IAGUID;
        private string _server_Seed;
        private string _threeDESKEY;
        private string _remark;
        private string _ext1;
        private string _ext2;
        private int? _status;
        private string _lastLogIP;
        private DateTime? _lastLogDate;
        private int? _sortIndex;
        private DateTime? _lastModifiedDate;
        private DateTime? _createDate;
        private string _createId;
        private string _createName;

        private string _htbegindate;
        private string _htenddate;
        private string _indutydate;
        private string _pk_Jndj;
        private string _postLavel;
        private string _pk_zw;
        private string _pk_gw;
        private string _pk_corp;
        private string _pk_deptdoc;
        private string _pk_rylb;

        #endregion

        #region Constructors

        public SysUser()
        {
        }

        public SysUser(
            string p_userid,
            string p_loginName,
            string p_workNo,
            string p_password,
            string p_name,
            string p_email,
            string p_remark,
            byte? p_status,
            string p_lastLogIP,
            DateTime? p_lastLogDate,
            int? p_sortIndex,
            DateTime? p_lastModifiedDate,
            DateTime? p_createDate,

            string p_htbegindate,
            string p_htenddate,
            string p_indutydate,
            string p_pk_Jndj,
            string p_postLavel,
            string p_pk_zw, string p_pk_gw, string p_pk_corp, string p_pk_deptdoc, string p_pk_rylb)
        {
            _userid = p_userid;
            _loginName = p_loginName;
            _workNo = p_workNo;
            _password = p_password;
            _name = p_name;
            _email = p_email;
            _remark = p_remark;
            _status = p_status;
            _lastLogIP = p_lastLogIP;
            _lastLogDate = p_lastLogDate;
            _sortIndex = p_sortIndex;
            _lastModifiedDate = p_lastModifiedDate;
            _createDate = p_createDate;

            _htbegindate = p_htbegindate;
            _htenddate = p_htenddate;
            _indutydate = p_indutydate;
            _pk_Jndj = p_pk_Jndj;
            _postLavel = p_postLavel;
            _pk_zw = p_pk_zw;
            _pk_gw = p_pk_gw;
            _pk_corp = p_pk_corp;
            _pk_deptdoc = p_pk_deptdoc;
            _pk_rylb = p_pk_rylb;
        }

        #endregion

        #region Properties

        [PrimaryKey("UserID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string UserID
        {
            get { return _userid; }
        }

        [Property("LoginName", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
        public string LoginName
        {
            get { return _loginName; }
            set
            {
                if ((_loginName == null) || (value == null) || (!value.Equals(_loginName)))
                {
                    _loginName = value;
                    NotifyPropertyChanged(SysUser.Prop_LoginName);
                }
            }
        }

        [Property("WorkNo", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 16)]
        public string WorkNo
        {
            get { return _workNo; }
            set
            {
                if ((_workNo == null) || (value == null) || (!value.Equals(_workNo)))
                {
                    _workNo = value;
                    NotifyPropertyChanged(SysUser.Prop_WorkNo);
                }
            }
        }

        [JsonIgnore]
        [Property("Password", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
        public string Password
        {
            get { return _password; }
            set
            {
                if ((_password == null) || (value == null) || (!value.Equals(_password)))
                {
                    _password = value;
                    NotifyPropertyChanged(SysUser.Prop_Password);
                }
            }
        }

        [Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if ((_name == null) || (value == null) || (!value.Equals(_name)))
                {
                    _name = value;
                    NotifyPropertyChanged(SysUser.Prop_Name);
                }
            }
        }

        [Property("Email", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string Email
        {
            get { return _email; }
            set
            {
                if ((_email == null) || (value == null) || (!value.Equals(_email)))
                {
                    _email = value;
                    NotifyPropertyChanged(SysUser.Prop_Email);
                }
            }
        }


        [Property("Phone", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Phone
        {
            get { return _phone; }
            set
            {
                if ((_phone == null) || (value == null) || (!value.Equals(_phone)))
                {
                    object oldValue = _phone;
                    _phone = value;
                    NotifyPropertyChanged(SysUser.Prop_Phone);
                }
            }

        }

        [Property("HomePhone", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                if ((_homePhone == null) || (value == null) || (!value.Equals(_homePhone)))
                {
                    object oldValue = _homePhone;
                    _homePhone = value;
                    NotifyPropertyChanged(SysUser.Prop_HomePhone);
                }
            }

        }

        [Property("Fax", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Fax
        {
            get { return _fax; }
            set
            {
                if ((_fax == null) || (value == null) || (!value.Equals(_fax)))
                {
                    object oldValue = _fax;
                    _fax = value;
                    NotifyPropertyChanged(SysUser.Prop_Fax);
                }
            }

        }

        [Property("Sex", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Sex
        {
            get { return _sex; }
            set
            {
                if ((_sex == null) || (value == null) || (!value.Equals(_sex)))
                {
                    object oldValue = _sex;
                    _sex = value;
                    NotifyPropertyChanged(SysUser.Prop_Sex);
                }
            }

        }

        [Property("Server_IAGUID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Server_IAGUID
        {
            get { return _server_IAGUID; }
            set
            {
                if ((_server_IAGUID == null) || (value == null) || (!value.Equals(_server_IAGUID)))
                {
                    object oldValue = _server_IAGUID;
                    _server_IAGUID = value;
                    NotifyPropertyChanged(SysUser.Prop_Server_IAGUID);
                }
            }

        }

        [Property("Server_Seed", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Server_Seed
        {
            get { return _server_Seed; }
            set
            {
                if ((_server_Seed == null) || (value == null) || (!value.Equals(_server_Seed)))
                {
                    object oldValue = _server_Seed;
                    _server_Seed = value;
                    NotifyPropertyChanged(SysUser.Prop_Server_Seed);
                }
            }

        }

        [Property("ThreeDESKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string ThreeDESKEY
        {
            get { return _threeDESKEY; }
            set
            {
                if ((_threeDESKEY == null) || (value == null) || (!value.Equals(_threeDESKEY)))
                {
                    object oldValue = _threeDESKEY;
                    _threeDESKEY = value;
                }
            }

        }

        [Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if ((_remark == null) || (value == null) || (!value.Equals(_remark)))
                {
                    object oldValue = _remark;
                    _remark = value;
                    RaisePropertyChanged(SysUser.Prop_Remark, oldValue, value);
                }
            }

        }

        [Property("Ext1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string Ext1
        {
            get { return _ext1; }
            set
            {
                if ((_ext1 == null) || (value == null) || (!value.Equals(_ext1)))
                {
                    object oldValue = _ext1;
                    _ext1 = value;
                    NotifyPropertyChanged(SysUser.Prop_Ext1);
                }
            }

        }

        [Property("Ext2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string Ext2
        {
            get { return _ext2; }
            set
            {
                if ((_ext2 == null) || (value == null) || (!value.Equals(_ext2)))
                {
                    object oldValue = _ext2;
                    _ext2 = value;
                    NotifyPropertyChanged(SysUser.Prop_Ext2);
                }
            }

        }
        [Property("Status", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    NotifyPropertyChanged(SysUser.Prop_Status);
                }
            }
        }

        [Property("LastLogIP", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string LastLogIP
        {
            get { return _lastLogIP; }
            set
            {
                if ((_lastLogIP == null) || (value == null) || (!value.Equals(_lastLogIP)))
                {
                    _lastLogIP = value;
                    NotifyPropertyChanged(SysUser.Prop_LastLogIP);
                }
            }
        }

        [Property("LastLogDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? LastLogDate
        {
            get { return _lastLogDate; }
            set
            {
                if (value != _lastLogDate)
                {
                    _lastLogDate = value;
                    NotifyPropertyChanged(SysUser.Prop_LastLogDate);
                }
            }
        }

        [Property("SortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? SortIndex
        {
            get { return _sortIndex; }
            set
            {
                if (value != _sortIndex)
                {
                    _sortIndex = value;
                    NotifyPropertyChanged(SysUser.Prop_SortIndex);
                }
            }
        }

        [Property("LastModifiedDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set
            {
                if (value != _lastModifiedDate)
                {
                    _lastModifiedDate = value;
                    NotifyPropertyChanged(SysUser.Prop_LastModifiedDate);
                }
            }
        }

        [Property("CreateDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? CreateDate
        {
            get { return _createDate; }
            set
            {
                if (value != _createDate)
                {
                    _createDate = value;
                    NotifyPropertyChanged(SysUser.Prop_CreateDate);
                }
            }
        }

        //add by cc
        [Property("Htbegindate", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Htbegindate
        {
            get { return _htbegindate; }
            set
            {
                if (value != _htbegindate)
                {
                    _htbegindate = value;
                    NotifyPropertyChanged(SysUser.Prop_Htbegindate);
                }
            }
        }

        [Property("Htenddate", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Htenddate
        {
            get { return _htenddate; }
            set
            {
                if (value != _htenddate)
                {
                    _htenddate = value;
                    NotifyPropertyChanged(SysUser.Prop_Htenddate);
                }
            }
        }

        [Property("Indutydate", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Indutydate
        {
            get { return _indutydate; }
            set
            {
                if (value != _indutydate)
                {
                    _indutydate = value;
                    NotifyPropertyChanged(SysUser.Prop_Indutydate);
                }
            }
        }


        [Property("Pk_Jndj", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_Jndj
        {
            get { return _pk_Jndj; }
            set
            {
                if ((_pk_Jndj == null) || (value == null) || (!value.Equals(_pk_Jndj)))
                {
                    _pk_Jndj = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_Jndj);
                }
            }
        }

        [Property("PostLavel", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string PostLavel
        {
            get { return _postLavel; }
            set
            {
                if ((_postLavel == null) || (value == null) || (!value.Equals(_postLavel)))
                {
                    _postLavel = value;
                    NotifyPropertyChanged(SysUser.Prop_PostLavel);
                }
            }
        }

        [Property("Pk_zw", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_zw
        {
            get { return _pk_zw; }
            set
            {
                if ((_pk_zw == null) || (value == null) || (!value.Equals(_pk_zw)))
                {
                    _pk_zw = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_zw);
                }
            }
        }

        [Property("Pk_gw", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_gw
        {
            get { return _pk_gw; }
            set
            {
                if ((_pk_gw == null) || (value == null) || (!value.Equals(_pk_gw)))
                {
                    _pk_gw = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_gw);
                }
            }
        }

        [Property("Pk_corp", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_corp
        {
            get { return _pk_corp; }
            set
            {
                if ((_pk_corp == null) || (value == null) || (!value.Equals(_pk_corp)))
                {
                    _pk_corp = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_corp);
                }
            }
        }

        [Property("Pk_deptdoc", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_deptdoc
        {
            get { return _pk_deptdoc; }
            set
            {
                if ((_pk_deptdoc == null) || (value == null) || (!value.Equals(_pk_deptdoc)))
                {
                    _pk_deptdoc = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_deptdoc);
                }
            }
        }

        [Property("Pk_rylb", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Pk_rylb
        {
            get { return _pk_rylb; }
            set
            {
                if ((_pk_rylb == null) || (value == null) || (!value.Equals(_pk_rylb)))
                {
                    _pk_rylb = value;
                    NotifyPropertyChanged(SysUser.Prop_Pk_rylb);
                }
            }
        }
        [Property("CreateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string CreateId
        {
            get { return _createId; }
            set
            {
                if ((_createId == null) || (value == null) || (!value.Equals(_createId)))
                {
                    _createId = value;
                }
            }
        }
        [Property("CreateName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string CreateName
        {
            get { return _createName; }
            set
            {
                if ((_createName == null) || (value == null) || (!value.Equals(_createName)))
                {
                    _createName = value;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler localPropertyChanged = PropertyChanged;
            if (localPropertyChanged != null)
            {
                localPropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

    } // SysUser
}
