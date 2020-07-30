namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class ProductPricture {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ProductPricture() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.ProductPricture", typeof(ProductPricture).Assembly);
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
    public static string FileName {
            get {
                return ResourceManager.GetString("FileName", resourceCulture);
            }
    }
    public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
    }
    public static string LineNo {
            get {
                return ResourceManager.GetString("LineNo", resourceCulture);
            }
    }
    public static string Size {
            get {
                return ResourceManager.GetString("Size", resourceCulture);
            }
    }
    public static string Folder {
            get {
                return ResourceManager.GetString("Folder", resourceCulture);
            }
    }
    public static string FileId {
            get {
                return ResourceManager.GetString("FileId", resourceCulture);
            }
    }
    public static string FilePath {
            get {
                return ResourceManager.GetString("FilePath", resourceCulture);
            }
    }
    public static string RelativePath {
            get {
                return ResourceManager.GetString("RelativePath", resourceCulture);
            }
    }
    public static string ProductId {
            get {
                return ResourceManager.GetString("ProductId", resourceCulture);
            }
    }
 }
}