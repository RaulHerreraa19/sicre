namespace VLCitas.DataLayer
{
    public class GetResource : Resource
    {
        private System.Globalization.CultureInfo customResource;
        public GetResource()
        {
        }

        public string getValueFromResouce(string key, string lang)
        {
            customResource = new System.Globalization.CultureInfo(lang);
            string op = ResourceManager.GetString(key, customResource);
            return op;
        }
    }
}