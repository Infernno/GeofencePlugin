using Android.Gms.Location;

namespace Plugin.Geofence
{
    /// <summary>
    /// GeofenceLocationListener class
    /// Listens to location updates
    /// </summary>
    public class GeofenceLocationListener : Android.Gms.Location.LocationCallback
    {
        /// <summary>
        /// Location listener instance
        /// </summary>
        public static GeofenceLocationListener SharedInstance { get; } = new GeofenceLocationListener();

        private GeofenceLocationListener()
        {

        }
        public override void OnLocationResult(LocationResult result)
        {
            //Location Updated
            var currentGeofenceImplementation = CrossGeofence.Current as GeofenceImplementation; 
                  
            // Check if we need to reset the listener in case there was an error, e.g. location services turned off
            if (currentGeofenceImplementation != null && currentGeofenceImplementation.LocationHasError)
            {
                // Reset the broadcast receiver here
                currentGeofenceImplementation.AddGeofences();

                // Reset
                currentGeofenceImplementation.LocationHasError = false;
            }

            foreach (var location in result.Locations)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"{CrossGeofence.Id} - Location Update: {location.Latitude},{location.Longitude}");
                currentGeofenceImplementation?.SetLastKnownLocation(location);
            }
        }
    }
}
