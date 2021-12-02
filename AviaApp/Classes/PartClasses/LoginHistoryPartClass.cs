using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaApp.Model
{
    public partial class LoginHistory
    {
        public string GetTime
        {
            get
            {
                if(LogoutDateTime == null)
                {
                    return "Отстань";
                }
                else
                {
                    TimeSpan? time = LogoutDateTime - LoginDateTime;

                    return time.Value.ToString();

                }
            }
        }
    }
}
