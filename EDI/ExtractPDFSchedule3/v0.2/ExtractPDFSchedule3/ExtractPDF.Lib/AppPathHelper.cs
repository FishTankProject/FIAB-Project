using System;

namespace ExtractPDF.Lib
{
    public static class AppPathHelper
    {
        /// <summary>
        /// Return the application path 
        /// </summary>
        /// <returns></returns>
        public static string GetAppPath()
        {
            /*
                https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application  
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
                */

            // modify the above code so that it will work for both .NET Core & .NET Framework

            string full_path = System.Reflection.Assembly.GetEntryAssembly().Location;

            string app_name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            int index = full_path.LastIndexOf(app_name);

            // return just the application path without the application name
            return full_path.Substring(0, index);
        }
    }
}
