using System.Collections.Generic;
using System.Net;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator.Classes
{
    public class SettingsStorage
    {

        private Settings settings;

        public SettingsStorage(Settings settings)
        {
            this.settings = settings;
        }

        public string Api_key
        {
            get
            {
                return settings.api_key;
            }

            set
            {
                settings.api_key = value;
            }
        }

        
      
    }
}
