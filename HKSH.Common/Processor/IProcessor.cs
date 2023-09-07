namespace HKSH.Common.Processor
{
    /// <summary>
    /// processor interface
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="I"></typeparam>
    public interface IProcessor<R, I> where R : class where I : class
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="processCtx">The process CTX.</param>
        /// <returns></returns>
        R Process(I input, ProcessCtx processCtx);
    }
}