<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload2.aspx.cs" Inherits="CRM.Web.CommonPages.File.Upload2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../App_Themes/Default/jquery-ui-1.8/redmond.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/Ext/form.css" rel="stylesheet" type="text/css" />
    <link href="../../js/jquery.uploadify-v2.1.0/default.css" rel="stylesheet" type="text/css" />

    <script src="../../js/jquery.uploadify-v2.1.0/jquery-1.3.2.min.js" type="text/javascript"></script>

    <link href="../../JS/jquery.uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../JS/jquery.uploadify-v2.1.0/swfobject.js"
        charset="gb2312"></script>

    <script src="../../js/jquery.uploadify-v2.1.0/jquery.uploadify.v2.1.0.js" charset="gb2312"
        type="text/javascript"></script>

    <%--<base target="_self" />--%>
    <title>文件上传</title>
    <style type="text/css">
        body
        {
            width: 100%;
            height: 190px;
            overflow: auto;
            margin-left: 20px;
            background-color: #FAFAFA !important;
        }
        #silverlightControlHost
        {
            float: left;
            text-align: left;
            width: 100%;
        }
    </style>

    <script type="text/javascript">
        function upload() {
            $('#uploadify').uploadifyUpload();
        }

        function UploadComplete() {
            $.ajax({
                url: '<%=UploadServiceUrl%>',
                async: false,
                type: 'get',
                dataType: "jsonp",
                jsonp: "callbackparam",
                jsonpCallback: "success_jsonpCallback",
                contentType: "text/plain; charset=utf-8",
                success: function(result) {
                    if (result != null) {
                        $("#files").val(result[0].name);

                        if (result[0].name && window.opener && window.opener.FinishUpload) {
                            window.opener.FinishUpload(result[0].name.substring(1));
                        }
                        window.close();
                    }
                }
            });
        }

        $(document).ready(function() {

            var multi = getQueryString("IsSingle") == "true" ? false : true;
            //默认10M上传 	
            var size = getQueryString("MaxFileSize") == "" ? 10 : getQueryString("MaxFileSize");
            var sizeLimit = getQueryString("MaxFileSize") == "" ? 10 * 1024 * 1024 : parseFloat(getQueryString("MaxFileSize")) * 1024 * 1024;
            var Filter = getQueryString("Filter") == "" ? "*.*" : unescape(getQueryString("Filter"));
            var MaxNumberToUpload = getQueryString("MaxNumberToUpload") == "" ? "100" : getQueryString("MaxNumberToUpload");
            $("#uploadify").uploadify({
                uploader: '../../JS/jquery.uploadify-v2.1.0/uploadify.swf',
                script: '<%=UploadServiceUrl%>',
                height: 25,
                width: 80,
                fileDataName: 'Filedata',
                cancelImg: '../../JS/jquery.uploadify-v2.1.0/cancel.png',
                folder: 'Portal',
                scriptData: { FolderKey: 'Portal' },
                queueID: 'fileQueue',
                fileExt: Filter,
                method: "post",
                wmode: 'transparent',
                fileDesc: '选择文件' + Filter + '',
                auto: false,
                queueSizeLimit: MaxNumberToUpload,
                multi: multi,
                sizeLimit: sizeLimit,
                onAllComplete: function(filesUploaded, errors, allBytesLoaded, speed) {
                    UploadComplete();
                },
                onComplete: function(event, queueId, fileObj, response, data) {

                    return false;
                },
                overrideEvents: ['onSelectError', 'onDialogClose'],
                onSelectError: function(file, errorCode, errorMsg) {
                    if (errorCode == -110) {
                        alert("文件" + file.name + "超过(" + size + "M)大小限制，请重新上传");
                        return false;
                    }

                },
                onError: function(event, ID, fileObj, errorObj) {
                    if (errorObj.type === "File Size") {
                        alert("文件" + fileObj.name + "超过(" + size + "M)大小限制，请重新上传");

                        countinue;
                    }

                }, onSelectOnce: function(event, data) {
                    if (data.fileCount <= 0 || data.fileCount == null) {
                        alert("请浏览上传的文件");
                    }
                }, onProgress: function(event, queueId, fileObj, data) {
                    $("#files").val("0");
                }
            });
            $("#Btnadd").bind("click", function() {
                upload();
            });
            $("#BtnClear").bind("click", function() {
                $('#uploadify').uploadifyClearQueue();
            });
        });

        function getQueryString(name)//name 是URL的参数名字 
        {
            var reg = new RegExp("(^|&|\\?)" + name + "=([^&]*)(&|$)"), r;
            if (r = window.location.href.match(reg)) return unescape(r[2]); return null;
        } 
    </script>

</head>
<body style="width: 70%; margin-left: 0px; margin-top: 1px">
    <form id="form1" runat="server" style="width: 62%;">
    <div style="text-align: center">
        <div style="width: 405px;">
            <div style="padding: 0; float: left; margin-top: 3px; margin-bottom: 5px;">
                <input type="file" name="uploadify" style="width: 110px; height: 0px; padding-bottom: 0px;
                    margin: 0px" id="uploadify" />
            </div>
            <div style="text-align: right; float: right; margin-top: 3px; width: 200px; margin-bottom: 5px;
                padding-left: 54px;">
                <input type="button" id="Btnadd" value="开始上传" class="aim-ui-button" style="width: 80px;
                    height: 25px;" />&nbsp;&nbsp;
                <input type="button" id="BtnClear" value="清空上传" class="aim-ui-button" style="width: 80px;
                    height: 25px;" />
            </div>
        </div>
        <div id="fileQueue">
        </div>
        <div style="float: left;">
            <input type="hidden" id="files" />
        </div>
    </div>
    </form>
</body>
</html>
