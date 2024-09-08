namespace LAF
{
    namespace Models.Config
    {
        public class ServiceConfigurationOptions
        {
            public const string ServiceConfiguration = "ServiceConfiguration";

            public int MatchMinimumValue { get; set; } = 5;
        }

        public class DataProvidersOptions
        {
            public const string DataProviders = "DataServiceProviders";
        }

        public class DataProviderOptions
        {
            public const string DataProvider = "DataServiceProvider";

            public string ServiceType { get; set; } = String.Empty;

            public string ServiceUrl { get; set; } = String.Empty;

            public bool Default { get; set; } = false;
        }
    }
}
