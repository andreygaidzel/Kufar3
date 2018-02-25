class BasePagination {
    constructor(func)
    {
        this.func = func;
        this.container = $('<ul></ul>');
        this.pageCount = Math.ceil(countDeclaration / pageSize);

        this._start();
    }

    _start()
    {
        this._generate();

        $('#pagination').append(this.container);
    }

    _generate()
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
                this._append(currentPage - 1, '<<');
            }

            this._append(1, 1);

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

                this._append(minValue - 1, '...');
            }

            for (var i = minValue; i <= maxValue; i++)
            {
                this._append(i, i);
            }

            if (this.pageCount > 6)
            {
                if (!(this.pageCount - currentPage < 3 && this.pageCount > 6))
                {
                    this._append(maxValue + 1, '...');
                    this._append(this.pageCount, this.pageCount);
                }
            }

            if (currentPage < this.pageCount)
            {
                this._append(currentPage + 1, '>>');
            }
        }
    }

    _append(pageNumber, text)
    {
        var url = this.func(pageNumber);

        var selectedClass = (currentPage === pageNumber) ? 'class="darc-cub"' : '';
        var html = `<li><a href="${url}" ${selectedClass}><b>${text}</b></a></li>`;

        this.container.append(html);
    }
}

class Pagination extends BasePagination {
    constructor(func)
    {
        super(func);
    }

    static urlHome(pageNumber)
    {
        var url = `/Home/Index?page=${pageNumber}`;

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

        return url;
    }

    static urlPersonal(pageNumber)
    {
        return `/Personal/MyDeclarations?page=${pageNumber}&declarationType=${declarationType}`;
    }

    static urlModerator(pageNumber)
    {
        return `/Moderator/DeclarationList?page=${pageNumber}`;
    }
}