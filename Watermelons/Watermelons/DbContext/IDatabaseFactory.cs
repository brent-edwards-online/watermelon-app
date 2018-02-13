namespace Watermelons.Repository
{
    using System;

    /// <summary>
    /// The DatabaseFactory interface.
    /// </summary>
    public interface IDatabaseFactory : IDisposable
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>The <see cref="WatermelonDbContext"/>.</returns>
        WatermelonDbContext Get();
    }
}
