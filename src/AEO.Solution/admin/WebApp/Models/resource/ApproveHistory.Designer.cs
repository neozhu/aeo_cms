namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class ApproveHistory {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApproveHistory() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.ApproveHistory", typeof(ApproveHistory).Assembly);
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
    public static string RefId {
            get {
                return ResourceManager.GetString("RefId", resourceCulture);
            }
    }
    public static string RekKey {
            get {
                return ResourceManager.GetString("RefKey", resourceCulture);
            }
    }
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string Initiator {
            get {
                return ResourceManager.GetString("Initiator", resourceCulture);
            }
    }
    public static string SubmitDate {
            get {
                return ResourceManager.GetString("SubmitDate", resourceCulture);
            }
    }
    public static string ToAuditor {
            get {
                return ResourceManager.GetString("ToAuditor", resourceCulture);
            }
    }
    public static string Approver {
            get {
                return ResourceManager.GetString("Approver", resourceCulture);
            }
    }
    public static string ApprovedDate {
            get {
                return ResourceManager.GetString("ApprovedDate", resourceCulture);
            }
    }
    public static string Result {
            get {
                return ResourceManager.GetString("Result", resourceCulture);
            }
    }
    public static string Comment {
            get {
                return ResourceManager.GetString("Comment", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
            }
    }
 }
}