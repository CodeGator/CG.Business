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
        ///   Looks up a localized string similar to {0} failed to add a new {1}! parameter: {2}. See inner exception(s) for more detail..
        /// </summary>
        internal static string CrudStoreBase_AddAsync {
            get {
                return ResourceManager.GetString("CrudStoreBase_AddAsync", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} failed to delete a {1}! parameter: {2}. See inner exception(s) for more detail..
        /// </summary>
        internal static string CrudStoreBase_DeleteAsync {
            get {
                return ResourceManager.GetString("CrudStoreBase_DeleteAsync", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} failed to update a {1}! parameter: {2}. See inner exception(s) for more detail..
        /// </summary>
        internal static string CrudStoreBase_UpdateAsync {
            get {
                return ResourceManager.GetString("CrudStoreBase_UpdateAsync", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The KeyUtility doesn&apos;t know how to deal with a key of type: &apos;{0}&apos;..
        /// </summary>
        internal static string KeyUtility_KeyType {
            get {
                return ResourceManager.GetString("KeyUtility_KeyType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LinqRepository options require a &apos;ConnectionString&apos; field and it appears to be missing, or empty. .
        /// </summary>
        internal static string LinqRepositoryOptions_CS {
            get {
                return ResourceManager.GetString("LinqRepositoryOptions_CS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loader strategy options require a &apos;Strategy&apos; field and it appears to be missing, or empty. .
        /// </summary>
        internal static string LoaderOptions_Strategy {
            get {
                return ResourceManager.GetString("LoaderOptions_Strategy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Repository options require a &apos;Strategy&apos; field and it appears to be missing, or empty. Check the configuration to ensure that it contains a &apos;Strategy&apos; field under the &apos;Repositories&apos; section. Also, ensure that you&apos;re binding to the properties above the &apos;Repositories&apos; section, so that the binding operation won&apos;t fail and the options will validate properly..
        /// </summary>
        internal static string RepositoryOptions_Strategy {
            get {
                return ResourceManager.GetString("RepositoryOptions_Strategy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Despite our best efforts, you&apos;ve somehow managed to call the &apos;{0}&apos; method with an empty strategy name! Please correct the strategy name in the associated options and try again..
        /// </summary>
        internal static string ServiceCollectionExtensions_EmptyStrategyName {
            get {
                return ResourceManager.GetString("ServiceCollectionExtensions_EmptyStrategyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to locate an extension method named: &apos;{0}&apos; accepting IServiceCollection and IConfiguration arguments. Do you have the corresponding assembly loaded, or specified for loading in the configuration? Also, is the strategy name correct?.
        /// </summary>
        internal static string ServiceCollectionExtensions_MethodNotFound {
            get {
                return ResourceManager.GetString("ServiceCollectionExtensions_MethodNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to locate an extension method named: &apos;{0}&apos; accepting {1} and IConfiguration arguments. Do you have the corresponding assembly loaded, or specified for loading in the configuration? Also, is the strategy name correct?.
        /// </summary>
        internal static string ServiceCollectionExtensions_MethodNotFound2 {
            get {
                return ResourceManager.GetString("ServiceCollectionExtensions_MethodNotFound2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to load the specified assembly: &apos;{0}&apos;. If this is a path to an assembly, is it correct? If this is an assembly name, is the corresponding dll file in your path? See inner exception(s) for more detail..
        /// </summary>
        internal static string ServiceCollectionExtensions_NoLoadAssembly {
            get {
                return ResourceManager.GetString("ServiceCollectionExtensions_NoLoadAssembly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Strategy options require a &apos;Strategy&apos; field and it appears to be missing, or empty. Check the configuration to ensure that it contains a &apos;Strategy&apos; field under the &apos;Strategies&apos; section. Also, ensure that you&apos;re binding to the properties above the &apos;Strategies&apos; section, so that the binding operation won&apos;t fail and the options will validate properly..
        /// </summary>
        internal static string StrategyOptions_Strategy {
            get {
                return ResourceManager.GetString("StrategyOptions_Strategy", resourceCulture);
            }
        }
    }
}
