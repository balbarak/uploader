class Uploader {

    constructor(input,vueSelector) {

        this.selector = input;

        this.vueSelector = vueSelector;

        this.form = $(input).closest('form');
        
        this.initFileUploader();

        this.vue = new Vue({
            el: this.vueSelector,
            data: {
                totalSize: null,
                progress: null,
                speed: null,
                uploadedBytes: null,
                isUploading:false
            },
            watch: {

            },
            methods: {

            },
            created() {

            },
            updated: function () {

            },
            mounted() {
            }
        });
    }

    initFileUploader() {

        $(this.selector).fileinput({
            language: "en",
            theme: "explorer",
            minFileCount: 1,
            showUpload: false,
            showBrowse: true,
            browseClass: "btn btn-secondary",
            browseLabel: "Browse ...",
            overwriteInitial: false,
            showPreview: true,
            preferIconicPreview: true,
            previewFileIconSettings: { // configure your icon file extensions
                'doc': '<i class="fa fa-file-word-o text-primary"></i>',
                'xls': '<i class="fa fa-file-excel-o text-success"></i>',
                'ppt': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
                'pdf': '<i class="fa fa-file-pdf-o text-danger"></i>',
                'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
                'htm': '<i class="fa fa-file-code-o text-info"></i>',
                'txt': '<i class="fa fa-file-text-o text-info"></i>',
                'mov': '<i class="fa fa-file-movie-o text-warning"></i>',
                'mp3': '<i class="fa fa-file-audio-o text-warning"></i>',
                // note for these file types below no extension determination logic 
                // has been configured (the keys itself will be used as extensions)
                'jpg': '<i class="fa fa-file-photo text-danger"></i>',
                'gif': '<i class="fa fa-file-photo text-muted"></i>',
                'png': '<i class="fa fa-file-photo text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'doc': function (ext) {
                    return ext.match(/(doc|docx)$/i);
                },
                'xls': function (ext) {
                    return ext.match(/(xls|xlsx)$/i);
                },
                'ppt': function (ext) {
                    return ext.match(/(ppt|pptx)$/i);
                },
                'zip': function (ext) {
                    return ext.match(/(zip|rar|tar|gzip|gz|7z)$/i);
                },
                'htm': function (ext) {
                    return ext.match(/(htm|html)$/i);
                },
                'txt': function (ext) {
                    return ext.match(/(txt|ini|csv|java|php|js|css)$/i);
                },
                'mov': function (ext) {
                    return ext.match(/(avi|mpg|mkv|mov|mp4|3gp|webm|wmv)$/i);
                },
                'mp3': function (ext) {
                    return ext.match(/(mp3|wav)$/i);
                }
            }
        });
    }

    upload() {

        let formData = new FormData(this.form[0]);
        let url = this.form.attr('action');
        let method = this.form.attr('method');

        var instance = this;

        $.ajax({
            url: url,
            method: method,
            processData: false,
            contentType: false,
            data: formData,
            beforeSend: function (xhr) {

                block('#form-div');
            },
            success: function (xhr, status) {


            },
            error: function (xhr, status, error) {

            },
            complete: function (xhr, response) {

                unblock('#form-div');

            },
            xhr: function () {

                console.log('ss');

                var xhrSettings = $.ajaxSettings.xhr();

                var startedDate = new Date();

                if (xhrSettings.upload) {

                    // For handling the progress of the upload
                    xhrSettings.upload.addEventListener('progress', function (e) {

                        if (e.lengthComputable) {

                            var loaded = e.loaded;

                            var progress = Math.floor((e.loaded / e.total) * 100);

                            var currentSize = instance.formatBytes(e.loaded, 2);
                            var totalSize = instance.formatBytes(e.total, 2);
                            var secondsElapsed = (new Date().getTime() - startedDate.getTime()) / 1000;
                            var bytesPerSeconds = secondsElapsed ? loaded / secondsElapsed : 0;
                            var speed = instance.formatBytes(bytesPerSeconds, 2);

                            instance.vue.$data.totalSize = totalSize;
                            instance.vue.$data.progress = progress;
                            instance.vue.$data.speed = speed;
                            instance.vue.$data.uploadedBytes = currentSize;

                        }
                    }, false);
                }
                return xhrSettings;
            }
        });
    }

    formatBytes(totalBytes, digit) {

        if (0 === totalBytes)
            return "0 Bytes";

        var defaultSize = 1024, d = digit || 2,
            postFix = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"],
            total = Math.floor(Math.log(totalBytes) / Math.log(defaultSize));

        return parseFloat((totalBytes / Math.pow(defaultSize, total)).toFixed(d)) + postFix[total];
    }
}