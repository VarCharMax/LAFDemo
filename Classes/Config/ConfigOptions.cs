namespace LAF
{
    namespace Models.Config
    {
        public interface IDataProviderOptions: IDisposable
        {
            public const string DataProvider = "DataServiceProvider";

            public string ServiceType { get; set; }

            public string ServiceUrl { get; set; }

            public bool Default { get; set; }
        }

        public class ServiceConfigurationOptions
        {
            public const string ServiceConfiguration = "ServiceConfiguration";

            public int MatchMinimumValue { get; set; } = 5;
        }

        public class DataProvidersOptions
        {
            public const string DataProviders = "DataServiceProviders";
        }

        public class DataProviderOptions: IDataProviderOptions
        {
            public const string DataProvider = "DataServiceProvider";
            private bool disposedValue;

            public string ServiceType { get; set; } = String.Empty;

            public string ServiceUrl { get; set; } = String.Empty;

            public bool Default { get; set; } = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~DataProviderOptions()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
