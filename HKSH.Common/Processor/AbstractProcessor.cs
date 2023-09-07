namespace HKSH.Common.Processor
{
    /// <summary>
    /// abstract processor
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="I"></typeparam>
    public abstract class AbstractProcessor<R, I> : IProcessor<R, I> where R : class where I : class
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="processCtx">The process CTX.</param>
        /// <returns></returns>
        public R Process(I input, ProcessCtx processCtx)
        {
            R result = ProcessInternal(input, processCtx);

            if (HasNextProcessor(input, result, processCtx))
            {
                ProcessNext(input, result, processCtx);
            }

            return result;
        }

        /// <summary>
        /// Determines whether [has next processor] [the specified input].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="result">The result.</param>
        /// <param name="processCtx">The process CTX.</param>
        /// <returns>
        ///   <c>true</c> if [has next processor] [the specified input]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool HasNextProcessor(I input, R result, ProcessCtx processCtx)
        {
            return true;
        }

        /// <summary>
        /// Processes the internal.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="processCtx">The process CTX.</param>
        /// <returns></returns>
        protected abstract R ProcessInternal(I input, ProcessCtx processCtx);

        /// <summary>
        /// Processes the next.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="result">The result.</param>
        /// <param name="processCtx">The process CTX.</param>
        protected abstract void ProcessNext(I input, R result, ProcessCtx processCtx);
    }
}