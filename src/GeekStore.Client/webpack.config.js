const {BundleAnalyzerPlugin} = require('webpack-bundle-analyzer');

module.exports = (config, options) => {
  if (config.mode === 'production') {
    config.plugins.push(
      new BundleAnalyzerPlugin({
        analyzerMode: 'static',
        reportFilename: '../bundle-report.html',
        openAnalyzer: false,
      })
    );
  }
  return config;
};
