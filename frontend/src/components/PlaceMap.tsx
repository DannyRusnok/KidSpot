import { MapContainer, TileLayer, CircleMarker, Popup, useMap } from 'react-leaflet';
import { useEffect, useState } from 'react';
import { usePlacesStore } from '../store/usePlacesStore';
import { useNavigate } from 'react-router-dom';
import type { PlaceDto } from '../types';
import 'leaflet/dist/leaflet.css';

const PLACE_TYPE_COLORS: Record<string, string> = {
  Playground: '#22c55e',
  Restaurant: '#f59e0b',
  Museum: '#8b5cf6',
  Beach: '#06b6d4',
  Park: '#10b981',
  Zoo: '#ef4444',
  Pool: '#3b82f6',
  Other: '#6b7280',
};

const DEFAULT_CENTER: [number, number] = [50.0755, 14.4378]; // Praha

function RecenterMap({ places, cityCenter }: { places: PlaceDto[]; cityCenter: { lat: number; lng: number } | null }) {
  const map = useMap();

  useEffect(() => {
    if (cityCenter) {
      map.setView([cityCenter.lat, cityCenter.lng], 13);
    } else if (places.length > 0) {
      map.setView([places[0].latitude, places[0].longitude], 13);
    }
  }, [places, cityCenter, map]);

  return null;
}

export function PlaceMap() {
  const { places, cityCenter } = usePlacesStore();
  const [activePlace, setActivePlace] = useState<PlaceDto | null>(null);
  const navigate = useNavigate();

  return (
    <MapContainer
      center={DEFAULT_CENTER}
      zoom={13}
      className="w-full h-full"
      scrollWheelZoom={true}
    >
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />

      <RecenterMap places={places} cityCenter={cityCenter} />

      {places.map((place) => (
        <CircleMarker
          key={place.id}
          center={[place.latitude, place.longitude]}
          radius={8}
          pathOptions={{
            fillColor: PLACE_TYPE_COLORS[place.type] ?? '#6b7280',
            fillOpacity: 0.9,
            color: '#fff',
            weight: 2,
          }}
          eventHandlers={{ click: () => setActivePlace(place) }}
        >
          {activePlace?.id === place.id && (
            <Popup eventHandlers={{ remove: () => setActivePlace(null) }}>
              <div className="max-w-[220px]">
                <h3 className="font-semibold text-sm">{place.name}</h3>
                <p className="text-xs text-gray-500 mt-1">
                  {place.type} · {place.ageFrom}–{place.ageTo} let
                </p>
                <div className="flex gap-2 mt-1 text-xs text-gray-500">
                  {place.changingTable && <span>🚼</span>}
                  {place.kidsMenu && <span>🍽️</span>}
                  {place.strollerFriendly && <span>👶</span>}
                </div>
                {place.averageRating > 0 && (
                  <p className="text-xs text-amber-600 mt-1">
                    ★ {place.averageRating.toFixed(1)}
                  </p>
                )}
                <button
                  onClick={() => {
                    navigate(`/place/${place.id}`);
                    setActivePlace(null);
                  }}
                  className="mt-2 text-xs text-indigo-600 hover:underline"
                >
                  Detail →
                </button>
              </div>
            </Popup>
          )}
        </CircleMarker>
      ))}
    </MapContainer>
  );
}
