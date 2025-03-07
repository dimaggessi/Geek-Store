window.__env = {
  API_URL: process.env.API_URL || 'https://localhost:5005/api',
  STRIPE_PUBLIC_KEY: process.env.STRIPE_PUBLIC_KEY || 'default_key',
  NOTIFICATION_HUB_URL:
    process.env.NOTIFICATION_HUB_URL || 'https://localhost:5005/hub/notifications',
};
