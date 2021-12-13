namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Api information
    /// </summary>
    public class ApiInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the minimum version.
        /// </summary>
        /// <value>
        /// The minimum version.
        /// </value>
        public int MinVersion { get; set; }
        /// <summary>
        /// Gets or sets the maximum version.
        /// </summary>
        /// <value>
        /// The maximum version.
        /// </value>
        public int MaxVersion { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the request format.
        /// </summary>
        /// <value>
        /// The request format.
        /// </value>
        public string RequestFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiInfo"/> class.
        /// </summary>
        public ApiInfo() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiInfo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="spec">The spec.</param>
        public ApiInfo(string name, ApiSpecification spec)
        {
            Name = name;
            MinVersion = spec.MinVersion;
            MaxVersion = spec.MaxVersion;
            RequestFormat = spec.RequestFormat;
            Path = spec.Path;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{nameof(Name)}={Name}, {nameof(MaxVersion)}={MaxVersion}, {nameof(Path)}={Path}, {nameof(RequestFormat)}={RequestFormat}";
    }
}
