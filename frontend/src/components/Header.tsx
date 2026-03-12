import { useAuthStore } from '../store/useAuthStore';
import { GoogleLogin } from './GoogleLogin';

export function Header() {
  const { user, logout } = useAuthStore();

  return (
    <header className="bg-white shadow-sm border-b border-gray-200">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">
        <a href="/" className="text-xl font-bold text-indigo-600">
          KidSpot
        </a>
        <div className="flex items-center gap-3">
          {user ? (
            <>
              <a
                href="/saved"
                className="text-sm text-gray-600 hover:text-gray-800 font-medium"
              >
                Moje místa
              </a>
              {user.isAdmin && (
                <a
                  href="/admin"
                  className="text-sm text-indigo-600 hover:text-indigo-800 font-medium"
                >
                  Admin
                </a>
              )}
              <div className="flex items-center gap-2">
                {user.avatarUrl && (
                  <img
                    src={user.avatarUrl}
                    alt={user.name}
                    className="w-8 h-8 rounded-full"
                    referrerPolicy="no-referrer"
                  />
                )}
                <span className="text-sm text-gray-700">{user.name}</span>
              </div>
              <button
                onClick={logout}
                className="text-sm text-gray-500 hover:text-gray-700"
              >
                Odhlásit
              </button>
            </>
          ) : (
            <GoogleLogin />
          )}
        </div>
      </div>
    </header>
  );
}
