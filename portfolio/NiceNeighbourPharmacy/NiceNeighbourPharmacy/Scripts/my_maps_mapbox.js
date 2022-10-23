// This is for the MapBox component
const THE_ACCESS_TOKEN = 'pk.eyJ1IjoiYnJhbnRsZXl4dSIsImEiOiJjbDkxY2xlZXkxOHl3M25tZjB4bTZnOTN5In0.x49xvjeyzM-Fu5pPubqswg';

LOCATION_DEFAULT = {  // This is Monash University Clayton Campus
    latitude: -37.91500000,
    longitude: 145.13000000,
};

// set access token
mapboxgl.accessToken = THE_ACCESS_TOKEN;

const map = new mapboxgl.Map({
    container: 'map', // container ID
    style: 'mapbox://styles/mapbox/streets-v11', // style URL
    center: [LOCATION_DEFAULT.longitude, LOCATION_DEFAULT.latitude], // starting position [lng, lat],
    zoom: 12, // starting zoom
    cooperativeGestures: true
    // projection: 'globe' // display the map as a 3D globe
});

map.on('style.load', () => {
    map.setFog({}); // Set the default atmosphere style
});

// add a default marker
const defaultMarker = new mapboxgl.Marker({ color: 'black' })
    .setLngLat([LOCATION_DEFAULT.longitude, LOCATION_DEFAULT.latitude])
    .addTo(map);

// add the Geocoder Search control
map.addControl(
    new MapboxGeocoder({
        accessToken: mapboxgl.accessToken,
        mapboxgl: mapboxgl
    }),
    'top-right'
);

// add the Full Screen control
map.addControl(
    new mapboxgl.FullscreenControl(),
    'bottom-right'
);

// add the Direction control
map.addControl(
    new MapboxDirections({
        accessToken: mapboxgl.accessToken
    }),
    'top-left'
);