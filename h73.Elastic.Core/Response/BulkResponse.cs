namespace h73.Elastic.Core.Response
{
    /// <summary>
    /// A response of a bulk execution. Holding a response for each item responding (in order) of the bulk requests.
    /// Each item holds the index/type/id is operated on, and if it failed or not (with the failure message).
    /// </summary>
    public class BulkResponse
    {
        /// <summary>
        /// Gets or sets the took.
        /// </summary>
        /// <value>
        /// The took.
        /// </value>
        public int Took { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BulkResponse"/> is errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if errors; otherwise, <c>false</c>.
        /// </value>
        public bool Errors { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public Item[] Items { get; set; }

        /// <summary>
        /// Gets or sets the original exception.
        /// </summary>
        /// <value>
        /// The original exception.
        /// </value>
        public string OriginalException { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }
    }
}
