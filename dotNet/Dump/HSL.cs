[Fact]
public async Task should_get_lines()
{
	var lineRef = "4615";
	var vehicleRef = "1113";

	var lineJson = await GetLocation(0, lineRef);
	var vehicleJson = await GetLocation(1, vehicleRef);
}

private async Task<string> GetLocation(int type, string reference)
{
	using (var client = new HttpClient())
	{
		var data = await client.GetStringAsync("http://dev.hsl.fi/siriaccess/vm/json?operatorRef=HSL");

		var locations = JObject.Parse(data)["Siri"]["ServiceDelivery"]["VehicleMonitoringDelivery"]
				.SelectMany(s => s["VehicleActivity"])
				.Where(s => s["MonitoredVehicleJourney"][(type == 0 ? "LineRef" : "VehicleRef")]["value"].ToString() == reference)
				.Select(s => s["MonitoredVehicleJourney"])
				.Select(s => new {
					Lon = s["VehicleLocation"]["Longitude"],
					Lat = s["VehicleLocation"]["Latitude"]
				});

		return JsonConvert.SerializeObject(locations);
	}
}