const path = require('path');

export default () => (
    {
        mode: 'production',
        entry: './src/dot-chat-signalr.js',
        output: {
            path: path.resolve(__dirname, './dist'),
            filename: 'dot-chat-signalr.js',
            libraryTarget: 'umd',
            libraryExport: 'default',
            globalObject: 'this',
            library: 'dotChatSignalR'
        },
        externals: {
            'lodash': {
                commonjs: 'lodash',
                commonjs2: 'lodash',
                amd: 'lodash',
                root: '_'
            },
            'signalr': {
                commonjs: 'signalr',
                commonjs2: 'signalr',
                amd: 'signalr',
                root: '$'
            }
        },
        module: {
            rules: [
                {
                    test: /\.(js)$/,
                    exclude: /(node_modules|bower_components)/,
                    use: ['babel-loader', 'eslint-loader'],
                }
            ]
        },
    }
);
