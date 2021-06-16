import * as signalR from 'signalr';

export let createDefaultOptions = function () {
    const defaultOptions = {};
    defaultOptions.withUrlOptionsProvider = null; //(hubName)
    defaultOptions.configureConnectionBuilder = null; //(connectionBuilder)
    defaultOptions.start = function defaultStart(connection) {
        const start = async function (resolve, reject, retryContext) {
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
                const timeout = defaultOptions.getStartTimeoutMs(retryContext);
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
    defaultOptions.configureConnection = null; //(connection)
    defaultOptions.getTimeoutMs = function (retryContext) {
        const count = retryContext.previousRetryCount;
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
        chatParticipantsHub: 'chatParticipantsHub',
        chatMessagesHub: 'chatMessagesHub'
    };
    defaultOptions.hubConnectionBuilderFactory = function () {
        return new signalR.HubConnectionBuilder();
    };
    return defaultOptions;
};