namespace GalliumPlusApi.CompatibilityHelpers
{
    internal static class Format
    {
        public static decimal FloatToMonetary(float value)
        {
            string roundedRepr = value.ToString("0.00");
            return decimal.Parse(roundedRepr);
        }
    }
}
