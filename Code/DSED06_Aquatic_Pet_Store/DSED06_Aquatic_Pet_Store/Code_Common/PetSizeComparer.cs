using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace DSED06_WebApp.Code_Common
{
    public class PetSizeComparer : IComparer<string>
    {

        private static string getNumeric(string input)
        {
            return new string(input.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
        }
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                /*
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
                */
                if (Convert.ToInt32(getNumeric(s1)) > Convert.ToInt32(getNumeric(s2))) return 1;
                if (Convert.ToInt32(getNumeric(s1)) < Convert.ToInt32(getNumeric(s2))) return -1;
                if (Convert.ToInt32(getNumeric(s1)) == Convert.ToInt32(getNumeric(s2)))
                {
                    return string.Compare(s1, s2);
                    //return 0;
                }

            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, true);
        }


        public static bool IsNumeric(object value)
        {
            try
            {
                //string output = new string(value.ToString().ToCharArray().Where(c => char.IsDigit(c)).ToArray());

                int i = Convert.ToInt32(getNumeric(value.ToString()));

                return true;

            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}