import { useState } from 'react';
import { useAuthStore } from '../store/useAuthStore';
import { savePlace, removeSavedPlace } from '../api/auth';

interface Props {
  placeId: string;
}

export function SavePlaceButton({ placeId }: Props) {
  const user = useAuthStore((s) => s.user);
  const [saved, setSaved] = useState(false);
  const [loading, setLoading] = useState(false);

  if (!user) return null;

  const handleToggle = async () => {
    setLoading(true);
    if (saved) {
      await removeSavedPlace(placeId);
      setSaved(false);
    } else {
      await savePlace(placeId);
      setSaved(true);
    }
    setLoading(false);
  };

  return (
    <button
      onClick={handleToggle}
      disabled={loading}
      className={`px-4 py-2 text-sm font-medium rounded-md transition-colors ${
        saved
          ? 'bg-red-50 text-red-600 border border-red-200 hover:bg-red-100'
          : 'bg-indigo-50 text-indigo-600 border border-indigo-200 hover:bg-indigo-100'
      } disabled:opacity-50`}
    >
      {saved ? '✕ Odebrat z plánu' : '♡ Uložit do plánu'}
    </button>
  );
}
