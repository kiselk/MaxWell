namespace MaxWell
{
	public static class Constants
	{
		// URL of REST service
	    public static string site_azure = "https://maxwellwebapp.azurewebsites.net";
	    public static string site_reg = "http://maxwell.vmobile.online";

        public static string RestUrl = site_reg+ "/api/todoitems/{0}";
	    public static string PersonsUrl = site_reg + "/api/persons/{0}";
	    public static string PersonsVkUrl = site_reg + "/api/persons/vk/{0}";
	    public static string PersonsFbUrl = site_reg + "/api/persons/fb/{0}";
	    public static string RemoteImagesUrl = site_reg + "/api/remoteimages/{0}";
	    public static string FoodsUrl = site_reg + "/api/foods/{0}";
	    public static string FoodsByVkIdUrl = site_reg + "/api/foods/ByVkId/{0}";
	     public static string FoodDescsUrl = site_reg + "/api/foods/description/{0}";
	    public static string NutritionUrl = site_reg + "/api/foods/nutrition/{0}";
	    public static string PhenylUrl = site_reg + "/api/foods/phenyl/getAll";
		// Credentials that are hard coded into the REST service
        public static string Username = "Xamarin";
		public static string Password = "Pa$$w0rd";
	    public static string vlAppOd = "6810503";
	}
}
