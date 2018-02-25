class BasePagination {

    constructor()
    {
        this.element = $('#pagination');
        this.conteiner = $('<ul></ul>');
        this.pageCount = Math.ceil(countDeclaration / pageSize);
    }

    Pages(url)
    {
        if (pageSize < countDeclaration)
        {
            var minValue = 2;
            var maxValue = 6;
            if (this.pageCount < 6)
            {
                maxValue = this.pageCount;
            }

            if (currentPage > 1)
            {
                this.Url(currentPage - 1, '<<');
            }

            this.Url(1, 1);

            if (currentPage > 4 && this.pageCount > 6)
            {
                if (this.pageCount - currentPage > 2)
                {
                    minValue = currentPage - 2;
                    maxValue = currentPage + 2;
                } else
                {
                    minValue = this.pageCount - 5;
                    maxValue = this.pageCount;
                }

                this.Url(minValue - 1, '...');
            }

            for (var i = minValue; i <= maxValue; i++)
            {
                this.Url(i, i);
            }

            if (this.pageCount > 6)
            {
                if (!(this.pageCount - currentPage < 3 && this.pageCount > 6))
                {
                    this.Url(maxValue + 1, '...');
                    this.Url(this.pageCount, this.pageCount);
                }
            }

            if (currentPage < this.pageCount)
            {
                this.Url(currentPage + 1, '>>');
            }
        }
        this.element.append(this.conteiner);
    }

    Url(pageNumber, text)
    {
        var url = `/Home/Index?pageNumber=${pageNumber}`;

        if ((idCategory !== 0) && (idSubcategory === 0))
        {
            url = url + `&idCategory=${idCategory}`;
        } else if ((idCategory === 0) && (idSubcategory !== 0))
        {
            url = url + `&idSubcategory=${idSubcategory}`;
        } else if ((idCategory !== 0) && (idSubcategory !== 0))
        {
            url = url + `&idSubcategory=${idSubcategory}&idCategory=${idCategory}`;
        }

        var selectedClass = (currentPage === pageNumber) ? 'class="darc-cub"' : '';

        var html = `<li><a href="${url}" ${selectedClass}><b>${text}</b></a></li>`;

        this.conteiner.append(html);
    }
}

new BasePagination().Pages();


class BaseTest {
    constructor(callback)
    {
        this.bla(callback);
    }

    bla(callback)
    {
        callback();
    }
}

class Test extends BaseTest {
    constructor(callback)
    {
        super(callback);
    }

    static url1()
    {
        console.log('url1');
    }

    static url2()
    {
        console.log('url2');
    }
}

new Test(Test.url2);