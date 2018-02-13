namespace Watermelons.Repository
{
    using System;
    /// <summary>
    /// The database factory.
    /// </summary>
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private WatermelonDbContext dataContext;

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>The <see cref="WatermelonDbContext"/>.</returns>
        public WatermelonDbContext Get()
        {
            return this.dataContext ?? (this.dataContext = new WatermelonDbContext());
        }

        /// <summary>
        /// The dispose core.
        /// </summary>
        protected override void DisposeCore()
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }
        }
    }
}
