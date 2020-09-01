// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using EnvDTE;

namespace NuGet.VisualStudio
{
    /// <summary> Wrapper around <see cref="DTE"/> class. </summary>
    public class EnvDte
    {
        private readonly DTE _dte;

        /// <summary> Create a new instance of <see cref="EnvDte"/>. </summary>
        /// <param name="dte"> DTE object to wrap. </param>
        public EnvDte(DTE dte)
        {
            _dte = dte;
        }

        /// <summary> Underlying <see cref="DTE"/> object. </summary>
        /// <remarks> Temporary until refactoring is finished. </remarks>
        public DTE DTE => _dte;

        /// <summary> Get DTE version. </summary>
        /// <returns> DTE version. </returns>
        public async Task<string> GetVersionAsync()
        {
            await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            return _dte.Version;
        }

        /// <summary> Get Visual Studio SKU. </summary>
        /// <returns> Name of Visual Studio SKU. </returns>
        public async Task<string> GetSkuAsync()
        {
            await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var sku = _dte.Edition;
            if (sku.Equals("Ultimate", StringComparison.OrdinalIgnoreCase)
                ||
                sku.Equals("Premium", StringComparison.OrdinalIgnoreCase)
                ||
                sku.Equals("Professional", StringComparison.OrdinalIgnoreCase))
            {
                sku = "Pro";
            }

            return sku;
        }

        /// <summary> Get full Visual Studio version. </summary>
        /// <returns> Full Visual Studio version. </returns>
        public async Task<string> GetFullVsVersionStringAsync()
        {
            await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            return _dte.Edition + "/" + _dte.Version;
        }

        /// <summary> Get property value. </summary>
        /// <param name="category"> Property category. </param>
        /// <param name="page"> Property page. </param>
        /// <param name="propertyName">Property name. </param>
        /// <returns> Property value. </returns>
        public async Task<object> GetPropertyValueAsync(string category, string page, string name)
        {
            await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var properties = _dte.get_Properties(category, page);
            return properties.Item(name).Value;
        }
    }
}
