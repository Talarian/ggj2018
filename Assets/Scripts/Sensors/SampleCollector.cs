public class SampleCollector : Sensor
{
	public override void ActivateSensor()
	{
		SampleData data = new SampleData();
		data.location = this.transform.position;
		AddSensorData(data);
	}
}