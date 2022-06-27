window.Download = (options) => {
    var fileUrl = "data:" + options.mimeType + ";base64," + options.byteArray;
   // console.log(fileUrl);
    fetch(fileUrl)
        .then(response => response.blob())
        .then(blob => {
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: options.mimeType });
           // link.download = options.fileName;
           // document.body.appendChild(link);
           // link.click();
           // document.body.removeChild(link);
            console.log('The Link Is => '+link.href);
            webviewerFunctions.initWebViewer(blob);
            return link.href;
        });
    
}

window.webviewerFunctions = {
    initWebViewer: function (blob) {

        const viewerElement = document.getElementById('viewer');
        WebViewer({
            path: 'lib',
            extension: 'pdf',
        }, viewerElement).then((instance) => {
            instance.UI.loadDocument(blob, { filename: 'myfile.pdf' });
            //instance.initWebViewer();
            const { documentViewer } = instance.Core;
            documentViewer.addEventListener('documentLoaded', () => {
                // perform document operations
            });
        })
    }
};