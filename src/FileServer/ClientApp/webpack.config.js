var path = require('path');
var UglifyJSPlugin = require('uglifyjs-webpack-plugin');

module.exports = {
    entry: './src//main.js',
    output: {
        path: path.resolve(__dirname, '../wwwroot/dist'),
        publicPath: '/dist',
        filename:'scripts.js'
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            }
        ]
    },
    plugins: [
        new UglifyJSPlugin()
    ]
}
