namespace DigiStore.Utilities
{
    public static class JsonConvert
    {
		public static string ConvertObjectToJson
			(object theObject, bool writeIndented = true)
		{
			var options =
				new System.Text.Json.JsonSerializerOptions
				{
					WriteIndented = writeIndented,
				};

			var result =
				System.Text.Json.JsonSerializer
				.Serialize(value: theObject, options: options);

			return result;
		}		
	}
}
