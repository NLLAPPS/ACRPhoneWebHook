namespace ACRPhoneWebHook.Utils
{



    public static class HumanReadableSize
    {
        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }


        public static string Convert(Int64 value, SizeUnits unit)
        {
       
            return String.Format("{0} {1}", (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00"), unit);  
        }
    }
}

