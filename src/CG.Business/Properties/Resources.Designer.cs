﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CG.Business.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CG.Business.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {0} error! The method failed to process one or more selections! section: &apos;{1}&apos;..
        /// </summary>
        internal static string AggregatedFail {
            get {
                return ResourceManager.GetString("AggregatedFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! The method encountered an empty or missing &apos;Name&apos; property!.
        /// </summary>
        internal static string EmptyStrategyName {
            get {
                return ResourceManager.GetString("EmptyStrategyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! The method was pointed to: &apos;{1}&apos; but no LoaderOptions were found there!.
        /// </summary>
        internal static string InvalidLoaderSection {
            get {
                return ResourceManager.GetString("InvalidLoaderSection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LoaderOptons requires a &apos;Name&apos; field and it appears to be missing, or empty. .
        /// </summary>
        internal static string LoaderOptions_Name {
            get {
                return ResourceManager.GetString("LoaderOptions_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! The method encountered a missing or malformed &apos;Selection&apos; property! section: &apos;{1}&apos;..
        /// </summary>
        internal static string MalformedSelectionArray {
            get {
                return ResourceManager.GetString("MalformedSelectionArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! Failed to locate an extension method named: &apos;{1}&apos;, parameters: &apos;{2}&apos;!.
        /// </summary>
        internal static string MethodNotFound {
            get {
                return ResourceManager.GetString("MethodNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! The method was pointed to: &apos;{1}&apos; but no &apos;Selected&apos; field was found there!.
        /// </summary>
        internal static string MissingSelectedField {
            get {
                return ResourceManager.GetString("MissingSelectedField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to load the specified assembly: &apos;{0}&apos;! See inner exception(s) for more detail. .
        /// </summary>
        internal static string NoLoadAssembly {
            get {
                return ResourceManager.GetString("NoLoadAssembly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error! The method failed to process selection: &apos;{1}&apos;! section: &apos;{2}&apos;..
        /// </summary>
        internal static string SingleFail {
            get {
                return ResourceManager.GetString("SingleFail", resourceCulture);
            }
        }
    }
}
