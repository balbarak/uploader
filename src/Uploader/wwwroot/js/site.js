function block(ele) {

    $(ele).block({
        message: '<h6><i class="fa fa-circle-notch fa-spin"></i> Uploading ...</h1>',
        css: {
            padding: '10px',
            backgroundColor: '#dad7d7',
            borderRadius: '5px',
            border:'',
            color: 'Black'
        },
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.4,
            cursor: 'wait'
        }
    });
}

function unblock(ele) {

    $(ele).unblock();
}