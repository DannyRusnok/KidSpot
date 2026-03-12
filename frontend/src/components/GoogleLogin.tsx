import { useEffect, useRef } from 'react';
import { useAuthStore } from '../store/useAuthStore';

const GOOGLE_CLIENT_ID = import.meta.env.VITE_GOOGLE_CLIENT_ID;

export function GoogleLogin() {
  const buttonRef = useRef<HTMLDivElement>(null);
  const login = useAuthStore((s) => s.login);

  useEffect(() => {
    if (!GOOGLE_CLIENT_ID || !buttonRef.current) return;

    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.onload = () => {
      window.google?.accounts.id.initialize({
        client_id: GOOGLE_CLIENT_ID,
        callback: async (response: { credential: string }) => {
          await login(response.credential);
        },
      });
      window.google?.accounts.id.renderButton(buttonRef.current!, {
        theme: 'outline',
        size: 'medium',
        text: 'signin_with',
      });
    };
    document.body.appendChild(script);

    return () => {
      script.remove();
    };
  }, [login]);

  if (!GOOGLE_CLIENT_ID) {
    return <span className="text-sm text-red-500">Google Client ID not set</span>;
  }

  return <div ref={buttonRef} />;
}
