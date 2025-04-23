class DamagedBattery : Battery
{
    public DamagedBattery(int id)
    {
        this.batteryId = id;
    }

    new public int Pickup()
    {
        this.Explode();
        return -1;
    }
    private void Explode()
    {
        Battery.batteries[this.batteryId] = null;
    }
}