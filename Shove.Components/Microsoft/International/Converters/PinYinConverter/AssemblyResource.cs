namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class AssemblyResource
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal AssemblyResource()
        {
        }

        internal static string CHARACTER_NOT_SUPPORTED
        {
            get
            {
                return ResourceManager.GetString("CHARACTER_NOT_SUPPORTED", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string EXCEED_BORDER_EXCEPTION
        {
            get
            {
                return ResourceManager.GetString("EXCEED_BORDER_EXCEPTION", resourceCulture);
            }
        }

        internal static string INDEX_OUT_OF_RANGE
        {
            get
            {
                return ResourceManager.GetString("INDEX_OUT_OF_RANGE", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Microsoft.International.Converters.PinYinConverter.AssemblyResource", typeof(AssemblyResource).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

