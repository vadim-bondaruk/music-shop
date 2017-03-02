namespace Shop.Infrastructure.Core
{
    using System;
    using System.Diagnostics;

    public abstract class BaseDisposable : IDisposable
    {
        protected bool Disposed = false;

        ~BaseDisposable()
        {
            Dispose(false);
#if DEBUG
            Debug.WriteLine("GC Deleted: <" + this.GetType().Name + ">");
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//don't call finalizator
        }

        protected abstract void Dispose(bool disposing);
    }
}
