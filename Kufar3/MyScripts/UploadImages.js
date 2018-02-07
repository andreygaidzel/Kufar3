var newImage = "/ContentImages/null.jpg";

var dataImages = [
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
    },
    {
        imgId: 'img-6',
        path: null,
    }
];

var imagesService = {
    add: function(image)
    {
        for (var img of dataImages)
        {
            if ((img.path === null))
            {
                img.path = image;

                $(`#${img.imgId} img`).attr('src', img.path);
                $(`#${img.imgId} button`).css('display', 'block');
                break;
            }
        }
    },
    addRange: function(images)
    {
        var k = 0;
        for (var j = 0; j < dataImages.length; j++)
        {
            if (dataImages[j].path !== null)
            {
                imagesService.onServer(j);
                k++;
            }
        }
        for (var i = 0; i < images.length; i++)
        {
            imagesService.add(images[i]);
           
            $(`#formId${i+k}`).val(images[i]);
            
        }
    },
    remove: function(index)
    {
        imagesService.delete(dataImages[index].path);
        imagesService.shift(index);
        
    },
    shift: function(index)
    {
        for (var i = index; i < 6; i++)
        {
            if (i === 5)
            {
                dataImages[i].path = null;
                imagesService.srcKill(i);
                imagesService.onServer(i);
                continue;
            }
            dataImages[i].path = dataImages[i + 1].path;
            $(`#${dataImages[i].imgId} img`).attr('src', dataImages[i].path);
            imagesService.onServer(i);
            if (dataImages[i].path === null)
            {
                imagesService.srcKill(i);
            }
        }
    },
    onServer: function (i) {
        $(`#formId${i}`).val(dataImages[i].path);
    },
    srcKill: function(i)
    {
        $(`#${dataImages[i].imgId} button`).css('display', 'none');
        $(`#${dataImages[i].imgId} img`).attr('src', '/ContentImages/invis.png');
    },
    delete: function(url)
    {
        $.ajax({
            url: `/Declaration/DeleteImage?url=${url}`,
            type: 'GET',
            success: function(data)
            {
                console.log(data);
            },
            error: function(xhr, error, status)
            {
                console.log(error, status);
            }
        });
    }
}

$("#all-images img").click(function() // клик на диф фотки
{
    var num = $('#all-images img').index(this);

    if (dataImages[num].path === null)
    {
        $('#files').trigger('click');
    }
});

$("#all-images button").click(function()
{
    var index = $('#all-images button').index(this);
    imagesService.remove(index);
});


$('#files').on('change', function()
{
    var files = $('#files')[0].files;

    console.log('file11', files);
    const formData = new FormData();
    for (var file of files)
    {
        formData.append('file', file, file.name);
    }
    //////////////////////////////////////////////////////
    $.ajax({
        url: '/Declaration/UploadImage',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        beforeSend: function()
        {
            toggleSpinner(files);
        },
        success: function(data)
        {
            toggleSpinner(files);

            imagesService.addRange(data);
        },
        error: function(xhr, error, status)
        {
            console.log(error, status);

            toggleSpinner(files);
        }
    });
});

function toggleSpinner(files)
{
    var index = dataImages.findIndex(x => x.path === null);

    for (var i = index; i <= index + files.length; i++)
    {
        $(`#img${i}`).toggleClass('spiner');
    }
}