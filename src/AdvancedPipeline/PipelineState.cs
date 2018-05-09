namespace AdvancedPipeline
{
    /// <summary>
    ///     Represents the state of a pipeline.
    /// </summary>
    public enum PipelineState
    {
        /// <summary>
        ///     Indicates that the pipeline hasn't yet been executed.
        /// </summary>
        None,

        /// <summary>
        ///     Indicates that the pipeline is currently being executed.
        /// </summary>
        Busy,

        /// <summary>
        ///     Indicates that the execution of the pipeline was successful.
        /// </summary>
        Success,

        /// <summary>
        ///     Indicates that the last execution of the pipeline resulted in an error.
        /// </summary>
        Error
    }
}