using System.Collections.Generic;

namespace Code.ApplicationLayer.Services.Server.Gateways
{
    public class DataResult
    {
        public readonly Dictionary<string, string> Data;

        public DataResult( Dictionary<string, string> data )
        {
            Data = data;
        }
    }
}

