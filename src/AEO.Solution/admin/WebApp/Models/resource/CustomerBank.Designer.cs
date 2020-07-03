namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class CustomerBank {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomerBank() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.CustomerBank", typeof(CustomerBank).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
    public static string Id {
            get {
                return ResourceManager.GetString("Id", resourceCulture);
            }
    }
    public static string CustomerCode {
            get {
                return ResourceManager.GetString("CustomerCode", resourceCulture);
            }
    }
    public static string CustomerName {
            get {
                return ResourceManager.GetString("CustomerName", resourceCulture);
            }
    }
    public static string AccountName {
            get {
                return ResourceManager.GetString("AccountName", resourceCulture);
            }
    }
    public static string Bank {
            get {
                return ResourceManager.GetString("Bank", resourceCulture);
            }
    }
    public static string AccountNo {
            get {
                return ResourceManager.GetString("AccountNo", resourceCulture);
            }
    }
    public static string AccountType {
            get {
                return ResourceManager.GetString("AccountType", resourceCulture);
            }
    }
    public static string BankCountry {
            get {
                return ResourceManager.GetString("BankCountry", resourceCulture);
            }
    }
    public static string BankUse {
            get {
                return ResourceManager.GetString("BankUse", resourceCulture);
            }
    }
    public static string BankAddress1 {
            get {
                return ResourceManager.GetString("BankAddress1", resourceCulture);
            }
    }
    public static string BankAddress2 {
            get {
                return ResourceManager.GetString("BankAddress2", resourceCulture);
            }
    }
    public static string SWIFT {
            get {
                return ResourceManager.GetString("SWIFT", resourceCulture);
            }
    }
    public static string CUR {
            get {
                return ResourceManager.GetString("CUR", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
            }
    }
    public static string CustomerId {
            get {
                return ResourceManager.GetString("CustomerId", resourceCulture);
            }
    }
 }
}