//---------------------------------------------------------------------
// Author: jachymko
//
// Description: Holds current provider object instance.
//
// Creation Date: Feb 16, 2007
//---------------------------------------------------------------------

using System;
using System.Management.Automation.Provider;

namespace PoshCode.Pansies
{

    public static class ProviderContext<TProvider> where TProvider : CmdletProvider
    {
        public static TProvider Current
        {
            get { return (TProvider)ThreadedProviderContext.Current(typeof(TProvider)); }
        }

        public static ThreadedProviderContext.Cookie Enter(TProvider provider)
        {
            if (null == provider)
            {
                throw new ArgumentNullException("provider");
            }

            return ThreadedProviderContext.Enter(typeof(TProvider), provider);
        }
    }
}
