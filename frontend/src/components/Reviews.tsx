import { useEffect, useState } from 'react';
import { useAuthStore } from '../store/useAuthStore';
import { fetchReviews, addReview } from '../api/reviews';
import type { ReviewDto } from '../types';

interface Props {
  placeId: string;
}

export function Reviews({ placeId }: Props) {
  const user = useAuthStore((s) => s.user);
  const [reviews, setReviews] = useState<ReviewDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [rating, setRating] = useState(5);
  const [text, setText] = useState('');
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    setLoading(true);
    fetchReviews(placeId).then((res) => {
      setReviews(res.data ?? []);
      setLoading(false);
    });
  }, [placeId]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!text.trim()) return;
    setSubmitting(true);
    const res = await addReview(placeId, rating, text);
    if (res.data) {
      setReviews((prev) => [res.data!, ...prev]);
      setText('');
      setRating(5);
    }
    setSubmitting(false);
  };

  return (
    <div className="mt-6">
      <h2 className="text-lg font-semibold text-gray-800 mb-3">Recenze</h2>

      {user && (
        <form onSubmit={handleSubmit} className="mb-4 p-3 bg-gray-50 rounded-lg border border-gray-200">
          <div className="flex gap-3 items-center mb-2">
            <label className="text-sm text-gray-600">Hodnocení:</label>
            <select
              value={rating}
              onChange={(e) => setRating(Number(e.target.value))}
              className="px-2 py-1 border border-gray-300 rounded text-sm"
            >
              {[5, 4, 3, 2, 1].map((v) => (
                <option key={v} value={v}>{'★'.repeat(v)}{'☆'.repeat(5 - v)}</option>
              ))}
            </select>
          </div>
          <textarea
            value={text}
            onChange={(e) => setText(e.target.value)}
            placeholder="Jak se vám tam líbilo s dětmi?"
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm resize-none focus:outline-none focus:ring-2 focus:ring-indigo-500"
            rows={3}
          />
          <button
            type="submit"
            disabled={submitting || !text.trim()}
            className="mt-2 px-4 py-1.5 bg-indigo-600 text-white text-sm rounded-md hover:bg-indigo-700 disabled:opacity-50"
          >
            {submitting ? 'Odesílám...' : 'Přidat recenzi'}
          </button>
        </form>
      )}

      {loading ? (
        <p className="text-sm text-gray-400">Načítám recenze...</p>
      ) : reviews.length === 0 ? (
        <p className="text-sm text-gray-400">Zatím žádné recenze.</p>
      ) : (
        <div className="flex flex-col gap-3">
          {reviews.map((r) => (
            <div key={r.id} className="p-3 border border-gray-200 rounded-lg">
              <div className="flex items-center gap-2 mb-1">
                {r.userAvatarUrl && (
                  <img src={r.userAvatarUrl} alt="" className="w-6 h-6 rounded-full" referrerPolicy="no-referrer" />
                )}
                <span className="text-sm font-medium text-gray-700">{r.userName}</span>
                <span className="text-xs text-amber-600">{'★'.repeat(r.rating)}</span>
              </div>
              <p className="text-sm text-gray-600">{r.text}</p>
              <p className="text-xs text-gray-400 mt-1">
                {new Date(r.createdAt).toLocaleDateString('cs-CZ')}
              </p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
