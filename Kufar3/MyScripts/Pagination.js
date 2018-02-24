var element = $('.pagination ul');

$(function ()
{
    var pageSize = 5; // TODO: исправить
    var pageCount = Math.ceil(countDeclaration / pageSize);

    if (pageSize < countDeclaration)
    {
        var minValue = 2;
        var maxValue = 6;
        if (pageCount < 6)
        {
            maxValue = pageCount;
        }
        
        if (currentPage > 1)
        {
            Url(currentPage - 1, "<<");
        }
        
        Url(1, 1);

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

            Url(minValue - 1, "...");
        }

        for (var i = minValue; i <= maxValue; i++)
        {
            Url(i, i);
        }

        if (pageCount > 6)
        {
            if (!(pageCount - currentPage < 3 && pageCount > 6))
            {
                Url(maxValue + 1, "...");
                Url(pageCount, pageCount);
            }
        }

        if (currentPage < pageCount)
        {
            Url(currentPage + 1, ">>");
        }
    }
});

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
    
    element.append(html);
}
