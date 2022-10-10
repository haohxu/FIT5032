// This is for the MapBox component
const THE_ACCESS_TOKEN = 'pk.eyJ1IjoiYnJhbnRsZXl4dSIsImEiOiJjbDkxY2xlZXkxOHl3M25tZjB4bTZnOTN5In0.x49xvjeyzM-Fu5pPubqswg';

mapboxgl.accessToken = THE_ACCESS_TOKEN;

const map = new mapboxgl.Map({
    container: 'map', // container ID
    style: 'mapbox://styles/mapbox/streets-v11', // style URL
    center: [145.13000000, -37.91500000], // starting position [lng, lat],
    // Monash University Claytion Campus: [lon, lat] = [145.13000000, -37.91500000]
    zoom: 11, // starting zoom
    projection: 'globe' // display the map as a 3D globe
});
map.on('style.load', () => {
    map.setFog({}); // Set the default atmosphere style
});