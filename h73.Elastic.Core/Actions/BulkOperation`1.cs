using System;

namespace h73.Elastic.Core.Actions
{
    /// <inheritdoc />
    /// <summary>
    /// Bulk operation
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <seealso cref="T:h73.Elastic.Core.Actions.BulkAction" />
    public class BulkOperation<T> : Tuple<BulkAction, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkOperation{T}"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public BulkOperation(BulkAction item1, T item2)
            : base(item1, item2)
        {
        }
    }
}
