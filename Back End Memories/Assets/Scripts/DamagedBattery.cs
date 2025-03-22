class DamagedBattery : Battery
{
    public DamagedBattery() { }
    new public Battery Pickup()
    {
        this.Explode();
        return null;
    }
    private void Explode() {
        Battery.batteries[this.batteryId] = null;
    }
}