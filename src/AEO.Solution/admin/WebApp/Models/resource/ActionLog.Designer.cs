namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class ActionLog {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ActionLog() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.ActionLog", typeof(ActionLog).Assembly);
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
    public static string ActionDateTime {
            get {
                return ResourceManager.GetString("ActionDateTime", resourceCulture);
            }
    }
    public static string User {
            get {
                return ResourceManager.GetString("User", resourceCulture);
            }
    }
    public static string Action {
            get {
                return ResourceManager.GetString("Action", resourceCulture);
            }
    }
    public static string Content {
            get {
                return ResourceManager.GetString("Content", resourceCulture);
            }
    }
    public static string Flag {
            get {
                return ResourceManager.GetString("Flag", resourceCulture);
            }
    }
 }
}