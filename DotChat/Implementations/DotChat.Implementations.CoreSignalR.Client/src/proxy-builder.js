export let createProxyBuilder = function (options, hub) {
    var connection = options
        .hubConnectionBuilderFactory()
        .withUrl('/' + options.hubNames[hub])
        .build();

    if (options.configureConnection) {
        options.configureConnection(connection);
    }

    var proxyBuilder = {
        proxy: {}
    };

    proxyBuilder.start = function () {
        return options.start(connection);
    };

    proxyBuilder.addHandler = function(name){
        proxyBuilder.proxy[name] = null;
        connection.on(name, function (notification) {
            var handler = proxyBuilder.proxy[name];
            if(handler){
                handler(notification);
            }
        });
    },
    proxyBuilder.addMethod = function(name){
        proxyBuilder.proxy[name] = function(...args){
            var promise = connection.invoke(name, ...args);
            return Promise.resolve(promise);
        };   
    };
    return proxyBuilder;
};