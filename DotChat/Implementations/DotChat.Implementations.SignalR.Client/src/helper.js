export let createProxyHelper = function(proxy, callback){
    return  {
        addHandler: function(name){
            callback[name] = null;
            proxy.on(name, function(notification){
                var handler = callback[name];
                if(handler){
                    handler(notification);
                }
            });
        },
        addMethod: function(name){
            callback[name] = function(...args){
                var promise = proxy.invoke(name, ...args);
                return Promise.resolve(promise);
            };   
        }
    };
};