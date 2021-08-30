export let createProxyBuilder = function (options, hub) {
    let connectionBuilder = options
        .hubConnectionBuilderFactory();

    const ulr = options.hubNames[hub];

    if (options.withUrlOptionsProvider) {
        const withUrlOptions = options.withUrlOptionsProvider();
        connectionBuilder = connectionBuilder.withUrl(ulr, withUrlOptions);
    } else {
        connectionBuilder = connectionBuilder.withUrl(ulr);
    }

    if (options.configureConnectionBuilder) {
        connectionBuilder = options.configureConnectionBuilder(connectionBuilder);
    }

    const connection = connectionBuilder.build();

    if (options.configureConnection) {
        options.configureConnection(connection);
    }

    const proxyBuilder = {
        proxy: {}
    };

    proxyBuilder.start = function () {
        return options.start(connection);
    };

    proxyBuilder.addHandler = function(name){
        proxyBuilder.proxy[name] = null;
        connection.on(name, function (notification) {
            const handler = proxyBuilder.proxy[name];
            if(handler){
                handler(notification);
            }
        });
    },
    proxyBuilder.addMethod = function(name, argMapper){
        proxyBuilder.proxy[name] = function (...args) {
            if (argMapper != null) {
                args = argMapper(args);
            }
            const promise = connection.invoke(name, ...args);
            return Promise.resolve(promise);
        };   
    };
    return proxyBuilder;
};