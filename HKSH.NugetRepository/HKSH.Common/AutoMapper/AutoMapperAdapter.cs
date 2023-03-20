using AutoMapper;
using System.Diagnostics;

namespace HKSH.Common.AutoMapper
{
    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter : ITypeAdapter
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomapperTypeAdapter"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public AutomapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns>
        ///   <paramref name="source" /> mapped to <typeparamref name="TTarget" />
        /// </returns>
        [DebuggerStepThrough]
        public TTarget Adapt<TSource, TTarget>(TSource source)
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// Adapts the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        /// target mapped from source
        /// </returns>
        [DebuggerStepThrough]
        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
        {
            return _mapper.Map(source, target);
        }

        /// <summary>
        /// Adapt a source object to an instnace of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns>
        ///   <paramref name="source" /> mapped to <typeparamref name="TTarget" />
        /// </returns>
        [DebuggerStepThrough]
        public TTarget Adapt<TTarget>(object source)
        {
            return _mapper.Map<TTarget>(source);
        }
    }
}