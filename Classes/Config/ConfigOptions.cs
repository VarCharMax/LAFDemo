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

    }
}
