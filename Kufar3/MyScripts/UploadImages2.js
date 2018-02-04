var newImage = "/Images/null.jpg";

var dataImages = [
    {
        imgId: 'img-0',
        path: null,
    },
    {
        imgId: 'img-1',
        path: null,
    },
    {
        imgId: 'img-2',
        path: null,
    },
    {
        imgId: 'img-3',
        path: null,
    },
    {
        imgId: 'img-4',
        path: null,
    },
    {
        imgId: 'img-5',
        path: null,
    }
];

for (var i = 0; i < 6; i++){
    var src = $(`#formId${i}`).val();
    if (src !== undefined && src !== newImage)
    {
        dataImages[i].path = src;
        $(`#${dataImages[i].imgId} img`).attr('src', src);
        $(`#${dataImages[i].imgId} button`).css('display', 'block');
    }
    else
    {
        $(`#formId${i}`).val("");
    }
}

//for (var dataImage of dataImages) {
//    var src = $(`#${dataImage.imgId} img`).attr('src');
    
//    if (src !== undefined && src !== newImage)
//    {
//        dataImage.path = src;
//        $(`#formId${i + k}`).val(images[i]);
//        $(`#${dataImage.imgId} button`).css('display', 'block');
//    }
//}

var imagesService = {

    remove: function (index) {
        imagesService.delete(dataImages[index].path);
        imagesService.shift(index);

    },
    shift: function (index) {
        for (var i = index; i < 6; i++) {
            if (i === 5) {
                dataImages[i].path = null;
                imagesService.srcKill(i);
                imagesService.onServer(i);
                continue;
            }
            dataImages[i].path = dataImages[i + 1].path;
            $(`#${dataImages[i].imgId} img`).attr('src', dataImages[i].path);
            imagesService.onServer(i);
            if (dataImages[i].path === null) {
                imagesService.srcKill(i);
            }
        }
    },
    onServer: function (i) {
        $(`#formId${i}`).val(dataImages[i].path);
    },
    srcKill: function (i) {
        $(`#${dataImages[i].imgId} button`).css('display', 'none');
        $(`#${dataImages[i].imgId} img`).attr('src', '/Images/invis.png');
    },
    delete: function (url) {
        $.ajax({
            url: `/Moderator/DeleteImage?url=${url}`,
            type: 'GET',
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });
    }
}



$("#all-images button").click(function () {
    var index = $('#all-images button').index(this);
    imagesService.remove(index);
    console.log("fffffff");
});


$('#files').on('change', function () {
    var files = $('#files')[0].files;

    console.log('file11', files);
    const formData = new FormData();
    for (var file of files) {
        formData.append('file', file, file.name);
    }
    //////////////////////////////////////////////////////
    $.ajax({
        url: '/Moderator/UploadImage',
        type: 'POST',
        data: formData,
        error: function (xhr, error, status) {
            console.log(error, status);
        }
    });
});

function toggleSpinner(files) {
    var index = dataImages.findIndex(x => x.path === null);

    for (var i = index; i <= index + files.length; i++) {
        $(`#img${i}`).toggleClass('spiner');
    }
}