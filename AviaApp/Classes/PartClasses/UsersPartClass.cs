using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaApp.Model
{
    public partial class Users
    {
        public string GetAge
        {
            get
            {
                TimeSpan? timeSpan = DateTime.Now - Birthdate;
                int age = timeSpan.Value.Days / 365;
                return age.ToString();
            }
        }

        public string GetColor
        {
            get
            {
                if(Active == false)
                {
                    return "Red";
                }
                else
                {
                    return "Green";
                }
            }
        }
    }
}
