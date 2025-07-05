// This configuration allows running Angular/Karma tests in Microsoft Edge instead of Chrome.
module.exports = function (config) {
  config.set({
    browsers: ['Edge'],
    customLaunchers: {
      Edge: {
        base: 'Edge',
        flags: []
      }
    },
    // ...existing config...
  });
};
