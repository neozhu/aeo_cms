namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class CustomerSales {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomerSales() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.CustomerSales", typeof(CustomerSales).Assembly);
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
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string Salesman {
            get {
                return ResourceManager.GetString("Salesman", resourceCulture);
            }
    }
    public static string Dept {
            get {
                return ResourceManager.GetString("Dept", resourceCulture);
            }
    }
    public static string Assigner {
            get {
                return ResourceManager.GetString("Assigner", resourceCulture);
            }
    }
    public static string AssignDate {
            get {
                return ResourceManager.GetString("AssignDate", resourceCulture);
            }
    }
    public static string StopCase {
            get {
                return ResourceManager.GetString("StopCase", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
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
    public static string CustomerId {
            get {
                return ResourceManager.GetString("CustomerId", resourceCulture);
            }
    }
 }
}