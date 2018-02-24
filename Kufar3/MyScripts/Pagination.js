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
        
        if (currentPage > 1)
        {
            html = Url(currentPage - 1, "<<");
            element.append(html);
        }
        
        Append(1, 1, 1);

        if (currentPage > 4 && pageCount > 6) 
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
    }
});

function Append(pageNumber, text)
{
    var html = Url(pageNumber, text);
    element.append(html);
}

function Url(pageNumber, text)
{
    var url = `/Home/Index?num=${pageNumber}`;

    if ((idCategory !== 0) && (idSubcategory === 0))
    {
        url = url + `&idCategory=${idCategory}`;
    }
    else if ((idCategory === 0) && (idSubcategory !== 0))
    {
        url = url + `&idSubcategory=${idSubcategory}`;
    }
    else if ((idCategory !== 0) && (idSubcategory !== 0))
    {
        url = url + `&idSubcategory=${idSubcategory}&idCategory=${idCategory}`;
    }

    var selectedClass = (currentPage === pageNumber) ? 'class="darc-cub"' : '';

    var html = `<a href="${url}" ${selectedClass}><li><b>${text}</b></li></a>`;

    return html;
}
