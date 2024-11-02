namespace Resunet.BL.General
{
    public static class Helpers
    {
        public static int? stringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }
    }
}
