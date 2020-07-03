namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class CustomerFollow {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomerFollow() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.CustomerFollow", typeof(CustomerFollow).Assembly);
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
    public static string ContactName {
            get {
                return ResourceManager.GetString("ContactName", resourceCulture);
            }
    }
    public static string FollowType {
            get {
                return ResourceManager.GetString("FollowType", resourceCulture);
            }
    }
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string Owner {
            get {
                return ResourceManager.GetString("Owner", resourceCulture);
            }
    }
    public static string FollowDate {
            get {
                return ResourceManager.GetString("FollowDate", resourceCulture);
            }
    }
    public static string Content {
            get {
                return ResourceManager.GetString("Content", resourceCulture);
            }
    }
    public static string ReminderTime {
            get {
                return ResourceManager.GetString("ReminderTime", resourceCulture);
            }
    }
    public static string ReminderContent {
            get {
                return ResourceManager.GetString("ReminderContent", resourceCulture);
            }
    }
    public static string ReminderTo {
            get {
                return ResourceManager.GetString("ReminderTo", resourceCulture);
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