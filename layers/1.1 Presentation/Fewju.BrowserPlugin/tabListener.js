$(function () {
    function a() {
        alert("a");
    }
    chrome.extension.onRequest.addListener(function (request, sender, sendResponse) {
        if (request.getUrl) {
            sendResponse(location.href);
        }
        else if (request.getHost) {
            sendResponse(location.host);
        }
        else if (request.getDom) {
            sendResponse(jQuery(request.getDom).html());
        }
        else if (request.all) {
            sendResponse(jQuery("html").html());
        }
    });

})
