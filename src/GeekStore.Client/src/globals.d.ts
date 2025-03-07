declare global {
  interface Window {
    env: {
      API_URL: string;
      STRIPE_PUBLIC_KEY: string;
      NOTIFICATION_HUB_URL: string;
    };
  }
}

export {};
