const path = require('path');

export default () => (
    {
        mode: 'production',
        entry: './src/dot-chat-asp-net-core-signalr-connector.js',
        devtool: 'source-map',
        output: {
            path: path.resolve(__dirname, './dist'),
            filename: 'dot-chat-asp-net-core-signalr-connector.js',
            libraryTarget: 'umd',
            libraryExport: 'default',
            globalObject: 'this',
            library: 'DotChatAspNetCoreSignalRConnector'
        },
        externals: {
            'lodash': {
                commonjs: 'lodash',
                commonjs2: 'lodash',
                amd: 'lodash',
                root: '_'
            },
            'signalr': {
                commonjs: 'signalR',
                commonjs2: 'signalR',
                amd: 'signalR',
                root: 'signalR'
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
