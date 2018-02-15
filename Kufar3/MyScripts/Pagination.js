var element = $('.pagination ul');
$(function ()
{
    var inPage = 5;
    var pageCount = Math.ceil(countDeclaration / inPage);

    if (inPage < countDeclaration)
    {
        var html;
        var minValue = 2;
        var maxValue = 6;
        if (pageCount < 6)
        {
            maxValue = pageCount;
        }

        if (pageNumber > 1)
        {
            html = Url(pageNumber - 1, "<<", "<<");
        }
        else
        {
            html = `<a></a>`;
        }

        element.append(html);

        Append(1, 1, 1);

        if (pageNumber > 4 && pageCount > 6)
        {
            if (pageCount - pageNumber > 2)
            {
                minValue = pageNumber - 2;
                maxValue = Number(pageNumber) + 2;
            }
            else
            {
                minValue = pageCount - 5;
                maxValue = pageCount;
            }
            Append(minValue - 1, "...", "...");
        }

        for (var i = minValue; i <= maxValue; i++)
        {
            Append(i, i, i);
        }
        if (pageCount > 6)
        {
            if (pageCount - pageNumber < 3 && pageCount > 6)
            {

                html = `<a></a>`;
                element.append(html);
            }
            else
            {
                Append(maxValue + 1, "...", "...");
                Append(pageCount, pageCount, pageCount);
            }
        }

        if (pageNumber < pageCount)
        {
            html = Url(Number(pageNumber) + 1, ">>", ">>");
        }
        else
        {
            html = `<a></a>`;
        }

        element.append(html);

        $(`.pagination a[name=${pageNumber}]`).addClass("darc-cub");
    }
});

function Append(page, name, text)
{
    var html = Url(page, name, text);
    element.append(html);
}

function Url(page, name, text)
{
    var _idCategory = parseInt(idCategory);
    var _idSubcategory = parseInt(idSubcategory);
    var url = `/Home/Index?num=${page}`;

    if ((_idCategory !== 0) && (_idSubcategory === 0))
    {
        url = url + `&idCategory=${_idCategory}`;
    }
    else if ((_idCategory === 0) && (_idSubcategory !== 0))
    {
        url = url + `&idSubcategory=${_idSubcategory}`;
    }
    else if ((_idCategory !== 0) && (_idSubcategory !== 0))
    {
        url = url + `&idSubcategory=${_idSubcategory}&idCategory=${_idCategory}`;
    }
    var html = `<a href="${url}" name="${name}"><li><b>${text}</b></li></a>`;
    return html;
}
