export const environment = {
  production: false,
  apiUrl: window['env']?.API_URL || 'https://localhost:5005/api',
  hubUrl: window['env']?.NOTIFICATION_HUB_URL || 'https://localhost:5005/hub/notifications',
  stripePublicKey:
    window['env']?.STRIPE_PUBLIC_KEY ||
    'pk_test_51Q1BOXIzbDBtrqwbOQ0tIUNA91gYgehFZZkSaDUIn8DHbqxvCL9zYz22RPabu2IQpKtwqugSjNnI7tA97490a4qO00N3AldmLd',
  limit: 20,
};
