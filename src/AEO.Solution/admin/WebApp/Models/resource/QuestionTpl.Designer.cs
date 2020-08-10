namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class QuestionTpl {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal QuestionTpl() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.QuestionTpl", typeof(QuestionTpl).Assembly);
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
    public static string Tpl {
            get {
                return ResourceManager.GetString("Tpl", resourceCulture);
            }
    }
    public static string AuthType {
            get {
                return ResourceManager.GetString("AuthType", resourceCulture);
            }
    }
    public static string Category {
            get {
                return ResourceManager.GetString("Category", resourceCulture);
            }
    }
    public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
    }
    public static string Code {
            get {
                return ResourceManager.GetString("Code", resourceCulture);
            }
    }
    public static string Title {
            get {
                return ResourceManager.GetString("Title", resourceCulture);
            }
    }
    public static string StdDescription {
            get {
                return ResourceManager.GetString("StdDescription", resourceCulture);
            }
    }
    public static string Notes {
            get {
                return ResourceManager.GetString("Notes", resourceCulture);
            }
    }
    public static string StdScore {
            get {
                return ResourceManager.GetString("StdScore", resourceCulture);
            }
    }
    public static string ScoreDescription {
            get {
                return ResourceManager.GetString("ScoreDescription", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
            }
    }
 }
}