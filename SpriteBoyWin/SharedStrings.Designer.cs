﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpriteBoy {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SharedStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SpriteBoy.SharedStrings", typeof(SharedStrings).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initializing application interface....
        /// </summary>
        internal static string AppCreatingUIState {
            get {
                return ResourceManager.GetString("AppCreatingUIState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registering associated editors....
        /// </summary>
        internal static string AppEditorRegisterState {
            get {
                return ResourceManager.GetString("AppEditorRegisterState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading plugin:
        ///%PLUGIN%....
        /// </summary>
        internal static string AppPluginAddingLabel {
            get {
                return ResourceManager.GetString("AppPluginAddingLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scanning for plugins....
        /// </summary>
        internal static string AppPluginRegisterState {
            get {
                return ResourceManager.GetString("AppPluginRegisterState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registering file preview mechainsm....
        /// </summary>
        internal static string AppPreviewRegisterState {
            get {
                return ResourceManager.GetString("AppPreviewRegisterState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Done!.
        /// </summary>
        internal static string AppReadyState {
            get {
                return ResourceManager.GetString("AppReadyState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application is starting....
        /// </summary>
        internal static string AppStartupState {
            get {
                return ResourceManager.GetString("AppStartupState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skybox.
        /// </summary>
        internal static string SkyFileVisualName {
            get {
                return ResourceManager.GetString("SkyFileVisualName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to None (rough pixels)
        ///Bilinear (smooth).
        /// </summary>
        internal static string TextureFiltering {
            get {
                return ResourceManager.GetString("TextureFiltering", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Clamp
        ///Repeat
        ///Mirrored repeat.
        /// </summary>
        internal static string TextureWrapMode {
            get {
                return ResourceManager.GetString("TextureWrapMode", resourceCulture);
            }
        }
    }
}
