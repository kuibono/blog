//var setting = {
//    title: '#news_title',
//    summary: '.introduction',
//    content: '.content'
//};
var siteDomain = "http://www.fewju.com/";
var settings = JSON.parse(localStorage.getItem("settings"));
var setting = {
    title: '#cb_post_title_url',
    summary: '',
    content: '#cnblogs_post_body'
};
var editor;
$(function () {
    $("#btn_submit").click(function () {
        if (editor) {
            editor.post();
        }
        var dto = {
            Title: $("#txt_title").val(),
            UrlName: $("#txt_url_name").val(),
            Summary: $("#txt_summary").val(),
            ContentContent: $("#txt_Content").val(),
            Domain: $("#txt_domain").val()
        };

        $.ajax({
            type: 'POST',
            url: siteDomain+"api/Post",
            data: JSON.stringify(dto),
            dataType: 'json',
            processData: false,
            contentType: 'application/json',
            success: function (result) {
                var url = siteDomain + "Post/Index/" + result;
                //alert(result)
                $("#msg").html("<a target='_blank' href='" + url + "'+>" + url + "</a>");
            },
            error: function (req, status, ex) { },
            timeout: 60000
        });
    });
    
    /* editor */

       
    $("#test").click(function () {
        loadSettings();
    });
    loadPageInfo();
    setTimeout(function () {
        editor = new TINY.editor.edit('editor', {
            id: 'txt_Content',
            width: '100%',
            height: 250,
            cssclass: 'te',
            controlclass: 'tecontrol',
            rowclass: 'teheader',
            dividerclass: 'tedivider',
            controls: ['bold', 'italic', 'underline', 'strikethrough',
                      'orderedlist', 'unorderedlist', 'leftalign',
                      'centeralign', 'rightalign','unformat','font', 'size', 'image'],
            footer: true,
            fonts: ['Verdana', 'Arial', 'Georgia', 'Trebuchet MS'],
            xhtml: true,
            cssfile: 'style.css',
            bodyid: 'editor',
            footerclass: 'tefooter',
            toggle: { text: 'source', activetext: 'wysiwyg', cssclass: 'toggle' },
            resize: { cssclass: 'resize' }
        });
    }, 100);
})

function loadSettings() {
    $.ajax({
        type: 'GET',
        url: siteDomain + "api/Collect",
        dataType: 'json',
        processData: false,
        contentType: 'application/json',
        success: function (result) {
            settings = result;
            localStorage.setItem("settings", JSON.stringify(result));
        },
        error: function (req, status, ex) { },
        timeout: 60000
    });
}

function loadPageInfo() {
    doInTab({ all: true }, function (obj) {
        var html = $(obj);
        doInTab({ getUrl: true },function (obj) {
            $("#txt_url").val(obj);
        });
        doInTab({ getHost: true }, function (obj) {
            $("#txt_domain").val(obj);
            for (var i = 0; i < settings.length; i++) {
                if (obj == settings[i].Domain) {
                    setting = settings[i];
                }
            }
            var title = $(html).find(setting.Title).text().replace(/\s/ig, "");
            $("#txt_title").val(title);
            trans(title, function (msg) {
                $("#txt_url_name").val(msg);
            })
            //$("#txt_Content").val($(html).find(setting.Content).html());
            $("#txt_summary").val($(html).find(setting.Content).text().replace(/\s/ig, "").substring(0, 200));
            document.getElementById("txt_Content").value = $(html).find(setting.Content).html();
        });
        
    });
}

function doInTab(param,callback) {
    chrome.tabs.getSelected(function (tab) {
        chrome.tabs.sendRequest(tab.id, param, callback);
    })
}

function trans(title, callback) {
    try {
        title = title.replace(/\//ig, "").replace(/"/ig, "").replace(/,/ig, "").replace(/\?/ig, "").replace(/\./ig, "");
        $.getJSON("http://openapi.baidu.com/public/2.0/bmt/translate?client_id=rqnxVxKYgvjfafDcYLWGtpQF&q=" + title + "&from=zh&to=en", function (j) {
            var result = j.trans_result[0].dst;
            result = result.replace(/\s/ig, "_");
            callback(result);
        })
    }
    catch (e) { }
   
}