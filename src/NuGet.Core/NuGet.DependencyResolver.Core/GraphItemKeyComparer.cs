// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace NuGet.DependencyResolver
{
    public sealed class GraphItemKeyComparer<T> : IEqualityComparer<GraphItem<T>>
    {
#pragma warning disable IDE1006 // Naming Styles
        private static readonly Lazy<GraphItemKeyComparer<T>> _defaultComparer = new Lazy<GraphItemKeyComparer<T>>(() => new GraphItemKeyComparer<T>());
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Returns a singleton instance for the <see cref="GraphItemKeyComparer"/>.
        /// </summary>
        public static GraphItemKeyComparer<T> Instance
        {
            get
            {
                return _defaultComparer.Value;
            }
        }

        /// <summary>
        /// Get a singleton instance only through the <see cref="GraphItemKeyComparer.Instance"/>.
        /// </summary>
        private GraphItemKeyComparer()
        {
        }

        public bool Equals(GraphItem<T> x, GraphItem<T> y)
        {
            if (x == null)
            {
                return y == null;
            }
            return x.Key.Equals(y.Key);
        }

        public int GetHashCode(GraphItem<T> obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return obj.Key.GetHashCode();
        }
    }
}
