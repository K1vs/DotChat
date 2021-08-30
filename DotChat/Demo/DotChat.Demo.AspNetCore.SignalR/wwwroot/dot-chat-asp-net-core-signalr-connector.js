!function(t,e){"object"==typeof exports&&"object"==typeof module?module.exports=e(require("signalR")):"function"==typeof define&&define.amd?define(["signalR"],e):"object"==typeof exports?exports.DotChatAspNetCoreSignalRConnector=e(require("signalR")):t.DotChatAspNetCoreSignalRConnector=e(t.signalR)}(this,(function(t){return function(t){var e={};function n(r){if(e[r])return e[r].exports;var a=e[r]={i:r,l:!1,exports:{}};return t[r].call(a.exports,a,a.exports,n),a.l=!0,a.exports}return n.m=t,n.c=e,n.d=function(t,e,r){n.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:r})},n.r=function(t){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},n.t=function(t,e){if(1&e&&(t=n(t)),8&e)return t;if(4&e&&"object"==typeof t&&t&&t.__esModule)return t;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var a in t)n.d(r,a,function(e){return t[e]}.bind(null,a));return r},n.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return n.d(e,"a",e),e},n.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},n.p="",n(n.s=1)}([function(e,n){e.exports=t},function(t,e,n){"use strict";function r(t){return function(t){if(Array.isArray(t)){for(var e=0,n=new Array(t.length);e<t.length;e++)n[e]=t[e];return n}}(t)||function(t){if(Symbol.iterator in Object(t)||"[object Arguments]"===Object.prototype.toString.call(t))return Array.from(t)}(t)||function(){throw new TypeError("Invalid attempt to spread non-iterable instance")}()}n.r(e),n.d(e,"default",(function(){return c}));var a=function(t,e){var n=t.hubConnectionBuilderFactory(),a=t.hubNames[e];if(t.withUrlOptionsProvider){var o=t.withUrlOptionsProvider();n=n.withUrl(a,o)}else n=n.withUrl(a);t.configureConnectionBuilder&&(n=t.configureConnectionBuilder(n));var i=n.build();t.configureConnection&&t.configureConnection(i);var d={proxy:{},start:function(){return t.start(i)},addHandler:function(t){d.proxy[t]=null,i.on(t,(function(e){var n=d.proxy[t];n&&n(e)}))},addMethod:function(t,e){d.proxy[t]=function(){for(var n=arguments.length,a=new Array(n),o=0;o<n;o++)a[o]=arguments[o];null!=e&&(a=e(a));var d=i.invoke.apply(i,[t].concat(r(a)));return Promise.resolve(d)}}};return d},o=n(0);function i(t,e,n,r,a,o,i){try{var d=t[o](i),u=d.value}catch(t){return void n(t)}d.done?e(u):Promise.resolve(u).then(r,a)}var d=function(){var t={withUrlOptionsProvider:null,configureConnectionBuilder:null};return t.start=function(e){var n=function(){var r,a=(r=regeneratorRuntime.mark((function r(a,o,i){var d;return regeneratorRuntime.wrap((function(r){for(;;)switch(r.prev=r.next){case 0:return r.prev=0,r.next=3,e.start();case 3:a(),r.next=12;break;case 6:r.prev=6,r.t0=r.catch(0),console.log(r.t0),i?i.previousRetryCount+=1:i={previousRetryCount:0},null==(d=t.getStartTimeoutMs(i))?o(r.t0):setTimeout((function(){return n(a,o,i)}),d);case 12:case"end":return r.stop()}}),r,null,[[0,6]])})),function(){var t=this,e=arguments;return new Promise((function(n,a){var o=r.apply(t,e);function d(t){i(o,n,a,d,u,"next",t)}function u(t){i(o,n,a,d,u,"throw",t)}d(void 0)}))});return function(t,e,n){return a.apply(this,arguments)}}();return new Promise((function(t,e){n(t,e)}))},t.configureConnection=null,t.getTimeoutMs=function(t){var e=t.previousRetryCount;return e<3?0:e<10?1e4:6e4},t.getStartTimeoutMs=t.getTimeoutMs,t.getRecconectTimeoutMs=t.getTimeoutMs,t.hubNames={chatsHub:"/chatsHub",chatParticipantsHub:"/chatParticipantsHub",chatMessagesHub:"/chatMessagesHub"},t.hubConnectionBuilderFactory=function(){return new o.HubConnectionBuilder},t};function u(t,e){for(var n=0;n<e.length;n++){var r=e[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(t,r.key,r)}}var c=function(){function t(e){!function(t,e){if(!(t instanceof e))throw new TypeError("Cannot call a class as a function")}(this,t);var n,r,o=d();this._options=e?e(o):o,this._chats=(n=this._options,(r=a(n,"chatsHub")).addMethod("getSummary"),r.addMethod("getPage"),r.addMethod("get"),r.addMethod("add"),r.addMethod("editInfo"),r.addMethod("remove"),r.addHandler("chatAdded"),r.addHandler("chatInfoEdited"),r.addHandler("chatRemoved"),r),this._participants=function(t){var e=a(t,"chatParticipantsHub");return e.addMethod("add"),e.addMethod("invite"),e.addMethod("apply"),e.addMethod("remove"),e.addMethod("block"),e.addMethod("changeType"),e.addMethod("append"),e.addHandler("chatParticipantAdded"),e.addHandler("chatParticipantApplied"),e.addHandler("chatParticipantInvited"),e.addHandler("chatParticipantRemoved"),e.addHandler("chatParticipantBlocked"),e.addHandler("chatParticipantsAppended"),e.addHandler("chatParticipantTypeChanged"),e}(this._options),this._messages=function(t){var e=a(t,"chatMessagesHub");return e.addMethod("getPage",(function(t){return 2===t.length&&t.splice(1,0,[]),t})),e.addMethod("read"),e.addMethod("add"),e.addMethod("edit"),e.addMethod("remove"),e.addHandler("chatMessageAdded"),e.addHandler("chatMessageEdited"),e.addHandler("chatMessageRemoved"),e.addHandler("chatMessagesRead"),e}(this._options)}var e,n,r;return e=t,(n=[{key:"start",value:function(){if(!this._startingPromise){var t=this._chats.start(),e=this._participants.start(),n=this._messages.start();this._startingPromise=Promise.all([t,e,n])}return this._startingPromise}},{key:"chats",get:function(){return this._chats.proxy}},{key:"participants",get:function(){return this._participants.proxy}},{key:"messages",get:function(){return this._messages.proxy}}])&&u(e.prototype,n),r&&u(e,r),t}()}]).default}));
//# sourceMappingURL=dot-chat-asp-net-core-signalr-connector.js.map