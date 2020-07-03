namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class CustomerAttentionProduct {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomerAttentionProduct() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.CustomerAttentionProduct", typeof(CustomerAttentionProduct).Assembly);
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
    public static string ProductNo {
            get {
                return ResourceManager.GetString("ProductNo", resourceCulture);
            }
    }
    public static string ProductName {
            get {
                return ResourceManager.GetString("ProductName", resourceCulture);
            }
    }
    public static string CUR {
            get {
                return ResourceManager.GetString("CUR", resourceCulture);
            }
    }
    public static string Pric {
            get {
                return ResourceManager.GetString("Pric", resourceCulture);
            }
    }
    public static string SummaryQuote {
            get {
                return ResourceManager.GetString("SummaryQuote", resourceCulture);
            }
    }
    public static string SummaryOrders {
            get {
                return ResourceManager.GetString("SummaryOrders", resourceCulture);
            }
    }
    public static string CustomerId {
            get {
                return ResourceManager.GetString("CustomerId", resourceCulture);
            }
    }
 }
}