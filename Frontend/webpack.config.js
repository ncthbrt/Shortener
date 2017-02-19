'use strict';
const webpack = require('webpack');
const path = require('path');
const appJsEntry = path.join(__dirname, 'src', 'index.jsx');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const autoprefixer = require('autoprefixer');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');


module.exports = {
    entry: {
        jsx: appJsEntry,
        vendor: [
            'react',
            'react-dom',
        ],
    },
    output: {
        filename: '[name].[hash].js',
        path: path.join(__dirname, '../wwwroot'),
        publicPath: './'
    },
    module: {
        loaders: [
            {
                test: /\.json$/,
                loader: 'json'
            },
            {
                test: /\.(js|jsx)$/,
                loaders: [
                    'react-hot',
                    'babel-loader',
                ],
                include: path.join(__dirname, '.', 'src')
            },
            {
                test: /\.scss$/,
                loader: ExtractTextPlugin.extract('css!sass')
            }
        ],
    },
    resolve: {
        extensions: ['', '.js', '.jsx', '.json', '.css', '.scss'],
        root: [path.join(__dirname, './src')]
    },
    plugins: [
        new HtmlWebpackPlugin({
            showErrors: true,
            minify: false,
            inject: true,
            template: 'src/index.html',
            filename: 'index.html',
            hash: true                        
      }),
        new ExtractTextPlugin('style.css', {
            allChunks: true
        }),
        new webpack.optimize.OccurenceOrderPlugin(),
        new webpack.DefinePlugin({
            'process.env': { NODE_ENV: JSON.stringify(process.env.NODE_ENV || 'development') },
            SHORTENAPI_URL : "'/'"
        }),
        new webpack.SourceMapDevToolPlugin({
            exclude: /node_modules/,
        })
    ],
    postcss: [
        autoprefixer({
            browsers: ['last 2 versions']
        })
    ],
    devServer: {
        contentBase: '../wwwroot',
        hot: true
    }
}



