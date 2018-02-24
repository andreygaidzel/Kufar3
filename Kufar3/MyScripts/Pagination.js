var element = $('.pagination ul');
$(function ()
{
    var pageSize = 5;
    var pageCount = Math.ceil(countDeclaration / pageSize);

    if (pageSize < countDeclaration)
    {
        var html;
        var minValue = 2;
        var maxValue = 6;
        if (pageCount < 6)
        {
            maxValue = pageCount;
        }
        
        if (currentPage > 1) {
            html = Url(currentPage - 1, "<<");
            element.append(html);
        }
        
        Append(1, 1, 1);

        if (pageCount > 6)
        {
            if (pageCount - currentPage > 2)
            {
                minValue = currentPage - 2;
                maxValue = currentPage + 2;
            }
            else
            {
                minValue = pageCount - 5;
                maxValue = pageCount;
            }
            Append(minValue - 1, "...");
        }

        for (var i = minValue; i <= maxValue; i++)
        {
            Append(i, i);
        }
        if (pageCount > 6)
        {
            if (pageCount - currentPage < 3 && pageCount > 6)
            {

                html = `<a></a>`;
                element.append(html);
            }
            else
            {
                Append(maxValue + 1, "...");
                Append(pageCount, pageCount);
            }
        }

        if (currentPage < pageCount)
        {
            html = Url(currentPage + 1, ">>");
            element.append(html);
        }

        //$(`.pagination a[name=${currentPage}]`).addClass("darc-cub");
    }
});

function Append(pageNumber, text)
{
    var html = Url(pageNumber, text);
    element.append(html);
}

function Url(pageNumber, text)
{
    var _idCategory = parseInt(idCategory);
    var _idSubcategory = parseInt(idSubcategory);
    var url = `/Home/Index?num=${pageNumber}`;

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

    if (currentPage === pageNumber)
    {
        var html = `<a href="${url}" class = "darc-cub"><li><b>${text}</b></li></a>`;
    }
    else
    {
        var html = `<a href="${url}"><li><b>${text}</b></li></a>`;
    }
    
    return html;
}
