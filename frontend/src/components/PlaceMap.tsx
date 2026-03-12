import { APIProvider, Map, AdvancedMarker, InfoWindow } from '@vis.gl/react-google-maps';
import { useState } from 'react';
import { usePlacesStore } from '../store/usePlacesStore';
import { useNavigate } from 'react-router-dom';
import type { PlaceDto } from '../types';

const GOOGLE_MAPS_KEY = import.meta.env.VITE_GOOGLE_MAPS_KEY;

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

export function PlaceMap() {
  const { places } = usePlacesStore();
  const [activePlace, setActivePlace] = useState<PlaceDto | null>(null);
  const navigate = useNavigate();

  if (!GOOGLE_MAPS_KEY) {
    return (
      <div className="flex items-center justify-center h-full bg-gray-100 text-gray-500">
        Google Maps API key not configured (VITE_GOOGLE_MAPS_KEY)
      </div>
    );
  }

  const center = places.length > 0
    ? { lat: places[0].latitude, lng: places[0].longitude }
    : { lat: 50.0755, lng: 14.4378 }; // Praha default

  return (
    <APIProvider apiKey={GOOGLE_MAPS_KEY}>
      <Map
        defaultCenter={center}
        defaultZoom={13}
        mapId="kidspot-map"
        className="w-full h-full"
        gestureHandling="greedy"
      >
        {places.map((place) => (
          <AdvancedMarker
            key={place.id}
            position={{ lat: place.latitude, lng: place.longitude }}
            onClick={() => setActivePlace(place)}
          >
            <div
              className="w-4 h-4 rounded-full border-2 border-white shadow-md"
              style={{ backgroundColor: PLACE_TYPE_COLORS[place.type] ?? '#6b7280' }}
            />
          </AdvancedMarker>
        ))}

        {activePlace && (
          <InfoWindow
            position={{ lat: activePlace.latitude, lng: activePlace.longitude }}
            onCloseClick={() => setActivePlace(null)}
          >
            <div className="p-1 max-w-[220px]">
              <h3 className="font-semibold text-sm">{activePlace.name}</h3>
              <p className="text-xs text-gray-500 mt-1">{activePlace.type} · {activePlace.ageFrom}-{activePlace.ageTo} let</p>
              <div className="flex gap-2 mt-1 text-xs text-gray-500">
                {activePlace.changingTable && <span>🚼</span>}
                {activePlace.kidsMenu && <span>🍽️</span>}
                {activePlace.strollerFriendly && <span>🧑‍🦽</span>}
              </div>
              {activePlace.averageRating > 0 && (
                <p className="text-xs text-amber-600 mt-1">★ {activePlace.averageRating.toFixed(1)}</p>
              )}
              <button
                onClick={() => {
                  navigate(`/place/${activePlace.id}`);
                  setActivePlace(null);
                }}
                className="mt-2 text-xs text-indigo-600 hover:underline"
              >
                Detail →
              </button>
            </div>
          </InfoWindow>
        )}
      </Map>
    </APIProvider>
  );
}
