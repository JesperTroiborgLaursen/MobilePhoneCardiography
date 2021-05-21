using System.Collections.Generic;

namespace MobilePhoneCardiography.Services.DataStore
{
    public class Salt
    {
        private List<string> Salts = new List<string> (){"W)ska5zX#8G:", "gGNaNRcMBow7", "1DUr3/W(8ue:"};
        public string GetSalt(int nr)
        {
            return Salts[nr];
        }
    }
}