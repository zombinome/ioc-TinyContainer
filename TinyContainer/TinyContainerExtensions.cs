namespace TinyContainer
{
    public static class TinyContainerExtensions
    {
        public static TinyContainer RegisterSignleton<TObject>(this TinyContainer container, TObject instance)
        {
            container.Register(typeof(TObject), new SingletonLifetimeManager(instance));
            return container;
        }

        public static TinyContainer RegisterSignleton<TObject>(this TinyContainer container)
        {
            container.Register(typeof(TObject), new SingletonLifetimeManager(typeof(TObject)));
            return container;
        }

        public static TinyContainer RegisterTransient<TMappedType, TActualType>(this TinyContainer container, string name = null)
        {
            container.Register(typeof(TMappedType), new TransientLifetimeManager(typeof(TActualType)));
            return container;
        }

        public static TinyContainer RegisterTransient<TDependency>(this TinyContainer container)
            => RegisterTransient<TDependency, TDependency>(container);

        public static TDependency Resolve<TDependency>(this TinyContainer container)
        {
            return (TDependency)container.Resolve(typeof(TDependency));
        }
    }
}