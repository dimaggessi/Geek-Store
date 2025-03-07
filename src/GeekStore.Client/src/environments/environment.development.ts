export const environment = {
  production: false,
  apiUrl: window['env']?.API_URL || 'https://localhost:5005/api',
  hubUrl: window['env']?.NOTIFICATION_HUB_URL || 'https://localhost:5005/hub/notifications',
  stripePublicKey: window['env']?.STRIPE_PUBLIC_KEY || 'default_key',
  limit: 20,
};
