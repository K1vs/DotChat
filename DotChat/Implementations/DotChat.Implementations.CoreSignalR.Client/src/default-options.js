export let createDefaultOptions = function () {
    var defaultOptions = {};
    defaultOptions.start = function defaultStart(connection) {
        var start = async function (resolve, reject, retryContext) {
            try {
                await connection.start();
                resolve();
            } catch (err) {
                console.log(err);
                if (retryContext) {
                    retryContext.previousRetryCount += 1;
                } else {
                    retryContext = {
                        previousRetryCount: 0
                    };
                }
                var timeout = defaultOptions.getStartTimeoutMs(retryContext);
                if (timeout == null) {
                    reject(err);
                } else {
                    setTimeout(() => start(resolve, reject, retryContext), timeout);
                }
            }
        };
        return new Promise((resolve, reject) => {
            start(resolve, reject);
        });
    };
    defaultOptions.configureConnection = null;
    defaultOptions.getTimeoutMs = function (retryContext) {
        let count = retryContext.previousRetryCount;
        if (count < 3) {
            return 0;
        } else if (count < 10) {
            return 10000;
        } else {
            return 60000;
        }
    };
    defaultOptions.getStartTimeoutMs = defaultOptions.getTimeoutMs;
    defaultOptions.getRecconectTimeoutMs = defaultOptions.getTimeoutMs;
    defaultOptions.hubNames = {
        chatsHub: 'chatsHub',
        participantsHub: 'chatParticipantsHub',
        messagesHub: 'chatMessagesHub'
    };
    defaultOptions.hubConnectionBuilderFactory = function () {
        return new window.signalR.HubConnectionBuilder();
    };
    return defaultOptions;
};