const path = require('path');

export default () => (
    {
        mode: 'production',
        entry: ['babel-polyfill', './src/dot-chat-client.js'],
        devtool: 'source-map',
        output: {
            path: path.resolve(__dirname, './dist'),
            filename: 'dot-chat-client.js',
            libraryTarget: 'umd',
            libraryExport: 'default',
            globalObject: 'this',
            library: 'DotChatClient'
        },
        externals: {
            'lodash': {
                commonjs: 'lodash',
                commonjs2: 'lodash',
                amd: 'lodash',
                root: '_'
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
