var path = require('path');
var UglifyJSPlugin = require('uglifyjs-webpack-plugin');

module.exports = {
    entry: './src//main.js',
    output: {
        path: path.resolve(__dirname, '../wwwroot/dist'),
        publicPath: '/dist',
        filename:'scripts.js'
    },
    resolve: {
        alias: {
            vue: 'vue/dist/vue.js'
        }
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.js$/,
                loader: 'babel-loader'
            }
        ]
    },
    plugins: [
        new UglifyJSPlugin()
    ]
}
